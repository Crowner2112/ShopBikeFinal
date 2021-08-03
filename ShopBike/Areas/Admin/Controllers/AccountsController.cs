using Models.DAO;
using Models.EF;
using ShopPhone.Areas.Admin.Controllers;
using ShopPhone.Common;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopBike.Areas.Admin.Controllers
{
    public class AccountsController : BaseController
    {
        private AccountDAO dao = new AccountDAO();

        // GET: Admin/Accounts
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var model = dao.ListAllPaging(searchString, page, pageSize);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {
                account.Password = Encryptor.MD5Hash(account.Password);
                var id = dao.Create(account);
                if (id)
                {
                    return RedirectToAction("Index", "Accounts");
                }
                else
                {
                    ModelState.AddModelError("", "Add Account successfully");
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Admin/Accounts/Edit/5
        public ActionResult Edit(int id)
        {
            var acc = dao.GetById(id);
            return View(acc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FullName,Email,DOB,Address,Password,PhoneNumber,Role")] Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (dao.Update(account))
                    {
                        return RedirectToAction("Index", "Accounts");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update Account successfully");
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View(account);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var item = dao.GetById(id);
            if (dao.Delete(item))
            {
                return RedirectToAction("Index", "Accounts");
            }
            else
            {
                ModelState.AddModelError("", "Update Account successfully");
                return RedirectToAction("Index", "Accounts");
            }
        }

        public static IEnumerable<SelectListItem> GetRole()
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem{Text = "Admin", Value = "1" },
                new SelectListItem{Text = "Khách hàng", Value = "0" }
            };
            return items;
        }
    }
}