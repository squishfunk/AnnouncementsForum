using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AnnouncementsForum.Models;
using AnnouncementsForum.Services.Interfaces;

namespace AnnouncementsForum.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICategoryService _category;
        private readonly IForbiddenWordsService _forbiddenWords;
        private readonly IReportedAnnoucment _reportedA;
        private readonly IHtmlTagsService _htmlTags;
        private readonly IAdminAnnoucmentsService _adminAnnoucments;

        public AdminController(ICategoryService category,
            IForbiddenWordsService forbiddenWords, 
            IReportedAnnoucment reportedA, 
            IHtmlTagsService htmlTags, 
            IAdminAnnoucmentsService adminAnnoucments)
        {
            _category = category;
            _forbiddenWords = forbiddenWords;
            _reportedA = reportedA;
            _htmlTags = htmlTags;
            _adminAnnoucments = adminAnnoucments;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Category()
        {
            ViewData["Categories"] = _category.GetSortedCategories();
            ViewData["CategoriesNotSorted"] = _category.GetAll();
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult ForbiddenWords()
        {
            ViewData["ForbiddenWords"] = _forbiddenWords.GetAll();
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult ReportedAnnoucments()
        {
            ViewData["ReportedAnnoucments"] = _reportedA.GetAll();
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult HtmlTags()
        {
            return View(_htmlTags.GetAll());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AdminAnnoucments()
        {
            return View(_adminAnnoucments.GetAll());
        }

    }
}
