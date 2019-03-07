using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroAPI.BusinessModel
{
    public class InterestPercentageData
    {
        public int InterestPercentageID { get; set; }
        public Nullable<int> UserAccountID { get; set; }
        public string InterestPercentage1 { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
    }
}