using System;

namespace Notch_Peak_Filter
{
    class Notch_Peak_Filter_Implementation : Notch_Peak_Filter_Interface
    {

		double[] num_filt;   //Vector where to temporarily save the numerator
		double[] den_filt;   // Vector where to temporarily save the denumerator
		double[][] save_filt_coeff;  //Matrix where to save the numerator and denominator. First row is the numerator; second row is the denominator

		//Notch Filter
		public double[][] IIRnotch_cpp(double WO, double BW)
		{

			if (!(save_filt_coeff == null))
			{

				//Reset the matrix to save the numerator
				Array.Clear(num_filt, 0, num_filt.Length);
				//Reset the matrix to save the denumerator
				Array.Clear(den_filt, 0, den_filt.Length);
				//Reset the matrix to save the coefficients
				Array.Clear(save_filt_coeff, 0, save_filt_coeff.Length);

			}

			int numb_coeff = 3;

			//Default value
			double AB = Math.Abs(10 * Math.Log10(0.5));   //it corresponds to abs(-3dB)

			//Normalize the input by PI
			WO = WO * Math.PI;
			BW = BW * Math.PI;

			double GB = Math.Pow(10, (-AB / 20));
			double BETA = (Math.Sqrt(1 - Math.Pow(GB, 2)) / GB) * Math.Tan(BW / 2);
			double GAIN = 1 / (1 + BETA);

			num_filt = new double[numb_coeff];

			//Calculate the numerator
			for (int kk = 0; kk < numb_coeff; kk++)
			{

				if (kk != 1)
				{

					num_filt[kk] = GAIN;

				}

				else
				{

					num_filt[kk] = -GAIN * 2 * Math.Cos(WO);

				}

			}

			den_filt = new double[numb_coeff];
			//Calculate the denominator
			for (int kk = 0; kk < numb_coeff; kk++)
			{

				switch (kk)
				{

					case 0:

						den_filt[kk] = 1;
						break;

					case 1:

						den_filt[kk] = -2 * GAIN * Math.Cos(WO);
						break;

					case 2:

						den_filt[kk] = 2 * GAIN - 1;
						break;

				}

			}


			save_filt_coeff = new double[2][];

			for (int kk = 0; kk < 2; kk++)
			{

				save_filt_coeff[kk] = new double[numb_coeff];

			}


			//Save the coefficients in the output matrix
			for (int kk = 0; kk < 2; kk++)
			{

				for (int ll = 0; ll < numb_coeff; ll++)
				{

					if (kk == 0)
					{

						save_filt_coeff[kk][ll] = num_filt[ll];

					}

					else
					{

						save_filt_coeff[kk][ll] = den_filt[ll];

					}

				}

			}

			return save_filt_coeff;

		}

