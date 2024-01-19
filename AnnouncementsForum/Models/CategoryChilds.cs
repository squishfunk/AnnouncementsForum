namespace AnnouncementsForum.Models
{
    public class CategoryChilds : Category
    {
        public List<CategoryChilds> Childs { get; set; } = new List<CategoryChilds>();
    }
}
