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
    public class HomeController : Controller
    {
        isTakipEntities entities = new isTakipEntities();
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult projeEkle()
        {
            List<SelectListItem> PROJETURU = new List<SelectListItem>();

            string[] vars = { "Özel", "Masaüstü", "Web", "Mobil" };
            foreach (var item in vars)
            {
                PROJETURU.Add(new SelectListItem { Text = item, Value = item });
            }

            ViewBag.PROJE_TURU = PROJETURU;
            return View();
        }

        [HttpPost]
        public ActionResult projeEkle(TBLPROJE model)
        {

            model.REC_DATE = DateTime.Now;
         
            model.REC_UPDATE = DateTime.Now;
            model.REC_UPUSERNAME =(string) Session["UserName"];
            model.REC_UPUSERNO = (int) Session["UserId"];
            model.REC_USERNAME = (string)Session["UserName"];
            model.REC_USERNO = (int)Session["UserId"];
            model.REC_CHANGED = "0";
            model.REC_VERSION = CreateCommon.REC_VERSION;
            entities.TBLPROJE.Add(model);
            entities.SaveChanges();
            return RedirectToAction("ProjePersonel" );
            
        }

        public ActionResult ProjePersonel()
        {

            //ProjePersonelViewModel ProjePersonelVM = new ProjePersonelViewModel();
            //ProjePersonelVM.TBLPERSONEL = new SelectList(entities.TBLPERSONEL, "PERSONEL_KOD", "PERSONEL_ADI");
            //ProjePersonelVM.TBLPROJEPERSONEL = new SelectList(entities.TBLPROJEPERSONEL, "PROJE_KOD", "PROJE_ADI");



            List<SelectListItem> personeller = new List<SelectListItem>();
            foreach (var item in entities.TBLPERSONEL.ToList())
            {
                personeller.Add(new SelectListItem { Text = item.PERSONEL_KOD + "/" + item.PERSONEL_ADI + " " + item.PERSONEL_SOYADI, Value = item.PERSONEL_KOD });
            }
            ViewBag.PERSONEL_KOD = personeller;


            List<SelectListItem> projekod = new List<SelectListItem>();
            foreach (var item in entities.TBLPROJE.ToList())
            {
                projekod.Add(new SelectListItem { Text = item.PROJE_ADI + " / " + item.PROJE_KOD, Value = item.PROJE_KOD });

            }

            ViewBag.PROJE_KOD = projekod;

            return View();

        }

        [HttpPost]
        public ActionResult ProjePersonel(TBLPROJEPERSONEL model)
        {
           
            model.REC_DATE = DateTime.Now;
            model.REC_UPDATE = DateTime.Now;
            model.REC_UPUSERNAME = "esra33";
            model.REC_UPUSERNO = 33;
            model.REC_USERNAME = "esra33";
            model.REC_USERNO = 33;
            model.REC_CHANGED = "0";
            model.REC_VERSION = CreateCommon.REC_VERSION;
            entities.TBLPROJEPERSONEL.Add(model);
            entities.SaveChanges();
            return RedirectToAction("ProjePersonel");
        }


        public JsonResult ProjePersonelTest()
        {
            var routeValue = RouteData.Values["id"];
           

            SqlConnection conn = new SqlConnection("Data Source=ESRA\\SQLEXPRESS; Initial Catalog=isTakip;integrated security=True;MultipleActiveResultSets=True;");
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT  PROJEPERSONEL.PROJE_KOD ,PROJEPERSONEL.PERSONEL_KOD, PERSONEL.PERSONEL_ADI, PERSONEL.PERSONEL_SOYADI FROM TBLPERSONEL PERSONEL, TBLPROJEPERSONEL PROJEPERSONEL WHERE PROJEPERSONEL.PERSONEL_KOD=PERSONEL.PERSONEL_KOD AND PROJEPERSONEL.PROJE_KOD='"+routeValue+"'", conn);

            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dataTable);
            sqlDataAdapter.Dispose();

            List<SelectListItem> personeller = new List<SelectListItem>();
            if (command != null)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    personeller.Add(new SelectListItem { Text = dataTable.Rows[i]["PERSONEL_KOD"].ToString() + "/" + dataTable.Rows[i]["PERSONEL_ADI"].ToString() + " " + dataTable.Rows[i]["PERSONEL_SOYADI"].ToString(), Value = dataTable.Rows[i]["PERSONEL_KOD"].ToString() });

                }

            }


            return Json(new SelectList(personeller, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public ActionResult  projeGörüntüle()
        {

            return View(entities.TBLPROJE.ToList());
        }

        public ActionResult ProjeDüzenle(string projekod)
        {
            return View();
        }
        public ActionResult personelEkle()
        {
            return View();
            
        }
        public ActionResult personelCheck(string personelkod)
        {
            TBLPERSONEL personeller = new TBLPERSONEL();
            personeller = entities.TBLPERSONEL.Find(personelkod);

            return View(personeller);
        }

        [HttpPost]
        public JsonResult GetJsonTest(string personelkod)
        {
            TBLPERSONEL personeller = new TBLPERSONEL();
            personeller = entities.TBLPERSONEL.Find(personelkod);
            
            return Json(personeller, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPipeMaterialValues(string id)
        {

            TBLPERSONEL personeller = new TBLPERSONEL();
            personeller = entities.TBLPERSONEL.Find(id);
           
            return Json(personeller, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult personelEkle(TBLPERSONEL model)
        {
            model.REC_DATE = DateTime.Now;
            model.REC_UPDATE = DateTime.Now;
            model.REC_UPUSERNAME = (String)Session["UserName"];
            model.REC_UPUSERNO = (int)Session["UserId"];
            model.REC_USERNAME = (String)Session["UserName"];
            model.REC_USERNO = (int)Session["UserId"];
            model.REC_CHANGED = "0";
            model.REC_VERSION = CreateCommon.REC_VERSION;
            entities.TBLPERSONEL.Add(model);

            entities.SaveChanges();


            TBLKULLANICI kullaniciModel = new TBLKULLANICI();
            
            kullaniciModel.REC_DATE = DateTime.Now;
            kullaniciModel.REC_UPDATE = DateTime.Now;
            kullaniciModel.REC_UPUSERNAME = (String)Session["UserName"];
            kullaniciModel.REC_UPUSERNO = (int)Session["UserId"];
            kullaniciModel.REC_USERNAME = (String)Session["UserName"];
            kullaniciModel.REC_USERNO = (int)Session["UserId"];
            kullaniciModel.REC_CHANGED = "0";
            kullaniciModel.REC_VERSION = CreateCommon.REC_VERSION;
            kullaniciModel.PERSONEL_KOD = model.PERSONEL_KOD;
            kullaniciModel.SIFRE = CreateCommon.ilkSifre;
            kullaniciModel.KULLANICI_ADI = model.PERSONEL_KOD;
            entities.TBLKULLANICI.Add(kullaniciModel);
            entities.SaveChanges();



            ViewBag.Messsage = "kayıdınız başarılı bir şekilde oluşturuldu";
            return RedirectToAction("Index");

            //return RedirectToAction("kullanıcıEkle", new { personel_kod = model.PERSONEL_KOD });
        }

    

        public ActionResult GorevAta()
        {
            List<SelectListItem> projekod = new List<SelectListItem>();
            foreach (var item in entities.TBLPROJE.ToList())
            {
                projekod.Add(new SelectListItem { Text = item.PROJE_KOD + " / "+item.PROJE_ADI  , Value = item.PROJE_KOD });

            }
            
            ViewBag.PROJE_KOD = projekod;

            //List<SelectListItem> personeller = new List<SelectListItem>();
            //foreach (var item in entities.TBLPERSONEL.ToList())
            //{

            //    personeller.Add(new SelectListItem { Text = item.PERSONEL_ADI + " / " + item.PERSONEL_KOD, Value = item.PERSONEL_KOD });

            //}

            //ViewBag.PERSONEL_KOD = personeller;

            return View();
        }

        [HttpPost]
        ActionResult GorevAta(TBLGOREV model)
        {
            model.REC_DATE = DateTime.Now;
            model.REC_UPDATE = DateTime.Now;
            model.REC_UPUSERNAME = (String)Session["UserName"];
            model.REC_UPUSERNO = (int)Session["UserId"];
            model.REC_USERNAME = (String)Session["UserName"];
            model.REC_USERNO = (int)Session["UserId"];
            model.REC_CHANGED = "0";
            model.REC_VERSION = CreateCommon.REC_VERSION;
            model.DURUM = '1';
            entities.TBLGOREV.Add(model);
            entities.SaveChanges();
            return View();
        }
        public ActionResult personelDetails(string id)
        {
            TBLPERSONEL personeller = new TBLPERSONEL();
            personeller = entities.TBLPERSONEL.Find(id);

            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(personeller);
            }
           
        }

        public ActionResult Sil(string id)
        {
            TBLPERSONEL personeller = new TBLPERSONEL();
            personeller = entities.TBLPERSONEL.Find(id);
            entities.TBLPERSONEL.Remove(personeller);
            entities.SaveChanges();
            return RedirectToAction("PersonelListesi");
        }



        public ActionResult personelDüzenle(string id)
        {
            TBLPERSONEL personeller = new TBLPERSONEL();
            personeller = entities.TBLPERSONEL.Find(id);

            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(personeller);
            }

        }
        [HttpPost]
        public ActionResult personelDüzenle(TBLPERSONEL model)
        {
            if (ModelState.IsValid)
            {
                model.REC_UPDATE = DateTime.Now;
                model.REC_UPUSERNAME = (String)Session["UserName"];
                model.REC_UPUSERNO = (int)Session["UserId"];
                model.REC_CHANGED = "1";
                model.REC_VERSION = CreateCommon.REC_VERSION;
                entities.Entry(model).State = EntityState.Modified;
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
        

            return RedirectToAction("personelListele");
        }
            public ActionResult kullanıcıEkle(string personel_kod)
        {
            ViewBag.personel_kod = personel_kod;
            return View();

        }

        [HttpPost]
        public ActionResult kullanıcıEkle(TBLKULLANICI model)
        {
            model.REC_DATE = DateTime.Now;
        
            model.REC_UPDATE = DateTime.Now;
            model.REC_UPUSERNAME = "esra33"; /*(string)Session["UserName"];*/
            model.REC_UPUSERNO = 33; /*(int)Session["UserId"];*/
            model.REC_USERNAME = "esra33";/*(string)Session["UserName"]; ;*/
            model.REC_USERNO = 33;/*(int)Session["UserId"];*/
            model.REC_CHANGED = "0";
            model.REC_VERSION = CreateCommon.REC_VERSION;
            entities.TBLKULLANICI.Add(model);
            entities.SaveChanges();

            ViewBag.Messsage = "kayıdınız başarılı bir şekilde oluşturuldu";
            return RedirectToAction("Index");

        }
        public ActionResult kayıtControl()
        {

            return View(from isTakipEntities in entities.TBLPERSONEL.Take(20) select isTakipEntities);
        }

        [HttpGet]
        public ActionResult PersonelListele()
        {
            return View(entities.TBLPERSONEL.ToList());
        }

        [HttpPost]
        public ActionResult PersonelListele(string aranacakKelime)
        {
            var personeller = from f in entities.TBLPERSONEL
                                             select f;
           if(String.IsNullOrEmpty(aranacakKelime))
            {
                return RedirectToAction("Index");
            }
            if (!String.IsNullOrEmpty(aranacakKelime))
            {
                personeller = personeller.Where(f => f.PERSONEL_ADI.Contains(aranacakKelime) || f.PERSONEL_SOYADI.Contains(aranacakKelime));
                
                
                
            }
            return View(personeller.ToList());
        }
        public ActionResult KullanıcıListele()
        {
            return View(from isTakipEntities in entities.TBLKULLANICI.Take(20) select isTakipEntities);
        }
        public ActionResult perlist()
        {
            return View(entities.TBLPERSONEL.ToList());
        }

        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username != "" & password != "")
            {
                var obj = entities.TBLKULLANICI.Where(a => a.KULLANICI_ADI.Equals(username) && a.SIFRE.Equals(password)).FirstOrDefault();
                Session["UserId"] = obj.REC_ID;
                Session["UserName"] = obj.KULLANICI_ADI;
                CreateCommon.UserName = obj.KULLANICI_ADI;
                if (obj.SIFRE.Equals(CreateCommon.ilkSifre))
                {
                    return RedirectToAction("KullanıcıGüncelle");
                }
                var yetki = entities.TBLYETKI.Where(y => y.KULLANICI_KOD.Equals(obj.REC_ID)).FirstOrDefault();

                if (yetki.TAM_YETKI.Equals("1"))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Personel");
                }
            }
            else
            {
                TempData["Message"] = "kullanıcı adını veya şifreyi boş bırakmayın !!!!";
                return RedirectToAction("Login");
            }
           

        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }


        public ActionResult KullanıcıGüncelle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KullanıcıGüncelle(string username,string password,string eposta)
        {
            int userid = (int)Session["UserId"];
            TBLKULLANICI obj = new TBLKULLANICI();
            obj = entities.TBLKULLANICI.Where(a => a.REC_ID==userid).FirstOrDefault();
            obj.KULLANICI_ADI = username;
            obj.SIFRE = password;
            obj.REC_UPUSERNO = (int)Session["UserId"];
            obj.REC_UPUSERNAME = username;
            obj.REC_UPDATE = DateTime.Now;
            obj.E_POSTA = eposta;
            obj.REC_CHANGED = "1";
            obj.REC_VERSION = CreateCommon.REC_VERSION;
            entities.Entry(obj).State = EntityState.Modified;
            entities.SaveChanges();


            return RedirectToAction("Index", "Personel");
        }
    }
}