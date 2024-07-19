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
            int currentUserId = 0;
            if (GetCurrentUserId() != null)
            {
                currentUserId = GetCurrentUserId();
            }
            IEnumerable<Blog> objScriptList = _db.Blogs.Include(s => s.User).ToList();

            ViewBag.CurrentUserId = currentUserId;

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
            try
            {

                // Set the UserId based on the currently logged-in user
                objScript.UserId = GetCurrentUserId();

                // Verify the UserId exists in the Users table
                var userExists = _db.Users.Any(u => u.Id == objScript.UserId);
                if (!userExists)
                {
                    ModelState.AddModelError("", "User does not exist.");
                    return View(objScript);
                }

                if (ModelState.IsValid)
                {
                    _db.Blogs.Add(objScript);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }
            return View(objScript);
        }


        private int GetCurrentUserId()
        {
            // Logic to get the current logged-in admin's ID
            string? name = User.Identity.Name;
            if (string.IsNullOrEmpty(name))
            {
                return 0;
            }
            var user = _db.Users.FirstOrDefault(dto => dto.Name == name);
            if (user == null)
            {
                // Log or debug
                throw new Exception($"User with name '{name}' not found");
            }
            if (user.Id == 0)
            {
                throw new Exception("Retrieved user ID is 0, which is invalid.");
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
            var currentUserId = GetCurrentUserId();
            if (currentUserId == 0)
            {
                return NotFound();
            }
            var blogFromDb = _db.Blogs.Find(id);
            if (blogFromDb == null)
            {
                return NotFound();
            }
            // Verify the UserId exists in the Users table
            var userExists = _db.Users.Any(u => u.Id == blogFromDb.UserId);
            if (!userExists)
            {
                ModelState.AddModelError("", "User does not exist.");
                return View("Error");
            }
            if (blogFromDb.UserId != currentUserId)
            {
                return Forbid(); // or redirect to an error page
            }

            return View(blogFromDb);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog obj)
        {
            if (ModelState.IsValid)
            {
                // Verify the UserId exists in the Users table
                var userExists = _db.Users.Any(u => u.Id == obj.UserId);
                if (!userExists)
                {
                    ModelState.AddModelError("", $"User with ID {obj.UserId} does not exist.");
                    return View(obj);
                }
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
            var blogFromDb = _db.Blogs.Find(id);
            if (blogFromDb == null)
            {
                return NotFound();
            }
            // Check if the current user is the owner of the blog post
            var currentUserId = GetCurrentUserId();
            if (blogFromDb.UserId != currentUserId)
            {
                return Forbid(); // or redirect to an error page
            }
            return View(blogFromDb);
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
