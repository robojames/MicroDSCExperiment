using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace ThermalControl
{
    public partial class plotDisplay : Form
    {
        double tempDiff;

        public plotDisplay()
        {
            InitializeComponent();
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveChart_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();

                saveFile.Filter = "Png|*.png";

                saveFile.Title = "Save the Chart Image";

                saveFile.DefaultExt = "*.png";

                saveFile.ShowDialog();

                string imagePath = saveFile.FileName;

                tempChart.SaveImage(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch
            {
                MessageBox.Show("Error saving file, please try again--Make sure directory is valid", "Save Error!", MessageBoxButtons.OK);
            }
            
        }

        public void updateChart(double currentProgress)
        {
            
            this.Text = "Plot Display -- Current Progress:  " + currentProgress.ToString() + "%";
            this.Update();
            
        }

        public void plotData(double time, double temp, string seriesName)
        {
            
                tempChart.Series[seriesName].Points.AddXY(time, temp);
                if ((-1.5 * time + 15) <= -45)
                {
                    tempChart.Series["Comparison"].Points.AddXY(time, -45);

                }
                else
                {
                    tempChart.Series["Comparison"].Points.AddXY(time, -1.5 * time + 15);
                }
                Application.DoEvents();
                this.Update();
            

        }

        public void InitializePlotDisplay(List<string> stringArray)
        {
            stringArray.Add("Comparison");
            foreach (string stringName in stringArray)
            {
                tempChart.Series.Add(stringName);
                tempChart.Series[stringName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
                
            }

        }

        private void plotDisplay_Load(object sender, EventArgs e)
        {
            tempChart.Series.Clear();
        }

        public void updateValues(double time, double temp, double dtdt, double amps)
        {
            try
            {
                txtdtdt.Text = String.Format("{0:0.000}", dtdt);
                txtTemp.Text = String.Format("{0:0.000}", temp);
                txtTime.Text = String.Format("{0:0.000}", time);
                txtAmps.Text = String.Format("{0:0.000}", amps);

                tempDiff = double.Parse(txtTemp.Text) - double.Parse(txtdtdt.Text);

                txtTempDiff.Text = string.Format("{0:0.000}", tempDiff);

                txtTempDiff.Update();
                txtAmps.Update();
                txtTime.Update();
                txtTemp.Update();
                txtdtdt.Update();
            }
            catch
            {
                MessageBox.Show("Error Adding data point, Terminating.");
                this.Close();
            }

        }



        public void CoolingorWarming(bool cool)
        {
            if (cool == true)
            {
                label4.Text = "Amps:  Cooling";
            }

            if (cool == false)
            {
                label4.Text = "Amps:  Warming";
            }
        }

        private void tempChart_Click(object sender, EventArgs e)
        {

        }


    }
}
