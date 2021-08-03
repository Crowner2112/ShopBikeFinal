using Models.DAO;
using Models.EF;
using ShopPhone.Areas.Admin.Controllers;
using System.Web.Mvc;

namespace ShopBike.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private CategoryDAO dao = new CategoryDAO();

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var model = dao.ListAllPaging(searchString, page, pageSize);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var Category = dao.GetById(id);
            return View(Category);
        }

        [HttpPost]
        public ActionResult Edit(Category Category)
        {
            if (ModelState.IsValid)
            {
                var result = dao.Update(Category);
                if (result)
                {
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Modify successfully");
                }
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult Create(Category Category)
        {
            if (ModelState.IsValid)
            {
                var id = dao.Create(Category);
                if (id)
                {
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Add Category successfully");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var item = dao.GetById(id);
            if (dao.Delete(item))
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Delete Category successfully");
                return RedirectToAction("Index");
            }
        }
    }
}