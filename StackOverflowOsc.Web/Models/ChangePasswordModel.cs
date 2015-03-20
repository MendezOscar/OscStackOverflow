using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using StackOverflowOsc.Web.CostumeDataNotations;

namespace StackOverflowOsc.Web.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(16, ErrorMessage = "The password must be between 8 and 16 characters", MinimumLength = 8)]
        [VocalAtTribute]
        [ContainsNumber]
        [RepeatedLetters]
        [LettersNumber]
        [CapitalLetter(ErrorMessage = "The password must be between 8 and 16 characters")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(16, ErrorMessage = "The password must be between 8 and 16 characters", MinimumLength = 8)]
        [VocalAtTribute]
        [ContainsNumber]
        [RepeatedLetters]
        [LettersNumber]
        [CapitalLetter(ErrorMessage = "The password must be between 8 and 16 characters")]
        public string ComfirmPassword { get; set; }

        public Guid OwnerId { get; set; }
    }
}