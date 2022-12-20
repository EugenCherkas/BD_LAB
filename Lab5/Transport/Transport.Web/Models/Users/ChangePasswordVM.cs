namespace Transport.Web.Models.Users
{
    public class ChangePasswordVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}