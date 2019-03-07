using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroAPI.BusinessModel
{
    public class FinancialInstitution
    {
        public int FinancialInstitutionID { get; set; }
        public Nullable<int> UserAccountID { get; set; }
        public string AccountName { get; set; }
        public string NickName { get; set; }
        public string AccountNumber { get; set; }
        public string InstitutionName { get; set; }
        public string InstitutionIFSCCode { get; set; }
        public string InstitutionLocation { get; set; }
        public string InstitutionCity { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
    }
}