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

        SqlConnection conn = new SqlConnection("Data Source=ESRA\\SQLEXPRESS; Initial Catalog=isTakip;integrated security=True;MultipleActiveResultSets=True;");

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
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT GOREV.GOREV_KOD,PROJE.PROJE_ADI, GOREV.GOREV_TANIMI  ,GOREV.DURUM FROM  TBLGOREV GOREV, TBLKULLANICI KULLANICI, TBLPROJE PROJE, TBLPROJEPERSONEL PROJPERSONEL WHERE GOREV.PERSONEL_KOD = KULLANICI.PERSONEL_KOD AND PROJE.PROJE_KOD = GOREV.PROJE_KOD AND PROJE.PROJE_KOD = PROJPERSONEL.PROJE_KOD AND GOREV.PERSONEL_KOD = PROJPERSONEL.PERSONEL_KOD AND KULLANICI.REC_ID = " + Session["UserId"] + " AND GOREV.DURUM IN ('1', '2')", conn);
          
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            sqlDataAdapter.Dispose();


            //List<gorevTablo> gorevList = new List<gorevTablo>();
            
            var gorevList = new List<gorevTablo>();
         
            for (int i = 0;i < dataTable.Rows.Count;i++)
            {
                gorevTablo gTablo = new gorevTablo();
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
            return RedirectToAction("GörevGörüntüle");
        }

        public ActionResult gorevtamamla(string id)
        {
            TBLDOKUMAN model = new TBLDOKUMAN();

            TBLGOREV gorevmodel = new TBLGOREV();

            gorevmodel = entities.TBLGOREV.Find(id);

            model.PROJE_KOD = gorevmodel.PROJE_KOD;
            model.GOREV_KOD = id;

            return View(model);

        }
        [HttpPost]
        public ActionResult gorevtamamla(TBLDOKUMAN model, HttpPostedFileBase file)
        {
            if (file != null)
            {
                byte[] belge = new byte[file.ContentLength];
                file.InputStream.Read(belge, 0, file.ContentLength);
                model.BELGE = belge;

                model.REC_DATE = DateTime.Now;
                model.REC_UPDATE = DateTime.Now;
                model.REC_UPUSERNAME = (string)Session["UserName"];
                model.REC_UPUSERNO = (int)Session["UserId"];
                model.REC_USERNAME = (string)Session["UserName"];
                model.REC_USERNO = (int)Session["UserId"];
                model.REC_CHANGED = "0";
                model.REC_VERSION = CreateCommon.REC_VERSION;

                entities.TBLDOKUMAN.Add(model);

                entities.SaveChanges();

                TBLGOREV gorevmodel = new TBLGOREV();
                gorevmodel = entities.TBLGOREV.Where(m => m.GOREV_KOD.Equals(model.GOREV_KOD)).FirstOrDefault();
                gorevmodel.DURUM = 3;
                entities.Entry(gorevmodel).State = EntityState.Modified;
                entities.SaveChanges();
            }
           
            return View();
        }
        public ActionResult x()
        {
            return View();
        }

        [HttpPost]
        public ActionResult taskCheck(string gorev_kod,DateTime start_date,int complate_time)
        {


            return View();
        }
    }
}
