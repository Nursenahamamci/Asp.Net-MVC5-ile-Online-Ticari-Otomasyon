using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicaret.Models.Siniflar;
namespace MvcOnlineTicaret.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        Context c = new Context();

        public ActionResult Index()
        {
            var degerler = c.SatisHarekets.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> deger1 = (from x in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.UrunId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.Cariid.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelId.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;

            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket k)
        {
            k.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            //verileri ekledi
            c.SatisHarekets.Add(k);
            //veritabanına kaydetti
            c.SaveChanges();
            //sayfaya yönlendirdi
            return RedirectToAction("Index");
        }
        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.UrunId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.Cariid.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelId.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            var deger = c.SatisHarekets.Find(id);
            return View("SatisGetir", deger);
        }
        public ActionResult Guncellemeİslemi(SatisHareket p)
        {
            var urn = c.SatisHarekets.Find(p.SatisId);
            urn.Urunid = p.Urunid;
            urn.Adet = p.Adet;
            urn.ToplamTutar = p.ToplamTutar;
     
            urn.Fiyat = p.Fiyat;
            urn.Cariid = p.Cariid;
            urn.Personelid = p.Personelid;
            urn.Tarih = p.Tarih;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisYazdir(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.SatisId == id).ToList();
       
            return View(degerler);
        }
    }
}