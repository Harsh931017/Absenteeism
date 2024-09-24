namespace Absenteeism.Models.Input
{
    public class InputCheckToken
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public string confirmPassword { get; set; }
        public string newPassword { get; set; }
    }
}
