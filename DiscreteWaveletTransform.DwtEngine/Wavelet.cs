using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteWaveletTransform.DwtEngine
{
    public class Wavelet
    {
        internal double[] DecompositionLow;
        internal double[] DecompositionHigh;

        internal double[] ReconstructionLow;
        internal double[] ReconstructionHigh;

        public WaveletType WaveletType;

        public Int32 FilterLength
        {
            get { return DecompositionLow.Length; }
        }

        public Wavelet(WaveletType waveletType, double[] decLowPass)
        {
            WaveletType = waveletType;
            DecompositionLow = decLowPass;
            DecompositionHigh = new double[decLowPass.Length];
            ReconstructionLow = new double[decLowPass.Length];
            ReconstructionHigh = new double[decLowPass.Length];

            for (int i = 0; i < decLowPass.Length; i++)
            {
                DecompositionHigh[i] = decLowPass[decLowPass.Length - i - 1];
                if (i % 2 != 0) DecompositionHigh[i] *= -1;
                ReconstructionLow[i] = decLowPass[decLowPass.Length - i - 1];
            }
            for (int i = 0; i < decLowPass.Length; i++) ReconstructionHigh[i] = DecompositionHigh[decLowPass.Length - i - 1];
        }
    }

}
