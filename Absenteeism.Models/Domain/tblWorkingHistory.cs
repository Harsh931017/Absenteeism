namespace Absenteeism.Models.Domain
{
    public class tblWorkingHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DepartmentId { get; set; }
        public int StationId { get; set; }
        public int ShiftId { get; set; }
        public int PlantId { get; set; }
    }
}
