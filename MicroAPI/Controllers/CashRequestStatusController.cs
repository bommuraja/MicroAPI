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
    public class CashRequestStatusController : ApiController
    {
        private MicroEntities db = new MicroEntities();

        // Method : 1
        // GET: api/DataEntryOperators
        public List<BusinessModel.CashRequestStatu> GetCashRequestStatus()
        {
            List<BusinessModel.CashRequestStatu> objList = new List<BusinessModel.CashRequestStatu>();
            foreach (var item in db.CashRequestStatus)
            {
                objList.Add(
                    new BusinessModel.CashRequestStatu
                    {
                        CashRequestStatusID = item.CashRequestStatusID,
                        CashRequestStatusName = item.CashRequestStatusName,
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
        [Route("api/CashRequestStatu/{id}")]
        public bool GetRemoveCashRequestStatuDetail(int id)
        {
            var cashRequestStatu = db.CashRequestStatus.Find(id);
            if (cashRequestStatu == null)
            {
                return false;
            }
            try
            {
                db.CashRequestStatus.Remove(cashRequestStatu);
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
        [ResponseType(typeof(BusinessModel.CashRequestStatu))]
        public BusinessModel.CashRequestStatu GetCashRequestStatu(int id)
        {
            var cashRequestStatu = db.CashRequestStatus.Find(id);
            BusinessModel.CashRequestStatu obj = new BusinessModel.CashRequestStatu();
            if (cashRequestStatu != null)
            {
                obj = new BusinessModel.CashRequestStatu
                {
                    CashRequestStatusID = cashRequestStatu.CashRequestStatusID,
                    CashRequestStatusName = cashRequestStatu.CashRequestStatusName,
                    CreatedDate = cashRequestStatu.CreatedDate,
                    CreatedBy = cashRequestStatu.CreatedBy,
                    LastModifiedDate = cashRequestStatu.LastModifiedDate,
                    LastModifiedBy = cashRequestStatu.LastModifiedBy
                };
            }
            return obj;
        }

        // Method : 4
        // POST: api/UserAccounts
        [ResponseType(typeof(BusinessModel.CashRequestStatu))]
        public IHttpActionResult PostCashRequestStatu(BusinessModel.CashRequestStatu cashRequestStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (cashRequestStatu.CashRequestStatusID > 0)
            {
                Models.CashRequestStatu obj = db.CashRequestStatus.Find(cashRequestStatu.CashRequestStatusID);
                obj.CashRequestStatusID = cashRequestStatu.CashRequestStatusID;
                obj.CashRequestStatusName = cashRequestStatu.CashRequestStatusName;
                obj.CreatedDate = cashRequestStatu.CreatedDate;
                obj.CreatedBy = cashRequestStatu.CreatedBy;
                obj.LastModifiedDate = cashRequestStatu.LastModifiedDate;
                obj.LastModifiedBy = cashRequestStatu.LastModifiedBy;
                db.SaveChanges();

            }
            else
            {
                Models.CashRequestStatu obj = new Models.CashRequestStatu
                {

                    CashRequestStatusID = cashRequestStatu.CashRequestStatusID,
                    CashRequestStatusName = cashRequestStatu.CashRequestStatusName,
                    CreatedDate = cashRequestStatu.CreatedDate,
                    CreatedBy = cashRequestStatu.CreatedBy,
                    LastModifiedDate = cashRequestStatu.LastModifiedDate,
                    LastModifiedBy = cashRequestStatu.LastModifiedBy
                };
                db.CashRequestStatus.Add(obj);
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = cashRequestStatu.CashRequestStatusID }, cashRequestStatu);
        }
    }
}