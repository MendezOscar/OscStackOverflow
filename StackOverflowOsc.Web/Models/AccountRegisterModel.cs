using System.ComponentModel.DataAnnotations;

namespace StackOverflowOsc.Web.Controllers
{
    public class AccountRegisterModel 
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ComfirmPassword { get; set; }

    }
}