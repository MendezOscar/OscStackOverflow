﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackOverflowOsc.Web.Models
{
    public class CreateAnswerModel
    {
        [Required]
        public string Description { get; set; }
    }
}