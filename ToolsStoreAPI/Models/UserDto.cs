using System.ComponentModel.DataAnnotations;

namespace ToolsStoreAPI.Models
{
    public class UserDto
    {
        [Required, StringLength(20)]
        [RegularExpression("[a-zA-Z0-9]+$", ErrorMessage = "The allowed characters are: A-Z a-z 0-9, max length is 20.")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,20}$", ErrorMessage = "The password must contain 8-20 characters, at least one uppercase letter, one lowercase letter, one number and one special character.")]
        public string Password { get; set; } = string.Empty;
    }
}
