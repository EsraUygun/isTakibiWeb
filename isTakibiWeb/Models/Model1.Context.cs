﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class isTakipEntities1 : DbContext
    {
        public isTakipEntities1()
            : base("name=isTakipEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TBLDOKUMAN> TBLDOKUMAN { get; set; }
        public virtual DbSet<TBLGOREV> TBLGOREV { get; set; }
        public virtual DbSet<TBLKULLANICI> TBLKULLANICI { get; set; }
        public virtual DbSet<TBLPERSONEL> TBLPERSONEL { get; set; }
        public virtual DbSet<TBLPROJE> TBLPROJE { get; set; }
        public virtual DbSet<TBLPROJEPERSONEL> TBLPROJEPERSONEL { get; set; }
        public virtual DbSet<TBLYETKI> TBLYETKI { get; set; }
    }
}
