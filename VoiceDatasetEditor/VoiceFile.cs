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
using NAudio.Wave;

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

            //SoundPlayer soundPlayer = new SoundPlayer(Entry.filepath);
            //soundPlayer.Play();

            var audioFile = new AudioFileReader(Entry.filepath);
            WaveOutEvent outputDevice = new WaveOutEvent();
            var volumeProvider = new VolumeSampleProvider(audioFile.ToSampleProvider(), (float)MainForm.Settings.VolumeBoost);

            outputDevice.Init(volumeProvider);
            outputDevice.Play();
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

        public void EditTranscription(string NewTranscription)
        {
            // Entry.transcription = NewTranscription;
            tbxTranscription.Text = NewTranscription;
        }

        private class VolumeSampleProvider : ISampleProvider
        {
            private readonly ISampleProvider source;
            private readonly float volumeBoost;

            public VolumeSampleProvider(ISampleProvider source, float volumeBoost)
            {
                this.source = source;
                this.volumeBoost = volumeBoost;
            }

            public WaveFormat WaveFormat => source.WaveFormat;

            public int Read(float[] buffer, int offset, int count)
            {
                int samplesRead = source.Read(buffer, offset, count);

                // Apply volume boost
                for (int i = offset; i < offset + samplesRead; i++)
                {
                    buffer[i] *= volumeBoost;
                }

                return samplesRead;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string message = MainForm.Settings.Language == "JP" ? "このエントリを削除してもよろしいですか？" : "Are you sure you want to delete this entry?";
            string title = MainForm.Settings.Language == "JP" ? "確認" : "Confirmation";
            
            var result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MainForm.DeleteTranscription(Entry);
                Dispose();
            }
        }
    }
}
