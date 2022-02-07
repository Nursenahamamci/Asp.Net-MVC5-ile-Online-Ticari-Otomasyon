using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicaret.Models.Siniflar;
namespace MvcOnlineTicaret.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context c = new Context();
        public ActionResult Index()
        {
            var dpt = c.Departmans.Where(x=>x.Durum== true).ToList();
            return View(dpt);
        }
        [Authorize(Roles ="A")]
            [HttpGet]
            public ActionResult DepartmanEkle()
            {
                return View();
            }
            [HttpPost]
            public ActionResult DepartmanEkle(Departman k)
            {
                //verileri ekledi
                c.Departmans.Add(k);
                //veritabanına kaydetti
                c.SaveChanges();
                //sayfaya yönlendirdi
                return RedirectToAction("Index");
            }
        public ActionResult DepartmanSil(Departman id)
        {
            var ktg = c.Departmans.Find(id);
            ktg.Durum = false;
            c.SaveChanges();
            
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanGetir(int id)
        {

            var ktg = c.Departmans.Find(id);

            return View("DepartmanGetir", ktg);
        }
        public ActionResult DepartmanGuncelle(Departman k)
        {
            //sayfadaki ıd hafızsaya aldık
            var gnc = c.Departmans.Find(k.DepartmanId);
            // k parametre olarak viewe göndereceğimiz degerler
            gnc.DepartmanAd = k.DepartmanAd;
            
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanDetay(int id)
        {
            var degerler = c.Personels.Where(x => x.Departmanid == id).ToList();
            //deparatman adı alınıyor
            var dpt = c.Departmans.Where(x => x.DepartmanId == id).Select(y => y.DepartmanAd).FirstOrDefault();
            //contdan viewe veri taşıma =viewbag
            ViewBag.d = dpt;
            return View(degerler);
        }
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Personelid == id).ToList();
            var dpt = c.Personels.Where(x => x.PersonelId == id).Select(y => y.PersonelAd +" "+ y.PersonelSoyad).FirstOrDefault();
            //contdan viewe veri taşıma =viewbag
            ViewBag.dpers = dpt;
            return View(degerler);
        }
    }
    }
