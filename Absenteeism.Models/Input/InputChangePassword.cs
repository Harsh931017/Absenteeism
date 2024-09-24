namespace Absenteeism.Models.Input
{
    public class InputChangePassword
    {
        public string UserNumber { get; set; }
        public string PreviousPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class ChangePassword
    {
        public int UserId{ get; set; }
        public string PasswordHash{ get; set; }
        public string PasswordSalt{ get; set; }
    }
}
