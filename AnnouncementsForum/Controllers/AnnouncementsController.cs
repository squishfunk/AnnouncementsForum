using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnnouncementsForum.Models;
using AnnouncementsForum.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using static Azure.Core.HttpHeader;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AnnouncementsForum.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly DBContext _context;
        private readonly IAnnoucmentsService _annoucmentsService;
        private readonly ICategoryService _categoryService;
        private readonly IForbiddenWordsService _forbiddenWordsService;
        private readonly IHtmlTagsService _htmlTags;

        public AnnouncementsController(DBContext context, IAnnoucmentsService annoucmentsService, ICategoryService categoryService, IForbiddenWordsService forbiddenWordsService, IHtmlTagsService htmlTags)
        {
            _context = context;
            _annoucmentsService = annoucmentsService;
            _categoryService = categoryService;
            _forbiddenWordsService = forbiddenWordsService;
            _htmlTags = htmlTags;
        }

        // GET: Announcements
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Announcements.ToListAsync());
        }
        public async Task<IActionResult> AnnoucmentsPagination(int? page, string? search)
        {
            var pagin = new PaginAnnoucmentModel()
            {
                Page = page ?? 0,
                SearchString = search ?? string.Empty
            };
            var annoucments = _annoucmentsService.GetPagin(pagin);
            return View(annoucments);
        }

        // GET: Announcements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            announcement.Views = announcement.Views + 1;
            _context.Announcements.Update(announcement);
            await _context.SaveChangesAsync();

            return View(announcement);
        }

        // GET: Announcements/Create
        [Authorize]
        public IActionResult Create()
        {
            var categories = _categoryService.GetSortedCategories();
            var announcment = new Announcement();
            if(TempData["Errors"] != null)
            {
                ModelState.AddModelError("ForbiddenWords", TempData["Errors"] as string);
            }
            
            return View((announcment, categories));
        }

        // POST: Announcements/Create
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Announcement announcement, string[] categoryId)
        {
            var forbiddenWords = _forbiddenWordsService.GetAllForbiddenWordsInString(announcement.Description);
            var forbiddenTags = _htmlTags.GetListOfForbiddenTags(announcement.Description);
            if (forbiddenTags.Count > 0)
            {
                var result = String.Join(", ", forbiddenTags.ToArray());
                TempData["Errors"] = "Nie możesz używać tych tagów html: " + result;
                return RedirectToAction("Create", "Announcements");
            }
            if (forbiddenWords.Count > 0)
            {
                var result = String.Join(", ", forbiddenWords.ToArray());
                TempData["Errors"] = "Nie możesz używać tych wyrazów: " + result;
                return RedirectToAction("Create", "Announcements");
            }
            announcement.CreateDate = DateTime.Now;
            announcement.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Add(announcement);
            await _context.SaveChangesAsync();
            _categoryService.LinkCategoriesToAnnounments(announcement.Id, categoryId);
            return RedirectToAction("Details", "Announcements", new {id = announcement.Id });
        }

        // GET: Announcements/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (announcement.UserId != userId)
            {
                return RedirectToAction("Index", "Home");
            }
            if (TempData["Errors"] != null)
            {
                ModelState.AddModelError("ForbiddenWords", TempData["Errors"] as string);
            }
            return View(announcement);
        }

        // POST: Announcements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(Announcement announcement)
        {
            var forbiddenWords = _forbiddenWordsService.GetAllForbiddenWordsInString(announcement.Description);
            var forbiddenTags = _htmlTags.GetListOfForbiddenTags(announcement.Description);
            if (forbiddenTags.Count > 0)
            {
                var result = String.Join(", ", forbiddenTags.ToArray());
                TempData["Errors"] = "Nie możesz używać tych tagów html: " + result;
                return RedirectToAction("Edit", "Announcements");
            }
            if (forbiddenWords.Count > 0)
            {
                var result = String.Join(", ", forbiddenWords.ToArray());
                TempData["Errors"] = "Nie możesz używać tych wyrazów: " + result;
                return RedirectToAction("Edit", "Announcements");
            }
            announcement.UpdateDate = DateTime.Now;
            _context.Update(announcement);
            await _context.SaveChangesAsync();
            return RedirectToAction("Profile", "Account");
        }

        // GET: Announcements/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }
            return View(announcement);
        }

        // POST: Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement != null)
            {
                _context.Announcements.Remove(announcement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Profile", "Account");
        }

        private bool AnnouncementExists(int id)
        {
            return _context.Announcements.Any(e => e.Id == id);
        }
    }
}
