using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgeKutuphanesi.Models.Entity;
using PagedList;

namespace BilgeKutuphanesi.Controllers
{
    public class islemController : Controller
    {
        // GET: islem
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index(int sayfa = 1)
        {
            var degerler = db.TBLKitapTakip
                 .Where(x => x.ISLEMDURUM == true)
                 .OrderByDescending(x => x.ID) 
                 .ToPagedList(sayfa, 10);
                return View(degerler);
        }
    }
}