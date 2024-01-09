using System.Security.Cryptography;
using System.Text;
namespace ParamTech.Core.Persistence.Utilities.Helpers;
public class CiftYonluSifreleme
{
    private static readonly byte[] Key = Encoding.ASCII.GetBytes(@"qwr{@^h`h&_`50/ja9!'dcmh3!uw<&=?");
    private static readonly byte[] IV  = Encoding.ASCII.GetBytes(@"9/\~V).A,lY&=t2b");

    public static string EncryptStringToBytes_Aes(string plainText)
    {
        if (plainText == null || plainText.Length <= 0)
        {
            throw new ArgumentNullException("plainText");
        }
        if (Key == null || Key.Length <= 0)
        {
            throw new ArgumentNullException("Key");
        }
        if (IV == null || IV.Length <= 0)
        {
            throw new ArgumentNullException("IV");
        }
        byte[] encrypted;
#pragma warning disable SYSLIB0021 // Type or member is obsolete
        using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }
#pragma warning restore SYSLIB0021 // Type or member is obsolete
        return Convert.ToBase64String(encrypted);
    }

    public static string DecryptStringFromBytes_Aes(string Text)
    {
        if (Text == null || Text.Length <= 0)
        {
            throw new ArgumentNullException("cipherText");
        }
        if (Key == null || Key.Length <= 0)
        {
            throw new ArgumentNullException("Key");
        }
        if (IV == null || IV.Length <= 0)
        {
            throw new ArgumentNullException("IV");
        }
        string plaintext = null;
        byte[] cipherText = Convert.FromBase64String(Text.Replace(' ', '+'));
#pragma warning disable SYSLIB0021 // Type or member is obsolete
        using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

        }
