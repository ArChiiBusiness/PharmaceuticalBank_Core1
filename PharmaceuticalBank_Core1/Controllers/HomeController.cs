using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PharmaceuticalBank_Core1.Models.DAL;
using Microsoft.EntityFrameworkCore;
using PharmaceuticalBank_Core1.Models;

namespace PharmaceuticalBank_Core1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private pharmabank1Context db = new pharmabank1Context();
        private excelpro_pharmabankContext db = new excelpro_pharmabankContext();



        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Buyers"] = db.Shipments.AsNoTracking().Where(s => s.ConsigneeProfile != null).Distinct().Count();
            ViewData["Suppliers"] = db.Shipments.AsNoTracking().Where(s => s.ShipperProfile != null).Distinct().Count();
            return View();
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
                        var SearchQuery = db.Shipments.AsNoTracking().Where(s => s.Shipper != null && s.ShipperProfile != null);
                        SearchQuery = SearchQuery.Where(s => EF.Functions.Like(s.Shipper, "%" + searchtext + "%") || EF.Functions.Like(s.GoodsShipped, "%" + searchtext + "%"));
                        ShipmentsBOL = SearchQuery.OrderByDescending(d => d.Date).Select(s => new
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
                            Count = ShipmentsBOL.Count(),
                            Shipments = ShipmentsBOL.Skip(skip).Take(pageSize).ToList()
                        };
                        return Json(ResultsObj);

                    }
                case "buyer":
                    {
                        var SearchQuery = db.Shipments.AsNoTracking().Where(s => s.Consignee != null && s.ConsigneeProfile != null);
                        SearchQuery = SearchQuery.Where(s => EF.Functions.Like(s.Consignee, "%" + searchtext + "%") || EF.Functions.Like(s.GoodsShipped, "%" + searchtext + "%"));
                        ShipmentsBOL = SearchQuery.OrderByDescending(d => d.Date).Select(s => new
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
                            Count = ShipmentsBOL.Count(),
                            Shipments = ShipmentsBOL.Skip(skip).Take(pageSize).ToList()
                        };
                        return Json(ResultsObj);

                    }
                default:
                    return Json(new List<object>());
            };

        }

        public IActionResult LoadShipment(System.Guid id, string mode = default(string))
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
