namespace ThermalControl
{
    partial class plotDisplay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tempChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnSaveChart = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtTemp = new System.Windows.Forms.TextBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.txtdtdt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAmps = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTempDiff = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.tempChart)).BeginInit();
            this.SuspendLayout();
            // 
            // tempChart
            // 
            chartArea1.Name = "ChartArea1";
            this.tempChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.tempChart.Legends.Add(legend1);
            this.tempChart.Location = new System.Drawing.Point(0, 0);
            this.tempChart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tempChart.Name = "tempChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.tempChart.Series.Add(series1);
            this.tempChart.Size = new System.Drawing.Size(1036, 610);
            this.tempChart.TabIndex = 0;
            this.tempChart.Text = "chart1";
            this.tempChart.Click += new System.EventHandler(this.tempChart_Click);
            // 
            // btnSaveChart
            // 
            this.btnSaveChart.Location = new System.Drawing.Point(816, 638);
            this.btnSaveChart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSaveChart.Name = "btnSaveChart";
            this.btnSaveChart.Size = new System.Drawing.Size(100, 28);
            this.btnSaveChart.TabIndex = 1;
            this.btnSaveChart.Text = "Save Chart";
            this.btnSaveChart.UseVisualStyleBackColor = true;
            this.btnSaveChart.Click += new System.EventHandler(this.btnSaveChart_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(924, 638);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 28);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Close";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtTemp
            // 
            this.txtTemp.Location = new System.Drawing.Point(21, 641);
            this.txtTemp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTemp.MaxLength = 5;
            this.txtTemp.Name = "txtTemp";
            this.txtTemp.ReadOnly = true;
            this.txtTemp.Size = new System.Drawing.Size(105, 22);
            this.txtTemp.TabIndex = 3;
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(160, 641);
            this.txtTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTime.MaxLength = 5;
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            this.txtTime.Size = new System.Drawing.Size(105, 22);
            this.txtTime.TabIndex = 4;
            // 
            // txtdtdt
            // 
            this.txtdtdt.Location = new System.Drawing.Point(303, 641);
            this.txtdtdt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtdtdt.MaxLength = 5;
            this.txtdtdt.Name = "txtdtdt";
            this.txtdtdt.ReadOnly = true;
            this.txtdtdt.Size = new System.Drawing.Size(105, 22);
            this.txtdtdt.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 618);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Temp - C (6006)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 618);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Time (s)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(299, 618);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Temp - C (6007)";
            // 
            // txtAmps
            // 
            this.txtAmps.Location = new System.Drawing.Point(443, 641);
            this.txtAmps.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAmps.Name = "txtAmps";
            this.txtAmps.ReadOnly = true;
            this.txtAmps.Size = new System.Drawing.Size(132, 22);
            this.txtAmps.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(439, 618);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Amps:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(584, 618);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Temperature Difference";
            // 
            // txtTempDiff
            // 
            this.txtTempDiff.Location = new System.Drawing.Point(588, 641);
            this.txtTempDiff.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTempDiff.Name = "txtTempDiff";
            this.txtTempDiff.ReadOnly = true;
            this.txtTempDiff.Size = new System.Drawing.Size(153, 22);
            this.txtTempDiff.TabIndex = 12;
            // 
            // plotDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 681);
            this.Controls.Add(this.txtTempDiff);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAmps);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtdtdt);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.txtTemp);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSaveChart);
            this.Controls.Add(this.tempChart);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "plotDisplay";
            this.Text = "Plot Display";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.plotDisplay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tempChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart tempChart;
        private System.Windows.Forms.Button btnSaveChart;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtTemp;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.TextBox txtdtdt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAmps;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTempDiff;
    }
}