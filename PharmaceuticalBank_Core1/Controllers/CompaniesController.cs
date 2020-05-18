using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaceuticalBank_Core1.Models;
using PharmaceuticalBank_Core1.Models.DAL4;

namespace PharmaceuticalBank_Core1.Controllers
{
    public class CompaniesController : Controller
    {
        private pharmabank1Context db = new pharmabank1Context();

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return View(await db.Companies
                .Select(c => new CompanyViewModel
                {
                    Id = c.Id,
                    Address = c.Address,
                    Email = c.Email1,
                    Name = c.Name,
                    Country = c.Country
                }
                ).Take(50).ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CompanyBOL = await db.Companies.Where(c => c.Id == id)
                .Select(c => new CompanyViewModel
                {
                    Id = c.Id,
                    Address = c.Address + ", " + c.City + ", " + c.StateRegion,
                    Email = c.Email1,
                    Name = c.Name,
                    Country = c.Country
                }
                ).FirstOrDefaultAsync();

            if (CompanyBOL.Email == null)
            {
                CompanyBOL.Email = "Not disclosed";
            }

            if (CompanyBOL == null)
            {
                return NotFound();
            }

            return View(CompanyBOL);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,City,StateRegion,PostalCode,Country,FullAddress,Email1,Email2,Email3,Phone1,Phone2,Phone3,Fax,Website1,Website2,Profile,Dunsa,Industry,Revenue,Employees,MarketCapitalization,TradeRoles,Siccodes,StockTickers,UltimateParent,UltimateParentWebsite,UltimateParentHeadquartersAddress,UltimateParentProfile,UltimateParentStockTickers")] Companies companies)
        {
            if (ModelState.IsValid)
            {
                companies.Id = Guid.NewGuid();
                db.Add(companies);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companies);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CompanyBOL = await db.Companies.Where(c => c.Id == id)
                .Select(c => new CompanyViewModel
                {
                    Id = c.Id,
                    Address = c.Address,
                    Email = c.Email1,
                    Name = c.Name,
                    Country = c.Country
                }
                ).FirstOrDefaultAsync();

            if (CompanyBOL == null)
            {
                return NotFound();
            }
            return View(CompanyBOL);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Address,Email,Country")] CompanyViewModel CompanyBOL)
        {
            if (id != CompanyBOL.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var CompanyDAL = db.Companies.Where(c => c.Id == CompanyBOL.Id).FirstOrDefault();
                    db.Update(CompanyDAL);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompaniesExists(CompanyBOL.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(CompanyBOL);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companies = await db.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companies == null)
            {
                return NotFound();
            }

            return View(companies);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            return RedirectToAction(nameof(Index));
            //var companies = await db.Companies.FindAsync(id);
            //db.Companies.Remove(companies);
            //await db.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }

        private bool CompaniesExists(Guid id)
        {
            return db.Companies.Any(e => e.Id == id);
        }
    }
}
