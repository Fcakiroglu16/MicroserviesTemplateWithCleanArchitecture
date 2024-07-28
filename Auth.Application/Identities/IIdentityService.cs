using Core.ResultDto;

namespace MVC.Service.Identities;

public interface IIdentityService
{
    Task<ServiceResult> SignUp(SignUpDto request);

    Task<ServiceResult<SignInResponseDto>> SignIn(SignInDto request);

    Task<ServiceResult> AddRoleToUser(Guid userId, string roleName);
}