//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace isTakibiWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBLPERSONEL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBLPERSONEL()
        {
            this.TBLGOREV = new HashSet<TBLGOREV>();
            this.TBLKULLANICI = new HashSet<TBLKULLANICI>();
            this.TBLPROJEPERSONEL = new HashSet<TBLPROJEPERSONEL>();
        }
    
        public int REC_ID { get; set; }
        public System.DateTime REC_DATE { get; set; }
        public string REC_USERNAME { get; set; }
        public int REC_USERNO { get; set; }
        public System.DateTime REC_UPDATE { get; set; }
        public string REC_UPUSERNAME { get; set; }
        public int REC_UPUSERNO { get; set; }
        public string REC_CHANGED { get; set; }
        public string REC_VERSION { get; set; }
        public string PERSONEL_KOD { get; set; }
        public string PERSONEL_ADI { get; set; }
        public string PERSONEL_SOYADI { get; set; }
        public string TEL_NO { get; set; }
        public string ARSTR_1 { get; set; }
        public string ARSTR_2 { get; set; }
        public string ARSTR_3 { get; set; }
        public Nullable<decimal> ARFLOAT_1 { get; set; }
        public Nullable<decimal> ARFLOAT_2 { get; set; }
        public Nullable<decimal> ARFLOAT_3 { get; set; }
        public Nullable<System.DateTime> ARDATE_1 { get; set; }
        public Nullable<System.DateTime> ARDATE_2 { get; set; }
        public Nullable<System.DateTime> ARDATE_3 { get; set; }
        public string DURUM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBLGOREV> TBLGOREV { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBLKULLANICI> TBLKULLANICI { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBLPROJEPERSONEL> TBLPROJEPERSONEL { get; set; }
    }
}
