using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ContactBook.Model.ViewModels
{
    public class Response<T>
    {
        public string Message { get; set; }
        public int ResponseCode { get; set; }
        public T Data { get; set; }


        public  Response<T> Success(string  message, int statusCode, T data)
        {
            return new Response<T>
            {
                Message = message,
                ResponseCode = statusCode,
                Data = data
            };
        }

        public Response<T> Failed(string message, int statusCode)
        {
            return new Response<T>
            {
                Message = message,
                ResponseCode = statusCode,               
            };
        }

        public Response<T> Failed(string message)
        {
            return new Response<T>
            {
                Message = message,                
            };
        }

        public Response<T> Success(string message, int statusCode)
        {
            return new Response<T>
            {
                Message = message,
                ResponseCode = statusCode,                
            };
        }
    }
}
