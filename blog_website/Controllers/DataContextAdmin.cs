using Microsoft.AspNetCore.Mvc;
using blog_website.Models.classes;
using blog_website.Data;
using System.ComponentModel.DataAnnotations;

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
        public IActionResult GetTable()
        {
            IEnumerable<Admin> objAdminList = _db.Admins.ToList();
            return View(objAdminList);
        }
        public IActionResult Create()
        {
            Console.WriteLine("GET Create");
            ModelState.Clear(); // Clear the ModelState
            return View(new Admin()); // Pass a new Admin object to the view
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Admin objAdmin)
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
    }
}
