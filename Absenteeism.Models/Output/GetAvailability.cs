namespace Absenteeism.Models.Output
{
    public class GetAvailability
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int ShiftId { get; set; }
        public string Shift { get; set; }

        public int StatusId { get; set; }
        public string Status { get; set; }

        public int StationId { get; set; }
        public string Station { get; set; }

        public int PlantId { get; set; }
        public string Plant { get; set; }

        public int UserId { get; set; }
        public string Employee { get; set; }
    }
}
