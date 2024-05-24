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
        private static FindAndReplace _instance;
        private Form1 MainForm;

        public static FindAndReplace GetInstance(Form1 mainForm)
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new FindAndReplace(mainForm);
            }
            return _instance;
        }

        private FindAndReplace(Form1 mainForm)
        {
            InitializeComponent();
            MainForm = mainForm;
            Localise();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            MainForm.FindAndReplaceEntries(tbxFind.Text, tbxReplace.Text);
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
