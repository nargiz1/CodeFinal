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
    public class MessageController : Controller
    {
        private readonly AppDbContext db;
        public MessageController(AppDbContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Messages.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Banner");
            return View(await db.Messages.FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Banner");
            return View(await db.Messages.FirstOrDefaultAsync(x => x.Id == id));
        }
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (!ModelState.IsValid) RedirectToAction("Index", "Message");
            Message message = await db.Messages.FirstOrDefaultAsync(x => x.Id == id);
            db.Messages.Remove(message);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Message");
        }
    }
}
