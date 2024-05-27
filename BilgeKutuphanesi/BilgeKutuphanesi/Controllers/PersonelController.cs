using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgeKutuphanesi.Models.Entity;

namespace BilgeKutuphanesi.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLPersonel.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(TBLPersonel p)
        {
            if (!ModelState.IsValid)
            {
                return View("PersonelEkle");
            }
            db.TBLPersonel.Add(p);
            db.SaveChanges();
            return View();
        }
        public ActionResult PersonelSil(int id)
        {
            var person = db.TBLPersonel.Find(id);
            //db.TBLPersonel.Remove(person);
            person.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            var prs = db.TBLPersonel.Find(id);
            return View("PersonelGetir", prs);
        }

        public ActionResult PersonelGuncelle(TBLPersonel p)
        {
            var prs = db.TBLPersonel.Find(p.ID);
            prs.PERSONEL = p.PERSONEL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}