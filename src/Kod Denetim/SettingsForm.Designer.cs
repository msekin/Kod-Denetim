namespace CodeAnalysis
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbNoCommentTesterEnabled = new System.Windows.Forms.CheckBox();
            this.nupdIfCount = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtParamCount = new System.Windows.Forms.TextBox();
            this.txtFuncLen = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblFrameworkVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nupdIfCount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(100, 10);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(2, 348);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Maksimum parametre sayısı";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(119, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "İç içe yuvalanmış if sayısı";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(119, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Fonksiyon uzunluğu";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(119, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "NoCommentTester Enabled";
            // 
            // cbNoCommentTesterEnabled
            // 
            this.cbNoCommentTesterEnabled.AutoSize = true;
            this.cbNoCommentTesterEnabled.Enabled = false;
            this.cbNoCommentTesterEnabled.Location = new System.Drawing.Point(282, 155);
            this.cbNoCommentTesterEnabled.Name = "cbNoCommentTesterEnabled";
            this.cbNoCommentTesterEnabled.Size = new System.Drawing.Size(15, 14);
            this.cbNoCommentTesterEnabled.TabIndex = 5;
            this.cbNoCommentTesterEnabled.UseVisualStyleBackColor = true;
            // 
            // nupdIfCount
            // 
            this.nupdIfCount.Location = new System.Drawing.Point(282, 77);
            this.nupdIfCount.Name = "nupdIfCount";
            this.nupdIfCount.Size = new System.Drawing.Size(47, 20);
            this.nupdIfCount.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(296, 330);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Kaydet";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(377, 330);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "İptal";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtParamCount
            // 
            this.txtParamCount.Location = new System.Drawing.Point(282, 41);
            this.txtParamCount.Name = "txtParamCount";
            this.txtParamCount.Size = new System.Drawing.Size(100, 20);
            this.txtParamCount.TabIndex = 6;
            // 
            // txtFuncLen
            // 
            this.txtFuncLen.Location = new System.Drawing.Point(282, 116);
            this.txtFuncLen.Name = "txtFuncLen";
            this.txtFuncLen.Size = new System.Drawing.Size(100, 20);
            this.txtFuncLen.TabIndex = 10;
            this.txtFuncLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(388, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "satır";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(119, 193);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = ".NET Framework";
            // 
            // lblFrameworkVersion
            // 
            this.lblFrameworkVersion.AutoSize = true;
            this.lblFrameworkVersion.Location = new System.Drawing.Point(279, 193);
            this.lblFrameworkVersion.Name = "lblFrameworkVersion";
            this.lblFrameworkVersion.Size = new System.Drawing.Size(131, 13);
            this.lblFrameworkVersion.TabIndex = 13;
            this.lblFrameworkVersion.Text = "[.NET Framework Version]";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 366);
            this.Controls.Add(this.lblFrameworkVersion);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFuncLen);
            this.Controls.Add(this.txtParamCount);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.nupdIfCount);
            this.Controls.Add(this.cbNoCommentTesterEnabled);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ayarlar";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nupdIfCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbNoCommentTesterEnabled;
        private System.Windows.Forms.NumericUpDown nupdIfCount;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtParamCount;
        private System.Windows.Forms.TextBox txtFuncLen;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblFrameworkVersion;
    }
}