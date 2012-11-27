using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ivi.Visa.Interop;
using Keithley.Ke37XX.Interop;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace ThermalControl
{
    public class Hardware
    {
        public FormattedIO488 InitializeAgilent6032()
        {
            try
            {
                // Initialization Logic
                ResourceManager pS1RM = new ResourceManager();
                FormattedIO488 pS1 = new FormattedIO488();

                pS1.IO = (IMessage)pS1RM.Open("GPIB0::5::INSTR", AccessMode.NO_LOCK, 20000, "");

                pS1.IO.WriteString("VSET 24.0\n");

                return pS1;
            }
            catch
            {
                MessageBox.Show("Initialization Error", "Error!", MessageBoxButtons.OK);
            }

            FormattedIO488 BAD = new FormattedIO488();
            return BAD;
           
        }

        public Ke37XX InitializeDMM(double timeStep, string channelNumber, double nplc)
        {
            try
            {
                Ke37XX DMM = new Ke37XX();
                FormattedIO488 udT = new FormattedIO488();
                ResourceManager udTR = new ResourceManager();

                

                if (DMM.Initialized == false)
                {
                    DMM.Initialize("GPIB0::16::INSTR", false, true, "");
                }

                DMM.Channel.Close("2001:2060,6009:6060");

                DMM.Measurement.Nplc = nplc;// 0.0005, 0.11 for accurate ultrafast;
                DMM.Measurement.AutoDelay = Ke37XXAutoDelayEnum.Ke37XXAutoDelayOff;
               
                DMM.Measurement.Function = Ke37XXMeasurementFunctionEnum.Ke37XXMeasurementFunctionDCVolts;

                DMM.Measurement.AutoRange = true;

                DMM.Measurement.AutoZero = Ke37XXAutoZeroEnum.Ke37XXAutoZeroOnce;

                DMM.Measurement.Configuration.Create("DCVoltsCfg");

                DMM.Scan.CreateScanList(channelNumber, "DCVoltsCfg");

                udT.IO = (IMessage)udTR.Open("GPIB::16::INSTR", AccessMode.NO_LOCK, 10000, "");

               // udT.WriteString("PERIOD = trigger.timer[1]", true);
               // udT.WriteString("PERIOD.delay = " + timeStep, true);

               // udT.WriteString("PERIOD.stimulus = scan.trigger.EVENT_SCAN_START", true);

               // udT.WriteString("PERIOD.count = 1", true);

               // udT.WriteString("scan.trigger.measure.stimulus = PERIOD.EVENT_ID", true);

                DMM.Scan.Mode = Ke37XXScanModeEnum.Ke37XXScanModeOpenAll;

                DMM.Display.Text = "Initialized";

                return DMM;
            }
            catch
            {
                MessageBox.Show("DMM Initialization Failure", "Error!", MessageBoxButtons.OK);
            }

            Ke37XX BAD = new Ke37XX();

            return BAD;
        }

        public FormattedIO488 InitializeAgilent6760()
        {
            try
            {

                ResourceManager pS2RM = new ResourceManager();
                FormattedIO488 pS2 = new FormattedIO488();

                pS2.IO = (IMessage)pS2RM.Open("GPIB0::7::INSTR", AccessMode.NO_LOCK, 20000, "");

                return pS2;
            }
            catch
            {
                MessageBox.Show("Error initializing Agilent PS", "Error!", MessageBoxButtons.OK);
            }

            FormattedIO488 BAD = new FormattedIO488();

            return BAD;
        }

    }
}
