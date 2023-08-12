using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
using System.Web.Mvc;
using MvcOkul.Models.Entity;

namespace MvcOkul.Controllers
{
    public class OgrenciController : Controller
    {
        // GET: Ogrenci
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index(int sayfa=1)
        {
            var ogrenci = db.Tbl_Ogrenciler.ToList().ToPagedList(sayfa,9);
            return View(ogrenci);
        }
        [HttpGet]
        public ActionResult OgrenciEkle()
        {
            List<SelectListItem> degerler = (from i in db.Tbl_Kulüpler.Where(x=>x.KlpDurum== true).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KlpAd,
                                                 Value = i.KulupId.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;                            
            return View();
        }
        [HttpPost]
        public ActionResult OgrenciEkle(Tbl_Ogrenciler p)
        {
            
            var klp = db.Tbl_Kulüpler.Where(m => m.KulupId == p.Tbl_Kulüpler.KulupId).FirstOrDefault();
            p.Tbl_Kulüpler = klp;
            db.Tbl_Ogrenciler.Add(p);
            p.OgrDurum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult OgrSil(int id)
        {
            var bul = db.Tbl_Ogrenciler.Find(id);
            bul.OgrDurum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AktifYap(int id)
        {
            var bul = db.Tbl_Ogrenciler.Find(id);
            bul.OgrDurum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult OgrGuncelle(int id)
        {
            List<SelectListItem> degerler = (from i in db.Tbl_Kulüpler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KlpAd,
                                                 Value = i.KulupId.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            var bul = db.Tbl_Ogrenciler.Find(id);
            return View("OgrGuncelle", bul);
        }
        [HttpPost]
        public ActionResult OgrGuncelle(Tbl_Ogrenciler p)
        {
            var ogr = db.Tbl_Ogrenciler.Find(p.OgrId);
            ogr.OgrAd = p.OgrAd;
            ogr.OgrSoyad = p.OgrSoyad;
            ogr.OgrCinsiyet = p.OgrCinsiyet;
            ogr.OgrFoto = p.OgrFoto;
            ogr.OgrKulup = p.OgrKulup;
            db.SaveChanges();
            return RedirectToAction("Index", "Ogrenci");
        }
    }
}