using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverflowOsc.Web.Models
{
    public class ShowQuestionModel
    {
        public string Title { get; set; }
        public string Description { set; get; }
        public int Votes { get; set; }
    }
}