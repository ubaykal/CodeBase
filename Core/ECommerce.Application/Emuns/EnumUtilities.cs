using System.ComponentModel;

namespace ECommerce.Application.Emuns;

public enum ErrorCode
{
    [Description("Model null olamaz.")] NullObject = 100,

    [Description("Bir hata oluştu tekrar deneyiniz.")]
    PaginationError = 101,

    [Description("Geçersiz istek.")] InValidRequest = 102,

    [Description("Bu email veya kullanıcı adı kullanılmaktadır. Lütfen yeni bir Email giriniz.")]
    ExistCustomer = 103,
    
    [Description("Bu Kitap sistemde mevcuttur. Lütfen yeni bir kitap giriniz ya da stoğu güncelleyiniz")]
    ExistBook = 104,
    
    [Description("Validation hatası.")] 
    ValidationError = 105,
    
    [Description("Kullanıcı bulunamadı.")] 
    NotFoundCustomer = 404,
    
    [Description("Kayıt bulunamadı.")] 
    NotFound = 404,

    [Description("Yetkisiz giriş.")]
    AuthenticationError = 401,
    
    [Description("Beklenmedik bir hata oluştu.")]
    UnexpectedError = 500,
    
    [Description("Ürün stoğu yetersiz.")]
    InsufficientStock = 501,
    
    [Description("Ürün stoğu sıfırdan büyük girilmelidir")]
    StockControl = 502,
    
    [Description("Girdiğiniz şifreler uyuşmamaktaıdr")]
    PasswordConfirm = 503,
}

public enum ExceptionType
{
    [Description("Custom")] Custom = 1,
    [Description("System")] System
}