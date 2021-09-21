using DataLayer;
using HIMS_Web.Global;
using HIMS_Web.Models.Patient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using static HIMS_Web.Global.Enums;

namespace HIMS_Web.BAL.Lookup
{
    public class LookupDetails
    {
        HIMS_WebEntities _db = null;

        enum LookupEnum
        {
            HelpLineNo
        }
        public List<MasterLookup> GetLookupDetail()
        {
            _db = new HIMS_WebEntities();
            return _db.MasterLookups.ToList();
        }
    }
}