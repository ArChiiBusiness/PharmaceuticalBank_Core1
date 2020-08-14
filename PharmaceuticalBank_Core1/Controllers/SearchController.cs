using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using PharmaceuticalBank_Core1.Models;
using PharmaceuticalBank_Core1.Models.DAL4;

namespace PharmaceuticalBank_Core1.Controllers
{
    //[Authorize]
    public class SearchController : Controller
    {
        private pharmabank1Context db = new pharmabank1Context();

        public IActionResult Index()
        {
            return RedirectToAction("Shipments", "Search", new { q = "", page = 1 });
        }

        [HttpPost]
        public IActionResult Filter(string q, string Year, string FromCountry, string ToCountry, int page)
        {
            return RedirectToAction("Shipments", "Search", new
            {
                q = q,
                page = page,
                year = Year,
                FromCountry = FromCountry,
                ToCountry = ToCountry
            });
        }

        [Authorize]
        public IActionResult ShipmentsData(string q, int page, int? Year, string? FromCountry, string? ToCountry)
        {
            var pageSize = 10;
            var skip = (page - 1) * pageSize;

            if (q == null )
            {
                q = "";
            }

            if (q != null && page >= 1)
            {
                //var Shipments = db.Shipments.Include(c => c.ShipperCompany).Include(c => c.ConsigneeCompany).AsNoTracking().Where(
                //    s => EF.Functions.Like(s.GoodsShipped, "%" + q + "%") && s.GoodsShipped != null
                //    && (s.ConsigneeCompanyId != null || s.ShipperCompanyId != null));

                string query = "";

                if (q =="") 
                {
                    query = "SELECT * FROM Shipments ORDER BY Shipments.Date DESC OFFSET 0 ROWS";
                }
                else
                {
                    query = "SELECT * FROM Shipments WHERE Contains(Shipments.[Goods Shipped], '" + q + "') ORDER BY Shipments.Date DESC OFFSET 0 ROWS";
                }

                var Shipments = db.Shipments.FromSqlRaw(query);

                if (Year != null)
                {
                    Shipments = Shipments.Where(s => s.Date.Year == Year);
                }
                if (FromCountry != null)
                {
                    Shipments = Shipments.Where(s => s.ShipperCompany.Country == FromCountry);
                }
                if (ToCountry != null)
                {
                    Shipments = Shipments.Where(s => s.ConsigneeCompany.Country == ToCountry);
                }

                var sCount = Shipments.Count();
                int totalShipments = sCount;
                double totalPages = Math.Ceiling(double.Parse(sCount.ToString()) / pageSize);
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
                    Date = s.Date
                }).Skip((pageSize) * (page - 1)).Take(pageSize).ToList();

