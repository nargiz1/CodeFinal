using CodePage.DAL;
using CodePage.HomeViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CodePage.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db;
        public HomeController(AppDbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            ViewModel vm = new ViewModel()
            {
                Banners=db.Banners.ToList(),
                Categories=db.Categories.ToList(),
            };
            return View(vm);
        }
    }
}
