using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ThermalControl
{
    class CSVReader
    {
        private double[,] Data;

        private int N_Columns = 4;

        private List<double> AmpArray = new List<double>();
        private List<double> TimeArray = new List<double>();
        private List<double> TemperatureArray = new List<double>();
        private List<double> RelayStateArray = new List<double>();

        public double[] DAmpArray { get; private set; }
        public double[] DTimeArray { get; private set; }
        public double[] DTemperatureArray { get; private set; }
        public double[] DRelayStateArray { get; private set; }
        
        public double[,] Read(string File_Title)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "CSV|*.csv";
            openFile.Title = File_Title;
            openFile.DefaultExt = "*.csv";

            openFile.ShowDialog();

            string directory = openFile.FileName;

            List<string> CurrentImporte = new List<string>();

            string line;

            TextReader dataRead = new StreamReader(directory);

            // Pulls each line into datastream as object
            while ((line = dataRead.ReadLine()) != null)
                CurrentImporte.Add(line);

            int j = 0;

            foreach (string dataEntry in CurrentImporte)
            {
                string[] placeHolder = dataEntry.Split(',');

                if (j > 0) // Eliminates Header Row to just pull in numerical Data
                {
                    AmpArray.Add(double.Parse(placeHolder[0]));

                    TimeArray.Add(double.Parse(placeHolder[1]));

                    TemperatureArray.Add(double.Parse(placeHolder[2]));

                    RelayStateArray.Add(double.Parse(placeHolder[3]));
                }

                j++;
            }

            DAmpArray = AmpArray.ToArray();
            DTimeArray = TimeArray.ToArray();
            DTemperatureArray = TemperatureArray.ToArray();
            DRelayStateArray = RelayStateArray.ToArray();

            Data = new double[DAmpArray.Count(), N_Columns];
            
            for (int i = 0; i < AmpArray.Count; i++)
            {
                for (int k = 0; k < N_Columns; k++)
                {
                    if (k == 0)
                        Data[i, k] = DAmpArray[i];
                    if (k == 1)
                        Data[i, k] = DTimeArray[i];
                    if (k == 2)
                        Data[i, k] = DTemperatureArray[i]; 
                    if (k == 3)
                        Data[i, k] = DRelayStateArray[i];
                }
            }

            return Data;
        }
    }
}
