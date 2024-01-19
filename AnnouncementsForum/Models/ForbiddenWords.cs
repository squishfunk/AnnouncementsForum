using System.ComponentModel.DataAnnotations;

namespace AnnouncementsForum.Models
{
    public class ForbiddenWords
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
    }
}
