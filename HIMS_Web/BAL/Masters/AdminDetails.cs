using System;
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

        public bool ChangePassword(string oldPassword, string newPassword, int userId)
        {
            _db = new HIMSDBEntities();
            int _effectRow = 0;
            var _deptRow = _db.HIMSUsers.Where(x => x.Id == userId && x.Password == oldPassword).FirstOrDefault();
            if (_deptRow != null)
            {
                _deptRow.Password = newPassword;
                _db.Entry(_deptRow).State = EntityState.Modified;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? true : false;
            }
            else
                return false;
        }
        public IpdPatientInfoModel GetIPDPatientById(int? patientId)
        {
            _db = new HIMSDBEntities();
            return GetIPDList().Where(x => x.PatientId.Equals(patientId)).FirstOrDefault();
        }
        public int SaveIPDEntry(IpdPatientInfo model)
        {
            _db = new HIMSDBEntities();
            int _effectRow = 0;
            var _deptRow = _db.IpdPatientInfoes.Where(x => x.PatientId.Equals(model.PatientId)).FirstOrDefault();
            if (_deptRow == null)
            {
                _db.Entry(model).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? model.PatientId : 0;
            }
            else
            {
                _deptRow.Address = !string.IsNullOrEmpty(model.Address) ? model.Address : _deptRow.Address;
                _deptRow.AdmittedDateTime = model.AdmittedDateTime != null ? model.AdmittedDateTime : _deptRow.AdmittedDateTime;
                _deptRow.Age = model.Age != null ? model.Age : _deptRow.Age;
                _deptRow.AreaId = model.AreaId != null ? model.AreaId : _deptRow.AreaId;
                _deptRow.CityId = model.CityId != null ? model.CityId : _deptRow.CityId;
                _deptRow.DepartmentId = model.DepartmentId != null ? model.DepartmentId : _deptRow.DepartmentId;
                _deptRow.FatherOrHusbandName = !string.IsNullOrEmpty(model.FatherOrHusbandName) ? model.FatherOrHusbandName : _deptRow.FatherOrHusbandName;
                _deptRow.Gender = !string.IsNullOrEmpty(model.Gender) ? model.Gender : _deptRow.Gender;
                _deptRow.IDNumber = !string.IsNullOrEmpty(model.IDNumber) ? model.IDNumber : _deptRow.IDNumber;
                _deptRow.IDorAadharNumber = !string.IsNullOrEmpty(model.IDorAadharNumber) ? model.IDorAadharNumber : _deptRow.IDorAadharNumber;
                _deptRow.MobileNumber = !string.IsNullOrEmpty(model.MobileNumber) ? model.MobileNumber : _deptRow.MobileNumber;
                _deptRow.PatientName = !string.IsNullOrEmpty(model.PatientName) ? model.PatientName : _deptRow.PatientName;
                _db.Entry(_deptRow).State = EntityState.Modified;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? _deptRow.PatientId : 0;
            }
        }
        public bool duplicateIPDNumber(string ipdNumber)
        {
            _db = new HIMSDBEntities();
            var _deptRow = _db.IpdPatientInfoes.Where(x => x.IpdNo.Equals(ipdNumber) && x.IsActive == true).FirstOrDefault();
            return _deptRow != null;
        }
        public Enums.CrudStatus SaveIPDTreatment(List<int> treatmentList, int patientID)
        {
            _db = new HIMSDBEntities();
            int _effectRow = 0;
            var treatments = _db.PatientTreatments.Where(x => x.PatientId == patientID).ToList();
            treatments.ForEach(x =>
            {
                _db.Entry(x).State = EntityState.Deleted;
            });
            treatmentList.ForEach(x =>
            {
                PatientTreatment pt = new PatientTreatment()
                {
                    CreatedBy = UserData.UserId,
                    CreatedDate = DateTime.Now,
                    PatientId = patientID,
                    PatientTreatmentId = x
                };
                _db.Entry(pt).State = EntityState.Added;
            });
            _db.SaveChanges();

            return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }
        public List<IpdPatientInfoModel> GetIPDList()
        {
            _db = new HIMSDBEntities();
            var _list = (from dept in _db.IpdPatientInfoes
                         join patTreat in _db.PatientTreatments on dept.PatientId equals patTreat.PatientId
                         join treat in _db.Treatments on patTreat.PatientTreatmentId equals treat.TreatmentID
                         join area in _db.Areas on dept.AreaId equals area.AreaId
                         join deptarment in _db.Departments on dept.DepartmentId equals deptarment.DepartmentID
                         join labReport1 in _db.IpdPatientLabReports on dept.PatientId equals labReport1.PatientId into labReport2
                         from labReport in labReport2.DefaultIfEmpty()
                         join patStatus1 in _db.IpdPatientStatus on dept.PatientId equals patStatus1.PatientId into patStatus2
                         from patStatus in patStatus2.DefaultIfEmpty()
                         where dept.IsActive == true
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
                             TreatmentId = treat.TreatmentID,
                             AreaName = area != null ? area.AreaName : "",
                             DepartmentName = deptarment != null ? deptarment.DepartmentName : "",
                             TreatmentName = treat != null ? treat.TreatmentName : "",
                             MalariaStatus = labReport != null && labReport.MalariaParasite_Status != null ? labReport.MalariaParasite_Status : "",
                             RapidKitNS1Status = labReport != null && labReport.RapidKitNS1_Status != null ? labReport.RapidKitNS1_Status : "",
                             RapidKitIGMStatus = labReport != null && labReport.RapidKitIGM_Status != null ? labReport.RapidKitIGM_Status : "",
                             ELISANS1Status = labReport != null && labReport.ELISANS1_Status != null ? labReport.ELISANS1_Status : "",
                             ELISAIGMStatus = labReport != null && labReport.ELISAIGM_Status != null ? labReport.ELISAIGM_Status : "",
                             ELISAScrubTyphusStatus = labReport != null && labReport.ELISAScrubTyphus_Status != null ? labReport.ELISAScrubTyphus_Status : "",
                             ELISALeptospiraStatus = labReport != null && labReport.ELISALaptospira_Status != null ? labReport.ELISALaptospira_Status : "",
                             DischargeDateTime = patStatus != null ? patStatus.DischargeDateTime : null,
                             ReferDateTime = patStatus != null ? patStatus.ReferDateTime : null,
                             LAMADateTime = patStatus != null ? patStatus.LAMADateTime : null,
                             DOPRDateTime = patStatus != null ? patStatus.DOPRDateTime : null,
                             DeathDateTime = patStatus != null ? patStatus.DeathDateTime : null,
                             AbscondDateTime = patStatus != null ? patStatus.AbscondDateTime : null,
                             OtherDateTime = patStatus != null ? patStatus.OtherDateTime : null
                         }).Distinct().ToList();

            var listGrouped = _list.GroupBy(x => x.PatientId).Select(x => new { patientId = x.Key });
            var output = new List<IpdPatientInfoModel>();
            foreach (var item in listGrouped)
            {
                var treartments = _list.Where(x => x.PatientId == item.patientId).ToList();
                var treatmentName = string.Join(",", treartments.Select(x => x.TreatmentName).ToList());
                var treatmentIds = string.Join(",", treartments.Select(x => x.TreatmentId).ToList());
                var patientInfo = _list.Where(x => x.PatientId == item.patientId).FirstOrDefault();
                output.Add(new IpdPatientInfoModel()
                {
                    Address = patientInfo.Address,
                    AdmittedDate = patientInfo.AdmittedDateTime,
                    AdmittedDateTime = patientInfo.AdmittedDateTime != null ? patientInfo.AdmittedDateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                    Age = patientInfo.Age,
                    FatherOrHusbandName = patientInfo.FatherOrHusbandName,
                    Gender = patientInfo.Gender,
                    IDNumber = patientInfo.IDNumber,
                    IDorAadharNumber = patientInfo.IDorAadharNumber,
                    IpdNo = patientInfo.IpdNo,
                    IPDStatus = patientInfo.IPDStatus,
                    MobileNumber = patientInfo.MobileNumber,
                    PatientId = patientInfo.PatientId,
                    PatientName = patientInfo.PatientName,
                    AreaId = patientInfo.AreaId,
                    DepartmentId = patientInfo.DepartmentId,
                    AreaName = patientInfo.AreaName,
                    DepartmentName = patientInfo.DepartmentName,
                    TreatmentIds = treatmentIds,
                    TreatmentName = treatmentName,
                    MalariaStatus = patientInfo.MalariaStatus,
                    RapidKitNS1Status = patientInfo.RapidKitNS1Status,
                    RapidKitIGMStatus = patientInfo.RapidKitIGMStatus,
                    ELISANS1Status = patientInfo.ELISANS1Status,
                    ELISAIGMStatus = patientInfo.ELISAIGMStatus,
                    ELISAScrubTyphusStatus = patientInfo.ELISAScrubTyphusStatus,
                    ELISALeptospiraStatus = patientInfo.ELISALeptospiraStatus,
                    DischargeDateTime = patientInfo.DischargeDateTime != null ? patientInfo.DischargeDateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                    ReferDateTime = patientInfo.ReferDateTime != null ? patientInfo.ReferDateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                    LAMADateTime = patientInfo.LAMADateTime != null ? patientInfo.LAMADateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                    DOPRDateTime = patientInfo.DOPRDateTime != null ? patientInfo.DOPRDateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                    DeathDateTime = patientInfo.DeathDateTime != null ? patientInfo.DeathDateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                    AbscondDateTime = patientInfo.AbscondDateTime != null ? patientInfo.AbscondDateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                    OtherDateTime = patientInfo.OtherDateTime != null ? patientInfo.OtherDateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                });

            }

            output.ForEach(x =>
            {
                if (x.IPDStatus == "Admit")
                    x.IPDStatusDate = x.AdmittedDateTime;
                else if (x.IPDStatus == "Discharge")
                    x.IPDStatusDate = x.DischargeDateTime;
                else if (x.IPDStatus == "Refer")
                    x.IPDStatusDate = x.ReferDateTime;
                else if (x.IPDStatus == "LAMA")
                    x.IPDStatusDate = x.LAMADateTime;
                else if (x.IPDStatus == "DOPR")
                    x.IPDStatusDate = x.DOPRDateTime;
                else if (x.IPDStatus == "Death")
                    x.IPDStatusDate = x.DeathDateTime;
                else if (x.IPDStatus == "Abscond")
                    x.IPDStatusDate = x.AbscondDateTime;
                else if (x.IPDStatus == "Other")
                    x.IPDStatusDate = x.OtherDateTime;
            });

            return output != null ? output : new List<IpdPatientInfoModel>();
        }

        public List<IpdPatientInfoModel> GetIPDDetail(string searchText)
        {
            _db = new HIMSDBEntities();
            var _list = (from dept in _db.IpdPatientInfoes
                         join patTreat in _db.PatientTreatments on dept.PatientId equals patTreat.PatientId
                         join treat in _db.Treatments on patTreat.PatientTreatmentId equals treat.TreatmentID
                         join area in _db.Areas on dept.AreaId equals area.AreaId
                         join deptarment in _db.Departments on dept.DepartmentId equals deptarment.DepartmentID
                         join labReport1 in _db.IpdPatientLabReports on dept.PatientId equals labReport1.PatientId into labReport2
                         from labReport in labReport2.DefaultIfEmpty()
                         join patStatus1 in _db.IpdPatientStatus on dept.PatientId equals patStatus1.PatientId into patStatus2
                         from patStatus in patStatus2.DefaultIfEmpty()
                         where ((!searchText.Contains("/") && (dept.IpdNo.Contains(searchText) || dept.PatientName.Contains(searchText))) || searchText.Contains("/"))
                         && dept.IsActive == true
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
                             TreatmentId = treat.TreatmentID,
                             AreaName = area != null ? area.AreaName : "",
                             DepartmentName = deptarment != null ? deptarment.DepartmentName : "",
                             TreatmentName = treat != null ? treat.TreatmentName : "",
                             MalariaStatus = labReport != null && labReport.MalariaParasite_Status != null ? labReport.MalariaParasite_Status : "",
                             RapidKitNS1Status = labReport != null && labReport.RapidKitNS1_Status != null ? labReport.RapidKitNS1_Status : "",
                             RapidKitIGMStatus = labReport != null && labReport.RapidKitIGM_Status != null ? labReport.RapidKitIGM_Status : "",
                             ELISANS1Status = labReport != null && labReport.ELISANS1_Status != null ? labReport.ELISANS1_Status : "",
                             ELISAIGMStatus = labReport != null && labReport.ELISAIGM_Status != null ? labReport.ELISAIGM_Status : "",
                             ELISAScrubTyphusStatus = labReport != null && labReport.ELISAScrubTyphus_Status != null ? labReport.ELISAScrubTyphus_Status : "",
                             ELISALeptospiraStatus = labReport != null && labReport.ELISALaptospira_Status != null ? labReport.ELISALaptospira_Status : "",
                             PatientStatusUpdated = patStatus != null ? patStatus.IPDStatus : "",
                             DischargeDateTime = patStatus != null ? patStatus.DischargeDateTime : null,
                             ReferDateTime = patStatus != null ? patStatus.ReferDateTime : null,
                             LAMADateTime = patStatus != null ? patStatus.LAMADateTime : null,
                             DOPRDateTime = patStatus != null ? patStatus.DOPRDateTime : null,
                             DeathDateTime = patStatus != null ? patStatus.DeathDateTime : null,
                             AbscondDateTime = patStatus != null ? patStatus.AbscondDateTime : null,
                             OtherDateTime = patStatus != null ? patStatus.OtherDateTime : null
                         }).Distinct().ToList();

            var listGrouped = _list.GroupBy(x => x.PatientId).Select(x => new { patientId = x.Key });
            var output = new List<IpdPatientInfoModel>();
            foreach (var item in listGrouped)
            {
                var treartments = _list.Where(x => x.PatientId == item.patientId).Select(x => x.TreatmentName).ToList();
                var treatmentName = string.Join(",", treartments);
                var patientInfo = _list.Where(x => x.PatientId == item.patientId).FirstOrDefault();
                output.Add(new IpdPatientInfoModel()
                {
                    Address = patientInfo.Address,
                    AdmittedDateTime = patientInfo.AdmittedDateTime != null ? patientInfo.AdmittedDateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                    Age = patientInfo.Age,
                    FatherOrHusbandName = patientInfo.FatherOrHusbandName,
                    Gender = patientInfo.Gender,
                    IDNumber = patientInfo.IDNumber,
                    IDorAadharNumber = patientInfo.IDorAadharNumber,
                    IpdNo = patientInfo.IpdNo,
                    IPDStatus = patientInfo.IPDStatus,
                    MobileNumber = patientInfo.MobileNumber,
                    PatientId = patientInfo.PatientId,
                    PatientName = patientInfo.PatientName,
                    AreaId = patientInfo.AreaId,
                    DepartmentId = patientInfo.DepartmentId,
                    AreaName = patientInfo.AreaName,
                    DepartmentName = patientInfo.DepartmentName,
                    MalariaStatus = patientInfo.MalariaStatus,
                    RapidKitNS1Status = patientInfo.RapidKitNS1Status,
                    RapidKitIGMStatus = patientInfo.RapidKitIGMStatus,
                    ELISANS1Status = patientInfo.ELISANS1Status,
                    ELISAIGMStatus = patientInfo.ELISAIGMStatus,
                    ELISAScrubTyphusStatus = patientInfo.ELISAScrubTyphusStatus,
                    ELISALeptospiraStatus = patientInfo.ELISALeptospiraStatus,
                    TreatmentName = treatmentName,
                    DischargeDateTime = patientInfo.DischargeDateTime != null ? patientInfo.DischargeDateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                    ReferDateTime = patientInfo.ReferDateTime != null ? patientInfo.ReferDateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                    LAMADateTime = patientInfo.LAMADateTime != null ? patientInfo.LAMADateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                    DOPRDateTime = patientInfo.DOPRDateTime != null ? patientInfo.DOPRDateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                    DeathDateTime = patientInfo.DeathDateTime != null ? patientInfo.DeathDateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                    AbscondDateTime = patientInfo.AbscondDateTime != null ? patientInfo.AbscondDateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                    OtherDateTime = patientInfo.OtherDateTime != null ? patientInfo.OtherDateTime.Value.ToString("dd/MM/yyyy hh:mm") : string.Empty,
                });
            }
            output.ForEach(x =>
            {
                if (x.IPDStatus == "Admit")
                    x.IPDStatusDate = x.AdmittedDateTime;
                else if (x.IPDStatus == "Discharge")
                    x.IPDStatusDate = x.DischargeDateTime;
                else if (x.IPDStatus == "Refer")
                    x.IPDStatusDate = x.ReferDateTime;
                else if (x.IPDStatus == "LAMA")
                    x.IPDStatusDate = x.LAMADateTime;
                else if (x.IPDStatus == "DOPR")
                    x.IPDStatusDate = x.DOPRDateTime;
                else if (x.IPDStatus == "Death")
                    x.IPDStatusDate = x.DeathDateTime;
                else if (x.IPDStatus == "Abscond")
                    x.IPDStatusDate = x.AbscondDateTime;
                else if (x.IPDStatus == "Other")
                    x.IPDStatusDate = x.OtherDateTime;
            });
            //output = output.Where(x => (searchText.Contains("/") && x.AdmittedDateTime.Contains(searchText)) || !searchText.Contains("/")).ToList();
            return output != null ? output : new List<IpdPatientInfoModel>();
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
                        pages.Description = model.Description;
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
                    var pages = _db.IpdPatientLabReports.Where(x => x.PatientId == report.PatientId).FirstOrDefault();
                    if (pages != null)
                    {
                        pages.ELISAIGM_Status = report.ELISAIGM_Status;
                        pages.ELISANS1_Status = report.ELISANS1_Status;
                        pages.ELISALaptospira_Status = report.ELISALaptospira_Status;
                        pages.ELISAScrubTyphus_Status = report.ELISAScrubTyphus_Status;
                        pages.RapidKitNS1_Status = report.RapidKitNS1_Status;
                        pages.RapidKitIGM_Status = report.RapidKitIGM_Status;
                        pages.LFT_Details = report.LFT_Details;
                        pages.KFT_Details = report.KFT_Details;
                        pages.RandomDonerPlatelet_Count = report.RandomDonerPlatelet_Count;
                        pages.SingleDonorPlatelet_Count = report.SingleDonorPlatelet_Count;
                        pages.WholeBloodCell_Count = report.WholeBloodCell_Count;
                        pages.PackedRBC_Count = report.PackedRBC_Count;
                        pages.MalariaParasite_Status = report.MalariaParasite_Status;
                        pages.PlateletCount = report.PlateletCount;
                        pages.HbCount = report.HbCount;

                        if (report.ELISAIGM_TestDate != null)
                            pages.ELISAIGM_TestDate = Convert.ToDateTime(report.ELISAIGM_TestDate);
                        if (report.ELISANS1_TestDate != null)
                            pages.ELISANS1_TestDate = Convert.ToDateTime(report.ELISANS1_TestDate);
                        if (report.ELISALaptospira_TestDate != null)
                            pages.ELISALaptospira_TestDate = Convert.ToDateTime(report.ELISALaptospira_TestDate);
                        if (report.ELISAScrubTyphus_TestDate != null)
                            pages.ELISAScrubTyphus_TestDate = Convert.ToDateTime(report.ELISAScrubTyphus_TestDate);
                        if (report.RapidKitNS1_TestDate != null)
                            pages.RapidKitNS1_TestDate = Convert.ToDateTime(report.RapidKitNS1_TestDate);
                        if (report.RapidKitIGM_TestDate != null)
                            pages.RapidKitIGM_TestDate = Convert.ToDateTime(report.RapidKitIGM_TestDate);
                        if (report.RandomDonerPlatelet_TestDate != null)
                            pages.RandomDonerPlatelet_TestDate = Convert.ToDateTime(report.RandomDonerPlatelet_TestDate);
                        if (report.SingleDonorPlatelet_TestDate != null)
                            pages.SingleDonorPlatelet_TestDate = Convert.ToDateTime(report.SingleDonorPlatelet_TestDate);
                        if (report.WholeBloodCell_TestDate != null)
                            pages.WholeBloodCell_TestDate = Convert.ToDateTime(report.WholeBloodCell_TestDate);
                        if (report.PackedRBC_TestDate != null)
                            pages.PackedRBC_TestDate = Convert.ToDateTime(report.PackedRBC_TestDate);
                        if (report.MalariaParasite_TestDate != null)
                            pages.MalariaParasite_TestDate = Convert.ToDateTime(report.MalariaParasite_TestDate);

                        _db.Entry(pages).State = EntityState.Modified;
                        _effectRow = _db.SaveChanges();
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
                if (report.PatientId > 0)
                {
                    var pages = _db.IpdRadioDiagnosisReports.Where(x => x.PatientId == report.PatientId).FirstOrDefault();
                    if (pages != null)
                    {
                        pages.USG_Details = report.USG_Details;
                        pages.Xray_Details = report.Xray_Details;
                        _db.Entry(pages).State = EntityState.Modified;
                        _effectRow = _db.SaveChanges();
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
                    pages.IPDStatus = !string.IsNullOrEmpty(report.IPDStatus) ? report.IPDStatus : pages.IPDStatus;

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
                    pages.LAMACaseSummary = !string.IsNullOrEmpty(report.LAMACaseSummary) ? report.LAMACaseSummary : pages.LAMACaseSummary;
                    pages.LAMADateTime = report.LAMADateTime != null ? report.LAMADateTime : pages.LAMADateTime;
                    pages.LAMAReasons = !string.IsNullOrEmpty(report.LAMAReasons) ? report.LAMAReasons : pages.LAMAReasons;
                    pages.OtherDateTime = report.OtherDateTime != null ? report.OtherDateTime : pages.OtherDateTime;
                    pages.OtherReasons = !string.IsNullOrEmpty(report.OtherReasons) ? report.OtherReasons : pages.OtherReasons;
                    pages.ReferCaseSummary = !string.IsNullOrEmpty(report.ReferCaseSummary) ? report.ReferCaseSummary : pages.ReferCaseSummary;
                    pages.ReferDateTime = report.ReferDateTime != null ? report.ReferDateTime : pages.ReferDateTime;
                    pages.ReferReasons = !string.IsNullOrEmpty(report.ReferReasons) ? report.ReferReasons : pages.ReferReasons;
                    _db.Entry(pages).State = EntityState.Modified;

                    var ipdPatientInfo = _db.IpdPatientInfoes.Where(x => x.PatientId == report.PatientId && x.IsActive == true).FirstOrDefault();
                    if (ipdPatientInfo != null)
                    {
                        ipdPatientInfo.IPDStatus = pages.IPDStatus;
                        if (pages.IPDStatus == "Admit")
                            ipdPatientInfo.AdmittedDateTime = pages.AdmittedDateTime;
                        _db.Entry(ipdPatientInfo).State = EntityState.Modified;
                    }

                    _effectRow = _db.SaveChanges();
                }
                else
                {
                    var ipdPatientInfo = _db.IpdPatientInfoes.Where(x => x.PatientId == report.PatientId && x.IsActive == true).FirstOrDefault();
                    if (ipdPatientInfo != null)
                    {
                        ipdPatientInfo.IPDStatus = report.IPDStatus;
                        _db.Entry(ipdPatientInfo).State = EntityState.Modified;
                    }

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

        public bool DeletePatient(int patientId)
        {
            _db = new HIMSDBEntities();
            int _effectRow = 0;
            var _deptRow = _db.IpdPatientInfoes.Where(x => x.PatientId == patientId).FirstOrDefault();
            if (_deptRow != null)
            {
                _deptRow.IsActive = false;
                _db.Entry(_deptRow).State = EntityState.Modified;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? true : false;
            }
            else
                return false;
        }

        public DashboardDataModel GetDashboardData()
        {
            _db = new HIMSDBEntities();
            var _list = (from dept in _db.IpdPatientInfoes
                         join PatientStatus1 in _db.IpdPatientStatus on dept.PatientId equals PatientStatus1.PatientId into PatientStatus2
                         from PatientStatus in PatientStatus2.DefaultIfEmpty()
                         where dept.IsActive == true
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
                             PatientStatus = dept.IPDStatus
                         }).ToList();

            var listGrouped = _list.GroupBy(x => x.PatientStatus).Select(x => new { PatientStatus = x.Key, Count = x.Count() });
            var output = new DashboardDataModel();
            int AdmitPatient = 0, DischargePatient = 0, Refer = 0, Abscond = 0, LAMA = 0, DOPR = 0, Death = 0, Other = 0;
            output.PatientTreated = _list.Count();
            foreach (var item in listGrouped)
            {
                if (item.PatientStatus == "Admit")
                    output.AdmitPatient = item.Count;
                else if (item.PatientStatus == "Discharge")
                    output.DischargePatient = item.Count;
                else if (item.PatientStatus == "Refer")
                    output.Refer = item.Count;
                else if (item.PatientStatus == "Abscond")
                    output.Abscond = item.Count;
                else if (item.PatientStatus == "LAMA")
                    output.LAMA = item.Count;
                else if (item.PatientStatus == "DOPR")
                    output.DOPR = item.Count;
                else if (item.PatientStatus == "Death")
                    output.Death = item.Count;
                else if (item.PatientStatus == "Other")
                    output.Other = item.Count;
            }
            return output != null ? output : new DashboardDataModel();
        }

        public IPDPatientDetailModel GetPatientDetailByPatientId(int? patientId)
        {
            _db = new HIMSDBEntities();
            var labData = (from lab in _db.IpdPatientLabReports
                           where lab.PatientId == patientId
                           select new IpdPatientLabReportModel
                           {
                               CreatedBy = lab.CreatedBy,
                               CreatedDate = lab.CreatedDate,
                               ELISAIGM_Status = lab.ELISAIGM_Status,
                               ELISAIGM_TestDate = lab.ELISAIGM_TestDate,
                               ELISALaptospira_Status = lab.ELISALaptospira_Status,
                               ELISALaptospira_TestDate = lab.ELISALaptospira_TestDate,
                               ELISANS1_Status = lab.ELISANS1_Status,
                               ELISANS1_TestDate = lab.ELISANS1_TestDate,
                               ELISAScrubTyphus_Status = lab.ELISAScrubTyphus_Status,
                               ELISAScrubTyphus_TestDate = lab.ELISAScrubTyphus_TestDate,
                               HbCount = lab.HbCount,
                               Id = lab.Id,
                               KFT_Details = lab.KFT_Details,
                               LFT_Details = lab.LFT_Details,
                               MalariaParasite_Status = lab.MalariaParasite_Status,
                               MalariaParasite_TestDate = lab.MalariaParasite_TestDate,
                               PackedRBC_Count = lab.PackedRBC_Count,
                               PackedRBC_TestDate = lab.PackedRBC_TestDate,
                               PatientId = lab.PatientId,
                               PlateletCount = lab.PlateletCount,
                               RandomDonerPlatelet_Count = lab.RandomDonerPlatelet_Count,
                               RandomDonerPlatelet_TestDate = lab.RandomDonerPlatelet_TestDate,
                               RapidKitIGM_Status = lab.RapidKitIGM_Status,
                               RapidKitIGM_TestDate = lab.RapidKitIGM_TestDate,
                               RapidKitNS1_Status = lab.RapidKitNS1_Status,
                               RapidKitNS1_TestDate = lab.RapidKitNS1_TestDate,
                               SingleDonorPlatelet_Count = lab.SingleDonorPlatelet_Count,
                               SingleDonorPlatelet_TestDate = lab.SingleDonorPlatelet_TestDate,
                               WholeBloodCell_Count = lab.WholeBloodCell_Count,
                               WholeBloodCell_TestDate = lab.WholeBloodCell_TestDate
                           }).FirstOrDefault();
            var diagnosisData = (from lab in _db.IpdRadioDiagnosisReports
                                 where lab.PatientId == patientId
                                 select new IpdRadioDiagnosisReportModel
                                 {
                                     CreatedBy = lab.CreatedBy,
                                     CreatedDate = lab.CreatedDate,
                                     Id = lab.Id,
                                     PatientId = lab.PatientId,
                                     USG_Details = lab.USG_Details,
                                     Xray_Details = lab.Xray_Details
                                 }).FirstOrDefault();
            var statusData = (from lab in _db.IpdPatientStatus
                              where lab.PatientId == patientId
                              select new IpdPatientStatusModel
                              {
                                  AbscondCaseSummary = lab.AbscondCaseSummary,
                                  PatientId = lab.PatientId,
                                  Id = lab.Id,
                                  CreatedDate = lab.CreatedDate,
                                  CreatedBy = lab.CreatedBy,
                                  AbscondDateTime = lab.AbscondDateTime,
                                  AbscondReasons = lab.AbscondReasons,
                                  AdmittedDateTime = lab.AdmittedDateTime,
                                  DeathCaseSummary = lab.DeathCaseSummary,
                                  DeathDateTime = lab.DeathDateTime,
                                  DeathReasons = lab.DeathReasons,
                                  DischargeDateTime = lab.DischargeDateTime,
                                  DOPRCaseSummary = lab.DOPRCaseSummary,
                                  DOPRDateTime = lab.DOPRDateTime,
                                  DOPRReasons = lab.DOPRReasons,
                                  IPDStatus = lab.IPDStatus,
                                  LAMACaseSummary = lab.LAMACaseSummary,
                                  LAMADateTime = lab.LAMADateTime,
                                  LAMAReasons = lab.LAMAReasons,
                                  OtherDateTime = lab.OtherDateTime,
                                  OtherReasons = lab.OtherReasons,
                                  ReferCaseSummary = lab.ReferCaseSummary,
                                  ReferDateTime = lab.ReferDateTime,
                                  ReferReasons = lab.ReferReasons
                              }).FirstOrDefault();
            var data = new IPDPatientDetailModel();
            data.LabReport = labData;
            data.DiagnosisReport = diagnosisData;
            data.PatientStatus = statusData;
            return data;
        }

        public List<BarChartModel> GetIPDChartDetail(string reportStartDate)
        {
            try
            {
                _db = new HIMSDBEntities();
                DateTime filterStartDate;
                DateTime filterEndDate;

                if (string.IsNullOrEmpty(reportStartDate))
                {
                    filterStartDate = DateTime.Now.AddDays(-6);
                    filterEndDate = DateTime.Now;
                }
                else
                {
                    filterStartDate = Convert.ToDateTime(reportStartDate);
                    filterEndDate = (Convert.ToDateTime(reportStartDate)).AddDays(6);
                }

                var _list = (from dept in _db.IpdPatientInfoes
                             join patStatus1 in _db.IpdPatientStatus on dept.PatientId equals patStatus1.PatientId into patStatus2
                             from patStatus in patStatus2.DefaultIfEmpty()
                             where dept.IsActive == true
                                    && ((DbFunctions.TruncateTime(dept.AdmittedDateTime) >= DbFunctions.TruncateTime(filterStartDate) && DbFunctions.TruncateTime(dept.AdmittedDateTime) <= DbFunctions.TruncateTime(filterEndDate))
                                    || (DbFunctions.TruncateTime(patStatus.DischargeDateTime) >= DbFunctions.TruncateTime(filterStartDate) && DbFunctions.TruncateTime(patStatus.DischargeDateTime) <= DbFunctions.TruncateTime(filterEndDate))
                                    || (DbFunctions.TruncateTime(patStatus.ReferDateTime) >= DbFunctions.TruncateTime(filterStartDate) && DbFunctions.TruncateTime(patStatus.ReferDateTime) <= DbFunctions.TruncateTime(filterEndDate)))
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
                                 DischargeDateTime = patStatus != null ? patStatus.DischargeDateTime : null,
                                 ReferDateTime = patStatus != null ? patStatus.ReferDateTime : null,
                                 LAMADateTime = patStatus != null ? patStatus.LAMADateTime : null,
                                 DOPRDateTime = patStatus != null ? patStatus.DOPRDateTime : null,
                                 DeathDateTime = patStatus != null ? patStatus.DeathDateTime : null,
                                 AbscondDateTime = patStatus != null ? patStatus.AbscondDateTime : null,
                                 OtherDateTime = patStatus != null ? patStatus.OtherDateTime : null
                             }).Distinct().ToList();

                var model = new List<BarChartModel>();
                for (int i = 6; i >= 0; i--)
                {
                    var reportDate = filterEndDate.AddDays(-i);
                    var dataList = _list.Where(x => (x.AdmittedDateTime != null && x.AdmittedDateTime.Value.Date == reportDate.Date)
                       || (x.DischargeDateTime != null && x.DischargeDateTime.Value.Date == reportDate.Date)
                       || (x.ReferDateTime != null && x.ReferDateTime.Value.Date == reportDate.Date)).GroupBy(x => x.IPDStatus).Select(x => new { Status = x.Key, Count = x.Count() }).ToList();
                    var admitPatientCount = dataList.Where(x => x.Status == "Admit").FirstOrDefault() != null ? dataList.Where(x => x.Status == "Admit").FirstOrDefault().Count : 0;
                    var admittedPatientWithDiffStatusCount = _list.Where(x => x.IPDStatus != "Admit" && x.AdmittedDateTime != null && x.AdmittedDateTime.Value.Date == reportDate.Date).Count();
                    admitPatientCount += admittedPatientWithDiffStatusCount;
                    model.Add(new BarChartModel()
                    {
                        MonthYr = reportDate.ToString("dd-MMM"),
                        Admit = admitPatientCount,
                        Discharge = dataList.Where(x => x.Status == "Discharge").FirstOrDefault() != null ? dataList.Where(x => x.Status == "Discharge").FirstOrDefault().Count : 0,
                        Refer = dataList.Where(x => x.Status == "Refer").FirstOrDefault() != null ? dataList.Where(x => x.Status == "Refer").FirstOrDefault().Count : 0
                    });
                }

                return model != null ? model : new List<BarChartModel>();
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
                return new List<BarChartModel>();
            }
        }

        public List<HIMSUserModel> GetGidaUsers()
        {
            try
            {
                _db = new HIMSDBEntities();
                return (from user in _db.HIMSUsers
                        select new HIMSUserModel
                        {
                            Email = user.Email,
                            Id = user.Id,
                            IsActive = user.IsActive,
                            MobileNo = user.MobileNo,
                            Name = user.Name,
                            Password = user.Password,
                            UserName = user.UserName,
                            UserRole = user.UserRole
                        }).OrderByDescending(x => x.Name).ToList();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                    }
                }
                return new List<HIMSUserModel>();
            }
        }

        public Enums.CrudStatus SaveGidaUser(HIMSUser user)
        {
            try
            {
                _db = new HIMSDBEntities();
                int _effectRow = 0;
                if (user.Id > 0)
                {
                    var gidaUser = _db.HIMSUsers.Where(x => x.Id == user.Id).FirstOrDefault();
                    if (gidaUser != null)
                    {
                        gidaUser.IsActive = user.IsActive;
                        gidaUser.Email = user.Email;
                        gidaUser.MobileNo = user.MobileNo;
                        gidaUser.Name = user.Name;
                        gidaUser.UserName = user.UserName;
                        gidaUser.UserRole = user.UserRole;
                        _db.Entry(gidaUser).State = EntityState.Modified;
                        _effectRow = _db.SaveChanges();
                    }
                }
                else
                {
                    _db.Entry(user).State = EntityState.Added;
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
                    }
                }
                return Enums.CrudStatus.InternalError;
            }
        }
        public HIMSUserModel GetUserDetail(int userId)
        {
            try
            {
                _db = new HIMSDBEntities();
                var model = (from user in _db.HIMSUsers
                             where user.Id == userId
                             select new HIMSUserModel
                             {
                                 Email = user.Email,
                                 Id = user.Id,
                                 IsActive = user.IsActive,
                                 MobileNo = user.MobileNo,
                                 Name = user.Name,
                                 Password = user.Password,
                                 UserName = user.UserName,
                                 UserRole = user.UserRole,
                             }).FirstOrDefault();
                return model;

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
                return null;
            }
        }
    }
}