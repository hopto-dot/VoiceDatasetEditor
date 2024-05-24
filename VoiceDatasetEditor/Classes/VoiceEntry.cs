using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using VoiceDatasetEditor.Forms;

namespace VoiceDatasetEditor.Classes
{
    public class VoiceEntry
    {
        public string filepath = "";
        public string speakerName = "";
        public string language = "JP";
        public string transcription = "";
        
        public decimal length = 0;
    }

}
