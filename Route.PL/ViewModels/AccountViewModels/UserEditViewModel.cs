using System.ComponentModel.DataAnnotations;

namespace Route.PL.ViewModels.AccountViewModels
{
    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public ICollection<string> Roles = new List<string>();
    }
}
