using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HIMS_Web.Infrastructure.Utility
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        List<string> ByPassActions = new List<string>()
        {
            "PaymentResponse",
            "PayAllotementMoney",
            "PaymentResponseAllotment",
            "AllotementPaymentResponseSuccess",
            "AllotementPaymentReciept"
        };
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (!ByPassActions.Contains(filterContext.ActionDescriptor.ActionName))
            {
                if (HttpContext.Current.Session["userid"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Login/ApplicantLogin");
                    return;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }

    public class AdminSessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["userid"] == null)
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}