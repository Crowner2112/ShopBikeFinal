using Models.DAO;
using ShopBike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            Session["CountCart"] = cart.Items.Count();
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            var pro = new ProductDAO().GetById(id);
            var image = new ImageDAO().GetMainPicByProID(id);
            var result = GetCart().Add(pro.ID, pro.Name, image.Link, pro.Price);
            if (result)
            {
                Session["CountCart"] = (int)Session["CountCart"] + 1;
            }
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
    }
}