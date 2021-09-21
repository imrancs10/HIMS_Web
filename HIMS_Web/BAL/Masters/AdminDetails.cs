using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using HIMS_Web.Global;
using System.Data.Entity;
using HIMS_Web.Models.Masters;

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