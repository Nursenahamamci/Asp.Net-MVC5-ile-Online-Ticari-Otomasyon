using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicaret.Models.Siniflar;
namespace MvcOnlineTicaret.Controllers
{
    public class İstatistikController : Controller
    {
        Context c = new Context();
        // GET: İstatistik
        public ActionResult Index()
        {
            var deger1 = c.Carilers.Count().ToString();
            var deger2 = c.Uruns.Count().ToString();
            var deger3 = c.Personels.Count().ToString();
            var deger4 = c.Kategoris.Count().ToString();
            var deger5 = c.Uruns.Sum(x=>x.Stok).ToString();
            var deger6 = (from x in c.Uruns select x.Marka).Distinct().Count().ToString();
            var deger7 = c.Uruns.Count(x=>x.Stok <=20).ToString();
            var deger8 = (from x in c.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
            var deger9 = (from x in c.Uruns orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();
            var deger10 = c.Uruns.Count(x => x.UrunAd=="Buzdolabı").ToString();
            var deger11 = c.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
            var deger12 = c.Uruns.GroupBy(x => x.Marka).OrderByDescending(z=>z.Count()).Select(y=>y.Key).FirstOrDefault();
            var deger13 = c.Uruns.Where(u => u.UrunId == (c.SatisHarekets.GroupBy(x => x.Urunid).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).Select(k => k.UrunAd).FirstOrDefault();
            var deger14 = c.SatisHarekets.Sum(x => x.ToplamTutar ).ToString();
            DateTime bugun = DateTime.Today;
            var deger15 = c.SatisHarekets.Count(x => x.Tarih==bugun).ToString();
            //var deger16 = c.SatisHarekets.Where(x => x.Tarih == bugun).Sum(y => (decimal)y.ToplamTutar).ToString();
            ViewBag.d1 = deger1;
            ViewBag.d2 = deger2;
            ViewBag.d3 = deger3;
            ViewBag.d4 = deger4;
            ViewBag.d5 = deger5;
            ViewBag.d6 = deger6;
            ViewBag.d7 = deger7;
            ViewBag.d8 = deger8;
            ViewBag.d9 = deger9;
            ViewBag.d10 = deger10;
            ViewBag.d11 = deger11;
            ViewBag.d12 = deger12;
            ViewBag.d13 = deger13;
            ViewBag.d14 = deger14;
            ViewBag.d15 = deger15;
            //ViewBag.d16 = deger16;
            return View();
        }
        public ActionResult KolayTablolar()
        {
            var s = from x in c.Carilers
                    group x by x.CariSehir into g
                    select new SinifGrup
                    {
                        Sehir = g.Key,
                        Sayi = g.Count()
                    };
            return View(s.ToList());
        }
        public PartialViewResult Partial1()
        {
            var sorgu = from x in c.Personels
                        group x by x.Departman.DepartmanAd into g
                        select new SinifGrup2
                        {
                            Departman = g.Key,
                            Sayi = g.Count()
                        };

            return PartialView(sorgu.ToList());
        }
        public PartialViewResult Partial2()
        {
            var sorgu = c.Carilers.ToList();

            return PartialView(sorgu);
        }
    }
}