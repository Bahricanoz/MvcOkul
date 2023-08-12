using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOkul.Models;
using PagedList.Mvc;
using PagedList;
using MvcOkul.Models.Entity;


namespace MvcOkul.Controllers
{
    public class NotlarController : Controller
    {
        // GET: Notlar
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index(int sayfa=1)
        {
            var notlar = db.Tbl_Notlar.Where(x => x.Tbl_Ogrenciler.OgrDurum == true && x.Tbl_Dersler.Dersdurum== true).ToList().ToPagedList(sayfa,9);
            return View(notlar);
        }
        [HttpGet]
        public ActionResult NotEkle()
        {
            List<SelectListItem> ogrenciler = (from i in db.Tbl_Ogrenciler.Where(x=>x.OgrDurum==true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.OgrId.ToString(),
                                                   Value = i.OgrId.ToString()
                                               }).ToList();
            ViewBag.ogr = ogrenciler;
            List<SelectListItem> degerler = (from i in db.Tbl_Dersler.Where(x=>x.Dersdurum==true).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.DersAd,
                                                 Value = i.DersId.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult NotEkle(Tbl_Notlar p, Class1 model, int Sınav1=0, int Sınav2=0, int Sınav3=0, int Proje=0)
        {
            
            if (model.islem == "NotHesapla")
            {
                int ortalama = (Sınav1 + Sınav2 + Sınav3 + Proje) / 4;
                db.Tbl_Notlar.Add(p);
                p.Ortalama = ortalama;
                if(p.Ortalama >= 50)
                {
                    p.Durum = true;
                }
                else
                {
                    p.Durum = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Notguncelle(int id)
        {
            var bul = db.Tbl_Notlar.Find(id);
            return View("Notguncelle",bul);
        }
        [HttpPost]
        public ActionResult Notguncelle(Class1 model, string durum, Tbl_Notlar p,int Sınav1=0, int Sınav2=0, int Sınav3=0,int Proje=0)
        { 

            if (model.islem == "Notgnclle")
            {
                int ortalama = (Sınav1 + Sınav2 + Sınav3 + Proje) / 4;
                ViewBag.ort = ortalama;
                
                //islem2
                var not = db.Tbl_Notlar.Find(p.NotId);
                not.Sınav1 = p.Sınav1;
                not.Sınav2 = p.Sınav2;
                not.Sınav3 = p.Sınav3;
                not.Proje = Proje;
                not.Ortalama = ortalama;
                if (ortalama>=50)
                {
                    not.Durum = true;
                }
                else
                {
                    not.Durum = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index", "Notlar");
            }

            return View();
        }
    }
}
