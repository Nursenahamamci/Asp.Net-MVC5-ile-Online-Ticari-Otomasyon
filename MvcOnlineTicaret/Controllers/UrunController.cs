using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MvcOnlineTicaret.Models.Siniflar;
namespace MvcOnlineTicaret.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index(int sayfa=1)
        {
            //listeleme
            var urunler = c.Uruns.Where(x=> x.Durum==true).ToList().ToPagedList(sayfa, 4); 
            return View(urunler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            //Text kısmında kategori adı yazcak ön tarafta seçmeli olarak göstericek
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value =x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Urun k)
        {
            //verileri ekledi
            c.Uruns.Add(k);
            //veritabanına kaydetti
            c.SaveChanges();
            //sayfaya yönlendirdi
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var urn = c.Uruns.Find(id);
            urn.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult UrunGuncelle(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var gnc = c.Uruns.Find(id);

                return View("UrunGuncelle", gnc);
            }
        public ActionResult Guncellemeİslemi(Urun p)
        {
            var urn = c.Uruns.Find(p.UrunId);
            urn.UrunAd = p.UrunAd;
            urn.Stok = p.Stok;
            urn.Marka = p.Marka;
            urn.SatisFiyat = p.SatisFiyat;
            urn.AlisFiyat = p.AlisFiyat;
            urn.Durum = p.Durum;
            urn.Kategoriid = p.Kategoriid;
            urn.UrunGorsel = p.UrunGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunListesi()
        {
            var dger = c.Uruns.ToList();
            return View(dger);
        }
        }
    }
