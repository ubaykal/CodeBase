namespace ECommerce.Application.ViewModels.BaseResponseModels;

public class BaseServiceResponseModel<T> where T : new()
{
    public int StatusCode { get; set; }
    public T Data { get; set; }
    public ResponseMessageModel ExceptionMessage { get; set; }
}