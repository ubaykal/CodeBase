using System.Globalization;
using ECommerce.Application.Emuns;
using ECommerce.Application.Helpers;

namespace ECommerce.Application.ViewModels.BaseResponseModels;

public class ApiException : Exception
{
    public ErrorCode Code { get; set; }
    public string? CustomMessage { get; set; }
    public ApiException(ErrorCode code) : base()
    {
        Code = code;
        CustomMessage = Code.GetEnumDescription();
    }

    public ApiException(string? message, ErrorCode code) : base(message)
    {
        Code = code;
        CustomMessage = message;
    }

    public ApiException(Exception exception, ErrorCode code) : base(exception.Message, exception)
    {
        Code = code;
        CustomMessage = Code.GetEnumDescription();
    }

    public ApiException(string? message, Exception exception, ErrorCode code) : base(message, exception)
    {
        Code = code;
        CustomMessage = message;
    }


    public static void ThrowIfNull(object o, string? message)
    {
        if (o == null)
            throw new ApiException(message, ErrorCode.NullObject);
    }
    public static void ThrowIfNullOrEmpty(string s, string? message)
    {
        if (string.IsNullOrEmpty(s))
            throw new ApiException(message, ErrorCode.NullObject);
    }
    public override string ToString()
    {
        return InnerException == null ? base.ToString() : string.Format(CultureInfo.InvariantCulture, "{0} [See nested exception: {1}]", base.ToString(), InnerException);
    }
}