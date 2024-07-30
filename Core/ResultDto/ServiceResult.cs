using System.Net;
using System.Text.Json.Serialization;

namespace Core.ResultDto;

public class ServiceResult<T>
{
    public T? Data { get; set; }


    public List<string>? Errors { get; set; }

    [JsonIgnore] public HttpStatusCode Status { get; set; }
    [JsonIgnore] public bool IsSuccess => Errors?.Count == 0;


    // success method
    public static ServiceResult<T> Success(T data, HttpStatusCode code)
    {
        return new ServiceResult<T>
        {
            Data = data,

            Status = code
        };
    }

    // fail method
    public static ServiceResult<T> Fail(List<string> errors, HttpStatusCode code)
    {
        return new ServiceResult<T>
        {
            Errors = errors,
            Status = code
        };
    }

    // fail method
    public static ServiceResult<T> Fail(string error, HttpStatusCode code)
    {
        return new ServiceResult<T>
        {
            Errors = [error],
            Status = code
        };
    }
}

public class ServiceResult
{
    public List<string>? Errors { get; set; }

    [JsonIgnore] public HttpStatusCode Status { get; set; }
    [JsonIgnore] public bool IsSuccess => Errors?.Count == 0;


    // success method
    public static ServiceResult Success(HttpStatusCode code)
    {
        return new ServiceResult
        {
            Status = code
        };
    }

    // fail method
    public static ServiceResult Fail(List<string> errors, HttpStatusCode code)
    {
        return new ServiceResult
        {
            Errors = errors,
            Status = code
        };
    }

    // fail method
    public static ServiceResult Fail(string error, HttpStatusCode code)
    {
        return new ServiceResult
        {
            Errors = [error],
            Status = code
        };
    }
}