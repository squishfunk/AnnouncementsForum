using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnnouncementsForum.Models
{
    public class Announcement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual UserModel User { get; set; }
        public string UserId { get; set; }
        public int Views { get; set; } = 0;
    }
}
