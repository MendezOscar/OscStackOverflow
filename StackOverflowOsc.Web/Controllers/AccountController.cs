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
        private readonly IMappingEngine _mappingEngine;
        public AccountController(IMappingEngine mappingEngine)
        {
            _mappingEngine = mappingEngine;
        }
        public ActionResult Register()
        {
            return View(new AccountRegisterModel());
        }


        public ActionResult ForgotPassword()
        {
            return View(new AccountForgotPasswordModel());
        }

        [HttpPost]
        public ActionResult ForgotPassword(AccountForgotPasswordModel modelPass)
        {
            //MailMessage mail = new MailMessage("oscarito16m@gmail.com", modelPass.Email);
            //SmtpClient client = new SmtpClient();
            //client.Port = 25;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.Credentials = new System.Net.NetworkCredential("oscarito16m@gmail.com", "oscar16m");
            //client.Host = "smtp.gmail.com";
            //mail.Subject = "this is a test email.";
            //mail.Body = "this is my test email body";
            //client.Send(mail);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var newAccount = _mappingEngine.Map<AccountRegisterModel, Account>(model);
                var context = new StackOverflowContext();
                context.Accounts.Add(newAccount);
                context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(AccountLoginModel modelLogin)
        {
            var Context = new StackOverflowContext();
            var account = Context.Accounts.FirstOrDefault(x=>x.Email == modelLogin.Email && x.Password==modelLogin.Password);
            if (account != null)
            {
                FormsAuthentication.SetAuthCookie(modelLogin.Email, false);
                return RedirectToAction("Index", "Question");
            }
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
        public ActionResult Profile(AccountProfileModel modelPro, Guid id)
        {
            var context = new StackOverflowContext();
            modelPro.Name = context.Accounts.FirstOrDefault(x => x.ID == id).Name;
            modelPro.Email = context.Accounts.FirstOrDefault(x => x.ID == id).Email;
            modelPro.Reputation = 0;
            modelPro.Badges = "Curious";
            
            return View(new AccountProfileModel());
        }

        public ActionResult PassWordRecovery()
        {
            return View(new ForgotPasswordRecoveryModel());
        }

        [HttpPost]
        public ActionResult ForgotPasswordRecovery(AccountForgotPasswordModel modelPass)
        {
            
            return RedirectToAction("Login");
        }
    }
}
