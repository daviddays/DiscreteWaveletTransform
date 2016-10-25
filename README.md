# Fast wavelet transform (DWT)
> The Fast Wavelet Transform is a mathematical algorithm designed to turn a waveform or signal in the time domain into a sequence of coefficients based on an orthogonal basis of small finite waves, or wavelets. The transform can be easily extended to multidimensional signals, such as images, where the time domain is replaced with the space domain. 

Further details: https://en.wikipedia.org/wiki/Fast_wavelet_transform

All coefficients taken from: http://wavelets.pybytes.com/ 

## Sample usage:
```cs
// input vector
double[] input = new double[8] { 1, 1, 1, 1, 8, 1, 1, 1 };
// choose wavelet
var wavelet = WaveletFactory.Create(WaveletType.Daubechies_2);
// make forward transformation - two passes
var transformed = DwtTransform.Forward(input, wavelet, 2);
// make inverse transformation
var inverse = DwtTransform.Inverse(transformed);
// clear details for compresion/smoothing
transformed.ZeroDetailCoefficients();
// get compressed time series
var compressed = transformed.ApproximationCoefficients;
// restore time series 
var restored = DwtTransform.Inverse(transformed);
```
## Warning
Code `does not` check entry conditions for level and for input vector length. No exception would be thrown in case improper input.

## Implemented wavelets:
- Daubechies 1 (Haar),
- Daubechies 2,
- Daubechies 3,
- Daubechies 4,
- Daubechies 5,
- Daubechies 6,
- Daubechies 7,
- Daubechies 8,
- Daubechies 9,
- Daubechies 10,
- Daubechies 11,
- Daubechies 12,
- Daubechies 13,
- Daubechies 14,
- Daubechies 15,
- Daubechies 16,
- Daubechies 17,
- Daubechies 18,
- Daubechies 19,
- Daubechies 20,
- Symlet 2,
- Symlet 3,
- Symlet 4,
- Symlet 5,
- Symlet 6,
- Symlet 7,
- Symlet 8,
- Symlet 9,
- Symlet 10,
- Coiflet 1,
- Coiflet 2,
- Coiflet 3,
- Coiflet 4,
- Coiflet 5.