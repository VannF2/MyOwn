using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
namespace lab6.Models
{
    public class ActionLog : ActionFilterAttribute
    {
        static readonly Database db = new Database();

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controllerName = filterContext.RouteData.Values["controller"];
            var actionName = filterContext.RouteData.Values["action"];
            db.Log(controllerName.ToString(), actionName.ToString(), DateTime.Now);
        }
    }
}