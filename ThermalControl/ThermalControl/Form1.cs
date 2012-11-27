#region Using Statements
// General Using Statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ivi.Visa.Interop; // VISA communication through GPIB
using Keithley.Ke37XX.Interop; // Keithley 3706A specific .dll for communication with DMM
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO.Ports;

#endregion

namespace ThermalControl
{
    public partial class Main : Form
    {
        #region Global Variable Definition / Initialization

        double nplc = 1.0;//0.8;//0.11;

        double[] ampArray;
        
        int SampleNumberDMMHalf;

        // All toWrite variables hold their prescribed values throughout each routine, and at the end are utilized by newWriteFile() to write directly
        // to a .csv file
        double[] toWriteAmp = new double[600000];
        double[] toWriteTemp = new double[600000];
        double[] toWriteTemp2 = new double[600000];
        double[] toWriteTime = new double[600000];
        double[] toWriteRelay = new double[600000];
        double[] readRelayArray = new double[600000];
        double[] ampArrayD;

        int toWriteArraySize = 0;

        // These three bool flags are all related to the state of the relay in the (0) off position or (1) on position.  One is used by newWriteFile().
        bool relayFlag = false;
        bool warmFlag = false;
        bool powerFlag = false;

        // ExperimentReadyFlag 
        bool ExperimentReadyFlag = false;

        double setTemp;
        double dUpperCurrent;
        double dLowerCurrent = 0;
        double dtdtCoolRef;
        double constantA;
        int SampleNumber;
        // test truncation and distance from line methods...at leaset think about them
        double timeStep;    
        double ki;
        double kp;
        double kd;
        double const_timeStep;

        // icep - Ice point reference voltage for Type E thermocouple.  This is determined and set experimentally prior to each experiment.
        // c0-c9 & c01-c91 are the coefficients to the 9th order polynomial from the ITS-90 (inverted)
        double icep = -0.00118774;

        double c0 = 0;
        double c1 = 1.7057035 * Math.Pow(10, -2);
        double c2 = -2.3301759 * Math.Pow(10, -7);
        double c3 = 6.5435585 * Math.Pow(10, -12);
        double c4 = -7.3562749 * Math.Pow(10, -17);
        double c5 = -1.7896001 * Math.Pow(10, -21);
        double c6 = 8.4036165 * Math.Pow(10, -26);
        double c7 = -1.3735879 * Math.Pow(10, -30);
        double c8 = 1.0629823 * Math.Pow(10, -35);
        double c9 = -3.2447087 * Math.Pow(10, -41);

        double c01 = 0;
        double c11 = 1.6977288 * Math.Pow(10, -2);
        double c21 = -4.3514970 * Math.Pow(10, -7);
        double c31 = -1.5859697 * Math.Pow(10, -10);
        double c41 = -9.250287 * Math.Pow(10, -14);
        double c51 = -2.6084314 * Math.Pow(10, -17);
        double c61 = -4.1360199 * Math.Pow(10, -21);
        double c71 = -3.4034030 * Math.Pow(10, -25);
        double c81 = -1.1564890 * Math.Pow(10, -29);
        double c91 = 0;
        double[] timeStepArray;
        
        int[] timeStepArrayInt;

        string channelNumber;

        // A list of strings (each string is the name of a plot to be generated) 
        List<string> ListPlot = new List<string>();
        List<double> TimeArray = new List<double>();

        public void InitializeValues()
        {
            // This routine simply sets the default values used in the UI and sets the default states of the buttons. 

            txtmaxCurrent.Text = "5.5";
            txtTimeStep.Text = "0.005";

            ListPlot.Add("Temperature");
            ListPlot.Add("dTdt");

            //txtkP.Text = "0.48";
            //txtkI.Text = "0.103"; // 0.083
            //txtkd.Text = "0.25";

            txtkP.Text = "0.60"; // 0.6 perfect max
            txtkI.Text = "0.005"; // 0.0085
            txtkd.Text = "0.03";

            //aWarmSet = 1.9 - Math.Abs((0.2 * dTdterr[p] + 0.02 * dERR[p] + 0.004 * dTdtint[p])); <-- These values work somewhat... aset=aset

            txtChannelNumber.Text = "6006";
           

            txtExperimentConstantAA.Text = "0";
          
            txtExperimentDTref.Text = "1.0";
            txtExperimentSampleNumber.Text = "450";
            txtExperimentTset.Text = "15";
            
            radioButton2.Checked = true;

            sampleNoTextBox.Text = "15000";
 
            txtExperimentTset.Enabled = false;
            btnExperimentReady.Enabled = false;

            

            checkBox1.Checked = true;

            
        }


        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // When the main menu is first loaded, this causes all values to be initialized

            InitializeValues();
        }

        #endregion

