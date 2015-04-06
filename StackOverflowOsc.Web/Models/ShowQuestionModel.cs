using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using StackOverflowOsc.Domain.Entities;

namespace StackOverflowOsc.Web.Models
{
    public class ShowQuestionModel
    {
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Vote> Voters { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerEmail { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Guid OwnerId { get; set; }
        public int Votes { get; set; }
        public Guid Id { get; set; }
        public string Date { get; set; }
        public int Views { get; set; }

    }
}