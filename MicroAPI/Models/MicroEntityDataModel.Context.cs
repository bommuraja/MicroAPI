﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MicroAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MicroEntities : DbContext
    {
        public MicroEntities()
            : base("name=MicroEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AccountTransaction> AccountTransactions { get; set; }
        public virtual DbSet<CashPayment> CashPayments { get; set; }
        public virtual DbSet<CashPaymentStatu> CashPaymentStatus { get; set; }
        public virtual DbSet<CashRequestStatu> CashRequestStatus { get; set; }
        public virtual DbSet<FinancialInstitution> FinancialInstitutions { get; set; }
        public virtual DbSet<InterestPercentage> InterestPercentages { get; set; }
        public virtual DbSet<CashRequest> CashRequests { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
    }
}
