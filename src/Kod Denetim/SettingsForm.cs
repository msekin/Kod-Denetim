using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeAnalysis
{
    public partial class SettingsForm : BaseForm
    {
        public const int DEFAULT_MaxParamCount = 3;
        public const int DEFAULT_NestedIfDepth = 3;
        public const int DEFAULT_FunctionMaxLength = 100;
        public const float DEFAULT_LCOM_Threshold = 0.8F;
        public const int DEFAULT_CBO_Threshold = 4;

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            Fill();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Fill()
        {
            txtParamCount.Text = Properties.Settings.Default.MaxParamCount.ToString();
            nupdIfCount.Text = Properties.Settings.Default.NestedIfDepth.ToString();
            txtFuncLen.Text = Properties.Settings.Default.FunctionMaxLength.ToString();
            cbNoCommentTesterEnabled.Checked = Settings.NoCommentTesterEnabled;
            lblFrameworkVersion.Text = Settings.DotNETFramework;
            label9.Text = Properties.Settings.Default.LCOM_Threshold.ToString();
            trackBar1.Value = (int)(Properties.Settings.Default.LCOM_Threshold * 10);
            textBox1.Text = Properties.Settings.Default.CBO_Threshold.ToString();
        }

        private void Save()
        {
            int paramCount = 0;
            int ifCount = 0;
            int funcLen = 0;
            float lcomt = 0;
            int cbot = 0;
            if (txtParamCount.Text.Trim() == "")
            {
                paramCount = 0;
            }
            if (nupdIfCount.Text.Trim() == "")
            {
                ifCount = 0;
            }
            if (txtFuncLen.Text.Trim() == "")
            {
                funcLen = 0;
            }
            if(!int.TryParse(txtParamCount.Text, out paramCount))
            {
                MessageBox.Show("Maksimum parametre sayısı geçerli değil.", Settings.ProjectName, MessageBoxButtons.OK);
                return;
            }
            if (!int.TryParse(nupdIfCount.Text, out ifCount))
            {
                MessageBox.Show("İç içe yuvalanmış if sayısı geçerli değil.", Settings.ProjectName, MessageBoxButtons.OK);
                return;
            }
            if (!int.TryParse(txtFuncLen.Text, out funcLen))
            {
                MessageBox.Show("Fonksiyon uzunluğu geçerli değil.", Settings.ProjectName, MessageBoxButtons.OK);
                return;
            }
            Properties.Settings.Default.MaxParamCount = paramCount;
            Properties.Settings.Default.NestedIfDepth = ifCount;
            Properties.Settings.Default.FunctionMaxLength = funcLen;
            Properties.Settings.Default.LCOM_Threshold = trackBar1.Value / 10.0F;
            Properties.Settings.Default.CBO_Threshold = int.Parse(textBox1.Text);
            Properties.Settings.Default.Save();
            this.Close();

            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            float f = trackBar1.Value / 10.0F;
            label9.Text = f.ToString("0.#");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Varsayılan ayarlara dönmek istediğinizden emin misiniz?", "Kod Denetim", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            Properties.Settings.Default.MaxParamCount = DEFAULT_MaxParamCount;
            Properties.Settings.Default.NestedIfDepth = DEFAULT_NestedIfDepth;
            Properties.Settings.Default.FunctionMaxLength = DEFAULT_FunctionMaxLength;
            Properties.Settings.Default.LCOM_Threshold = DEFAULT_LCOM_Threshold;
            Properties.Settings.Default.CBO_Threshold = DEFAULT_CBO_Threshold;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
