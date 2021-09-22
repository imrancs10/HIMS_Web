﻿using System;
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
    public class IpdPatientInfoModel
    {
        public int PatientId { get; set; }
        public string IpdNo { get; set; }
        public Nullable<System.DateTime> AdmittedDateTime { get; set; }
        public Nullable<int> TreatmentId { get; set; }
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
        public Nullable<int> Age { get; set; }
        public string IPDStatus { get; set; }
        public string AreaName { get; set; }
        public string TreatmentName { get; set; }
        public string DepartmentName { get; set; }
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