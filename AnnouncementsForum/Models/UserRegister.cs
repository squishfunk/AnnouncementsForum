using System.ComponentModel.DataAnnotations;

namespace AnnouncementsForum.Models
{
    public class UserRegister
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
