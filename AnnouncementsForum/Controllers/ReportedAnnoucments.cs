using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AnnouncementsForum.Models;
using AnnouncementsForum.Services.Interfaces;
using System.Security.Claims;

namespace AnnouncementsForum.Controllers
{
    public class ReportedAnnoucments : Controller
    {
        private readonly DBContext _context;
        private readonly IReportedAnnoucment _reportedA;

        public ReportedAnnoucments(DBContext context, IReportedAnnoucment reportedA)
        {
            _context = context;
            _reportedA = reportedA;
        }
        [Authorize]
        public IActionResult Index(int id)
        {
            ReportedAnnoucment reportedAnnoucment = new ReportedAnnoucment();
            reportedAnnoucment.AnnouncementId = id;
            return View(reportedAnnoucment);
        }
        [Authorize]
        public async Task<IActionResult> Create(ReportedAnnoucment reportedAnnoucment)
        {
            reportedAnnoucment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.ReportedAnnoucments.Add(reportedAnnoucment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult RemovePost(ReportedAnnoucmentModel reportedAnnoucmentModel)
        {
            _reportedA.RemovePost(reportedAnnoucmentModel);
            return RedirectToAction("ReportedAnnoucments", "Admin");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveReport(ReportedAnnoucmentModel reportedAnnoucmentModel)
        {
            _reportedA.RemoveReport(reportedAnnoucmentModel);
            return RedirectToAction("ReportedAnnoucments", "Admin");
        }
    }
}
