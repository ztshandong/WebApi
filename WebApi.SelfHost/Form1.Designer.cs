namespace WebApi.SelfHost
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGetMachineSerialNumber = new System.Windows.Forms.Button();
            this.txtMachineSerialHashNumber = new System.Windows.Forms.TextBox();
            this.btnGetRSAKey = new System.Windows.Forms.Button();
            this.btnSHA512 = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnGenAuthorizationini = new System.Windows.Forms.Button();
            this.btnPrivateSign = new System.Windows.Forms.Button();
            this.btnPublicValidate = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.txtSHA512 = new System.Windows.Forms.TextBox();
            this.txtValidateDate = new System.Windows.Forms.DateTimePicker();
            this.txtEncrypt = new System.Windows.Forms.TextBox();
            this.txtDecrypt = new System.Windows.Forms.TextBox();
            this.txtPrivateSign = new System.Windows.Forms.TextBox();
            this.txtPublicValidate = new System.Windows.Forms.TextBox();
            this.txtMachineSerialText = new System.Windows.Forms.TextBox();
            this.btnReadLic = new System.Windows.Forms.Button();
            this.txtLicDate = new System.Windows.Forms.TextBox();
            this.txtLicSign = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGetMachineSerialNumber
            // 
            this.btnGetMachineSerialNumber.Location = new System.Drawing.Point(508, 10);
            this.btnGetMachineSerialNumber.Name = "btnGetMachineSerialNumber";
            this.btnGetMachineSerialNumber.Size = new System.Drawing.Size(75, 21);
            this.btnGetMachineSerialNumber.TabIndex = 0;
            this.btnGetMachineSerialNumber.Text = "获取机器码";
            this.btnGetMachineSerialNumber.UseVisualStyleBackColor = true;
            this.btnGetMachineSerialNumber.Click += new System.EventHandler(this.btnGetMachineSerialNumber_Click);
            // 
            // txtMachineSerialHashNumber
            // 
            this.txtMachineSerialHashNumber.Location = new System.Drawing.Point(12, 12);
            this.txtMachineSerialHashNumber.Name = "txtMachineSerialHashNumber";
            this.txtMachineSerialHashNumber.Size = new System.Drawing.Size(375, 21);
            this.txtMachineSerialHashNumber.TabIndex = 1;
            this.txtMachineSerialHashNumber.Text = "MachineSerialHashNumber";
            // 
            // btnGetRSAKey
            // 
            this.btnGetRSAKey.Location = new System.Drawing.Point(14, 335);
            this.btnGetRSAKey.Name = "btnGetRSAKey";
            this.btnGetRSAKey.Size = new System.Drawing.Size(93, 23);
            this.btnGetRSAKey.TabIndex = 2;
            this.btnGetRSAKey.Text = "生成公钥私钥";
            this.btnGetRSAKey.UseVisualStyleBackColor = true;
            this.btnGetRSAKey.Click += new System.EventHandler(this.btnGetRSAKey_Click);
            // 
            // btnSHA512
            // 
            this.btnSHA512.Location = new System.Drawing.Point(508, 38);
            this.btnSHA512.Name = "btnSHA512";
            this.btnSHA512.Size = new System.Drawing.Size(75, 21);
            this.btnSHA512.TabIndex = 3;
            this.btnSHA512.Text = "SHA512加密";
            this.btnSHA512.UseVisualStyleBackColor = true;
            this.btnSHA512.Click += new System.EventHandler(this.btnSHA512_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(508, 66);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 21);
            this.btnEncrypt.TabIndex = 4;
            this.btnEncrypt.Text = "公钥加密";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnGenAuthorizationini
            // 
            this.btnGenAuthorizationini.Location = new System.Drawing.Point(12, 207);
            this.btnGenAuthorizationini.Name = "btnGenAuthorizationini";
            this.btnGenAuthorizationini.Size = new System.Drawing.Size(95, 21);
            this.btnGenAuthorizationini.TabIndex = 5;
            this.btnGenAuthorizationini.Text = "生成授权文件";
            this.btnGenAuthorizationini.UseVisualStyleBackColor = true;
            this.btnGenAuthorizationini.Click += new System.EventHandler(this.btnGenAuthorizationini_Click);
            // 
            // btnPrivateSign
            // 
            this.btnPrivateSign.Location = new System.Drawing.Point(508, 122);
            this.btnPrivateSign.Name = "btnPrivateSign";
            this.btnPrivateSign.Size = new System.Drawing.Size(75, 21);
            this.btnPrivateSign.TabIndex = 6;
            this.btnPrivateSign.Text = "私钥签名";
            this.btnPrivateSign.UseVisualStyleBackColor = true;
            this.btnPrivateSign.Click += new System.EventHandler(this.btnPrivateSign_Click);
            // 
            // btnPublicValidate
            // 
            this.btnPublicValidate.Location = new System.Drawing.Point(508, 150);
            this.btnPublicValidate.Name = "btnPublicValidate";
            this.btnPublicValidate.Size = new System.Drawing.Size(75, 21);
            this.btnPublicValidate.TabIndex = 7;
            this.btnPublicValidate.Text = "公钥验证";
            this.btnPublicValidate.UseVisualStyleBackColor = true;
            this.btnPublicValidate.Click += new System.EventHandler(this.btnPublicValidate_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(508, 94);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 21);
            this.btnDecrypt.TabIndex = 8;
            this.btnDecrypt.Text = "私钥解密";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // txtSHA512
            // 
            this.txtSHA512.Location = new System.Drawing.Point(12, 40);
            this.txtSHA512.Name = "txtSHA512";
            this.txtSHA512.Size = new System.Drawing.Size(490, 21);
            this.txtSHA512.TabIndex = 9;
            this.txtSHA512.Text = "SHA512";
            // 
            // txtValidateDate
            // 
            this.txtValidateDate.Location = new System.Drawing.Point(393, 12);
            this.txtValidateDate.Name = "txtValidateDate";
            this.txtValidateDate.Size = new System.Drawing.Size(109, 21);
            this.txtValidateDate.TabIndex = 10;
            this.txtValidateDate.Value = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            // 
            // txtEncrypt
            // 
            this.txtEncrypt.Location = new System.Drawing.Point(13, 67);
            this.txtEncrypt.Name = "txtEncrypt";
            this.txtEncrypt.Size = new System.Drawing.Size(490, 21);
            this.txtEncrypt.TabIndex = 11;
            this.txtEncrypt.Text = "公钥加密";
            // 
            // txtDecrypt
            // 
            this.txtDecrypt.Location = new System.Drawing.Point(12, 95);
            this.txtDecrypt.Name = "txtDecrypt";
            this.txtDecrypt.Size = new System.Drawing.Size(490, 21);
            this.txtDecrypt.TabIndex = 12;
            this.txtDecrypt.Text = "私钥解密";
            // 
            // txtPrivateSign
            // 
            this.txtPrivateSign.Location = new System.Drawing.Point(12, 122);
            this.txtPrivateSign.Name = "txtPrivateSign";
            this.txtPrivateSign.Size = new System.Drawing.Size(490, 21);
            this.txtPrivateSign.TabIndex = 13;
            this.txtPrivateSign.Text = "私钥签名";
            // 
            // txtPublicValidate
            // 
            this.txtPublicValidate.Location = new System.Drawing.Point(12, 150);
            this.txtPublicValidate.Name = "txtPublicValidate";
            this.txtPublicValidate.Size = new System.Drawing.Size(490, 21);
            this.txtPublicValidate.TabIndex = 14;
            this.txtPublicValidate.Text = "公钥验证";
            // 
            // txtMachineSerialText
            // 
            this.txtMachineSerialText.Location = new System.Drawing.Point(13, 178);
            this.txtMachineSerialText.Name = "txtMachineSerialText";
            this.txtMachineSerialText.Size = new System.Drawing.Size(489, 21);
            this.txtMachineSerialText.TabIndex = 15;
            this.txtMachineSerialText.Text = "MachineSerialText";
            // 
            // btnReadLic
            // 
            this.btnReadLic.Location = new System.Drawing.Point(128, 207);
            this.btnReadLic.Name = "btnReadLic";
            this.btnReadLic.Size = new System.Drawing.Size(107, 23);
            this.btnReadLic.TabIndex = 16;
            this.btnReadLic.Text = "读取授权文件";
            this.btnReadLic.UseVisualStyleBackColor = true;
            this.btnReadLic.Click += new System.EventHandler(this.btnReadLic_Click);
            // 
            // txtLicDate
            // 
            this.txtLicDate.Location = new System.Drawing.Point(13, 235);
            this.txtLicDate.Name = "txtLicDate";
            this.txtLicDate.Size = new System.Drawing.Size(489, 21);
            this.txtLicDate.TabIndex = 17;
            this.txtLicDate.Text = "授权文件中的日期";
            // 
            // txtLicSign
            // 
            this.txtLicSign.Location = new System.Drawing.Point(14, 262);
            this.txtLicSign.Name = "txtLicSign";
            this.txtLicSign.Size = new System.Drawing.Size(489, 21);
            this.txtLicSign.TabIndex = 17;
            this.txtLicSign.Tag = "";
            this.txtLicSign.Text = "授权文件中的密文";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 370);
            this.Controls.Add(this.txtLicSign);
            this.Controls.Add(this.txtLicDate);
            this.Controls.Add(this.btnReadLic);
            this.Controls.Add(this.txtMachineSerialText);
            this.Controls.Add(this.txtPublicValidate);
            this.Controls.Add(this.txtPrivateSign);
            this.Controls.Add(this.txtDecrypt);
            this.Controls.Add(this.txtEncrypt);
            this.Controls.Add(this.txtValidateDate);
            this.Controls.Add(this.txtSHA512);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnPublicValidate);
            this.Controls.Add(this.btnPrivateSign);
            this.Controls.Add(this.btnGenAuthorizationini);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.btnSHA512);
            this.Controls.Add(this.btnGetRSAKey);
            this.Controls.Add(this.txtMachineSerialHashNumber);
            this.Controls.Add(this.btnGetMachineSerialNumber);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetMachineSerialNumber;
        private System.Windows.Forms.TextBox txtMachineSerialHashNumber;
        private System.Windows.Forms.Button btnGetRSAKey;
        private System.Windows.Forms.Button btnSHA512;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnGenAuthorizationini;
        private System.Windows.Forms.Button btnPrivateSign;
        private System.Windows.Forms.Button btnPublicValidate;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.TextBox txtSHA512;
        private System.Windows.Forms.DateTimePicker txtValidateDate;
        private System.Windows.Forms.TextBox txtEncrypt;
        private System.Windows.Forms.TextBox txtDecrypt;
        private System.Windows.Forms.TextBox txtPrivateSign;
        private System.Windows.Forms.TextBox txtPublicValidate;
        private System.Windows.Forms.TextBox txtMachineSerialText;
        private System.Windows.Forms.Button btnReadLic;
        private System.Windows.Forms.TextBox txtLicDate;
        private System.Windows.Forms.TextBox txtLicSign;
    }
}

