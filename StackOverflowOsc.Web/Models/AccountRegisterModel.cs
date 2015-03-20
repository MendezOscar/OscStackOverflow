using System.ComponentModel.DataAnnotations;
using StackOverflowOsc.Web.CostumeDataNotations;

namespace StackOverflowOsc.Web.Controllers
{
    public class AccountRegisterModel 
    {
        [Required]
        [StringLength(50, ErrorMessage = "The Name must be between 2 and 50 characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The LastName must be between 2 and 50 characters", MinimumLength = 2)]
        public string LastName { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "The password must be between 8 and 16 characters", MinimumLength = 8)]
        [VocalAtTribute]
        [ContainsNumber]
        [RepeatedLetters]
        [LettersNumber]
        [CapitalLetter(ErrorMessage = "must contain at least one Capital Letter")] 
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "The password must be between 8 and 16 characters", MinimumLength = 8)]
        [VocalAtTribute]
        [ContainsNumber]
        [RepeatedLetters]
        [LettersNumber]
        [CapitalLetter(ErrorMessage = "must contain at least one Capital Letter")] 
        [DataType(DataType.Password)]
        public string ComfirmPassword { get; set; }

    }
}