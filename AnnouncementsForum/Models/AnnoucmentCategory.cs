using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnnouncementsForum.Models
{
    public class AnnoucmentCategory
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(AnnouncementId))]
        public virtual Announcement Announcement { get; set; }
        [Required]
        public int AnnouncementId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
        [Required]
        public string CategoryId { get; set; }
    }
}
