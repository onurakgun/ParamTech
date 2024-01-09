using Microsoft.AspNetCore.WebUtilities;
using System.IO.Compression;
using System.Text;
namespace ParamTech.Core.Persistence.Utilities.Helpers;
public class Base64Convert
{
    public static string Base64Sifrele(object data)
    {
        try
        {
            byte[] Base = Encoding.UTF8.GetBytes(data.ToString());
            string Base64 = Convert.ToBase64String(Base);
            return Base64;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static string Base64CozString(string base64EncodedData)
    {
        try
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            string data = Encoding.UTF8.GetString(base64EncodedBytes);
            return data.ToString();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static int? Base64CozInt(string base64EncodedData)
    {
        try
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            string data = Encoding.UTF8.GetString(base64EncodedBytes);
            return Convert.ToInt32(data);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static decimal? Base64CozDecimal(string base64EncodedData)
    {
        try
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            string data = Encoding.UTF8.GetString(base64EncodedBytes);
            return Convert.ToDecimal(data);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static DateTime? Base64CozDatetime(string base64EncodedData)
    {
        try
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            string data = Encoding.UTF8.GetString(base64EncodedBytes);
            return Convert.ToDateTime(data);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static short? Base64CozShort(string base64EncodedData)
    {
        try
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            string data = Encoding.UTF8.GetString(base64EncodedBytes);
            return Convert.ToInt16(data);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static string Base64SifreleSikistir(string data)
    {
        try
        {
            string inputStr = data;
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputStr);
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (GZipStream gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    gZipStream.Write(inputBytes, 0, inputBytes.Length);
                }
                byte[] outputBytes = outputStream.ToArray();
                string outputbase64 = Convert.ToBase64String(outputBytes);
                return outputbase64;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static string Base6CozAc(string data)
    {
        try
        {
            string inputStr = data;
            byte[] inputBytes = Convert.FromBase64String(inputStr);
            using (MemoryStream inputStream = new MemoryStream(inputBytes))
            using (GZipStream gZipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            using (StreamReader streamReader = new StreamReader(gZipStream))
            {
                string decompressed = streamReader.ReadToEnd();
                return decompressed;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static string Base64UrlEncode(string data)
    {
        byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(data);
        string codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
        return codeEncoded;
    }

    public static string Base64UrlDecode(string data)
    {
        byte[] codeDecodedBytes = WebEncoders.Base64UrlDecode(data);
        string codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
        return codeDecoded;
    }
}