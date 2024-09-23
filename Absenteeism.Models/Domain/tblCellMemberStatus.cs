namespace Absenteeism.Models.Domain
{
    public class tblCellMemberStatus
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int StationId { get; set; }
        public int EmployeeId { get; set; }
        public int ShiftId { get; set; }
        public int StatusId { get; set; }
        public int PlantId { get; set; }
    }
}
