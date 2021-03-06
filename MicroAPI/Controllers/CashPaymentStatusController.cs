﻿using System;
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
    public class CashPaymentStatusController : ApiController
    {
        private MicroEntities db = new MicroEntities();

        // Method : 1
        // GET: api/DataEntryOperators
        public List<BusinessModel.CashPaymentStatusData> GetCashPaymentStatus()
        {
            List<BusinessModel.CashPaymentStatusData> objList = new List<BusinessModel.CashPaymentStatusData>();
            foreach (var item in db.CashPaymentStatus)
            {
                objList.Add(
                    new BusinessModel.CashPaymentStatusData
                    {
                        CashPaymentStatusID = item.CashPaymentStatusID,
                        CashPaymentStatusName = item.CashPaymentStatusName,
                        CreatedDate = item.CreatedDate,
                        CreatedBy = item.CreatedBy,
                        LastModifiedDate = item.LastModifiedDate,
                        LastModifiedBy = item.LastModifiedBy
                    }
                );
            }
            return objList;
        }

        // Method : 2 test
        [Route("api/DropCashPaymentStatu/{id}")]
        public bool GetRemoveCashPaymentStatuDetail(int id)
        {
            var cashPaymentStatu = db.CashPaymentStatus.Find(id);
            if (cashPaymentStatu == null)
            {
                return false;
            }
            try
            {
                db.CashPaymentStatus.Remove(cashPaymentStatu);
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
        [ResponseType(typeof(BusinessModel.CashPaymentStatusData))]
        public BusinessModel.CashPaymentStatusData GetCashPaymentStatu(int id)
        {
            var cashPaymentStatu = db.CashPaymentStatus.Find(id);
            BusinessModel.CashPaymentStatusData obj = new BusinessModel.CashPaymentStatusData();
            if (cashPaymentStatu != null)
            {
                obj = new BusinessModel.CashPaymentStatusData
                {
                    CashPaymentStatusID = cashPaymentStatu.CashPaymentStatusID,
                    CashPaymentStatusName = cashPaymentStatu.CashPaymentStatusName,
                    CreatedDate = cashPaymentStatu.CreatedDate,
                    CreatedBy = cashPaymentStatu.CreatedBy,
                    LastModifiedDate = cashPaymentStatu.LastModifiedDate,
                    LastModifiedBy = cashPaymentStatu.LastModifiedBy
                };
            }
            return obj;
        }

        // Method : 4
        // POST: api/UserAccounts
        [ResponseType(typeof(BusinessModel.CashPaymentStatusData))]
        public IHttpActionResult PostCashPaymentStatu(BusinessModel.CashPaymentStatusData cashPaymentStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (cashPaymentStatu.CashPaymentStatusID > 0)
            {
                Models.CashPaymentStatu obj = db.CashPaymentStatus.Find(cashPaymentStatu.CashPaymentStatusID);
                obj.CashPaymentStatusID = cashPaymentStatu.CashPaymentStatusID;
                obj.CashPaymentStatusName = cashPaymentStatu.CashPaymentStatusName;
                obj.CreatedDate = cashPaymentStatu.CreatedDate;
                obj.CreatedBy = cashPaymentStatu.CreatedBy;
                obj.LastModifiedDate = cashPaymentStatu.LastModifiedDate;
                obj.LastModifiedBy = cashPaymentStatu.LastModifiedBy;

                db.SaveChanges();

            }
            else
            {
                Models.CashPaymentStatu obj = new Models.CashPaymentStatu
                {
                    CashPaymentStatusID = cashPaymentStatu.CashPaymentStatusID,
                    CashPaymentStatusName = cashPaymentStatu.CashPaymentStatusName,
                    CreatedDate = cashPaymentStatu.CreatedDate,
                    CreatedBy = cashPaymentStatu.CreatedBy,
                    LastModifiedDate = cashPaymentStatu.LastModifiedDate,
                    LastModifiedBy = cashPaymentStatu.LastModifiedBy
                };
                db.CashPaymentStatus.Add(obj);
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = cashPaymentStatu.CashPaymentStatusID }, cashPaymentStatu);
        }
    }
}