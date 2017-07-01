using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace WebAPI.ActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
         {
             if (actionContext.ModelState.IsValid == false)
             {
                 actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
             }
             base.OnActionExecuting(actionContext);
         }
    }
}