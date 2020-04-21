using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PharmaceuticalBank_Core1.Models.DAL3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Hangfire;

namespace PharmaceuticalBank_Core1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        //private pharmabank1Context db = new pharmabank1Context();
        //private excelpro_pharmabankContext db = new excelpro_pharmabankContext();
        private pharmabankContext db = new pharmabankContext();


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
                    Profile = c.ShipperProfile
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
                   Profile = c.ConsigneeProfile
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
            db.Companies.RemoveRange(db.Companies.ToList());
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult IndexPhrases()
        {
            BackgroundJob.Enqueue(() => IndexPhrasesBuyers());
            BackgroundJob.Enqueue(() => IndexPhrasesSellers());
            return RedirectToAction("Index");
        }

        public void IndexPhrasesBuyers() {
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