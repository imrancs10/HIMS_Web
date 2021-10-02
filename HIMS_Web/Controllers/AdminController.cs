//using HIMS_Web.BAL.Commom;
//using HIMS_Web.BAL.Masters;
//using HIMS_Web.BAL.Reports;
using DataLayer;
using HIMS_Web.BAL.Masters;
using HIMS_Web.Global;
using HIMS_Web.Infrastructure.Utility;
using HIMS_Web.Models.Masters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HIMS_Web.Controllers
{
    [AdminSessionTimeout]
    public class AdminController : CommonController
    {
        // GET: Admin
        public ActionResult IPDEntry(int? patientId)
        {
            if (patientId != null && patientId > 0)
            {
                ViewData["Mode"] = "Edit";
            }
            else
            {
                ViewData["Mode"] = "Add";
            }
            return View();
        }
        public JsonResult GetPatientDetailById(int patientId)
        {
            AdminDetails _details = new AdminDetails();
            var patientInfo = _details.GetIPDPatientById(patientId);
            return Json(patientInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveIPDEntry(int? PatientId, string IPDNo, string AdmittedDateTime,
            string PetientName, string Mobile, string Gender, string FathersHusbandName,
            List<int> Treatment, string Address, string AreaId, string OtherAreaName,
            string department, string IDorAadharNumber, string Age, string OtherTreatment, string IDNumber)
        //string Title,string Email, string DOB, string MariatalStatus, string state, string city,string religion, string pincode,
        {
            AdminDetails _details = new AdminDetails();
            if (PatientId == null)
            {
                bool isDuplicateIPDNumber = _details.duplicateIPDNumber(IPDNo);
                if (isDuplicateIPDNumber)
                {
                    SetAlertMessage("IPD Number is already exist", "IPD Patient");
                    return RedirectToAction("IPDEntry");
                }
            }
            int areaId = AreaId == "Other" ? 0 : Convert.ToInt32(AreaId);
            if (!string.IsNullOrEmpty(OtherAreaName))
            {
                var areaModel = new Area()
                {
                    AreaName = OtherAreaName,
                    CityId = 1318
                };
                areaId = _details.SaveArea(areaModel);
            }
            //int treatmentId = Treatment == "Other" ? 0 : Convert.ToInt32(Treatment);
            //if (!string.IsNullOrEmpty(OtherTreatment))
            //{
            //    var treatmentModel = new Treatment()
            //    {
            //        TreatmentName = OtherTreatment,
            //        Description = OtherTreatment
            //    };
            //    treatmentId = _details.SaveTreatment(treatmentModel);
            //}
            if (areaId > 0)
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
                    //TreatmentId = treatmentId,
                    Age = Convert.ToInt32(Age),
                    IPDStatus = "Admit",
                    IDNumber = IDNumber,
                    IsActive = true,
                    PatientId = Convert.ToInt32(PatientId)
                };

                var result = _details.SaveIPDEntry(ipdInfo);
                if (result > 0)
                {
                    _details.SaveIPDTreatment(Treatment, result);
                    SetAlertMessage("IPD Patient Info saved", "IPD Patient Info");
                }
                else
                    SetAlertMessage("IPD Patient Info saved failed", "IPD Patient Info");
            }
            else
                SetAlertMessage("IPD Patient Info saved failed", "IPD Patient Info");
            if (PatientId == null)
                return RedirectToAction("IPDEntry");
            else
                return RedirectToAction("IPDList");
        }
        public ActionResult IPDList()
        {
            var _details = new AdminDetails();
            var ipdList = _details.GetIPDList();
            return View(ipdList);
        }
        public ActionResult IPDListExportToExcel()
        {
            var _details = new AdminDetails();
            var ipdList = _details.GetIPDList();
            var data = (from ipd in ipdList
                        select new
                        {
                            PatientId = "RCH-" + ipd.PatientId,
                            IpdNo = ipd.IpdNo,
                            PatientName = ipd.PatientName,
                            IPDStatus = ipd.IPDStatus,
                            IPDStatusDate = ipd.IPDStatusDate,
                            FatherOrHusbandName = ipd.FatherOrHusbandName,
                            Gender = ipd.Gender,
                            MobileNumber = ipd.MobileNumber,
                            Address = ipd.Address,
                            AdmittedDateTime = ipd.AdmittedDateTime,
                            Age = ipd.Age,
                            AreaName = ipd.AreaName,
                            DepartmentName = ipd.DepartmentName,
                            TreatmentName = ipd.TreatmentName,
                            IDorAadharNumber = ipd.IDorAadharNumber,
                            IDNumber = ipd.IDNumber,
                            MalariaStatus = ipd.MalariaStatus,
                            RapidKitNS1Status = ipd.RapidKitNS1Status,
                            RapidKitIGMStatus = ipd.RapidKitIGMStatus,
                            ELISANS1Status = ipd.ELISANS1Status,
                            ELISAIGMStatus = ipd.ELISAIGMStatus,
                            ELISAScrubTyphusStatus = ipd.ELISAScrubTyphusStatus,
                            ELISALeptospiraStatus = ipd.ELISALeptospiraStatus,
                        }).ToList();
            var products = new System.Data.DataTable();
            products = ListtoDataTable.ToDataTable(data);
            var grid = new GridView();
            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=IPD_Patient_List_" + DateTime.Now.Date.ToString() + ".xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("IPDList");
        }
        public ActionResult IPDListExportToPDF()
        {
            var _details = new AdminDetails();
            var ipdList = _details.GetIPDList();
            var products = new System.Data.DataTable();
            products = ListtoDataTable.ToDataTable(ipdList);
            var grid = new GridView();
            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=IPD_Patient_List_" + DateTime.Now.Date.ToString() + ".pdf");
            Response.ContentType = "application/pdf";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("IPDList");
        }
        public ActionResult IPDListExportToCSV()
        {
            var _details = new AdminDetails();
            var ipdList = _details.GetIPDList();
            var products = new System.Data.DataTable();
            products = ListtoDataTable.ToDataTable(ipdList);
            var grid = new GridView();
            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=IPD_Patient_List_" + DateTime.Now.Date.ToString() + ".csv");
            Response.ContentType = "text/csv";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("IPDList");
        }
        public ActionResult IpdDashboard()
        {
            var _details = new AdminDetails();
            ViewData["PageData"] = _details.GetDashboardData();
            ViewData["IPDData"] = _details.GetIPDList().Where(x => x.AdmittedDate.Value.Date == DateTime.Now.Date).ToList();
            return View();
        }
        public ActionResult AdminDashboard()
        {
            return View();
        }
        public ActionResult AddUser()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult AddTreatment()
        {
            var _details = new MasterDetails();
            ViewData["PageData"] = _details.GetTreatment();
            return View();
        }
        public JsonResult GetTreatmentDetail(int TreatmentId)
        {
            var _details = new MasterDetails();
            return Json(_details.GetTreatmentById(TreatmentId), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveTreatmentMaster(string TreatmentId, string treatmentname, string treatmentdescription)
        {
            Treatment pages = new Treatment();
            pages.TreatmentID = !string.IsNullOrEmpty(TreatmentId) ? Convert.ToInt32(TreatmentId) : 0;
            pages.TreatmentName = treatmentname;
            pages.Description = treatmentdescription;
            AdminDetails _details = new AdminDetails();
            var result = _details.AddUpdateTreatment(pages);
            if (result == Enums.CrudStatus.Saved)
            {
                if (string.IsNullOrEmpty(TreatmentId))
                    SetAlertMessage("Treatment created", "Save Treatment");
                else
                    SetAlertMessage("Treatment updated", "Save Treatment");
            }
            else
                SetAlertMessage("Treatment creation failed", "Save Treatment");
            return RedirectToAction("AddTreatment");

        }
        public ActionResult AddDepartment()
        {
            var _details = new MasterDetails();
            ViewData["PageData"] = _details.DepartmentList();
            return View();
        }
        public JsonResult GetDepartmentDetail(int DepartmentId)
        {
            var _details = new MasterDetails();
            return Json(_details.GetDeparmentById(DepartmentId), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveDepartmentMaster(string DepartmentId, string departmentname)
        {
            Department pages = new Department();
            pages.DepartmentID = !string.IsNullOrEmpty(DepartmentId) ? Convert.ToInt32(DepartmentId) : 0;
            pages.DepartmentName = departmentname;
            AdminDetails _details = new AdminDetails();
            var result = _details.AddUpdateDepartment(pages);
            if (result == Enums.CrudStatus.Saved)
            {
                if (string.IsNullOrEmpty(DepartmentId))
                    SetAlertMessage("Department created", "Save Department");
                else
                    SetAlertMessage("Department updated", "Save Department");
            }
            else
                SetAlertMessage("Department creation failed", "Save Department");
            return RedirectToAction("AddDepartment");

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
                if (string.IsNullOrEmpty(AreaId))
                    SetAlertMessage("Area created", "Save Area");
                else
                    SetAlertMessage("Area updated", "Save Area");
            }
            else
                SetAlertMessage("Area creation failed", "Save Area");
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
                PatientId = model.PatientId,
                CreatedBy = UserData.UserId,
                CreatedDate = DateTime.Now,
                ELISAIGM_Status = model.ELISAIGMStatus,
                //ELISAIGM_TestDate = Convert.ToDateTime(model.ELISAIGMDate),
                ELISANS1_Status = model.ELISANS1Status,
                //ELISANS1_TestDate = Convert.ToDateTime(model.ELISANS1Date),
                ELISALaptospira_Status = model.ELISALeptospiraStatus,
                //ELISALaptospira_TestDate = Convert.ToDateTime(model.ELISALeptospiraDate),
                ELISAScrubTyphus_Status = model.ELISAScrubTyphusStatus,
                //ELISAScrubTyphus_TestDate = Convert.ToDateTime(model.ELISAScrubTyphusDate),
                RapidKitNS1_Status = model.RapidKitNS1Status,
                //RapidKitNS1_TestDate = Convert.ToDateTime(model.RapidKitNS1Date),
                RapidKitIGM_Status = model.RapidKitIGMStatus,
                //RapidKitIGM_TestDate = Convert.ToDateTime(model.RapidKitIGMDate),
                LFT_Details = model.LFT,
                KFT_Details = model.kft,
                RandomDonerPlatelet_Count = model.BTRandomDonerPlatelet,
                //RandomDonerPlatelet_TestDate = Convert.ToDateTime(model.BTRandomDonerPlateletDate),
                SingleDonorPlatelet_Count = model.BTSingleDonerPlatelet,
                //SingleDonorPlatelet_TestDate = Convert.ToDateTime(model.BTSingleDonerPlateletDate),
                WholeBloodCell_Count = model.WholeBloodCell,
                //WholeBloodCell_TestDate = Convert.ToDateTime(model.WholeBloodCellDate),
                PackedRBC_Count = model.PackedRBC,
                //PackedRBC_TestDate = Convert.ToDateTime(model.PackedRBCDate),
                MalariaParasite_Status = model.MalariaStatus,
                //MalariaParasite_TestDate = Convert.ToDateTime(model.malariadate),
                PlateletCount = model.Platelet,
                HbCount = model.Hb
            };
            if (!string.IsNullOrEmpty(model.ELISAIGMDate))
                report.ELISAIGM_TestDate = Convert.ToDateTime(model.ELISAIGMDate);
            if (!string.IsNullOrEmpty(model.ELISANS1Date))
                report.ELISANS1_TestDate = Convert.ToDateTime(model.ELISANS1Date);
            if (!string.IsNullOrEmpty(model.ELISALeptospiraDate))
                report.ELISALaptospira_TestDate = Convert.ToDateTime(model.ELISALeptospiraDate);
            if (!string.IsNullOrEmpty(model.ELISAScrubTyphusDate))
                report.ELISAScrubTyphus_TestDate = Convert.ToDateTime(model.ELISAScrubTyphusDate);
            if (!string.IsNullOrEmpty(model.RapidKitNS1Date))
                report.RapidKitNS1_TestDate = Convert.ToDateTime(model.RapidKitNS1Date);
            if (!string.IsNullOrEmpty(model.RapidKitIGMDate))
                report.RapidKitIGM_TestDate = Convert.ToDateTime(model.RapidKitIGMDate);
            if (!string.IsNullOrEmpty(model.BTRandomDonerPlateletDate))
                report.RandomDonerPlatelet_TestDate = Convert.ToDateTime(model.BTRandomDonerPlateletDate);
            if (!string.IsNullOrEmpty(model.BTSingleDonerPlateletDate))
                report.SingleDonorPlatelet_TestDate = Convert.ToDateTime(model.BTSingleDonerPlateletDate);
            if (!string.IsNullOrEmpty(model.WholeBloodCellDate))
                report.WholeBloodCell_TestDate = Convert.ToDateTime(model.WholeBloodCellDate);
            if (!string.IsNullOrEmpty(model.PackedRBCDate))
                report.PackedRBC_TestDate = Convert.ToDateTime(model.PackedRBCDate);
            if (!string.IsNullOrEmpty(model.malariadate))
                report.MalariaParasite_TestDate = Convert.ToDateTime(model.malariadate);
            var result = _details.SaveIPDLabReport(report);
            return Json("Save", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveDiagnosisReport(DiagnosisReportModel model)
        {
            AdminDetails _details = new AdminDetails();
            IpdRadioDiagnosisReport report = new IpdRadioDiagnosisReport()
            {
                PatientId = model.PatientId,
                CreatedBy = UserData.UserId,
                CreatedDate = DateTime.Now,
                USG_Details = model.USG_Details,
                Xray_Details = model.Xray_Details
            };
            var result = _details.SaveDiagnosisReport(report);
            return Json("Save", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveIPDStatus(IPDStatusModel model)
        {
            AdminDetails _details = new AdminDetails();
            IpdPatientStatu report = new IpdPatientStatu()
            {
                PatientId = model.PatientId,
                IPDStatus = model.IPDStatus
            };
            if (model.IPDStatus == "Admit")
            {
                if (!string.IsNullOrWhiteSpace(model.AdmittedDateTime))
                    report.AdmittedDateTime = Convert.ToDateTime(model.AdmittedDateTime);
            }
            else if (model.IPDStatus == "Discharge")
            {
                if (!string.IsNullOrWhiteSpace(model.AdmittedDateTime))
                    report.DischargeDateTime = Convert.ToDateTime(model.AdmittedDateTime);
            }
            else if (model.IPDStatus == "Refer")
            {
                if (!string.IsNullOrWhiteSpace(model.AdmittedDateTime))
                    report.ReferDateTime = Convert.ToDateTime(model.AdmittedDateTime);
                report.ReferReasons = model.Reason;
                report.ReferCaseSummary = model.casesummary;
            }
            else if (model.IPDStatus == "LAMA")
            {
                if (!string.IsNullOrWhiteSpace(model.AdmittedDateTime))
                    report.LAMADateTime = Convert.ToDateTime(model.AdmittedDateTime);
                report.LAMAReasons = model.Reason;
                report.LAMACaseSummary = model.casesummary;
            }
            else if (model.IPDStatus == "DOPR")
            {
                if (!string.IsNullOrWhiteSpace(model.AdmittedDateTime))
                    report.DOPRDateTime = Convert.ToDateTime(model.AdmittedDateTime);
                report.DOPRReasons = model.Reason;
                report.DOPRCaseSummary = model.casesummary;
            }
            else if (model.IPDStatus == "Death")
            {
                if (!string.IsNullOrWhiteSpace(model.AdmittedDateTime))
                    report.DeathDateTime = Convert.ToDateTime(model.AdmittedDateTime);
                report.DeathReasons = model.Reason;
                report.DeathCaseSummary = model.casesummary;
            }
            else if (model.IPDStatus == "Abscond")
            {
                if (!string.IsNullOrWhiteSpace(model.AdmittedDateTime))
                    report.AbscondDateTime = Convert.ToDateTime(model.AdmittedDateTime);
                report.AbscondReasons = model.Reason;
                report.AbscondCaseSummary = model.casesummary;
            }
            else if (model.IPDStatus == "Other")
            {
                if (!string.IsNullOrWhiteSpace(model.AdmittedDateTime))
                    report.OtherDateTime = Convert.ToDateTime(model.AdmittedDateTime);
                report.OtherReasons = model.Reason;
            }

            var result = _details.SaveIPDStatus(report);
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
        //[HttpPost]
        //[ValidateInput(false)]
        //public FileResult ExportIPDList(string GridHtml)
        //{
        //    return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "IPDList.xls");
        //}
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
        [HttpPost]
        public JsonResult DeletePatient(int patientId)
        {
            AdminDetails _details = new AdminDetails();
            return Json(_details.DeletePatient(patientId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPatientDetailByPatientId(int patientId)
        {
            AdminDetails _details = new AdminDetails();
            var patientInfo = _details.GetPatientDetailByPatientId(patientId);
            return Json(patientInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BarChart(ReportRequestModel model)
        {

            AdminDetails _details = new AdminDetails();
            return Json(_details.GetIPDChartDetail(model.reportStartDate), JsonRequestBehavior.AllowGet);
        }
    }

    public class ReportRequestModel
    {
        public string reportStartDate { get; set; }
    }
}