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
using System.Web.Security;

namespace isTakibiWeb.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        isTakipEntities entities = new isTakipEntities();

        SqlConnection conn = new SqlConnection("Data Source=ESRA\\SQLEXPRESS; Initial Catalog=isTakip;integrated security=True;MultipleActiveResultSets=True;");


        [HttpPost]
    public JsonResult PersonelTest()
        {

            var routeValue = RouteData.Values["id"];
            TBLPERSONEL personel = new TBLPERSONEL();

            SqlConnection conn = new SqlConnection("Data Source=ESRA\\SQLEXPRESS; Initial Catalog=isTakip;integrated security=True;MultipleActiveResultSets=True;");
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT  PERSONEL_KOD, PERSONEL_ADI, PERSONEL_SOYADI,TEL_NO FROM TBLPERSONEL WHERE PERSONEL_KOD ='" + routeValue + "'", conn);

            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            sqlDataAdapter.Dispose();


            //count = Convert.ToInt16(command.ExecuteScalar());
            if (command != null)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {

                    personel.PERSONEL_KOD = dataTable.Rows[i]["PERSONEL_KOD"].ToString();
                    personel.PERSONEL_ADI = dataTable.Rows[i]["PERSONEL_ADI"].ToString();
                    personel.PERSONEL_SOYADI = dataTable.Rows[i]["PERSONEL_SOYADI"].ToString();
                    personel.TEL_NO = dataTable.Rows[0]["TEL_NO"].ToString();
                }
            }

            return Json(personel, JsonRequestBehavior.AllowGet);

        }


    [HttpPost]
    public JsonResult ProjeTest()
    {
        var routeValue = RouteData.Values["id"];
            TBLPROJE proje = new TBLPROJE();
        conn.Open();
        SqlCommand command = new SqlCommand("SELECT  PROJE_TURU, PROJE_ADI, PROJE_TANIMI FROM TBLPROJE WHERE PROJE_KOD ='" + routeValue + "'", conn);

        DataTable dataTable = new DataTable();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
        sqlDataAdapter.Fill(dataTable);
        sqlDataAdapter.Dispose();


            //count = Convert.ToInt16(command.ExecuteScalar());
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                proje.PROJE_ADI = dataTable.Rows[i]["PROJE_ADI"].ToString();
                proje.PROJE_TANIMI = dataTable.Rows[i]["PROJE_TANIMI"].ToString();
                proje.PROJE_TURU = dataTable.Rows[i]["PROJE_TURU"].ToString();
            }
        

        return Json(proje, JsonRequestBehavior.AllowGet);

    }



    }
}