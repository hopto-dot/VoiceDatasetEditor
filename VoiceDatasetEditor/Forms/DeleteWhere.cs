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
            cbbDeleteCondition.SelectedIndex = 0;
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
            var selectedStrategy = strategies[cbbDeleteCondition.SelectedItem.ToString()];
            string value = tbxValue.Text;

            List<VoiceEntry> toRemove = new List<VoiceEntry>();
            foreach (var entry in _voiceEntries)
            {
                if (selectedStrategy.IsMatch(entry, value))
                {
                    //entry.transcription = "";
                    toRemove.Add(entry);
                }
            }

            foreach (var entry in toRemove)
            {
                _voiceEntries.Remove(entry);
            }

            _form1.LoadFirstPage();
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

