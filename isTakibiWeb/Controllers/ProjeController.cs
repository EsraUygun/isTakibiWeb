using isTakibiWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace isTakibiWeb.Controllers
{
    public class ProjeController : Controller
    {
        isTakipEntities entities = new isTakipEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProjeDüzenle(string projekod)
        {
            return View();
        }

        public ActionResult ProjeDetay(string id)
        {
            
        
            var tBLGOREV = from f in entities.TBLGOREV
                              select f;

            tBLGOREV = tBLGOREV.Where(f => f.PROJE_KOD.Contains(id));
            return View(tBLGOREV.ToList());
            //return View(Tuple.Create(tBLGOREV.ToList(),tBLPROJEPERSONEL.ToList()));
        }

        public ActionResult ProjeSil(string projekod)
        {
            return View();
        }



    }
}