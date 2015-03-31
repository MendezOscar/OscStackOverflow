using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackOverflowOsc.Web.Models
{
    public class ShowQuestionModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { set; get; }
        public int Votes { get; set; }
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}