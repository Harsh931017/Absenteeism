namespace Absenteeism.Models.Output
{
    public class GettblCellMemberStatus
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int StationId { get; set; }
        public string Station { get; set; }
        public int EmployeeId { get; set; }
        public string Employee { get; set; }
        public int ShiftId { get; set; }
        public string Shift{ get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }

        public int PlantId { get; set; }
        public string Plant { get; set; }
        public bool IsAllocated { get; set; }
    }
}
