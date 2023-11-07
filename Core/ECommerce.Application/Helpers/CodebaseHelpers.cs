using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ECommerce.Application.Helpers;

public static class CodebaseHelpers
{
    public static string? GetEnumDescription(this Enum value)
    {
        var fi = value.GetType().GetField(value.ToString());
        if (fi == null)
        {
            return value.ToString();
        }

        var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        if (attributes != null && attributes.Any())
        {
            return attributes.First().Description;
        }

        return value.ToString();
    }

    public static string PasswordEncypt(this string password)
    {
        var textBytes = System.Text.Encoding.UTF8.GetBytes(password);
        return System.Convert.ToBase64String(textBytes);
    }
    public static string PasswordDecrypt(this string password)
    {
        var base64Bytes = System.Convert.FromBase64String(password);
        return System.Text.Encoding.UTF8.GetString(base64Bytes);
    }

    /// <summary>
    /// Dakika cinsinden süre verilmelidir
    /// </summary>
    public static int TokenExpireTime => 330;
    /// <summary>
    /// Dakika cinsinden süre verilmelidir
    /// </summary>
    public static int RefreshTokenExpireTime => 15;
    
    public static string GetMonthName(this int month)  
    { 
        return CultureInfo.CurrentCulture.
            DateTimeFormat.GetMonthName
                (month);
    }
    
    public static string? GetPropertyDescription(this PropertyInfo value)
    {
        var fi = value.GetCustomAttributes();
        if (fi.FirstOrDefault(x => x.GetType() == typeof(DescriptionAttribute)) == null)
        {
            return value.Name;
        }

        var description = ((DescriptionAttribute) fi.FirstOrDefault(x => x.GetType() == typeof(DescriptionAttribute)))
            .Description;

        return description;
    }
}
