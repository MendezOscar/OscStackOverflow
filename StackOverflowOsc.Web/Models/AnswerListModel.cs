using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverflowOsc.Web.Models
{
    public class AnswerListModel
    {
        public string Description { get; set; }
        public int Votes { get; set; }
        public Guid AccountId { get; set; }
        public Guid QuestionId { get; set; }
        public string OwnerName { get; set; }
        public string LastName { get; set; }
        public Guid Id { get; set; }
        public bool Correct { get; set; }
        public string Date { get; set; }
        public int Views { get; set; }
        
    }
}