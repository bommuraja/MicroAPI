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

namespace MicroAPI.Controllers
{
    public class AccountTransactionsController : ApiController
    {
        private MicroEntities db = new MicroEntities();

        // Method : 1
        // GET: api/DataEntryOperators
        public List<BusinessModel.AccountTransaction> GetAccountTransactions()
        {
            List<BusinessModel.AccountTransaction> objList = new List<BusinessModel.AccountTransaction>();
            foreach (var item in db.AccountTransactions)
            {
                objList.Add(
                    new BusinessModel.AccountTransaction
                    {
                        AccountTransactionID = item.AccountTransactionID,
                        UserAccountID = item.UserAccountID,
                        TransactionReferenceID = item.TransactionReferenceID,
                        TransactionDate = item.TransactionDate,
                        TransactionAmount = item.TransactionAmount,
                        TransactionDescription = item.TransactionDescription,
                        IsCredit = item.IsCredit,
                        BalanceAmount = item.BalanceAmount,
                        CreatedDate = item.CreatedDate,
                        CreatedBy = item.CreatedBy,
                        LastModifiedDate = item.LastModifiedDate,
                        LastModifiedBy = item.LastModifiedBy
                    }
                );
            }
            return objList;
        }

        // Method : 2
        [Route("api/DropAccountTransaction/{id}")]
        public bool GetRemoveAccountTransactionDetail(int id)
        {
            var accountTransaction = db.AccountTransactions.Find(id);
            if (accountTransaction == null)
            {
                return false;
            }
            try
            {
                db.AccountTransactions.Remove(accountTransaction);
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
        [ResponseType(typeof(BusinessModel.AccountTransaction))]
        public BusinessModel.AccountTransaction GetAccountTransaction(int id)
        {
            var accountTransaction = db.AccountTransactions.Find(id);
            BusinessModel.AccountTransaction obj = new BusinessModel.AccountTransaction();
            if (accountTransaction != null)
            {
                obj = new BusinessModel.AccountTransaction
                {
                    AccountTransactionID = accountTransaction.AccountTransactionID,
                    UserAccountID = accountTransaction.UserAccountID,
                    TransactionReferenceID = accountTransaction.TransactionReferenceID,
                    TransactionDate = accountTransaction.TransactionDate,
                    TransactionAmount = accountTransaction.TransactionAmount,
                    TransactionDescription = accountTransaction.TransactionDescription,
                    IsCredit = accountTransaction.IsCredit,
                    BalanceAmount = accountTransaction.BalanceAmount,
                    CreatedDate = accountTransaction.CreatedDate,
                    CreatedBy = accountTransaction.CreatedBy,
                    LastModifiedDate = accountTransaction.LastModifiedDate,
                    LastModifiedBy = accountTransaction.LastModifiedBy
                };
            }
            return obj;
        }

        // Method : 4
        // POST: api/UserAccounts
        [ResponseType(typeof(BusinessModel.AccountTransaction))]
        public IHttpActionResult PostAccountTransaction(BusinessModel.AccountTransaction accountTransaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (accountTransaction.AccountTransactionID > 0)
            {
                Models.AccountTransaction obj = db.AccountTransactions.Find(accountTransaction.AccountTransactionID);
                obj.AccountTransactionID = accountTransaction.AccountTransactionID;
                obj.UserAccountID = accountTransaction.UserAccountID;
                obj.TransactionReferenceID = accountTransaction.TransactionReferenceID;
                obj.TransactionDate = accountTransaction.TransactionDate;
                obj.TransactionAmount = accountTransaction.TransactionAmount;
                obj.TransactionDescription = accountTransaction.TransactionDescription;
                obj.IsCredit = accountTransaction.IsCredit;
                obj.BalanceAmount = accountTransaction.BalanceAmount;
                obj.CreatedDate = accountTransaction.CreatedDate;
                obj.CreatedBy = accountTransaction.CreatedBy;
                obj.LastModifiedDate = accountTransaction.LastModifiedDate;
                obj.LastModifiedBy = accountTransaction.LastModifiedBy;

                db.SaveChanges();

            }
            else
            {
                Models.AccountTransaction obj = new Models.AccountTransaction
                {
                    AccountTransactionID = accountTransaction.AccountTransactionID,
                    UserAccountID = accountTransaction.UserAccountID,
                    TransactionReferenceID = accountTransaction.TransactionReferenceID,
                    TransactionDate = accountTransaction.TransactionDate,
                    TransactionAmount = accountTransaction.TransactionAmount,
                    TransactionDescription = accountTransaction.TransactionDescription,
                    IsCredit = accountTransaction.IsCredit,
                    BalanceAmount = accountTransaction.BalanceAmount,
                    CreatedDate = accountTransaction.CreatedDate,
                    CreatedBy = accountTransaction.CreatedBy,
                    LastModifiedDate = accountTransaction.LastModifiedDate,
                    LastModifiedBy = accountTransaction.LastModifiedBy
                };
                db.AccountTransactions.Add(obj);
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = accountTransaction.AccountTransactionID }, accountTransaction);
        }
    }
}