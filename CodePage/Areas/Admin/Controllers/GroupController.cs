using CodePage.DAL;
using CodePage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GroupController : Controller
    {
        private readonly AppDbContext db;
        public GroupController(AppDbContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Groups.Include(x=> x.Category).ToListAsync());
        }
        public IActionResult Add()
        {
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Group group)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Group");
            Group duplicate = await db.Groups.FirstOrDefaultAsync(x => x.Name == group.Name);
            if (duplicate != null) return RedirectToAction("Index", "Group");
            group.CreateDate = DateTime.Now;
            group.UpdateDate = DateTime.Now;
            await db.Groups.AddAsync(group);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Group");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Group");
            Group group = await db.Groups.FirstOrDefaultAsync(x => x.Id == id);
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name");
            return View(group);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Group group)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Group");
            group.UpdateDate = DateTime.Now;
            db.Groups.Update(group);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Group");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Group");
            return View(await db.Groups.Include(x=> x.Category).FirstOrDefaultAsync(x=> x.Id==id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Group");
            return View(await db.Groups.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Group");
            Group groupToDelete =await db.Groups.FirstOrDefaultAsync(x => x.Id == id);
            db.Groups.Remove(groupToDelete);
            await db.SaveChangesAsync();
            List<Student> studentsToDelete = db.Students.Where(x => x.GroupId == id).ToList();
            if (studentsToDelete.Count != 0)
            {
                foreach (Student item in studentsToDelete)
                {
                    db.Students.Remove(item);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Group");
        }
    }
}