        #region Experiments
        // **********************************************************************************************
        // ****************************** LINEAR COOLING PROFILE ****************************************
        // **********************************************************************************************
        public void LinearCooling(double[] ampArrayD)
        {
            // Generates an instance object of the custom-class plotDisplay() called tempChart;   
            plotDisplay tempChart = new plotDisplay();

            // This forces the plot window to show immediately
            if (checkBox3.Checked == true)
                tempChart.Show();

            Stopwatch pscom = new Stopwatch();

            // Generates an instance object of the custom-class Hardware, and names is LinearCoolingSetup
            Hardware LinearCoolingSetup = new Hardware();

            // Generates an instance object of type Ke37xx (Keithley .dll), naming it DMM for further commands
            Ke37XX DMM = LinearCoolingSetup.InitializeDMM(timeStep, channelNumber, nplc);

            // Instances two FormattedIO488 objects for both power supplies and sets their value equal to two different outputs
            // These outputs are determined by the initialize routines that are called in the Hardware class
            FormattedIO488 ps1 = LinearCoolingSetup.InitializeAgilent6032();

            FormattedIO488 ps2 = LinearCoolingSetup.InitializeAgilent6760();

            // ibufferSize is used to set the reading buffer within the Keithley (dependent upon how many measurements are taken)
            int ibufferSize = SampleNumber;

            // Sets the number of measurements taken per scan (per loop iteration)
            DMM.Scan.ScanCount = 1;

            // String containing name of buffer (used as a reference when sending commands to Keithley)
            string sbufferName = "Mybuffer";

            // Creates buffer named sbufferName with a size of ibufferSize
            DMM.Measurement.Buffer.Create(sbufferName, ibufferSize);

            // Sets IO timeout in ms
            DMM.System.DirectIO.IO.Timeout = 10000;

            // Used to hold value of experiment progress to user via PlotDisplay
            double currentProgress;

            // Holds readings from Keithley's Buffer
            double[] readings = new double[SampleNumber + 25000];

            // Value of derivative of temperature with respect to time
            double[] dTdt = new double[SampleNumber + 25000];

            // Value of time read from Keithley
            double[] time = new double[SampleNumber + 25000];

            // Value of temperature, passed back from getTemp() Function
            double[] temp = new double[SampleNumber + 25000];

            // Value of temperature for second thermocouple, passed back from getTemp() Function (mostly unused)
            double[] temp2 = new double[SampleNumber + 25000];

            // Holds value of time differential for numerical derivative calculation
            double[] timeDiff = new double[SampleNumber +  25000];

            // 
            double[] data = new double[SampleNumber +  25000];

            // Holds value of the error in what the derivative is vs. what it should be for a linear cooling profile
            double[] dERR = new double[SampleNumber +  25000];

            // Holds the value of the current set by the control system
            double[] ampArrayToWrite = new double[SampleNumber +  25000];

            // Holds the value in the error in the derivative (proportional)
            double[] dTdterr = new double[SampleNumber +  25000];

            // Holds the value of the error in the derivative (integral)
            double[] dTdtint = new double[SampleNumber +  25000];

            // Holds the value of the AVERAGE of the past 10 derivative errors to help smooth the numerical derivative error
            double[] dtdtTEST = new double[SampleNumber + 25000];

            // Holds the value of a constant current to be set
            double aSet;

            // Holds the value of the reference cooling rate
            double dTdtref = dtdtCoolRef;

            // Holds the value of the reference warming rate
            double aWarmSet;

            // Initializes the plot display (by setting the plot type) for each plot label [string] in ListPlot
            tempChart.InitializePlotDisplay(ListPlot);

            // Resets the internal timer in the DMM for reporting accurate time measurements
            

            // Clears the display on the Keithley and sets it to "Scanning..."
            DMM.Display.Clear();
            DMM.Display.Text = "Scanning...";

            // Three different counters utilized in measurement loop for derivative calculation
            int p = 0;
            int l = 0;
            int kk = 0;

            // Initially the relay is set to make the TE module warm the bridge (to eliminate water/ice formation)
                while (warmFlag == true)
                {
                    // Reads the time from the DMM
                    if (p == 0)
                        DMM.Timer.Reset();
                    time[p] = DMM.Timer.Measure();

                    // Executes the Scan, storing the measurement taken in the buffer named by sbufferName[string]
                    DMM.Scan.Execute(sbufferName);

                    

                    // Gets the readings from the measurement buffer on the DMM and assigns the value(s) to readings [double array]
                    readings = DMM.Measurement.Buffer.ReadingData.GetAllFormattedReadings(sbufferName);

                    // Converts the thermocouple voltage into a temperature, and stores it in temp[p]
                    temp[p] = getTemp(readings[0]);
                    
                    // Placeholder incase two thermocouples are utilized
                   // temp2[p] = getTemp(readings[1]);


                    if (p > 9)
                    {
                        kk = p - 2;

                        // Calculates the time difference used in the numerical derivative calculation
                        timeDiff[p] = time[p] - time[p - 1];

                        // Calculates the derivative
                        dTdt[p] = (8 * (temp[kk + 1] - temp[kk - 1]) + (temp[kk - 2] - temp[kk + 2])) / (12 * (timeDiff[p]));
                        
                        // Calculates the current error in the derivative
                        dTdterr[p] = dTdtref - dTdt[p];

                        // Calculates the derivative of the error in the derivative... yeah
                        dERR[p] = (8 * (dTdterr[p + 1] - dTdterr[p - 1]) + (dTdterr[p - 2] - dTdterr[p + 2])) / (12 * (timeDiff[p]));

                        // Integrates the error of the derivative
                        dTdtint[p] = dTdtint[p - 1] + dTdterr[p];
                    }
                    if (checkBox3.Checked == true)
                    {
                        // First checks to see if the 'temperature' plot box is checked, and if it is, it plots the data calculated prior
                        if (checkBoxTemp.Checked == true)
                            tempChart.plotData(time[p], temp[p], "Temperature");

                        // First checks to see if the 'dtdt' plot box is checked, and if it is, it plots the data calculated prior
                        if (checkBoxDtdt.Checked == true)
                            tempChart.plotData(time[p], temp2[p], "dTdt");
                    }
                    // Apply new current -- Might need to change this to subtract an amount of current based on it's current value;
                  //  Debug.WriteLine("Iter:  " + p.ToString() + "     Error in derivative:  " + dTdterr[p].ToString() + "        Derivative^2 of error:  " + dERR[p].ToString() + "      Integral Error:  " + dTdtint[p].ToString());
                    //Debug.WriteLine("Iter:  " + p.ToString() + "     Error in derivative[n]:  " + (0.08 * dTdterr[p]).ToString() + "        Derivative^2 of error[n]:  " + (0.02 * dERR[p]).ToString() + "      Integral Error[n]:  " + (0.0035 * dTdtint[p]).ToString());
                    
                    
                    aWarmSet = 1.9 - Math.Abs((0.2 * dTdterr[p] + 0.02 * dERR[p] + 0.004*dTdtint[p]));

                    // Sets safety range for current going to the TE module
                    if (aWarmSet >= 2.5)
                        aWarmSet = 2.5;

                    if (aWarmSet <= 0)
                        aWarmSet = 0;
                    
                    // Increments array size
                    toWriteArraySize++;

                    
                    // Sets the current on ps1 to aWarmSet
                 
                    ps1.IO.WriteString("ISET " + aWarmSet.ToString() + "\n");
          
                 
                    // if the temperature has went below -20 (Typically ambient), kick off heating and go to cooling
                    if (temp[p] <= -20)
                        warmFlag = false;
                    
                    // Updates numerical values located on the plot display
                    if (checkBox3.Checked == true)
                        tempChart.updateValues(time[p], temp[p], temp2[p], aWarmSet);
                   
                    // Writes values to their respective global double arrays
                    toWriteTemp[toWriteArraySize] = temp[p];
                    toWriteTemp2[toWriteArraySize] = temp2[p];
                    toWriteTime[toWriteArraySize] = time[p];
                    toWriteAmp[toWriteArraySize] = aWarmSet;
                    toWriteRelay[toWriteArraySize] = 0;

                    // Increments p 
                    p++;

                    // If calculated set current is equal to 0 then l is incremented 1.  Once l reaches 50 it breaks the warming loop
                    if (aWarmSet == 0)
                        l++;
                   
                    if (l > 45) // If the current has been set to 0 or less than 0 for 50 times then break the while/loop
                        warmFlag = false;

                }

            // relayFlag initialized as false (since it WAS in the warming state)
            relayFlag = false;

            // relayFlag then passed through switchRelay(and switched to TRUE), which switches to cooling mode
            relayFlag = switchRelay(ps2, relayFlag);

            // Linear Cooling loop
            for (int i = p; i <= SampleNumber - 1 + p; i++)
            {
                time[i] = DMM.Timer.Measure();

                DMM.Scan.Execute(sbufferName);

                readings = DMM.Measurement.Buffer.ReadingData.GetAllFormattedReadings(sbufferName);
                
                temp[i] = getTemp(readings[0]);

                //temp2[i] = getTemp(readings[1]);
                
                int j = i - 2;

                timeDiff[i] = time[i] - time[i - 1];

                dTdt[i] = (8 * (temp[j + 1] - temp[j - 1]) + (temp[j - 2] - temp[j + 2])) / (12 * (timeDiff[j]));

                dtdtTEST[i] = (dTdt[i] + dTdt[i - 1] + dTdt[i - 2] + dTdt[i - 3] + dTdt[i - 4] + dTdt[i - 5] + dTdt[i - 6] + dTdt[i - 7] + dTdt[i - 8]) / 9;

                dTdterr[i] = dTdtref + dtdtTEST[i];

                dERR[i] = (8 * (dTdterr[j + 1] - dTdterr[j - 1]) + (dTdterr[j - 2] - dTdterr[j + 2])) / (12 * (timeDiff[i]));

                dTdtint[i] = dTdtint[i - 1] + dTdterr[i];

               // MONITOR THIS 

               // if (time[i] < 50)
               // {

                // Checks to see if the integral error has gotten out of hand (accumulates large errors early on), and resets accordingly)
             //   if ((dTdtint[i] * ki) >= 12 | (dTdtint[i] * ki) <= -12)
             //   {
             //       dTdtint[i] = 0;
             //   }
                // Calcualtes new current based on passed through values of the derivative error, integral error, and the derivative of the error of the derivative
                aSet = ControlSysCR(dTdterr[i], dTdtint[i], dERR[i]);

                // Empirical relationship between Current and desired cooling rate [--------------------------------- NEEWWWW ----------------------------------]
                double i_aSet = dTdtref * (1.4682 / 2); // 1.4682
                int h = 0;
                if (i - p < 45) // 75 is a good value, well--decent
                {
                    aSet = i_aSet;
                }
                else
                {
                    //aSet = i_aSet + aSet * 1.4 + 0.01 * (i - p);
                    aSet = aSet;

                    //if (h == 0)
                    //{
                    //    dTdtint[i] = 0;
                    //    h++;
                    //}

                    
                }

                //if (temp[i] < -19)
                //{
                //    dTdterr[i] *= 11;
                    
                //}

                //int h = 0;

                //if (temp[i] < -19 && h == 0)
                //{
                //    kp *= 1.3;
                //    ki *= 1.25;

                //    kd *= 2.0;
                //    h++;
                //}
                

                //if (i < 200)
                //    dTdterr[i] *= 3;

                if (aSet >= 5.5)
                    aSet = 5.5;
                if (aSet <= 0)
                    aSet = 0;

                toWriteArraySize++;
               
                toWriteTemp[toWriteArraySize] = temp[i];
                toWriteTime[toWriteArraySize] = time[i];
                toWriteAmp[toWriteArraySize] = aSet;
                toWriteTemp2[toWriteArraySize] = temp2[i];
                toWriteRelay[toWriteArraySize] = 1;

                
                   // This checks to see if the current profile being used is imported or not, if so, it assigns the current based on the iteration #
                   // otherwise it assigns the calculated current
                if (chkboxImport.Checked == true && i <= ampArrayD.Length)
                {
                    ps1.IO.WriteString("ISET " + ampArrayD[i].ToString() + "\n");
                    toWriteAmp[i] = ampArrayD[i];
                }

                else
                {
                    ps1.IO.WriteString("ISET " + aSet.ToString() + "\n");
                }
                pscom.Start();
                if (checkBox3.Checked == true)
                {
                    if (checkBoxTemp.Checked == true)
                        tempChart.plotData(time[i], temp[i], "Temperature");


                    if (checkBoxDtdt.Checked == true)
                        tempChart.plotData(time[i], temp2[i], "dTdt");

                    // Calculates and posts the current progress of the experiment based on the current iteration
                    currentProgress = (i * (Math.Pow((double)SampleNumber - 1, -1))) * 100;//(double)((i/iscanCount) * 100);

                    currentProgress = (double)Math.Round((decimal)currentProgress, 2);

                    // Changes display to indicate whether or not TE module is warming or cooling to the user
                    tempChart.CoolingorWarming(relayFlag);

                    // Updates the calculated progress on the chart display
                    tempChart.updateChart(currentProgress);

                    // Updates numerical values listed on the chart display

                    tempChart.updateValues(time[i], temp[i], dTdt[i], aSet);
                }
            } // End of cooling loop

            // Start of Warming Profile
            int m = SampleNumber - 1 + p;

            if (checkBox2.Checked == true) // If Warming Profile is enabled
            {
                while (temp[m - 5] < 15)
                {


                }

            }
            // Sets current to TE module to 0
                ps1.IO.WriteString("ISET 2.0\n");
           
                switchRelay(ps2, relayFlag); // Turns relay back off

            // Clears display on DMM
                DMM.Display.Clear();

            // Sets display on DMM to Finished!
                DMM.Display.Text = "Finished!";

            // Writes write arrays to a .csv file
                newWriteFile();

            // Clears DMM measurement buffer
                DMM.Measurement.Buffer.Clear(sbufferName);

                Cleanup();
            
        }

