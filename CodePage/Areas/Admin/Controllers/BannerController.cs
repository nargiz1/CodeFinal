using CodePage.DAL;
using CodePage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private readonly AppDbContext db;
        public BannerController(AppDbContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Banners.ToListAsync());
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Banner banner)
        {
            if (!ModelState.IsValid) return View();
            banner.CreateDate = DateTime.Now;
            banner.UpdateDate = DateTime.Now;
            await db.Banners.AddAsync(banner);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Banner");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Banner");
            return View(await db.Banners.FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Banner");
            return View(await db.Banners.FirstOrDefaultAsync(x => x.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Banner banner)
        {
            if (!ModelState.IsValid) return View();
            banner.UpdateDate = DateTime.Now;
            db.Banners.Update(banner);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Banner");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Banner");
            return View(await db.Banners.FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<IActionResult> DeleteConfirmed(Banner banner)
        {
            if (!ModelState.IsValid) return View();
            db.Banners.Remove(banner);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Banner");
        }
    }
}
