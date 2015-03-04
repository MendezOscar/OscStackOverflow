
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Antlr.Runtime.Misc;
using AutoMapper;
using StackOverflow.data;
using StackOverflowOsc.Domain.Entities;
using StackOverflowOsc.Web.Models;

namespace StackOverflowOsc.Web.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly IMappingEngine _mappingEngine;
        public QuestionController(IMappingEngine mappingEngine)
        {
            _mappingEngine = mappingEngine;
        }
        //
        // GET: /Question/
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<QuestionListModel> models = new List<QuestionListModel>();
            var context = new StackOverflowContext();

            foreach (Question i in context.Questions)
            {
                QuestionListModel question = new QuestionListModel();
                question.OwnerName = i.Owner.Name;
                question.QuestionId = i.ID;
                question.OwnerId = Guid.NewGuid();
                question.CreationName = i.CreationDate;
                question.Tittle = i.Tittle;
                question.Tittle = i.Tittle ?? "Hola";
                models.Add(question);
            }

            return View(models);
            
        }

        [HttpPost]
        public ActionResult AskQuestion(AskQuestionModel modelQuestion)
        {
            if (ModelState.IsValid)
            {
                var context = new StackOverflowContext();
                var newQuestion = _mappingEngine.Map<AskQuestionModel, Question>(modelQuestion);
                HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                if (cookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    Guid ownerId = Guid.Parse(ticket.Name);
                    newQuestion.Votes = 0;
                    newQuestion.Owner = context.Accounts.FirstOrDefault(x => x.ID == ownerId);
                    newQuestion.CreationDate = DateTime.Now;
                    newQuestion.State = Guid.NewGuid();
                    context.Questions.Add(newQuestion);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(modelQuestion);
        }

        public ActionResult AskQuestion()
        {
            return View(new AskQuestionModel());
        }

        [AllowAnonymous]
        public ActionResult ShowQuestion(Guid id)
        {
            ShowQuestionModel modelShow = new ShowQuestionModel();
            var question = _mappingEngine.Map<ShowQuestionModel, Question>(modelShow);
            var context = new StackOverflowContext();
            modelShow.Title = context.Questions.FirstOrDefault(x => x.ID == id).Tittle;
            modelShow.Description = context.Questions.FirstOrDefault(x => x.ID == id).Description;
            modelShow.Votes = context.Questions.FirstOrDefault(x => x.ID == id).Votes;
            return View(modelShow);
        }
    }
}