        // **********************************************************************************************
        // ***************************** BASIC MEASUREMENT PROFILE **************************************
        // **********************************************************************************************
        private void btnBasicMeasure_Click(object sender, EventArgs e)
        {
            BasicMeasurement();
        }

        public void BasicMeasurement()
        {
            try
            {
                plotDisplay tempChart = new plotDisplay();

                if (checkBox3.Checked == true)
                    tempChart.Show();

                Hardware basicSetup = new Hardware();

                Ke37XX DMM = basicSetup.InitializeDMM(timeStep, channelNumber, nplc);

                FormattedIO488 pS1 = basicSetup.InitializeAgilent6032();

                FormattedIO488 pS2 = basicSetup.InitializeAgilent6760();
                int numberofChannels = 2;
                int iscanCount = SampleNumber;
                int ibufferSize = iscanCount * numberofChannels;
                

                List<double> dataList = new List<double>();

                double tempValue;
                double amps = constantA;
                double currentProgress;
                double[] dTdt = new double[iscanCount + 1];
                double[] time = new double[iscanCount + 1];
                double[] temp = new double[iscanCount + 1];
                double[] temp2 = new double[iscanCount + 1];
                double[] timeDiff = new double[iscanCount + 1];
                double[] data = new double[iscanCount + 1];
                double[] readings = new double[iscanCount + 1];
                double[] ampstoWrite = new double[iscanCount + 1];
                double AmbTemp = double.Parse(txtSensorTC.Text);
                string sbufferName = "Mybuffer";

                DMM.Display.Clear();

                DMM.Display.Text = "Scanning...";

                DMM.Scan.ScanCount = 1;

                DMM.Measurement.Buffer.Create(sbufferName, ibufferSize);

                DMM.System.DirectIO.IO.Timeout = 10000;

                DMM.Timer.Reset();

                relayFlag = switchRelay(pS2, relayFlag);

                tempChart.InitializePlotDisplay(ListPlot);

                for (int i = 0; i <= iscanCount; i++)
                {
                    Stopwatch debugTimer = new Stopwatch();
                    debugTimer.Start();


                    ampstoWrite[i] = amps;
                    pS1.IO.WriteString("ISET " + constantA.ToString() + "\n");

                    time[i] = DMM.Timer.Measure();


                    DMM.Scan.Execute(sbufferName);


                    // readings = DMM.Measurement.Buffer.ReadingData.GetFormattedReadings(sbufferName,1,2);
                    readings = DMM.Measurement.Buffer.ReadingData.GetAllFormattedReadings(sbufferName);

                    //foreach (double reading in readings)
                    //{

                    //    if (checkboxSensorRead.Checked == true)
                    //    {
                    //        //temp[i] = -1 * (0.9237 - 0.0041 * AmbTemp + 0.00004 * Math.Pow(AmbTemp, 2) - reading) / (0.0305 + 0.000044 * AmbTemp - 1.1 * Math.Pow(10, -6) * Math.Pow(AmbTemp, 2));
                    //        tempValue = -1 * (161.29 * (-reading + 0.16 * 4.5)) / 4.5;

                    //        temp[i] = tempValue / (1.0546 - 0.00216 * AmbTemp);
                    //    }
                    //    else
                    //    {
                    //        temp[i] = getTemp(reading);
                    //    }

                    //}

                    //foreach (double reading in readings)
                    //{
                    //    Debug.WriteLine(reading);
                    //    dataList.Add(reading);
                    //}


                    if (checkboxSensorRead.Checked == true)
                    {
                        tempValue = -1 * (161.29 * (-readings[0] + 0.16 * 5.0)) / 5.0;

                        temp[i] = tempValue / (1.0546 - 0.00216 * AmbTemp);
                    }
                    else
                    {
                        temp[i] = getTemp(readings[0]);
                    }
                    //temp[i] = getTemp(readings[0]);
                    // temp2[i] = getTemp(readings[1]);


                    dataList.Clear();

                    if (i > 5)
                    {
                        int j = i - 2;

                        timeDiff[i] = time[i] - time[i - 1];

                        dTdt[i] = (8 * (temp[j + 1] - temp[j - 1]) + (temp[j - 2] - temp[j + 2])) / (12 * (timeDiff[i]));
                    }
                    if (checkBox3.Checked == true)
                    {
                        if (checkBoxTemp.Checked == true)
                            tempChart.plotData(time[i], temp[i], "Temperature");

                        if (checkBoxDtdt.Checked == true)
                            tempChart.plotData(time[i], dTdt[i], "dTdt");

                        currentProgress = (i * (Math.Pow((double)iscanCount, -1))) * 100;//(double)((i/iscanCount) * 100);

                        currentProgress = (double)Math.Round((decimal)currentProgress, 2);

                        tempChart.updateChart(currentProgress);

                        tempChart.updateValues(time[i], temp[i], temp2[i], amps);
                    }

                }

                pS1.IO.WriteString("ISET 1.5\n");

                switchRelay(pS2, relayFlag); // Turns relay back off

                DMM.Display.Clear();

                DMM.Display.Text = "Finished!";

                writeFile(temp, time, dTdt, iscanCount, ampstoWrite); // Writes data to .csv file

                Cleanup();
            }
            catch
            {
                MessageBox.Show("General Measurement Error, Terminating", "Error!", MessageBoxButtons.OK);
            }
        }

