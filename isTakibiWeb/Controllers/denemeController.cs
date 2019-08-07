using isTakibiWeb.Classes;
using isTakibiWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace isTakibiWeb.Controllers
{
    public class denemeController : Controller
    {

        isTakipEntities entities = new isTakipEntities();
        // GET: deneme
        public ActionResult Index()
        {


            SqlConnection conn = new SqlConnection("Data Source=ESRA\\SQLEXPRESS; Initial Catalog=isTakip;integrated security=True;MultipleActiveResultSets=True;");
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT  PERSONEL_KOD,BITIRME_SURESI FROM TBLGOREV WHERE PROJE_KOD='istkp'", conn);

            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            sqlDataAdapter.Dispose();


            var gorevList = new List<Class1>();
           
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {

                Class1 class1 = new Class1();
                class1.personel_kod = dataTable.Rows[i]["PERSONEL_KOD"].ToString();
                class1.bitirme_suresi = Convert.ToInt32( dataTable.Rows[i]["BITIRME_SURESI"]);
               
                gorevList.Add(class1);
            }


           

            ViewBag.DataPoints = JsonConvert.SerializeObject(gorevList);


                return View();
            
           
        }
    }
}