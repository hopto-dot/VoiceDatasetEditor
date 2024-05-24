using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;

namespace VoiceDatasetEditor.Classes
{
    public class AppSettings
    {
        public string Language { get; set; } = "EN";
        public string LastList { get; set; } = "";
        public int ItemsPerPage { get; set; } = 10;

        public bool ResizeEntries = false;

        public decimal VolumeBoost = 1;

        private static string _savePath = "";
        public static AppSettings Load(string filePath)
        {
            _savePath = filePath;
            if (!File.Exists(filePath))
            {
                var defaultSettings = new AppSettings();
                defaultSettings.Save();
                return defaultSettings;
            }

            try
            {
                var json = File.ReadAllText(filePath);
                var settings = JsonConvert.DeserializeObject<AppSettings>(json);
                if (settings == null)
                    settings = new AppSettings();

                if (settings.Language != "JP" && settings.Language != "EN") { settings.Language = "EN"; }

                if (settings.ItemsPerPage < 1) { settings.ItemsPerPage = 1; }
                if (settings.ItemsPerPage > 1000) { settings.ItemsPerPage = 1000; }

                double[] validValues = { 0.5, 1, 2, 3, 4 };
                if (!Array.Exists(validValues, element => element == (double)settings.VolumeBoost))
                {
                    settings.VolumeBoost = 1;
                }


                return settings;
            }
            catch (Exception)
            {
                return new AppSettings();
            }
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(_savePath, json);
        }

        AppSettings()
        {
            if (IsOperatingSystemInJapanese())
            {
                Language = "JP";
            }
        }

        public static bool IsOperatingSystemInJapanese()
        {
            // Get the current culture info
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            // Check if the two-letter ISO language name is "ja" (Japanese)
            return currentCulture.TwoLetterISOLanguageName.Equals("ja", StringComparison.OrdinalIgnoreCase);
        }
    }
}
