using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace isTakibiWeb.Controllers
{
    public class ProjePersonelController : Controller
    {
        // GET: ProjePersonel
        Classes.db dblayer = new Classes.db();
        public ActionResult Index()
        {
            Proje_Bind();
            return View();
        }

        public void Proje_Bind()
        {
            DataSet ds = dblayer.Get_PROJE();
            List<SelectListItem> projelist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                projelist.Add(new SelectListItem { Text = dr["PROJE_ADI"].ToString(), Value = dr["PROJE_KOD"].ToString() });
            }
            ViewBag.Proje_KOD = projelist;
        }

        public JsonResult Personel_Bind(string PROJE_KOD)
        {
            DataSet ds = dblayer.Get_PERSONEL(PROJE_KOD);
            List<SelectListItem> personellist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                personellist.Add(new SelectListItem { Text = dr["PERSONEL_ADI"].ToString(), Value = dr["PERSONEL_KOD"].ToString() });
            }
            return Json(personellist, JsonRequestBehavior.AllowGet);
        }
    }
}