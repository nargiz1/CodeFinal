using bauen.Utils;
using CodePage.DAL;
using CodePage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
    public class CategoryController : Controller
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment env;
        public CategoryController(AppDbContext _db, IWebHostEnvironment _env)
        {
            db = _db;
            env = _env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Categories.ToListAsync());
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            if (!ModelState.IsValid) return View();
            Category duplicate = await db.Categories.FirstOrDefaultAsync(x => x.Name == category.Name);
            if (duplicate != null) return View();
            category.CreateDate = DateTime.Now;
            category.UpdateDate = DateTime.Now;
            await db.Categories.AddAsync(category);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Category");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Category");
            return View(await db.Categories.FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Category");
            return View(await db.Categories.FirstOrDefaultAsync(x => x.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid) return View();
            category.UpdateDate = DateTime.Now;
            db.Categories.Update(category);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Category");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Banner");
            return View(await db.Categories.FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (!ModelState.IsValid) RedirectToAction("Index", "Category");
            Category category = await db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            db.Categories.Remove(category);
            await db.SaveChangesAsync();
            List<Group> groupsToDelete = db.Groups.Where(x => x.CategoryId == category.Id).ToList();
            if (groupsToDelete.Count != 0)
            {
                foreach (Group item in groupsToDelete)
                {
                    db.Groups.Remove(item);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Category");
        }
    }
}
