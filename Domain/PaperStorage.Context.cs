﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HelloStudentEntities : DbContext
    {
        public HelloStudentEntities()
            : base("name=HelloStudentEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Paper> Papers { get; set; }
        public DbSet<PaperType> PaperTypes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SiteContent> SiteContents { get; set; }
    }
}
