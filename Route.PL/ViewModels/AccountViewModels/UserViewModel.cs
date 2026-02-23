namespace Route.PL.ViewModels.AccountViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<string> Roles = new List<string>();
    }
}
