using System.ComponentModel.DataAnnotations;

namespace Route.PL.ViewModels.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }
    }
}
