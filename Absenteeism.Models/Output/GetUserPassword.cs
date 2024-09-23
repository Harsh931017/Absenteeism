namespace Absenteeism.Models.Output
{
    public class GetUserPassword
    {
        public int Id { get; set; }
        public string UserNumber { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
