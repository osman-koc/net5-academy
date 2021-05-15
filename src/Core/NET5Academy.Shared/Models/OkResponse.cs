using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace NET5Academy.Shared.Models
{
    public class OkResponse<T>
    {
        public T Data { get; private set; }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccess { get; private set; }

        public List<string> Errors { get; set; }

        public static OkResponse<T> Success(HttpStatusCode statusCode, T data)
        {
            return new OkResponse<T>
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccess = true
            };
        }

        public static OkResponse<T> Success(HttpStatusCode statusCode)
        {
            return new OkResponse<T>
            {
                Data = default(T),
                StatusCode = statusCode,
                IsSuccess = true
            };
        }

        public static OkResponse<T> Error(HttpStatusCode statusCode, string errorMessage)
        {
            return new OkResponse<T>
            {
                Data = default(T),
                StatusCode = statusCode,
                IsSuccess = false,
                Errors = new List<string> { errorMessage }
            };
        }

        public static OkResponse<T> Error(HttpStatusCode statusCode, List<string> errorMessages)
        {
            return new OkResponse<T>
            {
                Data = default(T),
                StatusCode = statusCode,
                IsSuccess = false,
                Errors = errorMessages
            };
        }
    }
}
