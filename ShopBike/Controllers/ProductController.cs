using Models.DAO;
using System.Linq;
using System.Web.Mvc;

namespace ShopBike.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDAO productDao = new ProductDAO();
        private readonly ImageDAO imageDao = new ImageDAO();

        public ActionResult Index(int id)
        {
            var images = imageDao.GetByProductId(id);
            ViewData["MainPic"] = images.FirstOrDefault(x => x.MainPic == true);
            ViewData["PartialPics"] = images.Where(x => x.MainPic != true);
            var model = productDao.GetById(id);
            return View(model);
        }
    }
}