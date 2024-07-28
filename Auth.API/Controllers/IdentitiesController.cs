using Core;
using Microsoft.AspNetCore.Mvc;
using MVC.Service.Identities;

namespace Auth.API.Controllers;

public class IdentitiesController(IIdentityService identityService) : CustomControllerBase
{
    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp(SignUpDto request)
    {
        return CreateResult(await identityService.SignUp(request));
    }


    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn(SignInDto request)
    {
        return CreateResult(await identityService.SignIn(request));
    }


    [HttpPost("AddRoleToUser/{userId:guid}/{roleName}")]
    public async Task<IActionResult> AddRoleToUser(Guid userId, string roleName)
    {
        return CreateResult(await identityService.AddRoleToUser(userId, roleName));
    }
}