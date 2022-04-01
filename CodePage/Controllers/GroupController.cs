using CodePage.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Controllers
{
    public class GroupController : Controller
    {
        private readonly AppDbContext db;
        public GroupController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index(int? id)
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Category = id;
            return View(db.Groups.Include(x=> x.Category).Where(x=> x.CategoryId==id).ToList());
        }
    }
}
