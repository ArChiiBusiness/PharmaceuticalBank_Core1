using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PharmaceuticalBank_Core1.Models.DAL2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Hangfire;

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

        public IActionResult GenerateCompanies()
        {

            var CompaniesDAL = db.Companies.AsNoTracking()
                .Select(c => new
                {
                    Profile = c.Profile
                }).ToList();

            var nCompaniesDAL = new List<Companies>();

            var SellersDAL = db.Shipments.Where(s => s.Shipper != null && s.ShipperProfile != null)
                .Select(c => new Companies()
                {
                    Id = Guid.NewGuid(),
                    Name = c.Shipper,
                    Address = c.ShipperAddress,
                    City = c.ShipperCity,
                    StateRegion = c.ShipperStateRegion,
                    PostalCode = c.ShipperPostalCode,
                    Country = c.ShipperCountry,
                    FullAddress = c.ShipperFullAddress,
                    Email1 = c.ShipperEmail1,
                    Email2 = c.ShipperEmail2,
                    Email3 = c.ShipperEmail3,
                    Phone1 = c.ShipperPhone1,
                    Phone2 = c.ShipperPhone2,
                    Phone3 = c.ShipperPhone3,
                    Fax = c.ShipperFax,
                    Website1 = c.ShipperWebsite1,
                    Website2 = c.ShipperWebsite2,
                    Dunsa = c.ShipperDUNSâ,
                    Industry = c.ShipperIndustry,
                    Profile = c.ShipperProfile,
                    Revenue = c.ShipperRevenue,
                    Employees = c.ShipperEmployees,
                    //MarketCapitalization = c.ShipperMarketCapitalization,
                    TradeRoles = c.ShipperTradeRoles,
                    Siccodes = c.ShipperSicCodes,
                    StockTickers = c.ShipperStockTickers,
                    UltimateParent = c.ShipperUltimateParent,
                    UltimateParentWebsite = c.ShipperUltimateParentWebsite,
                    UltimateParentHeadquartersAddress = c.ShipperUltimateParentHeadquartersAddress,
                    UltimateParentProfile = c.ShipperUltimateParentProfile,
                    UltimateParentStockTickers = c.ShipperUltimateParentStockTickers
                }).Distinct().ToList();

            foreach (var SellerDAL in SellersDAL)
            {
                if (!CompaniesDAL.Where(c => c.Profile == SellerDAL.Profile).Any() && !nCompaniesDAL.Where(c => c.Profile == SellerDAL.Profile).Any())
                {
                    nCompaniesDAL.Add(SellerDAL);
                }
            }

            var BuyersDAL = db.Shipments.Where(s => s.Consignee != null && s.ConsigneeProfile != null)
               .Select(c => new Companies()
               {
                   Id = Guid.NewGuid(),
                   Name = c.Consignee,
                   Address = c.ConsigneeAddress,
                   City = c.ConsigneeCity,
                   StateRegion = c.ConsigneeStateRegion,
                   PostalCode = c.ConsigneePostalCode,
                   Country = c.ConsigneeCountry,
                   FullAddress = c.ConsigneeFullAddress,
                   Email1 = c.ConsigneeEmail1,
                   Email2 = c.ConsigneeEmail2,
                   Email3 = c.ConsigneeEmail3,
                   Phone1 = c.ConsigneePhone1,
                   Phone2 = c.ConsigneePhone2,
                   Phone3 = c.ConsigneePhone3,
                   Fax = c.ConsigneeFax,
                   Website1 = c.ConsigneeWebsite1,
                   Website2 = c.ConsigneeWebsite2,
                   Dunsa = c.ConsigneeDUNSâ,
                   Industry = c.ConsigneeIndustry,
                   Profile = c.ConsigneeProfile,
                   //Revenue = c.ConsigneeRevenue,
                   Employees = c.ConsigneeEmployees,
                   //MarketCapitalization = c.ConsigneeMarketCapitalization,
                   TradeRoles = c.ConsigneeTradeRoles,
                   Siccodes = c.ConsigneeSicCodes,
                   StockTickers = c.ConsigneeStockTickers,
                   UltimateParent = c.ConsigneeUltimateParent,
                   UltimateParentWebsite = c.ConsigneeUltimateParentWebsite,
                   UltimateParentHeadquartersAddress = c.ConsigneeUltimateParentHeadquartersAddress,
                   UltimateParentProfile = c.ConsigneeUltimateParentProfile,
                   UltimateParentStockTickers = c.ConsigneeUltimateParentStockTickers
               }).Distinct().ToList();

            foreach (var BuyerDAL in BuyersDAL)
            {
                if (!CompaniesDAL.Where(c => c.Profile == BuyerDAL.Profile).Any() && !nCompaniesDAL.Where(c => c.Profile == BuyerDAL.Profile).Any())
                {
                    nCompaniesDAL.Add(BuyerDAL);
                }
            }

            db.Companies.AddRange(nCompaniesDAL);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult DeleteCompanies()
        {
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

    }
}