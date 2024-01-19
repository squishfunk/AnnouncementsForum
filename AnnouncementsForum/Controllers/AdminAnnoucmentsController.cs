using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AnnouncementsForum.Models;

namespace AnnouncementsForum.Controllers
{
    public class AdminAnnoucmentsController : Controller
    {
        private readonly DBContext _context;

        public AdminAnnoucmentsController(DBContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create(AdminAnnoucments annoucment)
        {
            _context.AdminAnnoucments.Add(annoucment);
            _context.SaveChanges();
            return RedirectToAction("AdminAnnoucments", "Admin");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(AdminAnnoucments annoucment)
        {
            _context.AdminAnnoucments.Remove(annoucment);
            _context.SaveChanges();
            return RedirectToAction("AdminAnnoucments", "Admin");
        }
        // route
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            return View(_context.AdminAnnoucments.Find(id));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult EditChange(AdminAnnoucments annoucment)
        {
            _context.AdminAnnoucments.Update(annoucment);
            _context.SaveChanges();
            return RedirectToAction("AdminAnnoucments", "Admin");
        }
    }
}
