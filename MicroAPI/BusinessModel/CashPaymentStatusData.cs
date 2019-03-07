using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroAPI.BusinessModel
{
    public class CashPaymentStatusData
    {
        public int CashPaymentStatusID { get; set; }
        public string CashPaymentStatusName { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
    }
}