using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnnouncementsForum.Models
{
    public class Category
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Parent { get; set; } = string.Empty;
    }
}