        public void switchPowerOnOff(FormattedIO488 PS1, int chanNum, bool powerFlag)
        {
            // Switches power On/Off
            if (powerFlag == true) // power is already on so switch off
            {
                PS1.IO.WriteString("OUTP OFF,(@" + chanNum.ToString() + ")\n");
            }
            else // power is off so switch on
            {
                PS1.IO.WriteString("OUTP ON,(@" + chanNum.ToString() + ")\n");
            }
        }

        private void btnMeasure_Click(object sender, EventArgs e)
        {
       

            Hardware PowerMeasure = new Hardware();

            FormattedIO488 PS1 = PowerMeasure.InitializeAgilent6760();

            Stopwatch timer = new Stopwatch();
            
            double timeToStop = 0;
            double[] time = new double[50000];
            double[] reading = new double[50000];
            double[] filler = new double[50000];
            double[] ampstoWrite = new double[50000];
            int timeOn = 0;
            int timeOff = 0;
            double currentSet = 0;
            bool switchFLag;
            
            int i = 0;

            // Sets initial current to Agilent N6700B
           // PS1.IO.WriteString("CURR " + currentSet.ToString() + ",(@1)\n");

          

            timer.Start();

            switchFLag = true;

            while (powerFlag == false)
            {
               // PS1.IO.WriteString("OUTP ON,(@1)\n");

                time[i] = timer.Elapsed.TotalSeconds;

                PS1.IO.WriteString("MEAS:VOLT? (@1)\n");

                reading[i] = double.Parse(PS1.IO.ReadString(400));

              //  Thread.Sleep(timeOn);

              //  PS1.IO.WriteString("OUTP OFF,(@1)\n");

           //     Thread.Sleep(timeOff);

                
              
                i++;

                Application.DoEvents();

                if (i == 49990)
                    break;

                
            }


            writeFile(reading, time, filler, 50000, ampstoWrite);

            

            
        }

        #endregion

        #region Frequently Used SubRoutines

        

        public double ControlSysSetTemp(double setTemp, double currentTemp)
        {
            double newA = 0;

            return newA;
        }

        public bool switchRelay(FormattedIO488 ps2, bool flag)
        {
            if (flag == false)
            {
                ps2.IO.WriteString("VOLT 9.0,(@2)\n");
                ps2.IO.WriteString("OUTP ON,(@2)\n");
                flag = true;
                
                return flag;
            }

            if (flag == true)
            {
                ps2.IO.WriteString("OUTP OFF,(@2)\n");
                flag = false;
                
                return flag;
            }

            return flag;
        }



