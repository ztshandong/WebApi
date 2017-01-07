using System;
using System.IO;
using System.Windows.Forms;

using WebApi.Core;
using WebApi.Public;

namespace WebApi.SelfHost
{

    internal partial class Form1 : Form
    {
        RSAAuth _RSA = new RSAAuth();

        private string _Privatekey;
        private string _PublicKey;
        internal Form1()
        {
            InitializeComponent();
            if (File.Exists(AuthKeys._PublicKeyPath))
                _PublicKey = _RSA.ReadPublicKey(AuthKeys._PublicKeyPath);
            if (File.Exists(AuthKeys._PrivatekeyPath))
                _Privatekey = _RSA.ReadPrivateKey(AuthKeys._PrivatekeyPath);
        }

        private void btnGetMachineSerialNumber_Click(object sender, EventArgs e)
        {
            string s = GetComputerInfo.GetCpuID() + GetComputerInfo.GetHDid();
            txtMachineSerialText.Text = s;
            string str = _RSA.GetSHA512Hash(s + txtValidateDate.Value.ToString("yyyy-MM-dd") + "4x}ty#N3*w[2bXK2ne(DRLKov%NhmJ#Z");
            txtMachineSerialHashNumber.Text = str;
        }
        private void btnGetRSAKey_Click(object sender, EventArgs e)
        {
            _RSA.RSAKey(AuthKeys._PrivatekeyPath, AuthKeys._PublicKeyPath);
        }

        private void btnSHA512_Click(object sender, EventArgs e)
        {
            string s = _RSA.GetSHA512Hash(txtMachineSerialHashNumber.Text);
            txtSHA512.Text = s;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string s = _RSA.RSAEncrypt(_PublicKey, txtSHA512.Text);
            txtEncrypt.Text = s;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            string s = _RSA.RSADecrypt(_Privatekey, txtEncrypt.Text);
            txtDecrypt.Text = s;
        }

        private void btnPrivateSign_Click(object sender, EventArgs e)
        {
            string strHash = _RSA.GetSHA512Hash(txtMachineSerialHashNumber.Text.ToString());
            string strSign = _RSA.SignatureFormatter(_Privatekey, strHash);
            txtPrivateSign.Text = strSign;
        }
        private void btnPublicValidate_Click(object sender, EventArgs e)
        {
            string strHash = _RSA.GetSHA512Hash(txtMachineSerialHashNumber.Text.ToString());
            string strSign = _RSA.SignatureFormatter(_Privatekey, strHash);
            if (_RSA.SignatureDeformatter(_PublicKey, strHash, strSign))
            {
                txtPublicValidate.Text = "对";
            }
            else
            {
                txtPublicValidate.Text = "错";
            }
        }

        private void btnGenAuthorizationini_Click(object sender, EventArgs e)
        {
            string s = _RSA.GetSHA512Hash(txtMachineSerialText.Text + txtValidateDate.Value.ToString("yyyy-MM-dd") + "4x}ty#N3*w[2bXK2ne(DRLKov%NhmJ#Z");
            string strHash = _RSA.GetSHA512Hash(s);
            string strSign = _RSA.SignatureFormatter(_Privatekey, strHash);

            string PeriodDate = txtValidateDate.Value.ToString("yyyy-MM-dd");
            string SignValue = strSign;
            Write2Lic.GenLic(PeriodDate, SignValue, AuthKeys._AuthFileKEY, AuthKeys._AuthFilePath,"sfjSDJF^&*","VLKN87sd","sdfhIEy#");
            //IniFile cfg = new IniFile(_AuthFilePath);
            //if (cfg != null)
            //{
            //    cfg.IniWriteValue("AuthorizationInfo", "PeriodDate", txtValidateDate.Value.ToString("yyyy-MM-dd"));
            //    cfg.IniWriteValue("AuthorizationInfo", "SignValue", strSign);
            //}
        }

        private void btnReadLic_Click(object sender, EventArgs e)
        {
            //MyLicense lic = ReadFromLic.ReadLic(AuthKeys._AuthFileKEY, AuthKeys._AuthFilePath);
            //txtLicDate.Text = lic.PeriodDate;
            //txtLicSign.Text = lic.SignValue;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtValidateDate.Value = DateTime.Now.AddDays(2);
        }

        //private void Write2lic(MyLicense lic,string key)
        //{
        //    if (key != _LicKey) return;
        //    byte[] bs = ZipTools.CompressionObject(lic);
        //    File.WriteAllBytes(_AuthFilePath, bs);
        //}
    }

}
