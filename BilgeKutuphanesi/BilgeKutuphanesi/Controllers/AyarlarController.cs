using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgeKutuphanesi.Models.Entity;

namespace BilgeKutuphanesi.Controllers
{
    public class AyarlarController : Controller
    {
        // GET: Ayarlar
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index()
        {
            var kullanicilar = db.TBLAdmin.ToList();
            return View(kullanicilar);
        }
        public ActionResult Index2()
        {
            var kullanicilar = db.TBLAdmin.ToList();
            return View(kullanicilar);
        }

        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniAdmin(TBLAdmin t)
        {
            db.TBLAdmin.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index2");
        }

        public ActionResult AdminSil(int id)
        {
            var admin = db.TBLAdmin.Find(id);
            db.TBLAdmin.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index2");
        }
        [HttpGet]
        public ActionResult AdminGuncelle(int id)
        {
            var admin = db.TBLAdmin.Find(id);
            return View("AdminGuncelle", admin);
        }
        [HttpPost]
        public ActionResult AdminGuncelle(TBLAdmin p)
        {
            var admin = db.TBLAdmin.Find(p.ID);
            admin.Kullanici = p.Kullanici;
            admin.Sifre = p.Sifre;
            admin.Yetki = p.Yetki;
            db.SaveChanges();
            return RedirectToAction("Index2");
        }
    }
}