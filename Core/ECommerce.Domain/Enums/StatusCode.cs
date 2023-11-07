using System.ComponentModel;

namespace ECommerce.Domain.Enums;

public enum OrderStatus
{
    [Description("Devam ediyor")]
    Inprogress,
    [Description("Tamamlandı")]
    Complete,
}

public enum PaymentStatus
{
    [Description("Beklemede")]
    Pending,
    [Description("Ödeme alındı")]
    Success
}