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
            IEnumerable<Blog> objScriptList = _db.Blogs.Include(s => s.User).ToList();
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
            _db.Blogs.Add(objScript);
            _db.SaveChanges();
            return View();
        }
        private int GetCurrentAdminId()
        {
            // Logic to get the current logged-in admin's ID
            string? name = User.Identity.Name;
            var user = _db.Users.First(dto => dto.Name == name);
            if (user == null)
            {
                // Handle the case where the user is not found
                throw new Exception("User not found");
            }
            return user.Id;
        }
        public IActionResult Details(int id)
        {
            var script = _db.Blogs.Include(s => s.User).FirstOrDefault(s => s.Id == id);
            if (script == null)
            {
                return NotFound();
            }
            return View(script);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var BlogFromDb = _db.Blogs.Find(id);
            if (BlogFromDb == null)
            {
                return NotFound();
            }
            return View(BlogFromDb);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog obj)
        {
            if (ModelState.IsValid)
            {
                //// Check if UserId exists
                //var userExists = _db.Users.Any(u => u.Id == obj.UserId);
                //if (!userExists)
                //{
                //    ModelState.AddModelError("", "User does not exist.");
                //    return View(obj);
                //}
                _db.Blogs.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var BlogFromDb = _db.Blogs.Find(id);
            if (BlogFromDb == null)
            {
                return NotFound();
            }
            return View(BlogFromDb);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Blog obj)
        {
            _db.Blogs.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
