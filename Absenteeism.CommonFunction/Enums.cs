namespace Absenteeism.CommonFunction
{
    public class Enums
    {
        public enum HttpStatusCode
        {
            OK = 200,
            AccessDenied = 401,
            BadRequest = 400,
            NotFound = 404,
            InternalServerError = 500
        }
        public enum EmailStatus
        {
            Pic = 2,
            Successfully = 3,
            Error = 4
        }
    }
}
