using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgeKutuphanesi.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace BilgeKutuphanesi.Controllers
{
    public class UyeController : Controller
    {
        // GET: Uye
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index(string p, int sayfa = 1)
        {
            var uyeler = from k in db.TBLUyeler select k;
            if (!string.IsNullOrEmpty(p))
            {
                uyeler = uyeler.Where(m => m.AD.Contains(p));
            }
            return View(uyeler.ToList().ToPagedList(sayfa, 10));
        }
        [HttpGet]
        public ActionResult UyeEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeEkle(TBLUyeler p)
        {
            if (!ModelState.IsValid)
            {
                return View("UyeEkle");
            }
            db.TBLUyeler.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeSil(int id)
        {
            var uye = db.TBLUyeler.Find(id);
            if (uye != null)
            {
                var aktifKitapTakipKayitlari = db.TBLKitapTakip.Any(k => k.UYE == id && k.ISLEMDURUM == true);

                if (!aktifKitapTakipKayitlari)
                {
                    try
                    {
                        db.TBLUyeler.Remove(uye);
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "Üye başarıyla silindi.";
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Bu üyenin ödünç kitap işlemleri olduğundan silinemez.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Silinecek üye bulunamadı.";
            }

            return RedirectToAction("Index");
        }

        public ActionResult UyeGetir(int id)
        {
            var uye = db.TBLUyeler.Find(id);
            return View("UyeGetir", uye);
        }
        public ActionResult UyeGuncelle(TBLUyeler p)
        {
            var uye = db.TBLUyeler.Find(p.ID);
            uye.AD = p.AD;
            uye.SOYAD = p.SOYAD;
            uye.MAIL = p.MAIL;
            uye.KULLANICIADI = p.KULLANICIADI;
            uye.SIFRE = p.SIFRE;
            uye.OKUL = p.OKUL;
            uye.TELEFON = p.TELEFON;
            uye.FOTOGRAF = p.FOTOGRAF;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeKitapGecmis(int id)
        {
            var ktpgcms = db.TBLKitapTakip.Where(x => x.UYE == id).ToList();
            var uyekit = db.TBLUyeler.Where(y => y.ID == id).Select(z => z.AD + " " + z.SOYAD).FirstOrDefault();
            ViewBag.u1 = uyekit;
            return View(ktpgcms);
        }
    }
}