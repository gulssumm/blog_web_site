using blog_website.Data;
using blog_website.Models;
using blog_website.Models.classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace blog_website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbCon _db;
        public HomeController(ApplicationDbCon db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Blog> objScriptList = _db.Scripts.Include(s => s.User).ToList();
            return View(objScriptList);
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
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog objScript)
        {
            // Set the AdminId based on the currently logged-in admin
            // Assuming you have a method to get the current admin id
            objScript.UserId = GetCurrentAdminId();
            _db.Scripts.Add(objScript);
            _db.SaveChanges();
            return View();
        }
        private int GetCurrentAdminId()
        {
            // Logic to get the current logged-in admin's ID
            string? name = User.Identity.Name;
            return _db.Admins.First(dto => dto.Name == name).Id;
        }
        public IActionResult Details(int id)
        {
            var script = _db.Scripts.Include(s => s.User).FirstOrDefault(s => s.Id == id);
            if (script == null)
            {
                return NotFound();
            }
            return View(script);
        }

    }
}
