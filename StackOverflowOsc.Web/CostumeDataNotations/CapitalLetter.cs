using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackOverflowOsc.Web.CostumeDataNotations
{
    public class CapitalLetter : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var stringValue = (string)value;
                var string2 = stringValue.ToLower();
                if (stringValue.Equals(string2))
                    return false;
                return true;
            }
            return false;
        }
    }

}