                return Json(new { shipments = ShipmentsBOL, totalShipments = totalShipments, totalPages = totalPages});
            }
            else
            {
                if (q != null)
                {
                    return View();
                }
                else
                {
                    return View();
                }
            }
        }

        public IActionResult Shipments()
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return Unauthorized();
            //}
            ViewData["TotalShipments"] = 1613865; // db.Shipments.AsNoTracking().Count(); // db.Shipments.Where(s => s.GoodsShipped != null).Count();
            ViewData["TotalBuyers"] = 64734;// db.Shipments.AsNoTracking().Where(c => c.ConsigneeCompanyId != null).Select(c => c.ConsigneeCompanyId).ToList().Distinct().Count(); // db.Shipments.Where(s => s.GoodsShipped != null).Select(b => b.ConsigneeCompanyId).Distinct().Count();
            ViewData["TotalSuppliers"] = 84005; // db.Shipments.AsNoTracking().Where(c => c.ShipperCompanyId != null).Select(c => c.ShipperCompanyId).ToList().Distinct().Count(); // db.Shipments.Where(s => s.GoodsShipped != null).Select(b => b.ShipperCompanyId).Distinct().Count();

            return View();

            //var pageSize = 10;
            //var skip = (page - 1) * pageSize;
            //ViewData["LastShipmentData"] = db.Shipments.Include(c => c.ShipperCompany).Include(c => c.ConsigneeCompany).AsNoTracking().OrderByDescending(d => d.Date).First().Date.ToString();

            //if (q != null || page >= 1)
            //{
            //    var Shipments = db.Shipments.Include(c => c.ShipperCompany).Include(c => c.ConsigneeCompany).AsNoTracking().Where(
            //        s => EF.Functions.Like(s.GoodsShipped, "%" + q + "%") && s.GoodsShipped != null
            //        && (s.ConsigneeCompanyId != null || s.ShipperCompanyId != null));
            //    if (Year != null)
            //    {
            //        Shipments = Shipments.Where(s => s.Date.Year == Year);
            //    }
            //    if (FromCountry != null)
            //    {
            //        Shipments = Shipments.Where(s => s.ShipperCompany.Country == FromCountry);
            //    }
            //    if (ToCountry != null)
            //    {
            //        Shipments = Shipments.Where(s => s.ConsigneeCompany.Country == ToCountry);
            //    }
            //    Shipments = Shipments.OrderByDescending(d => d.Date);
            //    var sCount = Shipments.Count();
            //    ViewData["TotalShipments"] = sCount;
            //    ViewData["TotalPages"] = Math.Ceiling(double.Parse(sCount.ToString()) / pageSize);
            //    var ShipmentsBOL = Shipments.Select(s => new Models.ShipmentViewModel
            //    {
            //        Id = s.Id,
            //        Buyer = new Models.CompanyViewModel
            //        {
            //            Id = s.ConsigneeCompany.Id,
            //            Address = s.ConsigneeCompany.Address,
            //            Name = s.ConsigneeCompany.Name,
            //            Country = s.ConsigneeCompany.Country
            //        },
            //        Seller = new Models.CompanyViewModel
            //        {
            //            Id = s.ShipperCompany.Id,
            //            Address = s.ShipperCompany.Address,
            //            Name = s.ShipperCompany.Name,
            //            Country = s.ShipperCompany.Country
            //        },
            //        Description = Strings.StrConv(s.GoodsShipped.Replace("\"", "'"), VbStrConv.ProperCase, 0),
            //        Date = s.Date
            //    }).Skip((pageSize) * (page - 1)).Take(pageSize).ToList();

            //    return Json(ShipmentsBOL);
            //}
            //else
            //{
            //    return View();
            //}
        }

        public IActionResult Buyers(string q = default(string), int page = 1, int Year = 2020)
        {
            ViewData["TotalShipments"] = 1613865; // db.Shipments.AsNoTracking().Count(); // db.Shipments.Where(s => s.GoodsShipped != null).Count();
            ViewData["TotalBuyers"] = 64734;// db.Shipments.AsNoTracking().Where(c => c.ConsigneeCompanyId != null).Select(c => c.ConsigneeCompanyId).ToList().Distinct().Count(); // db.Shipments.Where(s => s.GoodsShipped != null).Select(b => b.ConsigneeCompanyId).Distinct().Count();
            ViewData["TotalSuppliers"] = 84005; // db.Shipments.AsNoTracking().Where(c => c.ShipperCompanyId != null).Select(c => c.ShipperCompanyId).ToList().Distinct().Count(); // db.Shipments.Where(s => s.GoodsShipped != null).Select(b => b.ShipperCompanyId).Distinct().Count();

            var pageSize = 10;
            var skip = (page - 1) * pageSize;

            var query = db.Shipments.Where(d => d.Date.Year == Year).Where(s => EF.Functions.Like(s.GoodsShipped, "%" + q + "%") && s.ConsigneeCompanyId != null);
            var CompaniesDAL = (from c in query.Select(cm => new SearchResultViewModel.CompanyViewModel
            {
                Id = cm.ConsigneeCompany.Id,
                Name = cm.ConsigneeCompany.Name,
                Address = cm.ConsigneeCompany.Address + "," + cm.ConsigneeCompany.City + "," + cm.ConsigneeCompany.StateRegion,
                Country = cm.ConsigneeCompany.Country,
                Phone = cm.ConsigneeCompany.Phone1
            }).Distinct()
                                //orderby query.Where(s => s.ConsigneeCompany.Id == c.Id).Count() descending
                                select new SearchResultViewModel.SearchResultViewModelCompany { Company = c, Count = 0 });

            ViewData["TotalPages"] = Math.Ceiling((decimal)CompaniesDAL.Count() / pageSize);

            var CompaniesBOL = new SearchResultViewModel { Companies = CompaniesDAL.Skip((pageSize) * (page - 1)).Take(pageSize).ToList(), TotalCount = CompaniesDAL.Count() };

            foreach (var c in CompaniesBOL.Companies)
            {
                c.Company.Address.Replace(",,", ",");
                if (c.Company.Address.Substring(0, 1) == ",")
                {
                    c.Company.Address = c.Company.Address.Substring(1, c.Company.Address.Length - 1);
                }

                if (c.Company.Address == "," || c.Company.Address == null || c.Company.Address == "")
                {
                    c.Company.Address = "-";
                }
            }

            return View(CompaniesBOL);
        }

        public IActionResult Suppliers(string q = default(string), int page = 1, int Year = 2020)
        {
            ViewData["TotalShipments"] = 1613865; // db.Shipments.AsNoTracking().Count(); // db.Shipments.Where(s => s.GoodsShipped != null).Count();
            ViewData["TotalBuyers"] = 64734;// db.Shipments.AsNoTracking().Where(c => c.ConsigneeCompanyId != null).Select(c => c.ConsigneeCompanyId).ToList().Distinct().Count(); // db.Shipments.Where(s => s.GoodsShipped != null).Select(b => b.ConsigneeCompanyId).Distinct().Count();
            ViewData["TotalSuppliers"] = 84005; // db.Shipments.AsNoTracking().Where(c => c.ShipperCompanyId != null).Select(c => c.ShipperCompanyId).ToList().Distinct().Count(); // db.Shipments.Where(s => s.GoodsShipped != null).Select(b => b.ShipperCompanyId).Distinct().Count();

            var pageSize = 10;
            var skip = (page - 1) * pageSize;

            var query = db.Shipments.Where(d => d.Date.Year == Year).Where(s => EF.Functions.Like(s.GoodsShipped, "%" + q + "%") && s.ShipperCompanyId != null);

            var CompaniesDAL = (from c in query.Select(cm => new SearchResultViewModel.CompanyViewModel
            {
                Id = cm.ShipperCompany.Id,
                Name = cm.ShipperCompany.Name,
                Address = cm.ShipperCompany.Address + "," + cm.ShipperCompany.City + "," + cm.ShipperCompany.StateRegion,
                Country = cm.ShipperCompany.Country,
                Phone = cm.ShipperCompany.Phone1
            }).Distinct()
                                //orderby query.Where(s => s.ShipperCompany.Id == c.Id).Count() descending
                                select new SearchResultViewModel.SearchResultViewModelCompany { Company = c, Count = 0 });

            ViewData["TotalPages"] = Math.Ceiling((decimal)CompaniesDAL.Count() / pageSize);

            var CompaniesBOL = new SearchResultViewModel { Companies = CompaniesDAL.Skip((pageSize) * (page - 1)).Take(pageSize).ToList(), TotalCount = CompaniesDAL.Count() };

            foreach (var c in CompaniesBOL.Companies)
            {
                c.Company.Address.Replace(",,", ",");
                if (c.Company.Address.Substring(0, 1) == ",")
                {
                    c.Company.Address = c.Company.Address.Substring(1, c.Company.Address.Length - 1);
                }

                if (c.Company.Address == "," || c.Company.Address == null || c.Company.Address == "")
                {
                    c.Company.Address = "-";
                }
            }

            return View(CompaniesBOL);
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