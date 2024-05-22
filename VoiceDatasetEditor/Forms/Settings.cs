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
        Form1 MainForm = new Form1();
        public Settings(Form1 mainForm)
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
                tbxItemsPerPage.Text = MainForm.Settings.ItemsPerPage.ToString();
                newItemsPerPage = MainForm.Settings.ItemsPerPage;
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
                MainForm.Settings.VolumeBoost = newVolumeBoost;
            }
            else
            {
                cbbVolumeBoost.Text = MainForm.Settings.VolumeBoost.ToString();
                newVolumeBoost = MainForm.Settings.VolumeBoost;
            }


            if (cbbLanguage.Text != MainForm.Settings.Language)
            {
                MainForm.Localise(cbbLanguage.Text);
            }
            if (newItemsPerPage != MainForm.Settings.ItemsPerPage)
            {
                MainForm.Settings.ItemsPerPage = newItemsPerPage;
                MainForm.LoadFirstPage();
            }
            else if (cbResizeEntries.Checked != MainForm.Settings.ResizeEntries) // <- note else if
            {
                MainForm.Settings.ResizeEntries = cbResizeEntries.Checked;
                MainForm.LoadFirstPage();
            }

            MainForm.Settings.ResizeEntries = cbResizeEntries.Checked;

            MainForm.Settings.Language = cbbLanguage.Text;
            MainForm.Settings.Save();

            Localise();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            cbbLanguage.Text = MainForm.Settings.Language;
            tbxItemsPerPage.Text = MainForm.Settings.ItemsPerPage.ToString();
            cbResizeEntries.Checked = MainForm.Settings.ResizeEntries;
            cbbVolumeBoost.Text = MainForm.Settings.VolumeBoost.ToString();

            Localise();
        }

        void Localise()
        {
            if (MainForm.Settings.Language == "JP")
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
