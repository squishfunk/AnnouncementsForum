using System.ComponentModel.DataAnnotations;

namespace AnnouncementsForum.Models
{
    public class HtmlTags
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsAllowed { get; set; }
    }
}
