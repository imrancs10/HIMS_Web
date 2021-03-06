using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using System.Data.Entity;
using HIMS_Web.Global;
using HIMS_Web.Models.Masters;

namespace HIMS_Web.BAL.Masters
{
    public class MasterDetails
    {
        HIMSDBEntities _db = null;

        public List<DepartmentModel> DepartmentList()
        {
            _db = new HIMSDBEntities();
            var _list = (from dept in _db.Departments
                         select new DepartmentModel
                         {
                             DeparmentName = dept.DepartmentName,
                             DepartmentId = dept.DepartmentID,
                             DepartmentUrl = dept.DepartmentUrl,
                             Description = dept.Description,
                             //Image = dept.Image
                             //ImageUrl= string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(dept.Image))
                         }).ToList();
            return _list != null ? _list : new List<DepartmentModel>();
        }
        public DepartmentModel GetDeparmentById(int deptId)
        {
            _db = new HIMSDBEntities();
            var _deptRow = _db.Departments.Where(x => x.DepartmentID.Equals(deptId)).FirstOrDefault();
            if (_deptRow != null)
            {
                var dep = new DepartmentModel()
                {
                    DeparmentName = _deptRow.DepartmentName,
                    DepartmentId = _deptRow.DepartmentID,
                    DepartmentUrl = _deptRow.DepartmentUrl,
                    Description = _deptRow.Description,
                    Image = _deptRow.Image
                };
                return dep;
            }
            return null;
        }
        public List<TreatmentModel> GetTreatment()
        {
            _db = new HIMSDBEntities();
            var _list = (from dept in _db.Treatments
                         select new TreatmentModel
                         {
                             TreatmentId = dept.TreatmentID,
                             TreatmentName = dept.TreatmentName,
                             TreatmentDescription = dept.Description
                         }).ToList();
            return _list != null ? _list : new List<TreatmentModel>();
        }
        public TreatmentModel GetTreatmentById(int TreatmentId)
        {
            _db = new HIMSDBEntities();
            int _effectRow = 0;
            var _deptRow = _db.Treatments.Where(x => x.TreatmentID.Equals(TreatmentId)).FirstOrDefault();
            if (_deptRow != null)
            {
                var dep = new TreatmentModel()
                {
                    TreatmentId = _deptRow.TreatmentID,
                    TreatmentName = _deptRow.TreatmentName,
                    TreatmentDescription = _deptRow.Description
                };
                return dep;
            }
            return null;
        }
        public List<StateModel> Getstate()
        {
            _db = new HIMSDBEntities();
            var _list = (from dept in _db.States
                         select new StateModel
                         {
                             StateId = dept.StateId,
                             StateName = dept.StateName,
                         }).ToList();
            return _list != null ? _list : new List<StateModel>();
        }
        public List<CityModel> GetCityByStateId(int stateId)
        {
            _db = new HIMSDBEntities();
            var _list = (from dept in _db.Cities
                         where dept.StateId == stateId
                         select new CityModel
                         {
                             CityId = dept.CityId,
                             CityName = dept.CityName,
                             StateId = dept.StateId
                         }).ToList();
            return _list != null ? _list : new List<CityModel>();
        }
        public List<AreaModel> GetAreaByCityId(int? cityId)
        {
            _db = new HIMSDBEntities();
            var _list = (from dept in _db.Areas
                         where (cityId != 0 && dept.CityId == cityId) || cityId == 0
                         select new AreaModel
                         {
                             AreaId = dept.AreaId,
                             AreaName = dept.AreaName,
                             CityId = dept.CityId,
                             WardId = dept.WardId
                         }).ToList();
            return _list != null ? _list : new List<AreaModel>();
        }
        public AreaModel GetAreaByAreaId(int areaId)
        {
            _db = new HIMSDBEntities();
            var _list = (from dept in _db.Areas
                         where dept.AreaId == areaId
                         select new AreaModel
                         {
                             AreaId = dept.AreaId,
                             AreaName = dept.AreaName,
                             CityId = dept.CityId,
                             WardId = dept.WardId
                         }).FirstOrDefault();
            return _list;
        }
        //public List<MasterLookupModel> GetMastersData()
        //{
        //    _db = new HIMSDBEntities();
        //    var _list = (from dept in _db.MasterLookups
        //                 select new MasterLookupModel
        //                 {
        //                     Name = dept.Name,
        //                     Id = dept.Id,
        //                     Value = dept.Value
        //                 }).ToList();
        //    return _list != null ? _list : new List<MasterLookupModel>();
        //}

       

        //public Enums.CrudStatus SaveMasterLookup(string name, string value)
        //{
        //    _db = new HIMSDBEntities();
        //    int _effectRow = 0;
        //    var _deptRow = _db.MasterLookups.Where(x => x.Name.Equals(name)).FirstOrDefault();
        //    if (_deptRow == null)
        //    {
        //        MasterLookup _newDept = new MasterLookup();
        //        _newDept.Name = name;
        //        _newDept.Value = value;
        //        _db.Entry(_newDept).State = EntityState.Added;
        //        _effectRow = _db.SaveChanges();
        //        return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        //    }
        //    else
        //        return Enums.CrudStatus.DataAlreadyExist;
        //}

        //public Enums.CrudStatus EditMasterLookup(string name, string value, int deptId)
        //{
        //    _db = new HIMSDBEntities();
        //    int _effectRow = 0;
        //    var _deptRow = _db.MasterLookups.Where(x => x.Id.Equals(deptId)).FirstOrDefault();
        //    if (_deptRow != null)
        //    {
        //        _deptRow.Name = name;
        //        _deptRow.Value = value;
        //        _db.Entry(_deptRow).State = EntityState.Modified;
        //        _effectRow = _db.SaveChanges();
        //        return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
        //    }
        //    else
        //        return Enums.CrudStatus.DataNotFound;
        //}
        //public Enums.CrudStatus DeleteMasterLookup(int deptId)
        //{
        //    _db = new HIMSDBEntities();
        //    int _effectRow = 0;
        //    var _deptRow = _db.MasterLookups.Where(x => x.Id.Equals(deptId)).FirstOrDefault();
        //    if (_deptRow != null)
        //    {
        //        _db.MasterLookups.Remove(_deptRow);
        //        //_db.Entry(_deptRow).State = EntityState.Deleted;
        //        _effectRow = _db.SaveChanges();
        //        return _effectRow > 0 ? Enums.CrudStatus.Deleted : Enums.CrudStatus.NotDeleted;
        //    }
        //    else
        //        return Enums.CrudStatus.DataNotFound;
        //}
    }
}