        public double ControlSysCR(double dTdterr, double dTdtint, double dERR)
        {
            double newA;

            newA = ((kp) * dTdterr + ki * dTdtint + dERR * kd);

            //Debug.WriteLine("P-term:  " + (kp * dTdterr).ToString() + "        " + "I-Term:  " + (ki * dTdtint).ToString() + "        " + "D-Term:  " + (dERR * kd).ToString());

            //Debug.WriteLine(newA.ToString());
          
            if (newA >= dUpperCurrent)
                newA = dUpperCurrent;


            if (newA <= dLowerCurrent)
                newA = dLowerCurrent;

            return newA;
        }

        public double getTemp(double volts)
        {
            double temp;

            Debug.WriteLine(volts.ToString());

            volts = (volts - icep) * Math.Pow(10, 6);

            if (volts >= 0)
                temp = c0 + c1 * volts + c2 * Math.Pow(volts, 2) + c3 * Math.Pow(volts, 3) + c4 * Math.Pow(volts, 4) + c5 * Math.Pow(volts, 5) + c6 * Math.Pow(volts, 6) + c7 * Math.Pow(volts, 7) + c8 * Math.Pow(volts, 8) + c9 * Math.Pow(volts, 9);
            else
                temp = c01 + c11 * volts + c21 * Math.Pow(volts, 2) + c31 * Math.Pow(volts, 3) + c41 * Math.Pow(volts, 4) + c51 * Math.Pow(volts, 5) + c61 * Math.Pow(volts, 6) + c71 * Math.Pow(volts, 7) + c81 * Math.Pow(volts, 8) + c91 * Math.Pow(volts, 9);
            return temp;
        }

        public double[] importCurrentProfile()
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                
                int samples = int.Parse(txtExperimentSampleNumber.Text);    

                openFile.Filter = "CSV|*.csv";
                openFile.Title = "Select Data File to Import";
                openFile.DefaultExt = "*.csv";

                openFile.ShowDialog();

                string directory = openFile.FileName;
                
                List<string> CurrentImporte = new List<string>();
                
                string line;
                
                TextReader dataRead = new StreamReader(directory);

                while ((line = dataRead.ReadLine()) != null)
                    CurrentImporte.Add(line);

                int j = 0;

                List<double> AmpArray = new List<double>();

                foreach (string dataEntry in CurrentImporte)
                {
                    string[] placeHolder = dataEntry.Split(',');

                    if (j > 0)
                    {
                        AmpArray.Add(double.Parse(placeHolder[0]));

                        TimeArray.Add(double.Parse(placeHolder[1]));

                        readRelayArray[j] = double.Parse(placeHolder[3]);
                    }

                    j++;
                }

                ampArray = new double[AmpArray.Count];
                
                int k = 0;

