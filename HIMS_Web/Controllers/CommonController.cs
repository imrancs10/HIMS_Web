using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HIMS_Web.BAL.Masters;
using HIMS_Web.Global;
//using HIMS_Web.BAL.Masters;
//using HIMS_Web.BAL.Commom;
using HIMS_Web.Infrastructure.Authentication;

namespace HIMS_Web.Controllers
{
    public class CommonController : Controller
    {
        //CommonDetails _details = null;

        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }

        public string LoginResponse(Enums.LoginMessage inputMessage)
        {
            if (inputMessage == Enums.LoginMessage.InvalidCreadential)
                return Resource.Login_InvalidCredential;
            else
                return Global.Resource.Common_NoResponseFromServer;
        }

        public string CrudResponse(Enums.CrudStatus inputMessage)
        {
            if (inputMessage == Enums.CrudStatus.DataAlreadyExist)
                return Resource.Crud_DataAlreadyExist;
            else if (inputMessage == Enums.CrudStatus.Saved)
                return Global.Resource.Crud_DataSaved;
            else if (inputMessage == Enums.CrudStatus.NotSaved)
                return Global.Resource.Crud_DataNotSaved;
            else if (inputMessage == Enums.CrudStatus.Deleted)
                return Global.Resource.Crud_DataDelete;
            else if (inputMessage == Enums.CrudStatus.NotDeleted)
                return Global.Resource.Crud_NotDelete;
            else if (inputMessage == Enums.CrudStatus.Updated)
                return Global.Resource.Crud_DataUpdated;
            else if (inputMessage == Enums.CrudStatus.NotUpdated)
                return Global.Resource.Crud_DataNotUpdated;
            else if (inputMessage == Enums.CrudStatus.NotSaved)
                return Global.Resource.Crud_DataNotSaved;
            else if (inputMessage == Enums.CrudStatus.SessionExpired)
                return Global.Resource.Session_Expired;
            else if (inputMessage == Enums.CrudStatus.InvalidPostedData)
                return Global.Resource.Invalid_Posted_Data;
            else if (inputMessage == Enums.CrudStatus.InvalidPastDate)
                return Global.Resource.Invalid_Past_Date;
            else
                return Resource.Common_NoResponseFromServer;
        }

        public void SetAlertMessage(string message, string title = "Alert")
        {
            TempData["Alert_Message"] = message;
            TempData["Alert_Title"] = title;
        }

        public virtual JsonResult GetDepartments()
        {
            MasterDetails _details = new MasterDetails();
            return Json(_details.DepartmentList(), JsonRequestBehavior.AllowGet);
        }

        public virtual JsonResult GetTreatment()
        {
            MasterDetails _details = new MasterDetails();
            return Json(_details.GetTreatment(), JsonRequestBehavior.AllowGet);
        }
        public virtual JsonResult Getstate()
        {
            MasterDetails _details = new MasterDetails();
            return Json(_details.Getstate(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCityByStateId(int stateId = 0)
        {
            MasterDetails _details = new MasterDetails();
            return Json(_details.GetCityByStateId(stateId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAreaByCityId(int? cityId = 0)
        {
            MasterDetails _details = new MasterDetails();
            return Json(_details.GetAreaByCityId(cityId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAreaAutoComplete(string prefix)
        {
            MasterDetails _details = new MasterDetails();
            var data = _details.GetAreaByCityId(0);
            var areas = (from area in data
                         where area.AreaName.ToLower().StartsWith(prefix.ToLower())
                             select new
                             {
                                 label = area.AreaName,
                                 val = area.AreaId
                             }).ToList();

            return Json(areas);
        }

        //public JsonResult GetDaysList()
        //{
        //    _details = new CommonDetails();
        //    return Json(_details.DaysList(), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult SearchPatient(string value)
        //{
        //    _details = new CommonDetails();
        //    return Json(_details.PatientSearch(value),JsonRequestBehavior.AllowGet);
        //}
    }
}