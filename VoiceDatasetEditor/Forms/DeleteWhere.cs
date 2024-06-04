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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VoiceDatasetEditor.Forms
{
    public partial class DeleteWhere : Form
    {
        private Form1 _form1;
        private List<VoiceEntry> _voiceEntries;

        private static DeleteWhere _instance = null;

        private static readonly object _lock = new object();

        private DeleteWhere(Form1 form1, List<VoiceEntry> voiceEntries)
        {
            _form1 = form1;
            _voiceEntries = voiceEntries;
            InitializeComponent();
            Localise();
            cbbDeleteCondition.SelectedIndex = 2;
        }

        public static DeleteWhere GetInstance(Form1 mainForm, List<VoiceEntry> voiceEntries)
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new DeleteWhere(mainForm, voiceEntries);
            }
            return _instance;
        }

        private Dictionary<string, IConditionStrategy> strategies = new Dictionary<string, IConditionStrategy>
        {
            { "audio length <", new AudioLengthLessThanStrategy() },
            { "audio length >", new AudioLengthGreaterThanStrategy() },
            { "contains text", new ContainsTextStrategy() },
        };

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxValue.Text))
            {
                string errorMessage = Form1.ApplicationSettings.Language == "JP" ? "値を入力してください" : "Please enter a value";
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK);
                return;
            }

            var selectedStrategy = strategies[cbbDeleteCondition.SelectedItem.ToString()];
            string value = tbxValue.Text;

            List<VoiceEntry> toRemove = new List<VoiceEntry>();
            foreach (var entry in _voiceEntries)
            {
                if (selectedStrategy.IsMatch(entry, value))
                {
                    toRemove.Add(entry);
                }
            }
            int deleteCount = toRemove.Count;

            string message = Form1.ApplicationSettings.Language == "JP" ? $"{deleteCount}件の項目を削除してもよろしいですか？" : $"This will delete {deleteCount} items. Are you sure you want to do this?";
            var result = MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                foreach (var entry in toRemove)
                {
                    _voiceEntries.Remove(entry);
                }
                _form1.LoadFirstPage();

                string successMessage = Form1.ApplicationSettings.Language == "JP" ? $"{deleteCount}件の文字起こしが削除されました。\n\nデータセットを保存するまで、削除された文字起こしは実際には削除されません。" : $"{deleteCount} transcriptions were deleted. \n\nNote: The deleted transcriptions are not actually removed from the list file until you save the dataset.";
                MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            _form1.unsavedChanges = true;
            _form1.updateLoadedCountLabels();

            Close();
        }


        void Localise()
        {
            if (Form1.ApplicationSettings.Language == "JP")
            {
                Text = "文字起こしを削除";

                lblDeleteTranscriptionsWhere.Text = "削除条件：";
                btnDelete.Text = "削除";

                strategies = new Dictionary<string, IConditionStrategy>
                {
                    { "音声の長さ <", new AudioLengthLessThanStrategy() },
                    { "音声の長さ >", new AudioLengthGreaterThanStrategy() },
                    { "テキストを含む", new ContainsTextStrategy() },
                };

                cbbDeleteCondition.Items.Clear();
                cbbDeleteCondition.Items.AddRange(strategies.Keys.ToArray());
            }
            else
            {
                Text = "Filter delete transcriptions";

                lblDeleteTranscriptionsWhere.Text = "Delete transcriptions where:";
                btnDelete.Text = "Delete";

                strategies = new Dictionary<string, IConditionStrategy>
                {
                    { "audio length <", new AudioLengthLessThanStrategy() },
                    { "audio length >", new AudioLengthGreaterThanStrategy() },
                    { "contains text", new ContainsTextStrategy() },
                };

                cbbDeleteCondition.Items.Clear();
                cbbDeleteCondition.Items.AddRange(strategies.Keys.ToArray());
            }
        }
    }

    public interface IConditionStrategy
    {
        bool IsMatch(VoiceEntry entry, string value);
    }

    public class AudioLengthLessThanStrategy : IConditionStrategy
    {
        public bool IsMatch(VoiceEntry entry, string value)
        {
            if (decimal.TryParse(value, out decimal length))
            {
                return entry.length < length;
            }
            return false;
        }
    }

    public class AudioLengthGreaterThanStrategy : IConditionStrategy
    {
        public bool IsMatch(VoiceEntry entry, string value)
        {
            if (decimal.TryParse(value, out decimal length))
            {
                return entry.length > length;
            }
            return false;
        }
    }

    public class ContainsTextStrategy : IConditionStrategy
    {
        public bool IsMatch(VoiceEntry entry, string value)
        {
            return entry.transcription.Contains(value);
        }
    }
}

