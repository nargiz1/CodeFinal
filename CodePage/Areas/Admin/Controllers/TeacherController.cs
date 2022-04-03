using CodePage.DAL;
using CodePage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext db;
        public TeacherController(AppDbContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Teachers.ToListAsync());
        }
        public IActionResult Add()
        {
            ViewData["Groups"] = db.Groups.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Teacher teacher, string groupIds)
        {
            if (ModelState.IsValid)
            {
                Teacher duplicate = await db.Teachers.FirstOrDefaultAsync(x => x.Mail == teacher.Mail);
                if (duplicate == null)
                {
                    teacher.CreateDate = DateTime.Now;
                    teacher.UpdateDate = DateTime.Now;
                    await db.Teachers.AddAsync(teacher);
                    await db.SaveChangesAsync();
                }
                if (!string.IsNullOrEmpty(groupIds))
                {
                    string[] gIds = groupIds.Split(",");
                    Array.Resize(ref gIds, gIds.Length - 1);
                    foreach (string gId in gIds)
                    {
                        TeacherToGroup ttg = new TeacherToGroup();
                        ttg.GroupId = Convert.ToInt32(gId);
                        ttg.TeacherId = teacher.Id;
                        await db.TeacherToGroups.AddAsync(ttg);
                    }
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index", "Teacher");
        }
        public async Task<IActionResult> Details(int? id)
        {
            return View(await db.Teachers.Include(x=> x.TeacherToGroups).FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return View(await db.Teachers.FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (!ModelState.IsValid) RedirectToAction("Index", "Teacher");
            Teacher teacher = await db.Teachers.FirstOrDefaultAsync(x => x.Id == id);
            db.Teachers.Remove(teacher);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Teacher");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Groups"] = db.Groups.ToList();
            List<TeacherToGroup> ttg = db.TeacherToGroups.Where(x => x.TeacherId == id).ToList();
            string groups = "";
            foreach(TeacherToGroup item in ttg)
            {
                groups+=item.GroupId.ToString()+ ",";
            }
            ViewBag.TeacherToGroup = groups;
            return View(await db.Teachers.Include(x =>x.TeacherToGroups).FirstOrDefaultAsync(x => x.Id == id));

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Teacher teacher, string groupIds)
        {
            if (ModelState.IsValid)
            {
                teacher.UpdateDate = DateTime.Now;
                db.Teachers.Update(teacher);
                if (!string.IsNullOrEmpty(groupIds))
                {
                    List<TeacherToGroup> GRemove = db.TeacherToGroups.Where(x => x.TeacherId == teacher.Id).ToList();
                    foreach(TeacherToGroup item in GRemove)
                    {
                        db.TeacherToGroups.Remove(item);
                    }
                    db.SaveChanges();
                    string[] gIds = groupIds.Split(",");
                    Array.Resize(ref gIds, gIds.Length - 1);
                    foreach (string gId in gIds)
                    {
                        TeacherToGroup ttg = new TeacherToGroup();
                        ttg.GroupId = Convert.ToInt32(gId);
                        ttg.TeacherId = teacher.Id;
                        await db.TeacherToGroups.AddAsync(ttg);
                    }
                    await db.SaveChangesAsync();
                }
               
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Teacher");
        }
    }
}
