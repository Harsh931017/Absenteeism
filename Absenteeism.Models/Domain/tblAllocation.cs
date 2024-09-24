namespace Absenteeism.Models.Domain
{
    public class tblAllocation
    {
        public int Id { get; set; }
        public int CellMemberId { get; set; }
        public int DepartmentId { get; set; }
        public int StationId { get; set; }
        public int ShiftId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PlantId { get; set; }
    }
}
