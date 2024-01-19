using AnnouncementsForum.Services.Interfaces;
using AnnouncementsForum.Models;

namespace AnnouncementsForum.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DBContext _context;
        public CategoryService(DBContext context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public List<CategoryChilds> GetSortedCategories()
        {
            var categories = _context.Categories.ToList();
            return SortCategories(ChangeCategoryToCategoryChilds(categories));
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.Find(id);
        }

        public Category Create(Category category)
        {
            if (category.Parent == null)
                category.Parent = String.Empty;
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }
        public async void LinkCategoriesToAnnounments(int annoucmentId, string[] categoriesIds)
        {
            foreach (var category in categoriesIds)
            {
                var AnnoucmentCategory = new AnnoucmentCategory();
                AnnoucmentCategory.AnnouncementId = annoucmentId;
                AnnoucmentCategory.CategoryId = category;
                _context.AnnoucmentCategory.Add(AnnoucmentCategory);
            }
            await _context.SaveChangesAsync();
        }
        private List<CategoryChilds> ChangeCategoryToCategoryChilds(List<Category> categories)
        {
            List<CategoryChilds> categoriesChilds = new List<CategoryChilds>();
            foreach (var category in categories)
            {
                CategoryChilds categoryChild = new CategoryChilds();
                categoryChild.Id = category.Id;
                categoryChild.Name = category.Name;
                categoryChild.Parent = category.Parent;
                categoriesChilds.Add(categoryChild);
            }

            return categoriesChilds;
        }

        private List<CategoryChilds> SortCategories(List<CategoryChilds> categories)
        {
            var categoriesMap = categories.ToDictionary(category => category.Id);
            var parents = new List<CategoryChilds>();
            foreach (var category in categories)
            {
                if (category.Parent != String.Empty)
                    categoriesMap[category.Parent].Childs.Add(category);
                else
                    parents.Add(category);
            }
            return parents;
        }
    }
}
