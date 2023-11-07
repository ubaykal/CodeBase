namespace ECommerce.Application.ViewModels.BaseResponseModels;

public class ApiValidationException : Exception
{
    public List<string?> Errors;
    public ApiValidationException(List<string?> ValidationMessages) : base()
    {
        Errors = ValidationMessages;
    }
}