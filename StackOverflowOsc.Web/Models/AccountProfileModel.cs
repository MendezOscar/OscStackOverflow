using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackOverflowOsc.Web.Models
{
    public class AccountProfileModel
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Badges { get; set; }
        public int Reputation { get; set; }
        public int Questions { get; set; }
        public int Answers { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Views { get; set; }
        public DateTime LastSeen { get; set; }

    }
}