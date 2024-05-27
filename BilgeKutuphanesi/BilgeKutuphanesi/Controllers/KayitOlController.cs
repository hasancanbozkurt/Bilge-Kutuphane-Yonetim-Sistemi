using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgeKutuphanesi.Models.Entity;

namespace BilgeKutuphanesi.Controllers
{
    [AllowAnonymous]
    public class KayitOlController : Controller
    {
        // GET: KayitOl
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        [HttpGet]
        public ActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kayit(TBLUyeler p)
        {
            if (!ModelState.IsValid)
            {
                return View("Kayit");
            }
            db.TBLUyeler.Add(p);
            db.SaveChanges();
            return View();
        }
    }
}