using Models.DAO;
using ShopPhone.Areas.Admin.Models;
using ShopPhone.Common;
using System.Web.Mvc;

namespace ShopBike.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private AccountDAO dao = new AccountDAO();

        public ActionResult Index()
        {
            var model = new LoginModel();
            return View(model);
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = dao.Login(model.Email, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByEmail(model.Email);
                    var userSession = new EmployeeLogin();
                    Session["username"] = user.FullName;
                    userSession.EmployeeName = user.FullName;
                    userSession.Email = user.Email;
                    Session.Add(CommonConstants.EMPLOYEE_SESSION, userSession);
                    Session["Account"] = user.FullName;
                    return Content("<script>window.location = '/Admin/Home';</script>");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản này không tồn tại!");
                }
                else if (result == -2)
                {
                    Session.Clear();
                    var user = dao.GetByEmail(model.Email);
                    Session["user"] = user;
                    Session["username"] = user.FullName;
                    Session.Add(CommonConstants.USER_SESSION, user);
                    return Redirect("/Home");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Sai mật khẩu hoặc email");
                }
            }
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}