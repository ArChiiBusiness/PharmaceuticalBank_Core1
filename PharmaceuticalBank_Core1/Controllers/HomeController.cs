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

        public IActionResult Search(string searchtext = default(string), int page = 1, string mode = default(string))
        {
            var pageSize = 10;
            var skip = (page - 1) * pageSize;
            var ShipmentsBOL = default(IEnumerable<object>);

            switch (mode)
            {
                case "supplier":
                    {
                        var SearchQuery = db.Shipments.Where(s => s.Shipper != null);
                        SearchQuery = SearchQuery.Where(s => EF.Functions.Like(s.Shipper, "%" + searchtext + "%") || EF.Functions.Like(s.GoodsShipped, "%" + searchtext + "%"));
                        var ShipmentsDAL = SearchQuery.OrderByDescending(d => d.Date).Skip(skip).Take(pageSize).ToList();
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
                        break;
                    }
                case "buyer":
                    {
                        var SearchQuery = db.Shipments.Where(s => s.Consignee != null);
                        SearchQuery = SearchQuery.Where(s => EF.Functions.Like(s.Consignee, "%" + searchtext + "%") || EF.Functions.Like(s.GoodsShipped, "%" + searchtext + "%"));
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
                        break;
                    }
                default:
                    return Json(new List<object>());
            };

            return Json(ShipmentsBOL);
        }

        public IActionResult LoadShipment(System.Guid id, string mode = default(string))
        {
            switch (mode)
            {
                case "supplier":
                    {
                        var SupplierDAL = db.Shipments.Where(s => s.Id == id).First();
                        var SupplierBOL = new
                        {
                            Company = new
                            {
                                Name = SupplierDAL.Shipper,
                                Address = SupplierDAL.ShipperAddress,
                                Country = SupplierDAL.ShipperCountry
                            }
                        };
                        return Json(SupplierBOL);
                    }
                case "buyer":
                    {
                        var BuyerDAL = db.Shipments.Where(s => s.Id == id).First();
                        var BuyerBOL = new
                        {
                            Company = new
                            {
                                Name = BuyerDAL.Consignee,
                                Address = BuyerDAL.ConsigneeAddress,
                                Country = BuyerDAL.ConsigneeCountry
                            }
                        };
                        return Json(BuyerBOL);
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