#pragma warning restore SYSLIB0021 // Type or member is obsolete
        return plaintext;
    }

    public static byte[] ByteDonustur(string deger)
    {
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        return ByteConverter.GetBytes(deger);
    }

    public static byte[] Byte8(string deger)
    {
        char[] arrayChar = deger.ToCharArray();
        byte[] arrayByte = new byte[arrayChar.Length];
        for (int i = 0; i < arrayByte.Length; i++)
        {
            arrayByte[i] = Convert.ToByte(arrayChar[i]);
        }
        return arrayByte;
    }

    public static string DESSifrele(string strGiris)
    {
        string sonuc;
        if (strGiris == "" || strGiris == null)
        {
            throw new ArgumentNullException("Şifrelenecek veri yok");
        }
        else
        {
            byte[] aryKey = Byte8("22031806"); // 8 bit string DEĞER
            byte[] aryIV = Byte8("22031806"); //  8 bit string DEĞER
#pragma warning disable SYSLIB0021 // Type or member is obsolete
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
#pragma warning restore SYSLIB0021 // Type or member is obsolete
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(aryKey, aryIV), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cs);
            writer.Write(strGiris);
            writer.Flush();
            cs.FlushFinalBlock();
            writer.Flush();
            sonuc = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            writer.Dispose();
            cs.Dispose();
            ms.Dispose();
        }
        return sonuc;
    }

    public static string DESCoz(string strGiris)
    {
        string strSonuc;
        if (strGiris == "" || strGiris == null)
        {
            throw new ArgumentNullException("Şifrelenecek veri yok.");
        }
        else
        {
            byte[] aryKey = Byte8("22031806");
            byte[] aryIV = Byte8("22031806");
#pragma warning disable SYSLIB0021 // Type or member is obsolete
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
#pragma warning restore SYSLIB0021 // Type or member is obsolete
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(strGiris));
            CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(aryKey, aryIV), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cs);
            strSonuc = reader.ReadToEnd();
            reader.Dispose();
            cs.Dispose();
            ms.Dispose();
        }
        return strSonuc;
    }

    public static string TripleDESSifrele(string strGiris)
    {
        string sonuc;
        if (strGiris == "" || strGiris == null)
        {
            throw new ArgumentNullException("Şifrelenecek veri yok.");
        }
        else
        {
            byte[] aryKey = Byte8("123456781234567812345678");
            byte[] aryIV = Byte8("12345678");
#pragma warning disable SYSLIB0021 // Type or member is obsolete
            TripleDESCryptoServiceProvider dec = new TripleDESCryptoServiceProvider();
#pragma warning restore SYSLIB0021 // Type or member is obsolete
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, dec.CreateEncryptor(aryKey, aryIV), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cs);
            writer.Write(strGiris);
            writer.Flush();
            cs.FlushFinalBlock();
            writer.Flush();
            sonuc = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            writer.Dispose();
            cs.Dispose();
            ms.Dispose();
        }
        return sonuc;
    }

    public static string TripleDESCoz(string strGiris)
    {
        string strSonuc = "";
        if (strGiris == "" || strGiris == null)
        {
            throw new ArgumentNullException("Şifrelenecek veri yok.");
        }
        else
        {
            byte[] aryKey = Byte8("123456781234567812345678");
            byte[] aryIV = Byte8("12345678");
#pragma warning disable SYSLIB0021 // Type or member is obsolete
            TripleDESCryptoServiceProvider cryptoProvider = new TripleDESCryptoServiceProvider();
#pragma warning restore SYSLIB0021 // Type or member is obsolete
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(strGiris));
            CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(aryKey, aryIV), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cs);
            strSonuc = reader.ReadToEnd();
            reader.Dispose();
            cs.Dispose();
            ms.Dispose();
        }
        return strSonuc;
    }

    public static string RC2Sifrele(string strGiris)
    {
        string sonuc = "";
        if (strGiris == "" || strGiris == null)
        {
            throw new ArgumentNullException("Şifrelenecek veri yok.");
        }
        else
        {
            byte[] aryKey = Byte8("?de_22*0");
            byte[] aryIV = Byte8("?de_22*0");
#pragma warning disable SYSLIB0021 // Type or member is obsolete
            RC2CryptoServiceProvider dec = new RC2CryptoServiceProvider();
#pragma warning restore SYSLIB0021 // Type or member is obsolete
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, dec.CreateEncryptor(aryKey, aryIV), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cs);
            writer.Write(strGiris);
            writer.Flush();
            cs.FlushFinalBlock();
            writer.Flush();
            sonuc = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            writer.Dispose();
            cs.Dispose();
            ms.Dispose();
        }
        return sonuc;
    }

    public static string RC2Coz(string strGiris)
    {
        string strSonuc = "";
        if (strGiris == "" || strGiris == null)
        {
            throw new ArgumentNullException("Şifresi çözülecek veri yok.");
        }
        else
        {
            byte[] aryKey = Byte8("?de_22*0");
            byte[] aryIV = Byte8("?de_22*0");
#pragma warning disable SYSLIB0021 // Type or member is obsolete
            RC2CryptoServiceProvider cp = new RC2CryptoServiceProvider();
#pragma warning restore SYSLIB0021 // Type or member is obsolete
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(strGiris));
            CryptoStream cs = new CryptoStream(ms, cp.CreateDecryptor(aryKey, aryIV), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cs);
            strSonuc = reader.ReadToEnd();
            reader.Dispose();
            cs.Dispose();
            ms.Dispose();
        }
        return strSonuc;
    }

    public static string RijndaelSifrele(string strGiris)
    {
        string sonuc = "";
        if (strGiris == "" || strGiris == null)
        {
            throw new ArgumentNullException("Şifrelenecek veri yok.");
        }
        else
        {
            byte[] aryKey = Byte8("12345678");
            byte[] aryIV = Byte8("1234567812345678");
#pragma warning disable SYSLIB0022 // Type or member is obsolete
            RijndaelManaged dec = new RijndaelManaged
            {
                Mode = CipherMode.CBC
            };
#pragma warning restore SYSLIB0022 // Type or member is obsolete
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, dec.CreateEncryptor(aryKey, aryIV), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cs);
            writer.Write(strGiris);
            writer.Flush();
            cs.FlushFinalBlock();
            writer.Flush();
            sonuc = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            writer.Dispose();
            cs.Dispose();
            ms.Dispose();
        }
        return sonuc;
    }

    public static string RijndaelCoz(string strGiris)
    {
        string strSonuc = "";
        if (strGiris == "" || strGiris == null)
        {
            throw new ArgumentNullException("Şifrezi çözülecek veri yok.");
        }
        else
        {
            byte[] aryKey = Byte8("?d*ak-g_");
            byte[] aryIV = Byte8("?d*ak-g_?d*ak-g_");
#pragma warning disable SYSLIB0022 // Type or member is obsolete
            using RijndaelManaged cp = new RijndaelManaged();
#pragma warning restore SYSLIB0022 // Type or member is obsolete
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(strGiris));
            CryptoStream cs = new CryptoStream(ms, cp.CreateDecryptor(aryKey, aryIV), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cs);
            strSonuc = reader.ReadToEnd();
            reader.Dispose();
            cs.Dispose();
            ms.Dispose();
        }
        return strSonuc;
    }

    public static string RSASifrele(string strGiris, out RSAParameters prm)
    {
        string strSonuc = "";
        if (strGiris == "")
        {
            throw new ArgumentNullException("Şifrelenecek veri yok.");
        }
        else
        {
            byte[] aryDizi = ByteDonustur(strGiris);
            RSACryptoServiceProvider dec = new RSACryptoServiceProvider();
            prm = dec.ExportParameters(true);
            byte[] aryDonus = dec.Encrypt(aryDizi, false);
            strSonuc = Convert.ToBase64String(aryDonus);
        }
        return strSonuc;
    }

    public static string RSACoz(string strGiris, RSAParameters prm)
    {
        string strSonuc = "";
        if (strGiris == "" || strGiris == null)
        {
            throw new ArgumentNullException("Çözülecek kayıt yok");
        }
        else
        {
            RSACryptoServiceProvider dec = new RSACryptoServiceProvider();
            byte[] aryDizi = Convert.FromBase64String(strGiris);
            UnicodeEncoding UE = new UnicodeEncoding();
            dec.ImportParameters(prm);
            byte[] aryDonus = dec.Decrypt(aryDizi, false);
            strSonuc = UE.GetString(aryDonus);
        }
        return strSonuc;
    }
}