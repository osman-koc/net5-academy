using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NET5Academy.Shared.Models
{
    public class OkResponse<T>
    {
        public T Data { get; private set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccess { get; private set; }

        public List<string> Errors { get; set; }

        public static OkResponse<T> Success(int statusCode, T data)
        {
            return new OkResponse<T>
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccess = true
            };
        }

        public static OkResponse<T> Success(int statusCode)
        {
            return new OkResponse<T>
            {
                Data = default(T),
                StatusCode = statusCode,
                IsSuccess = true
            };
        }

        public static OkResponse<T> Error(string errorMessage, int statusCode)
        {
            return new OkResponse<T>
            {
                Data = default(T),
                StatusCode = statusCode,
                IsSuccess = false,
                Errors = new List<string> { errorMessage}
            };
        }

        public static OkResponse<T> Error(List<string> errorMessages, int statusCode)
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
