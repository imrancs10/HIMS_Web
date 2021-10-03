using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using HIMS_Web.Global;
using System.Data.Entity;

namespace HIMS_Web.BAL.Login
{
    public class LoginDetails
    {
        HIMSDBEntities _db = null;

        /// <summary>
        /// Get Authenticate User credentials
        /// </summary>
        /// <param name="UserName">Username</param>
        /// <param name="Password">Password</param>
        /// <returns>Enums</returns>
        public Enums.LoginMessage GetLogin(string UserName, string Password)
        {
            _db = new HIMSDBEntities();

            var _userRow = _db.HIMSUsers.Where(x => x.UserName.Equals(UserName) && x.Password.Equals(Password) && x.IsActive == true).FirstOrDefault();

            if (_userRow != null)
            {
                UserData.UserId = _userRow.Id;
                UserData.Username = _userRow.UserName;
                UserData.Name = _userRow.Name;
                UserData.Email = _userRow.Email;
                return Enums.LoginMessage.Authenticated;
            }
            else
                return Enums.LoginMessage.InvalidCreadential;
        }
    }
}