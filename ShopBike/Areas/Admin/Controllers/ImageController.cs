using Models.DAO;
using Models.EF;
using ShopPhone.Areas.Admin.Controllers;
using System.Web.Mvc;

namespace ShopBike.Areas.Admin.Controllers
{
    public class ImageController : BaseController
    {
        private ImageDAO dao = new ImageDAO();
        private ProductDAO proDao = new ProductDAO();

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var model = dao.ListAllPaging(searchString, page, pageSize);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var selectList = new SelectList(proDao.GetAll(), "ID", "Name", 1);
            ViewData["Products"] = selectList;
            return View();
        }

        public ActionResult Edit(int id)
        {
            var selectList = new SelectList(proDao.GetAll(), "ID", "Name", 1);
            ViewData["Products"] = selectList;
            var Image = new ImageDAO().GetById(id);
            return View(Image);
        }

        [HttpPost]
        public ActionResult Create(Image Image)
        {
            if (ModelState.IsValid)
            {
                var id = dao.Create(Image);
                if (id)
                {
                    return RedirectToAction("Index", "Image");
                }
                else
                {
                    ModelState.AddModelError("", "Add Image successfully");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var item = dao.GetById(id);
            dao.Delete(item);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Image Image)
        {
            if (ModelState.IsValid)
            {
                var result = dao.Update(Image);
                if (result)
                {
                    return RedirectToAction("Index", "Image");
                }
                else
                {
                    ModelState.AddModelError("", "Modify successfully");
                }
            }
            return View("Index");
        }
    }
}