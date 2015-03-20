
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
            var modelQuestion = new List<QuestionListModel>();
            Mapper.CreateMap<Question, QuestionListModel>().ReverseMap();
            foreach (var q in questions)
            {
                var modelQ = Mapper.Map<Question, QuestionListModel>(q);
                modelQ.OwnerId = q.Owner;
                modelQ.OwnerName = UnitOfWork.AccountRepository.GetEntityById(modelQ.OwnerId).Name;
                modelQuestion.Add(modelQ);
            }
            return View(modelQuestion);
            
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
            var owner = UnitOfWork.AccountRepository.GetEntityById(question.Owner);
            var model = Mapper.Map<Question, ShowQuestionModel>(question);
            return View(model);
        }

        public ActionResult Vote(Guid id)
        {
            var question = UnitOfWork.QuestionRepository.GetEntityById(id);
            question.Votes++;
            UnitOfWork.QuestionRepository.Update(question);
            UnitOfWork.Save();
            return RedirectToAction("ShowQuestion", new { Id = id });
        }
    }
}