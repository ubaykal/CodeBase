using ECommerce.Application.ViewModels.BaseResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;

namespace CodeBase.ECommerce.Middlewares;

public class ResponseWrapper : ObjectResultExecutor
{
    public ResponseWrapper(OutputFormatterSelector formatterSelector, IHttpResponseStreamWriterFactory writerFactory,
        ILoggerFactory loggerFactory, IOptions<MvcOptions> mvcOptions) : base(formatterSelector, writerFactory,
        loggerFactory, mvcOptions)
    {
    }

    public override Task ExecuteAsync(ActionContext context, ObjectResult result)
    {
        if (result.Value == null) return base.ExecuteAsync(context, result);

        var response = new BaseServiceResponseModel<object>
        {
            Data = result.Value,
            StatusCode = result.StatusCode ?? 200
        };

        var typeCode = Type.GetTypeCode(result.Value.GetType());
        if (typeCode == TypeCode.Object)
            result.Value = response;
        return base.ExecuteAsync(context, result);
    }
}