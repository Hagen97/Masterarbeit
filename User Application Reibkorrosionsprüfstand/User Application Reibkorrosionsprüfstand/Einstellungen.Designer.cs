namespace User_Application_Reibkorrosionsprüfstand
{
    partial class Form_Einstellungen
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
            this.Label_ComPort = new System.Windows.Forms.Label();
            this.Label_Baudrate = new System.Windows.Forms.Label();
            this.ComboBox_ComPortMilliohmmeter = new System.Windows.Forms.ComboBox();
            this.GroupBox_Milliohmmeter = new System.Windows.Forms.GroupBox();
            this.ComboBox_BaudrateMilliohmmeter = new System.Windows.Forms.ComboBox();
            this.GroupBox_Kraftsensor = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ComboBox_ComPortKraftsensor = new System.Windows.Forms.ComboBox();
            this.GroupBox_Digitalanzeige = new System.Windows.Forms.GroupBox();
            this.ComboBox_BaudrateDigitalanzeige = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ComboBox_ComPortDigitalanzeige = new System.Windows.Forms.ComboBox();
            this.Button_Speichern = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_HystereseTemperatur = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown_AdresseTemperaturregler = new System.Windows.Forms.NumericUpDown();
            this.ComboBox_BaudrateTemperaturregler = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ComboBox_ComPortTemperaturregler = new System.Windows.Forms.ComboBox();
            this.checkBox_ReibkraftmessungAktiv = new System.Windows.Forms.CheckBox();
            this.GroupBox_Milliohmmeter.SuspendLayout();
            this.GroupBox_Kraftsensor.SuspendLayout();
            this.GroupBox_Digitalanzeige.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_HystereseTemperatur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AdresseTemperaturregler)).BeginInit();
            this.SuspendLayout();
            // 
            // Label_ComPort
            // 
            this.Label_ComPort.AutoSize = true;
            this.Label_ComPort.Location = new System.Drawing.Point(4, 20);
            this.Label_ComPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_ComPort.Name = "Label_ComPort";
            this.Label_ComPort.Size = new System.Drawing.Size(56, 13);
            this.Label_ComPort.TabIndex = 0;
            this.Label_ComPort.Text = "COM-Port:";
            this.Label_ComPort.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Label_Baudrate
            // 
            this.Label_Baudrate.AutoSize = true;
            this.Label_Baudrate.Location = new System.Drawing.Point(7, 44);
            this.Label_Baudrate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_Baudrate.Name = "Label_Baudrate";
            this.Label_Baudrate.Size = new System.Drawing.Size(53, 13);
            this.Label_Baudrate.TabIndex = 1;
            this.Label_Baudrate.Text = "Baudrate:";
            this.Label_Baudrate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ComboBox_ComPortMilliohmmeter
            // 
            this.ComboBox_ComPortMilliohmmeter.FormattingEnabled = true;
            this.ComboBox_ComPortMilliohmmeter.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            this.ComboBox_ComPortMilliohmmeter.Location = new System.Drawing.Point(60, 17);
            this.ComboBox_ComPortMilliohmmeter.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBox_ComPortMilliohmmeter.Name = "ComboBox_ComPortMilliohmmeter";
            this.ComboBox_ComPortMilliohmmeter.Size = new System.Drawing.Size(92, 21);
            this.ComboBox_ComPortMilliohmmeter.TabIndex = 2;
            // 
            // GroupBox_Milliohmmeter
            // 
            this.GroupBox_Milliohmmeter.Controls.Add(this.ComboBox_BaudrateMilliohmmeter);
            this.GroupBox_Milliohmmeter.Controls.Add(this.Label_ComPort);
            this.GroupBox_Milliohmmeter.Controls.Add(this.Label_Baudrate);
            this.GroupBox_Milliohmmeter.Controls.Add(this.ComboBox_ComPortMilliohmmeter);
            this.GroupBox_Milliohmmeter.Location = new System.Drawing.Point(9, 10);
            this.GroupBox_Milliohmmeter.Margin = new System.Windows.Forms.Padding(2);
            this.GroupBox_Milliohmmeter.Name = "GroupBox_Milliohmmeter";
            this.GroupBox_Milliohmmeter.Padding = new System.Windows.Forms.Padding(2);
            this.GroupBox_Milliohmmeter.Size = new System.Drawing.Size(158, 121);
            this.GroupBox_Milliohmmeter.TabIndex = 3;
            this.GroupBox_Milliohmmeter.TabStop = false;
            this.GroupBox_Milliohmmeter.Text = "Einstellungen Milliohmmeter";
            // 
            // ComboBox_BaudrateMilliohmmeter
            // 
            this.ComboBox_BaudrateMilliohmmeter.FormattingEnabled = true;
            this.ComboBox_BaudrateMilliohmmeter.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "57600",
            "115200"});
            this.ComboBox_BaudrateMilliohmmeter.Location = new System.Drawing.Point(60, 41);
            this.ComboBox_BaudrateMilliohmmeter.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBox_BaudrateMilliohmmeter.Name = "ComboBox_BaudrateMilliohmmeter";
            this.ComboBox_BaudrateMilliohmmeter.Size = new System.Drawing.Size(92, 21);
            this.ComboBox_BaudrateMilliohmmeter.TabIndex = 3;
            // 
            // GroupBox_Kraftsensor
            // 
            this.GroupBox_Kraftsensor.Controls.Add(this.label1);
            this.GroupBox_Kraftsensor.Controls.Add(this.ComboBox_ComPortKraftsensor);
            this.GroupBox_Kraftsensor.Location = new System.Drawing.Point(172, 10);
            this.GroupBox_Kraftsensor.Margin = new System.Windows.Forms.Padding(2);
            this.GroupBox_Kraftsensor.Name = "GroupBox_Kraftsensor";
            this.GroupBox_Kraftsensor.Padding = new System.Windows.Forms.Padding(2);
            this.GroupBox_Kraftsensor.Size = new System.Drawing.Size(158, 121);
            this.GroupBox_Kraftsensor.TabIndex = 4;
            this.GroupBox_Kraftsensor.TabStop = false;
            this.GroupBox_Kraftsensor.Text = "Einstellungen Kraftsensor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "COM-Port:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ComboBox_ComPortKraftsensor
            // 
            this.ComboBox_ComPortKraftsensor.FormattingEnabled = true;
            this.ComboBox_ComPortKraftsensor.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            this.ComboBox_ComPortKraftsensor.Location = new System.Drawing.Point(60, 17);
            this.ComboBox_ComPortKraftsensor.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBox_ComPortKraftsensor.Name = "ComboBox_ComPortKraftsensor";
            this.ComboBox_ComPortKraftsensor.Size = new System.Drawing.Size(92, 21);
            this.ComboBox_ComPortKraftsensor.TabIndex = 2;
            // 
            // GroupBox_Digitalanzeige
            // 
            this.GroupBox_Digitalanzeige.Controls.Add(this.ComboBox_BaudrateDigitalanzeige);
            this.GroupBox_Digitalanzeige.Controls.Add(this.label3);
            this.GroupBox_Digitalanzeige.Controls.Add(this.label4);
            this.GroupBox_Digitalanzeige.Controls.Add(this.ComboBox_ComPortDigitalanzeige);
            this.GroupBox_Digitalanzeige.Location = new System.Drawing.Point(334, 10);
            this.GroupBox_Digitalanzeige.Margin = new System.Windows.Forms.Padding(2);
            this.GroupBox_Digitalanzeige.Name = "GroupBox_Digitalanzeige";
            this.GroupBox_Digitalanzeige.Padding = new System.Windows.Forms.Padding(2);
            this.GroupBox_Digitalanzeige.Size = new System.Drawing.Size(158, 121);
            this.GroupBox_Digitalanzeige.TabIndex = 5;
            this.GroupBox_Digitalanzeige.TabStop = false;
            this.GroupBox_Digitalanzeige.Text = "Einstellungen Digitalanzeige";
            // 
            // ComboBox_BaudrateDigitalanzeige
            // 
            this.ComboBox_BaudrateDigitalanzeige.FormattingEnabled = true;
            this.ComboBox_BaudrateDigitalanzeige.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "57600",
            "115200"});
            this.ComboBox_BaudrateDigitalanzeige.Location = new System.Drawing.Point(60, 41);
            this.ComboBox_BaudrateDigitalanzeige.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBox_BaudrateDigitalanzeige.Name = "ComboBox_BaudrateDigitalanzeige";
            this.ComboBox_BaudrateDigitalanzeige.Size = new System.Drawing.Size(92, 21);
            this.ComboBox_BaudrateDigitalanzeige.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "COM-Port:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 44);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Baudrate:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ComboBox_ComPortDigitalanzeige
            // 
            this.ComboBox_ComPortDigitalanzeige.FormattingEnabled = true;
            this.ComboBox_ComPortDigitalanzeige.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            this.ComboBox_ComPortDigitalanzeige.Location = new System.Drawing.Point(60, 17);
            this.ComboBox_ComPortDigitalanzeige.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBox_ComPortDigitalanzeige.Name = "ComboBox_ComPortDigitalanzeige";
            this.ComboBox_ComPortDigitalanzeige.Size = new System.Drawing.Size(92, 21);
            this.ComboBox_ComPortDigitalanzeige.TabIndex = 2;
            // 
            // Button_Speichern
            // 
            this.Button_Speichern.Location = new System.Drawing.Point(9, 231);
            this.Button_Speichern.Margin = new System.Windows.Forms.Padding(2);
            this.Button_Speichern.Name = "Button_Speichern";
            this.Button_Speichern.Size = new System.Drawing.Size(69, 32);
            this.Button_Speichern.TabIndex = 6;
            this.Button_Speichern.Text = "Speichern";
            this.Button_Speichern.UseVisualStyleBackColor = true;
            this.Button_Speichern.Click += new System.EventHandler(this.Button_Speichern_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDown_HystereseTemperatur);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numericUpDown_AdresseTemperaturregler);
            this.groupBox1.Controls.Add(this.ComboBox_BaudrateTemperaturregler);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.ComboBox_ComPortTemperaturregler);
            this.groupBox1.Location = new System.Drawing.Point(496, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(170, 120);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Einstellungen Temperaturregler";
            // 
            // numericUpDown_HystereseTemperatur
            // 
            this.numericUpDown_HystereseTemperatur.Location = new System.Drawing.Point(60, 93);
            this.numericUpDown_HystereseTemperatur.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_HystereseTemperatur.Name = "numericUpDown_HystereseTemperatur";
            this.numericUpDown_HystereseTemperatur.Size = new System.Drawing.Size(92, 20);
            this.numericUpDown_HystereseTemperatur.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 95);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Hysterese:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 69);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Adresse:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numericUpDown_AdresseTemperaturregler
            // 
            this.numericUpDown_AdresseTemperaturregler.Location = new System.Drawing.Point(60, 67);
            this.numericUpDown_AdresseTemperaturregler.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_AdresseTemperaturregler.Name = "numericUpDown_AdresseTemperaturregler";
            this.numericUpDown_AdresseTemperaturregler.Size = new System.Drawing.Size(92, 20);
            this.numericUpDown_AdresseTemperaturregler.TabIndex = 7;
            // 
            // ComboBox_BaudrateTemperaturregler
            // 
            this.ComboBox_BaudrateTemperaturregler.FormattingEnabled = true;
            this.ComboBox_BaudrateTemperaturregler.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "57600",
            "115200"});
            this.ComboBox_BaudrateTemperaturregler.Location = new System.Drawing.Point(60, 41);
            this.ComboBox_BaudrateTemperaturregler.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBox_BaudrateTemperaturregler.Name = "ComboBox_BaudrateTemperaturregler";
            this.ComboBox_BaudrateTemperaturregler.Size = new System.Drawing.Size(92, 21);
            this.ComboBox_BaudrateTemperaturregler.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "COM-Port:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 44);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Baudrate:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ComboBox_ComPortTemperaturregler
            // 
            this.ComboBox_ComPortTemperaturregler.FormattingEnabled = true;
            this.ComboBox_ComPortTemperaturregler.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            this.ComboBox_ComPortTemperaturregler.Location = new System.Drawing.Point(60, 17);
            this.ComboBox_ComPortTemperaturregler.Margin = new System.Windows.Forms.Padding(2);
            this.ComboBox_ComPortTemperaturregler.Name = "ComboBox_ComPortTemperaturregler";
            this.ComboBox_ComPortTemperaturregler.Size = new System.Drawing.Size(92, 21);
            this.ComboBox_ComPortTemperaturregler.TabIndex = 2;
            // 
            // checkBox_ReibkraftmessungAktiv
            // 
            this.checkBox_ReibkraftmessungAktiv.AutoSize = true;
            this.checkBox_ReibkraftmessungAktiv.Location = new System.Drawing.Point(172, 136);
            this.checkBox_ReibkraftmessungAktiv.Name = "checkBox_ReibkraftmessungAktiv";
            this.checkBox_ReibkraftmessungAktiv.Size = new System.Drawing.Size(121, 30);
            this.checkBox_ReibkraftmessungAktiv.TabIndex = 7;
            this.checkBox_ReibkraftmessungAktiv.Text = "Reibkraftmessung\r\nstandardmäßig akitv";
            this.checkBox_ReibkraftmessungAktiv.UseVisualStyleBackColor = true;
            // 
            // Form_Einstellungen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 274);
            this.Controls.Add(this.checkBox_ReibkraftmessungAktiv);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Button_Speichern);
            this.Controls.Add(this.GroupBox_Digitalanzeige);
            this.Controls.Add(this.GroupBox_Kraftsensor);
            this.Controls.Add(this.GroupBox_Milliohmmeter);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form_Einstellungen";
            this.Text = "Einstellungen";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.GroupBox_Milliohmmeter.ResumeLayout(false);
            this.GroupBox_Milliohmmeter.PerformLayout();
            this.GroupBox_Kraftsensor.ResumeLayout(false);
            this.GroupBox_Kraftsensor.PerformLayout();
            this.GroupBox_Digitalanzeige.ResumeLayout(false);
            this.GroupBox_Digitalanzeige.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_HystereseTemperatur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AdresseTemperaturregler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_ComPort;
        private System.Windows.Forms.Label Label_Baudrate;
        private System.Windows.Forms.ComboBox ComboBox_ComPortMilliohmmeter;
        private System.Windows.Forms.GroupBox GroupBox_Milliohmmeter;
        private System.Windows.Forms.ComboBox ComboBox_BaudrateMilliohmmeter;
        private System.Windows.Forms.GroupBox GroupBox_Kraftsensor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ComboBox_ComPortKraftsensor;
        private System.Windows.Forms.GroupBox GroupBox_Digitalanzeige;
        private System.Windows.Forms.ComboBox ComboBox_BaudrateDigitalanzeige;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ComboBox_ComPortDigitalanzeige;
        private System.Windows.Forms.Button Button_Speichern;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox ComboBox_BaudrateTemperaturregler;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ComboBox_ComPortTemperaturregler;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown_AdresseTemperaturregler;
        private System.Windows.Forms.NumericUpDown numericUpDown_HystereseTemperatur;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox_ReibkraftmessungAktiv;
    }
}