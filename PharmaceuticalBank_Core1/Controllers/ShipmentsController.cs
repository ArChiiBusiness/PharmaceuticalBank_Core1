using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PharmaceuticalBank_Core1.Models;
using PharmaceuticalBank_Core1.Models.DAL4;

namespace PharmaceuticalBank_Core1.Controllers
{
    public class ShipmentsController : Controller
    {
        private pharmabank1Context db = new pharmabank1Context();
        //private excelpro_pharmabankContext db = new excelpro_pharmabankContext();
        //private pharmabankContext db = new pharmabankContext();

        // GET: ShipmentViewModels
        public async Task<IActionResult> Index()
        {
            var ShipmentsBOL = await db.Shipments.Where(s => s.GoodsShipped != null)
                .OrderByDescending(d => d.Date)
                .Select(s => new Models.ShipmentViewModel
                {
                    Id = s.Id,
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
                    Description = Strings.StrConv(s.GoodsShipped.Replace("\"", "'"), VbStrConv.ProperCase,0),
                    Date = s.Date ?? DateTime.Now.AddYears(-3)
                }).Take(100).ToListAsync();

            return View(ShipmentsBOL);
        }

        // GET: ShipmentViewModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentViewModel = await db.Shipments.Where(s => s.Id == id)
                .Select(s => new Models.ShipmentViewModel
                {
                    Id = s.Id,
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
                    Description = Strings.StrConv(s.GoodsShipped.Replace("\"", "'"), VbStrConv.ProperCase, 0),
                    Date = s.Date ?? DateTime.Now.AddYears(-3)
                }).FirstOrDefaultAsync();
            if (shipmentViewModel == null)
            {
                return NotFound();
            }

            return View(shipmentViewModel);
        }

        // GET: ShipmentViewModels/Create
        public IActionResult Create()
        {
            return View();
        }
    }
}

// POST: ShipmentViewModels/Create
// To protect from overposting attacks, enable the specific properties you want to bind to, for 
// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//[HttpPost]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> Create([Bind("Id,GoodsShipped,Description,Date")] ShipmentViewModel shipmentViewModel)
//{
//if (ModelState.IsValid)
//{
//    shipmentViewModel.Id = Guid.NewGuid();
//    db.Add(shipmentViewModel);
//    await db.SaveChangesAsync();
//    return RedirectToAction(nameof(Index));
//}
//return View(shipmentViewModel);
//}

// GET: ShipmentViewModels/Edit/5
//public async Task<IActionResult> Edit(Guid? id)
//{
//if (id == null)
//{
//    return NotFound();
//}

//var shipmentViewModel = await db.ShipmentViewModel.FindAsync(id);
//if (shipmentViewModel == null)
//{
//    return NotFound();
//}
//return View(shipmentViewModel);
//}

// POST: ShipmentViewModels/Edit/5
// To protect from overposting attacks, enable the specific properties you want to bind to, for 
// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//[HttpPost]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> Edit(Guid id, [Bind("Id,GoodsShipped,Description,Date")] ShipmentViewModel shipmentViewModel)
//{
//    if (id != shipmentViewModel.Id)
//    {
//        return NotFound();
//    }

//    if (ModelState.IsValid)
//    {
//        try
//        {
//            db.Update(shipmentViewModel);
//            await db.SaveChangesAsync();
//        }
//        catch (DbUpdateConcurrencyException)
//        {
//            if (!ShipmentViewModelExists(shipmentViewModel.Id))
//            {
//                return NotFound();
//            }
//            else
//            {
//                throw;
//            }
//        }
//        return RedirectToAction(nameof(Index));
//    }
//    return View(shipmentViewModel);
//}

// GET: ShipmentViewModels/Delete/5
//public async Task<IActionResult> Delete(Guid? id)
//{
//    if (id == null)
//    {
//        return NotFound();
//    }

//    var shipmentViewModel = await db.ShipmentViewModel
//        .FirstOrDefaultAsync(m => m.Id == id);
//    if (shipmentViewModel == null)
//    {
//        return NotFound();
//    }

//    return View(shipmentViewModel);
//}

// POST: ShipmentViewModels/Delete/5
//[HttpPost, ActionName("Delete")]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> DeleteConfirmed(Guid id)
//{
//    var shipmentViewModel = await db.ShipmentViewModel.FindAsync(id);
//    db.ShipmentViewModel.Remove(shipmentViewModel);
//    await db.SaveChangesAsync();
//    return RedirectToAction(nameof(Index));
//}

//private bool ShipmentViewModelExists(Guid id)
//{
//    return db.ShipmentViewModel.Any(e => e.Id == id);
//}
//}
//}
