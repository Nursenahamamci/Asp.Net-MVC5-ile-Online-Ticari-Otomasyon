using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicaret.Models.Siniflar;
namespace MvcOnlineTicaret.Controllers
{
    public class FaturaController : Controller
    {
        Context c = new Context();
        // GET: Fatura
        public ActionResult Index()
        {
            var f = c.Faturalars.ToList();
            return View(f);
        }


        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar k)
        {
            //verileri ekledi
            c.Faturalars.Add(k);
            //veritabanına kaydetti
            c.SaveChanges();
            //sayfaya yönlendirdi
            return RedirectToAction("Index");
        }
        public ActionResult FaturaGetir(int id)
        {
            var deger = c.Faturalars.Find(id);
            return View("FaturaGetir", deger);
        }
        public ActionResult GuncellemeIslemi(Faturalar p )
        {
            var urn = c.Faturalars.Find(p.FaturaId);
                 urn.FaturaSeriNo =p.FaturaSeriNo ;
            urn.FaturaSırano = p.FaturaSırano;
            urn.Saat = p.Saat;
            urn.Tarih = p.Tarih;
            urn.TeslimAlan = p.TeslimAlan;
            urn.TeslimEden = p.TeslimEden;
            urn.Toplam = p.Toplam;
            urn.VergiDairesi = p.VergiDairesi;
           
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
            var degerler = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();

            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKalem()
        {
            var ftrk = c.FaturaKalems.ToList();
            return View(ftrk);
        }
        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem f)
        {
            c.FaturaKalems.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");

        }
    }
    
}