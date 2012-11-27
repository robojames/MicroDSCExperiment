using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;

namespace ThermalControl
{
    public partial class ArduinoCP : Form
    {
        SerialPort arduino = new SerialPort("COM4", 9600);

        public ArduinoCP()
        {
            InitializeComponent();
        }

        public void Go()
        {
            this.chart1.Series.Clear();
            this.chart1.Series.Add("Humidity");
            this.chart1.Series[0].Label = "Humidity (%)";
            
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            this.chart1.Show();
            this.chart1.Update();


            this.arduino.Open();

            this.arduino.DtrEnable = true;

            int p = 0;

            while (p == 0)
            {
                Application.DoEvents();
                this.arduino.DataReceived += new SerialDataReceivedEventHandler(arduino_DataReceived);
            }
        }

        public delegate void SetTextCallback(double data);

        void arduino_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (arduino.IsOpen)
            {
                double data = double.Parse(arduino.ReadLine());

                plotData(data);
            }
        }

        public void plotData(double data)
        {
            if (arduino.IsOpen)
            {
                if (this.chart1.InvokeRequired == true)
                {
                    SetTextCallback d = new SetTextCallback(plotData);
                    this.Invoke(d, new object[] { data });

                }
                else
                {
                    if (data < 100)
                    {
                        this.chart1.Series[0].Points.Add(data);
                        this.textBox1.Text = Math.Round(data, 2).ToString();
                        this.chart1.Update();
                    }
                    else
                    {
                        // Do nothing
                    }

                }

            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.arduino.Close();
            this.arduino.Dispose();

            this.Close();
            
        }

        private void ArduinoCP_Load(object sender, EventArgs e)
        {

        }
    }
}
