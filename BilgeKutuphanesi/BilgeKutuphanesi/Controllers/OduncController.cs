using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgeKutuphanesi.Models.Entity;
namespace BilgeKutuphanesi.Controllers
{
    public class OduncController : Controller
    {
        // GET: Odunc
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        [Authorize(Roles ="A,B")] 
        public ActionResult Index()
        {
            var degerler = db.TBLKitapTakip.Where(x => x.ISLEMDURUM == false).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> deger1 = (from x in db.TBLUyeler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.AD + " " + x.SOYAD,
                                               Value = x.ID.ToString()
                                           }).ToList();
            List<SelectListItem> deger2 = (from y in db.TBLKitap.Where(x => x.DURUM == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = y.AD,
                                               Value = y.ID.ToString()
                                           }).ToList();

            List<SelectListItem> deger3 = (from z in db.TBLPersonel.ToList()
                                           select new SelectListItem
                                           {
                                               Text = z.PERSONEL,
                                               Value = z.ID.ToString()
                                           }).ToList();

            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            return View();
        }

        [HttpPost]
        public ActionResult OduncVer(TBLKitapTakip p)
        {
            var d1 = db.TBLUyeler.Where(x => x.ID == p.TBLUyeler.ID).FirstOrDefault();
            var d2 = db.TBLKitap.Where(y => y.ID == p.TBLKitap.ID).FirstOrDefault();
            var d3 = db.TBLPersonel.Where(z => z.ID == p.TBLPersonel.ID).FirstOrDefault();
            p.TBLUyeler = d1;
            p.TBLKitap = d2;
            p.TBLPersonel = d3;
            db.TBLKitapTakip.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Odunciade(TBLKitapTakip p)
        {
            var odn = db.TBLKitapTakip.Find(p.ID);
            DateTime d1 = DateTime.Parse(odn.IADETARIHI.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;
            ViewBag.dgr = d3.TotalDays;
            return View("Odunciade", odn);
        }
        public ActionResult OduncGuncelle(TBLKitapTakip p)
        {
            var hrk = db.TBLKitapTakip.Find(p.ID);
            hrk.UYEGETIRTARIH = p.UYEGETIRTARIH;
            hrk.ISLEMDURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}