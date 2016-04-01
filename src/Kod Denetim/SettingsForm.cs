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
        }

        private void Save()
        {
            int paramCount = 0;
            int ifCount = 0;
            int funcLen = 0;
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
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
