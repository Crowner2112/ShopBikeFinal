using Models.DAO;
using Models.EF;
using ShopBike.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ShopBike.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            Cart cart = Session["Cart"] as Cart;
            Session["Total"] = 0;
            if (cart != null)
            {
                Session["Total"] = cart.Total();
            }
            return View(cart);
        }

        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult AddToCart(int id)
        {
            var pro = new ProductDAO().GetById(id);
            var image = new ImageDAO().GetMainPicByProID(id);
            GetCart().Add(pro.ID, pro.Name, image.Link, pro.Price);
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult UpdateQuantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int idPro = int.Parse(form["ID_Product"]);
            int quantity = int.Parse(form["Quantity"]);
            var pro = new ProductDAO().GetById(idPro);
            cart.Update(idPro, quantity, pro.Price * quantity);
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult Delete(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Delete(id);
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult Payment(int? id)
        {
            if (id.HasValue)
            {
                int userID = id.Value;
                AccountDAO dao = new AccountDAO();
                var user = dao.GetById(userID);
                return View(user);
            }
            else
            {
                Account newAccount = new Account();
                return View(newAccount);
            }
        }

        [HttpPost]
        public ActionResult Payment(Account account, int total)
        {
            var orderDao = new OrderDAO();
            var orderDetailDao = new OrderDetailDAO();
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.Status = 1;
            order.Total = total;
            order.CustomerName = account.FullName;
            order.Email = account.Email;
            order.Address = account.Address;
            order.PhoneNumber = account.PhoneNumber;
            var idOrder = orderDao.CreateReturnId(order);
            if (idOrder >= 0)
            {
                Cart cart = Session["Cart"] as Cart;
                var listCart = cart.Items.ToList();
                foreach (var item in listCart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.OrderID = idOrder;
                    orderDetail.ProductID = item.ProductID;
                    orderDetail.Quantity = item.Quantity;
                    orderDetailDao.Create(orderDetail);
                }
                Session["Cart"] = new Cart();
                return new ContentResult()
                {
                    Content = "<script language='javascript' type='text/javascript'>alert('Đơn hàng được tạo thành công! Cảm ơn quý khách hàng!');location.href='/Home'</script>"
                };
            }
            else
            {
                return new ContentResult()
                {
                    Content = "<script language='javascript' type='text/javascript'>alert('Đơn hàng tạo thất bại!');location.href='/Home'</script>"
                };
            }
        }
    }
}