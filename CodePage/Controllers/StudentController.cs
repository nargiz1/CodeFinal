using CodePage.DAL;
using CodePage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext db;
        public StudentController(AppDbContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Group");
            ViewBag.Group = db.Groups.Include(x=> x.Category).FirstOrDefault(x => x.Id == id);
            return View(await db.Students.Where(x=> x.GroupId==id).ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Group");
            return View(await db.Students.Include(x => x.Group).Include(x=> x.Group.Category).FirstOrDefaultAsync(x => x.Id == id));
        }
    }
}
