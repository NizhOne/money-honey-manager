using API.Models;
using API.Models.Domain;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(ApplicationUser user);
        Task<FacebookUserData> GetFacebookUserInfoAsync(string accessToken);
    }
}