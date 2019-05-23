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
using MicroAPI.BusinessModel;
using MicroAPI.Models;

namespace MicroAPI.Controllers
{
    public class CashRequestsController : ApiController
    {
        private MicroEntities db = new MicroEntities();

        // Method : 1
        // GET: api/DataEntryOperators
        public List<BusinessModel.CashRequestData> GetCashRequests()
        {
            try
            {
                List<BusinessModel.CashRequestData> objList = new List<BusinessModel.CashRequestData>();

                List<BusinessModel.CashRequestStatusData> objCashRequestStatusList = new List<BusinessModel.CashRequestStatusData>();
                List<CashRequest> CasshRequestList = db.CashRequests.ToList();
                foreach (var item in db.CashRequestStatus)
                {
                    objCashRequestStatusList.Add(new BusinessModel.CashRequestStatusData
                    {
                        CashRequestStatusID = item.CashRequestStatusID,
                        CashRequestStatusName = item.CashRequestStatusName,
                        CreatedBy = item.CreatedBy,
                        CreatedDate = item.CreatedDate,
                        LastModifiedBy = item.LastModifiedBy,
                        LastModifiedDate = item.LastModifiedDate
                    });
                }
                for (int item=0; item < CasshRequestList.Count() ;item++)
                {
                    try
                    {
                        if (CasshRequestList[item].IsActive!=null && (bool)CasshRequestList[item].IsActive)
                        {
                            var objCashRequest = new BusinessModel.CashRequestData();


                            objCashRequest.CashRequestID = CasshRequestList[item].CashRequestID;
                            objCashRequest.UserAccountID = CasshRequestList[item].UserAccountID;
                            objCashRequest.UserAccountName = CasshRequestList[item].UserAccount.UserAccountName;
                            objCashRequest.CashRequestStatusID = CasshRequestList[item].CashRequestStatusID;
                            objCashRequest.CashRequestStatusName = (CasshRequestList[item].CashRequestStatu != null) ? CasshRequestList[item].CashRequestStatu.CashRequestStatusName : "";
                            objCashRequest.PaymentFromBankID = CasshRequestList[item].PaymentFromBankID;
                            objCashRequest.PaymentFromBankName = (CasshRequestList[item].FinancialInstitution != null) ? CasshRequestList[item].FinancialInstitution.InstitutionName : "";
                            objCashRequest.PaymentToBankID = CasshRequestList[item].PaymentToBankID;
                            objCashRequest.PaymentToBankName = (CasshRequestList[item].FinancialInstitution1 != null) ? CasshRequestList[item].FinancialInstitution1.InstitutionName : "";
                            objCashRequest.RequestDate = CasshRequestList[item].RequestDate;
                            objCashRequest.ResponseDate = CasshRequestList[item].ResponseDate;
                            objCashRequest.RequestAmount = CasshRequestList[item].RequestAmount;
                            objCashRequest.ResponseAmount = CasshRequestList[item].ResponseAmount;
                            objCashRequest.CashRequestStatusList = objCashRequestStatusList;
                            objCashRequest.CreatedDate = CasshRequestList[item].CreatedDate;
                            objCashRequest.CreatedBy = CasshRequestList[item].CreatedBy;
                            objCashRequest.LastModifiedDate = CasshRequestList[item].LastModifiedDate;
                            objCashRequest.LastModifiedBy = CasshRequestList[item].LastModifiedBy;

                            objList.Add(objCashRequest);
                        }

                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                   
                }
                return objList;
            }
            catch (Exception ex)
            {

                throw;
            }
           
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
        [ResponseType(typeof(BusinessModel.CashRequestData))]
        public BusinessModel.CashRequestData GetCashRequest(int id)
        {
            List<BusinessModel.CashRequestStatusData> objCashRequestStatusList = new List<BusinessModel.CashRequestStatusData>();
            foreach (var item in db.CashRequestStatus)
            {
                objCashRequestStatusList.Add(new BusinessModel.CashRequestStatusData
                {
                    CashRequestStatusID = item.CashRequestStatusID,
                    CashRequestStatusName = item.CashRequestStatusName,
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate,
                    LastModifiedBy = item.LastModifiedBy,
                    LastModifiedDate = item.LastModifiedDate
                });
            }
            var cashRequest = db.CashRequests.Find(id);
            BusinessModel.CashRequestData obj = new BusinessModel.CashRequestData();
            if (cashRequest != null)
            {
                var objPaymentFromBankList = new List<FinancialInstitutionData>();
                var objPaymentFromBankListDB = db.FinancialInstitutions.Where(m => m.NickName == "Admin").ToList();
                foreach (var item in objPaymentFromBankListDB)
                {
                    objPaymentFromBankList.Add(new FinancialInstitutionData
                    {
                        FinancialInstitutionID = item.FinancialInstitutionID,
                        AccountName = item.AccountName,
                        NickName = item.NickName,
                        AccountNumber = item.AccountName,
                        InstitutionName = item.InstitutionName,
                        InstitutionIFSCCode = item.InstitutionIFSCCode,
                        InstitutionLocation = item.InstitutionLocation,
                        InstitutionCity = item.InstitutionCity
                    });
                }
                var objPaymentToBankList = new List<FinancialInstitutionData>();
                var objPaymentToBankListDB = db.FinancialInstitutions.Where(m => m.UserAccountID == cashRequest.UserAccountID).ToList();
                foreach (var item in objPaymentToBankListDB)
                {
                    objPaymentToBankList.Add(new FinancialInstitutionData
                    {
                        FinancialInstitutionID = item.FinancialInstitutionID,
                        AccountName = item.AccountName,
                        NickName = item.NickName,
                        AccountNumber = item.AccountName,
                        InstitutionName = item.InstitutionName,
                        InstitutionIFSCCode = item.InstitutionIFSCCode,
                        InstitutionLocation = item.InstitutionLocation,
                        InstitutionCity = item.InstitutionCity
                    });
                }
                obj = new BusinessModel.CashRequestData
                {
                    CashRequestID = cashRequest.CashRequestID,
                    UserAccountID = cashRequest.UserAccountID,
                    UserAccountName = cashRequest.UserAccount.UserAccountName,
                    CashRequestStatusID = cashRequest.CashRequestStatusID,
                    CashRequestStatusName =(cashRequest.CashRequestStatu!=null)?cashRequest.CashRequestStatu.CashRequestStatusName:"",
                    PaymentFromBankID = cashRequest.PaymentFromBankID,
                    PaymentFromBankName = (cashRequest.FinancialInstitution!=null)?cashRequest.FinancialInstitution.InstitutionName:"",
                    PaymentFromBankList = objPaymentFromBankList,
                    PaymentToBankID = cashRequest.PaymentToBankID,
                    PaymentToBankName =(cashRequest.FinancialInstitution1!=null)?cashRequest.FinancialInstitution1.InstitutionName:"",
                    PaymentToBankList = objPaymentToBankList,
                    RequestDate = cashRequest.RequestDate,
                    ResponseDate = cashRequest.ResponseDate,
                    RequestAmount = cashRequest.RequestAmount,
                    ResponseAmount = cashRequest.ResponseAmount,
                    CashRequestStatusList = objCashRequestStatusList,
                    CreatedDate = cashRequest.CreatedDate,
                    CreatedBy = cashRequest.CreatedBy,
                    LastModifiedDate = cashRequest.LastModifiedDate,
                    LastModifiedBy = cashRequest.LastModifiedBy,
                    IsActive=(cashRequest.IsActive!=null)? (bool)cashRequest.IsActive:false
                };
            }
            return obj;
        }

        [ResponseType(typeof(BusinessModel.CashRequestData))]
        [Route("api/GetCashRequestByUserID/{id}")]
        public BusinessModel.CashRequestData GetCashRequestByUserID(int id)

        {
            try
            {
                List<BusinessModel.CashRequestStatusData> objCashRequestStatusList = new List<BusinessModel.CashRequestStatusData>();
                foreach (var item in db.CashRequestStatus)
                {
                    objCashRequestStatusList.Add(new BusinessModel.CashRequestStatusData
                    {
                        CashRequestStatusID = item.CashRequestStatusID,
                        CashRequestStatusName = item.CashRequestStatusName,
                        CreatedBy = item.CreatedBy,
                        CreatedDate = item.CreatedDate,
                        LastModifiedBy = item.LastModifiedBy,
                        LastModifiedDate = item.LastModifiedDate
                    });
                }
                var objPaymentFromBankList = new List<FinancialInstitutionData>();
                var objPaymentFromBankListDB = db.FinancialInstitutions.Where(m => m.NickName == "Admin").ToList();
                foreach (var item in objPaymentFromBankListDB)
                {
                    objPaymentFromBankList.Add(new FinancialInstitutionData
                    {
                        FinancialInstitutionID = item.FinancialInstitutionID,
                        AccountName = item.AccountName,
                        NickName = item.NickName,
                        AccountNumber = item.AccountName,
                        InstitutionName = item.InstitutionName,
                        InstitutionIFSCCode = item.InstitutionIFSCCode,
                        InstitutionLocation = item.InstitutionLocation,
                        InstitutionCity = item.InstitutionCity
                    });
                }
                var objPaymentToBankList = new List<FinancialInstitutionData>();
                var objPaymentToBankListDB = db.FinancialInstitutions.Where(m => m.UserAccountID == id).ToList();
                foreach (var item in objPaymentToBankListDB)
                {
                    objPaymentToBankList.Add(new FinancialInstitutionData
                    {
                        FinancialInstitutionID = item.FinancialInstitutionID,
                        AccountName = item.AccountName,
                        NickName = item.NickName,
                        AccountNumber = item.AccountName,
                        InstitutionName = item.InstitutionName,
                        InstitutionIFSCCode = item.InstitutionIFSCCode,
                        InstitutionLocation = item.InstitutionLocation,
                        InstitutionCity = item.InstitutionCity
                    });
                }
                var sdf = db.CashRequests.ToList();
                var cashRequest = db.CashRequests.Where(m => m.UserAccountID == id).OrderByDescending(n=>n.CashRequestID).FirstOrDefault();
                BusinessModel.CashRequestData obj = new BusinessModel.CashRequestData();
                if (cashRequest != null && cashRequest.IsActive==true)
                {
                    obj = new BusinessModel.CashRequestData();


                    obj.CashRequestID = cashRequest.CashRequestID;
                    obj.UserAccountID = cashRequest.UserAccountID;
                    obj.UserAccountName = cashRequest.UserAccount.UserAccountName;
                    obj.CashRequestStatusID = cashRequest.CashRequestStatusID;
                    obj.CashRequestStatusName = cashRequest.CashRequestStatu.CashRequestStatusName;
                    obj.PaymentFromBankID = (cashRequest.PaymentFromBankID!=null)? cashRequest.PaymentFromBankID:0;
                    obj.PaymentFromBankName = (cashRequest.FinancialInstitution!=null)? cashRequest.FinancialInstitution.InstitutionName:"";
                    obj.PaymentFromBankList = objPaymentFromBankList;
                    obj.PaymentToBankID = (cashRequest.PaymentToBankID != null)? cashRequest.PaymentToBankID : 0;
                    obj.PaymentToBankName = (cashRequest.FinancialInstitution1 != null) ? cashRequest.FinancialInstitution1.InstitutionName:"";
                    obj.PaymentToBankList = objPaymentToBankList;
                    obj.RequestDate = cashRequest.RequestDate;
                    obj.ResponseDate = cashRequest.ResponseDate;
                    obj.RequestAmount = cashRequest.RequestAmount;
                    obj.ResponseAmount = cashRequest.ResponseAmount;
                    obj.IsActive = cashRequest.IsActive ?? true;
                    obj.CashRequestStatusList = objCashRequestStatusList;
                    obj.CreatedDate = cashRequest.CreatedDate;
                    obj.CreatedBy = cashRequest.CreatedBy;
                    obj.LastModifiedDate = cashRequest.LastModifiedDate;
                    obj.LastModifiedBy = cashRequest.LastModifiedBy;
                    
                }
                else
                {
                    obj = new BusinessModel.CashRequestData();

                    obj.CashRequestID = 0;
                    obj.UserAccountID = id;
                    obj.UserAccountName = "";
                    obj.CashRequestStatusID = db.CashRequestStatus.Where(m => m.CashRequestStatusName == "newrequest").FirstOrDefault().CashRequestStatusID;
                    obj.CashRequestStatusName = "newrequest";
                    obj.PaymentFromBankID = 0;
                    obj.PaymentToBankID = 0;
                    obj.PaymentFromBankList = objPaymentFromBankList;
                    obj.PaymentToBankList = objPaymentToBankList;
                    obj.RequestDate = DateTime.Now.ToString();
                    obj.ResponseDate = "";
                    obj.RequestAmount = "0";
                    obj.ResponseAmount = "";
                    obj.IsActive = false;
                    obj.CashRequestStatusList = objCashRequestStatusList;
                    obj.CreatedDate = DateTime.Now.ToString();
                    obj.CreatedBy = db.UserAccounts.Where(m => m.UserAccountID == id).FirstOrDefault().UserAccountName;
                    obj.LastModifiedDate = DateTime.Now.ToString();
                    obj.LastModifiedBy = db.UserAccounts.Where(m => m.UserAccountID == id).FirstOrDefault().UserAccountName;
                   
                }
                return obj;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        // Method : 4
        // POST: api/UserAccounts
        [ResponseType(typeof(BusinessModel.CashRequestData))]
        public IHttpActionResult PostCashRequest(BusinessModel.CashRequestData cashRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var cashRequestStatusName= db.CashRequestStatus.Where(m => m.CashRequestStatusID == cashRequest.CashRequestStatusID).FirstOrDefault().CashRequestStatusName;
                if (cashRequest.CashRequestID > 0 && cashRequest.IsActive)
                {
                    if (cashRequest.CashRequestStatusName == "newrequest" && cashRequest.UserActionOnCashRequest == "submit")
                    {
                        cashRequest.CashRequestStatusName = "submit";
                        cashRequest.IsActive = true;
                        cashRequest.CashRequestStatusID = db.CashRequestStatus.Where(m => m.CashRequestStatusName == cashRequest.CashRequestStatusName).FirstOrDefault().CashRequestStatusID;
                        cashRequest.PaymentFromBankID = null;
                        cashRequest.PaymentToBankID = null;
                    }
                    if (cashRequest.UserActionOnCashRequest == "cancel")
                    {
                        cashRequest.CashRequestStatusName = "cancel";
                        cashRequest.IsActive = false;
                        cashRequest.CashRequestStatusID = db.CashRequestStatus.Where(m => m.CashRequestStatusName == cashRequest.CashRequestStatusName).FirstOrDefault().CashRequestStatusID;
                    }
                    if (cashRequestStatusName == "approved"|| cashRequestStatusName == "rejected")
                    {
                        cashRequest.IsActive = false;
                    }

                    Models.CashRequest obj = db.CashRequests.Find(cashRequest.CashRequestID);
                    obj.CashRequestStatusID = cashRequest.CashRequestStatusID;
                    obj.IsActive = cashRequest.IsActive;
                    obj.CashRequestID = cashRequest.CashRequestID;
                    obj.UserAccountID = cashRequest.UserAccountID;
                    obj.CashRequestStatusID = cashRequest.CashRequestStatusID;
                    obj.PaymentFromBankID = (cashRequest.PaymentFromBankID>0)? cashRequest.PaymentFromBankID:null;
                    obj.PaymentToBankID = (cashRequest.PaymentToBankID>0)? cashRequest.PaymentToBankID:null;
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
                    if (cashRequest.CashRequestStatusName == "newrequest" && cashRequest.UserActionOnCashRequest == "submit")
                    {
                        cashRequest.CashRequestStatusName = "submit";
                        cashRequest.IsActive = true;
                        cashRequest.CashRequestStatusID = db.CashRequestStatus.Where(m => m.CashRequestStatusName == cashRequest.CashRequestStatusName).FirstOrDefault().CashRequestStatusID;
                        cashRequest.PaymentFromBankID = null;
                        cashRequest.PaymentToBankID = null;
                    }
                    if (cashRequest.UserActionOnCashRequest == "cancel")
                    {
                        cashRequest.CashRequestStatusName = "cancel";
                        cashRequest.IsActive = false;
                        cashRequest.CashRequestStatusID = db.CashRequestStatus.Where(m => m.CashRequestStatusName == cashRequest.CashRequestStatusName).FirstOrDefault().CashRequestStatusID;
                    }
                    Models.CashRequest obj = new Models.CashRequest
                    {
                        CashRequestID = cashRequest.CashRequestID,
                        UserAccountID = cashRequest.UserAccountID,
                        CashRequestStatusID = cashRequest.CashRequestStatusID,
                        IsActive = cashRequest.IsActive,
                        PaymentFromBankID = cashRequest.PaymentFromBankID,
                        PaymentToBankID = cashRequest.PaymentToBankID,
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
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}