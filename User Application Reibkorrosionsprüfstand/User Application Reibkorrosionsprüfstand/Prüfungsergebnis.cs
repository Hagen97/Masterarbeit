using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User_Application_Reibkorrosionsprüfstand
{
    public partial class Prüfungsergebnis : Form
    {
        public string strStatus = string.Empty;

        public string strGrenzwertZyklen = string.Empty;
        public string strErreichteZyklen = string.Empty;
        public string strGrenzwertWiderstand = string.Empty;
        public string strErreichterWiderstand = string.Empty;
        public string strPruefungsdauer = string.Empty;

        public Prüfungsergebnis()
        {
            InitializeComponent();
        }

        private void Prüfungsergebnis_Load(object sender, EventArgs e)
        {
            textBox_Status.Text = strStatus;
            
            if (strGrenzwertZyklen != null)
            {
                label_GrenzwertZyklen.Text = strGrenzwertZyklen;
                label_ErreichteZyklen.Text = strErreichteZyklen;
                label_GrenzwertWiderstand.Text = strGrenzwertWiderstand;
                label_ErreichteWiderstand.Text = strErreichterWiderstand;

                label_Pruefungsdauer.Text = strPruefungsdauer;
            }
            
        }
    }
}
