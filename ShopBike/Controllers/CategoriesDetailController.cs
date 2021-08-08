using Models.DAO;
using System.Linq;
using System.Web.Mvc;

namespace ShopBike.Controllers
{
    public class CategoriesDetailController : Controller
    {
        private ImageDAO dao = new ImageDAO();

        public ActionResult Index(int id)
        {
            var model = dao.GetByCategoryId(id);
            ViewBag.CategoryName = model.FirstOrDefault().Product.Category.Name.ToLower();
            return View(model);
        }
    }
}