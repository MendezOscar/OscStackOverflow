using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AnswersController : Controller
    {
        public UnitOfWork UnitOfWork = new UnitOfWork();

        private readonly IMappingEngine _mappingEngine;
        public AnswersController(IMappingEngine mappingEngine)
        {
            _mappingEngine = mappingEngine;
        }
        //
        // GET: /Answers/
        public ActionResult Index(Guid questionId)
        {
            var quest = UnitOfWork.QuestionRepository.GetEntityById(questionId);
            List<AnswerListModel> modelAnswer = new List<AnswerListModel>();
            Mapper.CreateMap<Answer, AnswerListModel>();
            foreach (Answer a in quest.Answers)
            {
                var answer = Mapper.Map<Answer, AnswerListModel>(a);
                var account = UnitOfWork.AccountRepository.GetEntityById(a.AccountId);
                answer.OwnerName = account.Name;
                modelAnswer.Add(answer);
            }
            return View(modelAnswer);
        }

        public ActionResult CreateAnswer()
        {
            return View(new CreateAnswerModel());
        }

        [HttpPost]
        public ActionResult CreateAnswer(CreateAnswerModel modelAnswer, Guid questionId)
        {
            Mapper.CreateMap<CreateAnswerModel, Answer>().ReverseMap();
            var answer = Mapper.Map<CreateAnswerModel, Answer>(modelAnswer);
            answer.QuestionId = questionId;
            answer.AccountId = Guid.Parse(HttpContext.User.Identity.Name);
            UnitOfWork.AnswerRepository.InsertEntity(answer);
            UnitOfWork.Save();
            return RedirectToAction("Index", "Question");
        }

        [AllowAnonymous]
        public ActionResult ViewAnswer(Guid answerId)
        {
            Mapper.CreateMap<Answer, ShowAnswerModel>();
            var question = UnitOfWork.QuestionRepository.GetEntityById(answerId);
            var owner = UnitOfWork.AccountRepository.GetEntityById(question.Owner);
            var modelAnswer = Mapper.Map<Question, ShowQuestionModel>(question);
            return View(modelAnswer);
        }

        public ActionResult VotePlus(Guid id)
        {
            var answer = UnitOfWork.AnswerRepository.GetEntityById(id);
            answer.Votes++;
            UnitOfWork.AnswerRepository.Update(answer);
            UnitOfWork.Save();
            return RedirectToAction("ViewAnswer", new { id = id });
        }

        public ActionResult VoteLess(Guid id)
        {
            var answer = UnitOfWork.AnswerRepository.GetEntityById(id);
            answer.Votes--;
            UnitOfWork.AnswerRepository.Update(answer);
            UnitOfWork.Save();
            return RedirectToAction("ViewAnswer", new { id = id });
        }

        public ActionResult IsCorrect(Guid id, AnswerListModel model)
        {
            var question = UnitOfWork.QuestionRepository.GetEntityById(model.QuestionId);
            if (question.State == false)
            {
                Mapper.CreateMap<AnswerListModel, Answer>();
                question.State = true;
                var answer = Mapper.Map<AnswerListModel, Answer>(model);
                answer.Correct = true;
                UnitOfWork.QuestionRepository.Update(question);
                UnitOfWork.AnswerRepository.Update(answer);
                UnitOfWork.Save();
            }

            return RedirectToAction("ViewAnswer", new { id = id });
        }
	}
}