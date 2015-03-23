using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using StackOverflow.data;
using StackOverflowOsc.Domain.Entities;
using StackOverflowOsc.Web.Models;

namespace StackOverflowOsc.Web.Controllers
{
    public class AccountController : Controller
    {
        public UnitOfWork UnitOfWork = new UnitOfWork();
        private readonly IMappingEngine _mappingEngine;
        public AccountController(IMappingEngine mappingEngine)
        {
            _mappingEngine = mappingEngine;
        }

        public ActionResult Register()
        {
            return View(new AccountRegisterModel());
        }

        [HttpPost]
        public ActionResult Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password == model.ComfirmPassword)
                {
                    Mapper.CreateMap<AccountRegisterModel, Account>();
                    var newAccount = _mappingEngine.Map<AccountRegisterModel, Account>(model);
                    UnitOfWork.AccountRepository.InsertEntity(newAccount);
                    UnitOfWork.Save();
                    return RedirectToAction("Login");
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(AccountLoginModel modelLogin)
        {
            if (ModelState.IsValid)
            {
                var account = UnitOfWork.AccountRepository.GetWithFilter(x => x.Email == modelLogin.Email && x.Password == modelLogin.Password);
                if (account != null)
                {
                    FormsAuthentication.SetAuthCookie(modelLogin.Email, false);
                    return RedirectToAction("Index", "Question");
                }
            }
            ViewBag.Message = "Invalid email or password ";
            return View(modelLogin);
        }

        public ActionResult Login()
        {
            return View(new AccountLoginModel());
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Question");
        }

        [Authorize] 
        public ActionResult Profile(Guid id)
        {
            Mapper.CreateMap<Account, AccountProfileModel>();
            var owner = UnitOfWork.AccountRepository.GetEntityById(id);
            var model = Mapper.Map<Account, AccountProfileModel>(owner);
            return View(model);
        }

        public ActionResult ChangePassword(Guid id)
        {
            var model = new ChangePasswordModel() { OwnerId = id };
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var account = UnitOfWork.AccountRepository.GetEntityById(model.OwnerId);
            if (ModelState.IsValid)
            {
                if (model.Password == model.ComfirmPassword)
                {
                    account.Password = model.Password;
                    UnitOfWork.AccountRepository.Update(account);
                    UnitOfWork.Save();
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("Error", "Password and Confirm Passsword must be the same");
            }
            return View(model);

        }

        public ActionResult PassWordRecovery()
        {
            return View(new ChangePasswordModel());
        }

        [HttpPost]
        public ActionResult ForgotPasswordRecovery(ForgotPasswordModel modelPass)
        {
            
            return RedirectToAction("Login");
        }
    }
}
