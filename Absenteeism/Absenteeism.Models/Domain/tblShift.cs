namespace Absenteeism.Models.Domain
{
    public class tblShift
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Timing { get; set; }
        public int PlantId { get; set; }
        public string Plant { get; set; }
    }
}
