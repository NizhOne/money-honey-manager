using API.Models;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmationMail(ApplicationUser user, string code);
    }
}