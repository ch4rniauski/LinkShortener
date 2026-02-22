using System.Security.Cryptography;
using ch4rniauski.LinkShortener.Application.Contracts.Services.ShortLink;

namespace ch4rniauski.LinkShortener.Application.Services.ShortLink;

internal class ShortLinkTokenProvider : IShortLinkTokenProvider
{
    private const string Chars = "abcdefghijklmnopqrstuvwxyz0123456789";
    private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();
    
    public string GenerateToken(int length = 7)
    {
        var bytes = new byte[length];
        Rng.GetBytes(bytes);
        
        var result = new char[length];
        
        for (var i = 0; i < length; i++)
        {
            result[i] = Chars[bytes[i] % Chars.Length];
        }
        
        return new string(result);
    }
}
