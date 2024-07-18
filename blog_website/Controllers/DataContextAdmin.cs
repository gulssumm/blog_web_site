using Microsoft.AspNetCore.Mvc;
using blog_website.Models.classes;
using blog_website.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace blog_website.Controllers
{
    
    public class DataContextAdmin : Controller
    {
        private readonly ApplicationDbCon _db;
        public DataContextAdmin(ApplicationDbCon db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult GetTable()
        {
            IEnumerable<User> objAdminList = _db.Admins.ToList();
            return View(objAdminList);
        }
        public IActionResult Create()
        {
            Console.WriteLine("GET Create");
            ModelState.Clear(); // Clear the ModelState
            return View(new User()); // Pass a new Admin object to the view
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User objAdmin)
        {
            Console.WriteLine("POST Create"); // Log to check if the POST method is hit
            Console.WriteLine($"Name: {objAdmin.Name}, Password: {objAdmin.Password}"); // Log form values
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Admins.Add(objAdmin);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Home"); // Redirect to Home/Index
                }
                catch (Exception ex)
                {
                    // Add error message to ModelState
                    ModelState.AddModelError("Name", "Name already exists.");
                }
            }
            return View(objAdmin);
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User objAdmin)
        {
            var admin = _db.Admins.SingleOrDefault(a => a.Name == objAdmin.Name && a.Password == objAdmin.Password);
            if (admin != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, admin.Name)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(objAdmin);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "DataContextAdmin");
        }
    }
}
