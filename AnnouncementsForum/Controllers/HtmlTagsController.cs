using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AnnouncementsForum.Models;
using AnnouncementsForum.Services.Interfaces;

namespace AnnouncementsForum.Controllers
{
    public class HtmlTagsController : Controller
    {
        private readonly DBContext _context;
        public HtmlTagsController(DBContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Change(HtmlTags tag)
        {
            _context.HtmlTags.Update(tag);
            _context.SaveChanges();
            return RedirectToAction("HtmlTags", "Admin");
        }
    }
}
