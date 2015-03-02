using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace StackOverflowOsc.Web.Models
{
    public class QuestionListModel
    {
        public string Tittle { get; set; }
        public int Votes { get; set; }
        public string OwnerName { get; set; }
        public DateTime CreationName { get; set; }
        public Guid OwnerId { get; set; }
        public Guid QuestionId { get; set; }
    }
}