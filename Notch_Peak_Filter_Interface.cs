using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notch_Peak_Filter
{
    interface Notch_Peak_Filter_Interface
    {

        //Estimate the coeffients of a band-pass filter and return a 2 rows x N coefficients matrix. Row 1 = Numerator; Row 2 = Denumerator
        double[][] IIRnotch_cpp(double WO, double BW); //Notch filter

        double[][] IIRcomb_cpp(double order, double BW, String type_filt); //Comb or peak filter

        //Filter the data by using the Direct-Form II Transpose, as explained in the Matlab documentation
        double[] Filter_Data_NCP(double[][] coeff_filt, double[] pre_filt_signal);

    }
}
