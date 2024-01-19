using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AnnouncementsForum.Models
{
    public class ReportedAnnoucment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(AnnouncementId))]
        public virtual Announcement Announcement { get; set; }
        [Required]
        public int AnnouncementId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual UserModel UserModel { get; set; }
        [Required]
        public String UserId { get; set; }
        [Required]
        public String Description { get; set; }
    }
}
