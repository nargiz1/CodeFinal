using CodePage.Areas.Admin.Models;
using CodePage.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController : Controller
    {
        private readonly AppDbContext db;
        public DashBoardController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            DashBoardViewModel dbvm = new DashBoardViewModel()
            {
                BannerCount = db.Banners.Count(),
            };
            return View(dbvm);
        }
    }
}
