namespace Absenteeism.Models.Domain
{
    public class tblDefect
    {
        public int Id { get; set; }
        public string DefectName { get; set; }
        public int Occurrance { get; set; }
        public int ShiftId { get; set; }
        //public string Shift{ get; set; }
        public DateTime Date { get; set; }
        public int PlantId { get; set; }
        public int StationId { get; set; }
    }
}
