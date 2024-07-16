using blog_website.Data;
using blog_website.Models;
using blog_website.Models.classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            return View();
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Script objScript)
        {
            _db.Scripts.Add(objScript);
            _db.SaveChanges();
            return View();
        }
        public IActionResult GetScript()
        {
            IEnumerable<Script> objScriptList = _db.Scripts.ToList();
            return View(objScriptList);
        }
    }
}
