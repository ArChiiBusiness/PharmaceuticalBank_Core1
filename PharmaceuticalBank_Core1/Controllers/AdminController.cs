using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PharmaceuticalBank_Core1.Models.DAL4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Hangfire;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using PB.Core.DE;

namespace PharmaceuticalBank_Core1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private pharmabank1Context db = new pharmabank1Context();
        //private excelpro_pharmabankContext db = new excelpro_pharmabankContext();
        //private pharmabankContext db = new pharmabankContext();


        public IActionResult Index()
        {
            ViewData["TotalCompanies"] = db.Companies.Count();
            ViewData["SearchPhrases"] = db.Phrases.Count();

            return View();
        }


        public void GenerateCompaniesFun()
        {
            db.Database.SetCommandTimeout(0);

            var CompaniesDAL = db.Companies.Where(c => c.Name == null)
                .ToList();

            //var nCompaniesDAL = new List<Companies>();

            foreach (var c in CompaniesDAL)
            {
                var ship = db.Shipments.Where(s => s.ConsigneeProfile == c.Profile).FirstOrDefault();
                var ship2 = db.Shipments.Where(s => s.ShipperProfile == c.Profile).FirstOrDefault();
                if (ship != null)
                {
                    c.Name = ship.Consignee;
                    c.Address = ship.ConsigneeAddress;
                    c.City = ship.ConsigneeCity;
                    c.StateRegion = ship.ConsigneeStateRegion;
                    c.PostalCode = ship.ConsigneePostalCode;
                    c.Country = ship.ConsigneeCountry;
                    c.FullAddress = ship.ConsigneeFullAddress;
                    c.Email1 = ship.ConsigneeEmail1;
                    c.Email2 = ship.ConsigneeEmail2;
                    c.Email3 = ship.ConsigneeEmail3;
                    c.Phone1 = ship.ConsigneePhone1;
                    c.Phone2 = ship.ConsigneePhone2;
                    c.Phone3 = ship.ConsigneePhone3;
                    c.Fax = ship.ConsigneeFax;
                    c.Website1 = ship.ConsigneeWebsite1;
                    c.Website2 = ship.ConsigneeWebsite2;
                    c.Dunsa = ship.ConsigneeDUNSâ;
                    c.Industry = ship.ConsigneeIndustry;
                    c.Profile = ship.ConsigneeProfile;
                    c.Revenue = ship.ConsigneeRevenue;
                    c.Employees = ship.ConsigneeEmployees;
                    c.MarketCapitalization = ship.ConsigneeMarketCapitalization;
                    c.TradeRoles = ship.ConsigneeTradeRoles;
                    c.Siccodes = ship.ConsigneeSicCodes;
                    c.StockTickers = ship.ConsigneeStockTickers;
                    c.UltimateParent = ship.ConsigneeUltimateParent;
                    c.UltimateParentWebsite = ship.ConsigneeUltimateParentWebsite;
                    c.UltimateParentHeadquartersAddress = ship.ConsigneeUltimateParentHeadquartersAddress;
                    c.UltimateParentProfile = ship.ConsigneeUltimateParentProfile;
                    c.UltimateParentStockTickers = ship.ConsigneeUltimateParentStockTickers;
                    db.SaveChanges();
                }

                if (ship2 != null)
                {
                    c.Name = ship2.Shipper;
                    c.Address = ship2.ShipperAddress;
                    c.City = ship2.ShipperCity;
                    c.StateRegion = ship2.ShipperStateRegion;
                    c.PostalCode = ship2.ShipperPostalCode;
                    c.Country = ship2.ShipperCountry;
                    c.FullAddress = ship2.ShipperFullAddress;
                    c.Email1 = ship2.ShipperEmail1;
                    c.Email2 = ship2.ShipperEmail2;
                    c.Email3 = ship2.ShipperEmail3;
                    c.Phone1 = ship2.ShipperPhone1;
                    c.Phone2 = ship2.ShipperPhone2;
                    c.Phone3 = ship2.ShipperPhone3;
                    c.Fax = ship2.ShipperFax;
                    c.Website1 = ship2.ShipperWebsite1;
                    c.Website2 = ship2.ShipperWebsite2;
                    c.Dunsa = ship2.ShipperDUNSâ;
                    c.Industry = ship2.ShipperIndustry;
                    c.Profile = ship2.ShipperProfile;
                    c.Revenue = ship2.ShipperRevenue;
                    c.Employees = ship2.ShipperEmployees;
                    c.MarketCapitalization = ship2.ShipperMarketCapitalization;
                    c.TradeRoles = ship2.ShipperTradeRoles;
                    c.Siccodes = ship2.ShipperSicCodes;
                    c.StockTickers = ship2.ShipperStockTickers;
                    c.UltimateParent = ship2.ShipperUltimateParent;
                    c.UltimateParentWebsite = ship2.ShipperUltimateParentWebsite;
                    c.UltimateParentHeadquartersAddress = ship2.ShipperUltimateParentHeadquartersAddress;
                    c.UltimateParentProfile = ship2.ShipperUltimateParentProfile;
                    c.UltimateParentStockTickers = ship2.ShipperUltimateParentStockTickers;
                    db.SaveChanges();
                }

            }

            return;
        }

        public IActionResult GenerateCompanies()
        {
            BackgroundJob.Enqueue(() => GenerateCompaniesFun());
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCompanies()
        {
            db.Database.SetCommandTimeout(5000);
            db.Database.ExecuteSqlRaw("UPDATE Shipments SET ShipperCompanyId = NULL");
            db.Database.ExecuteSqlRaw("UPDATE Shipments SET ConsigneeCompanyId = NULL");
            var rows = db.Database.ExecuteSqlRaw("DELETE FROM Companies");
            return RedirectToAction("Index");
        }

        public IActionResult AddCompaniesToShipments()
        {
            db.Database.ExecuteSqlRaw("UPDATE Shipments SET ShipperCompanyId = NULL");
            db.Database.ExecuteSqlRaw("UPDATE Shipments SET ConsigneeCompanyId = NULL");
            db.Database.ExecuteSqlRaw("UPDATE[Shipments] SET " +
                    "[Shipments].[ShipperCompanyId] = [Companies].[Id] " +
                    "FROM [Shipments], [Companies] WHERE " +
                    "[Shipments].[Shipper Profile] = [Companies].[Profile]");
            db.Database.ExecuteSqlRaw("UPDATE[Shipments] SET " +
                    "[Shipments].[ConsigneeCompanyId] = [Companies].[Id] " +
                    "FROM [Shipments], [Companies] WHERE " +
                    "[Shipments].[Consignee Profile] = [Companies].[Profile]");

            return RedirectToAction("Index");
        }

        public IActionResult IndexPhrases()
        {
            BackgroundJob.Enqueue(() => IndexPhrasesBuyers());
            BackgroundJob.Enqueue(() => IndexPhrasesSellers());
            return RedirectToAction("Index");
        }

        public void IndexPhrasesBuyers()
        {
            var PhrasesDAL = db.Phrases.ToList();
            var ShipmentsBOL = db.Shipments.Where(s => s.GoodsShipped != null && s.Consignee != null && s.ConsigneeProfile != null)
                .Select(sh => sh.GoodsShipped.ToLower()).ToArray();

            PhrasesDAL.ForEach(p => p.BuyerPopularity = ShipmentsBOL.Where(s => s.Contains(p.Phrase.ToLower())).Count());

            db.SaveChanges();
        }

        public void IndexPhrasesSellers()
        {
            var PhrasesDAL = db.Phrases.ToList();
            var ShipmentsBOL = db.Shipments.Where(s => s.GoodsShipped != null && s.Shipper != null && s.ShipperProfile != null)
                .Select(sh => sh.GoodsShipped.ToLower()).ToArray();

            PhrasesDAL.ForEach(p => p.SellerPopularity = ShipmentsBOL.Where(s => s.Contains(p.Phrase.ToLower())).Count());

            db.SaveChanges();
        }

        public IActionResult DEUpdateNeededCount()
        {
            var DataEverywhereProcessor = new DataEverywhereProcessor();
            return Content(DataEverywhereProcessor.GetUpdateCount().ToString());
        }

        public IActionResult DEUpdateFeeds()
        {
            DataEverywhereProcessor DEProcessor = new DataEverywhereProcessor();
            var feeds = DEProcessor.GetFeeds();

            foreach (var feed in feeds)
            {
                if (DEProcessor.PendingUpdate(feed))
                {
                    DEProcessor.UpdateFeed(feed);
                }
            }

            return Ok();
        }

    }
}