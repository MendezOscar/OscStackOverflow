using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using FilterAttribute = System.Web.Http.Filters.FilterAttribute;
using IActionFilter = System.Web.Mvc.IActionFilter;

namespace StackOverflowOsc.Web.CostumeDataNotations
{
    public class LoginAttribute : FilterAttribute, IActionFilter, IResultFilter, IAuthorizationFilter, IExceptionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            throw new NotImplementedException();
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            throw new NotImplementedException();
        }

        public void OnException(ExceptionContext filterContext)
        {
            throw new NotImplementedException();
        }
    }
}