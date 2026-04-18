using LearnNow.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LearnNow.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, SignInManager<IdentityUser> sim,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _signInManager = sim;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
        [Route("login")]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login (string email, string password)
        {
            var result=await _signInManager.PasswordSignInAsync(email, password, isPersistent:false, lockoutOnFailure:false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }
        public async Task<IActionResult> Index()
        {
            if (User.Identity!.IsAuthenticated)
            {
                var user=await _userManager.GetUserAsync(User);
                var roles=await _userManager.GetRolesAsync(user!);

                if (roles.Contains("Admin")||roles.Contains("Teacher"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                if (roles.Contains("Trainee"))
                {
                    return RedirectToAction("Index", "TrainingEvent");
                }
                return RedirectToAction("Login");
               
            }
                return RedirectToAction("Login");
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
