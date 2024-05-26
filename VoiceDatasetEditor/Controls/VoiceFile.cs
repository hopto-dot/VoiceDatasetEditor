using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using VoiceDatasetEditor.Classes;

namespace VoiceDatasetEditor
{
    public partial class VoiceFile : UserControl
    {
        public VoiceEntry Entry = new VoiceEntry();
        public Form1 MainForm = new Form1();
        public VoiceFile(VoiceEntry voiceEntry, Form1 mainForm)
        {
            InitializeComponent();
            Entry = voiceEntry;
            MainForm = mainForm;
        }

        private void VoiceFile_Load(object sender, EventArgs e)
        {
            tbxTranscription.Text = Entry.transcription;
            if (Form1.ApplicationSettings.Language == "JP")
            {
                lblLength.Text = $"{Entry.length}秒";
                btnPlay.Text = "再生";
                btnSave.Text = "保存";
                btnDelete.Text = "削除";
            }
            else
            {
                lblLength.Text = $"{Entry.length} seconds";
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Entry.filepath))
            {
                MessageBox.Show("That transcription doesn't have a corresponding audio file!", "Missing audio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MainForm.StopControlAudio(Entry.filepath))
            {
                return;
            }

            // Check if the Shift key is being held down.
            bool playAudioStartingFromHalfWayThrough = Control.ModifierKeys == Keys.Shift;

            MainForm.PlayControlAudio(Entry.filepath, playAudioStartingFromHalfWayThrough);
        }




        private void btnSave_Click(object sender, EventArgs e)
        {
            string filename = Path.GetFileName(Entry.filepath);
            VoiceListParser.SaveTranscription(filename, Entry.transcription, Form1.listFilePath);
        }

        private void tbxTranscription_TextChanged(object sender, EventArgs e)
        {
            Entry.transcription = tbxTranscription.Text;
        }

        public void EditTranscription(string NewTranscription)
        {
            // Entry.transcription = NewTranscription;
            tbxTranscription.Text = NewTranscription;
        }

        

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string message = Form1.ApplicationSettings.Language == "JP" ? "このエントリを削除してもよろしいですか？" : "Are you sure you want to delete this entry?";
            string title = Form1.ApplicationSettings.Language == "JP" ? "確認" : "Confirmation";

            DialogResult result = DialogResult.None;            
            if (!Form1.ApplicationSettings.DeleteAskConfirmation)
            {
                MainForm.DeleteTranscription(Entry);
                Dispose();
            }
            else
            {
                result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            if (result == DialogResult.Yes)
            {
                MainForm.DeleteTranscription(Entry);
                Dispose();
            }
        }
    }
}
