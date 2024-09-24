using Absenteeism.CommonFunction;
using Absenteeism.Models.Notification;
using Absenteeism.Repository.Interface;
using Absenteeism.Service.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;

namespace Absenteeism.Service
{
    public class CommonService : ICommonService
    {
        private readonly AppSettings _appSettings;
        private static string ApplicationDirectory;
        private readonly IMasterRepository _masterRepository;
    
        public CommonService(IOptions<AppSettings> appSettings, IMasterRepository masterRepository)
        {
            ApplicationDirectory = Environment.CurrentDirectory;
            _appSettings = appSettings.Value;
            _masterRepository = masterRepository;
        }


        public async Task SendMail(int UserId, string email, string attachment, string body)
        {
            try
            {
                string host = _appSettings.sendingEmailSettings.Host;
                string from = _appSettings.sendingEmailSettings.From;
                string password = _appSettings.sendingEmailSettings.Password;
                string Logo = _appSettings.sendingEmailSettings.Logo;
                string Reset = _appSettings.sendingEmailSettings.Reset;
                int RandomPasswordGenerator = _appSettings.sendingEmailSettings.RandomPasswordGenerator;
                int port = Convert.ToInt32(_appSettings.sendingEmailSettings.Port);

                Byte[] iconBytes = Convert.FromBase64String(Logo);
                System.IO.MemoryStream iconBitmap = new System.IO.MemoryStream(iconBytes);
                LinkedResource iconResource = new LinkedResource(iconBitmap, MediaTypeNames.Image.Jpeg);
                iconResource.ContentId = "Icon";

                Byte[] ResesticonBytes = Convert.FromBase64String(Reset);
                System.IO.MemoryStream ResesticonBitmap = new System.IO.MemoryStream(ResesticonBytes);
                LinkedResource ResesticonResource = new LinkedResource(ResesticonBitmap, MediaTypeNames.Image.Jpeg);
                ResesticonResource.ContentId = "Reset";

                string RandomGenerator = await GenerateUniqueRandomString(RandomPasswordGenerator);
                await _masterRepository.SaveRandomPassword(RandomGenerator, UserId);
                body = body.Replace("{{RadomString}}", RandomGenerator);

                MailMessage msg = new MailMessage();
                msg.IsBodyHtml = true;
                msg.To.Add(new MailAddress(email, "Recipient Name"));
                msg.From = new MailAddress(from, "Absenteeism");
                msg.Subject = "Forgot Password";


                AlternateView alternativeView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                alternativeView.LinkedResources.Add(iconResource);
                alternativeView.LinkedResources.Add(ResesticonResource);
                msg.AlternateViews.Add(alternativeView);

                SmtpClient client = new SmtpClient();
                client.EnableSsl = false;
                client.Credentials = new System.Net.NetworkCredential(from, password);
                client.Port = port;
                client.Host = host;
                client.Send(msg);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<string> GenerateUniqueRandomString(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var byteArray = new byte[length];
                rng.GetBytes(byteArray);

                // Convert to base64 string, then trim and replace any special characters if necessary
                string uniqueString = Convert.ToBase64String(byteArray)
                                      .TrimEnd('=')
                                      .Replace('+', '-')
                                      .Replace('/', '_');

                return uniqueString.Substring(0, length); // Adjust length if needed
            }
        }

        public async Task<bool> SendNotificationAsync(NotificationModel notification)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={_appSettings.fcmNotificationSetting.ServerKey}");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={_appSettings.fcmNotificationSetting.SenderId}");

                var message = new
                {
                    to = notification.To,  // Device token
                    notification = new
                    {
                        title = notification.Title,
                        body = notification.Body
                    },
                    data = notification.Data // Additional data
                };

                var jsonMessage = JsonConvert.SerializeObject(message);
                var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_appSettings.fcmNotificationSetting.FirebaseUrl, content);
                return response.IsSuccessStatusCode;
            }

        }
    }
}