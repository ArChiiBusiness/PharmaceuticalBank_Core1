using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PharmaceuticalBank_Core1.Models.DAL2;
//using PharmaceuticalBank_Core1.Models.DAL2;
using Microsoft.EntityFrameworkCore;
using PharmaceuticalBank_Core1.Models;
using Microsoft.AspNetCore.Identity;
using PharmaceuticalBank_Core1.Models.DAL3;

namespace PharmaceuticalBank_Core1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private pharmabank1Context db = new pharmabank1Context();
        //private excelpro_pharmabankContext db = new excelpro_pharmabankContext();
        //private pharmabankContext db = new pharmabankContext();


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Buyers"] = db.Shipments.AsNoTracking().Where(s => s.ConsigneeProfile != null).Count();
            ViewData["Suppliers"] = db.Shipments.AsNoTracking().Where(s => s.ShipperProfile != null).Count();
            return View();
        }

        public IActionResult PreSearch(string searchtext = default(string), string mode = default(string))
        {
            if (mode == "buyer")
            {
                var RecsDAL = db.Phrases.Where(p => p.Phrase.StartsWith(searchtext) && p.BuyerPopularity > 0).OrderByDescending(o => o.BuyerPopularity).Select(s => s.Phrase).Take(10).ToArray();
                return Json(RecsDAL);
            };
            if (mode == "supplier")
            {
                var RecsDAL = db.Phrases.Where(p => p.Phrase.StartsWith(searchtext) && p.SellerPopularity > 0).OrderByDescending(o => o.SellerPopularity).Select(s => s.Phrase).Take(10).ToArray();
                return Json(RecsDAL);
            };
            return Json(new { });
        }

        public IActionResult Search(string searchtext = default(string), int page = 1, string mode = default(string))
        {
            var pageSize = 10;
            var skip = (page - 1) * pageSize;
            var ShipmentsBOL = default(IEnumerable<object>);
            switch (mode)
            {
                case "supplier":
                    {
                        //var SearchQuery = db.Companies.Include(c => c.ShipmentsShipperCompany).AsNoTracking().Where(s => EF.Functions.Like(s.GoodsShipped, "%" + searchtext + "%") && s.ShipperCompanyId != null);

                        //var CompaniesDAL = SearchQuery.Select(c => c.ShipperCompany).OrderByDescending(s => s.s)

                        var q = db.Shipments.Where(s => EF.Functions.Like(s.GoodsShipped, "%" + searchtext + "%") && s.ShipperCompanyId != null);
                        var CompaniesDAL = (from c in q.Select(cm => cm.ShipperCompany)
                                            orderby q.Where(s => s.ShipperCompany == c).Count() descending
                                            select new { company = c, count = q.Where(s => s.ShipperCompany == c).Count() }).ToList();

                        return Json(CompaniesDAL);

                    }
                case "buyer":
                    {
                        var SearchQuery = db.Shipments.AsNoTracking().Where(s => EF.Functions.Like(s.GoodsShipped, "%" + searchtext + "%") && s.ConsigneeProfile != null);
                        var ShipmentsDAL = SearchQuery.Skip(skip).Take(pageSize).ToList();

                        ShipmentsBOL = ShipmentsDAL.Select(s => new
                        {
                            Company = new
                            {
                                Name = s.Consignee,
                                Address = s.ConsigneeAddress
                            },
                            Shipment = new
                            {
                                Id = s.Id,
                                Date = s.Date.HasValue ? s.Date.Value.ToString("MM/dd/yyyy") : null,
                                Description = s.GoodsShipped
                            }
                        });

                        var ResultsObj = new
                        {
                            Count = SearchQuery.Count(),
                            Shipments = ShipmentsBOL
                        };
                        return View();

                    }
                default:
                    return View();
            };


            return View();
        }

        public IActionResult SearchOld(string searchtext = default(string), int page = 1, string mode = default(string))
        {
            var pageSize = 10;
            var skip = (page - 1) * pageSize;
            var ShipmentsBOL = default(IEnumerable<object>);

            switch (mode)
            {
                case "supplier":
                    {
                        var SearchQuery = db.Shipments.AsNoTracking().Where(s => EF.Functions.Like(s.GoodsShipped, "%" + searchtext + "%") && s.ShipperProfile != null);
                        var ShipmentsDAL = SearchQuery.Skip(skip).Take(pageSize).ToList();

                        ShipmentsBOL = ShipmentsDAL.Select(s => new
                        {
                            Company = new
                            {
                                Name = s.Shipper,
                                Address = s.ShipperAddress
                            },
                            Shipment = new
                            {
                                Id = s.Id,
                                Date = s.Date.HasValue ? s.Date.Value.ToString("MM/dd/yyyy") : null,
                                Description = s.GoodsShipped
                            }
                        });

                        var ResultsObj = new
                        {
                            Count = SearchQuery.Count(),
                            Shipments = ShipmentsBOL
                        };
                        return Json(ResultsObj);

                    }
                case "buyer":
                    {
                        var SearchQuery = db.Shipments.AsNoTracking().Where(s => EF.Functions.Like(s.GoodsShipped, "%" + searchtext + "%") && s.ConsigneeProfile != null);
                        var ShipmentsDAL = SearchQuery.Skip(skip).Take(pageSize).ToList();

                        ShipmentsBOL = ShipmentsDAL.Select(s => new
                        {
                            Company = new
                            {
                                Name = s.Consignee,
                                Address = s.ConsigneeAddress
                            },
                            Shipment = new
                            {
                                Id = s.Id,
                                Date = s.Date.HasValue ? s.Date.Value.ToString("MM/dd/yyyy") : null,
                                Description = s.GoodsShipped
                            }
                        });

                        var ResultsObj = new
                        {
                            Count = SearchQuery.Count(),
                            Shipments = ShipmentsBOL
                        };
                        return Json(ResultsObj);

                    }
                default:
                    return Json(new List<object>());
            };

        }

        public IActionResult LoadCompany(System.Guid id, string mode = default(string))
        {
            switch (mode)
            {
                case "supplier":
                    {
                        var ShipmentDAL = db.Shipments.Where(s => s.Id == id).First();
                        var ShipmentBOL = new
                        {
                            Company = new
                            {
                                Name = ShipmentDAL.Shipper,
                                Address = ShipmentDAL.ShipperAddress,
                                Country = ShipmentDAL.ShipperCountry
                            }
                        };
                        return Json(ShipmentBOL);
                    }
                case "buyer":
                    {
                        var ShipmentDAL = db.Shipments.Where(s => s.Id == id).First();
                        var ShipmentBOL = new
                        {
                            Company = new
                            {
                                Name = ShipmentDAL.Consignee,
                                Address = ShipmentDAL.ConsigneeAddress,
                                Country = ShipmentDAL.ConsigneeCountry
                            }
                        };
                        return Json(ShipmentBOL);
                    }
                default:
                    {
                        return Json(new object());
                    }
            }
        }

        public IActionResult Privacy(string searchtext)
        {
            var user = User;
            var test = User.IsInRole("Admin");
            return Json(new { a = test });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
