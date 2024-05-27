using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgeKutuphanesi.Models.Entity;
using PagedList;

namespace BilgeKutuphanesi.Controllers
{
    public class MesajlarController : Controller
    {
        // GET: Mesajlar

        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var mesajlar = db.TBLMesajlar.Where(x => x.ALICI == uyemail.ToString()).ToList(); ;

            var d1 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.AD).FirstOrDefault();
            ViewBag.d1 = d1;
            var d2 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.SOYAD).FirstOrDefault();
            ViewBag.d2 = d2;
            var d3 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.FOTOGRAF).FirstOrDefault();
            ViewBag.d3 = d3;
            return View(mesajlar);
        }

        public ActionResult Giden()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var mesajlar = db.TBLMesajlar.Where(x => x.GONDEREN == uyemail.ToString()).ToList(); ;

            var d1 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.AD).FirstOrDefault();
            ViewBag.d1 = d1;
            var d2 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.SOYAD).FirstOrDefault();
            ViewBag.d2 = d2;
            var d3 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.FOTOGRAF).FirstOrDefault();
            ViewBag.d3 = d3;
            return View(mesajlar);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var d1 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.AD).FirstOrDefault();
            ViewBag.d1 = d1;
            var d2 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.SOYAD).FirstOrDefault();
            ViewBag.d2 = d2;
            var d3 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.FOTOGRAF).FirstOrDefault();
            ViewBag.d3 = d3;
            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(TBLMesajlar t)
        {
            var uyemail = (string)Session["Mail"].ToString();

            var aliciUye = db.TBLUyeler.FirstOrDefault(x => x.MAIL == t.ALICI);
            if (aliciUye == null)
            {
                ViewBag.ErrorMessage = "Alıcı e-posta adresi kayıtlı bir kullanıcıya ait değil. Yeniden Yazmanız İçin Yönlendiriliyorsunuz...";
                return View(t); 
            }

            if (string.IsNullOrWhiteSpace(t.ICERIK))
            {
                ViewBag.ErrorMessage = "İçerik alanı boş bırakılamaz. Yeniden Yazmanız İçin Yönlendiriliyorsunuz...";
                return View(t);
            }

            t.GONDEREN = uyemail.ToString();
            t.TARIH = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TBLMesajlar.Add(t);
            db.SaveChanges();

            var d1 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.AD).FirstOrDefault();
            ViewBag.d1 = d1;
            var d2 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.SOYAD).FirstOrDefault();
            ViewBag.d2 = d2;
            var d3 = db.TBLUyeler.Where(x => x.MAIL == uyemail).Select(y => y.FOTOGRAF).FirstOrDefault();
            ViewBag.d3 = d3;

            return RedirectToAction("Giden", "Mesajlar");
        }

        public ActionResult MesajSil(int id)
        {
            var mesaj = db.TBLMesajlar.Find(id);
            db.TBLMesajlar.Remove(mesaj);
            db.SaveChanges();
            return RedirectToAction("Giden");
        }

        public PartialViewResult Partial1()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var gelensayisi = db.TBLMesajlar.Where(x => x.ALICI == uyemail).Count();
            ViewBag.d1 = gelensayisi;

            var gidensayisi = db.TBLMesajlar.Where(x => x.GONDEREN == uyemail).Count();
            ViewBag.d2 = gidensayisi;
            return PartialView();
        }

    }
}
