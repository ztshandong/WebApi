using System;
using System.IO;

using WebApi.Public;

namespace WebApi.Core
{
    [Serializable]
    public class MyLicense
    {
        private string _PeriodDate;
        public string PeriodDate { get { return _PeriodDate; } set { _PeriodDate = value; } }
        private string _SignValue;
        public string SignValue { get { return _SignValue; } set { _SignValue = value; } }
    }

    public class Write2Lic
    {
        public static void GenLic(string dfyIU, string ljkFG, string sSDFs3, string tSDFfd, string dgIOUSED,string Sdferoi,string LSDJFe8r)
        {
            DoGenLic(dfyIU, ljkFG, sSDFs3, tSDFfd, dgIOUSED, Sdferoi, LSDJFe8r);
        }

        private static void DoGenLic(string PeriodDate, string SignValue, string LicKey, string LicPath, string s1, string s2, string s3)
        {
            MyLicense lic = new MyLicense();
            if (LicKey != AuthKeys._AuthFileKEY) return;
            lic.PeriodDate = PeriodDate;
            lic.SignValue = SignValue;
            byte[] bs = ZipTools.CompressionObject(lic);
            File.WriteAllBytes(LicPath, bs);
        }
    }

    internal class ReadFromLic
    {
        internal static MyLicense ReadLic(string LicKey, string Path)
        {
            if (LicKey != AuthKeys._AuthFileKEY) return null;
            byte[] bs = File.ReadAllBytes(Path);
            MyLicense lic = (MyLicense)ZipTools.DecompressionObject(bs);
            return lic;
        }
    }

 
}
