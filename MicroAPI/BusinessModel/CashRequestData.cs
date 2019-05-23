using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroAPI.BusinessModel
{
    public class CashRequestData
    {
        public int CashRequestID { get; set; }
        public Nullable<int> UserAccountID { get; set; }
        public string UserAccountName { get; set; }
        public Nullable<int> CashRequestStatusID { get; set; }
        public string CashRequestStatusName { get; set; }
        public Nullable<int> PaymentFromBankID { get; set; }
        public string PaymentFromBankName { get; set; }
        public List<FinancialInstitutionData> PaymentFromBankList { get; set; }
        public Nullable<int> PaymentToBankID { get; set; }
        public string PaymentToBankName { get; set; }
        public List<FinancialInstitutionData> PaymentToBankList { get; set; }
        public string RequestDate { get; set; }
        public string ResponseDate { get; set; }
        public string RequestAmount { get; set; }
        public string ResponseAmount { get; set; }
        public bool IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public List<CashRequestStatusData> CashRequestStatusList { get; set; }
        public string UserActionOnCashRequest { get; set; }
    }
}