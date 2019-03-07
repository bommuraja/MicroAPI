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
        public List<BusinessModel.CashRequestStatusData> GetCashRequestStatus()
        {
            List<BusinessModel.CashRequestStatusData> objList = new List<BusinessModel.CashRequestStatusData>();
            foreach (var item in db.CashRequestStatus)
            {
                objList.Add(
                    new BusinessModel.CashRequestStatusData
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
        [ResponseType(typeof(BusinessModel.CashRequestStatusData))]
        public BusinessModel.CashRequestStatusData GetCashRequestStatu(int id)
        {
            var cashRequestStatu = db.CashRequestStatus.Find(id);
            BusinessModel.CashRequestStatusData obj = new BusinessModel.CashRequestStatusData();
            if (cashRequestStatu != null)
            {
                obj = new BusinessModel.CashRequestStatusData
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
        [ResponseType(typeof(BusinessModel.CashRequestStatusData))]
        public IHttpActionResult PostCashRequestStatu(BusinessModel.CashRequestStatusData cashRequestStatu)
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