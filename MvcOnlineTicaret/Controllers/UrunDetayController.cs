using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicaret.Models.Siniflar;
namespace MvcOnlineTicaret.Controllers
{
    public class UrunDetayController : Controller
    {
        Context c = new Context();
        // GET: UrunDetay
        public ActionResult Index()
        {
            Class1 cls = new Class1();
         //   var deger = c.Uruns.Where(x=>x.UrunId==1).ToList();
         cls.Deger1= c.Uruns.Where(x => x.UrunId == 1).ToList();
            cls.Deger2 = c.Detays.Where(y => y.DetayID == 1).ToList();
            return View(cls);
        }
    }
}