		//Comb filter
		public double[][] IIRcomb_cpp(double order, double BW, String type_filt)
		{

			if (!(save_filt_coeff == null))
			{

				//Reset the matrix to save the numerator
				Array.Clear(num_filt, 0, num_filt.Length);
				//Reset the matrix to save the denumerator
				Array.Clear(den_filt, 0, den_filt.Length);

				//Reset the matrix to save the coefficients
				Array.Clear(save_filt_coeff, 0, save_filt_coeff.Length);

			}

			//If the order is not an integer value, then return an empty matrix of coefficients
			if (order != Math.Round(order))
			{

				return save_filt_coeff;

			}

			//Default value
			double AB = Math.Abs(10 * Math.Log10(0.5));   //it corresponds to abs(-3dB)

			//Normalize the input by PI
			BW = BW * Math.PI;

			double GB = Math.Pow(10, (-AB / 20));

			num_filt = new double[(int)order + 1];
			den_filt = new double[(int)order + 1];

			if (type_filt.CompareTo("notch") == 0)
			{

				double BETA = (Math.Sqrt(1 - Math.Pow(GB, 2)) / GB) * Math.Tan(order * BW / 4);
				double GAIN = 1 / (1 + BETA);

				//Calculate the numerator
				for (int kk = 0; kk < order + 1; kk++)
				{

					if (kk == 0)
					{

						num_filt[kk] = GAIN;

					}

					else if (kk == order)
					{

						num_filt[kk] = -GAIN;

					}

					else
					{

						num_filt[kk] = 0;

					}

				}

				//Calculate the denominator
				for (int kk = 0; kk < order + 1; kk++)
				{

					if (kk == 0)
					{

						den_filt[kk] = 1;

					}

					else if (kk == order)
					{

						den_filt[kk] = -(2 * GAIN - 1);

					}

					else
					{

						den_filt[kk] = 0;

					}

				}

			}

			else if (type_filt.CompareTo("peak") == 0)
			{

				double BETA = (GB / Math.Sqrt(1 - Math.Pow(GB, 2))) * Math.Tan(order * BW / 4);
				double GAIN = 1 / (1 + BETA);

				//Calculate the numerator
				for (int kk = 0; kk < order + 1; kk++)
				{

					if (kk == 0)
					{

						num_filt[kk] = 1 - GAIN;

					}

					else if (kk == order)
					{

						num_filt[kk] = -(1 - GAIN);

					}

					else
					{

						num_filt[kk] = 0;

					}

				}

				//Calculate the denominator
				for (int kk = 0; kk < order + 1; kk++)
				{

					if (kk == 0)
					{

						den_filt[kk] = 1;

					}

					else if (kk == order)
					{

						den_filt[kk] = 2 * GAIN - 1;

					}

					else
					{

						den_filt[kk] = 0;

					}

				}


			}

			else
			{

				//Return an empty matrix, if the user did not entered either "notch" or "peak"
				return save_filt_coeff;

			}


			save_filt_coeff = new double[2][];

			for (int kk = 0; kk < 2; kk++)
			{

				save_filt_coeff[kk] = new double[(int)order + 1];

			}


			//Save the coefficients in the output matrix
			for (int kk = 0; kk < 2; kk++)
			{

				for (int ll = 0; ll < order + 1; ll++)
				{

					if (kk == 0)
					{

						save_filt_coeff[kk][ll] = num_filt[ll];

					}

					else
					{

						save_filt_coeff[kk][ll] = den_filt[ll];

					}

				}

			}

			return save_filt_coeff;

		}

		//Filter the data by using the Direct-Form II Transpose, as explained in the Matlab documentation
		public double[] Filter_Data_NCP(double[][] coeff_filt, double[] pre_filt_signal)
		{

			double[] filt_signal = new double[pre_filt_signal.Length];
			Array.Clear(filt_signal, 0, filt_signal.Length);

			double[][] w_val = new double[coeff_filt[0].Length][];


			for (int ff = 0; ff < coeff_filt[0].Length; ff++)
			{

				w_val[ff] = new double[pre_filt_signal.Length];

			}


			//Convolution product to filter the data
			for (int kk = 0; kk < pre_filt_signal.Length; kk++)
			{

				if (kk == 0)
				{

					filt_signal[kk] = pre_filt_signal[kk] * coeff_filt[0][0];


					for (int ww = 1; ww < coeff_filt[0].Length; ww++)
					{

						w_val[ww - 1][kk] = pre_filt_signal[kk] * coeff_filt[0][ww] - filt_signal[kk] * coeff_filt[1][ww];


					}

				}

				else
				{

					filt_signal[kk] = pre_filt_signal[kk] * coeff_filt[0][0] + w_val[0][kk - 1];

					for (int ww = 1; ww < coeff_filt[0].Length; ww++)
					{

						w_val[ww - 1][kk] = pre_filt_signal[kk] * coeff_filt[0][ww] + w_val[ww][kk - 1] - filt_signal[kk] * coeff_filt[1][ww];

						if (ww == coeff_filt[0].Length - 1)
						{

							w_val[ww - 1][kk] = pre_filt_signal[kk] * coeff_filt[0][ww] - filt_signal[kk] * coeff_filt[1][ww];

						}

					}

				}



			}

			return filt_signal;

		}

	}
}
