namespace MVC.Service.Identities;

public record SignInResponseDto(Guid UserId, string UserName, string Email, List<string> Roles);