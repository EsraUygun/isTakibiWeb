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
    public class ProjeController : Controller
    {
        isTakipEntities entities = new isTakipEntities();

        SqlConnection conn = new SqlConnection("Data Source=ESRA\\SQLEXPRESS; Initial Catalog=isTakip;integrated security=True;MultipleActiveResultSets=True;");


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProjeDüzenle(string id)
        {
            return View();
        }

        public ActionResult ProjeDetay(string id)
        {

            var model = from f in entities.TBLPROJEPERSONEL
                           select f;


            model = model.Where(f => f.PROJE_KOD.Contains(id));
            return View(model.ToList());
            ////return View(Tuple.Create(tBLGOREV.ToList(),tBLPROJEPERSONEL.ToList()));
        }

        [HttpPost]//????????????????????????????????
        public ActionResult GörevBelgeGörüntüle(string PERSONEL_KOD , string PROJE_KOD)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT GOREV.GOREV_TANIMI,DOKUMAN.BELGE FROM TBLDOKUMAN DOKUMAN,TBLGOREV GOREV WHERE GOREV.PROJE_KOD='" + PROJE_KOD + "'and GOREV.PERSONEL_KOD='" + PERSONEL_KOD + "' AND GOREV.GOREV_KOD=DOKUMAN.GOREV_KOD",conn);

            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            sqlDataAdapter.Dispose();

            var gorevList = new List<gorevBelge>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                gorevBelge gBelge = new gorevBelge();
                gBelge.GOREV_TANIMI = dataTable.Rows[i]["GOREV_TANIMI"].ToString();
               //EKSİKKKKKK
                // gBelge.BELGE = Convert.ToByte(dataTable.Rows[i]["BELGE"]);
               
                gorevList.Add(gBelge);
            }

            return View(gorevList);

        }


    }
}