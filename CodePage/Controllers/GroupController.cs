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
            if (id != null)
            {
                return View(db.Groups.Include(x => x.Category).Include(x => x.TeacherToGroups).ThenInclude(x=>x.Teacher).Where(x => x.CategoryId == id).ToList());
            }
            return View(db.Groups.Include(x => x.Category).Include(x => x.TeacherToGroups).ToList());
            
        }
    }
}
