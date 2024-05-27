using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgeKutuphanesi.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace BilgeKutuphanesi.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index(int sayfa = 1)
        {
            var degerler = db.TBLYazar.ToList().ToPagedList(sayfa, 10);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YazarEkle()
        {
            return View();
        }
        public ActionResult YazarEkle(TBLYazar p)
        {
            if (!ModelState.IsValid)
            {
                return View("YazarEkle");
            }
            db.TBLYazar.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult YazarSil(int id)
        {
            var yazar = db.TBLYazar.Find(id);
            if (yazar != null)
            {
                
                var kitaplar = db.TBLKitap.Where(k => k.YAZAR == id).ToList();
                
                if (!kitaplar.Any())
                {
                    db.TBLYazar.Remove(yazar);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Yazar başarıyla silindi.";
                }
                else
                {
                   TempData["ErrorMessage"] = "Bu yazarın sistemde kayıtlı kitapları olduğundan silinemez.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Silinecek yazar bulunamadı.";
            }

            return RedirectToAction("Index");
        }

        public ActionResult YazarGetir(int id)
        {
            var yzr = db.TBLYazar.Find(id);
            return View("YazarGetir", yzr);
        }
        public ActionResult YazarGuncelle(TBLYazar p)
        {
            var yzr = db.TBLYazar.Find(p.ID);
            yzr.AD = p.AD;
            yzr.SOYAD = p.SOYAD;
            yzr.DETAY = p.DETAY;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult YazarKitaplar(int id)
        {
            var yazar = db.TBLKitap.Where(x => x.YAZAR == id).ToList();
            var yzrad = db.TBLYazar.Where(y => y.ID == id).Select(z => z.AD + " " + z.SOYAD).FirstOrDefault();
            ViewBag.y1 = yzrad;
            return View(yazar);
        }

    }
}