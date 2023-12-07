Code to calculate the coefficients of a notch, comb or peak filter

C# code to calculate the coefficients of a notch, comb or peak filter. The name space is: Notch_Peak_Filter.

Each filter function will return a 2 rows x N coefficients 2D vector, where Row 1 = Numerator and Row 2 = Denumerator.

Notch Filter: the function is "double[][] IIRcomb_cpp(double order, double BW)". The first argument is the frequency to be notched normalized with respect the Nyquist frequency (0.0 < wo < 1), while the second one is the bandwidth (wo / N) of the filter. The default dB level of the bandwidth is ~3.0103 dB. This function has the overload "double[][] IIRcomb_cpp(double order, double BW, double AB)" allowing the user to choose the dB level of the bandwidth;

Comb/Peak Filter: the function is "double[][] iircomb_cpp(double order, double BW, String type_filt)". The first two argument is the ratio (or order) between the frequency to be notched (F0) and the sampling frequency (SF)(F0/SF), the second one is the band width of the filter expressed as: (F0/(SF/2))/Q and the third one is a string that can be either "notch" (for the comb filter) or "peak" (for the peak filter). Please, remember that the ratio (order) needs to be an integer number. If the user inserts a ratio (order) that is not an integer, the code will return an empty matrix of coefficients. Also, if the user insert a string different from "notch" or "peak", the code will return an empty matrix of coefficients. The default dB level of the bandwidth is ~3.0103 dB. This function has the overload "double[][] iircomb_cpp(double order, double BW, double AB, String type_filt)" allowing the user to choose the dB level of the bandwidth;

Filter the data: the method is "double[] Filter_Data_NCP(double[][] coeff_filt, double[] pre_filt_signal)". The two arguments are the filter coefficients and the signal to be filtered. It returns the filtered signal.

If you have any questions and/or want to report bugs, please e-mail me (Ale) at: pressalex@hotmail.com
