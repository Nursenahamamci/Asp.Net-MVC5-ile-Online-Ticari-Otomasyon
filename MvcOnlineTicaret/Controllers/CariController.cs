using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicaret.Models.Siniflar;
namespace MvcOnlineTicaret.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari 
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Carilers.Where(x => x.Durum==true).ToList();
            return View(degerler);
        }
       
       
        [HttpGet]
        public ActionResult CariEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CariEkle(Cariler k)
        {
            k.Durum = true;
            //verileri ekledi
            c.Carilers.Add(k);
            //veritabanına kaydetti
            c.SaveChanges();
            //sayfaya yönlendirdi
            return RedirectToAction("Index");
        }
        public ActionResult CariSil(int id)
        {
            var cr = c.Carilers.Find(id);
            cr.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariGetir(int id)
        {

            var cari = c.Carilers.Find(id);

            return View("CariGetir", cari);
        }
        public ActionResult CariGuncelle(Cariler k)
        {
            if (!ModelState.IsValid)
            {
                return View("CariGetir");
            }
            //sayfadaki ıd hafızsaya aldık
            var gnc = c.Carilers.Find(k.Cariid);
            // k parametre olarak viewe göndereceğimiz degerler
            gnc.CariAd = k.CariAd;
            gnc.CariSoyad = k.CariSoyad;
            gnc.CariSehir = k.CariSehir;
            gnc.CariMail = k.CariMail;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            var dpt = c.Carilers.Where(x => x.Cariid == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            //contdan viewe veri taşıma =viewbag
            ViewBag.dcari = dpt;
            return View(degerler);
        }
    }
}