﻿//using HIMS_Web.BAL.Commom;
//using HIMS_Web.BAL.Masters;
//using HIMS_Web.BAL.Reports;
using DataLayer;
using HIMS_Web.BAL.Masters;
using HIMS_Web.Global;
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
            string Treatment, string Address, string Area, string OtherAreaName, string pincode,
            string department, string IDorAadharNumber, int Age)
        //string Title,string Email, string DOB, string MariatalStatus, string state, string city,string religion,
        {
            IpdPatientInfo ipdInfo = new IpdPatientInfo()
            {
                Address = Address,
                AdmittedDateTime = Convert.ToDateTime(AdmittedDateTime),
                AreaId = Convert.ToInt32(Area),
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
                PinCode = Convert.ToInt32(pincode),
                //Religion = religion,
                //StateId = Convert.ToInt32(state),
                //Title = Title,
                TreatmentId = Convert.ToInt32(Treatment),
                Age = Age
            };

            AdminDetails _details = new AdminDetails();
            var result = _details.SaveIPDEntry(ipdInfo);

            if (!string.IsNullOrEmpty(OtherAreaName))
            {
                var areaModel = new Area()
                {
                    AreaName = OtherAreaName,
                    CityId = 1318
                };
                var resultArea = _details.SaveArea(areaModel);
            }

            if (result == Enums.CrudStatus.Saved)
                SetAlertMessage("IPD Patient Info saved", "IPD Patient Info");
            else
                SetAlertMessage("IPD Patient Info saved failed", "IPD Patient Info");
            return RedirectToAction("IPDEntry");
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