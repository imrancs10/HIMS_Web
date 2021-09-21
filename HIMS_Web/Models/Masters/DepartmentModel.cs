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