using System.ComponentModel.DataAnnotations;

namespace Route.PL.ViewModels.AccountViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email can't be empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
