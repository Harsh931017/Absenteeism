namespace Absenteeism.Models.Output
{
    public class GetStations
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlantId { get; set; }
        public string Plant { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
    }
}
