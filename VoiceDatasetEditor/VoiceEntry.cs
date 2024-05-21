using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace VoiceDatasetEditor
{
    public class VoiceEntry
    {
        public string filepath = "";
        public string speakerName = "";
        public string language = "JP";
        public string transcription = "";
        
        public decimal length = 0;
    }

    public partial class Form1 : Form
    {
        public List<VoiceEntry> LoadVoiceEntries(string listFilePath, string audioFilePath = "raw\\")
        {
            var voiceEntries = new List<VoiceEntry>();

            foreach (var line in File.ReadLines(listFilePath))
            {
                var parts = line.Split('|');
                if (parts.Length < 4)
                {
                    MessageBox.Show("Each line in the file must contain exactly four parts separated by '|'.", "Invalid .list file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new List<VoiceEntry>();
                }

                string fullPath = Path.GetDirectoryName(listFilePath) + "\\" + audioFilePath;

                var voiceEntry = new VoiceEntry
                {
                    filepath = fullPath + parts[0],
                    speakerName = parts[1],
                    language = parts[2],
                    transcription = parts[3]
                };

                try
                {
                    voiceEntry.length = CalculateAudioLength(fullPath + parts[0]);
                }
                catch
                {
                    voiceEntry.length = -1;
                }

                voiceEntries.Add(voiceEntry);
            }

            if (Settings.Language == "EN")
            {
                lblLoaded.Text = $"Loaded {voiceEntries.Count} transcriptions";
            }
            else
            {
                lblLoaded.Text = $"{voiceEntries.Count}個のファイルを読み込みました";
            }
            Text = $"Voice Dataset Editor - {listFilePath}";
            return voiceEntries;
        }

        public void SaveTranscription(string audioName, string newTranscription)
        {
            var lines = File.ReadAllLines(listFilePath).ToList();
            var updated = false;

            for (int i = 0; i < lines.Count; i++)
            {
                var parts = lines[i].Split('|');
                if (parts.Length != 4)
                {
                    throw new FormatException("Each line in the file must contain exactly four parts separated by '|'.");
                }

                if (parts[0] == audioName)
                {
                    parts[3] = newTranscription;
                    lines[i] = string.Join("|", parts);
                    updated = true;
                    break;
                }
            }

            if (!updated)
            {
                throw new FileNotFoundException($"Audio file name '{audioName}' not found in the list.");
            }

            File.WriteAllLines(listFilePath, lines);
        }

        private decimal CalculateAudioLength(string audioFilePath)
        {
            // Determine the file extension
            string extension = Path.GetExtension(audioFilePath).ToLower();

            // Initialize the audio length in seconds
            double audioLength = -1;

            if (extension == ".wav")
            {
                // Use WaveFileReader for WAV files
                using (var reader = new WaveFileReader(audioFilePath))
                {
                    audioLength = reader.TotalTime.TotalSeconds;
                }
            }
            else if (extension == ".mp3")
            {
                // Use Mp3FileReader for MP3 files
                using (var reader = new Mp3FileReader(audioFilePath))
                {
                    audioLength = reader.TotalTime.TotalSeconds;
                }
            }
            else
            {
                throw new NotSupportedException("File format not supported.");
            }

            // Return the length as a decimal rounded to one decimal place
            return Math.Round((decimal)audioLength, 1);
        }
    }
}