                foreach (double amp in AmpArray)
                {
                    ampArray[k] = amp;

                    k++;
                }

                
            }
            catch (Exception error)
            {
                MessageBox.Show("Error Importing/Reading from selected file");
                MessageBox.Show(error.ToString());
            }

            return ampArray;
        }

        public void newWriteFile()
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();

                saveFile.Filter = "CSV|*.csv";
                saveFile.Title = "Save the data file:";
                saveFile.DefaultExt = "*.csv";

                saveFile.ShowDialog();

                string directory = saveFile.FileName;

                TextWriter dataWrite = new StreamWriter(directory);

                dataWrite.WriteLine("Current" + "," + "Time" + "," + "Temperature" + "," + "Relay State");

                for (int i = 0; i < toWriteTime.Length-1; i++)
                {
                    
                    //if (i >= 1 && toWriteTime[i] + timeIncrement < toWriteTime[i - 1])
                    //{
                      //  timeIncrement = toWriteTime[i - 1] + toWriteTime[i];
                    //}

                    if (toWriteTime[i] == 0)
                    {
                        // Do nothing
                    }
                    else
                    {
                        dataWrite.WriteLine(toWriteAmp[i] + "," + (toWriteTime[i]) + "," + toWriteTemp[i] + "," + toWriteRelay[i]);
                    }
                }
                dataWrite.Close();
            }
            catch
            {
                MessageBox.Show("Error saving file, or writing was canceled ", "Error!", MessageBoxButtons.OK);
            }
               
            
        }

        public void writeFile(double[] tempArray, double[] timeArray, double[] dTdt, int scanCount, double[] ampArraytoWrite)
        {

            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();

                saveFile.Filter = "CSV|*.csv";
                saveFile.Title = "Save the data file:";
                saveFile.DefaultExt = "*.csv";

                saveFile.ShowDialog();

                string directory = saveFile.FileName;

                TextWriter dataWrite = new StreamWriter(directory);

                dataWrite.WriteLine("Current" + "," + "Time" + "," + "Temperature" + "," + "dT/dt");

                for (int i = 1; i < scanCount; i++)
                    dataWrite.WriteLine(ampArraytoWrite[i] + "," + timeArray[i] + "," + tempArray[i] + "," + dTdt[i]);

                dataWrite.Close();
            }
            catch
            {
                MessageBox.Show("Error saving file, or writing was canceled ", "Error!", MessageBoxButtons.OK);
            }
        }

       


        #endregion

        #region User Logic

        private void chkboxImport_CheckedChanged(object sender, EventArgs e)
        {
            if (chkboxImport.Checked == true)
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                txtExperimentConstantAA.Enabled = false;
                txtExperimentDTref.Enabled = false;
            }

            if (chkboxImport.Checked == false)
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                txtExperimentConstantAA.Enabled = true;
                txtExperimentDTref.Enabled = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Hardware basicSetup = new Hardware();

            Ke37XX DMM = basicSetup.InitializeDMM(timeStep, channelNumber, nplc);

            FormattedIO488 pS1 = basicSetup.InitializeAgilent6032();

            FormattedIO488 pS2 = basicSetup.InitializeAgilent6760();

            ampArrayD = importCurrentProfile();

            btnExperimentReady.Enabled = true;

            bgWorkerWarmLinCool.RunWorkerAsync();

            while (ExperimentReadyFlag == false)
            {
                Application.DoEvents();
            }
            warmFlag = true;

            importedCurrentProfileNoMeasurement(ampArrayD, DMM, basicSetup, pS1, pS2);
        }



            
        
        private void btnExperimentStart_Click(object sender, EventArgs e)
        {
            btnExperimentStart.Enabled = false;
            
            

            if (radioButton1.Checked == true && checkBox1.Checked == false && checkBox2.Checked == false && chkboxImport.Checked==false)
            {
                BasicMeasurement();
                this.Close();
            }

            if (radioButton2.Checked == true && checkBox1.Checked == false)
            {
                LinearCooling(ampArray);
            }

            if (radioButton2.Checked == true && checkBox1.Checked == true && chkboxImport.Checked == false)
            {
                btnExperimentReady.Enabled = true;

                bgWorkerWarmLinCool.RunWorkerAsync();

                while (ExperimentReadyFlag == false)
                {
                    Application.DoEvents();
                }
                warmFlag = true;

                LinearCooling(ampArray);

            }

            if (chkboxImport.Checked == true)
            {
                double[] ampArrayD;

                ampArrayD = importCurrentProfile();

                btnExperimentReady.Enabled = true;

                bgWorkerWarmLinCool.RunWorkerAsync();

                while (ExperimentReadyFlag == false)
                {
                    Application.DoEvents();
                }
                warmFlag = true;

                importedCurrentProfile(ampArrayD);
                

            }


            btnExperimentStart.Enabled = true;

            if (radioButton1.Checked == true && checkBox1.Checked == false && checkBox2.Checked == false && chkboxImport.Checked == false)
            {
                BasicMeasurement();
                this.Close();
            }
        }

        public void importedCurrentProfile(double[] ampArrayD)
        {
            plotDisplay tempChart = new plotDisplay();

            if (checkBox3.Checked == true)
                tempChart.Show();

            Hardware basicSetup = new Hardware();

            Ke37XX DMM = basicSetup.InitializeDMM(timeStep, channelNumber, nplc);

            FormattedIO488 pS1 = basicSetup.InitializeAgilent6032();

            FormattedIO488 pS2 = basicSetup.InitializeAgilent6760();

            int iscanCount = ampArrayD.Length + 100;
            int ibufferSize = iscanCount;

          
            double amps = constantA;
            double currentProgress;
            double[] dTdt = new double[ampArrayD.Length + 1];
            double[] time = new double[ampArrayD.Length + 1];
            double[] temp = new double[ampArrayD.Length + 1];
            double[] temp2 = new double[ampArrayD.Length + 1];
            double[] timeDiff = new double[ampArrayD.Length + 1];
            double[] data = new double[ampArrayD.Length + 1];
            double[] readings = new double[ampArrayD.Length + 1];
            double[] ampstoWrite = new double[ampArrayD.Length + 1];
            
            string sbufferName = "Mybuffer";

            DMM.Display.Clear();

            DMM.Display.Text = "Scanning...";

            DMM.Scan.ScanCount = 1;

            DMM.Measurement.Buffer.Create(sbufferName, ibufferSize);

            DMM.System.DirectIO.IO.Timeout = 10000;

            DMM.Timer.Reset();

            tempChart.InitializePlotDisplay(ListPlot);


                for (int i = 0; i <= ampArrayD.Length - 1; i++)
                {

                    pS1.IO.WriteString("ISET " + ampArrayD[i] + "\n");

                    time[i] = DMM.Timer.Measure();

                    DMM.Scan.Execute(sbufferName);

                    ampstoWrite[i] = ampArrayD[i];
                    
                    readings = DMM.Measurement.Buffer.ReadingData.GetAllFormattedReadings(sbufferName);
             
                    temp[i] = getTemp(readings[0]);

                    //temp2[i] = getTemp(readings[1]);

         
                    if (i > 5)
                    {
                        int j = i - 2;

                        timeDiff[i] = time[i] - time[i - 1];

                        dTdt[i] = (8 * (temp[j + 1] - temp[j - 1]) + (temp[j - 2] - temp[j + 2])) / (12 * (timeDiff[i]));
                    }

                    if (relayFlag == false & readRelayArray[i] == 1)
                    {
                        relayFlag = switchRelay(pS2, relayFlag);
                    }

                    if (relayFlag == true & readRelayArray[i] == 0)
                    {
                        relayFlag = switchRelay(pS2, relayFlag);
                    }

                    if (checkBox3.Checked == true)
                    {
                        if (checkBoxTemp.Checked == true)
                            tempChart.plotData(time[i], temp[i], "Temperature");

                        if (checkBoxDtdt.Checked == true)
                            tempChart.plotData(time[i], temp2[i], "dTdt");

                        currentProgress = (i * (Math.Pow((double)iscanCount, -1))) * 100;//(double)((i/iscanCount) * 100);

                        currentProgress = (double)Math.Round((decimal)currentProgress, 2);

                        tempChart.updateChart(currentProgress);

                        tempChart.updateValues(time[i], temp[i], temp2[i], amps);
                    }
                    toWriteAmp[i] = ampArrayD[i];
                    toWriteRelay[i] = readRelayArray[i];
                    toWriteTemp[i] = temp[i];
                    toWriteTime[i] = time[i];
                    toWriteTemp2[i] = temp2[i];
                }

                pS1.IO.WriteString("ISET 0.0\n");

                //switchRelay(pS2, relayFlag); // Turns relay back off

                DMM.Display.Clear();

                DMM.Display.Text = "Finished!";

                //writeFile(temp, time, dTdt, iscanCount, ampstoWrite); // Writes data to .csv file
                newWriteFile();

                Cleanup();

                switchRelay(pS2, relayFlag);
            
        }

        private void btnExperimentReady_Click(object sender, EventArgs e)
        {
            ExperimentReadyFlag = true;
        }


        private void txtExperimentSampleNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SampleNumber = int.Parse(txtExperimentSampleNumber.Text);
            }
            catch
            {
                MessageBox.Show("Parse Error!", "Error!", MessageBoxButtons.OK);
            }
        }

        private void txtExperimentDTref_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dtdtCoolRef = double.Parse(txtExperimentDTref.Text);
            }
            catch
            {
                MessageBox.Show("Parse Error!", "Error!", MessageBoxButtons.OK);
            }
        }

        private void txtExperimentConstantAA_TextChanged(object sender, EventArgs e)
        {
            try
            {
                constantA = double.Parse(txtExperimentConstantAA.Text);
            }
            catch
            {
                MessageBox.Show("Parse Error!", "Error!", MessageBoxButtons.OK);
            }
        }

        private void txtExperimentTset_TextChanged(object sender, EventArgs e)
        {
            try
            {
                setTemp = double.Parse(txtExperimentTset.Text);
            }
            catch
            {
                MessageBox.Show("Parse Error!", "Error!", MessageBoxButtons.OK);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                txtExperimentDTref.Enabled = false;
                txtExperimentConstantAA.Enabled = true;
            }
            else
            {
                txtExperimentConstantAA.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                txtExperimentDTref.Enabled = true;
                txtExperimentConstantAA.Enabled = false;
            }
            else
            {
                txtExperimentConstantAA.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtExperimentTset.Enabled = true;
            }
            else
            {
                txtExperimentTset.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
               
            }
            else
            {
                
            }
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();

            aboutBox.Show();
        }

        private void txtmaxCurrent_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dUpperCurrent = double.Parse(txtmaxCurrent.Text);
                
            }
            catch
            {
                MessageBox.Show("Error:  Parse Error", "Error!", MessageBoxButtons.OK);
            }

        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void checkBoxTemp_CheckedChanged(object sender, EventArgs e)
        {
            ListPlot.Clear();

            if (checkBoxDtdt.Checked == true)
            {
                ListPlot.Add("dTdt");
            }


            if (checkBoxTemp.Checked == true)
            {
                ListPlot.Add("Temperature");
            }
        }

        private void checkBoxDtdt_CheckedChanged(object sender, EventArgs e)
        {
            ListPlot.Clear();

            if (checkBoxDtdt.Checked == true)
            {
                ListPlot.Add("dTdt");
            }

            if (checkBoxTemp.Checked == true)
            {
                ListPlot.Add("Temperature");
            }

        }

        private void txtTimeStep_TextChanged(object sender, EventArgs e)
        {
            try
            {
                timeStep = double.Parse(txtTimeStep.Text);
            }
            catch
            {
                MessageBox.Show("Parse Error!", "Error!", MessageBoxButtons.OK);
                txtTimeStep.Text = "0.005";
            }


        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtkI_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ki = double.Parse(txtkI.Text);
            }
            catch
            {
                MessageBox.Show("Parse Error", "Error!", MessageBoxButtons.OK);
                InitializeValues();
            }
        }

        private void txtkP_TextChanged(object sender, EventArgs e)
        {
            try
            {
                kp = double.Parse(txtkP.Text);
            }
            catch
            {
                MessageBox.Show("Parse Error", "Error!", MessageBoxButtons.OK);
                InitializeValues();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                kd = double.Parse(txtkd.Text);
            }
            catch
            {
                MessageBox.Show("Parse Error!", "Error!", MessageBoxButtons.OK);
                InitializeValues();
            }
        }

        private void txtChannelNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                channelNumber = txtChannelNumber.Text;
            }
            catch
            {
                MessageBox.Show("Parse Error!", "Error!", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region Cleanup/Disposal Routines

        public void Cleanup()
        {
            ListPlot.Clear();
            InitializeValues();
            //bgWorkerWarmLinCool.Dispose();
            
        }



        #endregion

        #region Help Boxes

        private void basicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Takes a specified number of measurements out of channel 6006, converting EMF's into temperature (Deg C) using a fixed ice point reference", "Explanation", MessageBoxButtons.OK);
        }



        private void linearCoolingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Produces a linear cooling rate, utilizing ki, kp, and kd.  Program first takes 100 measurements, then proceeds to match the given cooling rate.");
        }

        #endregion

        #region Warm/Lin/Cool + BGWorker Thread

        // When the background worker is called, a seperate thread is generated to perform the following task:
        private void bgWorkerWarmLinCool_DoWork(object sender, DoWorkEventArgs e)
        {
            Hardware warmLinCool = new Hardware();
            FormattedIO488 ps1 = warmLinCool.InitializeAgilent6032();
            FormattedIO488 ps2 = warmLinCool.InitializeAgilent6760();

            Ke37XX DMM = warmLinCool.InitializeDMM(timeStep,channelNumber, nplc);

            warmingProfile(ps1,ps2,DMM);
        }

        public void warmingProfile(FormattedIO488 ps1, FormattedIO488 ps2, Ke37XX DMM)
        {
            
            int iscanCount = 50000;
         
            double[] dTdt = new double[iscanCount + 1];
            double[] time = new double[iscanCount + 1];
            double[] temp = new double[iscanCount + 1];
            double[] temp2 = new double[iscanCount + 1];
            double[] timeDiff = new double[iscanCount + 1];
            double[] data = new double[iscanCount + 1];
            double[] readings = new double[iscanCount + 1];
            double[] tErr = new double[iscanCount + 1];
            double[] tErrInt = new double[iscanCount + 1];
            double amps;

            string sbufferName = "Mybuffer";
            DMM.Measurement.Buffer.Clear(sbufferName);

            DMM.Display.Clear();

            DMM.Display.Text = "Scanning...";

            DMM.Scan.ScanCount = 1;

            DMM.Measurement.Buffer.Create(sbufferName, 50000);

            DMM.System.DirectIO.IO.Timeout = 10000;

            DMM.Timer.Reset();

            int i = 0;

            Stopwatch debugTimer = new Stopwatch();

            while (ExperimentReadyFlag == false)
            {
               

                time[i] = DMM.Timer.Measure();

                DMM.Scan.Execute(sbufferName);

                readings = DMM.Measurement.Buffer.ReadingData.GetAllFormattedReadings(sbufferName);
              
                temp[i] = getTemp(readings[0]);

                tErr[i] = setTemp - temp[i];

                if (i > 2)
                    tErrInt[i] = tErrInt[i - 1] + tErr[i];

               // debugTimer.Start();

                if (i > 5)
                {
                    int kk = i - 2;

                    timeDiff[i] = time[i] - time[i - 1];

                    dTdt[i] = (8 * (temp[kk + 1] - temp[kk - 1]) + (temp[kk - 2] - temp[kk + 2])) / (12 * (timeDiff[i]));
                }
                else
                {
                    dTdt[i] = 0;
                }

                DMM.Display.Clear();

                DMM.Display.Text = "Temp: " + String.Format("{0:0.000}", temp[i]) + " C";

                Debug.WriteLine("Temp: " + temp[i]);

                amps = 0.45 * tErr[i] + 0.0015 * tErrInt[i];

                Debug.WriteLine(tErr[i].ToString() + "         " + tErrInt[i].ToString());

                if (amps > 4)
                    amps = 4;

                if (amps < 0)
                    amps = 0;


                ps1.IO.WriteString("ISET " + amps + "\n");


                i++;

               // debugTimer.Stop();

              //  Debug.WriteLine(debugTimer.ElapsedMilliseconds.ToString());

                //debugTimer.Reset();
                
            }

            DMM.Display.Clear();

            DMM.Display.Text = "Finished!";
        }


        #endregion

        private void stopButton_Click(object sender, EventArgs e)
        {
            powerFlag = true;   
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBoxDtdt.Enabled = true;
                checkBoxTemp.Enabled = true;
            }

            if (checkBox3.Checked == false)
            {
                checkBoxDtdt.Enabled = false;
                checkBoxTemp.Enabled = false;
            }
        }

        public void importedCurrentProfileNoMeasurement(double[] ampArrayD, Ke37XX DMM, Hardware basicSetup, FormattedIO488 pS1, FormattedIO488 pS2)
        {
            double[] timeArray = TimeArray.ToArray();

            timeStepArray = new double[timeArray.Length];

            timeStepArrayInt = new int[timeArray.Length];
            
            for (int i = 0; i <= timeArray.Length - 2; i++)
            {
                timeStepArray[i] = -1000*(timeArray[i] - timeArray[i + 1]);

                timeStepArray[i] = Math.Round(timeStepArray[i], 0);

                timeStepArrayInt[i] = (int)timeStepArray[i] - 4;

                if (checkBox4.Checked == true)
                {
                    timeStepArrayInt[i] = (int)(1000 * const_timeStep) - 4;
                    Debug.WriteLine(timeStepArray[i].ToString());
                }
                
            }
            


            int iscanCount = SampleNumberDMMHalf + 100;
            int ibufferSize = iscanCount;


            double amps = constantA;

            double[] dTdt = new double[SampleNumberDMMHalf + 1];
            double[] time = new double[SampleNumberDMMHalf + 1];
            double[] temp = new double[SampleNumberDMMHalf + 1];
            double[] temp2 = new double[SampleNumberDMMHalf + 1];
            double[] timeDiff = new double[SampleNumberDMMHalf + 1];
            double[] data = new double[SampleNumberDMMHalf + 1];
            double[] readings = new double[SampleNumberDMMHalf + 1];
            double[] ampstoWrite = new double[SampleNumberDMMHalf + 1];

            string sbufferName = "Mybuffer";

            

            DMM.Display.Clear();

            DMM.Display.Text = "Scanning...";

            DMM.Scan.ScanCount = SampleNumberDMMHalf;

            DMM.Measurement.Buffer.Create(sbufferName, ibufferSize);

            DMM.System.DirectIO.IO.Timeout = 10000;

            DMM.Timer.Reset();

            DMM.Scan.ExecuteBackground(sbufferName);


            for (int i = 0; i <= ampArrayD.Length-1; i++)
            {
                Thread.Sleep(timeStepArrayInt[i]);

                pS1.IO.WriteString("ISET " + ampArrayD[i] + "\n");


                if (relayFlag == false & readRelayArray[i] == 1)
                {
                    relayFlag = switchRelay(pS2, relayFlag);
                }

                if (relayFlag == true & readRelayArray[i] == 0)
                {
                    relayFlag = switchRelay(pS2, relayFlag);
                }

            }

            readings = DMM.Measurement.Buffer.ReadingData.GetAllFormattedReadings(sbufferName);

            double[] timereadings = DMM.Measurement.Buffer.TimeStampData.GetAllRelativeTimeStamps(sbufferName);


            for (int i = 0; i <= readings.Length - 1; i++)
            {
                temp[i] = getTemp(readings[i]);
                
            }

            for (int j = 0; j <= readings.Length - 1; j++)
            {
                toWriteAmp[j] = 0;
                toWriteTemp[j] = temp[j];
                toWriteTime[j] = timereadings[j];
                toWriteTemp2[j] = 0;
            }
            pS1.IO.WriteString("ISET 2.0\n");

            //switchRelay(pS2, relayFlag); // Turns relay back off

            DMM.Display.Clear();

            DMM.Display.Text = "Finished!";

            //writeFile(temp, time, dTdt, iscanCount, ampstoWrite); // Writes data to .csv file
            newWriteFile();

            Cleanup();

            switchRelay(pS2, relayFlag);

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                SampleNumberDMMHalf = int.Parse(sampleNoTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Parse Error!  Try Again", "Error!", MessageBoxButtons.OK);
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hardware basicSetup = new Hardware();

            Ke37XX DMM = basicSetup.InitializeDMM(timeStep, channelNumber, nplc);

            SampleNumberDMMHalf = 20000;

            int iscanCount = SampleNumberDMMHalf + 100;
            int ibufferSize = iscanCount;


            double amps = constantA;
            double[] dTdt = new double[SampleNumberDMMHalf + 1];
            double[] time = new double[SampleNumberDMMHalf + 1];
            double[] temp = new double[SampleNumberDMMHalf + 1];
            double[] temp2 = new double[SampleNumberDMMHalf + 1];
            double[] timeDiff = new double[SampleNumberDMMHalf + 1];
            double[] data = new double[SampleNumberDMMHalf + 1];
            double[] readings = new double[SampleNumberDMMHalf + 1];
            double[] ampstoWrite = new double[SampleNumberDMMHalf + 1];

            string sbufferName = "Mybuffer";

            DMM.Display.Clear();

            DMM.Display.Text = "Scanning...";

            DMM.Scan.ScanCount = SampleNumberDMMHalf;

            DMM.Measurement.Buffer.Create(sbufferName, ibufferSize);

            DMM.Timer.Reset();

            DMM.Scan.ExecuteBackground(sbufferName);

            Thread.Sleep(60000);

            readings = DMM.Measurement.Buffer.ReadingData.GetAllFormattedReadings(sbufferName);
            
            double[] timereadings = DMM.Measurement.Buffer.TimeStampData.GetAllRelativeTimeStamps(sbufferName);

            for (int i = 0; i <= readings.Length - 1; i++)
            {
                temp[i] = getTemp(readings[i]);

                toWriteAmp[i] = 0;
                toWriteRelay[i] = 0;
                toWriteTemp[i] = temp[i];
                toWriteTime[i] = timereadings[i];
            }

            DMM.Measurement.Buffer.Clear(sbufferName);

            //switchRelay(pS2, relayFlag); // Turns relay back off

            DMM.Display.Clear();

            DMM.Display.Text = "Finished!";

            DMM.Measurement.ResetAll();

            DMM.Close();

            
            newWriteFile();

            Cleanup();

          
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
           // Open Arduino Control Panel 
            bgArduino.RunWorkerAsync();

            
           
        }

        private void bgArduino_DoWork(object sender, DoWorkEventArgs e)
        {
            ArduinoCP arduino = new ArduinoCP();

            arduino.Show();

            arduino.Go();
        }

        private void btn_Analyze_Click(object sender, EventArgs e)
        {
            // Begin data analysis by forcing users to pick two data files
            double[,] Data_File_Baseline;
            double[,] Data_File_Sample;

            CSVReader Read1 = new CSVReader();
            CSVReader Read2 = new CSVReader();

            Data_File_Baseline = Read1.Read("Select Baseline File");
            Data_File_Sample = Read2.Read("Select Sample File");

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                textBox1.Enabled = true;
                textBox1.Text = "0.05";
            }
            else
            {
                textBox1.Enabled = false;
               
            }
        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {
            try
            {
                const_timeStep = double.Parse(textBox1.Text);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

      

    }
}