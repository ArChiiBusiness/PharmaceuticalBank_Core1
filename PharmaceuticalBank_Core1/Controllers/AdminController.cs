using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PharmaceuticalBank_Core1.Models.DAL2;
//using PharmaceuticalBank_Core1.Models.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace PharmaceuticalBank_Core1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private pharmabank1Context db = new pharmabank1Context();
        //private excelpro_pharmabankContext db = new excelpro_pharmabankContext();

        public IActionResult Index()
        {
            ViewData["TotalCompanies"] = db.Companies.Count();
            ViewData["Buyers"] = db.Companies.Where(c => c.Type == "Buyer").Count();
            ViewData["Sellers"] = db.Companies.Where(c => c.Type == "Seller").Count();
            ViewData["SearchPhrases"] = db.Phrases.Count();

            return View();
        }

        public IActionResult GenerateCompanies()
        {

            var SellersDAL = db.Shipments.Where(s => s.Shipper != null && s.ShipperProfile != null)
                .Select(c => new Companies()
                {
                    Name = c.Shipper,
                    Type = "Seller",
                    Profile = c.ShipperProfile
                }).Distinct().ToList();

            var SellerCompaniesDAL = new List<Companies>();

            foreach (var SellerDAL in SellersDAL)
            {
                if (!db.Companies.Where(c => c.Profile == SellerDAL.Profile).Any())
                {
                    SellerCompaniesDAL.Add(SellerDAL);
                }
            }

            db.Companies.AddRange(SellerCompaniesDAL);

            var BuyersDAL = db.Shipments.Where(s => s.Consignee != null && s.ConsigneeProfile != null)
               .Select(c => new Companies()
               {
                   Name = c.Consignee,
                   Type = "Buyer",
                   Profile = c.ConsigneeProfile
               }).Distinct().ToList();

            var BuyerCompaniesDAL = new List<Companies>();

            foreach (var BuyerDAL in BuyersDAL)
            {
                if (!db.Companies.Where(c => c.Profile == BuyerDAL.Profile).Any())
                {
                    BuyerCompaniesDAL.Add(BuyerDAL);
                }
            }

            db.Companies.AddRange(BuyerCompaniesDAL);

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
            var time1 = DateTime.Now;
            var PhrasesDAL = db.Phrases.ToList();
            var ShipmentsBOL = db.Shipments.Where(s => s.GoodsShipped != null)
                .Select(sh => sh.GoodsShipped.ToLower()).ToArray();

            PhrasesDAL.ForEach(p => p.Popularity = ShipmentsBOL.Where(s => s.Contains(p.Phrase.ToLower())).Count());

            db.SaveChanges();

            var time2 = DateTime.Now;
            return Content(time2.Subtract(time1).TotalMilliseconds.ToString());
            //return RedirectToAction("Index");
        }

    }
}