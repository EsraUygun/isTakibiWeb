using isTakibiWeb.Classes;
using isTakibiWeb.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
            List<SelectListItem> projeturu = new List<SelectListItem>();
            
            projeturu.Add(new SelectListItem { Text = "ÖZEL", Value = "özel" });
            projeturu.Add(new SelectListItem { Text = "MASAÜSTÜ", Value = "masaüstü" });
            projeturu.Add(new SelectListItem { Text = "WEB", Value = "web" });
            projeturu.Add(new SelectListItem { Text = "MOBİL", Value = "mobil" });

            ViewBag.PROJE_TURU = projeturu;

            List<SelectListItem> projedurumu = new List<SelectListItem>();

            projedurumu.Add(new SelectListItem { Text = "Aktif yap", Value = "2" });
            projedurumu.Add(new SelectListItem { Text = "Askıya al", Value = "1" });
            projedurumu.Add(new SelectListItem { Text = "İptal et", Value = "0" });
            projedurumu.Add(new SelectListItem { Text = "Tamamla", Value = "3" });

            ViewBag.PROJE_TURU = projeturu;
            ViewBag.DURUM = projedurumu;
            return View(entities.TBLPROJE.Find(id));
        }

        [HttpPost]
        public ActionResult ProjeDüzenle(TBLPROJE model)
        {
            model.REC_UPDATE = DateTime.Now;
            model.REC_UPUSERNAME = (string)Session["UserName"];
            model.REC_UPUSERNO = (int)Session["UserId"];
            model.REC_VERSION = CreateCommon.REC_VERSION;
            model.REC_CHANGED = "1";

            entities.Entry(model).State = EntityState.Modified;
            entities.SaveChanges();

            return RedirectToAction("projeGörüntüle");
        }

        public ActionResult CharterColumn( string id)
        {

            SqlConnection conn = new SqlConnection("Data Source=ESRA\\SQLEXPRESS; Initial Catalog=isTakip;integrated security=True;MultipleActiveResultSets=True;");
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT  PERSONEL_KOD,BITIRME_SURESI,TAHMINI_BASLANGIC FROM TBLGOREV WHERE PROJE_KOD='"+id+"'and BITIRME_SURESI is not null", conn);

            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            sqlDataAdapter.Dispose();


            var gorevList = new List<Class1>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {

                Class1 class1 = new Class1();
                class1.personel_kod = dataTable.Rows[i]["PERSONEL_KOD"].ToString();
                class1.tarih = (Convert.ToDateTime(dataTable.Rows[i]["TAHMINI_BASLANGIC"])).AddDays(Convert.ToInt32(dataTable.Rows[i]["BITIRME_SURESI"]));
                gorevList.Add(class1);
            }
            TBLPROJE tBLPROJE = new TBLPROJE();
            tBLPROJE = entities.TBLPROJE.Find(id);
            String proje_ad = tBLPROJE.PROJE_ADI;

            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            var result = from c in gorevList select c;
            result.ToList().ForEach(rs => xValue.Add(rs.personel_kod));
            result.ToList().ForEach(rs => yValue.Add(rs.tarih));


            new System.Web.Helpers.Chart(width: 600, height: 300, theme: ChartTheme.Green)
              .AddTitle(proje_ad)
              .AddSeries("Default", chartType: "Column", xValue: xValue, yValues: yValue)
             .Write("bmp");

            return null;
        }
        public ActionResult ProjeDetay(string id)
        {

            var model = from f in entities.TBLGOREV
                           select f;


            model = model.Where(f => f.PROJE_KOD.Contains(id));

            TBLPROJE tBLPROJE = new TBLPROJE();
            tBLPROJE = entities.TBLPROJE.Find(id);
            ViewBag.Proje_ad = tBLPROJE.PROJE_ADI;
            ViewBag.PERSONEL_KOD = id;
            return View(model.ToList());
            ////return View(Tuple.Create(tBLGOREV.ToList(),tBLPROJEPERSONEL.ToList()));
        }

        [HttpPost]
        public ActionResult GörevBelgeGörüntüle(string PERSONEL_KOD , string PROJE_KOD)
        {
     
            var document = (from k in entities.TBLDOKUMAN
                        from p in entities.TBLGOREV
                       where p.PROJE_KOD==PROJE_KOD & p.PERSONEL_KOD==PERSONEL_KOD & p.GOREV_KOD==k.GOREV_KOD
                       select new {
                           p.GOREV_TANIMI,
                           k.DOSYA_YOLU,
                           k.BELGE
                       }).ToList().FirstOrDefault();

            //byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(document.DOSYA_YOLU + document.BELGE + ""));
            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "x");

        
            return File(document.BELGE, System.Net.Mime.MediaTypeNames.Application.Octet, document.DOSYA_YOLU);
        }


    }
}