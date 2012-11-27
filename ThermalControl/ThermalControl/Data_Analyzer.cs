using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;


namespace ThermalControl
{
    class Data_Analyzer
    {
        public double[] Temp_Baseline { get; private set; }
        public double[] Temp_Sample { get; private set; }
        public double[] dTdt_Sample { get; private set; }
        public double[] Temp_DT { get; private set; }
        public double[] Time { get; private set; }

        public Data_Analyzer(double[,] Baseline_File, double[,] Sample_File)
        {
            Temp_DT = new double[Baseline_File.GetLength(1)];
            Time = new double[Baseline_File.GetLength(1)];
            dTdt_Sample = new double[Baseline_File.GetLength(1)];

            // Need to extract Temperature vs. Time plots
            // Data[i,1] == Time
            // Data[i,2] == Temperature

            for (int i = 0; i < Baseline_File.GetLength(1); i++)
            {
                Temp_Baseline[i] = Baseline_File[i, 2];

                Temp_Sample[i] = Sample_File[i, 2];

                Temp_DT[i] = Temp_Sample[i] - Temp_Baseline[i];

                Time[i] = Baseline_File[i, 1];
            }

            // Also need derivative information from the sample run
            dTdt_Sample = Calculate_Derivative(Temp_Sample);
            
        }

        public double[] Calculate_Derivative(double[] Temperature)
        {
            // Assumes uniform TimeStep
            double[] dTdt = new double[Temperature.Length];
            double timeStep = 0.003;

            dTdt[0] = 0;
            dTdt[1] = 0;

            for (int i = 2; i < (Temperature.Length) - 2; i++)
            {
                dTdt[i] = (8 * (Temperature[i + 1] - Temperature[i - 1]) + (Temperature[i - 2] - Temperature[i + 2])) / (12 * timeStep);
            }

            return dTdt;
        }

    }
}
