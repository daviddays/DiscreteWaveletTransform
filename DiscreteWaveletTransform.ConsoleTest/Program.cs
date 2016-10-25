using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscreteWaveletTransform.DwtEngine;

namespace DiscreteWaveletTransform.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // input vector
            double[] input = new double[8] { 1, 1, 1, 1, 8, 1, 1, 1 };
            // chosen wavelet
            var wavelet = WaveletFactory.Create(WaveletType.Daubechies_2);
            // forward transformation - two passes
            var transformed = DwtTransform.Forward(input, wavelet, 2);
            // inverse transformation
            var inverse = DwtTransform.Inverse(transformed);
            // clear details for compresion/smothing
            transformed.ZeroDetailCoefficients();
            // get compressed time series
            var compressed = transformed.ApproximationCoefficients;
            // restore time series
            var restored = DwtTransform.Inverse(transformed);
            Console.ReadKey();

        }
    }
}
