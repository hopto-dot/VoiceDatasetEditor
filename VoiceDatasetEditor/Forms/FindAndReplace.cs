using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceDatasetEditor.Forms
{
    public partial class FindAndReplace : Form
    {
        Form1 Mainform;
        public FindAndReplace(Form1 form1)
        {
            InitializeComponent();
            Mainform = form1;

            Localise();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            Mainform.FindAndReplace(tbxFind.Text, tbxReplace.Text);
            Close();
        }

        private void Localise()
        {
            if (Form1.ApplicationSettings.Language == "JP")
            {
                btnCancel.Text = "キャンセル";
                btnReplaceAll.Text = "すべて置換";

                lblFindWhat.Text = "検索する文字列：";
                lblReplaceWith.Text = "置換する文字列：";

                tbxFind.PlaceholderText = "";
                tbxReplace.PlaceholderText = "";
            }
        }
    }
}
