using System;
using System.IO;

namespace Notch_Peak_Filter
{
    class Notch_Peak_Filter_Main
    {
        static void Main(string[] args)
        {

            Notch_Peak_Filter_Interface NOF = new Notch_Peak_Filter.Notch_Peak_Filter_Implementation();

            int coeff_numb = 3;

            double WO = 60.0 / (300.0 / 2);
            double BW = WO / 35;
            double AB = 20;

            double q = 35;
            double f0 = 60;
            double fs = 300;
            double BW_Comb = (f0 / (fs / 2)) / q;

            double[][] coeff_final = new double[2][];
            
            int choice_filter = 6;

            for (int kk = 0; kk < 2; kk++)
            { }

            //Directory where to save the pre-filt and post-filt data
            string dir = @"C:\Users\bioni\Desktop\C#\Notch_Peak_Filter";
            try
            {
                //Set the current directory.
                Directory.SetCurrentDirectory(dir);

            }

            catch (DirectoryNotFoundException e)
            {

                Console.WriteLine("The specified directory does not exist. {0}", e);

            }

            //Build the signal to be used to test the filter
            int length_test_signal = 300;
            double[] test_signal = new double[length_test_signal];
            double[] time = new double[length_test_signal];
            double freq_data_I = 20;
            double freq_data_II = 60;

            double[] output_filt_signal = new double[length_test_signal];

            for (int kk = 0; kk < length_test_signal; kk++)
            {

                time[kk] = (double)kk / fs;
                test_signal[kk] = Math.Sin(2 * Math.PI * time[kk] * freq_data_I) + Math.Sin(2 * Math.PI * time[kk] * freq_data_II);

            }

            using (StreamWriter sw = new StreamWriter("Pre_Filtered_data.txt"))
            {

                for (int hh = 0; hh < length_test_signal; hh++)
                {

                    sw.WriteLine(Convert.ToString(test_signal[hh]));

                }

            }

            using (StreamWriter sw = new StreamWriter("Time_domain.txt"))
            {

                for (int hh = 0; hh < length_test_signal; hh++)
                {

                    sw.WriteLine(Convert.ToString(time[hh]));

                }

            }

            switch (choice_filter)
            {

                case 1:
                    coeff_final = NOF.IIRnotch_cs(WO, BW);


                    for (int kk = 0; kk < 2; kk++)
                    {

                        if (kk == 0)
                        {

                            Console.Write("Numerator notch: ");

                        }

                        else
                        {

                            Console.Write("Denumerator notch: ");

                        }

                        for (int ll = 0; ll < coeff_numb; ll++)

                        {

                            Console.Write(coeff_final[kk][ll] + "\t");

                        }

                        Console.WriteLine("");

                    }
                    break;

                case 2:
                    coeff_final = NOF.IIRnotch_cs(WO, BW, AB);


                    for (int kk = 0; kk < 2; kk++)
                    {

                        if (kk == 0)
                        {

                            Console.Write("Numerator notch(dB): ");

                        }

                        else
                        {

                            Console.Write("Denumerator notch(dB): ");

                        }

                        for (int ll = 0; ll < coeff_numb; ll++)

                        {

                            Console.Write(coeff_final[kk][ll] + "\t");

                        }

                        Console.WriteLine("");

                    }
                    break;

                case 3:
                    coeff_final = NOF.IIRcomb_cs(fs / f0, BW_Comb, "notch");

                    for (int kk = 0; kk < 2; kk++)
                    {

                        if (kk == 0)
                        {

                            Console.Write("Numerator notch: ");

                        }

                        else
                        {

                            Console.Write("Denumerator notch: ");

                        }

                        for (int ll = 0; ll < (fs / f0) + 1; ll++)

                        {

                            Console.Write(coeff_final[kk][ll] + "\t");

                        }

                        Console.WriteLine("");

                    }
                    break;

                case 4:
                    coeff_final = NOF.IIRcomb_cs(fs / f0, BW_Comb, AB, "notch");

                    for (int kk = 0; kk < 2; kk++)
                    {

                        if (kk == 0)
                        {

                            Console.Write("Numerator notch(dB): ");

                        }

                        else
                        {

                            Console.Write("Denumerator notch(dB): ");

                        }

                        for (int ll = 0; ll < (fs / f0) + 1; ll++)

                        {

                            Console.Write(coeff_final[kk][ll] + "\t");

                        }

                        Console.WriteLine("");

                    }
                    break;

                case 5:
                    coeff_final = NOF.IIRcomb_cs(fs / f0, BW_Comb, "peak");

                    for (int kk = 0; kk < 2; kk++)
                    {

                        if (kk == 0)
                        {

                            Console.Write("Numerator peak: ");

                        }

                        else
                        {

                            Console.Write("Denumerator peak: ");

                        }

                        for (int ll = 0; ll < (fs / f0) + 1; ll++)

                        {

                            Console.Write(coeff_final[kk][ll] + "\t");

                        }

                        Console.WriteLine("");

                    }
                    break;

                case 6:
                    coeff_final = NOF.IIRcomb_cs(fs / f0, BW_Comb, AB, "peak");

                    for (int kk = 0; kk < 2; kk++)
                    {

                        if (kk == 0)
                        {

                            Console.Write("Numerator peak: ");

                        }

                        else
                        {

                            Console.Write("Denumerator peak: ");

                        }

                        for (int ll = 0; ll < (fs / f0) + 1; ll++)

                        {

                            Console.Write(coeff_final[kk][ll] + "\t");

                        }

                        Console.WriteLine("");

                    }
                    break;
            }

            output_filt_signal = NOF.Filter_Data_NCP(coeff_final, test_signal);

            using (StreamWriter sw = new StreamWriter("Filtered_data.txt"))
            {

                for (int hh = 0; hh < length_test_signal; hh++)
                {

                    sw.WriteLine(Convert.ToString(output_filt_signal[hh]));

                }

            }

        }
    }
}
