using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgeKutuphanesi.Models.Entity;
namespace BilgeKutuphanesi.Controllers
{
    public class DuyuruController : Controller
    {
        // GET: Duyuru
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLDuyurular.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniDuyuru()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniDuyuru(TBLDuyurular t)
        {
            db.TBLDuyurular.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyuruSil(int id)
        {
            var duyuru = db.TBLDuyurular.Find(id);
            db.TBLDuyurular.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyuruDetay(TBLDuyurular p)
        {
            var duyuru = db.TBLDuyurular.Find(p.ID);
            return View("DuyuruDetay", duyuru);
        }
        public ActionResult DuyuruGuncelle(TBLDuyurular t)
        {
            var duyuru = db.TBLDuyurular.Find(t.ID);
            duyuru.KATEGORI = t.KATEGORI;
            duyuru.ICERIK = t.ICERIK;
            duyuru.TARIH = t.TARIH;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}