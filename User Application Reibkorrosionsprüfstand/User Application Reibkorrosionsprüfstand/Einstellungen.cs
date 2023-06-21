using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinCAT.TypeSystem;

namespace User_Application_Reibkorrosionsprüfstand
{
    public partial class Form_Einstellungen : Form
    {
        public bool bMilliohmmeterVerbunden = false;
        public bool bKraftsensorVerbunden = false;
        public bool bDigitalanzeigeVerbunden = false;
        public bool bTemperaturreglerVerbunden = false;
        public bool bReibkraftmessungAktiv = false;

        public string sComPortMilliohmmeter;
        public string sComPortKraftsensor;
        public string sComPortDigitalanzeige;
        public string sComPortTemperaturregler;

        public int iBaudrateMilliohmmeter;
        public int iBaudrateDigitalanzeige;
        public int iBaudrateTemperaturregler;

        public int iAdresseTemperaturregler;
        public int iHystereseTemperauturregler;


        public Form_Einstellungen()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Aktuellen Einstellungen aus der Config-Datei hinterlegen
            ComboBox_ComPortMilliohmmeter.Text = sComPortMilliohmmeter;
            ComboBox_ComPortKraftsensor.Text = sComPortKraftsensor;
            ComboBox_ComPortDigitalanzeige.Text = sComPortDigitalanzeige;
            ComboBox_ComPortTemperaturregler.Text = sComPortTemperaturregler;

            ComboBox_BaudrateMilliohmmeter.Text = iBaudrateMilliohmmeter.ToString();
            ComboBox_BaudrateDigitalanzeige.Text = iBaudrateDigitalanzeige.ToString();
            ComboBox_BaudrateTemperaturregler.Text = iBaudrateTemperaturregler.ToString();

            numericUpDown_AdresseTemperaturregler.Value = iAdresseTemperaturregler;
            numericUpDown_HystereseTemperatur.Value = iHystereseTemperauturregler;

            checkBox_ReibkraftmessungAktiv.Checked = bReibkraftmessungAktiv;

            // Einstellen welche Boxen verändert werden dürfen
            if (bMilliohmmeterVerbunden == true)
            {
                ComboBox_ComPortMilliohmmeter.Enabled = false;
                ComboBox_BaudrateMilliohmmeter.Enabled = false;
            }
            else
            {
                ComboBox_ComPortMilliohmmeter.Enabled = true;
                ComboBox_BaudrateMilliohmmeter.Enabled = true;
            }

            if (bKraftsensorVerbunden == true)
            {
                ComboBox_ComPortKraftsensor.Enabled = false;
            }
            else
            {
                ComboBox_ComPortKraftsensor.Enabled = true;
            }

            if (bDigitalanzeigeVerbunden == true)
            {
                ComboBox_ComPortDigitalanzeige.Enabled = false;
                ComboBox_BaudrateDigitalanzeige.Enabled = false;
            }
            else
            {
                ComboBox_ComPortDigitalanzeige.Enabled = true;
                ComboBox_BaudrateDigitalanzeige.Enabled = true;
            }

            if (bTemperaturreglerVerbunden == true)
            {
                ComboBox_ComPortTemperaturregler.Enabled = false;
                ComboBox_BaudrateTemperaturregler.Enabled = false;

                numericUpDown_AdresseTemperaturregler.Enabled = false;
            }
            else
            {
                ComboBox_ComPortTemperaturregler.Enabled = true;
                ComboBox_BaudrateTemperaturregler.Enabled = true;

                numericUpDown_AdresseTemperaturregler.Enabled = true;
            }
        }

        private void Button_Speichern_Click(object sender, EventArgs e)
        {
            sComPortMilliohmmeter = ComboBox_ComPortMilliohmmeter.Text;
            sComPortKraftsensor = ComboBox_ComPortKraftsensor.Text;
            sComPortDigitalanzeige = ComboBox_ComPortDigitalanzeige.Text;

            iBaudrateMilliohmmeter = Int32.Parse(ComboBox_BaudrateMilliohmmeter.Text);
            iBaudrateDigitalanzeige = Int32.Parse(ComboBox_BaudrateDigitalanzeige.Text);

            iAdresseTemperaturregler = Convert.ToInt16(numericUpDown_AdresseTemperaturregler.Value);

            iHystereseTemperauturregler = Convert.ToInt16(numericUpDown_HystereseTemperatur.Value);

            bReibkraftmessungAktiv = checkBox_ReibkraftmessungAktiv.Checked;
        }
    }
}
