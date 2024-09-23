namespace Absenteeism.Models.Domain
{
    public class tblNotification
    {
        public int Id { get; set; }
        public int AvailabilityId { get; set; }
        public bool IsCheck { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
