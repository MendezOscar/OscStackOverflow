using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackOverflowOsc.Web.Models
{
    public class AccountProfileModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Website { get; set; }
        public int QuestionsCount { get; set; }
        public int AnswerCount { get; set; }
        public string RegisterDate { get; set; }
        public int Views { get; set; }
        public string LastSeen { get; set; }
        public IEnumerable Answers { get; set; }
        public IEnumerable Questions { get; set; }

    }
}