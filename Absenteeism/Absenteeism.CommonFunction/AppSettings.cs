namespace Absenteeism.CommonFunction
{
    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public Jwt Jwt { get; set; }

        public SendingEmailSettings sendingEmailSettings { get; set; }
        public LDAP ldap { get; set; }
        public FcmNotificationSetting fcmNotificationSetting { get; set; }

    }
    public class FcmNotificationSetting
    {
        public string SenderId { get; set; }
        public string ServerKey { get; set; }
        public string FirebaseUrl { get; set; }
    }
    public class ConnectionStrings
    {
        public string MyConnections { get; set; }
    }

    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

    }


    public class SendingEmailSettings
    {
        public string From { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Logo { get; set; }
        public string Reset { get; set; }
        public int RandomPasswordGenerator { get; set; }
    }

    public class LDAP
    {
        public string ADPath { get; set; }
    }
}
