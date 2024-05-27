using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using BilgeKutuphanesi.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace BilgeKutuphanesi.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap

        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index(string p, int sayfa=1)
        {
            var kitaplar = from k in db.TBLKitap select k;
            if (!string.IsNullOrEmpty(p))
            {
                kitaplar = kitaplar.Where(m => m.AD.Contains(p));
            }            
            return View(kitaplar.OrderByDescending(k => k.ID).ToList().ToPagedList(sayfa, 10));

        }
        [HttpGet]
        public ActionResult KitapEkle()
        {
            List<SelectListItem> deger1 = (from i in db.TBLKategori.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLYazar.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD + ' ' + i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            return View();
        }

        [HttpPost]
        public ActionResult KitapEkle(TBLKitap p)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    string dosyaadi = Path.GetFileNameWithoutExtension(file.FileName);
                    string uzanti = Path.GetExtension(file.FileName).ToLower();
                    string yolu = "~/Image/" + dosyaadi + uzanti;

                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    if (!allowedExtensions.Contains(uzanti))
                    {
                        ViewBag.Error = "Sadece JPG, JPEG veya PNG dosyalarına izin verilir.";
                        return View(p);
                    }

                    if (file.ContentLength > 1242880) 
                    {
                        ViewBag.Error = "Dosya boyutu 1 MB'dan küçük olmalıdır.";
                        return View(p);
                    }

                    using (var ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        using (var img = Image.FromStream(ms))
                        {                            
                            int targetWidth = 220;
                            int targetHeight = 340;
                            var resizedImg = new Bitmap(targetWidth, targetHeight);
                            using (var graphics = Graphics.FromImage(resizedImg))
                            {
                                graphics.CompositingQuality = CompositingQuality.HighQuality;
                                graphics.SmoothingMode = SmoothingMode.HighQuality;
                                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                graphics.DrawImage(img, 0, 0, targetWidth, targetHeight);
                            }                           
                            string savePath = Path.Combine(Server.MapPath("~/Image/"), dosyaadi + uzanti);
                            resizedImg.Save(savePath, ImageFormat.Jpeg);
                            p.KITAPRESIM = yolu;
                        }
                    }
                }
            }
            var ktg = db.TBLKategori.Where(k => k.ID == p.TBLKategori.ID).FirstOrDefault();
            var yzr = db.TBLYazar.Where(y => y.ID == p.TBLYazar.ID).FirstOrDefault();
            p.TBLKategori = ktg;
            p.TBLYazar = yzr;
            db.TBLKitap.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KitapSil(int id)
        {
            var kitap = db.TBLKitap.Find(id);
            kitap.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KitapGetir(int id)
        {
            var ktp = db.TBLKitap.Find(id);
            List<SelectListItem> deger1 = (from i in db.TBLKategori.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLYazar.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD + ' ' + i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;

            return View("KitapGetir", ktp);
        }
        public ActionResult KitapGuncelle(TBLKitap p)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    string dosyaadi = Path.GetFileNameWithoutExtension(file.FileName);
                    string uzanti = Path.GetExtension(file.FileName).ToLower();
                    string yolu = "~/Image/" + dosyaadi + uzanti;

                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    if (!allowedExtensions.Contains(uzanti))
                    {
                        ViewBag.Error = "Sadece JPG, JPEG veya PNG dosyalarına izin verilir.";
                        return View(p);
                    }

                    if (file.ContentLength > 1242880)
                    {
                        ViewBag.Error = "Dosya boyutu 1 MB'dan küçük olmalıdır.";
                        return View(p);
                    }

                    using (var ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        using (var img = Image.FromStream(ms))
                        {
                            int targetWidth = 220;
                            int targetHeight = 340;
                            var resizedImg = new Bitmap(targetWidth, targetHeight);
                            using (var graphics = Graphics.FromImage(resizedImg))
                            {
                                graphics.CompositingQuality = CompositingQuality.HighQuality;
                                graphics.SmoothingMode = SmoothingMode.HighQuality;
                                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                graphics.DrawImage(img, 0, 0, targetWidth, targetHeight);
                            }

                            string savePath = Path.Combine(Server.MapPath("~/Image/"), dosyaadi + uzanti);
                            resizedImg.Save(savePath, ImageFormat.Jpeg);
                            p.KITAPRESIM = yolu;
                        }
                    }
                }
            }
            var kitap = db.TBLKitap.Find(p.ID);
            kitap.AD = p.AD;
            kitap.BASIMYIL = p.BASIMYIL;
            kitap.SAYFA = p.SAYFA;
            kitap.YAYINEVI = p.YAYINEVI;
            kitap.DURUM = true;
            var ktg = db.TBLKategori.Where(k => k.ID == p.TBLKategori.ID).FirstOrDefault();
            var yzr = db.TBLYazar.Where(y => y.ID == p.TBLYazar.ID).FirstOrDefault();
            kitap.KATEGORI = ktg.ID;
            kitap.YAZAR = yzr.ID;
            kitap.KITAPRESIM = p.KITAPRESIM;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}