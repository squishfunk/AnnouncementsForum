using AnnouncementsForum.Models;
namespace AnnouncementsForum.Services.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        List<CategoryChilds> GetSortedCategories();
        Category GetCategoryById(int id);
        Category Create(Category category);
        void LinkCategoriesToAnnounments(int annoucmentId, string[] categoriesIds);
    }
}
