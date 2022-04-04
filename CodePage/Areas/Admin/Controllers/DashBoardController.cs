using CodePage.Areas.Admin.Models;
using CodePage.DAL;
using CodePage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Teacher")]
    public class DashBoardController : Controller
    {
        private readonly AppDbContext db;
        private readonly UserManager<AppUser> userManager;

        public DashBoardController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            DashBoardViewModel dbvm = new DashBoardViewModel()
            {
                BannerCount = db.Banners.Count(),
                AboutUsCount = db.AboutUs.Count(),
                CategoryCount = db.Categories.Count(),
                GroupCount = db.Groups.Count(),
                MessageCount = db.Messages.Count(),
                TeacherCount = db.Teachers.Count(),
                StudentCount = db.Students.Count(),
            };
            return View(dbvm);
        }
    }
}
