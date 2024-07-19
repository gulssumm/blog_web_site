using Microsoft.AspNetCore.Mvc;
using blog_website.Models.classes;
using blog_website.Data;
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
            IEnumerable<User> objAdminList = _db.Users.ToList();
            return View(objAdminList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User objUser)
        {
            Console.WriteLine("POST Create"); // Log to check if the POST method is hit
            Console.WriteLine($"Name: {objUser.Name}, Password: {objUser.Password}"); // Log form values
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        ModelState.AddModelError("", $"User with ID {error.ErrorMessage}\n does not add.");
                    }
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Users.Add(objUser);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Home"); // Redirect to Home/Index
                }
                catch (Exception ex)
                {
                    // Add error message to ModelState
                    ModelState.AddModelError("Name", "Name already exists.");
                }
            }
            return View(objUser);
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User objUser)
        {
            var user = _db.Users.SingleOrDefault(a => a.Name == objUser.Name && a.Password == objUser.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(objUser);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "DataContextAdmin");
        }
    }
}
