using System;
using System.Collections.Generic;
using System.Linq;

using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BilgeKutuphanesi.Models.Entity;

namespace BilgeKutuphanesi.Controllers
{
    [Authorize]
    public class PanelimController : Controller
    {
        // GET: Panelim

        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"];
            var degerler = db.TBLDuyurular.ToList();

            var d1 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.AD).FirstOrDefault();
            ViewBag.d1 = d1;
            var d2 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.SOYAD).FirstOrDefault();
            ViewBag.d2 = d2;
            var d3 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.FOTOGRAF).FirstOrDefault();
            ViewBag.d3 = d3;
            var d4 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.KULLANICIADI).FirstOrDefault();
            ViewBag.d4 = d4;
            var d5 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.OKUL).FirstOrDefault();
            ViewBag.d5 = d5;
            var d6 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.TELEFON).FirstOrDefault();
            ViewBag.d6 = d6;
            var d7 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.MAIL).FirstOrDefault();
            ViewBag.d7 = d7;

            var uyeid = db.TBLUyeler.Where(x=>x.MAIL == uyemail).Select(y => y.ID).FirstOrDefault();
            var d8 = db.TBLKitapTakip.Where(x => x.UYE == uyeid).Count();
            ViewBag.d8 = d8;
            var d9 = db.TBLMesajlar.Where(x => x.ALICI == uyemail).Count();
            ViewBag.d9 = d9;
            var d10 = db.TBLDuyurular.Count();
            ViewBag.d10 = d10;
            return View(degerler);
        }

        [HttpPost]
        public ActionResult Index2(TBLUyeler p)
        {
            var kullanici = (string)Session["Mail"];
            var uye = db.TBLUyeler.FirstOrDefault(x => x.MAIL == kullanici);
            uye.SIFRE = p.SIFRE;
            uye.AD = p.AD;
            uye.FOTOGRAF = p.FOTOGRAF;
            uye.OKUL = p.OKUL;
            uye.KULLANICIADI = p.KULLANICIADI;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Kitaplarim()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TBLUyeler.Where(x => x.MAIL == kullanici.ToString()).Select(z => z.ID).FirstOrDefault();
            var degerler = db.TBLKitapTakip.Where(x => x.UYE == id).ToList();

            var d1 = db.TBLUyeler.Where(x => x.MAIL == kullanici).Select(y => y.AD).FirstOrDefault();
            ViewBag.d1 = d1;
            var d2 = db.TBLUyeler.Where(x => x.MAIL == kullanici).Select(y => y.SOYAD).FirstOrDefault();
            ViewBag.d2 = d2;
            var d3 = db.TBLUyeler.Where(x => x.MAIL == kullanici).Select(y => y.FOTOGRAF).FirstOrDefault();
            ViewBag.d3 = d3;
            return View(degerler);
        } 
        public ActionResult Duyurular()
        {
            var duyurulistesi = db.TBLDuyurular.ToList();
            return View(duyurulistesi);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // Oturumdaki tüm verileri temizler
            return RedirectToAction("Index", "Vitrin/Index/");
        }

        public PartialViewResult Partial1()
        {
            var model = db.TBLDuyurular.OrderByDescending(x => x.TARIH).ToList();
            return PartialView(model);
        }

        public PartialViewResult Partial2()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TBLUyeler.Where(x => x.MAIL == kullanici).Select(y => y.ID).FirstOrDefault();
            var uyebul = db.TBLUyeler.Find(id);
            return PartialView("Partial2", uyebul);
        }

    }
}