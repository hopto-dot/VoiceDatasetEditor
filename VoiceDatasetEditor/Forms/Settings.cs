using System;
using System.CodeDom;
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
    public partial class Settings : Form
    {
        private static Settings _instance;
        private Form1 MainForm;

        public static Settings GetInstance(Form1 mainForm)
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new Settings(mainForm);
            }
            return _instance;
        }

        private Settings(Form1 mainForm)
        {
            InitializeComponent();
            MainForm = mainForm;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            bool itemsPerPageIsNumeric;
            int newItemsPerPage;
            itemsPerPageIsNumeric = int.TryParse(tbxItemsPerPage.Text, out newItemsPerPage);

            bool volumeBoostIsNumeric;
            decimal newVolumeBoost;
            volumeBoostIsNumeric = decimal.TryParse(cbbVolumeBoost.Text, out newVolumeBoost);

            if (itemsPerPageIsNumeric)
            {
                if (newItemsPerPage <= 1)
                {
                    newItemsPerPage = 1;
                    tbxItemsPerPage.Text = "1";
                }
                else if (newItemsPerPage > 500)
                {
                    newItemsPerPage = 500;
                    tbxItemsPerPage.Text = "500";
                }
            }
            else
            {
                tbxItemsPerPage.Text = Form1.ApplicationSettings.ItemsPerPage.ToString();
                newItemsPerPage = Form1.ApplicationSettings.ItemsPerPage;
            }

            if (volumeBoostIsNumeric)
            {
                if (newVolumeBoost <= (decimal)0.1)
                {
                    newVolumeBoost = (decimal)0.1;
                    cbbVolumeBoost.Text = "0.5";
                }
                else if (newVolumeBoost > 4)
                {
                    newVolumeBoost = 4;
                    cbbVolumeBoost.Text = "4";
                }
                Form1.ApplicationSettings.VolumeBoost = newVolumeBoost;
            }
            else
            {
                cbbVolumeBoost.Text = Form1.ApplicationSettings.VolumeBoost.ToString();
                newVolumeBoost = Form1.ApplicationSettings.VolumeBoost;
            }


            if (cbbLanguage.Text != Form1.ApplicationSettings.Language)
            {
                MainForm.Localise(cbbLanguage.Text);
            }
            if (newItemsPerPage != Form1.ApplicationSettings.ItemsPerPage)
            {
                Form1.ApplicationSettings.ItemsPerPage = newItemsPerPage;
                Form1.ApplicationSettings.Save();
                MainForm.LoadFirstPage();
            }
            else if (cbResizeEntries.Checked != Form1.ApplicationSettings.ResizeEntries) // <- note else if
            {
                Form1.ApplicationSettings.ResizeEntries = cbResizeEntries.Checked;
                MainForm.LoadFirstPage();
            }

            Form1.ApplicationSettings.ResizeEntries = cbResizeEntries.Checked;

            Form1.ApplicationSettings.Language = cbbLanguage.Text;
            Form1.ApplicationSettings.Save();

            Localise();

            Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            cbbLanguage.Text = Form1.ApplicationSettings.Language;
            tbxItemsPerPage.Text = Form1.ApplicationSettings.ItemsPerPage.ToString();
            cbResizeEntries.Checked = Form1.ApplicationSettings.ResizeEntries;
            cbbVolumeBoost.Text = Form1.ApplicationSettings.VolumeBoost.ToString();

            Localise();
        }

        void Localise()
        {
            if (Form1.ApplicationSettings.Language == "JP")
            {
                Text = "設定";
                lblLanguage.Text = "言語";
                lblItemsPerPage.Text = "ページ当たりの項目数";
                lblResizeEntries.Text = "エントリをリサイズ";
                lblVolumeBoost.Text = "再生音量（一時的）";

                btnSave.Text = "保存";
            }
            else
            {
                Text = "Settings";
                lblLanguage.Text = "Language";
                lblItemsPerPage.Text = "Items per page";
                lblResizeEntries.Text = "Resize entries";
                lblVolumeBoost.Text = "Playback volume (temporary)";

                btnSave.Text = "Save";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
