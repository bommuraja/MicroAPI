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
    public class CashRequestsController : ApiController
    {
        private MicroEntities db = new MicroEntities();

        // Method : 1
        // GET: api/DataEntryOperators
        public List<BusinessModel.CashRequest> GetCashRequests()
        {
            List<BusinessModel.CashRequest> objList = new List<BusinessModel.CashRequest>();
            foreach (var item in db.CashRequests)
            {
                objList.Add(
                    new BusinessModel.CashRequest
                    {
                      CashRequestID = item.CashRequestID,
                      UserAccountID = item.UserAccountID,
                      RequestDate = item.RequestDate,
                      ResponseDate = item.ResponseDate,
                      RequestAmount = item.RequestAmount,
                      ResponseAmount = item.ResponseAmount,
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
        [Route("api/DropCashRequest/{id}")]
        public bool GetRemoveCashRequestDetail(int id)
        {
            var cashRequest = db.CashRequests.Find(id);
            if (cashRequest == null)
            {
                return false;
            }
            try
            {
                db.CashRequests.Remove(cashRequest);
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
        [ResponseType(typeof(BusinessModel.CashRequest))]
        public BusinessModel.CashRequest GetCashRequest(int id)
        {
            var cashRequest = db.CashRequests.Find(id);
            BusinessModel.CashRequest obj = new BusinessModel.CashRequest();
            if (cashRequest != null)
            {
                obj = new BusinessModel.CashRequest
                {
                    CashRequestID = cashRequest.CashRequestID,
                    UserAccountID = cashRequest.UserAccountID,
                    RequestDate = cashRequest.RequestDate,
                    ResponseDate = cashRequest.ResponseDate,
                    RequestAmount = cashRequest.RequestAmount,
                    ResponseAmount = cashRequest.ResponseAmount,
                    CreatedDate = cashRequest.CreatedDate,
                    CreatedBy = cashRequest.CreatedBy,
                    LastModifiedDate = cashRequest.LastModifiedDate,
                    LastModifiedBy = cashRequest.LastModifiedBy
                };
            }
            return obj;
        }

        // Method : 4
        // POST: api/UserAccounts
        [ResponseType(typeof(BusinessModel.CashRequest))]
        public IHttpActionResult PostCashRequest(BusinessModel.CashRequest cashRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (cashRequest.CashRequestID > 0)
            {
                Models.CashRequest obj = db.CashRequests.Find(cashRequest.CashRequestID);
                
                obj.CashRequestID = cashRequest.CashRequestID;
                obj.UserAccountID = cashRequest.UserAccountID;
                obj.RequestDate = cashRequest.RequestDate;
                obj.ResponseDate = cashRequest.ResponseDate;
                obj.RequestAmount = cashRequest.RequestAmount;
                obj.ResponseAmount = cashRequest.ResponseAmount;
                obj.CreatedDate = cashRequest.CreatedDate;
                obj.CreatedBy = cashRequest.CreatedBy;
                obj.LastModifiedDate = cashRequest.LastModifiedDate;
                obj.LastModifiedBy = cashRequest.LastModifiedBy;

                db.SaveChanges();

            }
            else
            {
                Models.CashRequest obj = new Models.CashRequest
                {
                    CashRequestID = cashRequest.CashRequestID,
                    UserAccountID = cashRequest.UserAccountID,
                    RequestDate = cashRequest.RequestDate,
                    ResponseDate = cashRequest.ResponseDate,
                    RequestAmount = cashRequest.RequestAmount,
                    ResponseAmount = cashRequest.ResponseAmount,
                    CreatedDate = cashRequest.CreatedDate,
                    CreatedBy = cashRequest.CreatedBy,
                    LastModifiedDate = cashRequest.LastModifiedDate,
                    LastModifiedBy = cashRequest.LastModifiedBy
                };
                db.CashRequests.Add(obj);
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = cashRequest.CashRequestID }, cashRequest);
        }
    }
}