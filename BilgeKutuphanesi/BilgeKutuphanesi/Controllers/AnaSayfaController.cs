using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgeKutuphanesi.Models.Entity;

namespace BilgeKutuphanesi.Controllers
{
    public class AnaSayfaController : Controller
    {
        // GET: AnaSayfa
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLHakkimizda.ToList();
            return View(degerler);
        }

        public ActionResult AnaSayfaGetir(int id)
        {
            var ktg = db.TBLHakkimizda.Find(id);
            return View("AnaSayfaGetir", ktg);
        }
        public ActionResult AnaSayfaGuncelle(TBLHakkimizda p)
        {
            var ktg = db.TBLHakkimizda.Find(p.ID);
            ktg.ACIKLAMA = p.ACIKLAMA;
            ktg.SLOGAN = p.SLOGAN;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}