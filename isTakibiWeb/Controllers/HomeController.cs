using isTakibiWeb.Classes;
using isTakibiWeb.Models;
using System;
using System.Collections.Generic;
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

            ViewBag.PROJETURU = PROJETURU;
            return View();
        }

        [HttpPost]
        public ActionResult projeEkle(TBLPROJE model)
        {

           

            model.REC_DATE = DateTime.Now;
         
            model.REC_UPDATE = DateTime.Now;
            model.REC_UPUSERNAME = "esra33";
            model.REC_UPUSERNO = 33;
            model.REC_USERNAME = "esra33";
            model.REC_USERNO = 33;
            model.REC_CHANGED = "0";
            model.REC_VERSION = CreateCommon.REC_VERSION;
            entities.TBLPROJE.Add(model);
            entities.SaveChanges();
            return RedirectToAction("ProjePersonel" );
            
        }

        public ActionResult ProjePersonel()
        {
           

            List<SelectListItem> personeller = new List<SelectListItem>();
            foreach (var item in entities.TBLPERSONEL.ToList())
            {
                 
                personeller.Add(new SelectListItem { Text = item.PERSONEL_ADI+item.PERSONEL_KOD, Value = item.PERSONEL_KOD });

            }

            ViewBag.PERSONEL_KOD = personeller;

            List<SelectListItem> projekod = new List<SelectListItem>();
            foreach (var item in entities.TBLPROJE.ToList())
            {
                projekod.Add(new SelectListItem { Text = item.PROJE_ADI + item.PROJE_KOD, Value = item.PROJE_KOD });

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

        public ActionResult  projeGörüntüle()
        {
            return View();
        }
        public ActionResult personelEkle()
        {
            return View();
            
        }
        public ActionResult personelCheck(string personelkod)
        {
            var SearchList = from m in entities.TBLPERSONEL
                             select m;
            if (!String.IsNullOrEmpty(personelkod))
            {
                SearchList = SearchList.Where(s => s.PERSONEL_KOD.Contains(personelkod));
            }
            if (Request.IsAjaxRequest())
                return PartialView(SearchList);

            return View(SearchList);
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
        public ActionResult personelDetails(int? personelkod)
        {
            var personeller = from f in entities.TBLPERSONEL
                              select f;
            //if (String.IsNullOrEmpty(personelkod))
            //{
            //    return RedirectToAction("Index");
            //}
            //if (!String.IsNullOrEmpty(personelkod))
            //{
            //    personeller = personeller.Where(f => f.PERSONEL_KOD.Contains(personelkod) );
            //}
            
            return View(entities.TBLPERSONEL.Find(personelkod));
           
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
            var obj = entities.TBLKULLANICI.Where(a => a.KULLANICI_ADI.Equals(username) && a.SIFRE.Equals(password)).FirstOrDefault();
            Session["UserId"] = obj.REC_ID;
            Session["UserName"] = obj.KULLANICI_ADI;
            CreateCommon.UserName = obj.KULLANICI_ADI;
            return RedirectToAction("Index");

        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
    }
}