namespace Solvintech.API.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string AccessToken { get; set; }
    }
}
