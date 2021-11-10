using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIMS_Web.Models.Masters
{
    public class DepartmentModel
    {
        public int DepartmentId { get; set; }
        public string DeparmentName { get; set; }
        public string DepartmentUrl { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public byte[] Image { get; set; }
    }
    public class TreatmentModel
    {
        public int TreatmentId { get; set; }
        public string TreatmentName { get; set; }
        public string TreatmentDescription { get; set; }
    }
    public class StateModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
    }
    public class CityModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
    }
    public class AreaModel
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int? WardId { get; set; }
        public string WardName { get; set; }
    }
    public class DashboardDataModel
    {
        public int PatientTreated { get; set; }
        public int AdmitPatient { get; set; }
        public int DischargePatient { get; set; }
        public int Refer { get; set; }
        public int Abscond { get; set; }
        public int LAMA { get; set; }
        public int DOPR { get; set; }
        public int Death { get; set; }
        public int Other { get; set; }
    }
    public class IpdPatientLabReportModel
    {
        public int Id { get; set; }
        public Nullable<int> PatientId { get; set; }
        public string HbCount { get; set; }
        public string PlateletCount { get; set; }
        public string MalariaParasite_Status { get; set; }
        public Nullable<System.DateTime> MalariaParasite_TestDate { get; set; }
        public string RapidKitNS1_Status { get; set; }
        public Nullable<System.DateTime> RapidKitNS1_TestDate { get; set; }
        public string RapidKitIGM_Status { get; set; }
        public Nullable<System.DateTime> RapidKitIGM_TestDate { get; set; }
        public string ELISANS1_Status { get; set; }
        public Nullable<System.DateTime> ELISANS1_TestDate { get; set; }
        public string ELISAIGM_Status { get; set; }
        public Nullable<System.DateTime> ELISAIGM_TestDate { get; set; }
        public string ELISAScrubTyphus_Status { get; set; }
        public Nullable<System.DateTime> ELISAScrubTyphus_TestDate { get; set; }
        public string ELISALaptospira_Status { get; set; }
        public Nullable<System.DateTime> ELISALaptospira_TestDate { get; set; }
        public string LFT_Details { get; set; }
        public string KFT_Details { get; set; }
        public string RandomDonerPlatelet_Count { get; set; }
        public Nullable<System.DateTime> RandomDonerPlatelet_TestDate { get; set; }
        public string SingleDonorPlatelet_Count { get; set; }
        public Nullable<System.DateTime> SingleDonorPlatelet_TestDate { get; set; }
        public string WholeBloodCell_Count { get; set; }
        public Nullable<System.DateTime> WholeBloodCell_TestDate { get; set; }
        public string PackedRBC_Count { get; set; }
        public Nullable<System.DateTime> PackedRBC_TestDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    }
    public class IpdRadioDiagnosisReportModel
    {
        public int Id { get; set; }
        public Nullable<int> PatientId { get; set; }
        public string Xray_Details { get; set; }
        public string USG_Details { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
    }
    public class IpdPatientStatusModel
    {
        public int Id { get; set; }
        public Nullable<int> PatientId { get; set; }
        public string IPDStatus { get; set; }
        public Nullable<System.DateTime> AdmittedDateTime { get; set; }
        public Nullable<System.DateTime> DischargeDateTime { get; set; }
        public Nullable<System.DateTime> ReferDateTime { get; set; }
        public string ReferReasons { get; set; }
        public string ReferCaseSummary { get; set; }
        public Nullable<System.DateTime> LAMADateTime { get; set; }
        public string LAMAReasons { get; set; }
        public string LAMACaseSummary { get; set; }
        public Nullable<System.DateTime> DOPRDateTime { get; set; }
        public string DOPRReasons { get; set; }
        public string DOPRCaseSummary { get; set; }
        public Nullable<System.DateTime> DeathDateTime { get; set; }
        public string DeathReasons { get; set; }
        public string DeathCaseSummary { get; set; }
        public Nullable<System.DateTime> AbscondDateTime { get; set; }
        public string AbscondReasons { get; set; }
        public string AbscondCaseSummary { get; set; }
        public Nullable<System.DateTime> OtherDateTime { get; set; }
        public string OtherReasons { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }

    }

    public class HIMSUserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string Password { get; set; }
    }

    public class BarChartModel
    {
        public int Admit { get; set; }
        public int Discharge { get; set; }
        public int Refer { get; set; }
        public string MonthYr { get; set; }
    }
    public class IPDPatientDetailModel
    {
        public IpdPatientLabReportModel LabReport { get; set; }
        public IpdRadioDiagnosisReportModel DiagnosisReport { get; set; }
        public IpdPatientStatusModel PatientStatus { get; set; }
        public string userRole { get; set; }
    }
    public class IpdPatientInfoModel
    {
        public int PatientId { get; set; }
        public string IpdNo { get; set; }
        public DateTime? AdmittedDate { get; set; }
        public string AdmittedDateTime { get; set; }
        public string TreatmentIds { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string PatientName { get; set; }
        public string MaritalStatus { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<int> PinCode { get; set; }
        public string Religion { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> StateId { get; set; }
        public byte[] Photo { get; set; }
        public string FatherOrHusbandName { get; set; }
        public Nullable<System.DateTime> ValidUpto { get; set; }
        public string IDorAadharNumber { get; set; }
        public string IDNumber { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string RenewalStatusIPD { get; set; }
        public string RegistrationStatusIPD { get; set; }
        public Nullable<int> Age_Year { get; set; }
        public Nullable<int> Age_Month { get; set; }
        public string IPDStatus { get; set; }
        public string AreaName { get; set; }
        public string TreatmentName { get; set; }
        public string DepartmentName { get; set; }
        public string MalariaStatus { get; set; }
        public string RapidKitNS1Status { get; set; }
        public string RapidKitIGMStatus { get; set; }
        public string ELISANS1Status { get; set; }
        public string ELISAIGMStatus { get; set; }
        public string ELISAScrubTyphusStatus { get; set; }
        public string ELISALeptospiraStatus { get; set; }
        public string DischargeDateTime { get; set; }
        public string ReferDateTime { get; set; }
        public string LAMADateTime { get; set; }
        public string DOPRDateTime { get; set; }
        public string DeathDateTime { get; set; }
        public string AbscondDateTime { get; set; }
        public string OtherDateTime { get; set; }
        public string IPDStatusDate { get; set; }
        public string HBCount { get; set; }
        public string PlateletCount { get; set; }
    }

    public class IPDSearchPatientDetail : IpdPatientInfoModel
    {
        public string HbCount { get; set; }
        public string PlateletCount { get; set; }
        public string LFT_Details { get; set; }
        public string KFT_Details { get; set; }
        public string RandomDonerPlatelet_Count { get; set; }
        public string RandomDonerPlatelet_TestDate { get; set; }
        public string WholeBloodCell_Count { get; set; }
        public string WholeBloodCell_TestDate { get; set; }
        public string PackedRBC_Count { get; set; }
        public string PackedRBC_TestDate { get; set; }
        public string Xray_Details { get; set; }
        public string USG_Details { get; set; }

        public string MalariaParasite_TestDate { get; set; }
        public string RapidKitNS1_TestDate { get; set; }
        public string RapidKitIGM_TestDate { get; set; }
        public string ELISANS1_TestDate { get; set; }
        public string ELISAIGM_TestDate { get; set; }
        public string ELISAScrubTyphus_TestDate { get; set; }
        public string ELISALaptospira_TestDate { get; set; }
    }

    public class IpdLabReportModel
    {
        public int PatientId { get; set; }
        public string Hb { get; set; }
        public string Platelet { get; set; }
        public string MalariaStatus { get; set; }
        public string malariadate { get; set; }
        public string RapidKitNS1Status { get; set; }
        public string RapidKitNS1Date { get; set; }
        public string RapidKitIGMStatus { get; set; }
        public string RapidKitIGMDate { get; set; }
        public string ELISANS1Status { get; set; }
        public string ELISANS1Date { get; set; }
        public string ELISAIGMStatus { get; set; }
        public string ELISAIGMDate { get; set; }
        public string ELISAScrubTyphusStatus { get; set; }
        public string ELISAScrubTyphusDate { get; set; }
        public string ELISALeptospiraStatus { get; set; }
        public string ELISALeptospiraDate { get; set; }
        public string LFT { get; set; }
        public string kft { get; set; }
        public string BTRandomDonerPlatelet { get; set; }
        public string BTRandomDonerPlateletDate { get; set; }
        public string BTSingleDonerPlatelet { get; set; }
        public string BTSingleDonerPlateletDate { get; set; }
        public string WholeBloodCell { get; set; }
        public string WholeBloodCellDate { get; set; }
        public string PackedRBC { get; set; }
        public string PackedRBCDate { get; set; }

    }

    public class DiagnosisReportModel
    {
        public int PatientId { get; set; }
        public string Xray_Details { get; set; }
        public string USG_Details { get; set; }
    }
    public class IPDStatusModel
    {
        public int PatientId { get; set; }
        public string IPDStatus { get; set; }
        public string AdmittedDateTime { get; set; }
        public string Reason { get; set; }
        public string casesummary { get; set; }

    }

    public class DoctorModel
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Designation { get; set; }
        public string Degree { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public byte[] Image { get; set; }
    }
    public class DoctorTypeModel
    {
        public int Id { get; set; }
        public string DoctorType { get; set; }
    }
    public class MasterLookupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}