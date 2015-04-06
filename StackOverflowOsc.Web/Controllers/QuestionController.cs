
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
        public UnitOfWork UnitOfWork = new UnitOfWork();
        [AllowAnonymous]
        public ActionResult Index()
        {
            var questions = UnitOfWork.QuestionRepository.Get();
            var models = new List<QuestionListModel>();
            Mapper.CreateMap<Question, QuestionListModel>().ReverseMap();
            TimeCalculator calculator = new TimeCalculator();
            foreach (var q in questions)
            {
                var date = calculator.GetTime(q.CreationDate);
                var model = Mapper.Map<Question, QuestionListModel>(q);
                model.Date = date;
                model.OwnerId = q.Owner;
                model.OwnerName = UnitOfWork.AccountRepository.GetEntityById(model.OwnerId).Name;
                model.LastName = UnitOfWork.AccountRepository.GetEntityById(model.OwnerId).LastName;
                models.Add(model);
            }
            return View(models);
            
        }

        [HttpPost]
        public ActionResult AskQuestion(AskQuestionModel modelQuestion)
        {
            Mapper.CreateMap<AskQuestionModel, Question>().ReverseMap();
            var question = Mapper.Map<AskQuestionModel, Question>(modelQuestion);
            question.Owner = Guid.Parse(HttpContext.User.Identity.Name);
            UnitOfWork.QuestionRepository.InsertEntity(question);
            UnitOfWork.Save();
            return RedirectToAction("Index", "Question");
        }

        public ActionResult AskQuestion()
        {
            return View(new AskQuestionModel());
        }

        [AllowAnonymous]
        public ActionResult ShowQuestion(Guid questionId)
        {
            Mapper.CreateMap<Question, ShowQuestionModel>();
            var question = UnitOfWork.QuestionRepository.GetEntityById(questionId);
            question.Views += 1;
            var owner = UnitOfWork.AccountRepository.GetEntityById(question.Owner);
            var model = Mapper.Map<Question, ShowQuestionModel>(question);
            TimeCalculator calculator = new TimeCalculator();
            string date = calculator.GetTime(question.CreationDate);
            model.Date = date;
            UnitOfWork.QuestionRepository.Update(question);
            UnitOfWork.Save();
            model.OwnerEmail = owner.Email;
            model.Name = owner.Name;
            model.LastName = owner.LastName;
            return View(model);
        }

        public ActionResult VotePlus(Guid questId)
        {
            var ownerId = Guid.Parse(HttpContext.User.Identity.Name);

            var question = UnitOfWork.QuestionRepository.GetEntityById(questId);
            question.Votes += 1;
            UnitOfWork.QuestionRepository.Update(question);
            UnitOfWork.Save();
            return RedirectToAction("ShowQuestion", "Question", new { questionId = questId });
        }

        public ActionResult VoteLess(Guid questId)
        {
            var question = UnitOfWork.QuestionRepository.GetEntityById(questId);
            question.Votes -= 1;
            UnitOfWork.QuestionRepository.Update(question);
            UnitOfWork.Save();
            return RedirectToAction("ShowQuestion", "Question", new { questionId = questId });
        }
    }
}