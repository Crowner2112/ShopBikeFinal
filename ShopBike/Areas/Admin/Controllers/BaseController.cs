using ShopPhone.Common;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopPhone.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sess = (EmployeeLogin)Session[CommonConstants.EMPLOYEE_SESSION];
            if (sess == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}