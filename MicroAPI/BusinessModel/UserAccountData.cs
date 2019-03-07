using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroAPI.BusinessModel
{
    public class UserAccountData
    {
        public int UserAccountID { get; set; }
        public Nullable<int> RoleID { get; set; }
        public string UserAccountName { get; set; }
        public string ContactNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string EMailAddress { get; set; }
        public string ContactAddress { get; set; }
    }
}