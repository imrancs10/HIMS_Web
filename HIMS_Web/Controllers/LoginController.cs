using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using HIMS_Web.BAL.Login;
using HIMS_Web.Global;
using HIMS_Web.Infrastructure.Authentication;

namespace HIMS_Web.Controllers
{
    public class LoginController : CommonController
    {
        // GET: Login
        public ActionResult Index()
        {
            ViewData["LoginPage"] = true;
            return View();
        }

        public ActionResult GetLogin(string username, string password)
        {
            LoginDetails _details = new LoginDetails();
            string _response = string.Empty;
            Enums.LoginMessage message = _details.GetLogin(username, password);
            _response = LoginResponse(message);
            if (message == Enums.LoginMessage.Authenticated)
            {
                setUserClaim();
                return RedirectToAction("IpdDashboard", "Admin");
            }
            else
            {
                SetAlertMessage(_response, "Login Response");
                return View("index");
            }
        }

        private void setUserClaim()
        {
            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.Id = UserData.UserId;
            serializeModel.Name = string.IsNullOrEmpty(UserData.Name) ? string.Empty : UserData.Name;
            serializeModel.Email = string.IsNullOrEmpty(UserData.Email) ? string.Empty : UserData.Email;

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     UserData.Email,
                     DateTime.Now,
                     DateTime.Now.AddMinutes(15),
                     false,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}