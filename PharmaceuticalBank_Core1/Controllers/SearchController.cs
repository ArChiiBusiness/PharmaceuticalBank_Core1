using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PharmaceuticalBank_Core1.Models;
using PharmaceuticalBank_Core1.Models.DAL4;

namespace PharmaceuticalBank_Core1.Controllers
{
    public class SearchController : Controller
    {
        private pharmabank1Context db = new pharmabank1Context();
        //private excelpro_pharmabankContext db = new excelpro_pharmabankContext();
        //private pharmabankContext db = new pharmabankContext();

        public IActionResult Index()
        {
            return RedirectToAction("Shipments");
        }

        public IActionResult Shipments(string q = default(string), int page = 1)
        {
            var pageSize = 10;
            var skip = (page - 1) * pageSize;
            var Shipments = db.Shipments.Where(
                s => EF.Functions.Like(s.GoodsShipped, "%" + q + "%") && s.GoodsShipped != null
                && (s.ConsigneeCompanyId != null || s.ShipperCompanyId != null)).OrderByDescending(d => d.Date);
            ViewData["TotalShipments"] = Shipments.Count();
            var ShipmentsBOL = Shipments.Select(s => new Models.ShipmentViewModel
            {
                Id = s.Id,
                GoodsShipped = s.GoodsShipped,
                Buyer = new Models.CompanyViewModel
                {
                    Id = s.ConsigneeCompany.Id,
                    Address = s.ConsigneeCompany.Address,
                    Name = s.ConsigneeCompany.Name
                },
                Seller = new Models.CompanyViewModel
                {
                    Id = s.ShipperCompany.Id,
                    Address = s.ShipperCompany.Address,
                    Name = s.ShipperCompany.Name
                },
                Description = s.GoodsShipped.Substring(0, 150),
                Date = s.Date ?? DateTime.Now.AddYears(-3)
            }).Skip(skip).Take(pageSize).ToList();

            return View(ShipmentsBOL);
        }

        public IActionResult Buyers(string q = default(string), int page = 1)
        {
            var pageSize = 10;
            var skip = (page - 1) * pageSize;

            var query = db.Shipments.Where(s => EF.Functions.Like(s.GoodsShipped, "%" + q + "%") && s.ConsigneeCompanyId != null);
            var CompaniesDAL = (from c in query.Select(cm => new SearchResultViewModel.CompanyViewModel { Id = cm.ConsigneeCompany.Id, Name = cm.ConsigneeCompany.Name, Address = cm.ConsigneeCompany.Address }).Distinct()
                                orderby query.Where(s => s.ConsigneeCompany.Id == c.Id).Count() descending
                                select new SearchResultViewModel.SearchResultViewModelCompany { Company = c, Count = query.Where(s => s.ConsigneeCompanyId == c.Id).Count() });

            return View(new SearchResultViewModel { Companies = CompaniesDAL.Skip(skip).Take(pageSize).ToList(), TotalCount = CompaniesDAL.Count() });
        }

        public IActionResult Suppliers(string q = default(string), int page = 1)
        {
            var pageSize = 10;
            var skip = (page - 1) * pageSize;

            var query = db.Shipments.Where(s => EF.Functions.Like(s.GoodsShipped, "%" + q + "%") && s.ShipperCompanyId != null);
            var CompaniesDAL = (from c in query.Select(cm => new SearchResultViewModel.CompanyViewModel { Id = cm.ShipperCompany.Id, Name = cm.ShipperCompany.Name, Address = cm.ShipperCompany.Address }).Distinct()
                                orderby query.Where(s => s.ShipperCompany.Id == c.Id).Count() descending
                                select new SearchResultViewModel.SearchResultViewModelCompany { Company = c, Count = query.Where(s => s.ShipperCompanyId == c.Id).Count() });

            return View(new SearchResultViewModel { Companies = CompaniesDAL.Skip(skip).Take(pageSize).ToList(), TotalCount = CompaniesDAL.Count() });
        }

        public IActionResult PreSearchShipments(string q = default(string))
        {
            var RecsDAL = db.Phrases.Where(p => p.Phrase.StartsWith(q) && (p.BuyerPopularity + p.SellerPopularity) > 0).OrderByDescending(o => (o.BuyerPopularity + o.SellerPopularity)).Select(s => s.Phrase).Take(10).ToArray();
            return Json(RecsDAL);
        }

    }
}