using System.ComponentModel.DataAnnotations;

namespace ToolsStoreAPI.Models
{
    public class User
    {
        public User(string username, byte[] passwordHash, byte[] passwordSalt)
        {
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            LastLogIn = DateTime.Now;
        }

        [Key]
        public int UserId { get; set; }

        [Required, StringLength(20)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public DateTime LastLogIn { get; set; }
    }
}
