using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MvcOkul.Models.Entity;

namespace MvcOkul.Controllers
{
    public class KulupController : Controller
    {
        // GET: Kulup
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index(int sayfa=1)
        {
            
            var kulup = db.Tbl_Kulüpler.ToList().ToPagedList(sayfa, 9); 
            return View(kulup);
        }
        [HttpGet]
        public ActionResult KulupEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KulupEkle(Tbl_Kulüpler p)
        {
            if (!ModelState.IsValid)
            {
                return View("KulupEkle");
            }
            db.Tbl_Kulüpler.Add(p);
            p.KlpDurum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var bul = db.Tbl_Kulüpler.Find(id);
            bul.KlpDurum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Aktifyap(int id)
        {
            var bul = db.Tbl_Kulüpler.Find(id);
            bul.KlpDurum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult KlpGetir(int id)
        {
            var bul = db.Tbl_Kulüpler.Find(id);
            return View("KlpGetir", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Kulüpler p)
        {
            if (!ModelState.IsValid)
            {
                return View("KlpGetir");
            }
            var klp = db.Tbl_Kulüpler.Find(p.KulupId);
            klp.KlpAd = p.KlpAd;
            db.SaveChanges();
            return RedirectToAction("Index","Kulup");
        }
        
    }
}