using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgeKutuphanesi.Models.Entity;

namespace BilgeKutuphanesi.Controllers
{
    public class iletisimController : Controller
    {
        // GET: iletisim
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLiletisim.ToList();
            return View(degerler);
        }

        public ActionResult iletiSil(int id)
        {
            var ileti = db.TBLiletisim.Find(id);
            db.TBLiletisim.Remove(ileti);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}