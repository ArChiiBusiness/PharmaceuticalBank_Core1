using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PharmaceuticalBank_Core1.Models;
using PharmaceuticalBank_Core1.Models.DAL;
using Microsoft.EntityFrameworkCore;

namespace PharmaceuticalBank_Core1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private pharmabank1Context db = new pharmabank1Context();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchSuppliers(string searchtext = default(string), int page = 1)
        {
            var CompaniesQuery = db.Companies.Where(c => EF.Functions.Like(c.Name,"%" + searchtext + "%"));
            var pageSize = 10;
            var skip = (page - 1) * pageSize;
            var CompaniesDAL = CompaniesQuery.Skip(skip).Take(pageSize).ToList();
            return Json(CompaniesDAL);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
