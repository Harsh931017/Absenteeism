using System;

namespace Absenteeism.CommonFunction.Wrappers
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
        }

        //public ApiResponse(object data)
        //{
        //    //Succeeded = true;
        //    Message = string.Empty;
        //    userData = data;
        //}

        public bool Succeeded { get; set; }
        public Enums.HttpStatusCode Outputcode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        //public object objectdata { get; set; }
        public short ResponseType { get; set; }
    }
}
