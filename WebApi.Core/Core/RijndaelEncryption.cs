using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using WebApi.Public;

namespace WebApi.Core
{
    internal class RijndaelEncryption
    {
        private static readonly Lazy<RijndaelEncryption> lazy = new Lazy<RijndaelEncryption>(() => new RijndaelEncryption());
        internal static RijndaelEncryption Instance { get { return lazy.Value; } }
        private SymmetricAlgorithm mobjCryptoService;
        private string _Key;
        private string _IV;

        /// <summary>    
        /// Rijndael对称加密类的构造函数    
        /// </summary>    
        private RijndaelEncryption()
        {
            mobjCryptoService = new RijndaelManaged();
            _Key = "hNur`RX!}NqP&$}QZ*.jx4pRb?HWvIR3";
            _IV = "+I@ka'e!c|lj=H2F1,3/7h7<7o:'$LR.";
        }
        private int GetSplitPosition(string Encry)
        {
            int i = CommonMethod.Asc(Encry.Substring(0, 1)) + CommonMethod.Asc(Encry.Substring(10, 1)) + CommonMethod.Asc(Encry.Substring(20, 1)) + CommonMethod.Asc(Encry.Substring(30, 1)) + CommonMethod.Asc(Encry.Substring(40, 1)) + CommonMethod.Asc(Encry.Substring(50, 1)) + CommonMethod.Asc(Encry.Substring(60, 1));
            return i;
        }
        private string GetKey(string Encry, int split)
        {
            string s1 = Encry.Substring(0, split) + AuthKeys._RijndaelProKey.Substring(split) + Encry.Substring(split) + AuthKeys._RijndaelProKey.Substring(0, split);
            return s1;
        }
        private string GetIv(string Encry, int split)
        {
            string s2 = Encry.Substring(split) + AuthKeys._RijndaelProIV.Substring(0, split) + Encry.Substring(0, split) + AuthKeys._RijndaelProIV.Substring(split);
            return s2;
        }
        internal void SetKeyAndIv(string Encry)
        {
            if (Encry.Length < 128) return;
            int i = GetSplitPosition(Encry);
            int split = i % 64;
            split = split + 32;
            string s1 = GetKey(Encry, split);
            string s2 = GetIv(Encry, split);
            _Key = s1;
            _IV = s2;
        }
        /// <summary>    
        /// 获得密钥    
        /// </summary>    
        /// <returns>密钥</returns>    
        private byte[] GetLegalKey()
        {
            string KeyTemp = _Key;
            mobjCryptoService.GenerateKey();
            byte[] bytTemp = mobjCryptoService.Key;
            int KeyLength = bytTemp.Length;
            if (KeyTemp.Length > KeyLength)
                KeyTemp = KeyTemp.Substring(0, KeyLength);
            else if (KeyTemp.Length < KeyLength)
                KeyTemp = KeyTemp.PadRight(KeyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(KeyTemp);
        }
        /// <summary>    
        /// 获得初始向量IV    
        /// </summary>    
        /// <returns>初试向量IV</returns>    
        private byte[] GetLegalIV()
        {
            string IVTemp = _IV;
            mobjCryptoService.GenerateIV();
            byte[] bytTemp = mobjCryptoService.IV;
            int IVLength = bytTemp.Length;
            if (IVTemp.Length > IVLength)
                IVTemp = IVTemp.Substring(0, IVLength);
            else if (IVTemp.Length < IVLength)
                IVTemp = IVTemp.PadRight(IVLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(IVTemp);
        }
        /// <summary>    
        /// 加密方法    
        /// </summary>    
        /// <param name="Source">待加密的串</param>    
        /// <returns>经过加密的串</returns>    
        internal string Encrypto(string Source)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
            MemoryStream ms = new MemoryStream();
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            byte[] bytOut = ms.ToArray();
            return Convert.ToBase64String(bytOut);
        }
        /// <summary>    
        /// 解密方法    
        /// </summary>    
        /// <param name="Source">待解密的串</param>    
        /// <returns>经过解密的串</returns>    
        internal string Decrypto(string Source)
        {
            byte[] bytIn = Convert.FromBase64String(Source);
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }

    }
}
