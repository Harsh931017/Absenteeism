namespace Absenteeism.Models.Output
{
    public class GetCellMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public int PlantId { get; set; }
        public string Role { get; set; }
        public string Plant { get; set; }
    }
}
