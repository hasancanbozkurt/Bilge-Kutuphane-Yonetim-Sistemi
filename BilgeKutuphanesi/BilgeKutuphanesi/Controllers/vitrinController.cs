 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgeKutuphanesi.Models.Entity;
using BilgeKutuphanesi.Models.Siniflarim;

namespace BilgeKutuphanesi.Controllers
{
    [AllowAnonymous]
    public class vitrinController : Controller
    {
        // GET: vitrin
        DBKutuphaneEntities db = new DBKutuphaneEntities();

        [HttpGet]
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            cs.Deger1 = db.TBLKitap.OrderByDescending(k => k.ID).Take(6).ToList();
            cs.Deger2 = db.TBLHakkimizda.ToList();
            return View(cs);
        }
        [HttpPost]
        public ActionResult Index(TBLiletisim t)
        {
            db.TBLiletisim.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}