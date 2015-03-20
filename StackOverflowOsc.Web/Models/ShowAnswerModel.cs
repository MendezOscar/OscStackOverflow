using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackOverflowOsc.Web.Models
{
    public class ShowAnswerModel
    {
        [Required]
        public string Description { get; set; }
        public int Votes { get; set; }
        public DateTime CreationDate { get; set; }
        public string OwnerName { get; set; }
        public Guid Id { get; set; }
        public bool State { get; set; }
    }
}