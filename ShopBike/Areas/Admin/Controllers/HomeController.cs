using ShopPhone.Areas.Admin.Controllers;
using System.Web.Mvc;

namespace ShopBike.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}