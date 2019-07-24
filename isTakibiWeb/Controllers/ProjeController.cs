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

        public ActionResult ProjeDetay(string projekod)
        {
            TBLPROJEPERSONEL tBLPROJEPERSONEL = new TBLPROJEPERSONEL();
            TBLGOREV tBLGOREV = new TBLGOREV();
            tBLPROJEPERSONEL = entities.TBLPROJEPERSONEL.Find(projekod);
            tBLGOREV = entities.TBLGOREV.Find(projekod);
            return View(Tuple.Create(tBLGOREV,tBLPROJEPERSONEL));
        }

        public ActionResult ProjeSil(string projekod)
        {
            return View();
        }
    }
}