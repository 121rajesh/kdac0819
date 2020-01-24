using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ExaminationPortal.Filters
{
    public class ActionFilter : ActionFilterAttribute
    {
        MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string msgFormat = "/{0}/{1} is called";

            string controller = actionContext.ActionDescriptor
                                .ControllerDescriptor.ControllerName;

            string method = actionContext.ActionDescriptor.ActionName;

            string msg = string.Format(msgFormat, controller, method);
            logger.Log(msg);
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            string msgFormat = "/{0}/{1} is executed";

            string msg = string.Format(msgFormat, actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName, actionExecutedContext.ActionContext.ActionDescriptor.ActionName);
            logger.Log(msg);
        }
    }
}