using isTakibiWeb.Classes;
using isTakibiWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace isTakibiWeb.Controllers
{
    public class PersonelController : Controller
    {
        isTakipEntities entities = new isTakipEntities();
        public ActionResult Index()
        {
            return View();
        }

        //public partial class GorevTablo {
        //    public string PROJE_ADI { get; set; }
        //    public string GOREV_TANIMI { get; set; }
        //}

        public ActionResult GörevGörüntüle()
        {
            SqlConnection conn = new SqlConnection("Data Source=ESRA\\SQLEXPRESS; Initial Catalog=isTakip;integrated security=True;MultipleActiveResultSets=True;");
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT GOREV.GOREV_KOD,PROJE.PROJE_ADI, GOREV.GOREV_TANIMI  ,GOREV.DURUM FROM  TBLGOREV GOREV, TBLKULLANICI KULLANICI, TBLPROJE PROJE, TBLPROJEPERSONEL PROJPERSONEL WHERE GOREV.PERSONEL_KOD = KULLANICI.PERSONEL_KOD AND PROJE.PROJE_KOD = GOREV.PROJE_KOD AND PROJE.PROJE_KOD = PROJPERSONEL.PROJE_KOD AND GOREV.PERSONEL_KOD = PROJPERSONEL.PERSONEL_KOD AND KULLANICI.REC_ID = " + Session["UserId"] + " AND GOREV.DURUM IN ('1', '2')", conn);
          
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            sqlDataAdapter.Dispose();


            //List<gorevTablo> gorevList = new List<gorevTablo>();
            
            var gorevList = new List<gorevTablo>();
            gorevTablo gTablo = new gorevTablo();
            gorevTablo RecATable = new gorevTablo();
            gorevTablo UnRecATable = new gorevTablo();
            for (int i = 0;i < dataTable.Rows.Count;i++)
            {
                gTablo.GOREV_KOD = dataTable.Rows[i]["GOREV_KOD"].ToString();
                gTablo.PROJE_ADI = dataTable.Rows[i]["PROJE_ADI"].ToString();
                gTablo.GOREV_TANIMI = dataTable.Rows[i]["GOREV_TANIMI"].ToString();
                gTablo.DURUM = (decimal)dataTable.Rows[i]["DURUM"];
                gorevList.Add(gTablo);
            }
         

            return View(gorevList);
        }
        public ActionResult gorevonay(string id)
        {
            TBLGOREV model = new TBLGOREV();
            model = entities.TBLGOREV.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult gorevonay(TBLGOREV model)
        {
            model.REC_VERSION = CreateCommon.REC_VERSION;
            model.REC_UPUSERNO =(int) Session["UserId"];
            model.REC_UPUSERNAME = (string)Session["UserName"];
            model.REC_UPDATE = DateTime.Now;
            model.REC_CHANGED = "1";
            model.DURUM = 2;
            entities.Entry(model).State = EntityState.Modified;
            entities.SaveChanges();
            return RedirectToAction("GörevGörünrüle");
        }
    }
}