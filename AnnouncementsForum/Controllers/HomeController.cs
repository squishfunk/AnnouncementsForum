using Microsoft.AspNetCore.Mvc;
using AnnouncementsForum.Models;
using AnnouncementsForum.Services.Interfaces;
using System.Diagnostics;

namespace AnnouncementsForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdminAnnoucmentsService _adminAnnoucments;

        public HomeController(ILogger<HomeController> logger, IAdminAnnoucmentsService adminAnnoucments)
        {
            _logger = logger;
            _adminAnnoucments = adminAnnoucments;
        }

        public IActionResult Index()
        {
            return View(_adminAnnoucments.GetAll());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
