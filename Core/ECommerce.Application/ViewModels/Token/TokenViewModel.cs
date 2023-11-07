namespace ECommerce.Application.ViewModels.Token;

public class TokenViewModel
{
    public string AccessToken { get; set; }
    public DateTime Expiration { get; set; }
    public string RefreshToken { get; set; }
}