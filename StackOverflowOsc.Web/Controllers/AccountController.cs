using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        readonly MailGun _email = new MailGun();

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
                var validateEmailAccount = UnitOfWork.AccountRepository.GetWithFilter(x => x.Email == model.Email);
                if (validateEmailAccount != null)
                {
                    TempData["Error"] = "El usuarion con el correo electronico: " + model.Email + " ya existe";
                    return View(model);
                }
                if (model.Password == model.ComfirmPassword)
                {
                    Mapper.CreateMap<AccountRegisterModel, Account>();
                    var newAccount = _mappingEngine.Map<AccountRegisterModel, Account>(model);
                    UnitOfWork.AccountRepository.InsertEntity(newAccount);
                    UnitOfWork.Save();

                    var host = HttpContext.Request.Url.Host;
                    if (host == "localhost")
                        host = Request.Url.GetLeftPart(UriPartial.Authority);
                    _email.SendWelcomeMessage(newAccount.Name, newAccount.Email,
                        host + "/Account/ConfirmRegistration/" + newAccount.Id.ToString());

                    TempData["Success"] = "An email has been sent, You need to confirm your account!";
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("Error", "Password and Confirm Passsword must be the same");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(AccountLoginModel modelLogin)
        {
            if (modelLogin.Email != null && modelLogin.Password != null)
            {
                var a = modelLogin.LoginTimes;
                var validateEmail = UnitOfWork.AccountRepository.GetWithFilter(x => x.Email == modelLogin.Email);
                //si el correo existe
                if (validateEmail != null)
                {
                    //si la contraseña es correcta
                    if (validateEmail.Password == modelLogin.Password)
                    {
                        if (validateEmail.Active == false)
                        {
                            TempData["Error"] = "Account is not confirmed yet, please confirm your account";
                            return View(new AccountLoginModel());
                        }
                        FormsAuthentication.SetAuthCookie(validateEmail.Id.ToString(), false);
                        return RedirectToAction("Index", "Question");
                    }
                    //si la contraseña es incorrecta
                    _email.SendLoginWarningMessage(validateEmail.Name, validateEmail.Email);
                    TempData["Error"] = "password invalid";
                    int ses = (int)(Session["Attempts"]);
                    ses += 1;
                    Session["Attempts"] = ses;
                    if (ses == 3)
                    {
                        Session["Attempts"] = 0;
                        modelLogin.CaptchaActive = true;
                    }
                    TempData["Error"] = "password invalid";
                    return View(modelLogin);
                }
                
                TempData["Error"] = "Email or password invalid";
            }
            return View(new AccountLoginModel());
        }

        public ActionResult Login()
        {
            const int n = 0;
            Session["Attempts"] = n;
            return View(new AccountLoginModel());
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Question");
        }

        public ActionResult Profile(Guid ownerId)
        {
            var owner = UnitOfWork.AccountRepository.GetEntityById(ownerId);
            UnitOfWork.AccountRepository.Load(owner, "Questions");
            UnitOfWork.AccountRepository.Load(owner, "Answers");
            Mapper.CreateMap<Account, AccountProfileModel>();
            owner.Views += 1;
            owner.LastSeen = DateTime.Now;
            TimeCalculator calculator = new TimeCalculator();
            owner.Questions = owner.Questions.OrderByDescending(x => x.CreationDate).ToList();
            owner.Answers = owner.Answers.OrderByDescending(x => x.CreationDate).ToList();
            UnitOfWork.AccountRepository.Update(owner);
            UnitOfWork.Save();
            var model = Mapper.Map<Account, AccountProfileModel>(owner);
            model.LastSeen = calculator.GetTime(DateTime.Now);
            model.RegisterDate = calculator.GetTime(DateTime.Now);
            return View(model);
        }

        public ActionResult ChangePassword(Guid id)
        {
            var model = new ChangePasswordModel() { OwnerId = id };
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model, Guid id)
        {
            var account = UnitOfWork.AccountRepository.GetEntityById(id);
            if (ModelState.IsValid)
            {
                if (model.Password == model.ComfirmPassword)
                {
                    account.Password = model.Password;
                    UnitOfWork.AccountRepository.Update(account);
                    UnitOfWork.Save();
                    TempData["Success"] = "Your password has been Updated";
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("Error", "Password and Confirm Passsword must be the same");
            }
            return View(model);

        }

        public ActionResult PasswordRecovery()
        {
            return View(new ForgotPasswordModel());
        }

        [HttpPost]
        public ActionResult PasswordRecovery(ForgotPasswordModel modelPass)
        {
            if (ModelState.IsValid)
            {
                var account = UnitOfWork.AccountRepository.GetWithFilter(x => x.Email == modelPass.Email);
                if (account != null)
                {
                    var host = HttpContext.Request.Url.Host;
                    if (host == "localhost")
                        host = Request.Url.GetLeftPart(UriPartial.Authority);
                    _email.SendRecoveryEmail(account.Name, account.Email, host + "/Account/ChangePassword/?id=" + account.Id);

                    TempData["Success"] = "An email has been sent with instructions to recover your password.";
                    return View(modelPass);
                }
                ModelState.AddModelError("AccountError", "The user with the email:" + modelPass.Email + " does not exist");
            }
            return View(modelPass);
        }

        public ActionResult ConfirmRegistration(Guid id)
        {
            var account = UnitOfWork.AccountRepository.GetEntityById(id);
            account.Active = true;
            UnitOfWork.AccountRepository.Update(account);
            UnitOfWork.Save();
            TempData["Success"] = "Registration has been successful";
            return RedirectToAction("Login");
        }
    }
}
