namespace Absenteeism.Models.Input
{
    public class InputAllocation
    {
        public int Id { get; set; }
        public int[] CellMemberIds { get; set; }
        public int DepartmentId { get; set; }
        public int StationId { get; set; }
        public int ShiftId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PlantId { get; set; }
    }
}
