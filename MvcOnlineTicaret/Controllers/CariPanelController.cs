using MvcOnlineTicaret.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicaret.Controllers
{
    public class CariPanelController : Controller
    {
        Context c = new Context();
        // GET: CariPanel
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Carilers.FirstOrDefault(x => x.CariMail == mail);
            ViewBag.m = mail;
            return View(degerler);
        }
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.Cariid).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }
        public ActionResult KargoTakip(string p)
        {
            var k = from x in c.KargoModels select x;

            k = k.Where(y => y.TakipKodu.Contains(p));


            return View(k.ToList());
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        //public ActionResult GelenMesajlar()
        //{
        //    var mail = (string)Session["CariMail"];
        //    var mesajlar = c.mesajlars.Where(x=>x.Alici==mail).ToList();
        //    var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
        //    ViewBag.d1 = gelensayisi;
        //    var gidensayisi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
        //    ViewBag.d2 = gidensayisi;
        //    return View(mesajlar);
        //}
        //public ActionResult GidenMesajlar()
        //{
        //    var mail = (string)Session["CariMail"];
        //    var mesajlar = c.mesajlars.Where(x => x.Gonderici == mail).ToList();
        //    var gidensayisi = c.mesajlars.Count(x => x.Gonderici == mail).ToString();
        //    ViewBag.d2 = gidensayisi;
        //    var gelensayisi = c.mesajlars.Count(x => x.Alici == mail).ToString();
        //    ViewBag.d1 = gelensayisi;
        //    return View(mesajlar);
        //}
        //[HttpGet]
        //public ActionResult YeniMesaj()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult YeniMesaj()
        //{
        //    return View();
        //}
    }
    
}