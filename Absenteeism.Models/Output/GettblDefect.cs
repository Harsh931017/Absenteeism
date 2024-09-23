

namespace Absenteeism.Models.Output
{
    public class GettblDefect
    {
        public int Id { get; set; }
        public string DefectName { get; set; }
        public int Occurrance { get; set; }
        public int ShiftId { get; set; }
        public string Shift { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int PlantId { get; set; }
        public string Plant { get; set; }

        public int StationId { get; set; }
        public string Station { get; set; }
    }
}
