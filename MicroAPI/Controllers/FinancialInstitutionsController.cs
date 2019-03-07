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
    public class FinancialInstitutionsController : ApiController
    {
        private MicroEntities db = new MicroEntities();

        // Method : 1
        // GET: api/DataEntryOperators
        public List<BusinessModel.FinancialInstitution> GetFinancialInstitutions()
        {
            List<BusinessModel.FinancialInstitution> objList = new List<BusinessModel.FinancialInstitution>();
            foreach (var item in db.FinancialInstitutions)
            {
                objList.Add(
                    new BusinessModel.FinancialInstitution
                    {
                        FinancialInstitutionID = item.FinancialInstitutionID,
                        UserAccountID = item.UserAccountID,
                        AccountName = item.AccountName,
                        NickName = item.NickName,
                        AccountNumber = item.AccountNumber,
                        InstitutionName = item.InstitutionName,
                        InstitutionIFSCCode = item.InstitutionIFSCCode,
                        InstitutionLocation = item.InstitutionLocation,
                        InstitutionCity = item.InstitutionCity,
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
        [Route("api/DropFinancialInstitution/{id}")]
        public bool GetRemoveFinancialInstitutionDetail(int id)
        {
            var financialInstitution = db.FinancialInstitutions.Find(id);
            if (financialInstitution == null)
            {
                return false;
            }
            try
            {
                db.FinancialInstitutions.Remove(financialInstitution);
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
        [ResponseType(typeof(BusinessModel.FinancialInstitution))]
        public BusinessModel.FinancialInstitution GetFinancialInstitution(int id)
        {
            var financialInstitution = db.FinancialInstitutions.Find(id);
            BusinessModel.FinancialInstitution obj = new BusinessModel.FinancialInstitution();
            if (financialInstitution != null)
            {
                obj = new BusinessModel.FinancialInstitution
                {
                    FinancialInstitutionID = financialInstitution.FinancialInstitutionID,
                    UserAccountID = financialInstitution.UserAccountID,
                    AccountName = financialInstitution.AccountName,
                    NickName = financialInstitution.NickName,
                    AccountNumber = financialInstitution.AccountNumber,
                    InstitutionName = financialInstitution.InstitutionName,
                    InstitutionIFSCCode = financialInstitution.InstitutionIFSCCode,
                    InstitutionLocation = financialInstitution.InstitutionLocation,
                    InstitutionCity = financialInstitution.InstitutionCity,
                    CreatedDate = financialInstitution.CreatedDate,
                    CreatedBy = financialInstitution.CreatedBy,
                    LastModifiedDate = financialInstitution.LastModifiedDate,
                    LastModifiedBy = financialInstitution.LastModifiedBy
                };
            }
            return obj;
        }

        // Method : 4
        // POST: api/UserAccounts
        [ResponseType(typeof(BusinessModel.FinancialInstitution))]
        public IHttpActionResult PostFinancialInstitution(BusinessModel.FinancialInstitution financialInstitution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (financialInstitution.FinancialInstitutionID > 0)
            {
                Models.FinancialInstitution obj = db.FinancialInstitutions.Find(financialInstitution.FinancialInstitutionID);
                obj.FinancialInstitutionID = financialInstitution.FinancialInstitutionID;
                obj.UserAccountID = financialInstitution.UserAccountID;
                obj.AccountName = financialInstitution.AccountName;
                obj.NickName = financialInstitution.NickName;
                obj.AccountNumber = financialInstitution.AccountNumber;
                obj.InstitutionName = financialInstitution.InstitutionName;
                obj.InstitutionIFSCCode = financialInstitution.InstitutionIFSCCode;
                obj.InstitutionLocation = financialInstitution.InstitutionLocation;
                obj.InstitutionCity = financialInstitution.InstitutionCity;
                obj.CreatedDate = financialInstitution.CreatedDate;
                obj.CreatedBy = financialInstitution.CreatedBy;
                obj.LastModifiedDate = financialInstitution.LastModifiedDate;
                obj.LastModifiedBy = financialInstitution.LastModifiedBy;
                db.SaveChanges();

            }
            else
            {
                Models.FinancialInstitution obj = new Models.FinancialInstitution
                {
                    FinancialInstitutionID = financialInstitution.FinancialInstitutionID,
                    UserAccountID = financialInstitution.UserAccountID,
                    AccountName = financialInstitution.AccountName,
                    NickName = financialInstitution.NickName,
                    AccountNumber = financialInstitution.AccountNumber,
                    InstitutionName = financialInstitution.InstitutionName,
                    InstitutionIFSCCode = financialInstitution.InstitutionIFSCCode,
                    InstitutionLocation = financialInstitution.InstitutionLocation,
                    InstitutionCity = financialInstitution.InstitutionCity,
                    CreatedDate = financialInstitution.CreatedDate,
                    CreatedBy = financialInstitution.CreatedBy,
                    LastModifiedDate = financialInstitution.LastModifiedDate,
                    LastModifiedBy = financialInstitution.LastModifiedBy
                };
                db.FinancialInstitutions.Add(obj);
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = financialInstitution.FinancialInstitutionID }, financialInstitution);
        }
    }
}