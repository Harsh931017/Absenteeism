namespace Absenteeism.Models.Domain
{
    public class tblUser
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public int PlantId { get; set; }
    }
}
