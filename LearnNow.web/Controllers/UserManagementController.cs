using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace LearnNow.web.Controllers
{
   
    public class UserManagementController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public  IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string email, string password, string role)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
                {
                ModelState.AddModelError(string.Empty, "All fields are requiered.");
                return View();
            }

            var user = new IdentityUser { UserName = email, Email = email };
            var result =await _userManager.CreateAsync(user, password);
            if(result.Succeeded)
            {
                if(await _roleManager.RoleExistsAsync(role))
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Role does not exist");
                    return View();
                }
                return RedirectToAction("Index");
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }


        // GET: UserManagementController
        public  async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRoles=new Dictionary<string, string>();

            foreach (var user in users)
            {
                var roles=await _userManager.GetRolesAsync(user);
                userRoles[user.Email!]=string.Join(",", roles);
            }
            ViewBag.UserRoles = userRoles;
            return View(users);
        }

        // GET: UserManagementController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserManagementController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserManagementController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserManagementController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserManagementController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserManagementController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserManagementController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user!=null)
            {
                var result=await _userManager.DeleteAsync(user);
                if(result.Succeeded)
                {
                    return RedirectToAction("Idex");
                }
            }
            ModelState.AddModelError("", "Error deleting user.");
            return View();
        }
    }
}
