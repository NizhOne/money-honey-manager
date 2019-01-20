using API.Models;

namespace API.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(ApplicationUser user);
    }
}