using bauen.Utils;
using CodePage.DAL;
using CodePage.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment env;
        public StudentController(AppDbContext _db, IWebHostEnvironment _env)
        {
            db = _db;
            env = _env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Students.Include(x=>x.Group).ToListAsync());
        }
        public IActionResult Add()
        {
            ViewData["GroupId"] = new SelectList(db.Groups, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Student student)
        {
            if (ModelState.IsValid)
            {
                Student duplicate = await db.Students.FirstOrDefaultAsync(x => x.Mail == student.Mail);
                if (duplicate == null)
                {
                    student.CreateDate = DateTime.Now;
                    student.UpdateDate = DateTime.Now;
                    student.Image = await student.ImageFile.Upload(env.WebRootPath, @"img/students");
                    await db.Students.AddAsync(student);
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index", "Student");
        }
        public async Task<IActionResult> Details(int? id)
        {
            return View(await db.Students.Include(x => x.Group).FirstOrDefaultAsync(x=> x.Id==id));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["GroupId"] = new SelectList(db.Groups, "Id", "Name");
            return View(await db.Students.Include(x => x.Group).FirstOrDefaultAsync(x => x.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                student.UpdateDate = DateTime.Now;
                if (student.ImageFile != null)
                {
                    student.Image = await student.ImageFile.Upload(env.WebRootPath, @"img/students");
                }
                db.Students.Update(student);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Student");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return View(await db.Students.Include(x => x.Group).FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<IActionResult>  DeleteConfirmed(int? id)
        {
            if (!ModelState.IsValid) RedirectToAction("Index", "Student");
            Student student = await db.Students.FirstOrDefaultAsync(x => x.Id == id);
            db.Students.Remove(student);
            student.Image = await student.ImageFile.Upload(env.WebRootPath, @"img/students");
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Student");
        }
    }
}
