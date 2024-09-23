namespace Absenteeism.Models.Output
{
    public class GettblAllocation
    {
        public int Id { get; set; }
        public int CellMemberId { get; set; }
        public string CellMember { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }

        public int StationId { get; set; }
        public string Station { get; set; }

        public int ShiftId { get; set; }
        public string Shift { get; set; }

        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PlantId { get; set; }
        public string Plant { get; set; }

    }
}
