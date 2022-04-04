using CodePage.DAL;
using CodePage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CodePage.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Teacher")]
    public class GradeController : Controller
    {
        private readonly AppDbContext db;
        private readonly SignInManager<AppUser> signInManager;

        public GradeController(AppDbContext _db, SignInManager<AppUser> _signInManager)
        {
            db = _db;
            signInManager = _signInManager;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Student");
            return View(await db.Students.Include(x=> x.Grades).FirstOrDefaultAsync(x => x.Id == id));
        }
        public IActionResult Add()
        {
            ViewData["StudentId"] = new SelectList(db.Students, "Id", "FullName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Grade grade)
        {
            if (ModelState.IsValid)
            {
                if(grade.Mark<=100 && grade.Mark >= 0)
                {
                    grade.CreateDate = DateTime.Now;
                    grade.UpdateDate = DateTime.Now;
                    grade.TeacherId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    await db.Grades.AddAsync(grade);
                    await db.SaveChangesAsync();
                    Student student =await db.Students.FirstOrDefaultAsync(x => x.Id == grade.StudentId);
                    int count = db.Grades.Where(x => x.StudentId == grade.StudentId).Count();
                    student.CAP = ((student.CAP*(count-1) + grade.Mark)/count);
                    db.Students.Update(student);
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("Mark", "Mark cannot be more than 100 or less than 0");
                    ViewData["StudentId"] = new SelectList(db.Students, "Id", "FullName");
                    return View();
                }
            }
            return RedirectToAction("Index", "Student");
        }
    }
}
