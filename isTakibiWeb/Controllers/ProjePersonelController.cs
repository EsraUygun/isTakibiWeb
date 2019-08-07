using isTakibiWeb.Classes;
using isTakibiWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace isTakibiWeb.Controllers
{
    public class ProjePersonelController : Controller
    {
        isTakipEntities entities = new isTakipEntities();
        // GET: ProjePersonel
        Classes.db dblayer = new Classes.db();
        public ActionResult gorevAta()
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
        [HttpPost]
        public ActionResult gorevAta(TBLGOREV model)

        {
            if(model.GOREV_KOD!="" & model.PERSONEL_KOD!=""& model.PROJE_KOD!="" & model.GOREV_TANIMI!="")
            {
                model.REC_DATE = DateTime.Now;
                model.REC_UPDATE = DateTime.Now;
                model.REC_UPUSERNAME = (String)Session["UserName"];
                model.REC_UPUSERNO = (int)Session["UserId"];
                model.REC_USERNAME = (String)Session["UserName"];
                model.REC_USERNO = (int)Session["UserId"];
                model.REC_CHANGED = "0";
                model.REC_VERSION = CreateCommon.REC_VERSION;
                model.DURUM = 1;
                entities.TBLGOREV.Add(model);

                entities.SaveChanges();
                return RedirectToAction("Index", "ProjePersonel");
            }
            else
            {
              
                return View();
            }
            

        }
        
        public JsonResult gorevkodTest()
        {
            var routeValue = RouteData.Values["id"];


            //SqlConnection conn = new SqlConnection("Data Source=ESRA\\SQLEXPRESS; Initial Catalog=isTakip;integrated security=True;MultipleActiveResultSets=True;");
            //conn.Open();
            //SqlCommand command = new SqlCommand("select '" + routeValue + "'", conn);

            //DataTable dataTable = new DataTable();
            //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            //sqlDataAdapter.Fill(dataTable);
            //sqlDataAdapter.Dispose();

           Boolean result = false;
            TBLGOREV id = entities.TBLGOREV.SingleOrDefault(x => x.GOREV_KOD.Equals(routeValue.ToString()));
            if(id != null )
            {
                 result = true;
            }
           
           
           
            return Json(result ,JsonRequestBehavior.AllowGet);
        }
    }
}