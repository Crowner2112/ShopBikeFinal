using Models.DAO;
using Models.EF;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopBike.Controllers
{
    public class HomeController : Controller
    {
        private CategoryDAO categoryDao = new CategoryDAO();
        private ProductDAO productDao = new ProductDAO();
        private ImageDAO imageDao = new ImageDAO();

        public ActionResult Index()
        {
            var categories = categoryDao.GetAll();
            Session["Categories"] = categories;
            var products = new List<Product>();
            var cutListCategory = categories.Take(3).ToList();
            ViewData["CutListCategory"] = cutListCategory;
            foreach (var category in cutListCategory)
            {
                var product = productDao.GetAll().Where(x => x.CategoryID == category.ID).Take(3).ToList();
                products.AddRange(product);
            }
            var images = new List<Image>();
            foreach (var product in products)
            {
                var image = imageDao.GetAll().SingleOrDefault(x => x.ProductID == product.ID && x.MainPic == true);
                images.Add(image);
            }
            ViewData["Images"] = images;
            return View();
        }
    }
}