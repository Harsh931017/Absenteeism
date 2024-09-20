namespace Absenteeism.Models.Output
{
    public class GetWorkingHistory
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int StationId { get; set; }
        public string Station { get; set; }
        public int ShiftId { get; set; }
        public string Shift { get; set; }

        public int PlantId { get; set; }
        public string Plant { get; set; }
    }
}
