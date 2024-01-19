using Microsoft.AspNetCore.Mvc;
using AnnouncementsForum.Models;
using AnnouncementsForum.Services.Interfaces;

namespace AnnouncementsForum.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _category;
        public CategoryController(ICategoryService category) { 
            _category = category;
        }
        public IActionResult Create(Category category)
        {
            _category.Create(category);
            return RedirectToAction("Category", "Admin");
        }
    }
}
