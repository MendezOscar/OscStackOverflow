using AutoMapper;
using StackOverflowOsc.Domain.Entities;
using StackOverflowOsc.Web.Controllers;
using StackOverflowOsc.Web.Models;

namespace StackOverflowOsc.Web
{
    public class AutoMapperConfig
    {
        public static void RegisterMaps()
        {
            Mapper.CreateMap<AccountRegisterModel, Account>().ReverseMap();
            Mapper.CreateMap<AccountLoginModel, Account>().ReverseMap();
            Mapper.CreateMap<AskQuestionModel, Question>().ReverseMap();
            Mapper.CreateMap<AnsWersModel, Answer>().ReverseMap();
            Mapper.CreateMap<ShowQuestionModel, Question>().ReverseMap();
        }
    }
}