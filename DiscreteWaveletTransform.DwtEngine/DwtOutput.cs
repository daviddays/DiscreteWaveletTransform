using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteWaveletTransform.DwtEngine
{
    public class DwtOutput
    {
        public double[] ApproximationCoefficients { get; private set; }
        public double[] DetailCoefficients { get; private set; }
        public double[] FullVector { get; private set; }

        public int Level { get; private set; }
        public Wavelet Wavelet { get; private set; }

        public DwtOutput(double[] output, int level, Wavelet wavelet)
        {
            Level = level;
            Wavelet = wavelet;

            ApproximationCoefficients = new double[output.Length / (level + 1)];
            Array.Copy(output, ApproximationCoefficients, ApproximationCoefficients.Length);
            DetailCoefficients = new double[output.Length - ApproximationCoefficients.Length];
            Array.Copy(output, ApproximationCoefficients.Length, DetailCoefficients, 0, output.Length - ApproximationCoefficients.Length);
            FullVector = output;
        }

        public void ZeroDetailCoefficients()
        {
            Array.Clear(DetailCoefficients, 0, DetailCoefficients.Length);
            Array.Clear(FullVector, ApproximationCoefficients.Length, DetailCoefficients.Length);
        }
    }
}
