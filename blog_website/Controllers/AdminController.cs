using Microsoft.AspNetCore.Mvc;
using blog_website.Models.classes;
using blog_website.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace blog_website.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbCon _db;
        public AdminController(ApplicationDbCon db)
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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Admin objAdmin)
        {
            _db.Admins.Add(objAdmin);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
