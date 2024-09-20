using Absenteeism.Models.Notification;

namespace Absenteeism.Service.Interface
{
    public interface ICommonService
    {

        Task SendMail(int UserId, string email, string attachment, string body);
        Task<bool> SendNotificationAsync(NotificationModel notification);
    }
}
