using ECommerce.Application.Emuns;
using ECommerce.Application.Helpers;
using ECommerce.Application.ViewModels.BaseResponseModels;

namespace ECommerce.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;

    public ExceptionMiddleware(RequestDelegate next,
        IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task Invoke(HttpContext context, IHostEnvironment environment)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // _logger.LogError($"Error {ex.Message}");
            //ExceptionLog(ex, context);
            var response = new BaseServiceResponseModel<object>()
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var exceptionMessage = new ResponseMessageModel();
            switch (ex)
            {
                case ApiException exception:
                {
                    var apiEx = exception;
                    exceptionMessage.Code = (int) apiEx.Code;
                    exceptionMessage.Message = apiEx.CustomMessage ?? exception.Message;

                    context.Response.StatusCode = StatusCodes.Status200OK;
                    response.StatusCode = (int) apiEx.Code;
                    break;
                }
                case ApiValidationException exception:
                {
                    foreach (var item in exception.Errors)
                    {
                        exceptionMessage.Message = item;
                        exceptionMessage.Code = (int) ErrorCode.ValidationError;
                    }

                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                }
                default:
                    exceptionMessage.Code = (int) ErrorCode.UnexpectedError;
                    exceptionMessage.Message = environment.IsDevelopment()
                        ? ex.Message
                        : ErrorCode.UnexpectedError.GetEnumDescription();
                    break;
            }

            response.ExceptionMessage = exceptionMessage;
            await context.Response.WriteAsJsonAsync(response);
        }
    }

    private void ExceptionLog(Exception ex, HttpContext context)
    {
        var customMessage = string.Empty;
    
        if (ex is ApiException exception)
            customMessage = exception?.CustomMessage;
    
        var requestPath = context.Request.Path.Value;
        var userId = context.Request.Headers["UserId"].ToString();

        // var log = new Log
        // {
        //     UserId = new Guid(),
        //     CustomMessage = customMessage,
        //     ExceptionType = customMessage == null ? ExceptionType.System : ExceptionType.Custom,
        //     Message = ex?.Message,
        //     InnerException = ex?.InnerException?.Message,
        // };
    
        // using var scope = _serviceProvider.CreateScope();
        // var ICodeBaseLog = scope.ServiceProvider.GetRequiredService<ICodeBaseLog>();
        // ICodeBaseLog.AddAsync(log);
    }
}