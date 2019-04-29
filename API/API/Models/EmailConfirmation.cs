using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class EmailConfirmation
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
