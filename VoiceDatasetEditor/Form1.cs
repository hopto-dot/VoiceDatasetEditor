using System.Globalization;
using System.Windows.Forms;
using VoiceDatasetEditor.Forms;

namespace VoiceDatasetEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Settings = AppSettings.Load(SettingsFilePath);

            Localise(Settings.Language);

            flowAudioPanel.DragEnter += new DragEventHandler(flowAudioPanel_DragEnterEvent);
            flowAudioPanel.DragDrop += new DragEventHandler(flowAudioPanel_DragDropEvent);

            // menuFindAndReplace.Visible = false;
        }

        public void Localise(string language)
        {
            Settings.Language = language;
            Settings.Save();

            if (language == "JP")
            {
                toolstripFile.Text = "ファイル";
                toolstripEdit.Text = "編集";
                toolstripView.Text = "表示";
                toolstripLanguage.Text = "言語";
                toolstripSettings.Text = "設定";

                btnNextPage.Text = "次へ";
                btnPreviousPage.Text = "前へ";
                btnSave.Text = "ページを保存";
                btnSaveAll.Text = "すべて保存";

                menuLoadDataset.Text = "データセットを読み込む";
                menuSaveDataset.Text = "データセットを保存する";
                menuRefresh.Text = "データセットの再読み込み";
                menuUnload.Text = "データセットの読み込み解除";
                menuFindAndReplace.Text = "検索と置換";

                if (flowAudioPanel.Controls.Count > 0)
                    lblLoaded.Text = $"{voiceEntries.Count}個のファイルを読み込みました";
            }
            else
            {
                toolstripFile.Text = "File";
                toolstripEdit.Text = "Edit";
                toolstripView.Text = "View";
                toolstripLanguage.Text = "Language";
                toolstripSettings.Text = "Settings";

                btnNextPage.Text = "Next";
                btnPreviousPage.Text = "Previous";
                btnSave.Text = "Save page";
                btnSaveAll.Text = "Save all";

                menuLoadDataset.Text = "Load dataset";
                menuSaveDataset.Text = "Save dataset";
                menuRefresh.Text = "Refresh";
                menuUnload.Text = "Unload dataset";
                menuFindAndReplace.Text = "Find and replace";

                if (flowAudioPanel.Controls.Count > 0)
                    lblLoaded.Text = $"Loaded {voiceEntries.Count} transcriptions";
            }

            if (voiceEntries != null && flowAudioPanel.Controls.Count != 0) { LoadPagination(); }
        }

        private const string SettingsFilePath = "settings.json";
        public AppSettings Settings;

        List<VoiceEntry> voiceEntries = new List<VoiceEntry> { };
        int page = 0;

        string listFilePath = "";

        #region form_events
        private void Form1_Shown(object sender, EventArgs e)
        {
            if (Settings.LastList != "")
            {
                listFilePath = Settings.LastList;
                voiceEntries = LoadVoiceEntries(Settings.LastList);

                if (voiceEntries.Count == 0)
                {
                    return;
                }

                LoadPagination();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (Settings.ResizeEntries)
            {
                foreach (VoiceFile voiceFile in flowAudioPanel.Controls)
                {
                    voiceFile.Width = flowAudioPanel.ClientSize.Width - flowAudioPanel.Padding.Horizontal - 10;
                }
            }
        }


        private void LoadPagination()
        {
            lblPage.Text = $"{page + 1} / {(voiceEntries.Count / Settings.ItemsPerPage) + 1}";
            flowAudioPanel.Controls.Clear();
            for (int i = page * Settings.ItemsPerPage; i < (page * Settings.ItemsPerPage) + Settings.ItemsPerPage; i++)
            {
                if (i >= voiceEntries.Count)
                {
                    break;
                }
                AddEntryToPanel(voiceEntries[i]);
            }
        }

        public void LoadFirstPage()
        {
            page = 0;
            LoadPagination();
        }

        private void AddEntryToPanel(VoiceEntry voiceEntry)
        {
            VoiceFile voiceFile = new VoiceFile(voiceEntry, this);
            flowAudioPanel.Controls.Add(voiceFile);
            if (Settings.ResizeEntries)
            {
                voiceFile.Width = flowAudioPanel.ClientSize.Width - flowAudioPanel.Padding.Horizontal - 25;
            }
            else
            {
                voiceFile.Width = 990;
            }
        }

        public void FindAndReplace(string find, string replace)
        {
            int replacements = 0;
            foreach (VoiceEntry voiceEntry in voiceEntries)
            {
                if (voiceEntry.transcription != voiceEntry.transcription.Replace(find, replace))
                {
                    replacements++;
                }
                voiceEntry.transcription = voiceEntry.transcription.Replace(find, replace);
                SaveTranscription(Path.GetFileName(voiceEntry.filepath), voiceEntry.transcription.Replace(find, replace));
            }

            foreach (VoiceFile voiceFile in flowAudioPanel.Controls)
            {
                string oldTranscription = voiceFile.Entry.transcription;
                voiceFile.EditTranscription(oldTranscription.Replace(find, replace));
            }

            if (Settings.Language == "EN")
            {
                MessageBox.Show($"{replacements} transcriptions were replaced", "Find and replace success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"{replacements}件の文字起こしが置換されました", "検索と置換が成功しました", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void UpdateTranscriptionsWithPanel()
        {
            foreach (VoiceFile voiceFile in flowAudioPanel.Controls)
            {
                var entry = voiceEntries.FirstOrDefault(e => e.filepath == voiceFile.Entry.filepath);
                if (entry != null)
                {
                    entry.transcription = voiceFile.Entry.transcription;
                }
            }
        }



        #endregion

        #region buttons
        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            SaveAllVoiceEntries();
        }

        private void SaveAllVoiceEntries()
        {
            foreach (VoiceEntry voiceEntry in voiceEntries)
            {
                SaveTranscription(Path.GetFileName(voiceEntry.filepath), voiceEntry.transcription);
            }
        }

        private void WriteSaveAllVoiceEntries()
        {
            var lines = voiceEntries.Select(entry => $"{Path.GetFileName(entry.filepath)}|{entry.speakerName}|{entry.language}|{entry.transcription}");
            File.WriteAllLines(listFilePath, lines);
        }

        public void DeleteTranscription(VoiceEntry entry)
        {
            // Remove the entry from the list
            voiceEntries.Remove(entry);

            // Update the list file
            WriteSaveAllVoiceEntries();

            // LoadPagination();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (flowAudioPanel.Controls.Count == 0)
            {
                NoneLoaded();
                return;
            }

            foreach (VoiceFile control in flowAudioPanel.Controls)
            {
                SaveTranscription(Path.GetFileName(control.Entry.filepath), control.Entry.transcription);
            }
        }

        void NoneLoaded()
        {
            if (Settings.Language == "EN")
            {
                MessageBox.Show("You must load a dataset first.\n\nGo to [File -> Load dataset] then select a .list file", "No dataset loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("データセットを先に読み込む必要があります。\n\n＜ファイル -> データセットを読み込む＞に移動し、.listファイルを選択してください。", "データセットが読み込まれていません", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (flowAudioPanel.Controls.Count == 0 || voiceEntries == null)
            {
                NoneLoaded();
                return;
            }
            //UpdateTranscriptionsWithPanel();

            page += 1;
            if (page > voiceEntries.Count / 10)
            {
                page = voiceEntries.Count / 10;
            }
            else
            {
                LoadPagination();
            }
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (flowAudioPanel.Controls.Count == 0 || voiceEntries == null)
            {
                NoneLoaded();
                return;
            }
            //UpdateTranscriptionsWithPanel();

            page -= 1;
            if (page < 0)
            {
                page = 0;
            }
            else
            {
                LoadPagination();
            }
        }
        #endregion

        #region file_toolbar
        private void menuLoadDataset_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "List files (*.list)|*.list";
            openFileDialog.Title = "Open Dataset File";
            if (Settings.LastList != "")
            {
                openFileDialog.InitialDirectory = Settings.LastList;
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                listFilePath = openFileDialog.FileName;

                voiceEntries = LoadVoiceEntries(listFilePath);

                LoadPagination();

                if (voiceEntries.Count < 1700)
                {
                    Settings.LastList = listFilePath;
                    Settings.Save();
                }
                else
                {
                    Settings.LastList = "";
                    Settings.Save();
                }
            }
        }

        private void menuSaveDataset_Click(object sender, EventArgs e)
        {
            if (flowAudioPanel.Controls.Count == 0)
            {
                NoneLoaded();
                return;
            }

            foreach (VoiceFile control in flowAudioPanel.Controls)
            {
                SaveTranscription(Path.GetFileName(control.Entry.filepath), control.Entry.transcription);
            }
        }
        #endregion

        #region edit_toolbar
        FindAndReplace findAndReplace;
        private void menuFindAndReplace_Click(object sender, EventArgs e)
        {
            if (findAndReplace != null)
            {
                findAndReplace.Close();
            }
            findAndReplace = new FindAndReplace(this);
            findAndReplace.Show();
        }
        #endregion

        #region view_toolbar
        private void menuRefresh_Click(object sender, EventArgs e)
        {
            LoadPagination();
        }

        private void menuUnload_Click(object sender, EventArgs e)
        {
            page = 0;
            voiceEntries = new List<VoiceEntry>();
            flowAudioPanel.Controls.Clear();

            lblLoaded.Text = "";
            lblPage.Text = "";
        }
        #endregion

        #region language_toolbar
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Localise("EN");
        }

        private void 日本語ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Localise("JP");
        }
        #endregion

        #region settings_toolbar

        Settings settingsMenu;

        private void menuSettings_Click(object sender, EventArgs e)
        {
            if (settingsMenu != null)
            {
                settingsMenu.Close();
            }
            settingsMenu = new Settings(this);
            settingsMenu.Show();
        }
        #endregion

        #region drag drop
        private void flowAudioPanel_DragEnterEvent(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void flowAudioPanel_DragDropEvent(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                listFilePath = files[0];

                voiceEntries = LoadVoiceEntries(files[0]);

                LoadPagination();
            }
        }
        #endregion




    }
}
