using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicaret.Models.Siniflar;
using PagedList;
using PagedList.Mvc;
namespace MvcOnlineTicaret.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();
        public ActionResult Index(int sayfa =1)
        {
            var degerler = c.Kategoris.ToList().ToPagedList(sayfa,4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            //verileri ekledi
            c.Kategoris.Add(k);
            //veritabanına kaydetti
            c.SaveChanges();
            //sayfaya yönlendirdi
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var ktg = c.Kategoris.Find(id);
            c.Kategoris.Remove(ktg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {

            var ktg = c.Kategoris.Find(id);
          
            return View("KategoriGetir", ktg);
        }
        public ActionResult KategoriGuncelle(Kategori k)
        {
            //sayfadaki ıd hafızsaya aldık
            var gnc = c.Kategoris.Find(k.KategoriID);
            // k parametre olarak viewe göndereceğimiz degerler
            gnc.KategoriAd = k.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}