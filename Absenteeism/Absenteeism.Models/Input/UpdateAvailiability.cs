namespace Absenteeism.Models.Input
{
    public class UpdateAvailiability
    {
        public int userId { get; set; }
        public int statusId { get; set; }
        public int isToday { get; set; }
    }
}
