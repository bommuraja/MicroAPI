//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class CashPayment
    {
        public int CashPaymentID { get; set; }
        public Nullable<int> UserAccountID { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentAmount { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
