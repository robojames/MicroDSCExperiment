namespace ThermalControl
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.experimentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.basicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linearCoolingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.warmLinCoolingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.warmLinWarmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.LAbel535 = new System.Windows.Forms.Label();
            this.txtChannelNumber = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtkd = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtkI = new System.Windows.Forms.TextBox();
            this.txtkP = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTimeStep = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtmaxCurrent = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxDtdt = new System.Windows.Forms.CheckBox();
            this.checkBoxTemp = new System.Windows.Forms.CheckBox();
            this.bgWorkerWarmLinCool = new System.ComponentModel.BackgroundWorker();
            this.txtSensorTC = new System.Windows.Forms.TextBox();
            this.checkboxSensorRead = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chkboxImport = new System.Windows.Forms.CheckBox();
            this.btnExperimentReady = new System.Windows.Forms.Button();
            this.btnExperimentStart = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.txtExperimentTset = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtExperimentConstantAA = new System.Windows.Forms.TextBox();
            this.txtExperimentDTref = new System.Windows.Forms.TextBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label17 = new System.Windows.Forms.Label();
            this.txtExperimentSampleNumber = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sampleNoTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.bgArduino = new System.ComponentModel.BackgroundWorker();
            this.btn_Analyze = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(496, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.experimentsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // experimentsToolStripMenuItem
            // 
            this.experimentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.basicToolStripMenuItem,
            this.linearCoolingToolStripMenuItem,
            this.warmLinCoolingToolStripMenuItem,
            this.warmLinWarmToolStripMenuItem});
            this.experimentsToolStripMenuItem.Name = "experimentsToolStripMenuItem";
            this.experimentsToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.experimentsToolStripMenuItem.Text = "Experiments";
            // 
            // basicToolStripMenuItem
            // 
            this.basicToolStripMenuItem.Name = "basicToolStripMenuItem";
            this.basicToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.basicToolStripMenuItem.Text = "Basic";
            this.basicToolStripMenuItem.Click += new System.EventHandler(this.basicToolStripMenuItem_Click);
            // 
            // linearCoolingToolStripMenuItem
            // 
            this.linearCoolingToolStripMenuItem.Name = "linearCoolingToolStripMenuItem";
            this.linearCoolingToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.linearCoolingToolStripMenuItem.Text = "Linear Cooling";
            this.linearCoolingToolStripMenuItem.Click += new System.EventHandler(this.linearCoolingToolStripMenuItem_Click);
            // 
            // warmLinCoolingToolStripMenuItem
            // 
            this.warmLinCoolingToolStripMenuItem.Name = "warmLinCoolingToolStripMenuItem";
            this.warmLinCoolingToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.warmLinCoolingToolStripMenuItem.Text = "Warm/Lin Cooling";
            // 
            // warmLinWarmToolStripMenuItem
            // 
            this.warmLinWarmToolStripMenuItem.Name = "warmLinWarmToolStripMenuItem";
            this.warmLinWarmToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.warmLinWarmToolStripMenuItem.Text = "Warm/Lin/Warm";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox3);
            this.groupBox3.Controls.Add(this.LAbel535);
            this.groupBox3.Controls.Add(this.txtChannelNumber);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtkd);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtkI);
            this.groupBox3.Controls.Add(this.txtkP);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtTimeStep);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtmaxCurrent);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.checkBoxDtdt);
            this.groupBox3.Controls.Add(this.checkBoxTemp);
            this.groupBox3.Location = new System.Drawing.Point(284, 29);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 375);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(64, 17);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox3.Size = new System.Drawing.Size(49, 14);
            this.checkBox3.TabIndex = 18;
            this.checkBox3.Text = "Enabled";
            this.checkBox3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // LAbel535
            // 
            this.LAbel535.AutoSize = true;
            this.LAbel535.Location = new System.Drawing.Point(26, 163);
            this.LAbel535.Name = "LAbel535";
            this.LAbel535.Size = new System.Drawing.Size(62, 13);
            this.LAbel535.TabIndex = 17;
            this.LAbel535.Text = "Channel #: ";
            // 
            // txtChannelNumber
            // 
            this.txtChannelNumber.Location = new System.Drawing.Point(94, 160);
            this.txtChannelNumber.Name = "txtChannelNumber";
            this.txtChannelNumber.Size = new System.Drawing.Size(100, 20);
            this.txtChannelNumber.TabIndex = 16;
            this.txtChannelNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtChannelNumber.TextChanged += new System.EventHandler(this.txtChannelNumber_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(54, 241);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "kd";
            // 
            // txtkd
            // 
            this.txtkd.Location = new System.Drawing.Point(79, 240);
            this.txtkd.Name = "txtkd";
            this.txtkd.Size = new System.Drawing.Size(59, 20);
            this.txtkd.TabIndex = 14;
            this.txtkd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtkd.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(108, 217);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "ki";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 217);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "kp";
            // 
            // txtkI
            // 
            this.txtkI.Location = new System.Drawing.Point(129, 214);
            this.txtkI.Name = "txtkI";
            this.txtkI.Size = new System.Drawing.Size(59, 20);
            this.txtkI.TabIndex = 11;
            this.txtkI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtkI.TextChanged += new System.EventHandler(this.txtkI_TextChanged);
            // 
            // txtkP
            // 
            this.txtkP.Location = new System.Drawing.Point(36, 214);
            this.txtkP.Name = "txtkP";
            this.txtkP.Size = new System.Drawing.Size(59, 20);
            this.txtkP.TabIndex = 10;
            this.txtkP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtkP.TextChanged += new System.EventHandler(this.txtkP_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 189);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Control System:";
            // 
            // txtTimeStep
            // 
            this.txtTimeStep.Location = new System.Drawing.Point(89, 88);
            this.txtTimeStep.Name = "txtTimeStep";
            this.txtTimeStep.Size = new System.Drawing.Size(100, 20);
            this.txtTimeStep.TabIndex = 8;
            this.txtTimeStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTimeStep.TextChanged += new System.EventHandler(this.txtTimeStep_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Time Step:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Max Current:";
            // 
            // txtmaxCurrent
            // 
            this.txtmaxCurrent.Location = new System.Drawing.Point(94, 121);
            this.txtmaxCurrent.Name = "txtmaxCurrent";
            this.txtmaxCurrent.Size = new System.Drawing.Size(100, 20);
            this.txtmaxCurrent.TabIndex = 5;
            this.txtmaxCurrent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtmaxCurrent.TextChanged += new System.EventHandler(this.txtmaxCurrent_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Plotting:";
            // 
            // checkBoxDtdt
            // 
            this.checkBoxDtdt.AutoSize = true;
            this.checkBoxDtdt.Location = new System.Drawing.Point(114, 41);
            this.checkBoxDtdt.Name = "checkBoxDtdt";
            this.checkBoxDtdt.Size = new System.Drawing.Size(56, 14);
            this.checkBoxDtdt.TabIndex = 1;
            this.checkBoxDtdt.Text = "Derivative";
            this.checkBoxDtdt.UseVisualStyleBackColor = true;
            this.checkBoxDtdt.CheckedChanged += new System.EventHandler(this.checkBoxDtdt_CheckedChanged);
            // 
            // checkBoxTemp
            // 
            this.checkBoxTemp.AutoSize = true;
            this.checkBoxTemp.Checked = true;
            this.checkBoxTemp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTemp.Location = new System.Drawing.Point(9, 41);
            this.checkBoxTemp.Name = "checkBoxTemp";
            this.checkBoxTemp.Size = new System.Drawing.Size(64, 14);
            this.checkBoxTemp.TabIndex = 0;
            this.checkBoxTemp.Text = "Temperature";
            this.checkBoxTemp.UseVisualStyleBackColor = true;
            this.checkBoxTemp.CheckedChanged += new System.EventHandler(this.checkBoxTemp_CheckedChanged);
            // 
            // bgWorkerWarmLinCool
            // 
            this.bgWorkerWarmLinCool.WorkerSupportsCancellation = true;
            this.bgWorkerWarmLinCool.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerWarmLinCool_DoWork);
            // 
            // txtSensorTC
            // 
            this.txtSensorTC.Location = new System.Drawing.Point(60, 168);
            this.txtSensorTC.Name = "txtSensorTC";
            this.txtSensorTC.Size = new System.Drawing.Size(73, 20);
            this.txtSensorTC.TabIndex = 8;
            this.txtSensorTC.Text = "22.0";
            this.txtSensorTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkboxSensorRead
            // 
            this.checkboxSensorRead.AutoSize = true;
            this.checkboxSensorRead.Location = new System.Drawing.Point(140, 170);
            this.checkboxSensorRead.Name = "checkboxSensorRead";
            this.checkboxSensorRead.Size = new System.Drawing.Size(66, 14);
            this.checkboxSensorRead.TabIndex = 7;
            this.checkboxSensorRead.Text = "Sensor Read";
            this.checkboxSensorRead.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chkboxImport);
            this.groupBox6.Controls.Add(this.btnExperimentReady);
            this.groupBox6.Controls.Add(this.btnExperimentStart);
            this.groupBox6.Controls.Add(this.checkBox2);
            this.groupBox6.Controls.Add(this.txtExperimentTset);
            this.groupBox6.Controls.Add(this.txtSensorTC);
            this.groupBox6.Controls.Add(this.checkBox1);
            this.groupBox6.Controls.Add(this.checkboxSensorRead);
            this.groupBox6.Controls.Add(this.txtExperimentConstantAA);
            this.groupBox6.Controls.Add(this.txtExperimentDTref);
            this.groupBox6.Controls.Add(this.radioButton2);
            this.groupBox6.Controls.Add(this.radioButton1);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.txtExperimentSampleNumber);
            this.groupBox6.Location = new System.Drawing.Point(12, 29);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(266, 234);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Experiment:";
            // 
            // chkboxImport
            // 
            this.chkboxImport.AutoSize = true;
            this.chkboxImport.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkboxImport.Location = new System.Drawing.Point(31, 147);
            this.chkboxImport.Name = "chkboxImport";
            this.chkboxImport.Size = new System.Drawing.Size(93, 14);
            this.chkboxImport.TabIndex = 12;
            this.chkboxImport.Text = "Import Current Profile";
            this.chkboxImport.UseVisualStyleBackColor = true;
            this.chkboxImport.CheckedChanged += new System.EventHandler(this.chkboxImport_CheckedChanged);
            // 
            // btnExperimentReady
            // 
            this.btnExperimentReady.Location = new System.Drawing.Point(185, 195);
            this.btnExperimentReady.Name = "btnExperimentReady";
            this.btnExperimentReady.Size = new System.Drawing.Size(75, 33);
            this.btnExperimentReady.TabIndex = 11;
            this.btnExperimentReady.Text = "Ready!";
            this.btnExperimentReady.UseVisualStyleBackColor = true;
            this.btnExperimentReady.Click += new System.EventHandler(this.btnExperimentReady_Click);
            // 
            // btnExperimentStart
            // 
            this.btnExperimentStart.Location = new System.Drawing.Point(6, 195);
            this.btnExperimentStart.Name = "btnExperimentStart";
            this.btnExperimentStart.Size = new System.Drawing.Size(173, 33);
            this.btnExperimentStart.TabIndex = 10;
            this.btnExperimentStart.Text = "Start";
            this.btnExperimentStart.UseVisualStyleBackColor = true;
            this.btnExperimentStart.Click += new System.EventHandler(this.btnExperimentStart_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox2.Location = new System.Drawing.Point(28, 124);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(97, 14);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "Final Warming (dT/dt)";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // txtExperimentTset
            // 
            this.txtExperimentTset.Location = new System.Drawing.Point(163, 98);
            this.txtExperimentTset.Name = "txtExperimentTset";
            this.txtExperimentTset.Size = new System.Drawing.Size(82, 20);
            this.txtExperimentTset.TabIndex = 7;
            this.txtExperimentTset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtExperimentTset.TextChanged += new System.EventHandler(this.txtExperimentTset_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Location = new System.Drawing.Point(46, 102);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(83, 14);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Initial Warming (T)";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtExperimentConstantAA
            // 
            this.txtExperimentConstantAA.Location = new System.Drawing.Point(163, 48);
            this.txtExperimentConstantAA.Name = "txtExperimentConstantAA";
            this.txtExperimentConstantAA.Size = new System.Drawing.Size(82, 20);
            this.txtExperimentConstantAA.TabIndex = 5;
            this.txtExperimentConstantAA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtExperimentConstantAA.TextChanged += new System.EventHandler(this.txtExperimentConstantAA_TextChanged);
            // 
            // txtExperimentDTref
            // 
            this.txtExperimentDTref.Location = new System.Drawing.Point(163, 72);
            this.txtExperimentDTref.Name = "txtExperimentDTref";
            this.txtExperimentDTref.Size = new System.Drawing.Size(82, 20);
            this.txtExperimentDTref.TabIndex = 4;
            this.txtExperimentDTref.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtExperimentDTref.TextChanged += new System.EventHandler(this.txtExperimentDTref_TextChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(62, 74);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(73, 14);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Constant dT/dt";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioButton1.Location = new System.Drawing.Point(80, 48);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(58, 14);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.Text = "Constant A";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(102, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 13);
            this.label17.TabIndex = 1;
            this.label17.Text = "Sample #:";
            // 
            // txtExperimentSampleNumber
            // 
            this.txtExperimentSampleNumber.Location = new System.Drawing.Point(163, 18);
            this.txtExperimentSampleNumber.Name = "txtExperimentSampleNumber";
            this.txtExperimentSampleNumber.Size = new System.Drawing.Size(82, 20);
            this.txtExperimentSampleNumber.TabIndex = 0;
            this.txtExperimentSampleNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtExperimentSampleNumber.TextChanged += new System.EventHandler(this.txtExperimentSampleNumber_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.sampleNoTextBox);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 269);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(266, 136);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Playback Current Profile";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(62, 62);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 20);
            this.textBox1.TabIndex = 15;
            this.textBox1.Text = "0.005";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_2);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(146, 63);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(89, 14);
            this.checkBox4.TabIndex = 14;
            this.checkBox4.Text = "Constant Time Step";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Sample #:";
            // 
            // sampleNoTextBox
            // 
            this.sampleNoTextBox.Location = new System.Drawing.Point(62, 26);
            this.sampleNoTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.sampleNoTextBox.Name = "sampleNoTextBox";
            this.sampleNoTextBox.Size = new System.Drawing.Size(76, 20);
            this.sampleNoTextBox.TabIndex = 8;
            this.sampleNoTextBox.Text = "4000";
            this.sampleNoTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(156, 17);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(9, 410);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(225, 57);
            this.button2.TabIndex = 8;
            this.button2.Text = "Ardunio Control Panel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // bgArduino
            // 
            this.bgArduino.WorkerSupportsCancellation = true;
            this.bgArduino.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgArduino_DoWork);
            // 
            // btn_Analyze
            // 
            this.btn_Analyze.Location = new System.Drawing.Point(261, 410);
            this.btn_Analyze.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Analyze.Name = "btn_Analyze";
            this.btn_Analyze.Size = new System.Drawing.Size(226, 56);
            this.btn_Analyze.TabIndex = 9;
            this.btn_Analyze.Text = "Analyze";
            this.btn_Analyze.UseVisualStyleBackColor = true;
            this.btn_Analyze.Click += new System.EventHandler(this.btn_Analyze_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 476);
            this.Controls.Add(this.btn_Analyze);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thermal Control";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxDtdt;
        private System.Windows.Forms.CheckBox checkBoxTemp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtmaxCurrent;
        private System.Windows.Forms.TextBox txtTimeStep;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtkI;
        private System.Windows.Forms.TextBox txtkP;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtkd;
        private System.Windows.Forms.ToolStripMenuItem experimentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem basicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linearCoolingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warmLinCoolingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warmLinWarmToolStripMenuItem;
        private System.Windows.Forms.Label LAbel535;
        private System.Windows.Forms.TextBox txtChannelNumber;
        private System.ComponentModel.BackgroundWorker bgWorkerWarmLinCool;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TextBox txtExperimentTset;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtExperimentConstantAA;
        private System.Windows.Forms.TextBox txtExperimentDTref;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtExperimentSampleNumber;
        private System.Windows.Forms.Button btnExperimentStart;
        private System.Windows.Forms.Button btnExperimentReady;
        private System.Windows.Forms.CheckBox chkboxImport;
        private System.Windows.Forms.CheckBox checkboxSensorRead;
        private System.Windows.Forms.TextBox txtSensorTC;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox sampleNoTextBox;
        private System.Windows.Forms.Button button2;
        private System.ComponentModel.BackgroundWorker bgArduino;
        private System.Windows.Forms.Button btn_Analyze;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox4;

    }
}

