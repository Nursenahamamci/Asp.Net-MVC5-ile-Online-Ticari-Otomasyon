using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicaret.Models.Siniflar;
namespace MvcOnlineTicaret.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Personels.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            //Text kısmında kategori adı yazcak ön tarafta seçmeli olarak göstericek
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.DepartmanId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
            
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel k)
        {
            if(Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                k.PersonelGorsel = "~/Image/" + dosyaadi + uzanti;
            }
            //verileri ekledi
            c.Personels.Add(k);
            //veritabanına kaydetti
            c.SaveChanges();
            //sayfaya yönlendirdi
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            //Text kısmında kategori adı yazcak ön tarafta seçmeli olarak göstericek
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.DepartmanId.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            
            var dgr = c.Personels.Find(id);
            return View("PersonelGetir",dgr);
        }
        
        public ActionResult PersonelGuncelle(Personel k)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                k.PersonelGorsel = "~/Image/" + dosyaadi + uzanti;
            }
            //sayfadaki ıd hafızsaya aldık
            var gnc = c.Personels.Find(k.PersonelId);
            // k parametre olarak viewe göndereceğimiz degerler
            gnc.PersonelAd = k.PersonelAd;
            gnc.PersonelSoyad = k.PersonelSoyad;
            gnc.PersonelGorsel = k.PersonelGorsel;
            gnc.Departmanid = k.Departmanid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelListe()
        {
            var sorgu = c.Personels.ToList();
            return View(sorgu);
        }
    }
}