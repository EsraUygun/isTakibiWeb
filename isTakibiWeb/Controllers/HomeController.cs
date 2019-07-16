using isTakibiWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace isTakibiWeb.Controllers
{
    public class HomeController : Controller
    {
        isTakipEntities1 entities = new isTakipEntities1();
        Classes.CommonClass common = new Classes.CommonClass();
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult projeEkle()
        {

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
            model.REC_VERSION = common.REC_VERSION;
            entities.TBLPROJE.Add(model);
            entities.SaveChanges();
            return RedirectToAction("ProjePersonel" );
            
        }

        public ActionResult ProjePersonel()
        {
           

            List<SelectListItem> personeller = new List<SelectListItem>();
            foreach (var item in entities.TBLPERSONEL.ToList())
            {
                personeller.Add(new SelectListItem { Text = item.PERSONEL_ADI, Value = item.PERSONEL_KOD });

            }

            ViewBag.PERSONEL_KOD = personeller;

            List<SelectListItem> projekod = new List<SelectListItem>();
            foreach (var item in entities.TBLPROJE.ToList())
            {
                projekod.Add(new SelectListItem { Text = item.PROJE_ADI, Value = item.PROJE_KOD });

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
            model.REC_VERSION = common.REC_VERSION;
            entities.TBLPROJEPERSONEL.Add(model);
            

            entities.SaveChanges();

            return RedirectToAction("ProjePersonel");
        }
        public ActionResult personelEkle()
        {

            return View();
        }
        [HttpPost]
        public ActionResult personelEkle(TBLPERSONEL model)
        {
            model.REC_DATE = DateTime.Now;
           
            model.REC_UPDATE = DateTime.Now;
            model.REC_UPUSERNAME = "esra33";
            model.REC_UPUSERNO = 33;
            model.REC_USERNAME = "esra33";
            model.REC_USERNO = 33;
            model.REC_CHANGED = "0";
            model.REC_VERSION = common.REC_VERSION;
            entities.TBLPERSONEL.Add(model);

            entities.SaveChanges();

            return RedirectToAction("kullanıcıEkle", new { personel_kod = model.PERSONEL_KOD });
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
            model.REC_UPUSERNAME = "esra33";
            model.REC_UPUSERNO = 33;
            model.REC_USERNAME = "esra33";
            model.REC_USERNO = 33;
            model.REC_CHANGED = "0";
            model.REC_VERSION = common.REC_VERSION;
            //model.PERSONEL_KOD = personel_kod;
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
    }
}