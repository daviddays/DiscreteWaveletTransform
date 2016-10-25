using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteWaveletTransform.DwtEngine
{
    public static class DwtTransform
    {
        public static unsafe DwtOutput Forward(double[] input, Wavelet wavelet, Int32 level)
        {
            Int32 Len = wavelet.DecompositionLow.Length;
            Int32 CircleInd;
            double[] output = new Double[input.Length];
            Double[] Buff = new Double[input.Length];
            Buffer.BlockCopy(input, 0, output, 0, input.Length * 8);
            Double BufScal = 0;
            Double BufDet = 0;
            Double* DecLow = stackalloc Double[Len];
            Double* DecHigh = stackalloc Double[Len];
            for (int i = 0; i < Len; i++)
            {
                DecLow[i] = wavelet.DecompositionLow[i];
                DecHigh[i] = wavelet.DecompositionHigh[i];
            }

            fixed (Double* pout = output, pbuf = Buff)
            {

                for (int lvl = 0; lvl < level; lvl++)
                {
                    Int32 Bound = input.Length >> lvl;
                    Int32 StartIndex = -((Len >> 1) - 1);
                    Buffer.BlockCopy(output, 0, Buff, 0, Bound * 8);

                    for (int i = 0; i < Bound >> 1; i++)
                    {
                        for (int j = StartIndex, k = 0; k < Len; j++, k++)
                        {
                            if ((StartIndex < 0) || j >= Bound) CircleInd = ((j % Bound) + Bound) % Bound;
                            else CircleInd = j;
                            BufScal += DecLow[k] * pout[CircleInd];
                            BufDet += DecHigh[k] * pout[CircleInd];
                        }
                        StartIndex += 2;
                        pbuf[i] = BufScal;
                        pbuf[i + (Bound >> 1)] = BufDet;
                        BufScal = 0;
                        BufDet = 0;
                    }
                    Buffer.BlockCopy(Buff, 0, output, 0, Bound * 8);
                }
            }

            DwtOutput res = new DwtOutput(output, level, wavelet);

            return res;
        }

        public static unsafe double[] Inverse(DwtOutput dwtInput)
        {
            var wavelet = dwtInput.Wavelet;
            var input = dwtInput.FullVector;
            var level = dwtInput.Level;
            Int32 Len = wavelet.ReconstructionLow.Length;
            Int32 CircleInd;
            double[] output = new Double[input.Length];
            Double[] Buff = new Double[input.Length];
            Double[] BufferLow = new Double[input.Length];
            Double[] BufferHigh = new Double[input.Length];
            Buffer.BlockCopy(input, 0, output, 0, input.Length * 8);
            Double Buf = 0;
            Double* RecLow = stackalloc Double[Len];
            Double* RecHigh = stackalloc Double[Len];
            for (int i = 0; i < Len; i++)
            {
                RecLow[i] = wavelet.ReconstructionLow[i];
                RecHigh[i] = wavelet.ReconstructionHigh[i];
            }

            fixed (Double* pbuf = Buff, pLow = BufferLow, pHigh = BufferHigh)
            {

                for (int lvl = level; lvl > 0; lvl--)
                {
                    Int32 Bound = input.Length >> lvl;
                    Int32 StartIndex = -((Len >> 1) - 1);
                    for (int i = 0, j = 0; i < Bound << 1; i += 2, j++)
                    {
                        pLow[i] = 0;
                        pHigh[i] = 0;
                        pLow[i + 1] = output[j];
                        pHigh[i + 1] = output[Bound + j];
                    }

                    for (int i = 0; i < Bound << 1; i++)
                    {
                        for (int j = StartIndex, k = 0; k < Len; j++, k++)
                        {
                            if ((StartIndex < 0) || j >= (Bound << 1)) CircleInd = (j % (Bound << 1) + (Bound << 1)) % (Bound << 1);
                            else CircleInd = j;
                            Buf += RecLow[k] * pLow[CircleInd] + RecHigh[k] * pHigh[CircleInd];
                        }
                        StartIndex += 1;
                        pbuf[i] = Buf;
                        Buf = 0;
                    }
                    Buffer.BlockCopy(Buff, 0, output, 0, Bound * 16);
                }
            }
            return output;
        }
    }
}
