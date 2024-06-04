using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using VoiceDatasetEditor.Forms;
using NAudio.Wave;

namespace VoiceDatasetEditor.Classes
{
    public static class VoiceListParser
    {
        public static List<VoiceEntry> LoadVoiceEntries(string listFilePath, string audioFilePath = "raw\\")
        {
            var voiceEntries = new List<VoiceEntry>();

            foreach (var line in File.ReadLines(listFilePath))
            {
                var parts = line.Split('|');
                if (parts.Length < 4)
                {
                    if (Form1.ApplicationSettings.Language == "JP")
                    {
                        MessageBox.Show("ファイルの各行には、「|」で区切られた４つの部分が含まれている必要があります。", ".list ファイルが無効です", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Each line in the file must contain exactly four parts separated by '|'.", "Invalid .list file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
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

            
            return voiceEntries;
        }

        public static void WriteSaveAllVoiceEntries(List<VoiceEntry> voiceEntries, string listFilePath)
        {
            var lines = voiceEntries.Select(entry => $"{Path.GetFileName(entry.filepath)}|{entry.speakerName}|{entry.language}|{entry.transcription}");
            File.WriteAllLines(listFilePath, lines);
        }

        public static void SaveTranscription(string audioName, string newTranscription, string listFilePath)
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

        public static void DeleteTranscriptionlessAudio(List<VoiceEntry> voiceEntries, string listFolderPath)
        {
            string movedFolderPath = Path.GetDirectoryName(listFolderPath) + "\\moved";

            Directory.CreateDirectory(movedFolderPath);

            var audioFiles = Directory.GetFiles(Path.GetDirectoryName(listFolderPath) + "\\raw");

            int toBeMoved = 0;
            foreach (var audioFile in audioFiles)
            {
                string audioFileName = Path.GetFileName(audioFile);
                if (!audioFileName.EndsWith(".wav") && !audioFileName.EndsWith(".mp3"))
                    continue;

                if (!voiceEntries.Any(ve => ve.filepath == audioFile))
                {
                    toBeMoved++;
                }
            }

            // Ask for confirmation before moving files
            string message = Form1.ApplicationSettings.Language == "JP"
                ? $"本当に{toBeMoved}個の音声ファイルを移動しますか？"
                : $"Are you sure you want to move {toBeMoved} audio files?";

            DialogResult dialogResult = MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                int moved = 0;
                foreach (var audioFile in audioFiles)
                {
                    string audioFileName = Path.GetFileName(audioFile);
                    if (!audioFileName.EndsWith(".wav") && !audioFileName.EndsWith(".mp3"))
                        continue;

                    if (!voiceEntries.Any(ve => ve.filepath == audioFile))
                    {
                        var movedFilePath = Path.Combine(movedFolderPath, Path.GetFileName(audioFile));
                        File.Move(audioFile, movedFilePath);
                        moved++;
                    }
                }

                MessageBox.Show($"Moved {moved} audio files to '{movedFolderPath}'.", "Transcriptionless audio files removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }







        private static decimal CalculateAudioLength(string audioFilePath)
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
