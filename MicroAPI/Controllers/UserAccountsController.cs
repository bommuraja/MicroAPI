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
        public List<BusinessModel.UserAccount> GetUserAccounts()
        {
            List<BusinessModel.UserAccount> objList = new List<BusinessModel.UserAccount>();
            foreach (var item in db.UserAccounts)
            {
                objList.Add(
                    new BusinessModel.UserAccount
                    {
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
        [ResponseType(typeof(BusinessModel.UserAccount))]
        public BusinessModel.UserAccount GetUserAccount(int id)
        {
            var userAccounts = db.UserAccounts.Find(id);
            BusinessModel.UserAccount obj = new BusinessModel.UserAccount();
            if (userAccounts != null)
            {
                obj = new BusinessModel.UserAccount
                {
                    RoleID = userAccounts.RoleID,
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

        // Method : 4
        // POST: api/UserAccounts
        [ResponseType(typeof(BusinessModel.UserAccount))]
        public IHttpActionResult PostUserAccount(BusinessModel.UserAccount userAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (userAccount.UserAccountID > 0)
            {
                Models.UserAccount obj= db.UserAccounts.Find(userAccount.UserAccountID);
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