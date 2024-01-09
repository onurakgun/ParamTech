using System.Security.Cryptography;
using System.Text;
namespace ParamTech.Core.Persistence.Utilities.Helpers;
public class ShaHash
{
    public static string Shai256Sifre(string sifre)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(sifre))
            {
                throw new ArgumentException("message", nameof(sifre));
            }
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(sifre);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static string Shai512Sifre(string sifre)
    {
        try
        {
            if (string.IsNullOrEmpty(sifre))
            {
                throw new ArgumentException("message", nameof(sifre));
            }
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(sifre);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static string GetStringFromHash(byte[] hash)
    {
        try
        {
            if (hash == null)
            {
                throw new ArgumentNullException(nameof(hash));
            }
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
        catch (Exception)
        {
            return null;
        }
    }
}