using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MicroAPI.Models;
using MicroAPI.BusinessModel;


namespace MicroAPI.Controllers
{
    public class UserAccountsController : ApiController
    {
        private MicroEntities db = new MicroEntities();

        // Method : 1
        // GET: api/DataEntryOperators
        public List<BusinessModel.UserAccountData> GetUserAccounts()
        {
            List<BusinessModel.UserAccountData> objList = new List<BusinessModel.UserAccountData>();
            foreach (var item in db.UserAccounts)
            {
                objList.Add(
                    new BusinessModel.UserAccountData
                    {
                        UserAccountID=item.UserAccountID,
                        RoleID=item.RoleID,
                        UserAccountName=item.UserAccountName,
                        ContactNumber=item.ContactNumber,
                        UserName = item.UserName,
                        Password = item.Password,
                        CreatedDate = item.CreatedDate,
                        CreatedBy = item.CreatedBy,
                        LastModifiedDate = item.LastModifiedDate,
                        LastModifiedBy = item.LastModifiedBy,
                        EMailAddress = item.EMailAddress,
                        ContactAddress = item.ContactAddress
                    }
                );
            }
            return objList;
        }

        // Method : 2
        [Route("api/DropUserAccount/{id}")]
        public bool GetRemoveUserAccountDetail(int id)
        {
            var userAccount = db.UserAccounts.Find(id);
            if (userAccount == null)
            {
                return false;
            }
            try
            {
                db.UserAccounts.Remove(userAccount);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        // Method : 3
        // GET: api/DataEntryOperators/5
        [ResponseType(typeof(BusinessModel.UserAccountData))]
        public BusinessModel.UserAccountData GetUserAccount(int id)
        {
            try
            {
                var userRoleList = db.UserRoles.ToList();
                var userRoleDataList = new List<UserRoleData>();
                foreach (var item in userRoleList)
                {
                    var userRole = new UserRoleData
                    {
                        RoleID = item.RoleID,
                        RoleName = item.RoleName,
                        CreatedBy = item.CreatedBy,
                        CreatedDate = item.CreatedDate,
                        IsActive = item.IsActive
                    };
                    userRoleDataList.Add(userRole);
                }
                var userAccounts = db.UserAccounts.Find(id);
                BusinessModel.UserAccountData obj = new BusinessModel.UserAccountData();
                if (userAccounts != null)
                {
                    obj = new BusinessModel.UserAccountData
                    {
                        UserAccountID = userAccounts.UserAccountID,
                        RoleID = userAccounts.RoleID,
                        RoleName = userAccounts.UserRole.RoleName,
                        UserRoleList = userRoleDataList,
                        UserAccountName = userAccounts.UserAccountName,
                        ContactNumber = userAccounts.ContactNumber,
                        UserName = userAccounts.UserName,
                        Password = userAccounts.Password,
                        CreatedDate = userAccounts.CreatedDate,
                        CreatedBy = userAccounts.CreatedBy,
                        LastModifiedDate = userAccounts.LastModifiedDate,
                        LastModifiedBy = userAccounts.LastModifiedBy,
                        EMailAddress = userAccounts.EMailAddress,
                        ContactAddress = userAccounts.ContactAddress
                    };
                }
                else
                {
                    obj = new BusinessModel.UserAccountData
                    {
                        UserAccountID = 0,
                        RoleID = 0,
                        RoleName = "",
                        UserRoleList = userRoleDataList,
                        UserAccountName = "",
                        ContactNumber = "",
                        UserName = "",
                        Password = "",
                        CreatedDate = "",
                        CreatedBy = "",
                        LastModifiedDate = "",
                        LastModifiedBy = "",
                        EMailAddress = "",
                        ContactAddress = ""
                    };
                }
                return obj;

            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        [ResponseType(typeof(BusinessModel.UserAccountData))]
        public BusinessModel.UserAccountData GetUserAccount(string userName,string passWord)
        {
            try
            {
                var userRoleList = db.UserRoles.ToList();
                var userRoleDataList = new List<UserRoleData>();
                foreach (var item in userRoleList)
                {
                    var userRole = new UserRoleData
                    {
                        RoleID = item.RoleID,
                        RoleName = item.RoleName,
                        CreatedBy = item.CreatedBy,
                        CreatedDate = item.CreatedDate,
                        IsActive = item.IsActive
                    };
                    userRoleDataList.Add(userRole);
                }
                var userAccounts = db.UserAccounts.Where(m => m.UserName == userName && m.Password == passWord).SingleOrDefault();
                BusinessModel.UserAccountData obj = new BusinessModel.UserAccountData();
                if (userAccounts != null)
                {
                    obj = new BusinessModel.UserAccountData
                    {
                        UserAccountID = userAccounts.UserAccountID,
                        RoleID = userAccounts.RoleID,
                        RoleName=userAccounts.UserRole.RoleName,
                        UserRoleList= userRoleDataList,
                        UserAccountName = userAccounts.UserAccountName,
                        ContactNumber = userAccounts.ContactNumber,
                        UserName = userAccounts.UserName,
                        Password = userAccounts.Password,
                        CreatedDate = userAccounts.CreatedDate,
                        CreatedBy = userAccounts.CreatedBy,
                        LastModifiedDate = userAccounts.LastModifiedDate,
                        LastModifiedBy = userAccounts.LastModifiedBy,
                        EMailAddress = userAccounts.EMailAddress,
                        ContactAddress = userAccounts.ContactAddress
                    };
                }
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        // Method : 4
        // POST: api/UserAccounts
        [ResponseType(typeof(BusinessModel.UserAccountData))]
        public IHttpActionResult PostUserAccount(BusinessModel.UserAccountData userAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (userAccount.UserAccountID > 0)
            {
                Models.UserAccount obj= db.UserAccounts.Find(userAccount.UserAccountID);
                obj.UserAccountID = userAccount.UserAccountID;
                obj.RoleID = userAccount.RoleID;
                obj.UserAccountName = userAccount.UserAccountName;
                obj.ContactNumber = userAccount.ContactNumber;
                obj.UserName = userAccount.UserName;
                obj.Password = userAccount.Password;
                obj.CreatedDate = userAccount.CreatedDate;
                obj.CreatedBy = userAccount.CreatedBy;
                obj.LastModifiedDate = userAccount.LastModifiedDate;
                obj.LastModifiedBy = userAccount.LastModifiedBy;
                obj.EMailAddress = userAccount.EMailAddress;
                obj.ContactAddress = userAccount.ContactAddress;
             
                db.SaveChanges();

            }
            else
            {
                Models.UserAccount obj = new Models.UserAccount
                {
                    UserAccountID = userAccount.UserAccountID,
                    RoleID = userAccount.RoleID,
                    UserAccountName = userAccount.UserAccountName,
                    ContactNumber = userAccount.ContactNumber,
                    UserName = userAccount.UserName,
                    Password = userAccount.Password,
                    CreatedDate = userAccount.CreatedDate,
                    CreatedBy = userAccount.CreatedBy,
                    LastModifiedDate = userAccount.LastModifiedDate,
                    LastModifiedBy = userAccount.LastModifiedBy,
                    EMailAddress = userAccount.EMailAddress,
                    ContactAddress = userAccount.ContactAddress
                };
                db.UserAccounts.Add(obj);
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = userAccount.UserAccountID }, userAccount);
        }       

      
    }
}