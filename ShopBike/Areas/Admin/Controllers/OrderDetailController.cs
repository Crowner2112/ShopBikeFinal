using Models.DAO;
using Models.EF;
using ShopPhone.Areas.Admin.Controllers;
using System.Web.Mvc;

namespace ShopBike.Areas.Admin.Controllers
{
    public class OrderDetailController : BaseController
    {
        private OrderDetailDAO dao = new OrderDetailDAO();
        private ProductDAO proDao = new ProductDAO();
        private static int currentId;

        public ActionResult Index(int orderID, int page = 1, int pageSize = 10)
        {
            currentId = orderID;
            ViewBag.OrderId = orderID;
            var model = dao.ListAllPaging(orderID, page, pageSize);
            return View(model);
        }

        public ActionResult Create()
        {
            var selectList = new SelectList(proDao.GetAll(), "ID", "Name", 1);
            ViewData["Products"] = selectList;
            ViewBag.OrderId = currentId;
            return View();
        }

        public ActionResult Edit(int orderID, int productID)
        {
            var Product = dao.GetListODByODIdAndPDId(orderID, productID);
            return View(Product);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(OrderDetail orderDetail)
        {
            orderDetail.OrderID = currentId;
            var id = dao.Create(orderDetail);
            if (id)
            {
                return RedirectToAction("Index", new { orderID = currentId });
            }
            else
            {
                ModelState.AddModelError("", "Add successfully");
            }
            return RedirectToAction("Index", new { orderID = currentId });
        }

        [HttpDelete]
        public ActionResult Delete(int orderId, int productId)
        {
            var item = dao.GetListODByODIdAndPDId(orderId, productId);
            if (dao.Delete(item))
            {
                return RedirectToAction("Index", new { orderID = currentId });
            }
            else
            {
                ModelState.AddModelError("", "Delete successfully");
                return RedirectToAction("Index", new { orderID = currentId });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                var result = dao.Update(orderDetail);
                if (result)
                {
                    return RedirectToAction("Index", new { orderID = currentId });
                }
                else
                {
                    ModelState.AddModelError("", "Modify successfully");
                }
            }
            return RedirectToAction("Index", new { orderID = currentId });
        }
    }
}