using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KuRuMi.Mio.DoMain.Infrastructure.Common
{
    /// <summary>
    /// 对称加密帮助类
    /// </summary>
    public class CryptographyExtension
    {
        // 对称加密算法提供器
        private ICryptoTransform encryptor;     // 加密器对象
        private ICryptoTransform decryptor;     // 解密器对象
        private const int BufferSize = 1024;
        private static string dataKey = "KurumiMioKotori-";

        public CryptographyExtension(string algorithmName, string key)
        {
            SymmetricAlgorithm provider = SymmetricAlgorithm.Create(algorithmName);
            provider.Key = Encoding.UTF8.GetBytes(key);
            provider.IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            encryptor = provider.CreateEncryptor();
            decryptor = provider.CreateDecryptor();
        }

        public CryptographyExtension(string key) : this("TripleDES", key) { }

        /// <summary>
        /// 加密算法
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns></returns>
        public string Encrypt(string clearText)
        {
            // 创建明文流
            byte[] clearBuffer = Encoding.UTF8.GetBytes(clearText);
            MemoryStream clearStream = new MemoryStream(clearBuffer);

            // 创建空的密文流
            MemoryStream encryptedStream = new MemoryStream();

            CryptoStream cryptoStream =
                new CryptoStream(encryptedStream, encryptor, CryptoStreamMode.Write);

            // 将明文流写入到buffer中
            // 将buffer中的数据写入到cryptoStream中
            int bytesRead = 0;
            byte[] buffer = new byte[BufferSize];
            do
            {
                bytesRead = clearStream.Read(buffer, 0, BufferSize);
                cryptoStream.Write(buffer, 0, bytesRead);
            } while (bytesRead > 0);

            cryptoStream.FlushFinalBlock();

            // 获取加密后的文本
            buffer = encryptedStream.ToArray();
            string encryptedText = Convert.ToBase64String(buffer);
            return encryptedText;
        }

        /// <summary>
        /// 解密算法
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns></returns>
        public string Decrypt(string encryptedText)
        {
            byte[] encryptedBuffer = Convert.FromBase64String(encryptedText);
            Stream encryptedStream = new MemoryStream(encryptedBuffer);

            MemoryStream clearStream = new MemoryStream();
            CryptoStream cryptoStream =
                new CryptoStream(encryptedStream, decryptor, CryptoStreamMode.Read);

            int bytesRead = 0;
            byte[] buffer = new byte[BufferSize];

            do
            {
                bytesRead = cryptoStream.Read(buffer, 0, BufferSize);
                clearStream.Write(buffer, 0, bytesRead);
            } while (bytesRead > 0);

            buffer = clearStream.GetBuffer();
            string clearText =
                Encoding.UTF8.GetString(buffer, 0, (int)clearStream.Length);

            return clearText;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="clearText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypts(string clearText)
        {
            CryptographyExtension helper = new CryptographyExtension(dataKey);
            return helper.Encrypt(clearText);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypts(string encryptedText)
        {
            CryptographyExtension helper = new CryptographyExtension(dataKey);
            return helper.Decrypt(encryptedText);
        }
    }
}
