using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverflowOsc.Web.Models
{
    public class AnswerListModel
    {
        public Guid AnswerId { get; set; }
        public string AnswerName { get; set; }
        public string OwnerName { get; set; }
        public Guid OwnerId { get; set; }
        public Guid QuestionId { get; set; }
        public int Votes { get; set; }
        public DateTime CreationTime { get; set; }      
        public bool Good { get; set; }
        
    }
}