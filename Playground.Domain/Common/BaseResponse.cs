namespace Playground.Domain.Common;

public class BaseResponse<T>
{
    public bool Success { get; private set; }
    public string? Message { get; private set; }
    public T? Data { get; private set; }

    private BaseResponse(bool success, T? data = default, string? message = null)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static BaseResponse<T> Ok(T data) => new(true, data);
    public static BaseResponse<T> Fail(string message) => new(false, default, message);
}