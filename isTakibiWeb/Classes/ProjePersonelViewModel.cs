using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace isTakibiWeb.Classes
{
    public class ProjePersonelViewModel
    {
        public IEnumerable<SelectListItem> TBLPROJEPERSONEL { get; set; }
        public IEnumerable<SelectListItem> TBLPERSONEL { get; set; }
    }
}