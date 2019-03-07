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
    public class InterestPercentagesController : ApiController
    {
        private MicroEntities db = new MicroEntities();

        // Method : 1
        // GET: api/DataEntryOperators
        public List<BusinessModel.InterestPercentage> GetInterestPercentages()
        {
            List<BusinessModel.InterestPercentage> objList = new List<BusinessModel.InterestPercentage>();
            foreach (var item in db.InterestPercentages)
            {
                objList.Add(
                    new BusinessModel.InterestPercentage
                    {
                       
                        InterestPercentageID = item.InterestPercentageID,
                        UserAccountID = item.InterestPercentageID,
                        InterestPercentage1 = item.InterestPercentage1,
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
        [Route("api/DropInterestPercentage/{id}")]
        public bool GetRemoveInterestPercentageDetail(int id)
        {
            var interestPercentage = db.InterestPercentages.Find(id);
            if (interestPercentage == null)
            {
                return false;
            }
            try
            {
                db.InterestPercentages.Remove(interestPercentage);
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
        [ResponseType(typeof(BusinessModel.InterestPercentage))]
        public BusinessModel.InterestPercentage GetInterestPercentage(int id)
        {
            var interestPercentage = db.InterestPercentages.Find(id);
            BusinessModel.InterestPercentage obj = new BusinessModel.InterestPercentage();
            if (interestPercentage != null)
            {
                obj = new BusinessModel.InterestPercentage
                {
                    InterestPercentageID = interestPercentage.InterestPercentageID,
                    UserAccountID = interestPercentage.InterestPercentageID,
                    InterestPercentage1 = interestPercentage.InterestPercentage1,
                    CreatedDate = interestPercentage.CreatedDate,
                    CreatedBy = interestPercentage.CreatedBy,
                    LastModifiedDate = interestPercentage.LastModifiedDate,
                    LastModifiedBy = interestPercentage.LastModifiedBy
                };
            }
            return obj;
        }

        // Method : 4
        // POST: api/UserAccounts
        [ResponseType(typeof(BusinessModel.InterestPercentage))]
        public IHttpActionResult PostInterestPercentage(BusinessModel.InterestPercentage interestPercentage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (interestPercentage.InterestPercentageID > 0)
            {
                Models.InterestPercentage obj = db.InterestPercentages.Find(interestPercentage.InterestPercentageID);

                obj.InterestPercentageID = interestPercentage.InterestPercentageID;
                obj.UserAccountID = interestPercentage.InterestPercentageID;
                obj.InterestPercentage1 = interestPercentage.InterestPercentage1;
                obj.CreatedDate = interestPercentage.CreatedDate;
                obj.CreatedBy = interestPercentage.CreatedBy;
                obj.LastModifiedDate = interestPercentage.LastModifiedDate;
                obj.LastModifiedBy = interestPercentage.LastModifiedBy;


                db.SaveChanges();

            }
            else
            {
                Models.InterestPercentage obj = new Models.InterestPercentage
                {
                    InterestPercentageID = interestPercentage.InterestPercentageID,
                    UserAccountID = interestPercentage.InterestPercentageID,
                    InterestPercentage1 = interestPercentage.InterestPercentage1,
                    CreatedDate = interestPercentage.CreatedDate,
                    CreatedBy = interestPercentage.CreatedBy,
                    LastModifiedDate = interestPercentage.LastModifiedDate,
                    LastModifiedBy = interestPercentage.LastModifiedBy
                };
                db.InterestPercentages.Add(obj);
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = interestPercentage.InterestPercentageID }, interestPercentage);
        }
    }
}