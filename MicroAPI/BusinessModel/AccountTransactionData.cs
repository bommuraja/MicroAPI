using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroAPI.BusinessModel
{
    public class AccountTransactionData
    {
        public int AccountTransactionID { get; set; }
        public Nullable<int> UserAccountID { get; set; }
        public Nullable<int> TransactionReferenceID { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionAmount { get; set; }
        public string TransactionDescription { get; set; }
        public Nullable<bool> IsCredit { get; set; }
        public string BalanceAmount { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
    }
}