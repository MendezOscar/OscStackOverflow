using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackOverflowOsc.Web.Models
{
    public class AccountLoginModel
    {
        public AccountLoginModel()
        {
            CaptchaActive = false;
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool CaptchaActive { get; set; }
        public int LoginTimes { get; set; }

    }
}