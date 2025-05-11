using System.Net;

namespace MarvelApp.Application
{
    public class ApiResponse<T>
    {
        public bool IsSuccessful { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; } = string.Empty;
        public object Errors { get; set; }
        public T Data { get; set; }
        public HttpStatusCode CodeStatus { get; set; }
        public ApiResponse()
        {
        }
        public static ApiResponse<T> Success(T data, string message)
        {
            return new ApiResponse<T>
            {
                IsSuccessful = true,
                IsError = false,
                Data = data,
                Message = message,
                CodeStatus = HttpStatusCode.OK
            };
        }
        public static ApiResponse<T> NoContent(string message)
        {
            return new ApiResponse<T>
            {
                IsSuccessful = true,
                IsError = false,
                Data = default(T),
                Message = message,
                CodeStatus = HttpStatusCode.NoContent
            };
        }
        public static ApiResponse<T> Error(string message)
        {
            return new ApiResponse<T>
            {
                IsSuccessful = false,
                IsError = true,
                Data = default(T),
                Message = message,
                CodeStatus = HttpStatusCode.BadRequest
            };
        }

        public static ApiResponse<T> ErrorValidation(object errors)
        {
            return new ApiResponse<T>
            {
                IsSuccessful = false,
                IsError = true,
                Data = default(T),
                Errors = errors,
                Message = string.Empty,
                CodeStatus = HttpStatusCode.BadRequest
            };
        }
    }
}
