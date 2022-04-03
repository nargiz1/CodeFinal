using CodePage.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GradeController : Controller
    {
        private readonly AppDbContext db;
        public GradeController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Add(int? StudentId)
        {
            ViewBag.Student = db.Students.FirstOrDefault(x => x.Id == StudentId);
            return View();
        }
    }
}
