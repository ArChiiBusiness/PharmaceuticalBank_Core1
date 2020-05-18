using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using PharmaceuticalBank_Core1.Models;
using PharmaceuticalBank_Core1.Models.DAL4;

namespace PharmaceuticalBank_Core1.Controllers
{
    public class SearchController : Controller
    {
        private pharmabank1Context db = new pharmabank1Context();

        public IActionResult Index()
        {
            return RedirectToAction("Shipments", "Search", new { q = "", page = 1 });
        }

        [HttpPost]
        public IActionResult Filter(string q,string Year, string FromCountry, string ToCountry, int page)
        {
            return RedirectToAction("Shipments", "Search", new { 
                q = q, 
                page = page,
                year = Year,
                FromCountry = FromCountry,
                ToCountry = ToCountry
            });
        }

        public IActionResult Shipments(string q, int page, int? Year, string? FromCountry, string? ToCountry)
        {
            ViewData["TotalShipments"] = 32520; // db.Shipments.Where(s => s.GoodsShipped != null).Count();
            ViewData["TotalBuyers"] = 4206; // db.Shipments.Where(s => s.GoodsShipped != null).Select(b => b.ConsigneeCompanyId).Distinct().Count();
            ViewData["TotalSuppliers"] = 3761; // db.Shipments.Where(s => s.GoodsShipped != null).Select(b => b.ShipperCompanyId).Distinct().Count();

            var pageSize = 10;
            var skip = (page - 1) * pageSize;
            ViewData["LastShipmentData"] = db.Shipments.Include(c => c.ShipperCompany).Include(c => c.ConsigneeCompany).AsNoTracking().OrderByDescending(d => d.Date).First().Date.ToString().Split(" ")[0];

            if (q != null || page >= 1)
            {
                var Shipments = db.Shipments.AsNoTracking().Where(
                    s => EF.Functions.Like(s.GoodsShipped, "%" + q + "%") && s.GoodsShipped != null
                    && (s.ConsigneeCompanyId != null || s.ShipperCompanyId != null));
                if (Year != null)
                {
                    Shipments = Shipments.Where(s => s.Date.Value.Year == Year);
                }
                if (FromCountry != null)
                {
                    Shipments = Shipments.Where(s => s.ShipperCompany.Country == WebUtility.UrlDecode(FromCountry));
                }
                if (ToCountry != null)
                {
                    Shipments = Shipments.Where(s => s.ConsigneeCompany.Country == WebUtility.UrlDecode(ToCountry));
                }
                Shipments = Shipments.OrderByDescending(d => d.Date);
                var sCount = Shipments.Count();
                ViewData["TotalShipments"] = sCount;
                ViewData["TotalPages"] = Math.Ceiling(double.Parse(sCount.ToString()) / pageSize);
                var ShipmentsBOL = Shipments.Select(s => new Models.ShipmentViewModel
                {
                    Id = s.Id,
                    Buyer = new Models.CompanyViewModel
                    {
                        Id = s.ConsigneeCompany.Id,
                        Address = s.ConsigneeCompany.Address,
                        Name = s.ConsigneeCompany.Name,
                        Country = s.ConsigneeCompany.Country
                    },
                    Seller = new Models.CompanyViewModel
                    {
                        Id = s.ShipperCompany.Id,
                        Address = s.ShipperCompany.Address,
                        Name = s.ShipperCompany.Name,
                        Country = s.ShipperCompany.Country
                    },
                    Description = Strings.StrConv(s.GoodsShipped.Replace("\"", "'"), VbStrConv.ProperCase, 0),
                    Date = s.Date ?? DateTime.Now.AddYears(-3)
                }).Skip((pageSize) * (page - 1)).Take(pageSize).ToList();

                return View(ShipmentsBOL);
            }
            else
            {
                return View();
            }
        }

