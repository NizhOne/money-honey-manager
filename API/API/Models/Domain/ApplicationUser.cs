using Microsoft.AspNetCore.Identity;

namespace API.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
