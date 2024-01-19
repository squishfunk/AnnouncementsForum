using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AnnouncementsForum.Models;

namespace AnnouncementsForum.Controllers
{
    public class ForbiddenWordsController : Controller
    {
        private readonly DBContext _context;

        public ForbiddenWordsController(DBContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ForbiddenWords word)
        {
            _context.ForbiddenWords.Add(word);
            await _context.SaveChangesAsync();
            return RedirectToAction("ForbiddenWords", "Admin");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(ForbiddenWords word)
        {
            _context.ForbiddenWords.Remove(word);
            await _context.SaveChangesAsync();
            return RedirectToAction("ForbiddenWords", "Admin");
        }
    }
}
