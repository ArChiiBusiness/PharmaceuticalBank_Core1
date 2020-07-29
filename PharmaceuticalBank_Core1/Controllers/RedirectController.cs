using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PharmaceuticalBank_Core1.Controllers
{
    public class RedirectController : Controller
    {
        // GET: Redirect
        [Route("[controller]/internal/{location}")]
        public void Internal(string location)
        {
            string url = System.Web.HttpUtility.UrlDecode(location);
            var uri = new Uri(url);
            var domainURL = string.Format("{0}://{1}", uri.Scheme, uri.Host);
            Response.Redirect(domainURL);
        }

        // GET: Redirect/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Redirect/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Redirect/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Redirect/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Redirect/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Redirect/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Redirect/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}