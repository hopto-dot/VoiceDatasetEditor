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
            if (MainForm.Settings.Language == "JP")
            {
                lblLength.Text = $"{Entry.length}秒";
                btnPlay.Text = "再生";
                btnSave.Text = "保存";
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
            
            SoundPlayer soundPlayer = new SoundPlayer(Entry.filepath);
            soundPlayer.Play();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string filename = Path.GetFileName(Entry.filepath);
            MainForm.SaveTranscription(filename, Entry.transcription);
        }

        private void tbxTranscription_TextChanged(object sender, EventArgs e)
        {
            Entry.transcription = tbxTranscription.Text;
        }
    }
}
