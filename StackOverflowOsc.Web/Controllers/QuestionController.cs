
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
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
        //
        // GET: /Question/
        [AllowAnonymous]
        public ActionResult Index(QuestionListModel modelQuestion)
        {
            List<QuestionListModel> models = new List<QuestionListModel>();
            AutoMapper.Mapper.CreateMap<Question, QuestionListModel>().ReverseMap();
            var Context = new StackOverflowContext();
            var contats = from contact in Context.Questions select contact;

            foreach (var i in contats)
            {
                //var model = Mapper.Map < Question, QuestionListModel>(i);
                QuestionListModel question = new QuestionListModel();
                question.OwnerName = "Oscar";
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
            AutoMapper.Mapper.CreateMap<Question, AskQuestionModel>().ReverseMap();
            Question newQuestion = AutoMapper.Mapper.Map<AskQuestionModel, Question>(modelQuestion);
            newQuestion.Tittle = modelQuestion.Title;
            newQuestion.Description = modelQuestion.Description;
            var Context = new StackOverflowContext();
            Context.Questions.Add(newQuestion);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AskQuestion()
        {
            return View(new AskQuestionModel());
        }

        [HttpPost]
        public ActionResult ShowQuestion(Guid id)
        {
            AutoMapper.Mapper.CreateMap<Question, ShowQuestionModel>().ReverseMap();
            var Context = new StackOverflowContext();
            var QuestionShow = Context.Questions.Find(id);
            ShowQuestionModel newShowQuestion = AutoMapper.Mapper.Map<Question, ShowQuestionModel>(QuestionShow);

            return View(newShowQuestion);
        }

        [AllowAnonymous]
        public ActionResult ShowQuestion()
        {
            return View(new ShowQuestionModel());
        }

        public ActionResult Answers()
        {
            return View(new AnsWersModel());
        }
	}
}