namespace Absenteeism.Models.Input
{
    public class InputUpdatePassword
    {
        public int UserId { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public int TokenId { get; set; }
    }
}
