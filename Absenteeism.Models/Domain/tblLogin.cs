namespace Absenteeism.Models.Domain
{
    public class tblLogin
    {
        public int Id { get; set; }
        public string UserNumber { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }

    }
}
