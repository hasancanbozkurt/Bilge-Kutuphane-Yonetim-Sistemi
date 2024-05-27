using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgeKutuphanesi.Models.Entity;

namespace BilgeKutuphanesi.Controllers
{
    public class istatistikController : Controller
    {
        // GET: istatistik
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index()
        {
            var deger1 = db.TBLUyeler.Count();
            var deger2 = db.TBLKitap.Count();
            var deger3 = db.TBLKitap.Where(x => x.DURUM == false).Count();
            var deger4 = db.TBLCezalar.Where(x => x.PARA > 0).Sum(x => x.PARA);
            var deger5 = db.TBLKategori.Count();
            var deger6 = db.EnFazlaKitapYazar().FirstOrDefault();
            var deger7 = db.TBLKitap
               .GroupBy(x => x.YAYINEVI)
               .OrderByDescending(z => z.Count())
               .Select(y => y.Key)
               .FirstOrDefault();
            var deger8 = db.TBLiletisim.Count();
            var deger9 = db.TBLKitapTakip.Count();
            var deger10 = db.TBLKitapTakip
                .Join(db.TBLPersonel,
                      kitapTakip => kitapTakip.PERSONEL,
                      personel => personel.ID, 
                      (kitapTakip, personel) => new { kitapTakip, personel }) 
                .GroupBy(x => x.personel.PERSONEL) 
                .OrderByDescending(z => z.Count()) 
                .Select(y => y.Key) 
                .FirstOrDefault();
            var deger11 = db.TBLKitapTakip
                 .Join(db.TBLKitap,
                       kitapTakip => kitapTakip.KITAP, 
                       kitap => kitap.ID,
                       (kitapTakip, kitap) => new { kitapTakip, kitap })
                .GroupBy(x => x.kitap.AD)
                .OrderByDescending(z => z.Count()) 
                .Select(y => y.Key) 
                .FirstOrDefault();
            var deger12 = db.TBLKitapTakip
                .Join(db.TBLUyeler,
                      kitapTakip => kitapTakip.UYE,
                      enuye => enuye.ID,
                      (kitapTakip, enuye) => new { kitapTakip, enuye })
                .GroupBy(x => x.enuye.AD)
                .OrderByDescending(z => z.Count())
                .Select(y => y.Key)
                .FirstOrDefault();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            ViewBag.dgr5 = deger5;
            ViewBag.dgr6 = deger6;
            ViewBag.dgr7 = deger7;
            ViewBag.dgr8 = deger8;
            ViewBag.dgr9 = deger9;
            ViewBag.dgr10 = deger10;
            ViewBag.dgr11 = deger11;
            ViewBag.dgr12 = deger12;
            return View();
        }

        [HttpPost]
        public ActionResult resimyukle(HttpPostedFileBase dosya)
        {
            if (dosya.ContentLength > 0)
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/resimler/"), Path.GetFileName(dosya.FileName));
                dosya.SaveAs(dosyayolu);
            }
            return RedirectToAction("Galeri");
        }

    }
}