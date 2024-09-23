using Newtonsoft.Json;

namespace Absenteeism.Models.Notification
{
    public class NotificationModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public object Data { get; set; }
    }
}
