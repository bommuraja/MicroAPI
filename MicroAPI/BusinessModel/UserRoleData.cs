using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroAPI.BusinessModel
{
    public class UserRoleData
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}