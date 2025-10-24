using Shared.Enums;

namespace Shared.Models;

public class ServiceResult<T>
{
    public bool Success { get; private set; }
    public string Message { get; private set; } = string.Empty;
    public ErrorCode ErrorCode { get; private set; }
    public T? Data { get; private set; }

    private ServiceResult(bool success, string message, ErrorCode errorCode, T? data)
    {
        Success = success;
        Message = message;
        ErrorCode = errorCode;
        Data = data;
    }

    public static ServiceResult<T> SuccessResult(T data) => new (true, String.Empty, ErrorCode.None, data);
    public static ServiceResult<T> FailResult(string message, ErrorCode errorCode) => new (false, message, errorCode, default);
}

public class ServiceResult
{
    public bool Success { get; private set; }
    public string Message { get; private set; } = string.Empty;
    public ErrorCode ErrorCode { get; private set; }

    private ServiceResult(bool success, string message, ErrorCode errorCode)
    {
        Success = success;
        Message = message;
        ErrorCode = errorCode;
    }

    public static ServiceResult SuccessResult() => new (true, String.Empty, ErrorCode.None);
    public static ServiceResult FailResult(string message, ErrorCode errorCode) => new (false, message, errorCode);
}