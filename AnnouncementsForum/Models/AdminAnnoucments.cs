using System.ComponentModel.DataAnnotations;

namespace AnnouncementsForum.Models
{
    public class AdminAnnoucments
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
