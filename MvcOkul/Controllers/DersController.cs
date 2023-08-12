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
    public class DersController : Controller
    {
        // GET: Ders
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Index(int sayfa=1)
        {
            var dersler = db.Tbl_Dersler.ToList().ToPagedList(sayfa,9);
            return View(dersler);
        }
        [HttpGet]
        public ActionResult DersEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DersEkle(Tbl_Dersler p)
        {
            if (!ModelState.IsValid)
            {
                return View("DersEkle");
            }
            db.Tbl_Dersler.Add(p);
            p.Dersdurum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DersSil(int id)
        {
            var bul = db.Tbl_Dersler.Find(id);
            bul.Dersdurum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Aktifyap(int id)
        {
            var bul = db.Tbl_Dersler.Find(id);
            bul.Dersdurum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DersGetir(int id)
        {
            var bul = db.Tbl_Dersler.Find(id);
            return View("DersGetir", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Dersler p)
        {
            if (!ModelState.IsValid)
            {
                return View("DersGetir");
            }
            var ders = db.Tbl_Dersler.Find(p.DersId);
            ders.DersAd = p.DersAd;
            db.SaveChanges();
            return RedirectToAction("Index", "Ders");
              
        }
    }
}