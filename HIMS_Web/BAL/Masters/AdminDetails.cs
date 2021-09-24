﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using HIMS_Web.Global;
using System.Data.Entity;
using HIMS_Web.Models.Masters;
using System.Data.Entity.Validation;

namespace HIMS_Web.BAL.Masters
{
    public class AdminDetails
    {
        HIMSDBEntities _db = null;
        public Enums.CrudStatus SaveIPDEntry(IpdPatientInfo model)
        {
            _db = new HIMSDBEntities();
            int _effectRow = 0;
            var _deptRow = _db.IpdPatientInfoes.Where(x => x.PatientId.Equals(model.PatientId)).FirstOrDefault();
            if (_deptRow == null)
            {
                _db.Entry(model).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
                return Enums.CrudStatus.DataAlreadyExist;
        }
        public List<IpdPatientInfoModel> GetIPDList()
        {
            _db = new HIMSDBEntities();
            var _list = (from dept in _db.IpdPatientInfoes
                         join treat in _db.Treatments on dept.TreatmentId equals treat.TreatmentID
                         join area in _db.Areas on dept.AreaId equals area.AreaId
                         join deptarment in _db.Departments on dept.DepartmentId equals deptarment.DepartmentID
                         select new
                         {
                             Address = dept.Address,
                             AdmittedDateTime = dept.AdmittedDateTime,
                             Age = dept.Age,
                             FatherOrHusbandName = dept.FatherOrHusbandName,
                             Gender = dept.Gender,
                             IDNumber = dept.IDNumber,
                             IDorAadharNumber = dept.IDorAadharNumber,
                             IpdNo = dept.IpdNo,
                             IPDStatus = dept.IPDStatus,
                             MobileNumber = dept.MobileNumber,
                             PatientId = dept.PatientId,
                             PatientName = dept.PatientName,
                             AreaId = dept.AreaId,
                             DepartmentId = dept.DepartmentId,
                             TreatmentId = dept.TreatmentId,
                             AreaName = area != null ? area.AreaName : "",
                             DepartmentName = deptarment != null ? deptarment.DepartmentName : "",
                             TreatmentName = treat != null ? treat.TreatmentName : "",
                         }).ToList().Select(x => new IpdPatientInfoModel
                         {
                             Address = x.Address,
                             AdmittedDateTime = x.AdmittedDateTime != null ? x.AdmittedDateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                             Age = x.Age,
                             FatherOrHusbandName = x.FatherOrHusbandName,
                             Gender = x.Gender,
                             IDNumber = x.IDNumber,
                             IDorAadharNumber = x.IDorAadharNumber,
                             IpdNo = x.IpdNo,
                             IPDStatus = x.IPDStatus,
                             MobileNumber = x.MobileNumber,
                             PatientId = x.PatientId,
                             PatientName = x.PatientName,
                             AreaId = x.AreaId,
                             DepartmentId = x.DepartmentId,
                             TreatmentId = x.TreatmentId,
                             AreaName = x.AreaName,
                             DepartmentName = x.DepartmentName,
                             TreatmentName = x.TreatmentName,
                         }).ToList();
            return _list != null ? _list : new List<IpdPatientInfoModel>();
        }

        public List<IpdPatientInfoModel> GetIPDDetail(string searchText)
        {
            _db = new HIMSDBEntities();
            var _list = (from dept in _db.IpdPatientInfoes
                         join treat in _db.Treatments on dept.TreatmentId equals treat.TreatmentID
                         join area in _db.Areas on dept.AreaId equals area.AreaId
                         join deptarment in _db.Departments on dept.DepartmentId equals deptarment.DepartmentID
                         where dept.IpdNo.Contains(searchText) || dept.PatientName.Contains(searchText)
                         select new
                         {
                             Address = dept.Address,
                             AdmittedDateTime = dept.AdmittedDateTime,
                             Age = dept.Age,
                             FatherOrHusbandName = dept.FatherOrHusbandName,
                             Gender = dept.Gender,
                             IDNumber = dept.IDNumber,
                             IDorAadharNumber = dept.IDorAadharNumber,
                             IpdNo = dept.IpdNo,
                             IPDStatus = dept.IPDStatus,
                             MobileNumber = dept.MobileNumber,
                             PatientId = dept.PatientId,
                             PatientName = dept.PatientName,
                             AreaId = dept.AreaId,
                             DepartmentId = dept.DepartmentId,
                             TreatmentId = dept.TreatmentId,
                             AreaName = area != null ? area.AreaName : "",
                             DepartmentName = deptarment != null ? deptarment.DepartmentName : "",
                             TreatmentName = treat != null ? treat.TreatmentName : "",
                         }).ToList().Select(x => new IpdPatientInfoModel
                         {
                             Address = x.Address,
                             AdmittedDateTime = x.AdmittedDateTime != null ? x.AdmittedDateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                             Age = x.Age,
                             FatherOrHusbandName = x.FatherOrHusbandName,
                             Gender = x.Gender,
                             IDNumber = x.IDNumber,
                             IDorAadharNumber = x.IDorAadharNumber,
                             IpdNo = x.IpdNo,
                             IPDStatus = x.IPDStatus,
                             MobileNumber = x.MobileNumber,
                             PatientId = x.PatientId,
                             PatientName = x.PatientName,
                             AreaId = x.AreaId,
                             DepartmentId = x.DepartmentId,
                             TreatmentId = x.TreatmentId,
                             AreaName = x.AreaName,
                             DepartmentName = x.DepartmentName,
                             TreatmentName = x.TreatmentName,
                         }).ToList();
            return _list != null ? _list : new List<IpdPatientInfoModel>();
        }
        public int SaveDepartment(Department model)
        {
            _db = new HIMSDBEntities();
            int _effectRow = 0;
            var _deptRow = _db.Departments.Where(x => x.DepartmentName.Equals(model.DepartmentName)).FirstOrDefault();
            if (_deptRow == null)
            {
                _db.Entry(model).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? model.DepartmentID : 0;
            }
            else
                return _deptRow.DepartmentID;
        }
        public Enums.CrudStatus AddUpdateDepartment(Department model)
        {
            try
            {
                _db = new HIMSDBEntities();
                int _effectRow = 0;
                if (model.DepartmentID > 0)
                {
                    var pages = _db.Departments.Where(x => x.DepartmentID == model.DepartmentID).FirstOrDefault();
                    if (pages != null)
                    {
                        pages.DepartmentName = model.DepartmentName;
                        pages.DepartmentUrl = model.DepartmentUrl;
                        pages.Image = model.Image;
                        _db.Entry(pages).State = EntityState.Modified;
                        _effectRow = _db.SaveChanges();
                    }
                }
                else
                {
                    _db.Entry(model).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                }

                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        //Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(e));
                    }
                }
                return Enums.CrudStatus.InternalError;
            }
        }
        public int SaveArea(Area model)
        {
            _db = new HIMSDBEntities();
            int _effectRow = 0;
            var _deptRow = _db.Areas.Where(x => x.AreaName.Equals(model.AreaName)).FirstOrDefault();
            if (_deptRow == null)
            {
                _db.Entry(model).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? model.AreaId : 0;
            }
            else
                return _deptRow.AreaId;
        }
        public Enums.CrudStatus AddUpdateArea(Area model)
        {
            try
            {
                _db = new HIMSDBEntities();
                int _effectRow = 0;
                if (model.AreaId > 0)
                {
                    var pages = _db.Areas.Where(x => x.AreaId == model.AreaId).FirstOrDefault();
                    if (pages != null)
                    {
                        pages.AreaName = model.AreaName;
                        pages.CityId = model.CityId;
                        _db.Entry(pages).State = EntityState.Modified;
                        _effectRow = _db.SaveChanges();
                    }
                }
                else
                {
                    _db.Entry(model).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                }

                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        //Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(e));
                    }
                }
                return Enums.CrudStatus.InternalError;
            }
        }
        public int SaveTreatment(Treatment model)
        {
            _db = new HIMSDBEntities();
            int _effectRow = 0;
            var _deptRow = _db.Treatments.Where(x => x.TreatmentName.Equals(model.TreatmentName)).FirstOrDefault();
            if (_deptRow == null)
            {
                _db.Entry(model).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? model.TreatmentID : 0;
            }
            else
                return _deptRow.TreatmentID;
        }
        public Enums.CrudStatus AddUpdateTreatment(Treatment model)
        {
            try
            {
                _db = new HIMSDBEntities();
                int _effectRow = 0;
                if (model.TreatmentID > 0)
                {
                    var pages = _db.Treatments.Where(x => x.TreatmentID == model.TreatmentID).FirstOrDefault();
                    if (pages != null)
                    {
                        pages.TreatmentName = model.TreatmentName;
                        //pages.DepartmentUrl = model.DepartmentUrl;
                        //pages.Image = model.Image;
                        _db.Entry(pages).State = EntityState.Modified;
                        _effectRow = _db.SaveChanges();
                    }
                }
                else
                {
                    _db.Entry(model).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                }

                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        //Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(e));
                    }
                }
                return Enums.CrudStatus.InternalError;
            }
        }
        public Enums.CrudStatus SaveIPDLabReport(IpdPatientLabReport report)
        {
            try
            {
                _db = new HIMSDBEntities();
                int _effectRow = 0;
                if (report.Id > 0)
                {
                    var pages = _db.IpdPatientLabReports.Where(x => x.Id == report.Id).FirstOrDefault();
                    if (pages != null)
                    {
                        //pages.AreaName = model.AreaName;
                        //pages.CityId = model.CityId;
                        //_db.Entry(pages).State = EntityState.Modified;
                        //_effectRow = _db.SaveChanges();
                    }
                }
                else
                {
                    _db.Entry(report).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                }

                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        //Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(e));
                    }
                }
                return Enums.CrudStatus.InternalError;
            }
        }

        public Enums.CrudStatus SaveDiagnosisReport(IpdRadioDiagnosisReport report)
        {
            try
            {
                _db = new HIMSDBEntities();
                int _effectRow = 0;
                if (report.Id > 0)
                {
                    var pages = _db.IpdRadioDiagnosisReports.Where(x => x.PatientId == report.PatientId).FirstOrDefault();
                    if (pages != null)
                    {
                        //pages.AreaName = model.AreaName;
                        //pages.CityId = model.CityId;
                        //_db.Entry(pages).State = EntityState.Modified;
                        //_effectRow = _db.SaveChanges();
                    }
                }
                else
                {
                    _db.Entry(report).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                }

                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        //Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(e));
                    }
                }
                return Enums.CrudStatus.InternalError;
            }
        }
        public Enums.CrudStatus SaveIPDStatus(IpdPatientStatu report)
        {
            try
            {
                _db = new HIMSDBEntities();
                int _effectRow = 0;
                var pages = _db.IpdPatientStatus.Where(x => x.PatientId == report.PatientId).FirstOrDefault();
                if (pages != null)
                {
                    pages.AbscondCaseSummary = !string.IsNullOrEmpty(report.AbscondCaseSummary) ? report.AbscondCaseSummary : pages.AbscondCaseSummary;
                    pages.AbscondDateTime = report.AbscondDateTime != null ? report.AbscondDateTime : pages.AbscondDateTime;
                    pages.AbscondReasons = !string.IsNullOrEmpty(report.AbscondReasons) ? report.AbscondReasons : pages.AbscondReasons;
                    pages.AdmittedDateTime = report.AdmittedDateTime != null ? report.AdmittedDateTime : pages.AdmittedDateTime;
                    pages.DeathCaseSummary = !string.IsNullOrEmpty(report.DeathCaseSummary) ? report.DeathCaseSummary : pages.DeathCaseSummary;
                    pages.DeathDateTime = report.DeathDateTime != null ? report.DeathDateTime : pages.DeathDateTime;
                    pages.DeathReasons = !string.IsNullOrEmpty(report.DeathReasons) ? report.DeathReasons : pages.DeathReasons;
                    pages.DischargeDateTime = report.DischargeDateTime != null ? report.DischargeDateTime : pages.DischargeDateTime;
                    pages.DOPRCaseSummary = !string.IsNullOrEmpty(report.DOPRCaseSummary) ? report.DOPRCaseSummary : pages.DOPRCaseSummary;
                    pages.DOPRDateTime = report.DOPRDateTime != null ? report.DOPRDateTime : pages.DOPRDateTime;
                    pages.DOPRReasons = !string.IsNullOrEmpty(report.DOPRReasons) ? report.DOPRReasons : pages.DOPRReasons;
                    pages.IPDStatus = !string.IsNullOrEmpty(report.IPDStatus) ? report.IPDStatus : pages.IPDStatus;
                    pages.LAMACaseSummary = !string.IsNullOrEmpty(report.LAMACaseSummary) ? report.LAMACaseSummary : pages.LAMACaseSummary;
                    pages.LAMADateTime = report.LAMADateTime != null ? report.LAMADateTime : pages.LAMADateTime;
                    pages.LAMAReasons = !string.IsNullOrEmpty(report.LAMAReasons) ? report.LAMAReasons : pages.LAMAReasons;
                    pages.OtherDateTime = report.OtherDateTime != null ? report.OtherDateTime : pages.OtherDateTime;
                    pages.OtherReasons = !string.IsNullOrEmpty(report.OtherReasons) ? report.OtherReasons : pages.OtherReasons;
                    pages.ReferCaseSummary = !string.IsNullOrEmpty(report.ReferCaseSummary) ? report.ReferCaseSummary : pages.ReferCaseSummary;
                    pages.ReferDateTime = report.ReferDateTime != null ? report.ReferDateTime : pages.ReferDateTime;
                    pages.ReferReasons = !string.IsNullOrEmpty(report.ReferReasons) ? report.ReferReasons : pages.ReferReasons;
                    _db.Entry(pages).State = EntityState.Modified;
                    _effectRow = _db.SaveChanges();
                }
                else
                {
                    _db.Entry(report).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                }
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        //Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(e));
                    }
                }
                return Enums.CrudStatus.InternalError;
            }
        }

        //public Enums.CrudStatus EditSchedule(ScheduleModel model)
        //{
        //    _db = new HIMSDBEntities();
        //    int _effectRow = 0;
        //    var _docRow = _db.DoctorSchedules.Where(x => x.DoctorScheduleID.Equals(model.ScheduleId)).FirstOrDefault();
        //    if (_docRow != null)
        //    {
        //        _docRow.DayID = model.DayId;
        //        _docRow.DoctorID = model.DoctorId;
        //        _docRow.TimeFrom = model.TimeFrom;
        //        _docRow.TimeFromMeridiemID = model.TimeFromMeridiumId;
        //        _docRow.TimeTo = model.TimeTo;
        //        _docRow.TimeToMeridiemID = model.TimeToMeridiumId;
        //        _db.Entry(_docRow).State = EntityState.Modified;
        //        _effectRow = _db.SaveChanges();
        //        return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
        //    }
        //    else
        //        return Enums.CrudStatus.DataNotFound;
        //}
        //public Enums.CrudStatus DeleteSchedule(int docId)
        //{
        //    _db = new HIMSDBEntities();
        //    int _effectRow = 0;
        //    var _docRow = _db.DoctorSchedules.Where(x => x.DoctorScheduleID.Equals(docId)).FirstOrDefault();
        //    if (_docRow != null)
        //    {
        //        _db.DoctorSchedules.Remove(_docRow);
        //        //_db.Entry(_deptRow).State = EntityState.Deleted;
        //        _effectRow = _db.SaveChanges();
        //        return _effectRow > 0 ? Enums.CrudStatus.Deleted : Enums.CrudStatus.NotDeleted;
        //    }
        //    else
        //        return Enums.CrudStatus.DataNotFound;
        //}
        //public IEnumerable<object> ScheduleList()
        //{
        //    _db = new HIMSDBEntities();
        //    var _list = (from docSchedule in _db.DoctorSchedules

        //                 orderby docSchedule.Doctor.DoctorName
        //                 select new 
        //                 {
        //                     DayId=docSchedule.DayID,
        //                     docSchedule.DayMaster.DayName,
        //                     docSchedule.DoctorID,
        //                     docSchedule.Doctor.DoctorName,
        //                     docSchedule.Doctor.Department.DepartmentName,
        //                     docSchedule.DoctorScheduleID,
        //                     TimeFrom=docSchedule.TimeFrom + (docSchedule.TimeFromMeridiemID==1?" AM":" PM"),
        //                     TimeTo=docSchedule.TimeTo + (docSchedule.TimeToMeridiemID == 1 ? " AM" : " PM"),
        //                     docSchedule.TimeFromMeridiemID,
        //                     docSchedule.TimeToMeridiemID 
        //                 }).ToList();
        //    return _list;
        //}
    }
}