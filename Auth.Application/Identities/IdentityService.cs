using System.Net;
using Auth.Domain;
using Core.ResultDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MVC.Service.Identities;

public class IdentityService(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager,
    ILogger<IdentityService> logger) : IIdentityService
{
    public async Task<ServiceResult> SignUp(SignUpDto request)
    {
        var hasUser = await userManager.FindByEmailAsync(request.Email);

        if (hasUser is not null)
        {
            logger.LogInformation("Email adresi olmadığı için kullanıcı bulunamadı");
            return ServiceResult.Fail("Email adresi başka bir kullanıcı tarafından kullanılıyor",
                HttpStatusCode.BadRequest);
        }

        hasUser = await userManager.FindByNameAsync(request.UserName);


        if (hasUser is not null)
            return ServiceResult.Fail("Kullanıcı adı başka bir kullanıcı tarafından kullanılıyor",
                HttpStatusCode.BadRequest);


        var newUser = AppUser.Create(request.Email, request.UserName);
        var result = await userManager.CreateAsync(newUser, request.Password);


        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description).ToList();

            return ServiceResult.Fail(errors, HttpStatusCode.BadRequest);
        }


        var httpClient = new HttpClient();
        var httpResult = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos/1");

        logger.LogInformation("Kullanıcı oluştu");
        return ServiceResult.Success(HttpStatusCode.Created);
    }

    public async Task<ServiceResult<SignInResponseDto>> SignIn(SignInDto request)
    {
        var hasUser = await userManager.FindByEmailAsync(request.Email);


        if (hasUser is null)
            return ServiceResult<SignInResponseDto>.Fail("Email veya şifre hatalı", HttpStatusCode.NotFound);

        var checkPassword = await userManager.CheckPasswordAsync(hasUser, request.Password);


        if (!checkPassword)
            return ServiceResult<SignInResponseDto>.Fail("Email veya şifre hatalı", HttpStatusCode.NotFound);


        var roles = await userManager.GetRolesAsync(hasUser);


        var signInResponseDto =
            new SignInResponseDto(hasUser.Id, hasUser.UserName!, hasUser.Email!, roles.ToList());


        return ServiceResult<SignInResponseDto>.Success(signInResponseDto, HttpStatusCode.OK);
    }

    public async Task<ServiceResult> AddRoleToUser(Guid userId, string roleName)
    {
        var hasUser = await userManager.FindByIdAsync(userId.ToString());

        if (hasUser is null) return ServiceResult.Fail("Kullanıcı bulunamadı", HttpStatusCode.NotFound);

        roleName = roleName.ToLower();
        var hasRole = await roleManager.FindByNameAsync(roleName);
        if (hasRole is null) await roleManager.CreateAsync(new AppRole { Name = roleName });

        var result = await userManager.AddToRoleAsync(hasUser, roleName);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description).ToList();

            return ServiceResult.Fail(errors, HttpStatusCode.BadRequest);
        }

        return ServiceResult.Success(HttpStatusCode.OK);
    }
}