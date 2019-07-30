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

namespace isTakibiWeb.Classes
{
   
    public class db
    {
        SqlConnection con = new SqlConnection("Data Source=ESRA\\SQLEXPRESS; Initial Catalog=isTakip;integrated security=True;MultipleActiveResultSets=True;");
        

        public DataSet Get_PROJE()
        {
            SqlCommand com = new SqlCommand("Select PROJE.PROJE_KOD,PROJE.PROJE_ADI  from TBLPROJE PROJE", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        //Get all State
        public DataSet Get_PERSONEL(string PROJE_KOD)
        {
            SqlCommand com = new SqlCommand("Select PROJEPERSONEL.PERSONEL_KOD,PERSONEL.PERSONEL_ADI from TBLPROJEPERSONEL PROJEPERSONEL,TBLPERSONEL PERSONEL  where PROJEPERSONEL.PROJE_KOD=@PROJE_KOD AND PROJEPERSONEL.PERSONEL_KOD=PERSONEL.PERSONEL_KOD", con);
            com.Parameters.AddWithValue("@PROJE_KOD", PROJE_KOD);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}