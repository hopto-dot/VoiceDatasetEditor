using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using VoiceDatasetEditor.Forms;
using VoiceDatasetEditor.Classes;
using NAudio.Wave;

namespace VoiceDatasetEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ApplicationSettings = AppSettings.Load(SettingsFilePath);

            Localise(ApplicationSettings.Language);

            flowAudioPanel.DragEnter += new DragEventHandler(flowAudioPanel_DragEnterEvent);
            flowAudioPanel.DragDrop += new DragEventHandler(flowAudioPanel_DragDropEvent);
        }

        public void Localise(string language)
        {
            ApplicationSettings.Language = language;
            ApplicationSettings.Save();

            if (language == "JP")
            {
                toolstripFile.Text = "ファイル";
                toolstripEdit.Text = "編集";
                toolstripView.Text = "表示";
                toolstripLanguage.Text = "言語";
                toolstripSettings.Text = "設定";

                btnNextPage.Text = "次へ";
                btnPreviousPage.Text = "前へ";
                btnSaveAll.Text = "すべて保存";

                menuLoadDataset.Text = "データセットを読み込む";
                menuSaveDataset.Text = "データセットを保存する";
                menuOpenListFile.Text = ".listファイルを開く";
                menuOpenListFolder.Text = ".listのフォルダを開く";

                menuRefresh.Text = "データセットの再読み込み";
                menuUnload.Text = "データセットの読み込み解除";
                menuFindAndReplace.Text = "検索と置換";
                menuCalculateTotalAudio.Text = "合計音声長を計算する";
                menuDeleteTranscriptionlessAudio.Text = "文字起こしがない音声を削除";

                menuSortBy.Text = "ソート";
                menuSortByFilename.Text = "ファイル名でソート";
                menuSortByTranscription.Text = "文字起こしでソート";
                menuSortByAudioLength.Text = "音声長でソート";
                menuSortByReverse.Text = "逆順でソート";

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
                btnSaveAll.Text = "Save all";

                menuLoadDataset.Text = "Load dataset";
                menuSaveDataset.Text = "Save dataset";
                menuOpenListFile.Text = "Open .list file";
                menuOpenListFolder.Text = "Open .list folder";

                menuRefresh.Text = "Refresh";
                menuUnload.Text = "Unload dataset";
                menuFindAndReplace.Text = "Find and replace";
                menuCalculateTotalAudio.Text = "Calculate total audio length";
                menuDeleteTranscriptionlessAudio.Text = "Delete transcriptionless audio";

                menuSortBy.Text = "Sort by";
                menuSortByFilename.Text = "File name";
                menuSortByTranscription.Text = "Transcription";
                menuSortByAudioLength.Text = "Audio length";
                menuSortByReverse.Text = "Reverse order";

                if (flowAudioPanel.Controls.Count > 0)
                    lblLoaded.Text = $"Loaded {voiceEntries.Count} transcriptions";
            }

            if (voiceEntries != null && flowAudioPanel.Controls.Count != 0) { LoadPagination(); }
        }

        private const string SettingsFilePath = "settings.json";
        public static AppSettings ApplicationSettings;

        List<VoiceEntry> voiceEntries = new List<VoiceEntry> { };
        int page = 0;

        public bool unsavedChanges = false;


        public static string listFilePath = "";

        #region audio
        WaveOutEvent outputDevice = new WaveOutEvent();
        AudioFileReader audioFile;
        public void PlayControlAudio(string filepath, bool playAudioStartingFromHalfWayThrough = false)
        {
            outputDevice.Stop();

            audioFile = new AudioFileReader(filepath);
            outputDevice = new WaveOutEvent();
            var volumeProvider = new VolumeSampleProvider(audioFile.ToSampleProvider(), (float)ApplicationSettings.VolumeBoost);

            if (playAudioStartingFromHalfWayThrough)
            {
                audioFile.Position = (long)(audioFile.Length * 0.5);
            }

            outputDevice.Init(volumeProvider);
            outputDevice.Play();
        }


        public Boolean StopControlAudio(string filepath)
        {
            if (audioFile == null) { return false; }
            if (outputDevice.PlaybackState == PlaybackState.Playing) // If the user clicked "Stop" and audio is playing
            {
                if (audioFile.FileName == filepath) // and the same "Stop" button was clicked
                {
                    outputDevice.Stop(); // stop the audio
                    audioFile = null;
                    return true;
                }
            }

            
            
            return false;
        }

        #endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Right))
            {
                btnNextPage_Click(this, new EventArgs());
                return true;
            }
            else if (keyData == (Keys.Control | Keys.Left))
            {
                btnPreviousPage_Click(this, new EventArgs());
                return true;
            }
            if (keyData == (Keys.Control | Keys.H))
            {
                menuFindAndReplace_Click(this, new EventArgs());
                return true;
            }
            if (keyData == (Keys.Control | Keys.S))
            {
                btnSave_Click(this, new EventArgs());
                return true;
            }
            if (keyData == (Keys.Control | Keys.O))
            {
                menuLoadDataset_Click_1(this, new EventArgs());
                return true;
            }
            if (keyData == (Keys.Control | Keys.I))
            {
                menuSettings_Click(this, new EventArgs());
                return true;
            }
            if (keyData == (Keys.Control | Keys.W))
            {
                // Ask user for confirmation in English or Japanese depending on setting
                if (ApplicationSettings.Language == "EN")
                {
                    if (MessageBox.Show("Are you sure you want to unload the dataset?", "Unload dataset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        menuUnload_Click(this, new EventArgs());
                    }
                }
                else
                {
                    if (MessageBox.Show("データセットをアンロードしてもよろしいですか？", "データセットのアンロード", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        menuUnload_Click(this, new EventArgs());
                    }
                }
                
                menuUnload_Click(this, new EventArgs());
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData); // Call base method for default handling
        }

        #region form_events
        private void Form1_Shown(object sender, EventArgs e)
        {
            if (ApplicationSettings.LastList != "" && ApplicationSettings.LastList.EndsWith(".list"))
            {
                listFilePath = ApplicationSettings.LastList;
                voiceEntries = VoiceListParser.LoadVoiceEntries(ApplicationSettings.LastList);

                if (voiceEntries.Count == 0)
                {
                    ApplicationSettings.LastList = "";
                    ApplicationSettings.Save();
                    return;
                }
                else
                {
                    updateLoadedCountLabels();
                }

                LoadPagination();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (ApplicationSettings.ResizeEntries)
            {
                foreach (VoiceFile voiceFile in flowAudioPanel.Controls)
                {
                    voiceFile.Width = flowAudioPanel.ClientSize.Width - flowAudioPanel.Padding.Horizontal - 15;
                }
            }
        }


        private void LoadPagination()
        {
            panelClientWidth = flowAudioPanel.ClientSize.Width;

            lblPage.Text = $"{page + 1} / {(voiceEntries.Count / ApplicationSettings.ItemsPerPage) + 1}";
            flowAudioPanel.Controls.Clear();
            for (int i = page * ApplicationSettings.ItemsPerPage; i < (page * ApplicationSettings.ItemsPerPage) + ApplicationSettings.ItemsPerPage; i++)
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

        private int panelClientWidth = 0;

        private void AddEntryToPanel(VoiceEntry voiceEntry)
        {
            VoiceFile voiceFile = new VoiceFile(voiceEntry, this);
            flowAudioPanel.Controls.Add(voiceFile);
            if (ApplicationSettings.ResizeEntries)
            {
                voiceFile.Width = panelClientWidth - flowAudioPanel.Padding.Horizontal - 25;
            }
            else
            {
                voiceFile.Width = 985;
            }
        }

        public void FindAndReplaceEntries(string find, string replace)
        {
            int replacements = 0;
            foreach (VoiceEntry voiceEntry in voiceEntries)
            {
                if (voiceEntry.transcription != voiceEntry.transcription.Replace(find, replace))
                {
                    replacements++;
                }
                voiceEntry.transcription = voiceEntry.transcription.Replace(find, replace);
                VoiceListParser.SaveTranscription(Path.GetFileName(voiceEntry.filepath), voiceEntry.transcription.Replace(find, replace), listFilePath);
            }

            foreach (VoiceFile voiceFile in flowAudioPanel.Controls)
            {
                string oldTranscription = voiceFile.Entry.transcription;
                voiceFile.EditTranscription(oldTranscription.Replace(find, replace));
            }

            if (ApplicationSettings.Language == "EN")
            {
                MessageBox.Show($"{replacements} transcriptions were replaced", "Find and replace success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"{replacements}件の文字起こしが置換されました", "検索と置換が成功しました", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (replacements > 0)
            {
                unsavedChanges = true;
                updateLoadedCountLabels();
            }
        }

        public void updateLoadedCountLabels()
        {
            if (ApplicationSettings.Language == "EN")
            {
                lblLoaded.Text = $"Loaded {voiceEntries.Count} transcriptions";
            }
            else
            {
                lblLoaded.Text = $"{voiceEntries.Count}個のファイルを読み込みました";
            }
            if (string.IsNullOrEmpty(listFilePath))
            {
                Text = "Voice Dataset Editor";
            }
            else
            {
                Text = $"Voice Dataset Editor - {listFilePath}" + (unsavedChanges ? "*" : "");
            }
        }

        #endregion

        #region buttons
        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            VoiceListParser.WriteSaveAllVoiceEntries(voiceEntries, listFilePath);
            unsavedChanges = false;
            updateLoadedCountLabels();
        }

        // Update the methods that make changes to the data
        public void DeleteTranscription(VoiceEntry entry)
        {
            voiceEntries.Remove(entry);
            unsavedChanges = true;
            updateLoadedCountLabels();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (flowAudioPanel.Controls.Count == 0)
            {
                ShowNoDataLoadedMsgBox();
                return;
            }

            foreach (VoiceFile control in flowAudioPanel.Controls)
            {
                VoiceListParser.SaveTranscription(Path.GetFileName(control.Entry.filepath), control.Entry.transcription, listFilePath);
            }
            unsavedChanges = false;
            updateLoadedCountLabels();
        }

        void ShowNoDataLoadedMsgBox()
        {
            if (ApplicationSettings.Language == "EN")
            {
                MessageBox.Show("You must load a dataset first.\n\nGo to [File -> Load dataset] then select a .list file", "No dataset loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("データセットを先に読み込む必要があります。\n\n＜ファイル -> データセットを読み込む＞に移動し、.listファイルを選択してください。", "データセットが読み込まれていません", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (flowAudioPanel.Controls.Count == 0 || voiceEntries == null)
            {
                ShowNoDataLoadedMsgBox();
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
        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (flowAudioPanel.Controls.Count == 0 || voiceEntries == null)
            {
                ShowNoDataLoadedMsgBox();
                return;
            }
            //UpdateTranscriptionsWithPanel();

            page += 1;
            if (page > voiceEntries.Count / ApplicationSettings.ItemsPerPage)
            {
                page = voiceEntries.Count / ApplicationSettings.ItemsPerPage;
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
            if (ApplicationSettings.LastList != "")
            {
                openFileDialog.InitialDirectory = ApplicationSettings.LastList;
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                listFilePath = openFileDialog.FileName;

                voiceEntries = VoiceListParser.LoadVoiceEntries(listFilePath);

                updateLoadedCountLabels();

                LoadPagination();

                if (voiceEntries.Count <= 2000)
                {
                    ApplicationSettings.LastList = listFilePath;
                    ApplicationSettings.Save();
                }
                else
                {
                    ApplicationSettings.LastList = "";
                    ApplicationSettings.Save();
                }
            }
        }

        private void menuSaveDataset_Click(object sender, EventArgs e)
        {
            if (flowAudioPanel.Controls.Count == 0)
            {
                ShowNoDataLoadedMsgBox();
                return;
            }

            VoiceListParser.WriteSaveAllVoiceEntries(voiceEntries, listFilePath);
        }

        private void menuOpenListFile_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(listFilePath))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = listFilePath,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            else
            {
                MessageBox.Show("No list file is currently loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuOpenListFolder_Click(object sender, EventArgs e)
        {
            // Open the folder (getfolderpath of listFilePath) in explorer
            if (!string.IsNullOrEmpty(listFilePath))
            {
                System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{listFilePath}\"");
            }
            else
            {
                MessageBox.Show("No list file is currently loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region edit_toolbar
        private void menuFindAndReplace_Click(object sender, EventArgs e)
        {
            FindAndReplace findAndReplace = FindAndReplace.GetInstance(this);
            if (!findAndReplace.Visible)
            {
                findAndReplace.Show();
            }
            else
            {
                findAndReplace.BringToFront();
            }
        }

        private void menuDeleteTranscriptionlessAudio_Click(object sender, EventArgs e)
        {
            VoiceListParser.DeleteTranscriptionlessAudio(voiceEntries, listFilePath);
        }
        #endregion

        #region view_toolbar
        private void menuRefresh_Click(object sender, EventArgs e)
        {
            page = 0;
            LoadPagination();
        }

        private void menuUnload_Click(object sender, EventArgs e)
        {
            page = 0;
            voiceEntries = new List<VoiceEntry>();
            flowAudioPanel.Controls.Clear();

            lblLoaded.Text = "";
            lblPage.Text = "";
            Text = "Voice Dataset Editor";
            listFilePath = "";
        }

        private void menuCalculateTotalAudio_Click(object sender, EventArgs e)
        {
            decimal totalLength = 0;
            foreach (VoiceEntry voiceEntry in voiceEntries)
            {
                if (voiceEntry.length > 0)
                {
                    totalLength += voiceEntry.length;
                }
            }

            if (ApplicationSettings.Language == "EN")
            {
                if (Math.Floor(totalLength / 3600) >= 1)
                {
                    MessageBox.Show($"Total audio length: {Math.Floor(totalLength / 3600)} hours and {Math.Round((totalLength % 3600) / 60, 1)} minutes", "Total audio length", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Total audio length: {Math.Floor(totalLength / 60)} minutes and {totalLength % 60} seconds", "Total audio length", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (Math.Floor(totalLength / 3600) >= 1)
                {
                    MessageBox.Show($"合計音声長: {Math.Floor(totalLength / 3600)}時間{Math.Round((totalLength % 3600) / 60, 1)}分", "合計音声長", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"合計音声長: {Math.Floor(totalLength / 60)}分{totalLength % 60}秒", "合計音声長", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

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
        private void menuSettings_Click(object sender, EventArgs e)
        {
            Settings settingsMenu = Settings.GetInstance(this);
            if (!settingsMenu.Visible)
            {
                settingsMenu.Show();
            }
            else
            {
                settingsMenu.BringToFront();
            }
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

                voiceEntries = VoiceListParser.LoadVoiceEntries(files[0]);

                if (voiceEntries.Count == 0)
                {
                    return;
                }
                
                updateLoadedCountLabels();

                LoadPagination();

                if (voiceEntries.Count <= 2000)
                {
                    ApplicationSettings.LastList = listFilePath;
                    ApplicationSettings.Save();
                }
                else
                {
                    ApplicationSettings.LastList = "";
                    ApplicationSettings.Save();
                }
            }
        }

        #endregion

        #region menu_sort
        private void menuSortByFilename_Click(object sender, EventArgs e)
        {
            voiceEntries.Sort((x, y) => Path.GetFileName(x.filepath).CompareTo(Path.GetFileName(y.filepath)));
            LoadFirstPage();
        }

        private void menuSortByTranscription_Click(object sender, EventArgs e)
        {
            voiceEntries.Sort((x, y) => x.transcription.CompareTo(y.transcription));
            LoadFirstPage();
        }

        private void menuSortByAudioLength_Click(object sender, EventArgs e)
        {
            voiceEntries.Sort((x, y) => y.length.CompareTo(x.length));
            LoadFirstPage();
        }

        private void menuSortByReverse_Click(object sender, EventArgs e)
        {
            voiceEntries.Reverse();
            LoadFirstPage();
        }
        #endregion


    }
}
