using System.Security.Cryptography;

namespace Krtshk.Services;

public interface IKeyService
{
    string Key(int length);
}

public class KeyService : IKeyService
{
    public string Key(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        byte[] data = new byte[length];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(data);
        }

        var result = new string([.. data.Select(b => chars[b % chars.Length])]);

        return result;
    }
}