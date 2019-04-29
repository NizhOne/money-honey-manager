using API.Models.Domain;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IEmailService
    {
        Task SendConfirmationMail(ApplicationUser user, string code);
    }
}