using Microsoft.AspNetCore.Identity;

namespace Auth.Domain;

public class AppUser : IdentityUser<Guid>
{
    public static AppUser Create(string email, string userName)
    {
        return new AppUser
        {
            Email = email,
            UserName = userName
        };
    }
}