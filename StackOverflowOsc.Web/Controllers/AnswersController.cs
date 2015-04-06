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
            var anws = UnitOfWork.QuestionRepository.GetEntityById(questionId);
            List<AnswerListModel> models = new List<AnswerListModel>();
            TimeCalculator calculator = new TimeCalculator();
            Mapper.CreateMap<Answer, AnswerListModel>();
            foreach (Answer a in anws.Answers)
            {
                var answer = Mapper.Map<Answer, AnswerListModel>(a);
                answer.OwnerName = UnitOfWork.AccountRepository.GetEntityById(answer.AccountId).Name;
                answer.LastName = UnitOfWork.AccountRepository.GetEntityById(answer.AccountId).LastName;
                var account = UnitOfWork.AccountRepository.GetEntityById(a.AccountId);
                answer.OwnerName = account.Name;
                answer.Date = calculator.GetTime(a.CreationDate);
                models.Add(answer);
            }
            return View(models);
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
            var question = UnitOfWork.QuestionRepository.GetEntityById(questionId);
            question.AnswerCount += 1;

            answer.AccountId = Guid.Parse(HttpContext.User.Identity.Name);
            UnitOfWork.QuestionRepository.Update(question);
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

        public ActionResult VotePlus(Guid answerId)
        {
            var answer = UnitOfWork.AnswerRepository.GetEntityById(answerId);
            answer.Votes++;
            UnitOfWork.AnswerRepository.Update(answer);
            UnitOfWork.Save();
            return RedirectToAction("ShowQuestion", "Question", new { questionId = answer.QuestionId });
        }

        public ActionResult VoteLess(Guid answerId)
        {
            var answer = UnitOfWork.AnswerRepository.GetEntityById(answerId);
            answer.Votes--;
            UnitOfWork.AnswerRepository.Update(answer);
            UnitOfWork.Save();
            return RedirectToAction("ShowQuestion", "Question", new { questionId = answer.QuestionId });
        }

        public ActionResult IsCorrect(AnswerListModel model)
        {
            var question = UnitOfWork.QuestionRepository.GetEntityById(model.QuestionId);
            var userId = Guid.Parse(HttpContext.User.Identity.Name);
            if (model.AccountId == userId)
            {
                if (model.Correct)
                {
                    question.State = false;
                    UnMark(model);
                }
                else if (!model.Correct)
                {
                    Mark(model);
                    question.State= true;
                }
                UnitOfWork.QuestionRepository.Update(question);
                UnitOfWork.Save();
            }

            return RedirectToAction("ShowQuestion", "Question", new { questionId = model.QuestionId });
        }

        private void Mark(AnswerListModel model)
        {
            Mapper.CreateMap<AnswerListModel, Answer>();
            var answer = Mapper.Map<AnswerListModel, Answer>(model);
            answer.Correct = true;
            UnitOfWork.AnswerRepository.Update(answer);
        }

        private void UnMark(AnswerListModel model)
        {
            Mapper.CreateMap<AnswerListModel, Answer>();
            var answer = Mapper.Map<AnswerListModel, Answer>(model);
            answer.Correct = false;
            UnitOfWork.AnswerRepository.Update(answer);
        }
	}
}