using NAudio.Gui;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VoiceDatasetEditor.Classes;
using static VoiceDatasetEditor.Classes.CustomWaveViewer;

namespace VoiceDatasetEditor.Forms
{
    public partial class EditAudio : Form
    {
        string originalFilePath = "";
        string tempFilePath = "temp.wav";
        public EditAudio(string FilePath)
        {
            InitializeComponent();

            originalFilePath = FilePath;
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath); // Ensure the temp file is fresh for each session
            }
            File.Copy(originalFilePath, tempFilePath);
        }


        CustomWaveViewer waveViewer = new CustomWaveViewer();

        private void EditAudio_Load(object sender, EventArgs e)
        {
            panel1.Controls.Add(waveViewer);
            waveViewer.Dock = DockStyle.Fill;
            waveViewer.WaveStream = new NAudio.Wave.WaveFileReader(tempFilePath);
            waveViewer.FitToScreen();

            // Subscribe to the SelectionMade event
            waveViewer.SelectionMade += WaveViewer_SelectionMade;
        }

        private void WaveViewer_SelectionMade(object sender, SelectionEventArgs e)
        {
            outputDevice.Stop();
            // Perform ripple delete directly on the temp file
            RippleDeleteAudio(tempFilePath, tempFilePath, e.StartSample, e.EndSample);

            // Reload the modified audio into a new CustomWaveViewer
            ReloadWaveViewer();
        }

        private void RippleDeleteAudio(string inputFilePath, string outputFilePath, int startSample, int endSample)
        {
            string tempOutputFilePath = "temp_edit.wav";
            using (var reader = new WaveFileReader(inputFilePath))
            {
                using (var writer = new WaveFileWriter(tempOutputFilePath, reader.WaveFormat))
                {
                    int bytesPerSample = reader.WaveFormat.BitsPerSample / 8 * reader.WaveFormat.Channels;
                    long startPos = startSample * bytesPerSample;
                    long endPos = endSample * bytesPerSample;
                    long lengthToCopy = startPos;

                    // Copy the audio data before the section to skip
                    CopyAudio(reader, writer, 0, lengthToCopy);

                    // Skip over the section to delete
                    reader.Position = endPos;

                    // Copy the remaining audio data
                    lengthToCopy = reader.Length - reader.Position;
                    CopyAudio(reader, writer, reader.Position, lengthToCopy);
                }
            }

            waveViewer.DisposeWaveStream();
            if (audioFile != null)
            {
                audioFile.Dispose();
            }

            // Now that the reader and writer are disposed, it's safe to manipulate the files
            File.Delete(inputFilePath);
            File.Move(tempOutputFilePath, inputFilePath);

            // Call ReloadWaveViewer to refresh the audio and visualization
            ReloadWaveViewer();
        }

        private void CopyAudio(WaveFileReader reader, WaveFileWriter writer, long startPos, long lengthToCopy)
        {
            reader.Position = startPos;
            byte[] buffer = new byte[1024];
            while (lengthToCopy > 0)
            {
                int bytesRead = reader.Read(buffer, 0, (int)Math.Min(buffer.Length, lengthToCopy));
                if (bytesRead == 0) break;
                writer.Write(buffer, 0, bytesRead);
                lengthToCopy -= bytesRead;
            }
        }



        private void ReloadWaveViewer()
        {
            panel1.Controls.Clear(); // Remove the old wave viewer

            // Dispose of the existing WaveStream to unlock the file
            if (waveViewer.WaveStream != null)
            {
                waveViewer.WaveStream.Dispose();
            }

            waveViewer = new CustomWaveViewer();
            panel1.Controls.Add(waveViewer);
            waveViewer.Dock = DockStyle.Fill;
            waveViewer.WaveStream = new NAudio.Wave.WaveFileReader(tempFilePath);
            waveViewer.FitToScreen();
            waveViewer.SelectionMade += WaveViewer_SelectionMade; // Ensure the event is subscribed to
        }

        WaveOutEvent outputDevice = new WaveOutEvent();
        AudioFileReader audioFile;
        public void PlayControlAudio()
        {
            outputDevice.Stop();

            audioFile = new AudioFileReader(tempFilePath);
            outputDevice = new WaveOutEvent();
            var volumeProvider = new VolumeSampleProvider(audioFile.ToSampleProvider(), (float)Form1.ApplicationSettings.VolumeBoost);

            outputDevice.Init(volumeProvider);
            outputDevice.Play();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            PlayControlAudio();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            outputDevice.Stop();
        }

        private void EditAudio_FormClosed(object sender, FormClosedEventArgs e)
        {
            waveViewer.DisposeWaveStream();
            outputDevice.Stop();
            if (audioFile != null)
            {
                audioFile.Dispose();
            }

            try
            {
                File.Delete(tempFilePath);
            } catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete temp file: {ex.Message}\n\nIt's a good idea to save now as there might be a later crash caused by this error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            waveViewer.DisposeWaveStream();
            if (File.Exists(originalFilePath.Replace(".wav", "_old.wav")))
            {
                File.Delete(originalFilePath.Replace(".wav", "_old.wav"));
            }
            File.Move(originalFilePath, originalFilePath.Replace(".wav", "_old.wav"));
            File.Copy(tempFilePath, originalFilePath);

            // display message box saying changes were saved depending on the language
            if (Form1.ApplicationSettings.Language == "JP")
            {
                MessageBox.Show("変更が保存されました", "保存成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Changes were saved", "Save success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnDiscard_Click(object sender, EventArgs e)
        {
            waveViewer.DisposeWaveStream();
            outputDevice.Stop();
            if (audioFile != null)
            {
                audioFile.Dispose();
            }
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }

            File.Copy(originalFilePath, tempFilePath);
            waveViewer.WaveStream = new NAudio.Wave.WaveFileReader(tempFilePath);
            this.Size = new Size(this.Width + 1, this.Height + 1);
            this.Size = new Size(this.Width - 1, this.Height - 1);
        }
    }
}
