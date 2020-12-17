using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dto
{
    public class UserLoginForDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8,MinimumLength = 4, ErrorMessage = "You must specify password .")]
        public string Password { get; set; }
    
    }
}