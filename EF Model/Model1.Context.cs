﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PayPal_Invoiceing.EF_Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PayPal_IntegrateEntities : DbContext
    {
        public PayPal_IntegrateEntities()
            : base("name=PayPal_IntegrateEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customer_Invoice> Customer_Invoices { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
    }
}
