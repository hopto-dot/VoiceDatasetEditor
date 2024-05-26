using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace VoiceDatasetEditor.Classes
{
    public class VolumeSampleProvider : ISampleProvider
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
}
