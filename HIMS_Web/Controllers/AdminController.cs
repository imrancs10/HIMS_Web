//using HIMS_Web.BAL.Commom;
//using HIMS_Web.BAL.Masters;
//using HIMS_Web.BAL.Reports;
using DataLayer;
using HIMS_Web.BAL.Masters;
using HIMS_Web.Global;
using HIMS_Web.Models.Masters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HIMS_Web.Controllers
{
    public class AdminController : CommonController
    {
        // GET: Admin
        public ActionResult IPDEntry()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveIPDEntry(string PatientId, string IPDNo, string AdmittedDateTime,
            string PetientName, string Mobile, string Gender, string FathersHusbandName,
            string Treatment, string Address, string Area, string OtherAreaName,
            string department, string IDorAadharNumber, string Age, string OtherTreatment, string IDNumber)
        //string Title,string Email, string DOB, string MariatalStatus, string state, string city,string religion, string pincode,
        {
            AdminDetails _details = new AdminDetails();
            int areaId = Area == "Other" ? 0 : Convert.ToInt32(Area);
            if (!string.IsNullOrEmpty(OtherAreaName))
            {
                var areaModel = new Area()
                {
                    AreaName = OtherAreaName,
                    CityId = 1318
                };
                areaId = _details.SaveArea(areaModel);
            }
            int treatmentId = Treatment == "Other" ? 0 : Convert.ToInt32(Treatment);
            if (!string.IsNullOrEmpty(OtherTreatment))
            {
                var treatmentModel = new Treatment()
                {
                    TreatmentName = OtherTreatment,
                    Description = OtherTreatment
                };
                treatmentId = _details.SaveTreatment(treatmentModel);
            }
            if (areaId > 0 && treatmentId > 0)
            {
                IpdPatientInfo ipdInfo = new IpdPatientInfo()
                {
                    Address = Address,
                    AdmittedDateTime = Convert.ToDateTime(AdmittedDateTime),
                    AreaId = areaId,
                    //CityId = Convert.ToInt32(city),
                    CreatedDate = DateTime.Now,
                    DepartmentId = Convert.ToInt32(department),
                    //DOB = Convert.ToDateTime(DOB),
                    //Email = Email,
                    FatherOrHusbandName = FathersHusbandName,
                    Gender = Gender,
                    IDorAadharNumber = IDorAadharNumber,
                    IpdNo = IPDNo,
                    //MaritalStatus = MariatalStatus,
                    MobileNumber = Mobile,
                    //PatientId = PatientId,
                    PatientName = PetientName,
                    //PinCode = Convert.ToInt32(pincode),
                    //Religion = religion,
                    //StateId = Convert.ToInt32(state),
                    //Title = Title,
                    TreatmentId = treatmentId,
                    Age = Convert.ToInt32(Age),
                    IPDStatus = "Admitted",
                    IDNumber = IDNumber,
                };

                var result = _details.SaveIPDEntry(ipdInfo);

                if (result == Enums.CrudStatus.Saved)
                    SetAlertMessage("IPD Patient Info saved", "IPD Patient Info");
                else
                    SetAlertMessage("IPD Patient Info saved failed", "IPD Patient Info");
            }
            else
                SetAlertMessage("IPD Patient Info saved failed", "IPD Patient Info");
            return RedirectToAction("IPDEntry");
        }
        public ActionResult IPDList()
        {
            var _details = new AdminDetails();
            ViewData["PageData"] = _details.GetIPDList();
            return View();
        }
        public ActionResult IpdDashboard()
        {
            return View();
        }
        public ActionResult AddArea()
        {
            var _details = new MasterDetails();
            ViewData["PageData"] = _details.GetAreaByCityId(0);
            return View();
        }
        public JsonResult GetAreaDetail(int AreaId)
        {
            var _details = new MasterDetails();
            return Json(_details.GetAreaByAreaId(AreaId), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveAreaMaster(string AreaId, string areaname)
        {
            Area pages = new Area();
            pages.AreaId = !string.IsNullOrEmpty(AreaId) ? Convert.ToInt32(AreaId) : 0;
            pages.AreaName = areaname;
            pages.CityId = 1318;
            //pages.CityId = UserData.UserId;
            // pages.CreatedDate = DateTime.Now;
            // pages.IsActive = active == "on" ? true : false;
            AdminDetails _details = new AdminDetails();
            var result = _details.AddUpdateArea(pages);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Area created", "Save Area master");
            }
            else
                SetAlertMessage("Area creation failed", "Save Area master");
            return RedirectToAction("AddArea");

        }
        public ActionResult IPDPatientStatus()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetPatientDetail(string searchText)
        {
            AdminDetails _details = new AdminDetails();
            var data = _details.GetIPDDetail(searchText);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveIPDLabReport(IpdLabReportModel model)
        {
            AdminDetails _details = new AdminDetails();
            IpdPatientLabReport report = new IpdPatientLabReport()
            {
                CreatedBy = UserData.UserId,
                CreatedDate = DateTime.Now,
                ELISAIGM_Status = model.ELISAIGMStatus
            };
            var result = _details.SaveIPDLabReport(report);
            return Json("Save", JsonRequestBehavior.AllowGet);
        }
        public ActionResult PatientBillReport()
        {
            return View();
        }

        public ActionResult PatientLabReport()
        {
            return View();
        }

        [HttpPost]
        //HttpPostedFileBase reportfile,
        public ActionResult SetBillingReport(int PatientId, string BillNo, string BillType, DateTime BillDate, string ReportUrl, decimal BillAmount, string BillID)
        {
            string ReportPath = string.Empty;
            //if (reportfile != null)
            //{
            //    CommonDetails fileupload = new CommonDetails();
            //    ReportPath = fileupload.ReportFileUpload(reportfile, Global.Enums.ReportType.Bill, BillNo);
            //}
            //else
            //{
            //    ReportPath = string.Empty;
            //}
            ReportPath = string.Empty;
            //ReportDetails _details = new ReportDetails();
            //_details.SetBillReportData(PatientId, BillNo, BillType, BillDate, ReportPath, BillAmount, BillID);
            return View("PatientBillReport");
        }

        [HttpPost]
        public ActionResult SetLabReport(HttpPostedFileBase reportfile, DateTime ReportDate, int PatientId, string BillNo, string RefNo, string LabName, string ReportUrl, string doctorId)
        {
            string ReportPath = string.Empty;
            //if (reportfile != null)
            //{
            //    CommonDetails fileupload = new CommonDetails();
            //    ReportPath = fileupload.ReportFileUpload(reportfile, Global.Enums.ReportType.Lab, RefNo);
            //}
            //else
            //{
            //    ReportPath = ReportUrl;
            //}
            //ReportDetails _details = new ReportDetails();
            //_details.SetLabReportData(PatientId, BillNo, RefNo, ReportPath, LabName, ReportDate, doctorId);
            return View("PatientLabReport");
        }

    }
}