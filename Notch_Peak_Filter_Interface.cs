using System;

namespace Notch_Peak_Filter
{
    interface Notch_Peak_Filter_Interface
    {

        //Estimate the coeffients of a band-pass filter and return a 2 rows x N coefficients matrix. Row 1 = Numerator; Row 2 = Denumerator
        double[][] IIRnotch_cs(double WO, double BW); //Notch filter
        double[][] IIRnotch_cs(double WO, double BW, double AB); //Overload of the Notch filter allowing the user to specify the dB level of the bandwidth

        double[][] IIRcomb_cs(double order, double BW, String type_filt); //Comb or peak filter
        double[][] IIRcomb_cs(double order, double BW, double AB, String type_filt); //Overload of the Comb or peak filter allowing the user to specify the dB level of the bandwidth

        //Filter the data by using the Direct-Form II Transpose, as explained in the Matlab documentation
        double[] Filter_Data_NCP(double[][] coeff_filt, double[] pre_filt_signal);

    }
}
