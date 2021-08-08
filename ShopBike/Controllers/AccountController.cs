using Models.DAO;
using Models.EF;
using ShopPhone.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBike.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountDAO dao = new AccountDAO();
        public ActionResult Index(int id)
        {
            var model = dao.GetById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(Account account)
        {
            if (ModelState.IsValid)
            {
                dao.Update(account);
                return new ContentResult
                {
                    Content = $"<script language='javascript' type='text/javascript'>alert('Cập nhật thành công!');location.href='/Account/Index/{account.ID}'</script>"
                };
            }
            else
            {
                return new ContentResult
                {
                    Content = $"<script language='javascript' type='text/javascript'>alert('Cập nhật thất bại!');location.href='/Account/Index/{account.ID}'</script>"
                };
            }
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(int id, string oldPass, string newPass)
        {
            var currentAccount = dao.GetById(id);
            oldPass = Encryptor.MD5Hash(oldPass);
            if(currentAccount.Password == oldPass)
            {
                newPass = Encryptor.MD5Hash(newPass);
                dao.ChangePasswordById(id, newPass);
                return new ContentResult
                {
                    Content = "<script language='javascript' type='text/javascript'>alert('Cập nhật mật khẩu thành công!');location.href='/Home'</script>"
                };
            }
            else
            {
                return new ContentResult
                {
                    Content = "<script language='javascript' type='text/javascript'>alert('Mật khẩu cũ sai!');location.href='/Account/ChangePassword'</script>"
                };
            }
        }
    }
}