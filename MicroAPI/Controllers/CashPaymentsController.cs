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
    public class CashPaymentsController : ApiController
    {
        private MicroEntities db = new MicroEntities();

        // Method : 1
        // GET: api/DataEntryOperators
        public List<BusinessModel.CashPaymentData> GetCashPayments()
        {
            List<BusinessModel.CashPaymentData> objList = new List<BusinessModel.CashPaymentData>();
            foreach (var item in db.CashPayments)
            {
                objList.Add(
                    new BusinessModel.CashPaymentData
                    {                      
                       CashPaymentID = item.CashPaymentID,
                       UserAccountID = item.UserAccountID,
                       PaymentDate = item.PaymentDate,
                       PaymentAmount = item.PaymentAmount,
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
        [Route("api/DropCashPayment/{id}")]
        public bool GetRemoveCashPaymentDetail(int id)
        {
            var cashPayment = db.CashPayments.Find(id);
            if (cashPayment == null)
            {
                return false;
            }
            try
            {
                db.CashPayments.Remove(cashPayment);
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
        [ResponseType(typeof(BusinessModel.CashPaymentData))]
        public BusinessModel.CashPaymentData GetCashPayment(int id)
        {
            var cashPayment = db.CashPayments.Find(id);
            BusinessModel.CashPaymentData obj = new BusinessModel.CashPaymentData();
            if (cashPayment != null)
            {
                obj = new BusinessModel.CashPaymentData
                {
                    CashPaymentID = cashPayment.CashPaymentID,
                    UserAccountID = cashPayment.UserAccountID,
                    PaymentDate = cashPayment.PaymentDate,
                    PaymentAmount = cashPayment.PaymentAmount,
                    CreatedDate = cashPayment.CreatedDate,
                    CreatedBy = cashPayment.CreatedBy,
                    LastModifiedDate = cashPayment.LastModifiedDate,
                    LastModifiedBy = cashPayment.LastModifiedBy
                };
            }
            return obj;
        }

        // Method : 4
        // POST: api/UserAccounts
        [ResponseType(typeof(BusinessModel.CashPaymentData))]
        public IHttpActionResult PostCashPayment(BusinessModel.CashPaymentData cashPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (cashPayment.CashPaymentID > 0)
            {
                Models.CashPayment obj = db.CashPayments.Find(cashPayment.CashPaymentID);
                
                obj.CashPaymentID = cashPayment.CashPaymentID;
                obj.UserAccountID = cashPayment.UserAccountID;
                obj.PaymentDate = cashPayment.PaymentDate;
                obj.PaymentAmount = cashPayment.PaymentAmount;
                obj.CreatedDate = cashPayment.CreatedDate;
                obj.CreatedBy = cashPayment.CreatedBy;
                obj.LastModifiedDate = cashPayment.LastModifiedDate;
                obj.LastModifiedBy = cashPayment.LastModifiedBy;

                db.SaveChanges();

            }
            else
            {
                Models.CashPayment obj = new Models.CashPayment
                {
                    CashPaymentID = cashPayment.CashPaymentID,
                    UserAccountID = cashPayment.UserAccountID,
                    PaymentDate = cashPayment.PaymentDate,
                    PaymentAmount = cashPayment.PaymentAmount,
                    CreatedDate = cashPayment.CreatedDate,
                    CreatedBy = cashPayment.CreatedBy,
                    LastModifiedDate = cashPayment.LastModifiedDate,
                    LastModifiedBy = cashPayment.LastModifiedBy
                };
                db.CashPayments.Add(obj);
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = cashPayment.CashPaymentID }, cashPayment);
        }
    }
}