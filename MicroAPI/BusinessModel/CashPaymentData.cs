using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroAPI.BusinessModel
{
    public class CashPaymentData
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