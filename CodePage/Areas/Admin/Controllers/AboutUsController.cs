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
    public class AboutUsController : Controller
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment env;
        public AboutUsController(AppDbContext _db, IWebHostEnvironment _env)
        {
            db = _db;
            env = _env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.AboutUs.ToListAsync());
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AboutUs aboutUs)
        {
            if (ModelState.IsValid)
            {
                aboutUs.CreateDate = DateTime.Now;
                aboutUs.UpdateDate = DateTime.Now;
                aboutUs.Image = await aboutUs.ImageFile.Upload(env.WebRootPath, @"img/aboutUs");
                await db.AboutUs.AddAsync(aboutUs);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "AboutUs");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction("Index", "AboutUs");
            return View(await db.AboutUs.FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            return View(await db.AboutUs.FirstOrDefaultAsync(x=> x.Id==id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AboutUs aboutUs)
        {
            if (ModelState.IsValid)
            {
                aboutUs.UpdateDate = DateTime.Now;
                if (aboutUs.ImageFile != null)
                {
                    aboutUs.Image = await aboutUs.ImageFile.Upload(env.WebRootPath, @"img/aboutUs");
                }
                db.AboutUs.Update(aboutUs);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "AboutUs");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return View(await db.AboutUs.FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null) return RedirectToAction("Index", "AboutUs");
            AboutUs aboutUs = await db.AboutUs.FirstOrDefaultAsync(x => x.Id == id);
            db.AboutUs.Remove(aboutUs);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "AboutUs");
        }
    }
}
