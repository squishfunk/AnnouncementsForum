using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using AnnouncementsForum.Models;
using System.Security.Claims;

namespace AnnouncementsForum.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly DBContext _context;

        public AccountController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, DBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public async Task<ActionResult> Profile()
        {
            if(User.FindFirstValue(ClaimTypes.Role) == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userAnnoucments = _context.Announcements.Where(A => A.UserId.Contains(userId));
            return View(userAnnoucments);
        }

        [HttpPost]
        public async Task<ActionResult> Login(Login userLoginData)
        {
            if (!ModelState.IsValid)
            {
                return View(userLoginData);
            }
            else
            {
                await _signInManager.PasswordSignInAsync(userLoginData.UserName, userLoginData.Password, false, false);

                return RedirectToAction("Index", "Home");
            }            
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserRegister userRegisterData)
        {
            if (!ModelState.IsValid)
            {
                return View(userRegisterData);
            }

            var newUser = new UserModel
            {
                Email = userRegisterData.Email,
                UserName = userRegisterData.UserName
            };

            await _userManager.CreateAsync(newUser, userRegisterData.Password);

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
