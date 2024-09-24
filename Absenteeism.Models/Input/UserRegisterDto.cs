namespace Absenteeism.Models.Input
{
    public class UserRegisterDto
    {
        public string UserNumber { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
