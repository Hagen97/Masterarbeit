namespace User_Application_Reibkorrosionsprüfstand
{
    partial class Hauptfenster
    {

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hauptfenster));
            this.checkBox_PruefungMitTemperatur = new System.Windows.Forms.CheckBox();
            this.checkBox_ReibkraftMessen = new System.Windows.Forms.CheckBox();
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten = new System.Windows.Forms.NumericUpDown();
            this.Label_SollTemperaturHeizpatroneOben = new System.Windows.Forms.Label();
            this.Label_SollTemperaturHeizpatroneUnten = new System.Windows.Forms.Label();
            this.Label_IstTemperaturHeizpatroneOben = new System.Windows.Forms.Label();
            this.Label_IstTemperaturHeizpatroneUnten = new System.Windows.Forms.Label();
            this.Label_IstTemperaturHeizpatroneUntenValue = new System.Windows.Forms.Label();
            this.Label_IstTemperaturHeizpatroneObenValue = new System.Windows.Forms.Label();
            this.Label_Reibkraft = new System.Windows.Forms.Label();
            this.Label_ReibkraftValue = new System.Windows.Forms.Label();
            this.NumericUpDown_RGrenzwertEnde = new System.Windows.Forms.NumericUpDown();
            this.Label_RGrenzwert = new System.Windows.Forms.Label();
            this.NumericUpDown_GrenzwertZyklen = new System.Windows.Forms.NumericUpDown();
            this.Label_GrenzwertZyklen = new System.Windows.Forms.Label();
            this.groupBox_Temperatur = new System.Windows.Forms.GroupBox();
            this.button_WerteAnTemperaturreglerÜbertragen = new System.Windows.Forms.Button();
            this.button_ProbenAufheizen = new System.Windows.Forms.Button();
            this.numericUpDown_OffsetTemperaturProbeUnten = new System.Windows.Forms.NumericUpDown();
            this.Label_IstTemperaturProbeUnten = new System.Windows.Forms.Label();
            this.numericUpDown_OffsetTemperaturProbeOben = new System.Windows.Forms.NumericUpDown();
            this.Label_IstTemperaturProbeOben = new System.Windows.Forms.Label();
            this.groupBox_Reibkraftmessung = new System.Windows.Forms.GroupBox();
            this.chart_Widerstand = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox_Pruefungsende = new System.Windows.Forms.GroupBox();
            this.chart_Reibkraft = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Chart_IstTemperatur = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Button_Start = new System.Windows.Forms.Button();
            this.Button_Pausieren = new System.Windows.Forms.Button();
            this.Button_PruefungAbbrechen = new System.Windows.Forms.Button();
            this.Button_WiderstandPruefen = new System.Windows.Forms.Button();
            this.Button_HubEinstellen = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Button_LetztePruefbeschreibungLaden = new System.Windows.Forms.Button();
            this.Visualisierung_MotorEingeschaltet = new System.Windows.Forms.Button();
            this.groupBox_DatenZurPruefung = new System.Windows.Forms.GroupBox();
            this.TextBox_Beschreibung = new System.Windows.Forms.TextBox();
            this.TextBox_Schmierstoff = new System.Windows.Forms.TextBox();
            this.Label_Pruefer = new System.Windows.Forms.Label();
            this.TextBox_Auftragsnummer = new System.Windows.Forms.TextBox();
            this.Label_Artikelnummer = new System.Windows.Forms.Label();
            this.TextBox_Projektnummer = new System.Windows.Forms.TextBox();
            this.Label_Projektnummer = new System.Windows.Forms.Label();
            this.TextBox_Artikelnummer = new System.Windows.Forms.TextBox();
            this.Label_Auftragsnummer = new System.Windows.Forms.Label();
            this.TextBox_Pruefer = new System.Windows.Forms.TextBox();
            this.Label_Schmierstoff = new System.Windows.Forms.Label();
            this.Label_Beschreibung = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.NumericUpDown_Hub = new System.Windows.Forms.NumericUpDown();
            this.NumericUpDown_Kontaktkraft = new System.Windows.Forms.NumericUpDown();
            this.NumericUpDown_Federweg = new System.Windows.Forms.NumericUpDown();
            this.NumericUpDown_Wartezeit = new System.Windows.Forms.NumericUpDown();
            this.groupBox_Prüfungsdurchführung = new System.Windows.Forms.GroupBox();
            this.numericUpDown_SollTemperaturUnten = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_SollTemperaturOben = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Button_Zuruecksetzen = new System.Windows.Forms.Button();
            this.Label_AktuellerZyklus = new System.Windows.Forms.Label();
            this.Label_VerstricheneZeit = new System.Windows.Forms.Label();
            this.Label_Restzeit = new System.Windows.Forms.Label();
            this.Label_AktuellerZyklusValue = new System.Windows.Forms.Label();
            this.Label_VerstricheneZeitValue = new System.Windows.Forms.Label();
            this.Label_RestzeitValue = new System.Windows.Forms.Label();
            this.Informationen = new System.Windows.Forms.GroupBox();
            this.Visualisierung_HeizpatronenZugeschaltet = new System.Windows.Forms.Button();
            this.Visualisierung_Betriebsart = new System.Windows.Forms.Button();
            this.Visualisierung_VentileEingeschaltet = new System.Windows.Forms.Button();
            this.Visualisierung_MikroschalterMesstaster = new System.Windows.Forms.Button();
            this.Visualisierung_AbdeckungGeschlossen = new System.Windows.Forms.Button();
            this.Visualisierung_PneumatikdruckVorhanden = new System.Windows.Forms.Button();
            this.Visualisierung_NotAus = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.Button_MilliohmmeterVerbinden = new System.Windows.Forms.Button();
            this.Button_KraftsensorVerbinden = new System.Windows.Forms.Button();
            this.Button_DigitalanzeigeVerbinden = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.Button_TwinCATVerbinden = new System.Windows.Forms.Button();
            this.Button_TemperaturreglerVerbinden = new System.Windows.Forms.Button();
            this.TextBos_StatusKommunikation = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenu_Einstellungen = new System.Windows.Forms.ToolStripMenuItem();
            this.Panel_HubEinstellen = new System.Windows.Forms.Panel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.textBox_HubEinstellenAktuelleAufgabe = new System.Windows.Forms.TextBox();
            this.button_HubErmitteln = new System.Windows.Forms.Button();
            this.Button_HubEinstellenAbbrechen = new System.Windows.Forms.Button();
            this.Button_HubEinstellenSpeichern = new System.Windows.Forms.Button();
            this.Label_EingestellteHub = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.CheckBox_HubEingestellt = new System.Windows.Forms.CheckBox();
            this.Button_StartpunktAnfahren = new System.Windows.Forms.Button();
            this.Label_EinzustellendeHub = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Button_MesstasterNullen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.serialPort_Milliohmmeter = new System.IO.Ports.SerialPort(this.components);
            this.Panel_WiderstandPruefen = new System.Windows.Forms.Panel();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.Label_WiderstandWert = new System.Windows.Forms.Label();
            this.Label_Widerstand = new System.Windows.Forms.Label();
            this.Button_WiderstandAbbrechen = new System.Windows.Forms.Button();
            this.Button_WiderstandMessen = new System.Windows.Forms.Button();
            this.Button_WiderstandOk = new System.Windows.Forms.Button();
            this.serialPort_Digitalanzeige = new System.IO.Ports.SerialPort(this.components);
            this.saveFileDialog_Messung = new System.Windows.Forms.SaveFileDialog();
            this.serialPort_Temperaturregler = new System.IO.Ports.SerialPort(this.components);
            this.backgroundWorker_Start = new System.ComponentModel.BackgroundWorker();
            this.button_csvKopieren = new System.Windows.Forms.Button();
            this.backgroundWorker_StartpunktAnfahren = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker_Refresh = new System.ComponentModel.BackgroundWorker();
            this.button_ReibkraftMessen = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_RGrenzwertEnde)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_GrenzwertZyklen)).BeginInit();
            this.groupBox_Temperatur.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_OffsetTemperaturProbeUnten)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_OffsetTemperaturProbeOben)).BeginInit();
            this.groupBox_Reibkraftmessung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Widerstand)).BeginInit();
            this.groupBox_Pruefungsende.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Reibkraft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_IstTemperatur)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox_DatenZurPruefung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Hub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Kontaktkraft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Federweg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Wartezeit)).BeginInit();
            this.groupBox_Prüfungsdurchführung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SollTemperaturUnten)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SollTemperaturOben)).BeginInit();
            this.Informationen.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.Panel_HubEinstellen.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.Panel_WiderstandPruefen.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox_PruefungMitTemperatur
            // 
            this.checkBox_PruefungMitTemperatur.AutoSize = true;
            this.checkBox_PruefungMitTemperatur.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.checkBox_PruefungMitTemperatur.Checked = true;
            this.checkBox_PruefungMitTemperatur.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_PruefungMitTemperatur.Location = new System.Drawing.Point(4, 17);
            this.checkBox_PruefungMitTemperatur.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_PruefungMitTemperatur.Name = "checkBox_PruefungMitTemperatur";
            this.checkBox_PruefungMitTemperatur.Size = new System.Drawing.Size(136, 17);
            this.checkBox_PruefungMitTemperatur.TabIndex = 0;
            this.checkBox_PruefungMitTemperatur.Text = "Prüfung mit Temperatur";
            this.checkBox_PruefungMitTemperatur.UseVisualStyleBackColor = true;
            this.checkBox_PruefungMitTemperatur.CheckedChanged += new System.EventHandler(this.checkBox_PruefungMitTemperatur_CheckedChanged);
            // 
            // checkBox_ReibkraftMessen
            // 
            this.checkBox_ReibkraftMessen.AutoSize = true;
            this.checkBox_ReibkraftMessen.Checked = true;
            this.checkBox_ReibkraftMessen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ReibkraftMessen.Location = new System.Drawing.Point(4, 17);
            this.checkBox_ReibkraftMessen.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_ReibkraftMessen.Name = "checkBox_ReibkraftMessen";
            this.checkBox_ReibkraftMessen.Size = new System.Drawing.Size(108, 17);
            this.checkBox_ReibkraftMessen.TabIndex = 1;
            this.checkBox_ReibkraftMessen.Text = "Reibkraft messen";
            this.checkBox_ReibkraftMessen.UseVisualStyleBackColor = true;
            this.checkBox_ReibkraftMessen.CheckedChanged += new System.EventHandler(this.checkBox_ReibkraftMessen_CheckedChanged);
            // 
            // numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben
            // 
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben.Location = new System.Drawing.Point(144, 46);
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben.Name = "numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben";
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben.Size = new System.Drawing.Size(90, 20);
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben.TabIndex = 2;
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben.Value = new decimal(new int[] {
            85,
            0,
            0,
            0});
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben.ValueChanged += new System.EventHandler(this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben_ValueChanged);
            // 
            // numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten
            // 
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten.Location = new System.Drawing.Point(144, 103);
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten.Name = "numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten";
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten.Size = new System.Drawing.Size(90, 20);
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten.TabIndex = 3;
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten.Value = new decimal(new int[] {
            85,
            0,
            0,
            0});
            this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten.ValueChanged += new System.EventHandler(this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten_ValueChanged);
            // 
            // Label_SollTemperaturHeizpatroneOben
            // 
            this.Label_SollTemperaturHeizpatroneOben.AutoSize = true;
            this.Label_SollTemperaturHeizpatroneOben.Location = new System.Drawing.Point(141, 18);
            this.Label_SollTemperaturHeizpatroneOben.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_SollTemperaturHeizpatroneOben.Name = "Label_SollTemperaturHeizpatroneOben";
            this.Label_SollTemperaturHeizpatroneOben.Size = new System.Drawing.Size(113, 26);
            this.Label_SollTemperaturHeizpatroneOben.TabIndex = 4;
            this.Label_SollTemperaturHeizpatroneOben.Text = "Soll-Temperatur der\r\nHeizpatrone Oben [°C]";
            // 
            // Label_SollTemperaturHeizpatroneUnten
            // 
            this.Label_SollTemperaturHeizpatroneUnten.AutoSize = true;
            this.Label_SollTemperaturHeizpatroneUnten.Location = new System.Drawing.Point(141, 75);
            this.Label_SollTemperaturHeizpatroneUnten.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_SollTemperaturHeizpatroneUnten.Name = "Label_SollTemperaturHeizpatroneUnten";
            this.Label_SollTemperaturHeizpatroneUnten.Size = new System.Drawing.Size(116, 26);
            this.Label_SollTemperaturHeizpatroneUnten.TabIndex = 5;
            this.Label_SollTemperaturHeizpatroneUnten.Text = "Soll-Temperatur der\r\nHeizpatrone Unten [°C]";
            // 
            // Label_IstTemperaturHeizpatroneOben
            // 
            this.Label_IstTemperaturHeizpatroneOben.AutoSize = true;
            this.Label_IstTemperaturHeizpatroneOben.Location = new System.Drawing.Point(269, 18);
            this.Label_IstTemperaturHeizpatroneOben.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_IstTemperaturHeizpatroneOben.Name = "Label_IstTemperaturHeizpatroneOben";
            this.Label_IstTemperaturHeizpatroneOben.Size = new System.Drawing.Size(113, 26);
            this.Label_IstTemperaturHeizpatroneOben.TabIndex = 6;
            this.Label_IstTemperaturHeizpatroneOben.Text = "Ist-Temperatur der\r\nHeizpatrone Oben [°C]";
            // 
            // Label_IstTemperaturHeizpatroneUnten
            // 
            this.Label_IstTemperaturHeizpatroneUnten.AutoSize = true;
            this.Label_IstTemperaturHeizpatroneUnten.Location = new System.Drawing.Point(269, 75);
            this.Label_IstTemperaturHeizpatroneUnten.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_IstTemperaturHeizpatroneUnten.Name = "Label_IstTemperaturHeizpatroneUnten";
            this.Label_IstTemperaturHeizpatroneUnten.Size = new System.Drawing.Size(116, 26);
            this.Label_IstTemperaturHeizpatroneUnten.TabIndex = 7;
            this.Label_IstTemperaturHeizpatroneUnten.Text = "Ist-Temperatur der\r\nHeizpatrone Unten [°C]";
            // 
            // Label_IstTemperaturHeizpatroneUntenValue
            // 
            this.Label_IstTemperaturHeizpatroneUntenValue.AutoSize = true;
            this.Label_IstTemperaturHeizpatroneUntenValue.Location = new System.Drawing.Point(269, 105);
            this.Label_IstTemperaturHeizpatroneUntenValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_IstTemperaturHeizpatroneUntenValue.MinimumSize = new System.Drawing.Size(41, 13);
            this.Label_IstTemperaturHeizpatroneUntenValue.Name = "Label_IstTemperaturHeizpatroneUntenValue";
            this.Label_IstTemperaturHeizpatroneUntenValue.Size = new System.Drawing.Size(41, 13);
            this.Label_IstTemperaturHeizpatroneUntenValue.TabIndex = 8;
            this.Label_IstTemperaturHeizpatroneUntenValue.Text = "--- °C";
            // 
            // Label_IstTemperaturHeizpatroneObenValue
            // 
            this.Label_IstTemperaturHeizpatroneObenValue.AutoSize = true;
            this.Label_IstTemperaturHeizpatroneObenValue.Location = new System.Drawing.Point(269, 48);
            this.Label_IstTemperaturHeizpatroneObenValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_IstTemperaturHeizpatroneObenValue.MinimumSize = new System.Drawing.Size(41, 13);
            this.Label_IstTemperaturHeizpatroneObenValue.Name = "Label_IstTemperaturHeizpatroneObenValue";
            this.Label_IstTemperaturHeizpatroneObenValue.Size = new System.Drawing.Size(41, 13);
            this.Label_IstTemperaturHeizpatroneObenValue.TabIndex = 9;
            this.Label_IstTemperaturHeizpatroneObenValue.Text = "--- °C";
            // 
            // Label_Reibkraft
            // 
            this.Label_Reibkraft.AutoSize = true;
            this.Label_Reibkraft.Location = new System.Drawing.Point(141, 17);
            this.Label_Reibkraft.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_Reibkraft.Name = "Label_Reibkraft";
            this.Label_Reibkraft.Size = new System.Drawing.Size(67, 13);
            this.Label_Reibkraft.TabIndex = 10;
            this.Label_Reibkraft.Text = "Reibkraft [N]";
            // 
            // Label_ReibkraftValue
            // 
            this.Label_ReibkraftValue.AutoSize = true;
            this.Label_ReibkraftValue.Location = new System.Drawing.Point(141, 30);
            this.Label_ReibkraftValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_ReibkraftValue.MinimumSize = new System.Drawing.Size(41, 13);
            this.Label_ReibkraftValue.Name = "Label_ReibkraftValue";
            this.Label_ReibkraftValue.Size = new System.Drawing.Size(41, 13);
            this.Label_ReibkraftValue.TabIndex = 11;
            this.Label_ReibkraftValue.Text = "--- N";
            // 
            // NumericUpDown_RGrenzwertEnde
            // 
            this.NumericUpDown_RGrenzwertEnde.Location = new System.Drawing.Point(7, 30);
            this.NumericUpDown_RGrenzwertEnde.Margin = new System.Windows.Forms.Padding(2);
            this.NumericUpDown_RGrenzwertEnde.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NumericUpDown_RGrenzwertEnde.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericUpDown_RGrenzwertEnde.Name = "NumericUpDown_RGrenzwertEnde";
            this.NumericUpDown_RGrenzwertEnde.Size = new System.Drawing.Size(90, 20);
            this.NumericUpDown_RGrenzwertEnde.TabIndex = 12;
            this.NumericUpDown_RGrenzwertEnde.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label_RGrenzwert
            // 
            this.Label_RGrenzwert.AutoSize = true;
            this.Label_RGrenzwert.Location = new System.Drawing.Point(4, 15);
            this.Label_RGrenzwert.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_RGrenzwert.Name = "Label_RGrenzwert";
            this.Label_RGrenzwert.Size = new System.Drawing.Size(176, 13);
            this.Label_RGrenzwert.TabIndex = 13;
            this.Label_RGrenzwert.Text = "Widerstand Prüfungsende [mOhm]:*";
            // 
            // NumericUpDown_GrenzwertZyklen
            // 
            this.NumericUpDown_GrenzwertZyklen.Location = new System.Drawing.Point(7, 76);
            this.NumericUpDown_GrenzwertZyklen.Margin = new System.Windows.Forms.Padding(2);
            this.NumericUpDown_GrenzwertZyklen.Maximum = new decimal(new int[] {
            90000000,
            0,
            0,
            0});
            this.NumericUpDown_GrenzwertZyklen.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericUpDown_GrenzwertZyklen.Name = "NumericUpDown_GrenzwertZyklen";
            this.NumericUpDown_GrenzwertZyklen.Size = new System.Drawing.Size(90, 20);
            this.NumericUpDown_GrenzwertZyklen.TabIndex = 14;
            this.NumericUpDown_GrenzwertZyklen.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label_GrenzwertZyklen
            // 
            this.Label_GrenzwertZyklen.AutoSize = true;
            this.Label_GrenzwertZyklen.Location = new System.Drawing.Point(4, 60);
            this.Label_GrenzwertZyklen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_GrenzwertZyklen.Name = "Label_GrenzwertZyklen";
            this.Label_GrenzwertZyklen.Size = new System.Drawing.Size(115, 13);
            this.Label_GrenzwertZyklen.TabIndex = 15;
            this.Label_GrenzwertZyklen.Text = "Zyklen Prüfungsende:*";
            this.Label_GrenzwertZyklen.Click += new System.EventHandler(this.Label_GrenzwertZyklen_Click);
            // 
            // groupBox_Temperatur
            // 
            this.groupBox_Temperatur.Controls.Add(this.button_WerteAnTemperaturreglerÜbertragen);
            this.groupBox_Temperatur.Controls.Add(this.button_ProbenAufheizen);
            this.groupBox_Temperatur.Controls.Add(this.numericUpDown_OffsetTemperaturProbeUnten);
            this.groupBox_Temperatur.Controls.Add(this.Label_IstTemperaturProbeUnten);
            this.groupBox_Temperatur.Controls.Add(this.numericUpDown_OffsetTemperaturProbeOben);
            this.groupBox_Temperatur.Controls.Add(this.Label_IstTemperaturProbeOben);
            this.groupBox_Temperatur.Controls.Add(this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten);
            this.groupBox_Temperatur.Controls.Add(this.checkBox_PruefungMitTemperatur);
            this.groupBox_Temperatur.Controls.Add(this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben);
            this.groupBox_Temperatur.Controls.Add(this.Label_SollTemperaturHeizpatroneOben);
            this.groupBox_Temperatur.Controls.Add(this.Label_SollTemperaturHeizpatroneUnten);
            this.groupBox_Temperatur.Controls.Add(this.Label_IstTemperaturHeizpatroneOben);
            this.groupBox_Temperatur.Controls.Add(this.Label_IstTemperaturHeizpatroneUnten);
            this.groupBox_Temperatur.Controls.Add(this.Label_IstTemperaturHeizpatroneObenValue);
            this.groupBox_Temperatur.Controls.Add(this.Label_IstTemperaturHeizpatroneUntenValue);
            this.groupBox_Temperatur.Location = new System.Drawing.Point(853, 499);
            this.groupBox_Temperatur.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Temperatur.Name = "groupBox_Temperatur";
            this.groupBox_Temperatur.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Temperatur.Size = new System.Drawing.Size(743, 127);
            this.groupBox_Temperatur.TabIndex = 16;
            this.groupBox_Temperatur.TabStop = false;
            this.groupBox_Temperatur.Text = "Messung unter Einfluss von Temperatur";
            // 
            // button_WerteAnTemperaturreglerÜbertragen
            // 
            this.button_WerteAnTemperaturreglerÜbertragen.Location = new System.Drawing.Point(4, 38);
            this.button_WerteAnTemperaturreglerÜbertragen.Margin = new System.Windows.Forms.Padding(2);
            this.button_WerteAnTemperaturreglerÜbertragen.Name = "button_WerteAnTemperaturreglerÜbertragen";
            this.button_WerteAnTemperaturreglerÜbertragen.Size = new System.Drawing.Size(79, 37);
            this.button_WerteAnTemperaturreglerÜbertragen.TabIndex = 55;
            this.button_WerteAnTemperaturreglerÜbertragen.Text = "Soll-Werte übertragen";
            this.button_WerteAnTemperaturreglerÜbertragen.UseVisualStyleBackColor = true;
            this.button_WerteAnTemperaturreglerÜbertragen.Click += new System.EventHandler(this.button_WerteAnTemperaturreglerÜbertragen_Click);
            // 
            // button_ProbenAufheizen
            // 
            this.button_ProbenAufheizen.Enabled = false;
            this.button_ProbenAufheizen.Location = new System.Drawing.Point(4, 86);
            this.button_ProbenAufheizen.Margin = new System.Windows.Forms.Padding(2);
            this.button_ProbenAufheizen.Name = "button_ProbenAufheizen";
            this.button_ProbenAufheizen.Size = new System.Drawing.Size(61, 37);
            this.button_ProbenAufheizen.TabIndex = 54;
            this.button_ProbenAufheizen.Text = "Proben aufheizen";
            this.button_ProbenAufheizen.UseVisualStyleBackColor = true;
            this.button_ProbenAufheizen.Click += new System.EventHandler(this.button_ProbenAufheizen_Click);
            // 
            // numericUpDown_OffsetTemperaturProbeUnten
            // 
            this.numericUpDown_OffsetTemperaturProbeUnten.Location = new System.Drawing.Point(406, 103);
            this.numericUpDown_OffsetTemperaturProbeUnten.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_OffsetTemperaturProbeUnten.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_OffsetTemperaturProbeUnten.Name = "numericUpDown_OffsetTemperaturProbeUnten";
            this.numericUpDown_OffsetTemperaturProbeUnten.Size = new System.Drawing.Size(90, 20);
            this.numericUpDown_OffsetTemperaturProbeUnten.TabIndex = 13;
            // 
            // Label_IstTemperaturProbeUnten
            // 
            this.Label_IstTemperaturProbeUnten.AutoSize = true;
            this.Label_IstTemperaturProbeUnten.Location = new System.Drawing.Point(403, 75);
            this.Label_IstTemperaturProbeUnten.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_IstTemperaturProbeUnten.Name = "Label_IstTemperaturProbeUnten";
            this.Label_IstTemperaturProbeUnten.Size = new System.Drawing.Size(105, 26);
            this.Label_IstTemperaturProbeUnten.TabIndex = 12;
            this.Label_IstTemperaturProbeUnten.Text = "Offset-Temperatur\r\nder Probe Unten [°C]";
            // 
            // numericUpDown_OffsetTemperaturProbeOben
            // 
            this.numericUpDown_OffsetTemperaturProbeOben.Location = new System.Drawing.Point(406, 46);
            this.numericUpDown_OffsetTemperaturProbeOben.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_OffsetTemperaturProbeOben.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_OffsetTemperaturProbeOben.Name = "numericUpDown_OffsetTemperaturProbeOben";
            this.numericUpDown_OffsetTemperaturProbeOben.Size = new System.Drawing.Size(90, 20);
            this.numericUpDown_OffsetTemperaturProbeOben.TabIndex = 11;
            // 
            // Label_IstTemperaturProbeOben
            // 
            this.Label_IstTemperaturProbeOben.AutoSize = true;
            this.Label_IstTemperaturProbeOben.Location = new System.Drawing.Point(403, 18);
            this.Label_IstTemperaturProbeOben.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_IstTemperaturProbeOben.Name = "Label_IstTemperaturProbeOben";
            this.Label_IstTemperaturProbeOben.Size = new System.Drawing.Size(102, 26);
            this.Label_IstTemperaturProbeOben.TabIndex = 10;
            this.Label_IstTemperaturProbeOben.Text = "Offset-Temperatur\r\nder Probe Oben [°C]";
            // 
            // groupBox_Reibkraftmessung
            // 
            this.groupBox_Reibkraftmessung.Controls.Add(this.checkBox_ReibkraftMessen);
            this.groupBox_Reibkraftmessung.Controls.Add(this.Label_Reibkraft);
            this.groupBox_Reibkraftmessung.Controls.Add(this.Label_ReibkraftValue);
            this.groupBox_Reibkraftmessung.Location = new System.Drawing.Point(9, 576);
            this.groupBox_Reibkraftmessung.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Reibkraftmessung.Name = "groupBox_Reibkraftmessung";
            this.groupBox_Reibkraftmessung.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Reibkraftmessung.Size = new System.Drawing.Size(226, 50);
            this.groupBox_Reibkraftmessung.TabIndex = 17;
            this.groupBox_Reibkraftmessung.TabStop = false;
            this.groupBox_Reibkraftmessung.Text = "Reibkraftmessung";
            // 
            // chart_Widerstand
            // 
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.Title = "Zyklen";
            chartArea1.AxisY.Interval = 50D;
            chartArea1.AxisY.Maximum = 300D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.AxisY.Title = "Widerstand [mOhm]";
            chartArea1.Name = "ChartArea1";
            this.chart_Widerstand.ChartAreas.Add(chartArea1);
            this.chart_Widerstand.Location = new System.Drawing.Point(853, 177);
            this.chart_Widerstand.Margin = new System.Windows.Forms.Padding(2);
            this.chart_Widerstand.Name = "chart_Widerstand";
            this.chart_Widerstand.Size = new System.Drawing.Size(820, 270);
            this.chart_Widerstand.TabIndex = 18;
            this.chart_Widerstand.Text = "chart_Widerstand";
            title1.Name = "Widerstand";
            title1.Text = "Widerstand";
            this.chart_Widerstand.Titles.Add(title1);
            // 
            // groupBox_Pruefungsende
            // 
            this.groupBox_Pruefungsende.Controls.Add(this.Label_RGrenzwert);
            this.groupBox_Pruefungsende.Controls.Add(this.NumericUpDown_RGrenzwertEnde);
            this.groupBox_Pruefungsende.Controls.Add(this.Label_GrenzwertZyklen);
            this.groupBox_Pruefungsende.Controls.Add(this.NumericUpDown_GrenzwertZyklen);
            this.groupBox_Pruefungsende.Location = new System.Drawing.Point(269, 347);
            this.groupBox_Pruefungsende.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Pruefungsende.Name = "groupBox_Pruefungsende";
            this.groupBox_Pruefungsende.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Pruefungsende.Size = new System.Drawing.Size(201, 100);
            this.groupBox_Pruefungsende.TabIndex = 19;
            this.groupBox_Pruefungsende.TabStop = false;
            this.groupBox_Pruefungsende.Text = "Bedingungen Prüfungsende";
            // 
            // chart_Reibkraft
            // 
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisX.Title = "Zyklen";
            chartArea2.AxisY.Interval = 1D;
            chartArea2.AxisY.IsStartedFromZero = false;
            chartArea2.AxisY.Maximum = 6D;
            chartArea2.AxisY.Minimum = -6D;
            chartArea2.AxisY.ScaleBreakStyle.MaxNumberOfBreaks = 5;
            chartArea2.AxisY.ScaleBreakStyle.Spacing = 5D;
            chartArea2.AxisY.ScaleBreakStyle.StartFromZero = System.Windows.Forms.DataVisualization.Charting.StartFromZero.Yes;
            chartArea2.AxisY.Title = "Reibkraft [N]";
            chartArea2.Name = "ChartArea1";
            this.chart_Reibkraft.ChartAreas.Add(chartArea2);
            legend1.Name = "Legend1";
            this.chart_Reibkraft.Legends.Add(legend1);
            this.chart_Reibkraft.Location = new System.Drawing.Point(9, 630);
            this.chart_Reibkraft.Margin = new System.Windows.Forms.Padding(2);
            this.chart_Reibkraft.Name = "chart_Reibkraft";
            this.chart_Reibkraft.Size = new System.Drawing.Size(820, 340);
            this.chart_Reibkraft.TabIndex = 20;
            this.chart_Reibkraft.Text = "chart_Reibkraft";
            title2.Name = "Reibkraft";
            title2.Text = "Mittlere Reibkraft";
            this.chart_Reibkraft.Titles.Add(title2);
            this.chart_Reibkraft.Click += new System.EventHandler(this.chart_Reibkraft_Click);
            // 
            // Chart_IstTemperatur
            // 
            chartArea3.AxisX.Minimum = 0D;
            chartArea3.AxisX.Title = "Zyklen";
            chartArea3.AxisY.Interval = 25D;
            chartArea3.AxisY.Maximum = 150D;
            chartArea3.AxisY.Minimum = 0D;
            chartArea3.AxisY.Title = "Temperatur [°C]";
            chartArea3.Name = "ChartArea1";
            this.Chart_IstTemperatur.ChartAreas.Add(chartArea3);
            legend2.Name = "Legend1";
            this.Chart_IstTemperatur.Legends.Add(legend2);
            this.Chart_IstTemperatur.Location = new System.Drawing.Point(853, 630);
            this.Chart_IstTemperatur.Margin = new System.Windows.Forms.Padding(2);
            this.Chart_IstTemperatur.Name = "Chart_IstTemperatur";
            this.Chart_IstTemperatur.Size = new System.Drawing.Size(820, 340);
            this.Chart_IstTemperatur.TabIndex = 21;
            this.Chart_IstTemperatur.Text = "Chart_IstTemperatur";
            title3.Name = "Title1";
            title3.Text = "Ist-Temperatur";
            this.Chart_IstTemperatur.Titles.Add(title3);
            // 
            // Button_Start
            // 
            this.Button_Start.Location = new System.Drawing.Point(9, 464);
            this.Button_Start.Margin = new System.Windows.Forms.Padding(2);
            this.Button_Start.Name = "Button_Start";
            this.Button_Start.Size = new System.Drawing.Size(56, 37);
            this.Button_Start.TabIndex = 22;
            this.Button_Start.Text = "Prüfung starten";
            this.Button_Start.UseVisualStyleBackColor = true;
            this.Button_Start.Click += new System.EventHandler(this.Button_Start_Click);
            // 
            // Button_Pausieren
            // 
            this.Button_Pausieren.Enabled = false;
            this.Button_Pausieren.Location = new System.Drawing.Point(139, 464);
            this.Button_Pausieren.Margin = new System.Windows.Forms.Padding(2);
            this.Button_Pausieren.Name = "Button_Pausieren";
            this.Button_Pausieren.Size = new System.Drawing.Size(61, 37);
            this.Button_Pausieren.TabIndex = 23;
            this.Button_Pausieren.Text = "Prüfung pausieren";
            this.Button_Pausieren.UseVisualStyleBackColor = true;
            this.Button_Pausieren.Click += new System.EventHandler(this.Button_Pausieren_Click);
            // 
            // Button_PruefungAbbrechen
            // 
            this.Button_PruefungAbbrechen.Enabled = false;
            this.Button_PruefungAbbrechen.Location = new System.Drawing.Point(69, 464);
            this.Button_PruefungAbbrechen.Margin = new System.Windows.Forms.Padding(2);
            this.Button_PruefungAbbrechen.Name = "Button_PruefungAbbrechen";
            this.Button_PruefungAbbrechen.Size = new System.Drawing.Size(66, 37);
            this.Button_PruefungAbbrechen.TabIndex = 24;
            this.Button_PruefungAbbrechen.Text = "Prüfung abbrechen";
            this.Button_PruefungAbbrechen.UseVisualStyleBackColor = true;
            this.Button_PruefungAbbrechen.Click += new System.EventHandler(this.Button_PruefungAbbrechen_Click);
            // 
            // Button_WiderstandPruefen
            // 
            this.Button_WiderstandPruefen.Enabled = false;
            this.Button_WiderstandPruefen.Location = new System.Drawing.Point(232, 17);
            this.Button_WiderstandPruefen.Margin = new System.Windows.Forms.Padding(2);
            this.Button_WiderstandPruefen.Name = "Button_WiderstandPruefen";
            this.Button_WiderstandPruefen.Size = new System.Drawing.Size(69, 37);
            this.Button_WiderstandPruefen.TabIndex = 25;
            this.Button_WiderstandPruefen.Text = "Widerstand überprüfen";
            this.Button_WiderstandPruefen.UseVisualStyleBackColor = true;
            this.Button_WiderstandPruefen.Click += new System.EventHandler(this.Button_WiderstandPruefen_Click);
            // 
            // Button_HubEinstellen
            // 
            this.Button_HubEinstellen.Enabled = false;
            this.Button_HubEinstellen.Location = new System.Drawing.Point(160, 17);
            this.Button_HubEinstellen.Margin = new System.Windows.Forms.Padding(2);
            this.Button_HubEinstellen.Name = "Button_HubEinstellen";
            this.Button_HubEinstellen.Size = new System.Drawing.Size(68, 37);
            this.Button_HubEinstellen.TabIndex = 26;
            this.Button_HubEinstellen.Text = "Hub einstellen";
            this.Button_HubEinstellen.UseVisualStyleBackColor = true;
            this.Button_HubEinstellen.Click += new System.EventHandler(this.Button_HubEinstellen_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Button_LetztePruefbeschreibungLaden);
            this.groupBox4.Controls.Add(this.Button_HubEinstellen);
            this.groupBox4.Controls.Add(this.Button_WiderstandPruefen);
            this.groupBox4.Location = new System.Drawing.Point(474, 44);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(306, 63);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Prüfung einrichten";
            // 
            // Button_LetztePruefbeschreibungLaden
            // 
            this.Button_LetztePruefbeschreibungLaden.Location = new System.Drawing.Point(4, 17);
            this.Button_LetztePruefbeschreibungLaden.Margin = new System.Windows.Forms.Padding(2);
            this.Button_LetztePruefbeschreibungLaden.Name = "Button_LetztePruefbeschreibungLaden";
            this.Button_LetztePruefbeschreibungLaden.Size = new System.Drawing.Size(152, 37);
            this.Button_LetztePruefbeschreibungLaden.TabIndex = 27;
            this.Button_LetztePruefbeschreibungLaden.Text = "Letzte Prüfungsbeschreibung laden";
            this.Button_LetztePruefbeschreibungLaden.UseVisualStyleBackColor = true;
            this.Button_LetztePruefbeschreibungLaden.Click += new System.EventHandler(this.Button_LetztePruefbeschreibungLaden_Click);
            // 
            // Visualisierung_MotorEingeschaltet
            // 
            this.Visualisierung_MotorEingeschaltet.Enabled = false;
            this.Visualisierung_MotorEingeschaltet.Location = new System.Drawing.Point(438, 17);
            this.Visualisierung_MotorEingeschaltet.Margin = new System.Windows.Forms.Padding(2);
            this.Visualisierung_MotorEingeschaltet.Name = "Visualisierung_MotorEingeschaltet";
            this.Visualisierung_MotorEingeschaltet.Size = new System.Drawing.Size(69, 37);
            this.Visualisierung_MotorEingeschaltet.TabIndex = 28;
            this.Visualisierung_MotorEingeschaltet.Text = "Motor eingeschaltet";
            this.Visualisierung_MotorEingeschaltet.UseVisualStyleBackColor = true;
            this.Visualisierung_MotorEingeschaltet.Click += new System.EventHandler(this.Button_MotorEinschalten_Click);
            // 
            // groupBox_DatenZurPruefung
            // 
            this.groupBox_DatenZurPruefung.Controls.Add(this.TextBox_Beschreibung);
            this.groupBox_DatenZurPruefung.Controls.Add(this.TextBox_Schmierstoff);
            this.groupBox_DatenZurPruefung.Controls.Add(this.Label_Pruefer);
            this.groupBox_DatenZurPruefung.Controls.Add(this.TextBox_Auftragsnummer);
            this.groupBox_DatenZurPruefung.Controls.Add(this.Label_Artikelnummer);
            this.groupBox_DatenZurPruefung.Controls.Add(this.TextBox_Projektnummer);
            this.groupBox_DatenZurPruefung.Controls.Add(this.Label_Projektnummer);
            this.groupBox_DatenZurPruefung.Controls.Add(this.TextBox_Artikelnummer);
            this.groupBox_DatenZurPruefung.Controls.Add(this.Label_Auftragsnummer);
            this.groupBox_DatenZurPruefung.Controls.Add(this.TextBox_Pruefer);
            this.groupBox_DatenZurPruefung.Controls.Add(this.Label_Schmierstoff);
            this.groupBox_DatenZurPruefung.Controls.Add(this.Label_Beschreibung);
            this.groupBox_DatenZurPruefung.Location = new System.Drawing.Point(9, 154);
            this.groupBox_DatenZurPruefung.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_DatenZurPruefung.Name = "groupBox_DatenZurPruefung";
            this.groupBox_DatenZurPruefung.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_DatenZurPruefung.Size = new System.Drawing.Size(256, 266);
            this.groupBox_DatenZurPruefung.TabIndex = 28;
            this.groupBox_DatenZurPruefung.TabStop = false;
            this.groupBox_DatenZurPruefung.Text = "Daten zur Prüfung";
            // 
            // TextBox_Beschreibung
            // 
            this.TextBox_Beschreibung.Location = new System.Drawing.Point(96, 167);
            this.TextBox_Beschreibung.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox_Beschreibung.Multiline = true;
            this.TextBox_Beschreibung.Name = "TextBox_Beschreibung";
            this.TextBox_Beschreibung.Size = new System.Drawing.Size(151, 90);
            this.TextBox_Beschreibung.TabIndex = 40;
            // 
            // TextBox_Schmierstoff
            // 
            this.TextBox_Schmierstoff.Location = new System.Drawing.Point(96, 108);
            this.TextBox_Schmierstoff.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox_Schmierstoff.Multiline = true;
            this.TextBox_Schmierstoff.Name = "TextBox_Schmierstoff";
            this.TextBox_Schmierstoff.Size = new System.Drawing.Size(151, 54);
            this.TextBox_Schmierstoff.TabIndex = 39;
            // 
            // Label_Pruefer
            // 
            this.Label_Pruefer.AutoSize = true;
            this.Label_Pruefer.Location = new System.Drawing.Point(54, 20);
            this.Label_Pruefer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_Pruefer.Name = "Label_Pruefer";
            this.Label_Pruefer.Size = new System.Drawing.Size(42, 13);
            this.Label_Pruefer.TabIndex = 29;
            this.Label_Pruefer.Text = "Prüfer:*";
            this.Label_Pruefer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TextBox_Auftragsnummer
            // 
            this.TextBox_Auftragsnummer.Location = new System.Drawing.Point(96, 85);
            this.TextBox_Auftragsnummer.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox_Auftragsnummer.Name = "TextBox_Auftragsnummer";
            this.TextBox_Auftragsnummer.Size = new System.Drawing.Size(151, 20);
            this.TextBox_Auftragsnummer.TabIndex = 38;
            // 
            // Label_Artikelnummer
            // 
            this.Label_Artikelnummer.AutoSize = true;
            this.Label_Artikelnummer.Location = new System.Drawing.Point(16, 42);
            this.Label_Artikelnummer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_Artikelnummer.Name = "Label_Artikelnummer";
            this.Label_Artikelnummer.Size = new System.Drawing.Size(80, 13);
            this.Label_Artikelnummer.TabIndex = 30;
            this.Label_Artikelnummer.Text = "Artikelnummer:*";
            this.Label_Artikelnummer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.Label_Artikelnummer.Click += new System.EventHandler(this.Label_Artikelnummer_Click);
            // 
            // TextBox_Projektnummer
            // 
            this.TextBox_Projektnummer.Location = new System.Drawing.Point(96, 63);
            this.TextBox_Projektnummer.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox_Projektnummer.Name = "TextBox_Projektnummer";
            this.TextBox_Projektnummer.Size = new System.Drawing.Size(151, 20);
            this.TextBox_Projektnummer.TabIndex = 37;
            // 
            // Label_Projektnummer
            // 
            this.Label_Projektnummer.AutoSize = true;
            this.Label_Projektnummer.Location = new System.Drawing.Point(16, 65);
            this.Label_Projektnummer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_Projektnummer.Name = "Label_Projektnummer";
            this.Label_Projektnummer.Size = new System.Drawing.Size(80, 13);
            this.Label_Projektnummer.TabIndex = 31;
            this.Label_Projektnummer.Text = "Projektnummer:";
            this.Label_Projektnummer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TextBox_Artikelnummer
            // 
            this.TextBox_Artikelnummer.Location = new System.Drawing.Point(96, 40);
            this.TextBox_Artikelnummer.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox_Artikelnummer.Name = "TextBox_Artikelnummer";
            this.TextBox_Artikelnummer.Size = new System.Drawing.Size(151, 20);
            this.TextBox_Artikelnummer.TabIndex = 36;
            // 
            // Label_Auftragsnummer
            // 
            this.Label_Auftragsnummer.AutoSize = true;
            this.Label_Auftragsnummer.Location = new System.Drawing.Point(8, 88);
            this.Label_Auftragsnummer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_Auftragsnummer.Name = "Label_Auftragsnummer";
            this.Label_Auftragsnummer.Size = new System.Drawing.Size(90, 13);
            this.Label_Auftragsnummer.TabIndex = 32;
            this.Label_Auftragsnummer.Text = "Auftragsnummer:*";
            this.Label_Auftragsnummer.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TextBox_Pruefer
            // 
            this.TextBox_Pruefer.Location = new System.Drawing.Point(96, 17);
            this.TextBox_Pruefer.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox_Pruefer.Name = "TextBox_Pruefer";
            this.TextBox_Pruefer.Size = new System.Drawing.Size(151, 20);
            this.TextBox_Pruefer.TabIndex = 35;
            // 
            // Label_Schmierstoff
            // 
            this.Label_Schmierstoff.AutoSize = true;
            this.Label_Schmierstoff.Location = new System.Drawing.Point(29, 110);
            this.Label_Schmierstoff.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_Schmierstoff.Name = "Label_Schmierstoff";
            this.Label_Schmierstoff.Size = new System.Drawing.Size(68, 13);
            this.Label_Schmierstoff.TabIndex = 33;
            this.Label_Schmierstoff.Text = "Schmierstoff:";
            this.Label_Schmierstoff.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Label_Beschreibung
            // 
            this.Label_Beschreibung.AutoSize = true;
            this.Label_Beschreibung.Location = new System.Drawing.Point(22, 169);
            this.Label_Beschreibung.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_Beschreibung.Name = "Label_Beschreibung";
            this.Label_Beschreibung.Size = new System.Drawing.Size(75, 13);
            this.Label_Beschreibung.TabIndex = 34;
            this.Label_Beschreibung.Text = "Beschreibung:";
            this.Label_Beschreibung.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 19);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Hub/Weg [µm]:*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 40);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Kontaktkraft [N]:*";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 63);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Federweg [µm]:*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 87);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Wartezeit [ms]:*";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // NumericUpDown_Hub
            // 
            this.NumericUpDown_Hub.Location = new System.Drawing.Point(107, 17);
            this.NumericUpDown_Hub.Margin = new System.Windows.Forms.Padding(2);
            this.NumericUpDown_Hub.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            this.NumericUpDown_Hub.Name = "NumericUpDown_Hub";
            this.NumericUpDown_Hub.Size = new System.Drawing.Size(90, 20);
            this.NumericUpDown_Hub.TabIndex = 16;
            // 
            // NumericUpDown_Kontaktkraft
            // 
            this.NumericUpDown_Kontaktkraft.DecimalPlaces = 3;
            this.NumericUpDown_Kontaktkraft.Location = new System.Drawing.Point(107, 40);
            this.NumericUpDown_Kontaktkraft.Margin = new System.Windows.Forms.Padding(2);
            this.NumericUpDown_Kontaktkraft.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            this.NumericUpDown_Kontaktkraft.Name = "NumericUpDown_Kontaktkraft";
            this.NumericUpDown_Kontaktkraft.Size = new System.Drawing.Size(90, 20);
            this.NumericUpDown_Kontaktkraft.TabIndex = 34;
            this.NumericUpDown_Kontaktkraft.ThousandsSeparator = true;
            // 
            // NumericUpDown_Federweg
            // 
            this.NumericUpDown_Federweg.Location = new System.Drawing.Point(107, 63);
            this.NumericUpDown_Federweg.Margin = new System.Windows.Forms.Padding(2);
            this.NumericUpDown_Federweg.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            this.NumericUpDown_Federweg.Name = "NumericUpDown_Federweg";
            this.NumericUpDown_Federweg.Size = new System.Drawing.Size(90, 20);
            this.NumericUpDown_Federweg.TabIndex = 35;
            // 
            // NumericUpDown_Wartezeit
            // 
            this.NumericUpDown_Wartezeit.Location = new System.Drawing.Point(107, 85);
            this.NumericUpDown_Wartezeit.Margin = new System.Windows.Forms.Padding(2);
            this.NumericUpDown_Wartezeit.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            this.NumericUpDown_Wartezeit.Name = "NumericUpDown_Wartezeit";
            this.NumericUpDown_Wartezeit.Size = new System.Drawing.Size(90, 20);
            this.NumericUpDown_Wartezeit.TabIndex = 36;
            // 
            // groupBox_Prüfungsdurchführung
            // 
            this.groupBox_Prüfungsdurchführung.Controls.Add(this.numericUpDown_SollTemperaturUnten);
            this.groupBox_Prüfungsdurchführung.Controls.Add(this.numericUpDown_SollTemperaturOben);
            this.groupBox_Prüfungsdurchführung.Controls.Add(this.label4);
            this.groupBox_Prüfungsdurchführung.Controls.Add(this.label3);
            this.groupBox_Prüfungsdurchführung.Controls.Add(this.NumericUpDown_Hub);
            this.groupBox_Prüfungsdurchführung.Controls.Add(this.NumericUpDown_Wartezeit);
            this.groupBox_Prüfungsdurchführung.Controls.Add(this.label5);
            this.groupBox_Prüfungsdurchführung.Controls.Add(this.NumericUpDown_Federweg);
            this.groupBox_Prüfungsdurchführung.Controls.Add(this.label6);
            this.groupBox_Prüfungsdurchführung.Controls.Add(this.NumericUpDown_Kontaktkraft);
            this.groupBox_Prüfungsdurchführung.Controls.Add(this.label7);
            this.groupBox_Prüfungsdurchführung.Controls.Add(this.label8);
            this.groupBox_Prüfungsdurchführung.Location = new System.Drawing.Point(269, 154);
            this.groupBox_Prüfungsdurchführung.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Prüfungsdurchführung.Name = "groupBox_Prüfungsdurchführung";
            this.groupBox_Prüfungsdurchführung.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Prüfungsdurchführung.Size = new System.Drawing.Size(201, 189);
            this.groupBox_Prüfungsdurchführung.TabIndex = 37;
            this.groupBox_Prüfungsdurchführung.TabStop = false;
            this.groupBox_Prüfungsdurchführung.Text = "Prüfungsdurchführung";
            // 
            // numericUpDown_SollTemperaturUnten
            // 
            this.numericUpDown_SollTemperaturUnten.Location = new System.Drawing.Point(107, 147);
            this.numericUpDown_SollTemperaturUnten.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_SollTemperaturUnten.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_SollTemperaturUnten.Name = "numericUpDown_SollTemperaturUnten";
            this.numericUpDown_SollTemperaturUnten.Size = new System.Drawing.Size(90, 20);
            this.numericUpDown_SollTemperaturUnten.TabIndex = 40;
            this.numericUpDown_SollTemperaturUnten.ValueChanged += new System.EventHandler(this.numericUpDown_SollTemperaturUnten_ValueChanged);
            // 
            // numericUpDown_SollTemperaturOben
            // 
            this.numericUpDown_SollTemperaturOben.Location = new System.Drawing.Point(107, 117);
            this.numericUpDown_SollTemperaturOben.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_SollTemperaturOben.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_SollTemperaturOben.Name = "numericUpDown_SollTemperaturOben";
            this.numericUpDown_SollTemperaturOben.Size = new System.Drawing.Size(90, 20);
            this.numericUpDown_SollTemperaturOben.TabIndex = 39;
            this.numericUpDown_SollTemperaturOben.ValueChanged += new System.EventHandler(this.numericUpDown_SollTemperaturOben_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 141);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 26);
            this.label4.TabIndex = 38;
            this.label4.Text = "Soll-Temperatur\r\nUntere Probe [°C]:*";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 111);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 26);
            this.label3.TabIndex = 37;
            this.label3.Text = "Soll-Temperatur\r\nObere Probe [°C]:*";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Button_Zuruecksetzen
            // 
            this.Button_Zuruecksetzen.Enabled = false;
            this.Button_Zuruecksetzen.Location = new System.Drawing.Point(285, 464);
            this.Button_Zuruecksetzen.Margin = new System.Windows.Forms.Padding(2);
            this.Button_Zuruecksetzen.Name = "Button_Zuruecksetzen";
            this.Button_Zuruecksetzen.Size = new System.Drawing.Size(80, 37);
            this.Button_Zuruecksetzen.TabIndex = 38;
            this.Button_Zuruecksetzen.Text = "Prüfung zurücksetzen";
            this.Button_Zuruecksetzen.UseVisualStyleBackColor = true;
            this.Button_Zuruecksetzen.Click += new System.EventHandler(this.Button_Zuruecksetzen_Click);
            // 
            // Label_AktuellerZyklus
            // 
            this.Label_AktuellerZyklus.AutoSize = true;
            this.Label_AktuellerZyklus.Location = new System.Drawing.Point(4, 72);
            this.Label_AktuellerZyklus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_AktuellerZyklus.Name = "Label_AktuellerZyklus";
            this.Label_AktuellerZyklus.Size = new System.Drawing.Size(85, 13);
            this.Label_AktuellerZyklus.TabIndex = 41;
            this.Label_AktuellerZyklus.Text = "Aktueller Zyklus:";
            this.Label_AktuellerZyklus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Label_VerstricheneZeit
            // 
            this.Label_VerstricheneZeit.AutoSize = true;
            this.Label_VerstricheneZeit.Location = new System.Drawing.Point(155, 72);
            this.Label_VerstricheneZeit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_VerstricheneZeit.Name = "Label_VerstricheneZeit";
            this.Label_VerstricheneZeit.Size = new System.Drawing.Size(90, 13);
            this.Label_VerstricheneZeit.TabIndex = 42;
            this.Label_VerstricheneZeit.Text = "Verstrichene Zeit:";
            this.Label_VerstricheneZeit.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Label_Restzeit
            // 
            this.Label_Restzeit.AutoSize = true;
            this.Label_Restzeit.Location = new System.Drawing.Point(346, 72);
            this.Label_Restzeit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_Restzeit.Name = "Label_Restzeit";
            this.Label_Restzeit.Size = new System.Drawing.Size(48, 13);
            this.Label_Restzeit.TabIndex = 43;
            this.Label_Restzeit.Text = "Restzeit:";
            this.Label_Restzeit.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Label_AktuellerZyklusValue
            // 
            this.Label_AktuellerZyklusValue.AutoSize = true;
            this.Label_AktuellerZyklusValue.Location = new System.Drawing.Point(87, 72);
            this.Label_AktuellerZyklusValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_AktuellerZyklusValue.MinimumSize = new System.Drawing.Size(45, 13);
            this.Label_AktuellerZyklusValue.Name = "Label_AktuellerZyklusValue";
            this.Label_AktuellerZyklusValue.Size = new System.Drawing.Size(45, 13);
            this.Label_AktuellerZyklusValue.TabIndex = 44;
            this.Label_AktuellerZyklusValue.Text = "------";
            // 
            // Label_VerstricheneZeitValue
            // 
            this.Label_VerstricheneZeitValue.AutoSize = true;
            this.Label_VerstricheneZeitValue.Location = new System.Drawing.Point(249, 72);
            this.Label_VerstricheneZeitValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_VerstricheneZeitValue.MinimumSize = new System.Drawing.Size(45, 13);
            this.Label_VerstricheneZeitValue.Name = "Label_VerstricheneZeitValue";
            this.Label_VerstricheneZeitValue.Size = new System.Drawing.Size(45, 13);
            this.Label_VerstricheneZeitValue.TabIndex = 45;
            this.Label_VerstricheneZeitValue.Text = "------";
            // 
            // Label_RestzeitValue
            // 
            this.Label_RestzeitValue.AutoSize = true;
            this.Label_RestzeitValue.Location = new System.Drawing.Point(401, 72);
            this.Label_RestzeitValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_RestzeitValue.MinimumSize = new System.Drawing.Size(45, 13);
            this.Label_RestzeitValue.Name = "Label_RestzeitValue";
            this.Label_RestzeitValue.Size = new System.Drawing.Size(45, 13);
            this.Label_RestzeitValue.TabIndex = 46;
            this.Label_RestzeitValue.Text = "------";
            // 
            // Informationen
            // 
            this.Informationen.Controls.Add(this.Visualisierung_HeizpatronenZugeschaltet);
            this.Informationen.Controls.Add(this.Visualisierung_Betriebsart);
            this.Informationen.Controls.Add(this.Visualisierung_VentileEingeschaltet);
            this.Informationen.Controls.Add(this.Visualisierung_MotorEingeschaltet);
            this.Informationen.Controls.Add(this.Visualisierung_MikroschalterMesstaster);
            this.Informationen.Controls.Add(this.Visualisierung_AbdeckungGeschlossen);
            this.Informationen.Controls.Add(this.Visualisierung_PneumatikdruckVorhanden);
            this.Informationen.Controls.Add(this.Visualisierung_NotAus);
            this.Informationen.Controls.Add(this.label9);
            this.Informationen.Controls.Add(this.progressBar);
            this.Informationen.Controls.Add(this.Label_AktuellerZyklus);
            this.Informationen.Controls.Add(this.Label_RestzeitValue);
            this.Informationen.Controls.Add(this.Label_VerstricheneZeit);
            this.Informationen.Controls.Add(this.Label_VerstricheneZeitValue);
            this.Informationen.Controls.Add(this.Label_Restzeit);
            this.Informationen.Controls.Add(this.Label_AktuellerZyklusValue);
            this.Informationen.Location = new System.Drawing.Point(903, 44);
            this.Informationen.Margin = new System.Windows.Forms.Padding(2);
            this.Informationen.Name = "Informationen";
            this.Informationen.Padding = new System.Windows.Forms.Padding(2);
            this.Informationen.Size = new System.Drawing.Size(770, 123);
            this.Informationen.TabIndex = 47;
            this.Informationen.TabStop = false;
            this.Informationen.Text = "Information";
            // 
            // Visualisierung_HeizpatronenZugeschaltet
            // 
            this.Visualisierung_HeizpatronenZugeschaltet.Enabled = false;
            this.Visualisierung_HeizpatronenZugeschaltet.Location = new System.Drawing.Point(511, 17);
            this.Visualisierung_HeizpatronenZugeschaltet.Margin = new System.Windows.Forms.Padding(2);
            this.Visualisierung_HeizpatronenZugeschaltet.Name = "Visualisierung_HeizpatronenZugeschaltet";
            this.Visualisierung_HeizpatronenZugeschaltet.Size = new System.Drawing.Size(89, 37);
            this.Visualisierung_HeizpatronenZugeschaltet.TabIndex = 55;
            this.Visualisierung_HeizpatronenZugeschaltet.Text = "Heizpatronen zugeschaltet";
            this.Visualisierung_HeizpatronenZugeschaltet.UseVisualStyleBackColor = true;
            // 
            // Visualisierung_Betriebsart
            // 
            this.Visualisierung_Betriebsart.BackColor = System.Drawing.Color.Transparent;
            this.Visualisierung_Betriebsart.Enabled = false;
            this.Visualisierung_Betriebsart.Location = new System.Drawing.Point(604, 17);
            this.Visualisierung_Betriebsart.Margin = new System.Windows.Forms.Padding(2);
            this.Visualisierung_Betriebsart.Name = "Visualisierung_Betriebsart";
            this.Visualisierung_Betriebsart.Size = new System.Drawing.Size(89, 37);
            this.Visualisierung_Betriebsart.TabIndex = 54;
            this.Visualisierung_Betriebsart.Text = "Betriebsart";
            this.Visualisierung_Betriebsart.UseVisualStyleBackColor = false;
            // 
            // Visualisierung_VentileEingeschaltet
            // 
            this.Visualisierung_VentileEingeschaltet.BackColor = System.Drawing.Color.Transparent;
            this.Visualisierung_VentileEingeschaltet.Enabled = false;
            this.Visualisierung_VentileEingeschaltet.Location = new System.Drawing.Point(349, 17);
            this.Visualisierung_VentileEingeschaltet.Margin = new System.Windows.Forms.Padding(2);
            this.Visualisierung_VentileEingeschaltet.Name = "Visualisierung_VentileEingeschaltet";
            this.Visualisierung_VentileEingeschaltet.Size = new System.Drawing.Size(85, 37);
            this.Visualisierung_VentileEingeschaltet.TabIndex = 53;
            this.Visualisierung_VentileEingeschaltet.Text = "Ventile eingeschaltet";
            this.Visualisierung_VentileEingeschaltet.UseVisualStyleBackColor = false;
            // 
            // Visualisierung_MikroschalterMesstaster
            // 
            this.Visualisierung_MikroschalterMesstaster.BackColor = System.Drawing.Color.Transparent;
            this.Visualisierung_MikroschalterMesstaster.Enabled = false;
            this.Visualisierung_MikroschalterMesstaster.Location = new System.Drawing.Point(158, 17);
            this.Visualisierung_MikroschalterMesstaster.Margin = new System.Windows.Forms.Padding(2);
            this.Visualisierung_MikroschalterMesstaster.Name = "Visualisierung_MikroschalterMesstaster";
            this.Visualisierung_MikroschalterMesstaster.Size = new System.Drawing.Size(88, 37);
            this.Visualisierung_MikroschalterMesstaster.TabIndex = 52;
            this.Visualisierung_MikroschalterMesstaster.Text = "Mikroschalter für Messtaster";
            this.Visualisierung_MikroschalterMesstaster.UseVisualStyleBackColor = false;
            this.Visualisierung_MikroschalterMesstaster.Click += new System.EventHandler(this.Visualisierung_MikroschalterMesstaster_Click);
            // 
            // Visualisierung_AbdeckungGeschlossen
            // 
            this.Visualisierung_AbdeckungGeschlossen.BackColor = System.Drawing.Color.Transparent;
            this.Visualisierung_AbdeckungGeschlossen.Enabled = false;
            this.Visualisierung_AbdeckungGeschlossen.Location = new System.Drawing.Point(76, 17);
            this.Visualisierung_AbdeckungGeschlossen.Margin = new System.Windows.Forms.Padding(2);
            this.Visualisierung_AbdeckungGeschlossen.Name = "Visualisierung_AbdeckungGeschlossen";
            this.Visualisierung_AbdeckungGeschlossen.Size = new System.Drawing.Size(77, 37);
            this.Visualisierung_AbdeckungGeschlossen.TabIndex = 51;
            this.Visualisierung_AbdeckungGeschlossen.Text = "Abdeckung geschlossen";
            this.Visualisierung_AbdeckungGeschlossen.UseVisualStyleBackColor = false;
            this.Visualisierung_AbdeckungGeschlossen.Click += new System.EventHandler(this.Visualisierung_AbdeckungGeschlossen_Click);
            // 
            // Visualisierung_PneumatikdruckVorhanden
            // 
            this.Visualisierung_PneumatikdruckVorhanden.BackColor = System.Drawing.Color.Transparent;
            this.Visualisierung_PneumatikdruckVorhanden.Enabled = false;
            this.Visualisierung_PneumatikdruckVorhanden.Location = new System.Drawing.Point(251, 17);
            this.Visualisierung_PneumatikdruckVorhanden.Margin = new System.Windows.Forms.Padding(2);
            this.Visualisierung_PneumatikdruckVorhanden.Name = "Visualisierung_PneumatikdruckVorhanden";
            this.Visualisierung_PneumatikdruckVorhanden.Size = new System.Drawing.Size(94, 37);
            this.Visualisierung_PneumatikdruckVorhanden.TabIndex = 50;
            this.Visualisierung_PneumatikdruckVorhanden.Text = "Pneumatikdruck vorhanden";
            this.Visualisierung_PneumatikdruckVorhanden.UseVisualStyleBackColor = false;
            this.Visualisierung_PneumatikdruckVorhanden.Click += new System.EventHandler(this.Visualisierung_PneumatikdruckVorhanden_Click);
            // 
            // Visualisierung_NotAus
            // 
            this.Visualisierung_NotAus.BackColor = System.Drawing.Color.Transparent;
            this.Visualisierung_NotAus.Enabled = false;
            this.Visualisierung_NotAus.Location = new System.Drawing.Point(4, 17);
            this.Visualisierung_NotAus.Margin = new System.Windows.Forms.Padding(2);
            this.Visualisierung_NotAus.Name = "Visualisierung_NotAus";
            this.Visualisierung_NotAus.Size = new System.Drawing.Size(68, 37);
            this.Visualisierung_NotAus.TabIndex = 29;
            this.Visualisierung_NotAus.Text = "Not-Aus";
            this.Visualisierung_NotAus.UseVisualStyleBackColor = false;
            this.Visualisierung_NotAus.Click += new System.EventHandler(this.Visualisierung_NotAus_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 98);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 49;
            this.label9.Text = "Fortschritt:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(70, 93);
            this.progressBar.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar.Maximum = 1000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(530, 19);
            this.progressBar.TabIndex = 48;
            // 
            // Button_MilliohmmeterVerbinden
            // 
            this.Button_MilliohmmeterVerbinden.Enabled = false;
            this.Button_MilliohmmeterVerbinden.Location = new System.Drawing.Point(4, 17);
            this.Button_MilliohmmeterVerbinden.Margin = new System.Windows.Forms.Padding(2);
            this.Button_MilliohmmeterVerbinden.Name = "Button_MilliohmmeterVerbinden";
            this.Button_MilliohmmeterVerbinden.Size = new System.Drawing.Size(83, 37);
            this.Button_MilliohmmeterVerbinden.TabIndex = 29;
            this.Button_MilliohmmeterVerbinden.Text = "Milliohmmeter verbinden";
            this.Button_MilliohmmeterVerbinden.UseVisualStyleBackColor = true;
            this.Button_MilliohmmeterVerbinden.Click += new System.EventHandler(this.Button_MilliohmmeterVerbinden_Click);
            // 
            // Button_KraftsensorVerbinden
            // 
            this.Button_KraftsensorVerbinden.Enabled = false;
            this.Button_KraftsensorVerbinden.Location = new System.Drawing.Point(92, 17);
            this.Button_KraftsensorVerbinden.Margin = new System.Windows.Forms.Padding(2);
            this.Button_KraftsensorVerbinden.Name = "Button_KraftsensorVerbinden";
            this.Button_KraftsensorVerbinden.Size = new System.Drawing.Size(69, 37);
            this.Button_KraftsensorVerbinden.TabIndex = 48;
            this.Button_KraftsensorVerbinden.Text = "Kraftsensor verbinden";
            this.Button_KraftsensorVerbinden.UseVisualStyleBackColor = true;
            this.Button_KraftsensorVerbinden.Click += new System.EventHandler(this.ButtonKraftsensorVerbinden_Click);
            // 
            // Button_DigitalanzeigeVerbinden
            // 
            this.Button_DigitalanzeigeVerbinden.Enabled = false;
            this.Button_DigitalanzeigeVerbinden.Location = new System.Drawing.Point(166, 17);
            this.Button_DigitalanzeigeVerbinden.Margin = new System.Windows.Forms.Padding(2);
            this.Button_DigitalanzeigeVerbinden.Name = "Button_DigitalanzeigeVerbinden";
            this.Button_DigitalanzeigeVerbinden.Size = new System.Drawing.Size(83, 37);
            this.Button_DigitalanzeigeVerbinden.TabIndex = 49;
            this.Button_DigitalanzeigeVerbinden.Text = "Digitalanzeige verbinden";
            this.Button_DigitalanzeigeVerbinden.UseVisualStyleBackColor = true;
            this.Button_DigitalanzeigeVerbinden.Click += new System.EventHandler(this.Button_DigitalanzeigeVerbinden_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.Button_TwinCATVerbinden);
            this.groupBox6.Controls.Add(this.Button_TemperaturreglerVerbinden);
            this.groupBox6.Controls.Add(this.TextBos_StatusKommunikation);
            this.groupBox6.Controls.Add(this.Button_MilliohmmeterVerbinden);
            this.groupBox6.Controls.Add(this.Button_DigitalanzeigeVerbinden);
            this.groupBox6.Controls.Add(this.Button_KraftsensorVerbinden);
            this.groupBox6.Location = new System.Drawing.Point(9, 44);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(461, 106);
            this.groupBox6.TabIndex = 50;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Verbinden";
            // 
            // Button_TwinCATVerbinden
            // 
            this.Button_TwinCATVerbinden.Enabled = false;
            this.Button_TwinCATVerbinden.Location = new System.Drawing.Point(254, 17);
            this.Button_TwinCATVerbinden.Margin = new System.Windows.Forms.Padding(2);
            this.Button_TwinCATVerbinden.Name = "Button_TwinCATVerbinden";
            this.Button_TwinCATVerbinden.Size = new System.Drawing.Size(69, 37);
            this.Button_TwinCATVerbinden.TabIndex = 51;
            this.Button_TwinCATVerbinden.Text = "TwinCAT verbinden";
            this.Button_TwinCATVerbinden.UseVisualStyleBackColor = true;
            this.Button_TwinCATVerbinden.Click += new System.EventHandler(this.Button_TwinCATVerbinden_Click);
            // 
            // Button_TemperaturreglerVerbinden
            // 
            this.Button_TemperaturreglerVerbinden.Enabled = false;
            this.Button_TemperaturreglerVerbinden.Location = new System.Drawing.Point(327, 17);
            this.Button_TemperaturreglerVerbinden.Margin = new System.Windows.Forms.Padding(2);
            this.Button_TemperaturreglerVerbinden.Name = "Button_TemperaturreglerVerbinden";
            this.Button_TemperaturreglerVerbinden.Size = new System.Drawing.Size(104, 37);
            this.Button_TemperaturreglerVerbinden.TabIndex = 50;
            this.Button_TemperaturreglerVerbinden.Text = "Temperaturregler verbinden";
            this.Button_TemperaturreglerVerbinden.UseVisualStyleBackColor = true;
            this.Button_TemperaturreglerVerbinden.Click += new System.EventHandler(this.Button_TemperaturreglerVerbinden_Click);
            // 
            // TextBos_StatusKommunikation
            // 
            this.TextBos_StatusKommunikation.Location = new System.Drawing.Point(5, 59);
            this.TextBos_StatusKommunikation.Margin = new System.Windows.Forms.Padding(2);
            this.TextBos_StatusKommunikation.Multiline = true;
            this.TextBos_StatusKommunikation.Name = "TextBos_StatusKommunikation";
            this.TextBos_StatusKommunikation.Size = new System.Drawing.Size(178, 38);
            this.TextBos_StatusKommunikation.TabIndex = 41;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenu_Einstellungen});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1684, 24);
            this.menuStrip1.TabIndex = 51;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenu_Einstellungen
            // 
            this.toolStripMenu_Einstellungen.Name = "toolStripMenu_Einstellungen";
            this.toolStripMenu_Einstellungen.Size = new System.Drawing.Size(90, 20);
            this.toolStripMenu_Einstellungen.Text = "Einstellungen";
            this.toolStripMenu_Einstellungen.Click += new System.EventHandler(this.toolStripMenu_Einstellungen_Click);
            // 
            // Panel_HubEinstellen
            // 
            this.Panel_HubEinstellen.Controls.Add(this.groupBox7);
            this.Panel_HubEinstellen.Location = new System.Drawing.Point(474, 178);
            this.Panel_HubEinstellen.Margin = new System.Windows.Forms.Padding(2);
            this.Panel_HubEinstellen.Name = "Panel_HubEinstellen";
            this.Panel_HubEinstellen.Size = new System.Drawing.Size(368, 394);
            this.Panel_HubEinstellen.TabIndex = 52;
            this.Panel_HubEinstellen.Visible = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.textBox_HubEinstellenAktuelleAufgabe);
            this.groupBox7.Controls.Add(this.button_HubErmitteln);
            this.groupBox7.Controls.Add(this.Button_HubEinstellenAbbrechen);
            this.groupBox7.Controls.Add(this.Button_HubEinstellenSpeichern);
            this.groupBox7.Controls.Add(this.Label_EingestellteHub);
            this.groupBox7.Controls.Add(this.label2);
            this.groupBox7.Controls.Add(this.textBox2);
            this.groupBox7.Controls.Add(this.CheckBox_HubEingestellt);
            this.groupBox7.Controls.Add(this.Button_StartpunktAnfahren);
            this.groupBox7.Controls.Add(this.Label_EinzustellendeHub);
            this.groupBox7.Controls.Add(this.textBox1);
            this.groupBox7.Controls.Add(this.Button_MesstasterNullen);
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Location = new System.Drawing.Point(4, 2);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox7.Size = new System.Drawing.Size(359, 387);
            this.groupBox7.TabIndex = 53;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Hub einstellen";
            // 
            // textBox_HubEinstellenAktuelleAufgabe
            // 
            this.textBox_HubEinstellenAktuelleAufgabe.Location = new System.Drawing.Point(275, 272);
            this.textBox_HubEinstellenAktuelleAufgabe.Multiline = true;
            this.textBox_HubEinstellenAktuelleAufgabe.Name = "textBox_HubEinstellenAktuelleAufgabe";
            this.textBox_HubEinstellenAktuelleAufgabe.ReadOnly = true;
            this.textBox_HubEinstellenAktuelleAufgabe.Size = new System.Drawing.Size(74, 58);
            this.textBox_HubEinstellenAktuelleAufgabe.TabIndex = 0;
            // 
            // button_HubErmitteln
            // 
            this.button_HubErmitteln.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.button_HubErmitteln.Location = new System.Drawing.Point(188, 17);
            this.button_HubErmitteln.Margin = new System.Windows.Forms.Padding(2);
            this.button_HubErmitteln.Name = "button_HubErmitteln";
            this.button_HubErmitteln.Size = new System.Drawing.Size(92, 37);
            this.button_HubErmitteln.TabIndex = 62;
            this.button_HubErmitteln.Text = "Eingestellten Hub ermitteln";
            this.button_HubErmitteln.UseVisualStyleBackColor = true;
            this.button_HubErmitteln.Click += new System.EventHandler(this.button_HubErmitteln_Click);
            // 
            // Button_HubEinstellenAbbrechen
            // 
            this.Button_HubEinstellenAbbrechen.Location = new System.Drawing.Point(94, 346);
            this.Button_HubEinstellenAbbrechen.Margin = new System.Windows.Forms.Padding(2);
            this.Button_HubEinstellenAbbrechen.Name = "Button_HubEinstellenAbbrechen";
            this.Button_HubEinstellenAbbrechen.Size = new System.Drawing.Size(69, 37);
            this.Button_HubEinstellenAbbrechen.TabIndex = 57;
            this.Button_HubEinstellenAbbrechen.Text = "Abbrechen";
            this.Button_HubEinstellenAbbrechen.UseVisualStyleBackColor = true;
            this.Button_HubEinstellenAbbrechen.Click += new System.EventHandler(this.Button_HubEinstellenAbbrechen_Click);
            // 
            // Button_HubEinstellenSpeichern
            // 
            this.Button_HubEinstellenSpeichern.Enabled = false;
            this.Button_HubEinstellenSpeichern.Location = new System.Drawing.Point(4, 346);
            this.Button_HubEinstellenSpeichern.Margin = new System.Windows.Forms.Padding(2);
            this.Button_HubEinstellenSpeichern.Name = "Button_HubEinstellenSpeichern";
            this.Button_HubEinstellenSpeichern.Size = new System.Drawing.Size(86, 37);
            this.Button_HubEinstellenSpeichern.TabIndex = 29;
            this.Button_HubEinstellenSpeichern.Text = "Hubeinstellung Speichern";
            this.Button_HubEinstellenSpeichern.UseVisualStyleBackColor = true;
            this.Button_HubEinstellenSpeichern.Click += new System.EventHandler(this.Button_HubEinstellenSpeichern_Click);
            // 
            // Label_EingestellteHub
            // 
            this.Label_EingestellteHub.AutoSize = true;
            this.Label_EingestellteHub.Location = new System.Drawing.Point(118, 144);
            this.Label_EingestellteHub.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_EingestellteHub.MinimumSize = new System.Drawing.Size(45, 0);
            this.Label_EingestellteHub.Name = "Label_EingestellteHub";
            this.Label_EingestellteHub.Size = new System.Drawing.Size(45, 13);
            this.Label_EingestellteHub.TabIndex = 56;
            this.Label_EingestellteHub.Text = "-----";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 143);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.MaximumSize = new System.Drawing.Size(120, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 55;
            this.label2.Text = "Eingestellte Hub [µm]:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(4, 230);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(266, 100);
            this.textBox2.TabIndex = 0;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // CheckBox_HubEingestellt
            // 
            this.CheckBox_HubEingestellt.AutoSize = true;
            this.CheckBox_HubEingestellt.Location = new System.Drawing.Point(246, 209);
            this.CheckBox_HubEingestellt.Margin = new System.Windows.Forms.Padding(2);
            this.CheckBox_HubEingestellt.Name = "CheckBox_HubEingestellt";
            this.CheckBox_HubEingestellt.Size = new System.Drawing.Size(109, 17);
            this.CheckBox_HubEingestellt.TabIndex = 53;
            this.CheckBox_HubEingestellt.Text = "Hub ist eingestellt";
            this.CheckBox_HubEingestellt.UseVisualStyleBackColor = true;
            this.CheckBox_HubEingestellt.CheckedChanged += new System.EventHandler(this.CheckBox_HubEingestellt_CheckedChanged);
            // 
            // Button_StartpunktAnfahren
            // 
            this.Button_StartpunktAnfahren.Enabled = false;
            this.Button_StartpunktAnfahren.Location = new System.Drawing.Point(274, 230);
            this.Button_StartpunktAnfahren.Margin = new System.Windows.Forms.Padding(2);
            this.Button_StartpunktAnfahren.Name = "Button_StartpunktAnfahren";
            this.Button_StartpunktAnfahren.Size = new System.Drawing.Size(69, 37);
            this.Button_StartpunktAnfahren.TabIndex = 29;
            this.Button_StartpunktAnfahren.Text = "Startpunkt anfahren";
            this.Button_StartpunktAnfahren.UseVisualStyleBackColor = true;
            this.Button_StartpunktAnfahren.Click += new System.EventHandler(this.Button_StartpunktAnfahren_Click);
            // 
            // Label_EinzustellendeHub
            // 
            this.Label_EinzustellendeHub.AutoSize = true;
            this.Label_EinzustellendeHub.Location = new System.Drawing.Point(137, 211);
            this.Label_EinzustellendeHub.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_EinzustellendeHub.MinimumSize = new System.Drawing.Size(45, 0);
            this.Label_EinzustellendeHub.Name = "Label_EinzustellendeHub";
            this.Label_EinzustellendeHub.Size = new System.Drawing.Size(45, 13);
            this.Label_EinzustellendeHub.TabIndex = 53;
            this.Label_EinzustellendeHub.Text = "-----";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 16);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(180, 125);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // Button_MesstasterNullen
            // 
            this.Button_MesstasterNullen.Location = new System.Drawing.Point(188, 59);
            this.Button_MesstasterNullen.Margin = new System.Windows.Forms.Padding(2);
            this.Button_MesstasterNullen.Name = "Button_MesstasterNullen";
            this.Button_MesstasterNullen.Size = new System.Drawing.Size(92, 37);
            this.Button_MesstasterNullen.TabIndex = 0;
            this.Button_MesstasterNullen.Text = "Messtaster nullen";
            this.Button_MesstasterNullen.UseVisualStyleBackColor = true;
            this.Button_MesstasterNullen.Click += new System.EventHandler(this.Button_MesstasterNullen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 210);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.MaximumSize = new System.Drawing.Size(135, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 53;
            this.label1.Text = "Einzustellende Weg [mm]:";
            // 
            // serialPort_Milliohmmeter
            // 
            this.serialPort_Milliohmmeter.BaudRate = 38400;
            this.serialPort_Milliohmmeter.PortName = "COM4";
            this.serialPort_Milliohmmeter.ReadTimeout = 500;
            // 
            // Panel_WiderstandPruefen
            // 
            this.Panel_WiderstandPruefen.Controls.Add(this.groupBox8);
            this.Panel_WiderstandPruefen.Location = new System.Drawing.Point(469, 110);
            this.Panel_WiderstandPruefen.Margin = new System.Windows.Forms.Padding(2);
            this.Panel_WiderstandPruefen.Name = "Panel_WiderstandPruefen";
            this.Panel_WiderstandPruefen.Size = new System.Drawing.Size(430, 63);
            this.Panel_WiderstandPruefen.TabIndex = 53;
            this.Panel_WiderstandPruefen.Visible = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.Label_WiderstandWert);
            this.groupBox8.Controls.Add(this.Label_Widerstand);
            this.groupBox8.Controls.Add(this.Button_WiderstandAbbrechen);
            this.groupBox8.Controls.Add(this.Button_WiderstandMessen);
            this.groupBox8.Controls.Add(this.Button_WiderstandOk);
            this.groupBox8.Location = new System.Drawing.Point(5, 7);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox8.Size = new System.Drawing.Size(414, 53);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Widerstand prüfen";
            // 
            // Label_WiderstandWert
            // 
            this.Label_WiderstandWert.AutoSize = true;
            this.Label_WiderstandWert.Location = new System.Drawing.Point(106, 20);
            this.Label_WiderstandWert.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_WiderstandWert.Name = "Label_WiderstandWert";
            this.Label_WiderstandWert.Size = new System.Drawing.Size(22, 13);
            this.Label_WiderstandWert.TabIndex = 54;
            this.Label_WiderstandWert.Text = "-----";
            // 
            // Label_Widerstand
            // 
            this.Label_Widerstand.AutoSize = true;
            this.Label_Widerstand.Location = new System.Drawing.Point(4, 20);
            this.Label_Widerstand.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_Widerstand.Name = "Label_Widerstand";
            this.Label_Widerstand.Size = new System.Drawing.Size(103, 13);
            this.Label_Widerstand.TabIndex = 56;
            this.Label_Widerstand.Text = "Widerstand [mOhm]:";
            // 
            // Button_WiderstandAbbrechen
            // 
            this.Button_WiderstandAbbrechen.Location = new System.Drawing.Point(333, 12);
            this.Button_WiderstandAbbrechen.Margin = new System.Windows.Forms.Padding(2);
            this.Button_WiderstandAbbrechen.Name = "Button_WiderstandAbbrechen";
            this.Button_WiderstandAbbrechen.Size = new System.Drawing.Size(69, 37);
            this.Button_WiderstandAbbrechen.TabIndex = 54;
            this.Button_WiderstandAbbrechen.Text = "Abbrechen";
            this.Button_WiderstandAbbrechen.UseVisualStyleBackColor = true;
            this.Button_WiderstandAbbrechen.Click += new System.EventHandler(this.Button_WiderstandAbbrechen_Click);
            // 
            // Button_WiderstandMessen
            // 
            this.Button_WiderstandMessen.Location = new System.Drawing.Point(187, 12);
            this.Button_WiderstandMessen.Margin = new System.Windows.Forms.Padding(2);
            this.Button_WiderstandMessen.Name = "Button_WiderstandMessen";
            this.Button_WiderstandMessen.Size = new System.Drawing.Size(69, 37);
            this.Button_WiderstandMessen.TabIndex = 55;
            this.Button_WiderstandMessen.Text = "Messen";
            this.Button_WiderstandMessen.UseVisualStyleBackColor = true;
            this.Button_WiderstandMessen.Click += new System.EventHandler(this.Button_WiderstandMessen_Click);
            // 
            // Button_WiderstandOk
            // 
            this.Button_WiderstandOk.Enabled = false;
            this.Button_WiderstandOk.Location = new System.Drawing.Point(260, 12);
            this.Button_WiderstandOk.Margin = new System.Windows.Forms.Padding(2);
            this.Button_WiderstandOk.Name = "Button_WiderstandOk";
            this.Button_WiderstandOk.Size = new System.Drawing.Size(69, 37);
            this.Button_WiderstandOk.TabIndex = 29;
            this.Button_WiderstandOk.Text = "OK";
            this.Button_WiderstandOk.UseVisualStyleBackColor = true;
            this.Button_WiderstandOk.Click += new System.EventHandler(this.Button_WiderstandOk_Click);
            // 
            // serialPort_Digitalanzeige
            // 
            this.serialPort_Digitalanzeige.BaudRate = 38400;
            this.serialPort_Digitalanzeige.PortName = "COM5";
            this.serialPort_Digitalanzeige.ReadTimeout = 1000;
            this.serialPort_Digitalanzeige.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_Digitalanzeige_DataReceived);
            // 
            // serialPort_Temperaturregler
            // 
            this.serialPort_Temperaturregler.BaudRate = 19200;
            this.serialPort_Temperaturregler.PortName = "COM6";
            this.serialPort_Temperaturregler.ReadTimeout = 500;
            // 
            // backgroundWorker_Start
            // 
            this.backgroundWorker_Start.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork_Pruefungsdurchfuehrung);
            // 
            // button_csvKopieren
            // 
            this.button_csvKopieren.Enabled = false;
            this.button_csvKopieren.Location = new System.Drawing.Point(204, 464);
            this.button_csvKopieren.Margin = new System.Windows.Forms.Padding(2);
            this.button_csvKopieren.Name = "button_csvKopieren";
            this.button_csvKopieren.Size = new System.Drawing.Size(77, 37);
            this.button_csvKopieren.TabIndex = 54;
            this.button_csvKopieren.Text = "csv-Datei kopieren";
            this.button_csvKopieren.UseVisualStyleBackColor = true;
            this.button_csvKopieren.Click += new System.EventHandler(this.button_csvKopieren_Click);
            // 
            // backgroundWorker_StartpunktAnfahren
            // 
            this.backgroundWorker_StartpunktAnfahren.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork_StartpunktAnfahren);
            // 
            // backgroundWorker_Refresh
            // 
            this.backgroundWorker_Refresh.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork_Refresh);
            // 
            // button_ReibkraftMessen
            // 
            this.button_ReibkraftMessen.Location = new System.Drawing.Point(295, 593);
            this.button_ReibkraftMessen.Name = "button_ReibkraftMessen";
            this.button_ReibkraftMessen.Size = new System.Drawing.Size(75, 23);
            this.button_ReibkraftMessen.TabIndex = 55;
            this.button_ReibkraftMessen.Text = "button1";
            this.button_ReibkraftMessen.UseVisualStyleBackColor = true;
            this.button_ReibkraftMessen.Visible = false;
            this.button_ReibkraftMessen.Click += new System.EventHandler(this.button_ReibkraftMessen_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(390, 598);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 56;
            this.label10.Text = "label10";
            this.label10.Visible = false;
            // 
            // Hauptfenster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(1684, 981);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button_ReibkraftMessen);
            this.Controls.Add(this.button_csvKopieren);
            this.Controls.Add(this.Panel_WiderstandPruefen);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.Informationen);
            this.Controls.Add(this.Button_Zuruecksetzen);
            this.Controls.Add(this.groupBox_Prüfungsdurchführung);
            this.Controls.Add(this.groupBox_DatenZurPruefung);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.Button_PruefungAbbrechen);
            this.Controls.Add(this.Button_Pausieren);
            this.Controls.Add(this.Button_Start);
            this.Controls.Add(this.Chart_IstTemperatur);
            this.Controls.Add(this.chart_Reibkraft);
            this.Controls.Add(this.groupBox_Pruefungsende);
            this.Controls.Add(this.groupBox_Reibkraftmessung);
            this.Controls.Add(this.groupBox_Temperatur);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.Panel_HubEinstellen);
            this.Controls.Add(this.chart_Widerstand);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1262, 826);
            this.Name = "Hauptfenster";
            this.Text = "Reibkorrosionsprüfstand";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_LoadComplete);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_RGrenzwertEnde)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_GrenzwertZyklen)).EndInit();
            this.groupBox_Temperatur.ResumeLayout(false);
            this.groupBox_Temperatur.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_OffsetTemperaturProbeUnten)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_OffsetTemperaturProbeOben)).EndInit();
            this.groupBox_Reibkraftmessung.ResumeLayout(false);
            this.groupBox_Reibkraftmessung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Widerstand)).EndInit();
            this.groupBox_Pruefungsende.ResumeLayout(false);
            this.groupBox_Pruefungsende.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Reibkraft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_IstTemperatur)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox_DatenZurPruefung.ResumeLayout(false);
            this.groupBox_DatenZurPruefung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Hub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Kontaktkraft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Federweg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Wartezeit)).EndInit();
            this.groupBox_Prüfungsdurchführung.ResumeLayout(false);
            this.groupBox_Prüfungsdurchführung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SollTemperaturUnten)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_SollTemperaturOben)).EndInit();
            this.Informationen.ResumeLayout(false);
            this.Informationen.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.Panel_HubEinstellen.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.Panel_WiderstandPruefen.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_PruefungMitTemperatur;
        private System.Windows.Forms.CheckBox checkBox_ReibkraftMessen;
        private System.Windows.Forms.NumericUpDown numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben;
        private System.Windows.Forms.NumericUpDown numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten;
        private System.Windows.Forms.Label Label_SollTemperaturHeizpatroneOben;
        private System.Windows.Forms.Label Label_SollTemperaturHeizpatroneUnten;
        private System.Windows.Forms.Label Label_IstTemperaturHeizpatroneOben;
        private System.Windows.Forms.Label Label_IstTemperaturHeizpatroneUnten;
        private System.Windows.Forms.Label Label_IstTemperaturHeizpatroneUntenValue;
        private System.Windows.Forms.Label Label_IstTemperaturHeizpatroneObenValue;
        private System.Windows.Forms.Label Label_Reibkraft;
        private System.Windows.Forms.Label Label_ReibkraftValue;
        private System.Windows.Forms.NumericUpDown NumericUpDown_RGrenzwertEnde;
        private System.Windows.Forms.Label Label_RGrenzwert;
        private System.Windows.Forms.NumericUpDown NumericUpDown_GrenzwertZyklen;
        private System.Windows.Forms.Label Label_GrenzwertZyklen;
        private System.Windows.Forms.GroupBox groupBox_Temperatur;
        private System.Windows.Forms.GroupBox groupBox_Reibkraftmessung;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Widerstand;
        private System.Windows.Forms.GroupBox groupBox_Pruefungsende;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Reibkraft;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_IstTemperatur;
        private System.Windows.Forms.Button Button_Start;
        private System.Windows.Forms.Button Button_Pausieren;
        private System.Windows.Forms.Button Button_PruefungAbbrechen;
        private System.Windows.Forms.Button Button_WiderstandPruefen;
        private System.Windows.Forms.Button Button_HubEinstellen;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button Button_LetztePruefbeschreibungLaden;
        private System.Windows.Forms.GroupBox groupBox_DatenZurPruefung;
        private System.Windows.Forms.Label Label_Pruefer;
        private System.Windows.Forms.Label Label_Artikelnummer;
        private System.Windows.Forms.Label Label_Projektnummer;
        private System.Windows.Forms.Label Label_Auftragsnummer;
        private System.Windows.Forms.Label Label_Schmierstoff;
        private System.Windows.Forms.Label Label_Beschreibung;
        private System.Windows.Forms.TextBox TextBox_Artikelnummer;
        private System.Windows.Forms.TextBox TextBox_Beschreibung;
        private System.Windows.Forms.TextBox TextBox_Schmierstoff;
        private System.Windows.Forms.TextBox TextBox_Auftragsnummer;
        private System.Windows.Forms.TextBox TextBox_Projektnummer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown NumericUpDown_Hub;
        private System.Windows.Forms.NumericUpDown NumericUpDown_Kontaktkraft;
        private System.Windows.Forms.NumericUpDown NumericUpDown_Federweg;
        private System.Windows.Forms.NumericUpDown NumericUpDown_Wartezeit;
        private System.Windows.Forms.GroupBox groupBox_Prüfungsdurchführung;
        private System.Windows.Forms.Button Button_Zuruecksetzen;
        private System.Windows.Forms.Label Label_AktuellerZyklus;
        private System.Windows.Forms.Label Label_VerstricheneZeit;
        private System.Windows.Forms.Label Label_Restzeit;
        private System.Windows.Forms.Label Label_AktuellerZyklusValue;
        private System.Windows.Forms.Label Label_VerstricheneZeitValue;
        private System.Windows.Forms.Label Label_RestzeitValue;
        private System.Windows.Forms.GroupBox Informationen;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button Visualisierung_MotorEingeschaltet;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button Button_MilliohmmeterVerbinden;
        private System.Windows.Forms.Button Button_KraftsensorVerbinden;
        private System.Windows.Forms.Button Button_DigitalanzeigeVerbinden;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button Visualisierung_NotAus;
        private System.Windows.Forms.TextBox TextBos_StatusKommunikation;
        private System.Windows.Forms.Button Visualisierung_PneumatikdruckVorhanden;
        private System.Windows.Forms.Button Visualisierung_AbdeckungGeschlossen;
        private System.Windows.Forms.Button Visualisierung_MikroschalterMesstaster;
        private System.Windows.Forms.Button Button_TemperaturreglerVerbinden;
        private System.Windows.Forms.Button Button_TwinCATVerbinden;
        private System.Windows.Forms.TextBox TextBox_Pruefer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenu_Einstellungen;
        private System.Windows.Forms.Panel Panel_HubEinstellen;
        private System.Windows.Forms.Button Button_MesstasterNullen;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Label_EinzustellendeHub;
        private System.Windows.Forms.Button Button_StartpunktAnfahren;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox CheckBox_HubEingestellt;
        private System.Windows.Forms.Button Button_HubEinstellenSpeichern;
        private System.Windows.Forms.Label Label_EingestellteHub;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Button_HubEinstellenAbbrechen;
        private System.IO.Ports.SerialPort serialPort_Milliohmmeter;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Panel Panel_WiderstandPruefen;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label Label_WiderstandWert;
        private System.Windows.Forms.Label Label_Widerstand;
        private System.Windows.Forms.Button Button_WiderstandAbbrechen;
        private System.Windows.Forms.Button Button_WiderstandMessen;
        private System.Windows.Forms.Button Button_WiderstandOk;
        private System.IO.Ports.SerialPort serialPort_Digitalanzeige;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_Messung;
        private System.Windows.Forms.Button button_HubErmitteln;
        private System.IO.Ports.SerialPort serialPort_Temperaturregler;
        private System.Windows.Forms.NumericUpDown numericUpDown_OffsetTemperaturProbeUnten;
        private System.Windows.Forms.Label Label_IstTemperaturProbeUnten;
        private System.Windows.Forms.NumericUpDown numericUpDown_OffsetTemperaturProbeOben;
        private System.Windows.Forms.Label Label_IstTemperaturProbeOben;
        private System.Windows.Forms.Button button_ProbenAufheizen;
        private System.Windows.Forms.Button Visualisierung_Betriebsart;
        private System.Windows.Forms.Button Visualisierung_VentileEingeschaltet;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Start;
        private System.Windows.Forms.Button button_csvKopieren;
        private System.ComponentModel.BackgroundWorker backgroundWorker_StartpunktAnfahren;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Refresh;
        private System.Windows.Forms.Button Visualisierung_HeizpatronenZugeschaltet;
        private System.Windows.Forms.Button button_WerteAnTemperaturreglerÜbertragen;
        private System.Windows.Forms.NumericUpDown numericUpDown_SollTemperaturUnten;
        private System.Windows.Forms.NumericUpDown numericUpDown_SollTemperaturOben;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_HubEinstellenAktuelleAufgabe;
        private System.Windows.Forms.Button button_ReibkraftMessen;
        private System.Windows.Forms.Label label10;
    }
}