        public IActionResult Buyers(string q = default(string), int page = 1)
        {
            ViewData["TotalShipments"] = 32520; // db.Shipments.Where(s => s.GoodsShipped != null).Count();
            ViewData["TotalBuyers"] = 4206; // db.Shipments.Where(s => s.GoodsShipped != null).Select(b => b.ConsigneeCompanyId).Distinct().Count();
            ViewData["TotalSuppliers"] = 3761; // db.Shipments.Where(s => s.GoodsShipped != null).Select(b => b.ShipperCompanyId).Distinct().Count();

            var pageSize = 10;
            var skip = (page - 1) * pageSize;

            var query = db.Shipments.Where(s => EF.Functions.Like(s.GoodsShipped, "%" + q + "%") && s.ConsigneeCompanyId != null);
            var CompaniesDAL = (from c in query.Select(cm => new SearchResultViewModel.CompanyViewModel
            {
                Id = cm.ConsigneeCompany.Id,
                Name = cm.ConsigneeCompany.Name,
                Address = cm.ConsigneeCompany.Address + ", " + cm.ConsigneeCompany.City + ", " + cm.ConsigneeCompany.StateRegion,
                Country = cm.ConsigneeCompany.Country,
                Phone = cm.ConsigneeCompany.Phone1
            }).Distinct()
                                orderby query.Where(s => s.ConsigneeCompany.Id == c.Id).Count() descending
                                select new SearchResultViewModel.SearchResultViewModelCompany { Company = c, Count = query.Where(s => s.ConsigneeCompanyId == c.Id).Count() });

            ViewData["TotalPages"] = Math.Ceiling((decimal)CompaniesDAL.Count() / pageSize);

            return View(new SearchResultViewModel { Companies = CompaniesDAL.Skip((pageSize) * (page - 1)).Take(pageSize).ToList(), TotalCount = CompaniesDAL.Count() });
        }

        public IActionResult Suppliers(string q = default(string), int page = 1)
        {
            ViewData["TotalShipments"] = 32520; // db.Shipments.Where(s => s.GoodsShipped != null).Count();
            ViewData["TotalBuyers"] = 4206; // db.Shipments.Where(s => s.GoodsShipped != null).Select(b => b.ConsigneeCompanyId).Distinct().Count();
            ViewData["TotalSuppliers"] = 3761; // db.Shipments.Where(s => s.GoodsShipped != null).Select(b => b.ShipperCompanyId).Distinct().Count();

            var pageSize = 10;
            var skip = (page - 1) * pageSize;

            var query = db.Shipments.Where(s => EF.Functions.Like(s.GoodsShipped, "%" + q + "%") && s.ShipperCompanyId != null);
            var CompaniesDAL = (from c in query.Select(cm => new SearchResultViewModel.CompanyViewModel
            {
                Id = cm.ShipperCompany.Id,
                Name = cm.ShipperCompany.Name,
                Address = cm.ShipperCompany.Address + ", " + cm.ShipperCompany.City + ", " + cm.ShipperCompany.StateRegion,
                Country = cm.ShipperCompany.Country,
                Phone = cm.ShipperCompany.Phone1
            }).Distinct()
                                orderby query.Where(s => s.ShipperCompany.Id == c.Id).Count() descending
                                select new SearchResultViewModel.SearchResultViewModelCompany { Company = c, Count = query.Where(s => s.ShipperCompanyId == c.Id).Count() });

            ViewData["TotalPages"] = Math.Ceiling((decimal)CompaniesDAL.Count() / pageSize);

            return View(new SearchResultViewModel { Companies = CompaniesDAL.Skip((pageSize) * (page - 1)).Take(pageSize).ToList(), TotalCount = CompaniesDAL.Count() });
        }

        public IActionResult PreSearchShipments(string q = default(string))
        {
            var RecsDAL = db.Phrases.Where(p => p.Phrase.StartsWith(q) && (p.BuyerPopularity + p.SellerPopularity) > 0).OrderByDescending(o => (o.BuyerPopularity + o.SellerPopularity))
                .Select(s => new
                {
                    Phrase = s.Phrase.ToLower(),
                    Count = 0
                }).Take(10).ToArray();

            var ShipmentsDAL = db.Shipments.AsNoTracking().Where(sh => sh.GoodsShipped != null
                        && (sh.ConsigneeCompanyId != null || sh.ShipperCompanyId != null)).Select(sh => new { Description = sh.GoodsShipped.ToLower() }).ToArray();

            var RecsBOL = RecsDAL.Select(r => new
            {
                Phrase = r.Phrase,
                Count = ShipmentsDAL.Where(
                        sh => EF.Functions.Like(sh.Description, "%" + r.Phrase.ToLower() + "%")).Count()
            }).ToList();

            return Json(RecsBOL);
        }

    }
}