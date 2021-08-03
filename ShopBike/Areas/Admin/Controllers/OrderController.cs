using Models.DAO;
using Models.EF;
using ShopPhone.Areas.Admin.Controllers;
using System;
using System.Web.Mvc;

namespace ShopBike.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        private OrderDAO dao = new OrderDAO();

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var model = dao.ListAllPaging(searchString, page, pageSize);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var selectList = new SelectList(new[]{
                new {ID = 0, Name = "Đã hủy"},
                new {ID = 1, Name = "Đang xử lý" },
                new{ID = 2, Name = "Đã thanh toán"}
            }, "ID", "Name", 1);
            ViewData["Status"] = selectList;
            return View();
        }

        public ActionResult Edit(int id)
        {
            var selectList = new SelectList(new[]{
                new {ID = 0, Name = "Đã hủy"},
                new {ID = 1, Name = "Đang xử lý" },
                new{ID = 2, Name = "Đã thanh toán"}
            }, "ID", "Name", 1);
            ViewData["Status"] = selectList;
            var Order = dao.GetById(id);
            return View(Order);
        }

        [HttpPost]
        public ActionResult Create(Order Order)
        {
            if (ModelState.IsValid)
            {
                Order.CreatedDate = DateTime.Now;
                var id = dao.Create(Order);
                if (id)
                {
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    ModelState.AddModelError("", "Add Order successfully");
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
                ModelState.AddModelError("", "Add Order successfully");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Order Order)
        {
            if (ModelState.IsValid)
            {
                var result = dao.Update(Order);
                if (result)
                {
                    return RedirectToAction("Index", "Order");
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