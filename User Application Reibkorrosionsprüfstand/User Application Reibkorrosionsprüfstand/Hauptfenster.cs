using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using TwinCAT.Ads;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Nocksoft.IO.ConfigFiles;
using static System.Net.WebRequestMethods;
using Sample;   // c# Datei für die Anbindung der 9206.dll
using System.Security.Cryptography.X509Certificates;
using System.IO.Ports;
using System.Web;
using System.Net.Http;
using TwinCAT.Ads.TypeSystem;
using System.Diagnostics;
using System.Timers;
using System.Linq.Expressions;
using System.Data.Common;
using System.Media;
using System.Net.Security;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.AxHost;
using System.Security.Cryptography;
using TwinCAT.TypeSystem;

namespace User_Application_Reibkorrosionsprüfstand
{
    public partial class Hauptfenster : Form
    {
        /* Private Variablen hier anlegen */

        //-----------------------------------------------------
        // Variablen für TwinCAT
        //-----------------------------------------------------
        // Variabeln für die Kommunikation mit TwinCAT
        private TcAdsClient tcClient;
        private AdsStream dataStream;
        private BinaryReader binReader;

        private const int iZyklusZeitTwinCAT = 20;    // Zykluszeit der Abfrage von TwinCAT in ms
        private bool bTwinCATVerbunden = false;
        
        // Timer für den Timeout von TwinCAT
        private static System.Timers.Timer Timer_TimeoutTwinCAT;
        private int iTimeTimeoutTwinCAT = 2000;   // Timeout 250 ms
        private int iTimeTimeoutServomotor = 4000;  // Timeout 2 Sekunde
        private bool bTimeoutTwinCAT = false;

        // Timer um auf Reaktion von TwinCAT zu warten
        private static System.Timers.Timer Timer_WartezeitReaktionTwinCAT;
        private int iTimeWartezeitReaktionTwinCAT_Init = 500;           // Wartezeit bei der Initialisierung
        private int iTimeWartezeitReaktionTwinCAT_Antwort = 50;         // Wartezeit bei normalen Aufrufen
        private bool bWartezeitReaktionTwinCATAbgelaufen = false;

        // Variablen für den Motor
        private bool bNotAus = false;
        private bool bNotAusQuittiert = false;

        // Variablen und Handles für den Austausch mit TwinCAT
        private bool bThreadAktiv = false;

        // MC Motor
        private int hSetMotorEinschalten;
        private int hSetDrehrichtungPositiv;
        private int hSetDrehrichtungNegativ;
        private int hGetMotorEingeschaltet;

        private bool bSetMotorEinschalten = false;
        private bool bSetDrehrichtungRechts = false;
        private bool bSetDrehrichtungLinks = false;
        private bool bGetMotorEingeschaltet = false;

        // Set Position
        private int hSetPosition;
        private int hSetClearPositionLag;
        private int hSetPositionValue;
        private int hGetPositionGesetzt;

        private double dSetIstPosition = 0.0;      // Ist Position setzen
        private bool bSetClearPositionLag = false;
        private bool bGetPositionGesetzt = false;

        // Move Absolute
        private int hSetFahrbefehl;
        private int hSetGeregelteFahrt;
        private int hSetFirstZielposition;
        private int hSetSecondZielposition;
        private int hSetThirdZielposition;
        private int hSetFirstZielgeschwindigkeit;
        private int hSetSecondZielgeschwindigkeit;
        private int hSetThirdZielgeschwindigkeit;
        private int hSetAcceleration;
        private int hSetDeceleration;
        private int hGetFahrbefehlGestartet;
        private int hGetFahrbefehlAbgeschlossen;

        private bool bSetUngeregelteFahrbefehl = false;
        private bool bSetGereglteFahrbefehl = false;
        private double dSetFirstZielposition = 0.0;                  // Vorgabe wie viel Grad der Motor drehen soll
        private double dSetSecondZielposition = 0.0;
        private double dSetThirdZielposition = 0.0;
        private double dSetFirstZielgeschwindigkeit = 0.0;
        private double dSetSecondZielgeschwindigkeit = 0.0;
        private double dSetThirdZielgeschwindigkeit = 0.0;
        private double dSetAcceleration = 0.0;
        private double dSetDeceleration = 0.0;
        private bool bGetFahrbefehlGestartet = false;
        private bool bGetFahrbefehlAbgeschlossen = false;

        // MC Stop
        private int hSetAchseStop;
        private int hGetAchseWirdGestoppt;
        private int hGetAchseGestoppt;

        private bool bGetAchseWirdGestoppt = false;
        private bool bGetAchseGestoppt = false;

        // Get Variablen
        private int hGetNotAus;
        private int hGetNotAusQuittiert;
        private int hGetPneumatikdruckVorhanden;
        private int hGetDruckluftZugeschaltet;
        private int hGetMesstasterVorhanden;
        private int hGetMesstasterGenullt;
        private int hGetAbdeckungGeschlossen;
        private int hGetKnebelschalterEinrichten;
        private int hGetKnebelschalterBetrieb;
        private int hGetMotorposition;
        private int hGetHeizpatronenZugeschaltet;
        private int hGetHeizpatronenMasseGeschaltet;

        private int hGetPositonsFlanke;

        private bool bGetNotAus = false;
        private bool bGetNotAusQuittiert = false;
        private bool bGetPneumatikdruckVorhanden = false;
        private bool bGetDruckluftZugeschaltet = false;
        private bool bGetMesstasterVorhanden = false;
        private bool bGetMesstasterGenullt = false;
        private bool bGetAbdeckungGeschlossen = false;
        private bool bGetKnebelschalterEinrichten = false;
        private bool bGetKnebelschalterBetrieb = false;
        private double dGetMotorposition = 0.0;
        private bool bGetHeizpatronenZugeschaltet = false;
        private bool bGetHeizpatronenMasseGeschaltet = false;

        private bool bGetPositonsFlanke = false;

        // Set Variablen
        private int hSetConnect;
        private int hSetLeuchtmelderBereit;
        private int hSetLeuchtmelderStoerung;
        private int hSetDruckluftAbschalten;
        private int hSetResetTwinCAT;
        private int hSetHeizpatronenFreigeben;
        private int hSetMesstasterNullen;
        private int hSetHeizpatronenMasseSchalten;

        private int hSetPositionsFlankeErzeugen;
        private int hSetPositonFuerFlanke;

        private bool bSetConnectTwinCAT = false;
        private bool bSetLeuchtmelderBereit = false;
        private bool bSetLeuchtmelderStoerung = false;
        private bool bSetDruckluftAbschalten = false;
        private bool bSetResetTwinCAT;
        private bool bSetHeizungFreigeben = false;
        private bool bMesstasterNullen = false;
        private bool bSetHeizpatronenMasseSchalten = false;

        private bool bSetPositionsFlankeErzeugen = false;
        private double dSetPositonFuerFlanke = 0.0;




        private bool bPruefungMitTemperatur = true;
        private bool bReibkraftmessungAktiviert = true;


        //-----------------------------------------------------
        // Fehlerstatus Variablen
        //-----------------------------------------------------
        // Timer zur zyklischen Abfrage, ob ein Fehler vorliegt, wenn das Programm im Idle Betrieb ist
        private static System.Timers.Timer Timer_IdleBetrieb;
        private int iTimerIdleBetrieb = 50;   // 50 ms Wartezeit
        private bool bTimerIdleBetriebAbgelaufen = false;
        private bool bFehlerbehandlungSicherheitseinrichtungenAbgeschlossen = false;              // Merker, dass auf einen Fehler bereits reagiert wurde

        private bool bErrorSicherheitseinrichtung = false;

        // Fehler Sicherheitseinrichtungen
        private bool bErrorNotAus = false;
        private bool bErrorAbdeckungGeschlossen = false;
        private bool bErrorMesstasterVorhanden = false;
        private bool bErrorPneumatikdruckVorhanden = false;
        private bool bErrorVentileEingeschaltet = false;
        private bool bErrorKnebelschalter = false;

        // Fehler der Kommunikationspartner
        private bool bMotorFehler = false;
        private bool bErrorTwinCAT = false;
        private bool bErrorDigitalanzeige = false;
        private bool bErrorMilliohmmeter = false;
        private bool bErrorKraftsensor = false;
        private bool bErrorTemperaturregler = false;

        private bool bErrorResetAbgeschlossen = false;

        private bool bErrorErstelleCSV = false;
        private bool bErrorSchreibeCSV = false;
        private bool bErstelleCSVAbgebrochen = false;

        string sExceptionText;

        //-----------------------------------------------------
        // Variablen für die Config-Datei
        //-----------------------------------------------------
        private INIFile ConfigFile;

        private bool bPruefungsbeschreibungGeladen = false;

        private string strFilenameINI = "Config.ini";
        private string strSectionLetztePruefungsbeschreibung = "LetztePruefungsbeschreibung";
        private string strSectionEinstellungen = "Einstellungen";
        private string strSectionMilliohmmeter = "Milliohmmeter";
        private string strSectionKraftsensor = "Kraftsensor";
        private string strSectionDigitalanzeige = "Digitalanzeige";
        private string strSectionTemperaturregler = "Temperaturregler";
        private string strLetzteDateipfad;
        private string strDateipfadCSV;




        //-----------------------------------------------------
        // Variablen für die csv Datei
        //-----------------------------------------------------
        FileStream fsFileCSV;
        StreamWriter swFileWriterPruefergebnisse;
        StreamWriter swFileWriterReibwerte;
        StreamWriter swFileCopyWriter;
        StreamReader srFileReader;

        private bool bFileCSVPruefergebnisseOffen = false;
        private bool bFileCSVReibwerteOffen = false;
        private bool bFileCopyCSVOffen = false;

        private string strFilenameCSV;
        private string strDateinameCSV;
        private string strFilenameCopyCSV;
        private string strFilenameReibwerteCSV;
        private string TAB = ";";   // Steuerzeichen für die nächste Spalte in der csv-Datei


        //-----------------------------------------------------
        // Variablen für das Einstellen des Hubs
        //-----------------------------------------------------
        private bool bFensterHubEinstellenOffen = false;
        private bool bHubEingestellt = false;               // Merker, dass mit der Checkbox dies bestätigt wurde
        private bool bHubeinstellungAbgeschlossen = false;
        private bool bStartpunktAngefahren = false;
        private bool bStartpunktAnfahrenAbbrechen = false;
        private int iAktuellerHub = 0;

        //-----------------------------------------------------
        // Variablen für die Serielle Kommunikation
        //-----------------------------------------------------
        // Steuerzeichen
        private string STX = (Convert.ToChar(2)).ToString();
        private string ETX = (Convert.ToChar(3)).ToString();
        private string EOT = (Convert.ToChar(4)).ToString();
        private string ENQ = (Convert.ToChar(5)).ToString();
        private string ACK = (Convert.ToChar(6)).ToString();
        private string LF = (Convert.ToChar(10)).ToString();
        private string CR = (Convert.ToChar(13)).ToString();
        private string NAK = (Convert.ToChar(21)).ToString();

        private int ACK_Dig = 6;
        private int NAK_Dig = 21;

        //-----------------------------------------------------
        // Variablen Digitalanzeige
        //-----------------------------------------------------
        private static System.Timers.Timer Timer_DigitalanzeigeWartezeitVorAnfrage;  // Wartezeit, bevor die Anzeige angesprochen wird
        private int iTimeWartezeitVorAnfrage = 50;
        private bool bTimerWartezeitVorAbfrageAbgelaufen = false;

        private static System.Timers.Timer Timer_DigitalanzeigeWartezeitVorAuslesen;  // Wartezeit, bevor das Ergebnis der Anfrage ausgelesen wird
        private int iTimeWartezeitVorAuslesen = 50;
        private bool bTimerWartezeitVorAuslesenAbgelaufen = false;

        private string strCOMPortDigitalanzeige;
        private int iBaudrateDigitalanzeige;
        private bool bDigitalanzeigeVerbunden = false;
        private string strDigitalanzeigeAdresse = "11";        // In den Einstellenungen der Digitalanzeige vergeben
        private double dMesswertDigitalanzeige;
        private bool bKommunikationsfehlerAufgetreten = false;
        private string strTeststring = "";

        //-----------------------------------------------------
        // Variablen Milliohmmeter
        //-----------------------------------------------------
        // Timer für die Wartezeit bei der Widerstandsmessung
        private static System.Timers.Timer Timer_WartezeitWiderstandsmessung;
        private int iTimerWartezeitWiderstandsmessung = 250;   // Timeout 250 ms
        private bool bWartezeitWiderstandsmessungAbgelaufen = false;

        // Antworten vom Milliohmmeter
        private const int iEinzelmessung = 0;
        private const int iDauermessung = 1;

        private const int i20mVBegrenzungAus = 0;
        private const int i20mVBegrenzungEin = 1;

        private const int iMessungAbgeschlossen = 101;
        private const int iMessungNichtAbgeschlossen = 102;

        // Zusätzliche Variablen
        private string strCOMPortMilliohmmeter;
        private int iBaudrateMilliohmmeter;
        private bool bMilliohmmeterVerbunden = false;   // Merker ob Milliohmmeter verbunden ist
        private bool bWiderstandUeberprueft = false;    // Merker ob der Widerstand überprüft wurde

        private bool bTimerWiderstandsmessungAbgelaufen = false;
        private int iMesszeitMilliohmmeter;

        //-----------------------------------------------------
        // Variablen Kraftsensor
        //-----------------------------------------------------
        // Timer nach der einmalig die Reibkraft gemessen werden soll
        private static System.Timers.Timer Timer_ReibkraftmessungEnde;
        private int iTimeReibkraftmessungEnde = 2000;    // 2 Sekunden
        private bool bTimerReibkraftmessungEndeErreicht = false;

        private string strCOMPortKraftsensor;
        private string strPortNummerKraftsensor;
        private int iPortNummerKraftsensor;
        private int iKraftsensorHandle = 0; // Handle für die Verbindung mit dem Kraftsensor
        private int iReturnCode = 0;        // Rückgabewert aus der API9206
        private bool bKraftsensorVerbunden = false;
        private bool bReibkraftmessungStandardAktiv = false;        // Merker, ob Reibkraftmessung standardmäßig aktiv sein soll

        private float fTaraWertKraftsensor = 0;

        // Drehgeschwindigkeit, wenn die Reibkraft gemessen werden soll
        private double dGeschwindigkeitServomotorReibkraftmessung = 400;

        //-----------------------------------------------------
        // Variablen Temperaturregler
        //-----------------------------------------------------
        // Timer für Timeout bei Start der Kommunikation
        private static System.Timers.Timer Timer_TimeoutTemperaturregler;
        private int iTimeTimeoutTemperaturregler = 500;     // 500 ms für Timeout
        private bool bTimeoutTemperaturregler = false;

        // Timer um einige Zeit zu warten, ehe die Temperatur neu ausgelesen wird
        private static System.Timers.Timer Timer_WartezeitTemperaturmessung;
        private int iWartezeitTemperaturmessung = 1000;     // 1 s
        private bool bWartezeitTemperaturmessungAbgelaufen = false;

        // Timer um den Temperaturregler im Idle Betrieb abzufragen
        private static System.Timers.Timer Timer_WartezeitTemperaturmessungIdlebetrieb;
        private int iTime_WartezeitTemperaturmessungIdlebetrieb = 1000; // Wartezeit von 2 Sekunden 2000
        private bool bTimerTemperaturmessungAusgeloest = false;

        private string strCOMPortTemperaturregler;
        private int iBaudrateTemperaturregler;
        private bool bTemperaturreglerVerbunden = false;
        private byte byAdresseTemperaturregler;
        private int iAdresseTemperaturregler;
        private ushort usModbusAdresse;                     // Modbus Adresse für den Wert der ausgelesen werden soll
        private ushort usAnzahlRegisterRead;                // Anzahl der Modbus Register, die ausgelesen werden sollen
        private ushort usCRC;                               // CRC für die Modbus Kommunikation
        private ushort usDatenTemperaturregler;
        private ushort usWertSchreibenTemperaturregler = 0;

        private int iHystereseTemperaturregler = 0;
        private double dSollTemperaturHeizpatroneOben = 0.0;
        private double dSollTemperaturHeizpatroneUnten = 0.0;
        private double dOffsetTemperaturOben = 0.0;
        private double dOffsetTemperaturUnten = 0.0;
        private double dIstTemperaturProbeOben = 0.0;
        private double dIstTemperaturProbeUnten = 0.0;

        private bool bButtonProbenAufheizenAktiv = false;
        private bool bTemperaturregelungAktiv = false;

        private bool TestCSVOffen = false;

        //-----------------------------------------------------
        // Variablen Prüfungsdurchführung
        //-----------------------------------------------------
        // Timer für die Wartezeit der Oxidation
        private static System.Timers.Timer Timer_WartezeitOxidation;
        private int iTimerWartezeitOxidation = 250;   // Timeout 250 ms
        private bool bWartezeitOxidationAbgelaufen = false;
        private bool bButtonPruefungAbbrechen = false;
        private bool bButtonPruefungPause = false;
        private bool bButtoncsvKopieren = false;

        private double dZielgeschwindigkeit1 = 0.0;
        private double dZielgeschwindigkeit2 = 0.0;
        private double dZielgeschwindigkeit3 = 0.0;
        private double dZielgeschwindigkeitGap1zu2 = 0;     // Feste Differenz zwischen Geschwindigkeit 1 und 2
        private double dZielgeschwindigkeitGap1zu3 = 0;      // Feste Differenz zwischen Geschwindigkeit 1 und 3

        private bool bPruefungAktiv = false;
        private bool bGeregelteFahrt = false;       // Merker ob geregelte oder ungeregelte Fahrt durchgeführt werden soll

        //-----------------------------------------------------
        // Variablen Prüfungsergebnis als string
        //-----------------------------------------------------
        private string strGrenzwertZyklen = "";
        private string strErreichteZyklen = "";
        private string strGrenzwertWiderstand = "";
        private string strLetzteWiderstandswert = "";
        private string strPruefungsdauer = "";

        //-----------------------------------------------------
        // Sonstige Variablen
        //-----------------------------------------------------
        double dStartpositionMotor = 0.0;   // Ermittelte Referenzpunkt des Motors

        Color colorStandardWeiß = Color.FromArgb(255, 255, 255, 255);
        Color colorStandardGrau = Color.FromArgb(255, 240, 240, 240);

        

        public Hauptfenster()
        {
            InitializeComponent();
        }

        //-----------------------------------------------------
        // Wird als erstes beim Starten des Programms aufgerufen
        //-----------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            // Timer für die Wartezeit auf eine Reaktion von TwinCAT initialisieren
            Timer_WartezeitReaktionTwinCAT = new System.Timers.Timer(iTimeWartezeitReaktionTwinCAT_Antwort);    // Timer anlegen
            Timer_WartezeitReaktionTwinCAT.Elapsed += OnTimer_WartezeitInitTwinCAT;
            Timer_WartezeitReaktionTwinCAT.AutoReset = false;
            Timer_WartezeitReaktionTwinCAT.Enabled = false;

            //-----------------------------------------------------
            // Config-Datei öffnen und auslesen
            //-----------------------------------------------------
            #region Config-Datei auslesen
            // Config-Datei öffnen
            try
            {
                ConfigFile = new INIFile(strFilenameINI);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Config-Datei wurde nicht gefunden.");
            }

            /* Daten aus der Config-Datei auslesen */
            // Element Dateipfad auslesen
            try
            {
                string sElement = "Dateipfad";
                strLetzteDateipfad = ConfigFile.GetValue(strSectionEinstellungen, sElement);
                if (strLetzteDateipfad == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element COMPort Milliohmmeter auslesen
            try
            {
                string sElement = "COMPortMilliohmmeter";
                strCOMPortMilliohmmeter = ConfigFile.GetValue(strSectionMilliohmmeter, sElement);
                if (strCOMPortMilliohmmeter == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element Baudrate Milliohmmeter auslesen
            try
            {
                string sElement = "BaudrateMilliohmmeter";
                string value = ConfigFile.GetValue(strSectionMilliohmmeter, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                // String in int wandeln
                try
                {
                    iBaudrateMilliohmmeter = Int32.Parse(value);
                }
                catch
                {
                    sExceptionText = "Eintrag von " + sElement + " kann nicht in eine Zahl gewandelt werden.";
                    MessageBox.Show(sExceptionText);
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element Messzeit des Milliohmmeter auslesen
            try
            {
                string sElement = "Messzeit";
                string value = ConfigFile.GetValue(strSectionMilliohmmeter, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                // String in int wandeln
                try
                {
                    iMesszeitMilliohmmeter = Int32.Parse(value);
                }
                catch
                {
                    sExceptionText = "Eintrag von " + sElement + " kann nicht in eine Zahl gewandelt werden.";
                    MessageBox.Show(sExceptionText);
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element COMPort Kraftsensor auslesen
            try
            {
                string sElement = "COMPortKraftsensor";
                strCOMPortKraftsensor = ConfigFile.GetValue(strSectionKraftsensor, sElement);
                if (strCOMPortKraftsensor == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element, Reibkraftmessung Standardmäßig Aktiv auslesen
            try
            {
                string sElement = "ReibkraftmessungAktiv";
                string value = ConfigFile.GetValue(strSectionKraftsensor, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }
                else
                {
                    // String in int wandeln
                    try
                    {
                        // Messung nicht aktiv?
                        if (Byte.Parse(value) == 0)
                        {
                            checkBox_ReibkraftMessen.Checked = false;
                            bReibkraftmessungStandardAktiv = false;
                        }
                        else
                        {
                            checkBox_ReibkraftMessen.Checked = true;
                            bReibkraftmessungStandardAktiv = true;
                        }
                    }
                    catch
                    {
                        sExceptionText = "Eintrag von " + sElement + " kann nicht in eine Zahl gewandelt werden.";
                        MessageBox.Show(sExceptionText);
                    }
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element COMPort Digitalanzeige auslesen
            try
            {
                string sElement = "COMPortDigitalanzeige";
                strCOMPortDigitalanzeige = ConfigFile.GetValue(strSectionDigitalanzeige, sElement);
                if (strCOMPortDigitalanzeige == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element Baudrate Digitalanzeige auslesen
            try
            {
                string sElement = "BaudrateDigitalanzeige";
                string value = ConfigFile.GetValue(strSectionDigitalanzeige, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                // String in int wandeln
                try
                {
                    iBaudrateDigitalanzeige = Int32.Parse(value);
                }
                catch
                {
                    sExceptionText = "Eintrag von " + sElement + " kann nicht in eine Zahl gewandelt werden.";
                    MessageBox.Show(sExceptionText);
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element COMPort Temperaturregler auslesen
            try
            {
                string sElement = "COMPortTemperaturregler";
                strCOMPortTemperaturregler = ConfigFile.GetValue(strSectionTemperaturregler, sElement);
                if (strCOMPortTemperaturregler == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element Baudrate Temperaturregler auslesen
            try
            {
                string sElement = "BaudrateTemperaturregler";
                string value = ConfigFile.GetValue(strSectionTemperaturregler, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                // String in int wandeln
                try
                {
                    iBaudrateTemperaturregler = Int32.Parse(value);
                }
                catch
                {
                    sExceptionText = "Eintrag von " + sElement + " kann nicht in eine Zahl gewandelt werden.";
                    MessageBox.Show(sExceptionText);
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element Adresse des Temperaturregler auslesen
            try
            {
                string sElement = "AdresseTemperaturregler";
                string value = ConfigFile.GetValue(strSectionTemperaturregler, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                // String in byte wandeln
                try
                {
                    byAdresseTemperaturregler = Byte.Parse(value);
                }
                catch
                {
                    sExceptionText = "Eintrag von " + sElement + " kann nicht in ein Byte gewandelt werden.";
                    MessageBox.Show(sExceptionText);
                }

                // String in int wandeln
                try
                {
                    iAdresseTemperaturregler = Int32.Parse(value);
                }
                catch
                {
                    sExceptionText = "Eintrag von " + sElement + " kann nicht in eine Zahl gewandelt werden.";
                    MessageBox.Show(sExceptionText);
                } 
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element Hysterese Temperaturregler auslesen
            try
            {
                string sElement = "HystereseTemperaturregler";
                string value = ConfigFile.GetValue(strSectionTemperaturregler, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                // String in int wandeln
                try
                {
                    iHystereseTemperaturregler = Int32.Parse(value);
                }
                catch
                {
                    sExceptionText = "Eintrag von " + sElement + " kann nicht in eine Zahl gewandelt werden.";
                    MessageBox.Show(sExceptionText);
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            #endregion

            //-----------------------------------------------------
            // Verbindung mit TwinCAT herstellen
            //-----------------------------------------------------
            // Timer Timeout TwinCAT initialisieren
            Timer_TimeoutTwinCAT = new System.Timers.Timer(iTimeTimeoutTwinCAT);    // Timer anlegen
            Timer_TimeoutTwinCAT.Elapsed += OnTimer_TimeoutTwinCAT;
            Timer_TimeoutTwinCAT.AutoReset = false;
            Timer_TimeoutTwinCAT.Enabled = false;

            Connect_TwinCAT();

            // TwinCAT verbunden und kein Fehler aufgetreten?
            if (bTwinCATVerbunden == true && bErrorTwinCAT == false)
            {
                Button_TwinCATVerbinden.BackColor = Color.Green;

                if (bGetHeizpatronenZugeschaltet == false)
                {
                    Visualisierung_HeizpatronenZugeschaltet.BackColor = Color.Red;
                }
                else
                {
                    Visualisierung_HeizpatronenZugeschaltet.BackColor = Color.Green;
                }
            }
            else
            {
                Button_TwinCATVerbinden.Enabled = true;
                Button_TwinCATVerbinden.BackColor = Color.Red;
            }


            //-----------------------------------------------------
            // Verbindung mit dem Milliohmmeter herstellen
            //-----------------------------------------------------
            Milliohmmeter_Verbinden();

            //-----------------------------------------------------
            // Verbindung mit der Digitalanzeige herstellen
            //-----------------------------------------------------
            // Motor einschalten
            //tcClient.WriteAny(hSetDrehrichtungPositiv, true);
            //tcClient.WriteAny(hSetDrehrichtungNegativ, true);
            //tcClient.WriteAny(hSetMotorEinschalten, true);

            // Timer für die Digitalanzeige, bevor das Ergebnis der Anfrage ausgelesen wird
            Timer_DigitalanzeigeWartezeitVorAuslesen = new System.Timers.Timer(iTimeWartezeitVorAuslesen);
            Timer_DigitalanzeigeWartezeitVorAuslesen.Elapsed += OnTimer_DigitalanzeigeWartezeitVorAuslesen;
            Timer_DigitalanzeigeWartezeitVorAuslesen.AutoReset = false;
            Timer_DigitalanzeigeWartezeitVorAuslesen.Enabled = false;

            Digitalanzeige_Verbinden();

            //-----------------------------------------------------
            // Verbindung mit dem Temperaturregler herstellen
            //-----------------------------------------------------
            Temperaturregler_Verbinden();

            //-----------------------------------------------------
            // Verbindung mit dem Kraftsensor herstellen
            //-----------------------------------------------------
            Kraftsensor_Verbinden();
        }

        //-----------------------------------------------------
        // wird nach dem Aufbau der GUI aufgerufen
        //-----------------------------------------------------
        private void Form1_LoadComplete(object sender, EventArgs e)
        {
            // Button Hub einstellen freigeben, wenn alle anderen Geräte verbunden sind
            if (bTwinCATVerbunden == true && bKraftsensorVerbunden == true && bTemperaturreglerVerbunden == true && bMilliohmmeterVerbunden == true)
            {
                Button_HubEinstellen.Enabled = true;
            }

            //-----------------------------------------------------
            // Timer initialisieren
            //-----------------------------------------------------
            // Timer Wartezeit Widerstandsmessung initialisieren
            Timer_WartezeitWiderstandsmessung = new System.Timers.Timer(iTimerWartezeitWiderstandsmessung);    // Timer anlegen
            Timer_WartezeitWiderstandsmessung.Elapsed += OnTimer_WartezeitWiderstandsmessung;
            Timer_WartezeitWiderstandsmessung.AutoReset = false;
            Timer_WartezeitWiderstandsmessung.Enabled = false;

            // Timer Wartezeit Oxidation initialisieren
            Timer_WartezeitOxidation = new System.Timers.Timer(iTimerWartezeitOxidation);    // Timer anlegen
            Timer_WartezeitOxidation.Elapsed += OnTimer_WartezeitOxidation;
            Timer_WartezeitOxidation.AutoReset = false;
            Timer_WartezeitOxidation.Enabled = false;

            // Timer Wartezeit Temperaturmessung
            Timer_WartezeitTemperaturmessung = new System.Timers.Timer();
            Timer_WartezeitTemperaturmessung.Elapsed += OnTimer_WartezeitTemperaturmessung;
            Timer_WartezeitTemperaturmessung.AutoReset = false;
            Timer_WartezeitTemperaturmessung.Enabled = false;

            // Timer Wartezeit Temperaturmessung Idlebetrieb
            Timer_WartezeitTemperaturmessungIdlebetrieb = new System.Timers.Timer();
            Timer_WartezeitTemperaturmessungIdlebetrieb.Elapsed += OnTimer_WartezeitTemperaturmessungIdlebetrieb;
            Timer_WartezeitTemperaturmessungIdlebetrieb.Interval = iTime_WartezeitTemperaturmessungIdlebetrieb;
            Timer_WartezeitTemperaturmessungIdlebetrieb.AutoReset = false;
            Timer_WartezeitTemperaturmessungIdlebetrieb.Enabled = false;

            // Timer für Idle Betrieb
            Timer_IdleBetrieb = new System.Timers.Timer();
            Timer_IdleBetrieb.Elapsed += OnTimer_IdelBetrieb;
            Timer_IdleBetrieb.AutoReset = false;
            Timer_IdleBetrieb.Enabled = false;

            // Timer für die Wartzeit, dass der Empfang von Daten des Temperaturreglers abgeschlossen ist


            // Timer für Reibkraftmessung
            Timer_ReibkraftmessungEnde = new System.Timers.Timer(iTimeReibkraftmessungEnde);    // Timer anlegen
            Timer_ReibkraftmessungEnde.Elapsed += OnTimer_ReibkraftmessungEnde;
            Timer_ReibkraftmessungEnde.AutoReset = false;
            Timer_ReibkraftmessungEnde.Enabled = false;

            // Timer bevor die Digitalanzeige angesprochen wird
            Timer_DigitalanzeigeWartezeitVorAnfrage = new System.Timers.Timer(iTimeWartezeitVorAnfrage);
            Timer_DigitalanzeigeWartezeitVorAnfrage.Elapsed += OnTimer_DigitalanzeigeWartezeitVorAbfrage;
            Timer_DigitalanzeigeWartezeitVorAnfrage.AutoReset = false;
            Timer_DigitalanzeigeWartezeitVorAnfrage.Enabled = false;

            

            //-----------------------------------------------------
            // Fehler rücksetzen
            //-----------------------------------------------------
            if (bGetNotAus == true)
            {
                bErrorNotAus = false;
            }

            if (bGetAbdeckungGeschlossen == true)
            {
                bErrorAbdeckungGeschlossen = false;
            }

            if (bGetMesstasterVorhanden == true)
            {
                bErrorMesstasterVorhanden = false;
            }

            if (bGetPneumatikdruckVorhanden == true)
            {
                bErrorPneumatikdruckVorhanden = false;
            }

            if (bGetDruckluftZugeschaltet == true)
            {
                bErrorVentileEingeschaltet = false;
            }

            // Fehler nicht mehr vorhanden?
            if (bGetNotAus == true && bGetPneumatikdruckVorhanden == true && bGetDruckluftZugeschaltet == true && (bGetKnebelschalterEinrichten == true || bGetAbdeckungGeschlossen == true))
            {
                // Fehler zurücksetzen
                bErrorNotAus = false;
                bErrorAbdeckungGeschlossen = false;
                bErrorPneumatikdruckVorhanden = false;
                bErrorVentileEingeschaltet = false;
                bErrorKnebelschalter = false;
                bErrorSicherheitseinrichtung = false;        // Merker für allgemeinen Fehler Sicherheitseinrichtungen

                bFehlerbehandlungSicherheitseinrichtungenAbgeschlossen = false; // Merker zurücksetzen

                // Alle Geräte weiterhin angeschlossen?
                if (bMilliohmmeterVerbunden == true && bDigitalanzeigeVerbunden == true && bTwinCATVerbunden == true && bTemperaturreglerVerbunden == true && bKraftsensorVerbunden == true)
                {
                    Button_HubEinstellen.Invoke(new Action(() => Button_HubEinstellen.Enabled = true));    // Button für die Hubeinstellung freigeben
                }
                else
                {
                    // Milliohmmeter nicht verbunden?
                    if (bMilliohmmeterVerbunden == false)
                    {
                        Button_MilliohmmeterVerbinden.Enabled = true;
                    }

                    // Kraftsensor nicht verbunden?
                    if (bKraftsensorVerbunden == false)
                    {
                        Button_KraftsensorVerbinden.Enabled = true;
                    }

                    // Digitalanzeige nicht verbunden?
                    if (bDigitalanzeigeVerbunden == false)
                    {
                        Button_DigitalanzeigeVerbinden.Enabled = true;
                    }

                    // TwinCAT nicht verbunden?
                    if (bTwinCATVerbunden == false)
                    {
                        Button_TwinCATVerbinden.Enabled = true;
                    }

                    // Temperaturregler nicht verbunden?
                    if (bTemperaturreglerVerbunden == false)
                    {
                        Button_TemperaturreglerVerbinden.Enabled = true;
                    }
                }
            }

            // Timer für den Idle Betrieb starten
            //Timer_IdleBetrieb.Interval = iTimerIdleBetrieb;
            //Timer_IdleBetrieb.Enabled = true;
        }

        //-----------------------------------------------------
        // wird beim Beenden des Programms aufgerufen
        //-----------------------------------------------------
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ist die Temperaturregelung akitv?
            if (bTemperaturregelungAktiv == true)
            {
                // Regelung stoppen
                usModbusAdresse = 1214;                 // Adresse zum Starten/Stoppen der Regelung
                usWertSchreibenTemperaturregler = 0;
                Modbus_WriteFunction06();
            }

            if (bErrorTwinCAT == false && bTwinCATVerbunden == true)
            {
                // Servomotor eingeschaltet?
                if (bGetMotorEingeschaltet == true)
                {
                    ServoMotor_Ausschalten();
                }

                // Druckluft zuschalten
                try
                {
                    bSetDruckluftAbschalten = false;
                    tcClient.WriteAny(hSetDruckluftAbschalten, bSetDruckluftAbschalten);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                // Masse der Heizpatronen abschalten
                try
                {
                    bSetHeizpatronenMasseSchalten = false;
                    tcClient.WriteAny(hSetHeizpatronenMasseSchalten, bSetHeizpatronenMasseSchalten);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                // Heizpatronen abschalten
                try
                {
                    bSetHeizungFreigeben = false;
                    tcClient.WriteAny(hSetHeizpatronenFreigeben, bSetHeizungFreigeben);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // Warten, solange der Thread für TwinCAT aktiv ist
            while (bThreadAktiv == true)
            {

            }

            try
            {
                // Löschen der Notifications und Handles
                // Deleting of the notifications and handles
                tcClient.DeleteDeviceNotification(hGetNotAus);

                tcClient.DeleteVariableHandle(hSetMotorEinschalten);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // csv-Datei schließen
            try
            {
                if (bFileCSVPruefergebnisseOffen == true)
                {
                    //fsFileCSV.Dispose();
                    swFileWriterPruefergebnisse.Dispose();
                    swFileWriterPruefergebnisse.Close();
                    bFileCSVPruefergebnisseOffen = false;
                }
            }
            catch (Exception _Exception)
            {
                MessageBox.Show("csv-Datei schließen:\n\n" + _Exception.GetType().Name);
            }

            tcClient.Dispose();

            // Verbindung zum Kraftsensor schließen
            if (iKraftsensorHandle > 0)
            {
                API9206.CloseInterface(iKraftsensorHandle);
            }
        }

        

        //-----------------------------------------------------
        // Methode wird aufgerufen, wenn es zu einem Fehler kommt.
        // Fehlerbehandlung bei:
        //                      - Sicherheitseinrichtungen haben ausgelöst
        //                      - Kommunikationsfehler
        //                      - Schreibfehlern
        // Aktuelle Prüfung wird abgebrochen.
        // Rückkehr zum Ausgangszustand
        //-----------------------------------------------------
        private void Fehlerbehandlung()
        {
            bool bPortMilliohmmeterOffen = false;
            bool bPortDigitalanzeigeOffen = false;
            bool bPortTemperaturreglerOffen = false;
            

            bool bFehlerbehandlungAbgeschlossen = false;

            int iCase = 0;

            //-----------------------------------------------------
            // Prüfstand in sicheren Zustand bringen
            //-----------------------------------------------------
            while (bErrorTwinCAT == false && bFehlerbehandlungAbgeschlossen == false)
            {
                switch (iCase)
                {
                    case 0:
                        // Fahrbefehl löschen
                        ServoMotor_FahrbefehlLoeschen();
                        iCase = 1;

                        break;

                    case 1:
                        // Servomotor stoppen
                        ServoMotor_Stop();
                        iCase = 2;

                        break;

                    case 2:
                        // Motor ausschalten
                        ServoMotor_Ausschalten();
                        iCase = 3;

                        break;

                    case 3:
                        // Masse der Heizpatronen abschalten
                        try
                        {
                            bSetHeizpatronenMasseSchalten = false;
                            tcClient.WriteAny(hSetHeizpatronenMasseSchalten, bSetHeizpatronenMasseSchalten);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            bErrorTwinCAT = true;
                        }
                        iCase = 4;

                        break;

                    case 4:
                        // Freigabe der Heizung entziehen
                        try
                        {
                            bSetHeizungFreigeben = false;
                            tcClient.WriteAny(hSetHeizpatronenFreigeben, bSetHeizungFreigeben);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            bErrorTwinCAT = true;
                        }

                        bFehlerbehandlungAbgeschlossen = true;

                        break;
                }
            }

            //-----------------------------------------------------
            // Fehler mit dem Milliohmmeter aufgetreten?
            //-----------------------------------------------------
            if (bErrorMilliohmmeter == true)
            {
                // Case Variablen rücksetzen
                bFehlerbehandlungAbgeschlossen = false;
                iCase = 0;

                while (bFehlerbehandlungAbgeschlossen == false)
                {
                    switch (iCase)
                    {
                        case 0:
                            // Abfragen, ob Port offen ist
                            try
                            {
                                bPortMilliohmmeterOffen = serialPort_Milliohmmeter.IsOpen;
                            }
                            catch (Exception _Exception)
                            {
                                MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                                bFehlerbehandlungAbgeschlossen = true;
                            }
                            iCase = 1;

                            break;

                        case 1:
                            // Port offen?
                            if (bPortMilliohmmeterOffen == true)
                            {
                                try
                                {
                                    serialPort_Milliohmmeter.Close();    // Port schließen
                                }
                                catch (Exception _Exception)
                                {
                                    MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                                }
                            }
                            bFehlerbehandlungAbgeschlossen = true;

                            break;
                    }
                }

                MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + "Verbindung unterbrochen, Port wurde geschlossen.");

                bMilliohmmeterVerbunden = false;

                Button_MilliohmmeterVerbinden.Enabled = true;
                Button_MilliohmmeterVerbinden.BackColor = Color.Red;

                bErrorMilliohmmeter = false;    // Reset da Fehlerbehandlung abgeschlossen
            }

            // Case Variablen rücksetzen
            bFehlerbehandlungAbgeschlossen = false;
            iCase = 0;

            //-----------------------------------------------------
            // Fehler mit dem Kraftsensor aufgetreten?
            //-----------------------------------------------------
            if (bErrorKraftsensor == true)
            {

            }

            //-----------------------------------------------------
            // Fehler mit der Digitalanzeige aufgetreten?
            //-----------------------------------------------------
            if (bErrorDigitalanzeige == true)
            {
                // Case Variablen rücksetzen
                bFehlerbehandlungAbgeschlossen = false;
                iCase = 0;

                while (bFehlerbehandlungAbgeschlossen == false)
                {
                    switch (iCase)
                    {
                        case 0:
                            // Abfragen, ob Port offen ist
                            try
                            {
                                bPortDigitalanzeigeOffen = serialPort_Digitalanzeige.IsOpen; // Abfrage ob Port offen ist
                            }
                            catch (Exception _Exception)
                            {
                                MessageBox.Show("Kommunikation Digitalanzeige:\n\n" + _Exception.GetType().Name);
                                bFehlerbehandlungAbgeschlossen = true;
                            }
                            iCase = 1;

                            break;

                        case 1:
                            // Port offen?
                            if (bPortDigitalanzeigeOffen == true)
                            {
                                try
                                {
                                    serialPort_Digitalanzeige.Close();  // Port schließen
                                }
                                catch (Exception _Exception)
                                {
                                    MessageBox.Show("Kommunikation Digitalanzeige:\n\n" + _Exception.GetType().Name);
                                }
                            }
                            bFehlerbehandlungAbgeschlossen = true;

                            break;
                    }
                }

                MessageBox.Show("Kommunikation Digitalanzeige:\n\n" + "Verbindung unterbrochen, Port wurde geschlossen.");

                bDigitalanzeigeVerbunden = false;   // Merker für Digitalanzeige verbunden zurücksetzen

                Button_DigitalanzeigeVerbinden.BackColor = Color.Red;
                Button_DigitalanzeigeVerbinden.Enabled = true;

                bErrorDigitalanzeige = false;   // Reset da Fehlerbehandlung abgeschlossen
            }

            //-----------------------------------------------------
            // Fehler mit TwinCAT aufgetreten
            //-----------------------------------------------------
            if (bErrorTwinCAT == true)
            {
                // Timeout aufgetreten?
                if (bTimeoutTwinCAT == true)
                {
                    MessageBox.Show("Kommunikation TwinCAT:\n\n" + "Timeout, Verbindung unterbrochen.");
                }
                else
                {
                    MessageBox.Show("Kommunikation TwinCAT:\n\n" + "Unbekannter Fehler, Verbindung unterbrochen.");
                }

                Button_TwinCATVerbinden.BackColor = Color.Red;
                Button_TwinCATVerbinden.Enabled = true;

                bErrorTwinCAT = false;
            }

            //-----------------------------------------------------
            // Fehler mit dem Temperaturregler aufgetreten?
            //-----------------------------------------------------
            if (bErrorTemperaturregler == true)
            {
                // Case Variablen rücksetzen
                bFehlerbehandlungAbgeschlossen = false;
                iCase = 0;

                while (bFehlerbehandlungAbgeschlossen == false)
                {
                    switch (iCase)
                    {
                        case 0:
                            // Abfragen, ob Port offen ist
                            try
                            {
                                bPortTemperaturreglerOffen = serialPort_Temperaturregler.IsOpen;
                            }
                            catch (Exception _Exception)
                            {
                                MessageBox.Show("Kommunikation Temperaturregler:\n\n" + _Exception.GetType().Name);
                                bFehlerbehandlungAbgeschlossen = true;
                            }
                            iCase = 1;

                            break;

                        case 1:
                            // Port offen?
                            if (bPortTemperaturreglerOffen == true)
                            {
                                try
                                {
                                    serialPort_Temperaturregler.Close();    // Port schließen
                                }
                                catch (Exception _Exception)
                                {
                                    MessageBox.Show("Kommunikation Temperaturregler:\n\n" + _Exception.GetType().Name);
                                }
                            }
                            bFehlerbehandlungAbgeschlossen = true;

                            break;
                    }
                }

                MessageBox.Show("Kommunikation Temperaturregler:\n\n" + "Verbindung unterbrochen, Port wurde geschlossen.");

                bTemperaturreglerVerbunden = false;

                Button_TemperaturreglerVerbinden.Enabled = true;
                Button_TemperaturreglerVerbinden.BackColor = Color.Red;

                bErrorTemperaturregler = false;    // Reset da Fehlerbehandlung abgeschlossen
            }


            //-----------------------------------------------------
            // Aufzeigen, welche Sicherheitseinrichtung ausgelöst hat
            //-----------------------------------------------------
            // Hat eine Sicherheitseinrichtung ausgelöst?
            if (bErrorSicherheitseinrichtung == true)
            {
                if (bErrorNotAus == true)
                {
                    MessageBox.Show("Not-Aus wurde gedrückt");
                }

                if (bErrorPneumatikdruckVorhanden == true)
                {
                    MessageBox.Show("Druckluft ist unter Soll gefallen");
                }

                if (bErrorKnebelschalter == true)
                {
                    MessageBox.Show("Knebelschalter wurde umgelegt");
                }

                if (bErrorAbdeckungGeschlossen == true)
                {
                    MessageBox.Show("Abdeckung wurde geöffnet");
                }

                if (bErrorVentileEingeschaltet == true)
                {
                    MessageBox.Show("Durckluftventile waren nicht zugeschaltet");
                }
            }

            //-----------------------------------------------------
            // Reset auf den Startzustand
            // Durchgeführte Schritte müssen wiederholt werden
            //-----------------------------------------------------
            // CSV-Datei: Ergebnisse offen?
            if (bFileCSVPruefergebnisseOffen == true)
            {
                // Datei schließen
                swFileWriterPruefergebnisse.Dispose();
                swFileWriterPruefergebnisse.Close();
                bFileCSVPruefergebnisseOffen = false;
            }

            // CSV-Datei: Kopie der Ergebnisse offen?
            if (bFileCopyCSVOffen == true)
            {
                // Datei schließen
                swFileCopyWriter.Dispose();
                swFileCopyWriter.Close();
                bFileCopyCSVOffen = false;
            }

            // CSV-Datei: Reibwerte offen?
            if (bFileCSVReibwerteOffen == true)
            {
                // Datei schließen
                swFileWriterReibwerte.Dispose();
                swFileWriterReibwerte.Close();
                bFileCSVReibwerteOffen = false;
            }

            bErrorSchreibeCSV = false;

            ResetSoftware();

            //-----------------------------------------------------
            // Alle Geräte verbunden?
            //-----------------------------------------------------
            if (bMilliohmmeterVerbunden == true && bDigitalanzeigeVerbunden == true && bTwinCATVerbunden == true && bTemperaturreglerVerbunden == true && bKraftsensorVerbunden == true)
            {
                // Keine Sicherheitskritischen Fehler?
                Button_HubEinstellen.Enabled = true;    // Button für die Hubeinstellung freigeben
            }
        }

        //-----------------------------------------------------
        // Reset der Software auf Startzustand
        //-----------------------------------------------------
        private void ResetSoftware()
        {
            // Hubeinstellung
            bHubEingestellt = false;
            bHubeinstellungAbgeschlossen = false;                    // Merker der Hub ist nicht eingestellt
            bStartpunktAngefahren = false;                // Merker Startpunkt für die Prüfung wurde nicht gefunden
            bStartpunktAnfahrenAbbrechen = false;
            bFensterHubEinstellenOffen = false;         // Merker Fenster Hub einstellen ist nicht offen
            Button_HubEinstellen.Enabled = false;       // Button sperren
            textBox_HubEinstellenAktuelleAufgabe.Text = ""; // Textbox leeren
            Panel_HubEinstellen.Visible = false;        // Panel für die Einstellung des Hubs ausblenden
            iAktuellerHub = 0;

            // Widerstand überprüfen
            bWiderstandUeberprueft = false;             // Merker Widerstand noch nicht geprüft
            Button_WiderstandPruefen.Enabled = false;   // Button für die Prüfung sperren
            Panel_WiderstandPruefen.Visible = false;    // Panel für die Überprüfung ausblenden

            // Prüfungsstart/Durchführung
            Button_Start.Enabled = false;
            Button_PruefungAbbrechen.Enabled = false;
            Button_Pausieren.Enabled = false;
            button_csvKopieren.Enabled = false;
            Button_Zuruecksetzen.Enabled = false;
            
            bPruefungAktiv = false;

            // Prüfbeschreibung freigeben
            groupBox_DatenZurPruefung.Enabled = true;
            groupBox_Prüfungsdurchführung.Enabled = true;
            groupBox_Pruefungsende.Enabled = true;
            groupBox_Reibkraftmessung.Enabled = true;

            Button_LetztePruefbeschreibungLaden.Enabled = true;

            checkBox_PruefungMitTemperatur.Enabled = false;
            checkBox_ReibkraftMessen.Enabled = true;

            // Labels leeren
            Label_VerstricheneZeitValue.Text = "------";
            Label_RestzeitValue.Text = "------";
            Label_AktuellerZyklusValue.Text = "------";

            progressBar.Value = 0;

            // Charts leeren
            chart_Widerstand.Series.Clear();
            chart_Reibkraft.Series.Clear();
            Chart_IstTemperatur.Series.Clear();

            Refresh();

        }


        //-----------------------------------------------------
        // Button um Kommunikation mit TwinCAT aufzubauen
        //-----------------------------------------------------
        private void Button_TwinCATVerbinden_Click(object sender, EventArgs e)
        {
            Button_TwinCATVerbinden.Enabled = false;

            Connect_TwinCAT();

            // TwinCAT verbunden und kein Fehler aufgetreten?
            if (bTwinCATVerbunden == true && bErrorTwinCAT == false)
            {
                Button_TwinCATVerbinden.BackColor = Color.Green;

                // Button Hub einstellen freigeben, wenn alle anderen Geräte verbunden sind
                if (bDigitalanzeigeVerbunden == true && bKraftsensorVerbunden == true && bTemperaturreglerVerbunden == true && bMilliohmmeterVerbunden == true)
                {
                    Button_HubEinstellen.Enabled = true;
                }
            }
            else
            {
                Button_TwinCATVerbinden.Enabled = true;
                Button_TwinCATVerbinden.BackColor = Color.Red;
            }
        }

        //-----------------------------------------------------
        // Button um mit der Digitalanzeige zu verbinden
        //-----------------------------------------------------
        private void Button_DigitalanzeigeVerbinden_Click(object sender, EventArgs e)
        {
            Button_DigitalanzeigeVerbinden.Enabled = false;

            Digitalanzeige_Verbinden();
        }

        //-----------------------------------------------------
        // Button um mit dem Milliohmmeter zu verbinden
        //-----------------------------------------------------
        private void Button_MilliohmmeterVerbinden_Click(object sender, EventArgs _Event)
        {
            Button_MilliohmmeterVerbinden.Enabled = false;

            Milliohmmeter_Verbinden();
        }

        //-----------------------------------------------------
        // Button um mit dem Kraftsensor zu verbinden
        //-----------------------------------------------------
        private void ButtonKraftsensorVerbinden_Click(object sender, EventArgs e)
        {
            Button_KraftsensorVerbinden.Enabled = false;

            Kraftsensor_Verbinden();
        }

        //-----------------------------------------------------
        // Button um den Temperaturregler zu verbinden
        //-----------------------------------------------------
        private void Button_TemperaturreglerVerbinden_Click(object sender, EventArgs e)
        {
            Button_TemperaturreglerVerbinden.Enabled = false;

            Temperaturregler_Verbinden();
        }

        //-----------------------------------------------------
        // wird bei Änderung der Checkbox aufgerufen
        // Ziel ist das Ein- und Ausblenden der entsprechenden GUI-Elemente
        //-----------------------------------------------------
        private void checkBox_PruefungMitTemperatur_CheckedChanged(object sender, EventArgs e)
        {
            // Timer für die Wartezeit auf eine Reaktion von TwinCAT initialisieren
            Timer_WartezeitReaktionTwinCAT.Interval = iTimeWartezeitReaktionTwinCAT_Antwort;
            Timer_WartezeitReaktionTwinCAT.Enabled = false;
            bWartezeitReaktionTwinCATAbgelaufen = false;

            /* Wenn Checkboxnicht gecheckt ist, wird die Prüfung mit Temperatur durchgeführt */
            if (checkBox_PruefungMitTemperatur.Checked == true)
            {
                // Temperaturwerte übernehmen
                numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben.Value = numericUpDown_SollTemperaturOben.Value;
                numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten.Value = numericUpDown_SollTemperaturUnten.Value;

                // Masse der Heizpatronen zuschalten
                try
                {
                    bSetHeizpatronenMasseSchalten = true;
                    tcClient.WriteAny(hSetHeizpatronenMasseSchalten, bSetHeizpatronenMasseSchalten);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    bErrorTwinCAT = true;
                }

                bPruefungMitTemperatur = true;

                // Steuerelemente einblenden
                button_WerteAnTemperaturreglerÜbertragen.Visible = true;
                button_ProbenAufheizen.Visible = true;

                Label_SollTemperaturHeizpatroneOben.Visible = true;
                numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben.Visible = true;
                Label_SollTemperaturHeizpatroneUnten.Visible = true;
                numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten.Visible = true;

                Label_IstTemperaturHeizpatroneOben.Visible = true;
                Label_IstTemperaturHeizpatroneObenValue.Visible = true;
                Label_IstTemperaturHeizpatroneUntenValue.Visible = true;
                Label_IstTemperaturHeizpatroneUnten.Visible = true;

                Label_IstTemperaturProbeOben.Visible = true;
                numericUpDown_OffsetTemperaturProbeOben.Visible = true;
                Label_IstTemperaturProbeUnten.Visible = true;
                numericUpDown_OffsetTemperaturProbeUnten.Visible = true;

                Chart_IstTemperatur.Visible = true;

                button_ProbenAufheizen.Enabled = false; // Button Sperren, da zunächst die Temperatur eingestellt werden muss

                // Heizpatronen freigeben
                bSetHeizungFreigeben = true;
                tcClient.WriteAny(hSetHeizpatronenFreigeben, bSetHeizungFreigeben);

                // Timer für Reaktion von TwinCAT starten
                bWartezeitReaktionTwinCATAbgelaufen = false;
                Timer_WartezeitReaktionTwinCAT.Enabled = true;

                // Solange der Timer noch nicht abgelaufen ist
                while (bWartezeitReaktionTwinCATAbgelaufen == false)
                {

                }

                // Wurden die Heizpatronen nicht zugeschaltet?
                if (bGetHeizpatronenZugeschaltet == false)
                {
                    MessageBox.Show("Heizpatronen konnten nicht zugeschaltet werden");

                    checkBox_PruefungMitTemperatur.Checked = false; // Checkbox zurücksetezn
                }

                // csv-Datei erstellen
                try
                {
                    // Neue Datei erstellen und durch false alte überschreiben. UTF8 Encoding um Umlaute darstellen zu können
                    swFileWriterPruefergebnisse = new StreamWriter("C:\\Users\\haheuer\\Documents\\Versuch Temperaturtest\\Test Temperatur.csv", false, Encoding.UTF8);

                    TestCSVOffen = true;

                    swFileWriterPruefergebnisse.WriteLine("Soll-Temperatur der Heizpatrone" + TAB + "Offset Vorgabe" + TAB + "Ist-Temperatur der Heizpatrone" + TAB + "Uhrzeit");
                }
                catch (Exception _Exception)
                {
                    MessageBox.Show("csv-Datei für Prüfergebnisse erstellen:\n\n" + _Exception.GetType().Name);

                    bErrorErstelleCSV = true;
                    bFileCSVPruefergebnisseOffen = false;   // Merker Datei ist geschlossen
                }
            }
            else
            {
                // Masse der Heizpatronen abschalten
                try
                {
                    bSetHeizpatronenMasseSchalten = false;
                    tcClient.WriteAny(hSetHeizpatronenMasseSchalten, bSetHeizpatronenMasseSchalten);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    bErrorTwinCAT = true;
                }

                bPruefungMitTemperatur = false;

                // Steuerelemente ausblenden
                button_WerteAnTemperaturreglerÜbertragen.Visible = false;
                button_ProbenAufheizen.Visible = false;

                Label_SollTemperaturHeizpatroneOben.Visible = false;
                numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben.Visible = false;
                Label_SollTemperaturHeizpatroneUnten.Visible = false;
                numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten.Visible = false;

                Label_IstTemperaturHeizpatroneOben.Visible = false;
                Label_IstTemperaturHeizpatroneObenValue.Visible = false;
                Label_IstTemperaturHeizpatroneUntenValue.Visible = false;
                Label_IstTemperaturHeizpatroneUnten.Visible = false;

                Label_IstTemperaturProbeOben.Visible = false;
                numericUpDown_OffsetTemperaturProbeOben.Visible = false;
                Label_IstTemperaturProbeUnten.Visible = false;
                numericUpDown_OffsetTemperaturProbeUnten.Visible = false;

                Chart_IstTemperatur.Visible = false;

                //-----------------------------------------------------
                // Temperaturregelung stoppen
                //-----------------------------------------------------
                usModbusAdresse = 1214;                 // Adresse zum Stoppen der Regelung
                usWertSchreibenTemperaturregler = 0;
                Modbus_WriteFunction06();

                // Kein Fehler aufgetreten?
                if (bErrorTemperaturregler == false)
                {
                    bButtonProbenAufheizenAktiv = false;
                    bTemperaturregelungAktiv = false;
                    button_ProbenAufheizen.BackColor = Color.Gray;
                }
                else
                {
                    MessageBox.Show("Temperaturregelung konnte nicht gestoppt werden");
                }

                //-----------------------------------------------------
                // Heizpatronen sperren
                //-----------------------------------------------------
                if (bErrorTwinCAT == false)
                {
                    bSetHeizungFreigeben = false;
                    tcClient.WriteAny(hSetHeizpatronenFreigeben, bSetHeizungFreigeben);
                }

                // Timer für Reaktion von TwinCAT starten
                Timer_WartezeitReaktionTwinCAT.Enabled = true;

                // Solange der Timer noch nicht abgelaufen ist
                while (bWartezeitReaktionTwinCATAbgelaufen == false)
                {

                }

                // Wurden die Heizpatronen nicht zugeschaltet?
                if (bGetHeizpatronenZugeschaltet == true)
                {
                    MessageBox.Show("Heizpatronen konnten nicht abgeschaltet werden");

                    checkBox_PruefungMitTemperatur.Checked = true; // Checkbox zurücksetezn
                }

                if (TestCSVOffen == true)
                {
                    // Datei schließen
                    swFileWriterPruefergebnisse.Dispose();
                    swFileWriterPruefergebnisse.Close();
                }
            }
        }

        //-----------------------------------------------------
        // wird bei Änderung der Checkbox aufgerufen
        // Ziel ist das Ein- und Ausblenden der entsprechenden GUI-Elemente
        //-----------------------------------------------------
        private void checkBox_ReibkraftMessen_CheckedChanged(object sender, EventArgs e)
        {
            /* Wenn Checkboxnicht gecheckt ist, wird die Prüfung mit der Reibkraftmessung durchgeführt */
            if (checkBox_ReibkraftMessen.Checked == true)
            {
                bReibkraftmessungAktiviert = true;

                // Steuerelemente einblenden
                Label_Reibkraft.Visible = true;
                Label_ReibkraftValue.Visible = true;

                chart_Reibkraft.Visible = true;
            }
            else
            {
                bReibkraftmessungAktiviert = false;

                // Steuerelemente einblenden
                Label_Reibkraft.Visible = false;
                Label_ReibkraftValue.Visible = false;

                chart_Reibkraft.Visible = false;

                // Wurde der Hub bereits eingestellt?
                if (bHubeinstellungAbgeschlossen == true)
                {
                    // Checkbox sperren, da erneutes Anwählen nicht mehr möglich sein darf,
                    // da Tara der Reibkraft während der Hubeinstellung erfasst wird
                    groupBox_Reibkraftmessung.Enabled = false;
                    fTaraWertKraftsensor = 0;
                }

            }
            
        }

        private void Button_PruefungAbbrechen_Click(object sender, EventArgs e)
        {
            Button_PruefungAbbrechen.Enabled = false;
            Button_Pausieren.Enabled = false;
            button_csvKopieren.Enabled = false;

            // Merker setzen, dass die Prüfung abgebrochen wurde
            bButtonPruefungAbbrechen = true;
        }

        //-----------------------------------------------------
        // Button um eine Prüfung zu starten
        //-----------------------------------------------------
        private void Button_Start_Click(object sender, EventArgs e)
        {
            
            // Steuerelemente sperren/freigeben
            Button_Start.Enabled = false;

            groupBox_DatenZurPruefung.Enabled = false;
            groupBox_Prüfungsdurchführung.Enabled = false;
            groupBox_Pruefungsende.Enabled = false;

            checkBox_ReibkraftMessen.Enabled = false;
            checkBox_PruefungMitTemperatur.Enabled = false;

            bPruefungAktiv = true;
            bButtonPruefungPause = false;
            bButtonPruefungAbbrechen = false;

            //-----------------------------------------------------
            // Variablen für die Prüfungsvorbereitung
            //-----------------------------------------------------
            bool bFehlerhafteFelder = false;    // Merker ob es Fehlerhafte Felder gibt
            bool bVorbereitungAbgeschlossen = false;    // Merker, dass die Vorbereitung abgeschlossen ist

            
            string strNeuerPfad;
            string strFehlerFelder = "Folgende Felder sind leer:" + LF;
            const string strFeldLeer = "";


            // Soll Vorgabe des Hubs
            decimal decSollHub;
            double dSollHub;

            // Wenn der Messtaster geparkt ist und der Knebelschalter auf Betrieb steht
            if (bGetMesstasterVorhanden == true && bGetKnebelschalterBetrieb == false)  // später true
            {
                //-----------------------------------------------------
                // Prüfbeschreibung Pflichtfelder überprüfen
                // NumericUpDown Felder müssen nicht geprüft werden, da ausschließlich Zahlen hinterlegt werden können
                //-----------------------------------------------------
                // Feld Prüfer leer?
                if (TextBox_Pruefer.Text == strFeldLeer)
                {
                    strFehlerFelder = strFehlerFelder + "Prüfer" + LF;
                    TextBox_Pruefer.BackColor = Color.Red;
                    bFehlerhafteFelder = true;  // Merker Fehler bei dem Pflichfeld
                }
                else
                {
                    TextBox_Pruefer.BackColor = colorStandardGrau;
                }

                // Feld Artikelnummer leer?
                if (TextBox_Artikelnummer.Text == strFeldLeer)
                {
                    strFehlerFelder = strFehlerFelder + "Artikelnummer" + LF;
                    TextBox_Artikelnummer.BackColor = Color.Red;
                    bFehlerhafteFelder = true;  // Merker Fehler bei dem Pflichfeld
                }
                else
                {
                    TextBox_Artikelnummer.BackColor = colorStandardGrau;
                }

                // Feld Auftragsnummer leer?
                if (TextBox_Auftragsnummer.Text == strFeldLeer)
                {
                    strFehlerFelder = strFehlerFelder + "Auftragsnummer" + LF;
                    TextBox_Auftragsnummer.BackColor = Color.Red;
                    bFehlerhafteFelder = true;  // Merker Fehler bei dem Pflichfeld
                }
                else
                {
                    TextBox_Auftragsnummer.BackColor = colorStandardGrau;
                }

                // Im Fehlerfall ausgeben, welche Felder fehlerhaft sind
                if (bFehlerhafteFelder == true)
                {
                    MessageBox.Show("Prüfungsbeschreibung:\n\n" + strFehlerFelder);

                    Button_Start.Enabled = true;    // Button zum Start der Prüfung wieder freigeben
                }
                else
                {
                    //-----------------------------------------------------
                    // Drehzahl und Gap zur Regelung bestimmen
                    // Die Hübe werden in 6 Bereiche unterteilt und entsprechend die Drehzahl über eine Funktion berechnet
                    // Sollte die Reibkraft mitgemessen werden, so muss Fall 1 betrachtet werden
                    //-----------------------------------------------------
                    // Feld Hub auslesen
                    decSollHub = NumericUpDown_Hub.Value;    // Wert auslesen
                    dSollHub = Convert.ToDouble(decSollHub);

                    // Prüfung mit Reibkraft?
                    if (bReibkraftmessungAktiviert == true)
                    {
                        bGeregelteFahrt = false;
                        dZielgeschwindigkeitGap1zu2 = 0;
                        dZielgeschwindigkeitGap1zu3 = 0;

                        dZielgeschwindigkeit1 = dGeschwindigkeitServomotorReibkraftmessung;
                    }
                    // Hub < 200 µm?
                    else if (dSollHub < 200.0)
                    {
                        bGeregelteFahrt = false;            // Merker keine geregelte Fahrt. Gap1zu2 und Gap1zu3 werden ignoriert
                        dZielgeschwindigkeitGap1zu2 = 0;
                        dZielgeschwindigkeitGap1zu3 = 0;

                        dZielgeschwindigkeit1 = 0.1325 * Math.Pow(dSollHub, 2) - 54.74 * dSollHub + 7228;
                    }
                    // 300 > Hub >= 200
                    else if (300.0 > dSollHub && dSollHub >= 200.0)
                    {
                        bGeregelteFahrt = true;            // Merker geregelte Fahrt. Gap1zu2 und Gap1zu3 werden berücksichtigt
                        dZielgeschwindigkeitGap1zu2 = 250;
                        dZielgeschwindigkeitGap1zu3 = 150;

                        dZielgeschwindigkeit1 = 0.03401 * Math.Pow(dSollHub, 2) - 22.05 * dSollHub + 4740;
                        dZielgeschwindigkeit2 = dZielgeschwindigkeit1 - dZielgeschwindigkeitGap1zu2;
                        dZielgeschwindigkeit3 = dZielgeschwindigkeit1 - dZielgeschwindigkeitGap1zu3;
                    }
                    // 480 > Hub >= 300
                    else if (480.0 > dSollHub && dSollHub >= 300.0)
                    {
                        bGeregelteFahrt = true;            // Merker geregelte Fahrt. Gap1zu2 und Gap1zu3 werden berücksichtigt
                        dZielgeschwindigkeitGap1zu2 = 150;
                        dZielgeschwindigkeitGap1zu3 = 80;

                        dZielgeschwindigkeit1 = 0.006203 * Math.Pow(dSollHub, 2) - 6.2725 * dSollHub + 2559;
                        dZielgeschwindigkeit2 = dZielgeschwindigkeit1 - dZielgeschwindigkeitGap1zu2;
                        dZielgeschwindigkeit3 = dZielgeschwindigkeit1 - dZielgeschwindigkeitGap1zu3;
                    }
                    // 720 >= Hub >= 480
                    else if (720.0 >= dSollHub && dSollHub >= 480.0)
                    {
                        bGeregelteFahrt = true;            // Merker geregelte Fahrt. Gap1zu2 und Gap1zu3 werden berücksichtigt
                        dZielgeschwindigkeitGap1zu2 = 100;
                        dZielgeschwindigkeitGap1zu3 = 40;

                        dZielgeschwindigkeit1 = 0.001852 * Math.Pow(dSollHub, 2) - 2.056 * dSollHub + 1740;
                        dZielgeschwindigkeit2 = dZielgeschwindigkeit1 - dZielgeschwindigkeitGap1zu2;
                        dZielgeschwindigkeit3 = dZielgeschwindigkeit1 - dZielgeschwindigkeitGap1zu3;
                    }
                    // 1200 >= Hub > 720
                    else if (1200.0 >= dSollHub && dSollHub > 720.0)
                    {
                        bGeregelteFahrt = true;            // Merker geregelte Fahrt. Gap1zu2 und Gap1zu3 werden berücksichtigt
                        dZielgeschwindigkeitGap1zu2 = 20;
                        dZielgeschwindigkeitGap1zu3 = 10;

                        dZielgeschwindigkeit1 = 0.000196 * Math.Pow(dSollHub, 2) - 0.723 * dSollHub + 834.1;
                        dZielgeschwindigkeit2 = dZielgeschwindigkeit1 - dZielgeschwindigkeitGap1zu2;
                        dZielgeschwindigkeit3 = dZielgeschwindigkeit1 - dZielgeschwindigkeitGap1zu3;
                    }
                    // Hub > 1200
                    else
                    {
                        bGeregelteFahrt = true;            // Merker geregelte Fahrt. Gap1zu2 und Gap1zu3 werden berücksichtigt
                        dZielgeschwindigkeitGap1zu2 = 0;
                        dZielgeschwindigkeitGap1zu3 = 0;

                        dZielgeschwindigkeit1 = 0.00001008 * Math.Pow(dSollHub, 2) - 0.1047 * dSollHub + 321.2;
                        dZielgeschwindigkeit2 = dZielgeschwindigkeit1 - dZielgeschwindigkeitGap1zu2;
                        dZielgeschwindigkeit3 = dZielgeschwindigkeit1 - dZielgeschwindigkeitGap1zu3;
                    }

                    CSV_Datei_Anlegen();  // Methode aufrufen um die CSV-Datei anzulegen

                    // Prüfbeschreibung in Config Datei speichern
                    Pruefbeschreibung_Zu_Config();
                }

                // Wenn die CSV-Datei erstellt werden konnte und keine Felder Fehlerhaft waren
                if (bErrorErstelleCSV == false && bErstelleCSVAbgebrochen == false && bFehlerhafteFelder == false)
                {
                    bVorbereitungAbgeschlossen = true;  // Merker, dass die Vorbereitung abgeschlossen ist
                }

            }            

            //-----------------------------------------------------
            // Fehler beim Erstellen der CSV aufgetreten oder aber Messtaster nicht geparkt?
            //-----------------------------------------------------
            if (bFehlerhafteFelder == true || bErrorErstelleCSV == true || bErstelleCSVAbgebrochen == true || bGetMesstasterVorhanden == false || bGetKnebelschalterBetrieb == true)
            {
                // Steuerelemente freigeben/ sperren
                Button_Start.Enabled = true;
                checkBox_ReibkraftMessen.Enabled = true;
                checkBox_PruefungMitTemperatur.Enabled = true;

                Button_PruefungAbbrechen.Enabled = false;
                Button_Pausieren.Enabled = false;

                // Messtaster nicht geparkt?
                if (bGetMesstasterVorhanden == false)
                {
                    MessageBox.Show("Messtaster S5 ist nicht geparkt.");    // Textausgabe, dass der Messtaster nicht geparkt ist
                }

                // Knebelschalter nicht in Stellung Betrieb?
                if (bGetKnebelschalterBetrieb == true) // Später false
                {
                    MessageBox.Show("Prüfstand ist im Einrichtbetrieb");
                }

                if (bErrorErstelleCSV == true)
                {
                    MessageBox.Show("Erstellen der CSV-Datei wurde abgebrochen");
                }

                // CSV-File Prüfergebnisse offen?
                if (bFileCSVPruefergebnisseOffen == true)
                {
                    // Datei schließen
                    swFileWriterPruefergebnisse.Dispose();
                    swFileWriterPruefergebnisse.Close();

                    bFileCSVPruefergebnisseOffen = false;   // Merker Datei ist geschlossen
                }

                // CSV-File Reibwerte offen?
                if (bFileCSVReibwerteOffen == true)
                {
                    // Datei schließen
                    swFileWriterReibwerte.Dispose();
                    swFileWriterReibwerte.Close();

                    bFileCSVReibwerteOffen = false;   // Merker Datei ist geschlossen
                }
            }
            else if (bVorbereitungAbgeschlossen == true) // Vorbereitung abgeschlossen?
            {
                // Steuerelemente freigeben/ sperren
                Button_Start.Enabled = false;
                Button_Zuruecksetzen.Enabled = false;
                Button_PruefungAbbrechen.Enabled = true;
                Button_Pausieren.Enabled = true;
                Button_LetztePruefbeschreibungLaden.Enabled = false;

                checkBox_ReibkraftMessen.Enabled = false;
                checkBox_PruefungMitTemperatur.Enabled = false;

                //-----------------------------------------------------
                // Prüfungsdurchführund durch BackgroundWorker im neuen Thread
                //-----------------------------------------------------
                // Backgroundworker nicht aktiv?
                if (backgroundWorker_Start.IsBusy != true)
                {
                    backgroundWorker_Start.RunWorkerAsync();
                }

                //-----------------------------------------------------
                // Fenster mit den Prüfungsergebnissen aufrufen
                //-----------------------------------------------------
                Prüfungsergebnis FormPruefungsergebnis = new Prüfungsergebnis();

                FormPruefungsergebnis.strGrenzwertZyklen = strGrenzwertZyklen;
                FormPruefungsergebnis.strErreichteZyklen = strErreichteZyklen;
                FormPruefungsergebnis.strGrenzwertWiderstand = strGrenzwertWiderstand;
                FormPruefungsergebnis.strErreichterWiderstand = strLetzteWiderstandswert;
                FormPruefungsergebnis.strPruefungsdauer = strPruefungsdauer;

                //-----------------------------------------------------
                // Neues Fenster im Vordergrund öffnen
                //-----------------------------------------------------
                FormPruefungsergebnis.ShowDialog();

            }
        }


        public override void Refresh()
        {
            Invoke(new Action(() => { base.Refresh(); }));
        }

        //-----------------------------------------------------
        // Methode zum Anlegen der csv-Datei
        //-----------------------------------------------------
        private void CSV_Datei_Anlegen()
        {
            string strTextToCSV;
            

            int iIndexBackslash;

            decimal decNumericUpDownValue;

            bErstelleCSVAbgebrochen = false;
            bErrorErstelleCSV = false;

            // Aktuelle Uhrzeit erfassen
            DateTime dtAktuelleUhrzeit = DateTime.Now;

            // Save File Dialog vorbereiten
            saveFileDialog_Messung.InitialDirectory = strLetzteDateipfad;

            saveFileDialog_Messung.Filter = "csv files (*.csv)|*.csv";
            saveFileDialog_Messung.FileName = "RK";
            saveFileDialog_Messung.RestoreDirectory = true;

            if (saveFileDialog_Messung.ShowDialog() == DialogResult.OK)
            {
                strFilenameCSV = saveFileDialog_Messung.FileName;               // Neuen Filename inklusíve Pfad speichern

                iIndexBackslash = strFilenameCSV.LastIndexOf('\\');             // Dateinamen finden
                strDateipfadCSV = strFilenameCSV.Remove(iIndexBackslash);       // Pfad ohne Dateinamen speichern
                strDateinameCSV = strFilenameCSV.Remove(0, iIndexBackslash+1);  // Dateinamen ohne Pfad speichern

                // Neuen Pfad als letzten Pfad in der Config speichern
                ConfigFile.SetValue(strSectionEinstellungen, "Dateipfad", strDateipfadCSV);

                // csv-Datei erstellen
                try
                {
                    // Neue Datei erstellen und durch false alte überschreiben. UTF8 Encoding um Umlaute darstellen zu können
                    swFileWriterPruefergebnisse = new StreamWriter(strFilenameCSV, false, Encoding.UTF8);

                    bFileCSVPruefergebnisseOffen = true;   // Merker Datei ist geöffnet
                }
                catch (Exception _Exception)
                {
                    MessageBox.Show("csv-Datei für Prüfergebnisse erstellen:\n\n" + _Exception.GetType().Name);

                    bErrorErstelleCSV = true;
                    bFileCSVPruefergebnisseOffen = false;   // Merker Datei ist geschlossen
                }

                if (bFileCSVPruefergebnisseOffen == true)  // Konnte die Datei geöffnet werden?
                {
                    //-----------------------------------------------------
                    // Daten zur Prüfung hinterlegen
                    //-----------------------------------------------------
                    strTextToCSV = "Daten zur Prüfung:" + LF;

                    // Prüfer
                    strTextToCSV = strTextToCSV + "Prüfer:" + TAB + TextBox_Pruefer.Text + LF;

                    // Artikelnummer
                    strTextToCSV = strTextToCSV + "Artikelnummer:" + TAB + TextBox_Artikelnummer.Text + LF;

                    // Projektnummer
                    strTextToCSV = strTextToCSV + "Projektnummer:" + TAB + TextBox_Projektnummer.Text + LF;

                    // Auftragsnummer
                    strTextToCSV = strTextToCSV + "Auftragsnummer:" + TAB + TextBox_Auftragsnummer.Text + LF;

                    // Schmierstoff
                    strTextToCSV = strTextToCSV + "Schmierstoff:" + TAB + TextBox_Schmierstoff.Text + LF;

                    // Beschreibung
                    strTextToCSV = strTextToCSV + "Beschreibung:" + TAB + TextBox_Beschreibung.Text + LF + LF;

                    //-----------------------------------------------------
                    // Daten zur Prüfungsdurchführung hinterlegen
                    //-----------------------------------------------------
                    strTextToCSV = strTextToCSV + "Prüfungsdurchführung:" + LF;

                    // Sollgeschwindigkeit speichern
                    strTextToCSV = strTextToCSV + "Soll-Geschwindigkeit:" + TAB + dZielgeschwindigkeit1.ToString() + "Grad/s" + LF;

                    // Hub/Weg
                    decNumericUpDownValue = NumericUpDown_Hub.Value;    // Wert auslesen
                    strTextToCSV = strTextToCSV + "Hub/Weg:" + TAB + Convert.ToString(decNumericUpDownValue) + " µm" + LF;

                    // Kontaktkraft
                    decNumericUpDownValue = NumericUpDown_Kontaktkraft.Value;    // Wert auslesen
                    strTextToCSV = strTextToCSV + "Kontaktkraft:" + TAB + Convert.ToString(decNumericUpDownValue) + " N" + LF;

                    // Federweg
                    decNumericUpDownValue = NumericUpDown_Federweg.Value;    // Wert auslesen
                    strTextToCSV = strTextToCSV + "Federweg:" + TAB + Convert.ToString(decNumericUpDownValue) + " µm" + LF;

                    // Wartezeit
                    decNumericUpDownValue = NumericUpDown_Wartezeit.Value;    // Wert auslesen
                    strTextToCSV = strTextToCSV + "Wartezeit:" + TAB + Convert.ToString(decNumericUpDownValue) + " ms" + LF;

                    // Reibkraftprüfung?
                    if (checkBox_ReibkraftMessen.Checked == true)
                    {
                        strTextToCSV = strTextToCSV + "Prüfung mit Reibkraftmessung:" + TAB + "Ja" + LF;
                    }
                    else
                    {
                        strTextToCSV = strTextToCSV + "Prüfung mit Reibkraftmessung:" + TAB + "Nein" + LF;
                    }

                    // Prüfung mit Temperautur?
                    if (checkBox_PruefungMitTemperatur.Checked == true)
                    {
                        strTextToCSV = strTextToCSV + "Prüfung mit Temperatur:" + TAB + "Ja" + LF;
                    }
                    else // Prüfung ohne Temperatur
                    {
                        strTextToCSV = strTextToCSV + "Prüfung mit Temperatur:" + TAB + "Nein" + LF;
                    }

                    // Soll-Temperatur obere Probe
                    decNumericUpDownValue = numericUpDown_SollTemperaturOben.Value;    // Wert auslesen
                    strTextToCSV = strTextToCSV + "Soll-Temperatur obere Probe:" + TAB + Convert.ToString(decNumericUpDownValue) + "°" + LF;

                    // Soll-Temperatur untere Probe
                    decNumericUpDownValue = numericUpDown_SollTemperaturUnten.Value;    // Wert auslesen
                    strTextToCSV = strTextToCSV + "Soll-Temperatur untere Probe:" + TAB + Convert.ToString(decNumericUpDownValue) + "°" + LF;

                    // Absatz einfügen
                    strTextToCSV = strTextToCSV + LF;

                    //-----------------------------------------------------
                    // Daten zum Prüfungsende hinterlegen
                    //-----------------------------------------------------
                    strTextToCSV = strTextToCSV + "Prüfungsende:" + LF;

                    // Grenzwert für den Widerstand
                    decNumericUpDownValue = NumericUpDown_RGrenzwertEnde.Value;    // Wert auslesen
                    strTextToCSV = strTextToCSV + "Grenzwert Widerstand:" + TAB + Convert.ToString(decNumericUpDownValue) + " mOhm" + LF;

                    // Grenzwert Zyklen
                    decNumericUpDownValue = NumericUpDown_GrenzwertZyklen.Value;    // Wert auslesen
                    strTextToCSV = strTextToCSV + "Grenzwert Zyklen:" + TAB + Convert.ToString(decNumericUpDownValue) + LF;

                    // Prüfbeschreibung in csv-Datei schreiben
                    try
                    {
                        swFileWriterPruefergebnisse.WriteLine(strTextToCSV);
                    }
                    catch (Exception _Exception)
                    {
                        MessageBox.Show("csv-Datei Prüfergebnisse beschreiben:\n\n" + _Exception.GetType().Name);

                        // Datei schließen
                        swFileWriterPruefergebnisse.Dispose();
                        swFileWriterPruefergebnisse.Close();

                        bFileCSVPruefergebnisseOffen = false;   // Merker Datei ist geschlossen
                        bErrorErstelleCSV = true;          // Merker für Fehler setzen
                    }
                }

                // Soll die Reibkraft mitgemessen werden?
                if (bReibkraftmessungAktiviert == true)
                {
                    // Dateinamen mit Pfad erstellen
                    strFilenameReibwerteCSV = "Reibwerte - " + strDateinameCSV;
                    strFilenameReibwerteCSV = strDateipfadCSV + "\\" + strFilenameReibwerteCSV;

                    // csv-Datei für die Reibkraftwerte erstellen
                    try
                    {
                        // Neue Datei erstellen und durch false alte überschreiben. UTF8 Encoding um Umlaute darstellen zu können
                        swFileWriterReibwerte = new StreamWriter(strFilenameReibwerteCSV, false, Encoding.UTF8);

                        bFileCSVReibwerteOffen = true;   // Merker Datei ist geöffnet
                    }
                    catch (Exception _Exception)
                    {
                        MessageBox.Show("csv-Datei für Reibwerte erstellen:\n\n" + _Exception.GetType().Name);

                        bErrorErstelleCSV = true;
                        bFileCSVReibwerteOffen = false;   // Merker Datei ist geschlossen
                    }
                }

                // Ist die Datei geöffnet?
                if (bFileCSVReibwerteOffen == true)
                {
                    // Tabelle füllen
                    strTextToCSV = "";
                    strTextToCSV = "Zyklusnummer" + TAB + "Reibwert in Druckrichtung in N" + TAB + "Reibwert in Zugrichtung in N";

                    try
                    {
                        swFileWriterReibwerte.WriteLine(strTextToCSV);
                    }
                    catch (Exception _Exception)
                    {
                        MessageBox.Show("csv-Datei Reibwerte beschreiben:\n\n" + _Exception.GetType().Name);

                        // Datei schließen
                        swFileWriterReibwerte.Dispose();
                        swFileWriterReibwerte.Close();

                        bFileCSVReibwerteOffen = false;   // Merker Datei ist geschlossen
                        bErrorErstelleCSV = true;          // Merker für Fehler setzen
                    }
                }
            }
            else // Wenn OK nicht gedrückt wurde
            {
                bErstelleCSVAbgebrochen = true; // Merker, dass das Erstellen abgebrochen wurde
                bErrorErstelleCSV = true;
            }

        }

        //-----------------------------------------------------
        // Methode um die Prüfungsbeschreibung in der Config Datei zu speichern
        //-----------------------------------------------------
        private void Pruefbeschreibung_Zu_Config()
        {
            try
            {
                // Elemente der Prüfbeschriebung speichern
                ConfigFile.SetValue(strSectionLetztePruefungsbeschreibung, "Pruefer", TextBox_Pruefer.Text);
                ConfigFile.SetValue(strSectionLetztePruefungsbeschreibung, "Artikelnummer", TextBox_Artikelnummer.Text);
                ConfigFile.SetValue(strSectionLetztePruefungsbeschreibung, "Projektnummer", TextBox_Projektnummer.Text);
                ConfigFile.SetValue(strSectionLetztePruefungsbeschreibung, "Auftragsnummer", TextBox_Auftragsnummer.Text);
                ConfigFile.SetValue(strSectionLetztePruefungsbeschreibung, "Schmierstoff", TextBox_Schmierstoff.Text);
                ConfigFile.SetValue(strSectionLetztePruefungsbeschreibung, "Beschreibung", TextBox_Beschreibung.Text);

                ConfigFile.SetValue(strSectionLetztePruefungsbeschreibung, "Hub", NumericUpDown_Hub.Value.ToString());
                ConfigFile.SetValue(strSectionLetztePruefungsbeschreibung, "Kontaktkraft", NumericUpDown_Kontaktkraft.Value.ToString());
                ConfigFile.SetValue(strSectionLetztePruefungsbeschreibung, "Federweg", NumericUpDown_Federweg.Value.ToString());
                ConfigFile.SetValue(strSectionLetztePruefungsbeschreibung, "Wartezeit", NumericUpDown_Wartezeit.Value.ToString());

                ConfigFile.SetValue(strSectionLetztePruefungsbeschreibung, "WiderstandPruefungsende", NumericUpDown_RGrenzwertEnde.Value.ToString());
                ConfigFile.SetValue(strSectionLetztePruefungsbeschreibung, "ZyklenPruefungsende", NumericUpDown_GrenzwertZyklen.Value.ToString());

                ConfigFile.SetValue(strSectionLetztePruefungsbeschreibung, "SollTemperaturOben", numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben.Value.ToString());
                ConfigFile.SetValue(strSectionLetztePruefungsbeschreibung, "SollTemperaturUnten", numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten.Value.ToString());
            }
            catch
            {
                MessageBox.Show("Fehler beim Beschreiben der Config Datei.\nPrüfung wird fortgesetzt.\n" + sExceptionText);
            }
        }

        //-----------------------------------------------------
        // Button der den Motor einschaltet
        //-----------------------------------------------------
        private void Button_MotorEinschalten_Click(object sender, EventArgs e)
        {
            // Ist der Motor nicht eingeschaltet?
            if (bGetMotorEingeschaltet == false)
            {
                // Drehrichung vorgeben
                bSetDrehrichtungRechts = false;
                bSetDrehrichtungLinks = true;

                // Motor einschalten
                ServoMotor_Einschalten();

                if (bErrorTwinCAT == true)
                {
                    Fehlerbehandlung();
                }
                else if (bGetMotorEingeschaltet == true)
                {

                }
            }
            else
            {
                // Motor ausschalten
                ServoMotor_Ausschalten();

                if (bErrorTwinCAT == true)
                {
                    Fehlerbehandlung();
                }
            }
        }

        //-----------------------------------------------------
        // Button der das Modul Hub einstellen öffnet
        //-----------------------------------------------------
        private void Button_HubEinstellen_Click(object sender, EventArgs e)
        {
            if (bFensterHubEinstellenOffen == false)
            {
                Button_HubEinstellen.Enabled = false;   // Button sperren, da schließen über Abbrechen erfolgt


                // Steuerelemente auf Grundeinstellung setzen
                button_HubErmitteln.Enabled = true;
                Button_MesstasterNullen.Enabled = false;
                Button_HubEinstellenAbbrechen.Enabled = true;
                Button_HubEinstellenSpeichern.Enabled = false;
                Button_StartpunktAnfahren.Enabled = false;
                CheckBox_HubEingestellt.Checked = false;    // Checkbox zurücksetzen
                CheckBox_HubEingestellt.Enabled = false;     // Checkbox sperren
                Panel_HubEinstellen.Visible = true;     // Panel für die Einstellung des Hubs einblenden

                Label_EinzustellendeHub.BackColor = colorStandardGrau;
                NumericUpDown_Hub.BackColor = colorStandardWeiß;

                // Textfelder zurücksetzen
                Label_EingestellteHub.Text = "-----";
                Label_EinzustellendeHub.Text = "-----";
                textBox_HubEinstellenAktuelleAufgabe.Text = "-----";

                // Variablen zurücksetzen
                bHubeinstellungAbgeschlossen = false;
                bStartpunktAngefahren = false;
                bWiderstandUeberprueft = false;

                bFensterHubEinstellenOffen = true;      // Merker Fenster geöffnet
            }
        }

        //-----------------------------------------------------
        // Button der den Motor um 360 Grad drehen lässt und den aktuellen Hub ermittelt
        //-----------------------------------------------------
        private void button_HubErmitteln_Click(object sender, EventArgs e)
        {
            // Alle Button sperren
            button_HubErmitteln.Enabled = false;
            Button_MesstasterNullen.Enabled = false;
            Button_StartpunktAnfahren.Enabled = false;
            Button_HubEinstellenAbbrechen.Enabled = false;

            int iCase = 0;
            
            bool bResetDigitalanzeigeAbgeschlossen = false;   // Merker für den Reset der Digitalanzeige
            bool bHubErmittelt = false;

            double dMinWertDigitalanzeige = 0.0;
            double dMaxWertDigitalanzeige = 0.0;
            double dAktuellerHub = 0.0;

            //-----------------------------------------------------
            // Zustandsautomat für die Ermittlung des aktuellen Hubs
            //-----------------------------------------------------
            // Solange der Hub nicht ermittelt wurde und kein Fehler aufgetreten ist
            while (bHubErmittelt == false && bErrorDigitalanzeige == false && bErrorTwinCAT == false && bErrorSicherheitseinrichtung == false && bGetMesstasterVorhanden == true)
            {
                switch (iCase)
                {
                    //-----------------------------------------------------
                    // Servomotor einschalten, wenn ausgeschaltet
                    //-----------------------------------------------------
                    case 0:
                        if (bGetMotorEingeschaltet == false)    // Motor ausgeschaltet?
                        {
                            // Drehrichung vorgeben
                            bSetDrehrichtungRechts = false;
                            bSetDrehrichtungLinks = true;

                            // Motor einschalten
                            ServoMotor_Einschalten();
                        }

                        iCase = 1; // Nächster Step

                        break;

                    //-----------------------------------------------------
                    // Aktuelle Position und Soll Position auf 0 setzen
                    //-----------------------------------------------------
                    case 1:
                        // Positionsvorgabe 0
                        dSetIstPosition = 0;
                        bSetClearPositionLag = false;   // Schleppabstand nicht löschen

                        ServoMotor_SetPosition();   // Position setzen

                        iCase = 2;  // Nächster Step

                        break;

                    //-----------------------------------------------------
                    // Anzeige der Digitalanzeige reseten
                    //-----------------------------------------------------
                    case 2:
                        Digitalanzeige_ResetAnzeige();

                        iCase = 3;   // Nächsten Step

                        break;

                    //-----------------------------------------------------
                    // Min und Max Werte der Digitalanzeige reseten
                    //-----------------------------------------------------
                    case 3:
                        Digitalanzeige_MinMaxReset();

                        iCase = 5;   // Überprüfen ob beide Wert 0 sind

                        break;

                    //-----------------------------------------------------
                    // Motor um 360 Grad drehen
                    //-----------------------------------------------------
                    case 4:
                        dSetFirstZielposition = 360 * (-1); // 360 Grad Vorgabe
                        dSetFirstZielgeschwindigkeit = 300;   // in Grad/s
                        dSetAcceleration = 0;           // Default Beschleunigung nutzen
                        dSetDeceleration = 0;           // Default Verzögerung nutzen
                        bSetUngeregelteFahrbefehl = true;
                        bSetGereglteFahrbefehl = false;

                        // Motor drehen
                        ServoMotor_DrehungAufZielposition();

                        iCase = 5;  // Nächster Step

                        break;

                    //-----------------------------------------------------
                    // Min Wert der Digitalanzeige auslesen
                    //-----------------------------------------------------
                    case 5:
                        dMinWertDigitalanzeige = Digitalanzeige_MinWertAuslesen();

                        if (bErrorDigitalanzeige == false)  // Kein Fehler aufgetreten?
                        {
                            iCase = 6;
                        }

                        break;

                    //-----------------------------------------------------
                    // Max Wert der Digitalanzeige auslesen
                    //-----------------------------------------------------
                    case 6:
                        dMaxWertDigitalanzeige = Digitalanzeige_MaxWertAuslesen();

                        if (bErrorDigitalanzeige == false)  // Kein Fehler aufgetreten?
                        {
                            // Reset der Digitalanzeige noch nicht abgeschlossen, dh Kontrolle ob 0 gesetzt wurde
                            if (bResetDigitalanzeigeAbgeschlossen == false)
                            {
                                // Liegt Min-Wert zwischen -2 µm und 2 µm
                                if (dMinWertDigitalanzeige >= -2 && dMinWertDigitalanzeige <= 2)
                                {
                                    // Liegt Max-Wert zwischen -2 µm und 2 µm
                                    if (dMaxWertDigitalanzeige >= -2 && dMaxWertDigitalanzeige <= 2)
                                    {
                                        bResetDigitalanzeigeAbgeschlossen = true;   // Merker Reset abgeschlossen
                                        iCase = 4;  // Hub kann ermittelt werden
                                    }
                                }

                                // Reset noch nicht abgeschlossen
                                if (bResetDigitalanzeigeAbgeschlossen == false)
                                {
                                    iCase = 2;  // Min und Max Wert erneut reseten
                                }
                            }
                            else
                            {
                                iCase = 7;    // Nächsten Step
                            }
                        }

                        break;

                    //-----------------------------------------------------
                    // Differenz (Hub) zwischen Min und Max berechnen und darstellen
                    //-----------------------------------------------------
                    case 7:
                        dAktuellerHub = Math.Abs(dMaxWertDigitalanzeige - dMinWertDigitalanzeige);
                        iAktuellerHub = Convert.ToInt16(dAktuellerHub);

                        Label_EingestellteHub.Text = dAktuellerHub.ToString();

                        iCase = 9;

                        break;

                    //-----------------------------------------------------
                    // Motor abschalten und Steuerelemente verwalten
                    //-----------------------------------------------------
                    case 9:
                        ServoMotor_Ausschalten();

                        // Motor ausgeschaltet und kein Timeout?
                        if (bGetMotorEingeschaltet == false && bErrorTwinCAT == false)
                        {
                            bHubErmittelt = true;   // Merker, dass der Hub ermittelt wurde
                        }

                        break;

                    default:

                        break;
                }
            }

            //-----------------------------------------------------
            // Fehlerbehandlung
            //-----------------------------------------------------
            // Fehler aufgetreten
            if (bErrorDigitalanzeige == true || bErrorTwinCAT == true || bErrorSicherheitseinrichtung == true)
            {
                // Fenster Hubeinstellung schließen
                // Steuerelemente zurücksetzen
                Panel_HubEinstellen.Visible = false;    // Panel für die Einstellung des Hubs ausblenden
                Button_HubEinstellen.Enabled = false;   // Button sperren, da Digitalanzeige nicht verbunden ist

                // Variablen zurücksetzen
                bHubeinstellungAbgeschlossen = false;
                bStartpunktAngefahren = false;
                bFensterHubEinstellenOffen = false;

                Fehlerbehandlung();     // Methode zur Fehlerbehandlung aufrufen
            }
            else if (bGetMesstasterVorhanden == false) // Messtaster nicht geparkt?
            {
                MessageBox.Show("Messtaster ist nicht geparkt.");

                // Button freigeben
                button_HubErmitteln.Enabled = true;
                Button_HubEinstellenAbbrechen.Enabled = true;
            }
            else
            {
                // Steuerelemente verwalten
                button_HubErmitteln.Enabled = true;         // Button wieder freigeben
                Button_MesstasterNullen.Enabled = true;     // Button zum Nullen des Messtasters freigeben
                Button_HubEinstellenAbbrechen.Enabled = true;

                bGetMesstasterGenullt = false;              // Merker, dass der Messtaster genullt wurde zurücksetzen
            }
        }

        //-----------------------------------------------------
        // Button um den Messtaster S5 für die Hubeinstellung zu Nullen
        //-----------------------------------------------------
        private void Button_MesstasterNullen_Click(object sender, EventArgs e)
        {
            // Button sperren
            Button_MesstasterNullen.Enabled = false;
            Button_HubEinstellenAbbrechen.Enabled = false;
            button_HubErmitteln.Enabled = false;

            decimal decHub = NumericUpDown_Hub.Value;    // Feld auslesen
            int iEinzustellendeWeg = 0;
            string strEinzustellendeWeg;

            //-----------------------------------------------------
            // Messtaster nullen
            //-----------------------------------------------------
            TwinCAT_MesstasterNullen();

            // Kein Fehler mit TwinCAT aufgetreten?
            if (bErrorTwinCAT == false)
            {
                //-----------------------------------------------------
                // Einzustellenden Weg auf Basis des aktuellen Hubs berechnen
                //-----------------------------------------------------
                // Vorgegebene Hub = 0?
                if (decimal.ToInt16(decHub) == 0)
                {
                    // Fehler, da Feld nicht gefüllt ist
                    strEinzustellendeWeg = "Fehler";
                    Label_EinzustellendeHub.Text = strEinzustellendeWeg;

                    // Fehlerhafte Felder kennzeichnen
                    Label_EinzustellendeHub.BackColor = Color.Red;
                    NumericUpDown_Hub.BackColor = Color.Red;

                    CheckBox_HubEingestellt.Enabled = false;     // Checkbox sperren
                }
                else // Feld korrekt gefüllt
                {
                    iEinzustellendeWeg = (decimal.ToInt16(decHub) - iAktuellerHub) / 2;
                    strEinzustellendeWeg = iEinzustellendeWeg.ToString();
                    Label_EinzustellendeHub.Text = strEinzustellendeWeg;
                    NumericUpDown_Hub.BackColor = Color.White;              // Kennzeichnung aufheben

                    // Potentielle Färbungen zurücksetzen
                    Label_EinzustellendeHub.BackColor = colorStandardGrau;
                    NumericUpDown_Hub.BackColor = colorStandardWeiß;

                    CheckBox_HubEingestellt.Enabled = true;     // Checkbox freigeben
                }

                Button_MesstasterNullen.Enabled = true;    // Button freigeben
                Button_HubEinstellenAbbrechen.Enabled = true;
                button_HubErmitteln.Enabled = true;
            }
            else
            {
                Fehlerbehandlung();
            }
            
        }

        //-----------------------------------------------------
        // Checkbox um die Hubeinstellung zu bestätigen
        //-----------------------------------------------------
        private void CheckBox_HubEingestellt_CheckedChanged(object sender, EventArgs e)
        {
            // Abfrage ob die Checkbox gecheckt ist oder nicht
            if (CheckBox_HubEingestellt.Checked == true)
            {
                bHubEingestellt = true; // Merker Hub eingestellt

                // Prüfbeschreibung sperren
                //groupBox_DatenZurPruefung.Enabled = false;
                //groupBox_Prüfungsdurchführung.Enabled = false;
                //groupBox_Pruefungsende.Enabled = false;

                groupBox_Reibkraftmessung.Enabled = false;

                NumericUpDown_Hub.Enabled = false;

                Button_MesstasterNullen.Enabled = false;    // Button sperren
                button_HubErmitteln.Enabled = false;
                Button_LetztePruefbeschreibungLaden.Enabled = false;

                // Ist die Schutzabdeckung geschlossen?
                if (bGetAbdeckungGeschlossen == true)
                {
                    Button_StartpunktAnfahren.Enabled = true;
                }
            }
            else
            {
                bHubEingestellt = false; // Merker Hub nicht eingestellt

                // Prüfbeschreibung freigeben
                //groupBox_DatenZurPruefung.Enabled = true;
                //groupBox_Prüfungsdurchführung.Enabled = true;
                //groupBox_Pruefungsende.Enabled = true;

                groupBox_Reibkraftmessung.Enabled = true;

                NumericUpDown_Hub.Enabled = true;

                // Steuerelemente einblenden/ausblenden
                Button_MesstasterNullen.Enabled = true;    // Button sperren
                button_HubErmitteln.Enabled = true;
                Button_StartpunktAnfahren.Enabled = false;
                Button_LetztePruefbeschreibungLaden.Enabled = true;
            }
        }

        //-----------------------------------------------------
        // Button um den Startpunkt des Servomotors anzufahren
        //-----------------------------------------------------
        private void Button_StartpunktAnfahren_Click(object sender, EventArgs e)
        {
            // Steuerelemente sperren/freigeben
            Button_StartpunktAnfahren.Enabled = false;
            Button_HubEinstellenAbbrechen.Enabled = true;
            CheckBox_HubEingestellt.Enabled = false;

            bStartpunktAngefahren = false;     // Merker Startpunkt nicht gefunden
            bStartpunktAnfahrenAbbrechen = false;

            //-----------------------------------------------------
            // Startpunkt Anfahren durch BackgroundWorker im neuen Thread
            //-----------------------------------------------------
            // Backgroundworker nicht aktiv?
            if (backgroundWorker_StartpunktAnfahren.IsBusy != true)
            {
                textBox_HubEinstellenAktuelleAufgabe.Text = "Startpunkt suchen";

                backgroundWorker_StartpunktAnfahren.RunWorkerAsync();
            }
        }

        //-----------------------------------------------------
        // Button der das Modul Hub einstellen schließt ohne zu speichern
        //-----------------------------------------------------
        private void Button_HubEinstellenAbbrechen_Click(object sender, EventArgs e)
        {
            bHubEingestellt = false;

            // Backgroundworker aktiv? - Startpunkt wird somit angefahren
            if (backgroundWorker_StartpunktAnfahren.IsBusy == true)
            {
                Invoke(new Action(() => textBox_HubEinstellenAktuelleAufgabe.Text = "Abgebrochen"));

                bStartpunktAnfahrenAbbrechen = true;    // Merker das die Bewegung abgebrochen werden soll
            }
            else
            {
                // Fahrbefehl für den Motor gesetzt?
                if (bGetFahrbefehlGestartet == true)
                {
                    ServoMotor_Stop();  // Servomotor stoppen
                }

                // Motor eingeschaltet?
                if (bGetMotorEingeschaltet == true)
                {
                    ServoMotor_Ausschalten();   // Motor ausschalten
                }

                //-----------------------------------------------------
                // Fehlerbehandlung
                //-----------------------------------------------------
                if (bErrorTwinCAT == true)  // Fehler aufgetreten?
                {
                    Fehlerbehandlung();
                }

                //-----------------------------------------------------
                // Steuerelemente visualisieren und Variablen setzen
                //-----------------------------------------------------
                // Steuerelemente visualisieren
                Panel_HubEinstellen.Visible = false;    // Panel für die Einstellung des Hubs ausblenden
                CheckBox_HubEingestellt.Checked = false;    // Checkbox zurücksetzen

                // Potentielle Färbungen zurücksetzen
                Label_EinzustellendeHub.BackColor = colorStandardGrau;
                NumericUpDown_Hub.BackColor = colorStandardWeiß;

                // Variablen zurücksetzen
                bHubeinstellungAbgeschlossen = false;
                bStartpunktAngefahren = false;
                bFensterHubEinstellenOffen = false;

                // Wenn ein Fehler aufgetreten ist - TwinCAT oder Digitalanzeige
                if (bErrorTwinCAT == true || bErrorDigitalanzeige == true || bErrorSicherheitseinrichtung == true)
                {
                    Button_HubEinstellen.Enabled = false;
                }
                else
                {
                    Button_HubEinstellen.Enabled = true;
                }
            }
        }

        //-----------------------------------------------------
        // Button der das Modul Hub einstellen schließt und speichert, dass der Hub eingestellt wurde
        //-----------------------------------------------------
        private void Button_HubEinstellenSpeichern_Click(object sender, EventArgs e)
        {
            Panel_HubEinstellen.Visible = false;        // Panel für die Einstellung des Hubs ausblenden
            
            Button_HubEinstellen.Enabled = false;       // Button weiter sperren, da der Hub eingestellt wurde

            bFensterHubEinstellenOffen = false;     // Merker Fenster wieder geschlossen

            bHubEingestellt = false;
            bHubeinstellungAbgeschlossen = true;                 // Merker Hub ist eingestellt

            // Soll während der Prüfung die Reibkraft gemessen werden?
            if (checkBox_ReibkraftMessen.Checked == true)
            {
                // Checkbox freigeben, da nachträglich diese Funktion abgewählt werden kann.
                // Anwählen jedoch nicht wieder möglich
                groupBox_Reibkraftmessung.Enabled = true;
            }

            // Wenn das Milliohmmeter verbunden ist, Button zur Widerstandsüberprüfung freigeben
            if (bMilliohmmeterVerbunden == true && bErrorMilliohmmeter == false)
            {
                Button_WiderstandPruefen.Enabled = true;
            }
        }

        //-----------------------------------------------------
        // Button um den Servomotor 1° nach Links zu drehen
        //-----------------------------------------------------
        private void button_MotorLinks1_Click(object sender, EventArgs e)
        {

        }



        //-----------------------------------------------------
        // Button der das Modul Widerstand überprüfen öffnet
        //-----------------------------------------------------
        private void Button_WiderstandPruefen_Click(object sender, EventArgs e)
        {
            // Masse der Heizpatronen abschalten
            try
            {
                bSetHeizpatronenMasseSchalten = false;
                tcClient.WriteAny(hSetHeizpatronenMasseSchalten, bSetHeizpatronenMasseSchalten);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            bWiderstandUeberprueft = false;
            Button_WiderstandPruefen.Enabled = false;
            Button_WiderstandOk.Enabled = false;

            Button_WiderstandAbbrechen.Enabled = true;
            Panel_WiderstandPruefen.Visible = true;
            Label_WiderstandWert.Text = "-----";    // Feld zurücksetzen
        }

        //-----------------------------------------------------
        // Button der den Widerstand misst
        //-----------------------------------------------------
        private void Button_WiderstandMessen_Click(object sender, EventArgs e)
        {
            Button_WiderstandMessen.Enabled = false;
            Button_WiderstandOk.Enabled = false;
            Button_WiderstandAbbrechen.Enabled = false;

            string strMesswert = "";
            double dMesswert;

            int iCase = 0;

            bool bMessungAbgeschlossen = false;
            bool bMesswertFehlerhaft = false;


            while (bMessungAbgeschlossen == false && bErrorMilliohmmeter == false)
            {
                switch (iCase)
                {
                    //-----------------------------------------------------
                    // Widertstandsmessung starten
                    //-----------------------------------------------------
                    case 0:
                        iTimerWartezeitWiderstandsmessung = 250;    // 250 ms Messzeit

                        Milliohmmeter_MessungStarten();             // Messung starten

                        iCase = 1;

                        break;

                    //-----------------------------------------------------
                    // Timer für die Messzeit starten
                    //-----------------------------------------------------
                    case 1:
                        bWartezeitWiderstandsmessungAbgelaufen = false;    // Merker für Widerstandsmessung abgelaufen rücksetzen
                        Timer_WartezeitWiderstandsmessung.Interval = iTimerWartezeitWiderstandsmessung;
                        Timer_WartezeitWiderstandsmessung.Enabled = true;    // Timer starten

                        iCase = 2;

                        break;

                    //-----------------------------------------------------
                    // Timer für die Messzeit abgelaufen? Abfragen, ob Messwert vorhanden ist
                    //-----------------------------------------------------
                    case 2:
                        if (bWartezeitWiderstandsmessungAbgelaufen == true)
                        {
                            // Timer stoppen
                            Timer_WartezeitWiderstandsmessung.Enabled = false;
                            bWartezeitWiderstandsmessungAbgelaufen = false; // Merker zurücksetzen

                            if (Milliohmmeter_IstMessungAbgeschlossen() == true)    // Messwert vorhanden?
                            {
                                iCase = 3;
                            }
                            else
                            {
                                iTimerWartezeitWiderstandsmessung = 10; // 10 ms zusätzliche Messzeit

                                iCase = 1;  // Timer neu starten
                            }
                        }
                        else
                        {
                            iCase = 2;
                        }

                        break;

                    //-----------------------------------------------------
                    // Messwert auslesen
                    //-----------------------------------------------------
                    case 3:
                        strMesswert = Milliohmmeter_MesswertAuslesen();

                        iCase = 4;

                        break;

                    //-----------------------------------------------------
                    // Messwert auswerten und Widerstand errechnen
                    //-----------------------------------------------------
                    case 4:
                        // Widerstand zu Groß?
                        if (strMesswert == ">>>")    // Zeichen dafür, das keine leitende Verbindung besteht
                        {
                            Label_WiderstandWert.Text = "inf"; // Widerstand anzeigen

                            bMesswertFehlerhaft = true;
                        }
                        else
                        {
                            // Einheit kürzen
                            if (strMesswert.Contains("MOHM"))    // Einheit in Milliohm?
                            {
                                strMesswert = strMesswert.Replace("MOHM", ""); // Einheit in Milliohm entfernen
                            }
                            else
                            {
                                strMesswert.Replace("OHM", ""); // Einheit in OHM entfernen
                                dMesswert = Convert.ToDouble(strMesswert);
                                dMesswert = dMesswert * 1000.0; // Einheit in mOhm wandeln
                                strMesswert = dMesswert.ToString();
                            }

                            Label_WiderstandWert.Text = strMesswert; // Ergebnis darstellen

                            bMesswertFehlerhaft = false;
                        }

                        bMessungAbgeschlossen = true;   // Merker, Messung ist abgeschlossen

                        break;
                }
            }

            Timer_WartezeitWiderstandsmessung.Enabled = false;  // Timer stoppen
            bWartezeitWiderstandsmessungAbgelaufen = false; // Merker zurücksetzen

            // Messung erfolgreich abgeschlossen und Messwert nicht fehlerhaft?
            if (bMessungAbgeschlossen == true && bMesswertFehlerhaft == false)
            {
                Button_WiderstandOk.Enabled = true;     // Button zum Bestätigen freigeben
                Button_WiderstandMessen.Enabled = true;
                Button_WiderstandAbbrechen.Enabled = true;

            }
            // Messung erfolgreich abgeschlossen und Messwert fehlerhaft?
            else if (bMessungAbgeschlossen == true && bMesswertFehlerhaft == true)
            {
                Button_WiderstandOk.Enabled = false;        // Button sperren, da der Widerstand nicht ok ist
                Button_WiderstandMessen.Enabled = true;
                Button_WiderstandAbbrechen.Enabled = true;
            }
            else // Fehler in der Kommunikation
            {
                Fehlerbehandlung();   // Software Reset auf Grund eines Fehlers

                Button_WiderstandPruefen.Enabled = false;

                Panel_WiderstandPruefen.Visible = false;
                Button_WiderstandMessen.Enabled = false;
                Button_WiderstandOk.Enabled = false;
            }
        }

        //-----------------------------------------------------
        // Button der Bestätigt, dass der Widerstand ok ist
        //-----------------------------------------------------
        private void Button_WiderstandOk_Click(object sender, EventArgs e)
        {
            Panel_WiderstandPruefen.Visible = false;
            bWiderstandUeberprueft = true;

            checkBox_PruefungMitTemperatur.Enabled = true;  // Checkbox für Prüfung unter Temperatur freigeben

            Button_Start.Enabled = true;    // Button zum Starten einer Prüfung freigeben
        }

        //-----------------------------------------------------
        // Button der die Überprüfung abbricht
        //-----------------------------------------------------
        private void Button_WiderstandAbbrechen_Click(object sender, EventArgs e)
        {
            Button_WiderstandPruefen.Enabled = true;
            Panel_WiderstandPruefen.Visible = false;
            bWiderstandUeberprueft = false;
        }

        private void Label_Artikelnummer_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Label_GrenzwertZyklen_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        

        private void Visualisierung_NotAus_Click(object sender, EventArgs e)
        {

        }

        

        private void Visualisierung_PneumatikdruckVorhanden_Click(object sender, EventArgs e)
        {

        }

        private void Visualisierung_AbdeckungGeschlossen_Click(object sender, EventArgs e)
        {

        }

        private void Visualisierung_MikroschalterMesstaster_Click(object sender, EventArgs e)
        {

        }

        private void chart_Reibkraft_Click(object sender, EventArgs e)
        {

        }

        //-----------------------------------------------------
        // wird bei Drücken des Buttons aufgerufen
        // Es wird die Prüfungbeschreibung der letzten Prüfung aus einer .ini Datei ausgelesen
        //-----------------------------------------------------
        private void Button_LetztePruefbeschreibungLaden_Click(object sender, EventArgs e)
        {
            Button_LetztePruefbeschreibungLaden.Enabled = false;

            // Element Prüfer auslesen und Maske füllen
            try
            {
                string sElement = "Pruefer";
                string value = ConfigFile.GetValue(strSectionLetztePruefungsbeschreibung, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                TextBox_Pruefer.Clear();
                TextBox_Pruefer.AppendText(value);
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element Artikelnummer auslesen und Maske füllen
            try
            {
                string sElement = "Artikelnummer";
                string value = ConfigFile.GetValue(strSectionLetztePruefungsbeschreibung, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                TextBox_Artikelnummer.Clear();
                TextBox_Artikelnummer.AppendText(value);
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element Projektnummer auslesen und Maske füllen
            try
            {
                string sElement = "Projektnummer";
                string value = ConfigFile.GetValue(strSectionLetztePruefungsbeschreibung, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                TextBox_Projektnummer.Clear();
                TextBox_Projektnummer.AppendText(value);
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element Auftragsnummer auslesen und Maske füllen
            try
            {
                string sElement = "Auftragsnummer";
                string value = ConfigFile.GetValue(strSectionLetztePruefungsbeschreibung, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                TextBox_Auftragsnummer.Clear();
                TextBox_Auftragsnummer.AppendText(value);
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element Schmierstoff auslesen und Maske füllen
            try
            {
                string sElement = "Schmierstoff";
                string value = ConfigFile.GetValue(strSectionLetztePruefungsbeschreibung, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                TextBox_Schmierstoff.Clear();
                TextBox_Schmierstoff.AppendText(value);
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element Beschreibung auslesen und Maske füllen
            try
            {
                string sElement = "Beschreibung";
                string value = ConfigFile.GetValue(strSectionLetztePruefungsbeschreibung, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                TextBox_Beschreibung.Clear();
                TextBox_Beschreibung.AppendText(value);
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element Hub auslesen und Maske füllen
            try
            {
                string sElement = "Hub";
                string value = ConfigFile.GetValue(strSectionLetztePruefungsbeschreibung, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                // String in int wandeln
                try
                {
                    NumericUpDown_Hub.Value = Int32.Parse(value);
                }
                catch 
                {
                    sExceptionText = "Eintrag von " + sElement + " kann nicht in eine Zahl gewandelt werden.";
                    MessageBox.Show(sExceptionText);
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element Kontaktkraft auslesen und Maske füllen
            try
            {
                string sElement = "Kontaktkraft";
                string value = ConfigFile.GetValue(strSectionLetztePruefungsbeschreibung, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                // String in double wandeln -> Dezimalschreibweise mit Komma
                try
                {
                    NumericUpDown_Kontaktkraft.Value = (decimal)Double.Parse(value);
                }
                catch
                {
                    sExceptionText = "Eintrag von " + sElement + " kann nicht in eine Zahl gewandelt werden.";
                    MessageBox.Show(sExceptionText);
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element Federweg auslesen und Maske füllen
            try
            {
                string sElement = "Federweg";
                string value = ConfigFile.GetValue(strSectionLetztePruefungsbeschreibung, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                // String in int wandeln
                try
                {
                    NumericUpDown_Federweg.Value = Int32.Parse(value);
                }
                catch
                {
                    sExceptionText = "Eintrag von " + sElement + " kann nicht in eine Zahl gewandelt werden.";
                    MessageBox.Show(sExceptionText);
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element Wartezeit auslesen und Maske füllen
            try
            {
                string sElement = "Wartezeit";
                string value = ConfigFile.GetValue(strSectionLetztePruefungsbeschreibung, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                // String in int wandeln
                try
                {
                    NumericUpDown_Wartezeit.Value = Int32.Parse(value);
                }
                catch
                {
                    sExceptionText = "Eintrag von " + sElement + " kann nicht in eine Zahl gewandelt werden.";
                    MessageBox.Show(sExceptionText);
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element WiderstandPruefungsende auslesen und Maske füllen
            try
            {
                string sElement = "WiderstandPruefungsende";
                string value = ConfigFile.GetValue(strSectionLetztePruefungsbeschreibung, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                // String in int wandeln
                try
                {
                    NumericUpDown_RGrenzwertEnde.Value = Int32.Parse(value);
                }
                catch
                {
                    sExceptionText = "Eintrag von " + sElement + " kann nicht in eine Zahl gewandelt werden.";
                    MessageBox.Show(sExceptionText);
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element ZyklenPruefungsende auslesen und Maske füllen
            try
            {
                string sElement = "ZyklenPruefungsende";
                string value = ConfigFile.GetValue(strSectionLetztePruefungsbeschreibung, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                // String in int wandeln
                try
                {
                    NumericUpDown_GrenzwertZyklen.Value = Int32.Parse(value);
                }
                catch
                {
                    sExceptionText = "Eintrag von " + sElement + " kann nicht in eine Zahl gewandelt werden.";
                    MessageBox.Show(sExceptionText);
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element SollTemperaturOben auslesen und Maske füllen
            try
            {
                string sElement = "SollTemperaturOben";
                string value = ConfigFile.GetValue(strSectionLetztePruefungsbeschreibung, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                // String in int wandeln
                try
                {
                    numericUpDown_SollTemperaturOben.Value = Int32.Parse(value);
                }
                catch
                {
                    sExceptionText = "Eintrag von " + sElement + " kann nicht in eine Zahl gewandelt werden.";
                    MessageBox.Show(sExceptionText);
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            // Element SollTemperaturUnten auslesen und Maske füllen
            try
            {
                string sElement = "SollTemperaturUnten";
                string value = ConfigFile.GetValue(strSectionLetztePruefungsbeschreibung, sElement);
                if (value == null)  // Abfrage ob Element nicht vorhanden war
                {
                    sExceptionText = "Eintrag " + sElement + " nicht gefunden.";
                    throw new ArgumentNullException();
                }

                // String in int wandeln
                try
                {
                    numericUpDown_SollTemperaturUnten.Value = Int32.Parse(value);
                }
                catch
                {
                    sExceptionText = "Eintrag von " + sElement + " kann nicht in eine Zahl gewandelt werden.";
                    MessageBox.Show(sExceptionText);
                }
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }

            Button_LetztePruefbeschreibungLaden.Enabled = true;
        }

        //-----------------------------------------------------
        // Einstellungsfenster für die Kommunikation mit den angeschlossenen Geräten
        //-----------------------------------------------------
        private void toolStripMenu_Einstellungen_Click(object sender, EventArgs e)
        {
            //-----------------------------------------------------
            // Neues Fenster erstellen
            //-----------------------------------------------------
            Form_Einstellungen FormEinstellungKommunikation = new Form_Einstellungen();

            //-----------------------------------------------------
            // Einstellungen übergeben
            //-----------------------------------------------------
            FormEinstellungKommunikation.bMilliohmmeterVerbunden = bMilliohmmeterVerbunden;
            FormEinstellungKommunikation.bKraftsensorVerbunden = bKraftsensorVerbunden;
            FormEinstellungKommunikation.bDigitalanzeigeVerbunden = bDigitalanzeigeVerbunden;
            FormEinstellungKommunikation.bTemperaturreglerVerbunden = bTemperaturreglerVerbunden;

            FormEinstellungKommunikation.sComPortMilliohmmeter = strCOMPortMilliohmmeter;
            FormEinstellungKommunikation.sComPortKraftsensor = strCOMPortKraftsensor;
            FormEinstellungKommunikation.sComPortDigitalanzeige = strCOMPortDigitalanzeige;
            FormEinstellungKommunikation.sComPortTemperaturregler = strCOMPortTemperaturregler;

            FormEinstellungKommunikation.iBaudrateMilliohmmeter = iBaudrateMilliohmmeter;
            FormEinstellungKommunikation.iBaudrateDigitalanzeige = iBaudrateDigitalanzeige;
            FormEinstellungKommunikation.iBaudrateTemperaturregler = iBaudrateTemperaturregler;

            FormEinstellungKommunikation.iAdresseTemperaturregler = iAdresseTemperaturregler;
            FormEinstellungKommunikation.iHystereseTemperauturregler = iHystereseTemperaturregler;

            FormEinstellungKommunikation.bReibkraftmessungAktiv = bReibkraftmessungStandardAktiv;

            //-----------------------------------------------------
            // Neues Fenster im Vordergrund öffnen
            //-----------------------------------------------------
            FormEinstellungKommunikation.ShowDialog();

            //-----------------------------------------------------
            // Einstellungen nach dem Schließen auslesen
            //-----------------------------------------------------
            strCOMPortMilliohmmeter = FormEinstellungKommunikation.sComPortMilliohmmeter;
            strCOMPortKraftsensor = FormEinstellungKommunikation.sComPortKraftsensor;
            strCOMPortDigitalanzeige = FormEinstellungKommunikation.sComPortDigitalanzeige;
            strCOMPortTemperaturregler = FormEinstellungKommunikation.sComPortTemperaturregler;

            iBaudrateMilliohmmeter = FormEinstellungKommunikation.iBaudrateMilliohmmeter;
            iBaudrateDigitalanzeige = FormEinstellungKommunikation.iBaudrateDigitalanzeige;
            iBaudrateTemperaturregler = FormEinstellungKommunikation.iBaudrateTemperaturregler;

            iAdresseTemperaturregler = FormEinstellungKommunikation.iAdresseTemperaturregler;
            iHystereseTemperaturregler = FormEinstellungKommunikation.iHystereseTemperauturregler;

            byAdresseTemperaturregler = Convert.ToByte(iAdresseTemperaturregler);

            bReibkraftmessungStandardAktiv = FormEinstellungKommunikation.bReibkraftmessungAktiv;

            //-----------------------------------------------------
            // Einstellungen in csv speichern
            //-----------------------------------------------------
            try
            {
                // Element COMPort Milliohmmeter speichern
                string sElement = "COMPortMilliohmmeter";
                ConfigFile.SetValue(strSectionMilliohmmeter, sElement, strCOMPortMilliohmmeter);

                // Element Baudrate Milliohmmeter speichern
                sElement = "BaudrateMilliohmmeter";
                ConfigFile.SetValue(strSectionMilliohmmeter, sElement, iBaudrateMilliohmmeter.ToString());

                // Element COMPort Kraftsensor speichern
                sElement = "COMPortKraftsensor";
                ConfigFile.SetValue(strSectionKraftsensor, sElement, strCOMPortKraftsensor);

                // Element Reibkraftmessung standardmäßig Aktiv speichern
                sElement = "ReibkraftmessungAktiv";

                if (bReibkraftmessungStandardAktiv == true)
                {
                    ConfigFile.SetValue(strSectionKraftsensor, sElement, "1");
                }
                else
                {
                    ConfigFile.SetValue(strSectionKraftsensor, sElement, "0");
                }

                // Element COMPort Digitalanzeige speichern
                sElement = "COMPortDigitalanzeige";
                ConfigFile.SetValue(strSectionDigitalanzeige, sElement, strCOMPortDigitalanzeige);

                // Element Baudrate Digitalanzeige speichern
                sElement = "BaudrateDigitalanzeige";
                ConfigFile.SetValue(strSectionDigitalanzeige, sElement, iBaudrateDigitalanzeige.ToString());

                // Element COMPort Temperaturregler speichern
                sElement = "COMPortTemperaturregler";
                ConfigFile.SetValue(strSectionTemperaturregler, sElement, strCOMPortTemperaturregler);

                // Element Baudrate Temperaturregler speichern
                sElement = "BaudrateTemperaturregler";
                ConfigFile.SetValue(strSectionTemperaturregler, sElement, iBaudrateTemperaturregler.ToString());

                // Element Adresse Temperaturregler speichern
                sElement = "AdresseTemperaturregler";
                ConfigFile.SetValue(strSectionTemperaturregler, sElement, iAdresseTemperaturregler.ToString());

                // Element Hysterese Temperaturregler speichern
                sElement = "HystereseTemperaturregler";
                ConfigFile.SetValue(strSectionTemperaturregler, sElement, iHystereseTemperaturregler.ToString());
            }
            catch
            {
                MessageBox.Show(sExceptionText);
            }
        }

        //-----------------------------------------------------
        // Methoden für TwinCAT
        //-----------------------------------------------------
        #region TwinCAT Methoden
        //-----------------------------------------------------
        // Methode zum Verbinden mit TwinCAT
        //-----------------------------------------------------
        private void Connect_TwinCAT()
        {
            tcClient_Verbinden();   // Versuch mit TwinCAT zu verbinden

            int iCase = 0;
            int iPufferCase = 0;

            bool bConnectAbgeschlossen = false;

            // Timer für die Wartezeit auf eine Reaktion von TwinCAT initialisieren
            Timer_WartezeitReaktionTwinCAT.Interval = iTimeWartezeitReaktionTwinCAT_Init;
            Timer_WartezeitReaktionTwinCAT.Enabled = false;
            bWartezeitReaktionTwinCATAbgelaufen = false;    // Merker zurücksetzen

            while (bConnectAbgeschlossen == false && bTwinCATVerbunden == true && bErrorTwinCAT == false)
            {
                switch (iCase)
                {
                    // TwinCAT initialisieren
                    case 0:
                        tcClient_Init();

                        iCase = 1;

                        break;

                    // Connect setzen - Angeben, dass die Verbindung neu aufgebaut wird
                    // Reset setzen um potenzielle Fehler zu löschen
                    case 1:
                        try
                        {
                            bSetConnectTwinCAT = true;
                            tcClient.WriteAny(hSetConnect, bSetConnectTwinCAT);

                            // Werte auf Standard setzen
                            tcClient.WriteAny(hSetPositionsFlankeErzeugen, false);
                            tcClient.WriteAny(hSetFahrbefehl, false);
                            tcClient.WriteAny(hSetMotorEinschalten, false);

                            bSetResetTwinCAT = true;
                            tcClient.WriteAny(hSetResetTwinCAT, bSetResetTwinCAT);

                            Timer_WartezeitReaktionTwinCAT.Enabled = true;

                            iCase = 2;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            bErrorTwinCAT = true;
                        }

                        break;

                    // Warten, dass der Timer abgelaufen ist
                    case 2:
                        // Timer abgelaufen?
                        if (bWartezeitReaktionTwinCATAbgelaufen == true)
                        {
                            iCase = 3;
                        }

                        break;

                    // Fahrbefehl löschen
                    case 3:                        
                        try
                        {
                            // Fahrbefehl stoppen
                            bSetUngeregelteFahrbefehl = false;
                            bSetGereglteFahrbefehl = false;
                            tcClient.WriteAny(hSetFahrbefehl, bSetUngeregelteFahrbefehl);
                            tcClient.WriteAny(hSetGeregelteFahrt, bSetGereglteFahrbefehl);

                            // Achse bereits gestoppt? - Hier negierte Logik
                            if (bGetAchseGestoppt == false)
                            {
                                iCase = 6;  // Stop Befehl zurück nehmen
                            }
                            else
                            {
                                iCase = 4;  // Achse stoppen
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            bErrorTwinCAT = true;
                        }

                        break;

                    // Achse stoppen
                    case 4:
                        try
                        {
                            tcClient.WriteAny(hSetAchseStop, true);

                            // Timer starten
                            Timer_TimeoutTwinCAT.Interval = iTimeTimeoutTwinCAT;
                            Timer_TimeoutTwinCAT.Enabled = true;

                            iCase = 5;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            bErrorTwinCAT = true;
                        }

                        break;

                    // Warten, dass der Motor gestoppt ist
                    case 5:
                        while (bGetAchseGestoppt == false && bTimeoutTwinCAT == false)
                        {

                        }

                        Timer_TimeoutTwinCAT.Enabled = false;   // Timer stoppen

                        // Timeout eingetreten?
                        if (bTimeoutTwinCAT == true)
                        {
                            bErrorTwinCAT = true;
                        }
                        else
                        {
                            iCase = 6;
                        }

                        break;

                    // Stop Befehl zurück nehmen
                    case 6:
                        try
                        {
                            tcClient.WriteAny(hSetAchseStop, false);

                            iCase = 7;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            bErrorTwinCAT = true;
                        }

                        break;

                    // Motor ausschalten
                    case 7:
                        try
                        {
                            tcClient.WriteAny(hSetDrehrichtungPositiv, false);
                            tcClient.WriteAny(hSetDrehrichtungNegativ, false);
                            tcClient.WriteAny(hSetMotorEinschalten, false);

                            Visualisierung_MotorEingeschaltet.BackColor = colorStandardGrau;

                            iCase = 8;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            bErrorTwinCAT = true;
                        }

                        break;

                    // Druckluft zuschalten
                    case 8:
                        try
                        {
                            bSetDruckluftAbschalten = false;
                            tcClient.WriteAny(hSetDruckluftAbschalten, bSetDruckluftAbschalten);

                            iCase = 9;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            bErrorTwinCAT = true;
                        }

                        break;

                    // Heizpatronen abschalten
                    case 9:
                        try
                        {
                            bSetHeizpatronenMasseSchalten = false;
                            tcClient.WriteAny(hSetHeizpatronenMasseSchalten, bSetHeizpatronenMasseSchalten);

                            bSetHeizungFreigeben = false;
                            tcClient.WriteAny(hSetHeizpatronenFreigeben, bSetHeizungFreigeben);

                            iCase = 10;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            bErrorTwinCAT = true;
                        }

                        break;


                    // Connect löschen
                    // Reset löschen
                    case 10:
                        // Connect löschen
                        try
                        {
                            bSetConnectTwinCAT = false;
                            tcClient.WriteAny(hSetConnect, bSetConnectTwinCAT);

                            bSetResetTwinCAT = false;
                            tcClient.WriteAny(hSetResetTwinCAT, bSetResetTwinCAT);

                            bConnectAbgeschlossen = true;   // Merker Connect abgeschlossen
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            bErrorTwinCAT = true;
                        }

                        break;
                }
            }

            // Timeout bei der Kommunikation mit TwinCAT aufgetreten?
            if (bTimeoutTwinCAT == true)
            {
                MessageBox.Show("Kommunikation TwinCAT:\n\n" + "Timeout bei der Kommunikation.");

                bConnectAbgeschlossen = false;
            }
            else if (bErrorTwinCAT == true) // Allgemeiner Fehler bei der Kommunikation mit TwinCAT?
            {
                MessageBox.Show("Kommunikation TwinCAT:\n\n" + "Fehler bei der Kommunikation mit TwinCAT.");

                bConnectAbgeschlossen = false;
            }

            // Warten um TwinCAT Zeit zum Reagieren zu geben
            bWartezeitReaktionTwinCATAbgelaufen = false;
            Timer_WartezeitReaktionTwinCAT.Interval = 1000; // 1 Sekunde warten
            Timer_WartezeitReaktionTwinCAT.Enabled = true;

            // Warten, solange der Timer noch nicht abgelaufen ist
            while (bWartezeitReaktionTwinCATAbgelaufen == false)
            {

            }

            // Verbindung hergestellt/abgeschlossen?
            if (bConnectAbgeschlossen == true)
            {
                
                bTwinCATVerbunden = true;
            }
            else
            {
                bTwinCATVerbunden = false;  // Merker für Verbindung erfolgreich löschen
            }
        }

        //-----------------------------------------------------
        // Kommunikation mit TwinCAT aufbauen
        //-----------------------------------------------------
        private void tcClient_Verbinden()
        {
            try
            {
                // Eine neue Instanz der Klasse AdsStream erzeugen
                dataStream = new AdsStream(26);

                // Eine neue Instanz der Klasse BinaryReader erzeugen
                binReader = new BinaryReader(dataStream);

                // Eine neue Instanz der Klasse TcAdsClient erzeugen
                tcClient = new TcAdsClient();

                // Verbinden mit lokaler SPS - Laufzeit 1 - Port 851 
                tcClient.Connect(851);  // "10.149.41.35.1.1"

                bTwinCATVerbunden = true;
            }
            catch
            {
                MessageBox.Show("Fehler beim Laden");

                bErrorTwinCAT = true;
                bTwinCATVerbunden = false;
            }
        }

        //-----------------------------------------------------
        // Initialisierung der Überwachung folgender SPS Variablen
        //-----------------------------------------------------
        private void tcClient_Init()
        {
            try
            {
                // MC_Power
                hGetMotorEingeschaltet = tcClient.AddDeviceNotification("GVL_Var.bGetMotorEingeschaltet", dataStream, 0, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);

                // Set Position
                hGetPositionGesetzt = tcClient.AddDeviceNotification("GVL_Var.bGetPositionGesetzt", dataStream, 1, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);

                // Move Absolute
                hGetFahrbefehlGestartet = tcClient.AddDeviceNotification("GVL_Var.bGetFahrbefehlGestartet", dataStream, 2, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);
                hGetFahrbefehlAbgeschlossen = tcClient.AddDeviceNotification("GVL_Var.bGetFahrbefehlAbgeschlossen", dataStream, 3, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);

                // MC_Stop
                hGetAchseWirdGestoppt = tcClient.AddDeviceNotification("GVL_Var.bGetAchseWirdGestoppt", dataStream, 4, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);
                hGetAchseGestoppt = tcClient.AddDeviceNotification("GVL_Var.bGetAchseGestoppt", dataStream, 5, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);

                // Get Variablen
                hGetNotAus = tcClient.AddDeviceNotification("GVL_Var.bGetNotAus", dataStream, 6, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);
                hGetNotAusQuittiert = tcClient.AddDeviceNotification("GVL_Var.bGetNotAusQuittiert", dataStream, 7, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);
                hGetPneumatikdruckVorhanden = tcClient.AddDeviceNotification("GVL_Var.bGetPneumatikdruckVorhanden", dataStream, 8, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);
                hGetDruckluftZugeschaltet = tcClient.AddDeviceNotification("GVL_Var.bGetDruckluftZugeschaltet", dataStream, 9, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);
                hGetMesstasterVorhanden = tcClient.AddDeviceNotification("GVL_Var.bGetMesstasterVorhanden", dataStream, 10, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);
                hGetMesstasterGenullt = tcClient.AddDeviceNotification("GVL_Var.bGetMesstasterGenullt", dataStream, 11, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);
                hGetAbdeckungGeschlossen = tcClient.AddDeviceNotification("GVL_Var.bGetAbdeckungGeschlossen", dataStream, 12, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);
                hGetKnebelschalterEinrichten = tcClient.AddDeviceNotification("GVL_Var.bGetKnebelschalterEinrichten", dataStream, 13, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);
                hGetKnebelschalterBetrieb = tcClient.AddDeviceNotification("GVL_Var.bGetKnebelschalterBetrieb", dataStream, 14, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);
                hGetHeizpatronenZugeschaltet = tcClient.AddDeviceNotification("GVL_Var.bGetHeizpatronenFreigegeben", dataStream, 15, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);
                hGetHeizpatronenMasseGeschaltet = tcClient.AddDeviceNotification("GVL_Var.bGetHeizpatronenMasseGeschaltet", dataStream, 16, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);
                hGetPositonsFlanke = tcClient.AddDeviceNotification("GVL_Var.bGetPositonsFlanke", dataStream, 17, 1, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);
                hGetMotorposition = tcClient.AddDeviceNotification("GVL_Var.lGetMotorposition", dataStream, 18, 8, AdsTransMode.OnChange, iZyklusZeitTwinCAT, 0, null);



                //-----------------------------------------------------
                // Initialisierung der Variablen, welche an die SPS gesendet werden
                //-----------------------------------------------------
                // MC_Power
                hSetMotorEinschalten = tcClient.CreateVariableHandle("GVL_Var.bSetMotorEinschalten");
                hSetDrehrichtungPositiv = tcClient.CreateVariableHandle("GVL_Var.bSetDrehrichtungPositiv");
                hSetDrehrichtungNegativ = tcClient.CreateVariableHandle("GVL_Var.bSetDrehrichtungNegativ");

                // Set Position
                hSetPosition = tcClient.CreateVariableHandle("GVL_Var.bSetPosition");
                hSetClearPositionLag = tcClient.CreateVariableHandle("GVL_Var.bSetClearPositionLag");
                hSetPositionValue = tcClient.CreateVariableHandle("GVL_Var.rSetPositionValue");

                // Move Absolute
                hSetFahrbefehl = tcClient.CreateVariableHandle("GVL_Var.bSetFahrbefehl");
                hSetGeregelteFahrt = tcClient.CreateVariableHandle("GVL_Var.bSetGeregelteFahrt");
                hSetFirstZielposition = tcClient.CreateVariableHandle("GVL_Var.rSetFirstZielposition");
                hSetSecondZielposition = tcClient.CreateVariableHandle("GVL_Var.rSetSecondZielposition");
                hSetThirdZielposition = tcClient.CreateVariableHandle("GVL_Var.rSetThirdZielposition");
                hSetFirstZielgeschwindigkeit = tcClient.CreateVariableHandle("GVL_Var.rSetFirstZielgeschwindigkeit");
                hSetSecondZielgeschwindigkeit = tcClient.CreateVariableHandle("GVL_Var.rSetSecondZielgeschwindigkeit");
                hSetThirdZielgeschwindigkeit = tcClient.CreateVariableHandle("GVL_Var.rSetThirdZielgeschwindigkeit");
                hSetAcceleration = tcClient.CreateVariableHandle("GVL_Var.rSetAcceleration");
                hSetDeceleration = tcClient.CreateVariableHandle("GVL_Var.rSetDeceleration");

                // MC_Stop
                hSetAchseStop = tcClient.CreateVariableHandle("GVL_Var.bSetAchseStop");

                // Set Variablen
                hSetConnect = tcClient.CreateVariableHandle("GVL_Var.bSetConnect");
                hSetLeuchtmelderBereit = tcClient.CreateVariableHandle("GVL_Var.bSetLeuchtmelderBereit");
                hSetLeuchtmelderStoerung = tcClient.CreateVariableHandle("GVL_Var.bSetLeuchtmelderStoerung");
                hSetDruckluftAbschalten = tcClient.CreateVariableHandle("GVL_Var.bSetDruckluftAbschalten");
                hSetResetTwinCAT = tcClient.CreateVariableHandle("GVL_Var.bSetResetTwinCAT");
                hSetHeizpatronenFreigeben = tcClient.CreateVariableHandle("GVL_Var.bSetHeizpatronenFreigeben");
                hSetMesstasterNullen = tcClient.CreateVariableHandle("GVL_Var.bSetMesstasterNullen");
                hSetHeizpatronenMasseSchalten = tcClient.CreateVariableHandle("GVL_Var.bSetHeizpatronenMasseSchalten");
                hSetPositionsFlankeErzeugen = tcClient.CreateVariableHandle("GVL_Var.bSetPositionsFlankeErzeugen");
                hSetPositonFuerFlanke = tcClient.CreateVariableHandle("GVL_Var.rSetPositonFuerFlanke");

                //-----------------------------------------------------
                // Erstellen eines Events für Änderungen an den SPS-Variablen-Werten
                //-----------------------------------------------------
                tcClient.AdsNotification += new AdsNotificationEventHandler(tcClient_OnNotification);

                /* Bestätigen, dass die Kommunikation hergestellt wurde */
                bTwinCATVerbunden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                /* Bestätigen, dass die Kommunikation nicht hergestellt wurde */
                bTwinCATVerbunden = false;
            }
        }

        //-----------------------------------------------------
        // wird bei Änderung einer SPS-Variablen aufgerufen
        // Abarbeitung hier gering halten
        //-----------------------------------------------------
        private void tcClient_OnNotification(object sender, AdsNotificationEventArgs e)
        {
            bThreadAktiv = true;    // Merker Thread aktiv

            try
            {
                // Setzen der Position von _Event.DataStream auf die des aktuellen benötigten Wertes
                e.DataStream.Position = e.Offset;

                //-----------------------------------------------------
                // Ermittlung welche Variable sich geändert hat
                //-----------------------------------------------------
                if (e.NotificationHandle == hGetMotorEingeschaltet) // Abfrage ob der Motor eingeschaltet wurde
                {
                    if (binReader.ReadBoolean() == true)
                    {
                        bGetMotorEingeschaltet = true;     // Merker Motor ist eingeschaltet
                    }
                    else
                    {
                        bGetMotorEingeschaltet = false;    // Merker Motor ist ausgeschaltet
                    }
                }
                else if (e.NotificationHandle == hGetPositionGesetzt)   // Abfrage ob die Position gesetzt wurde
                {
                    if (binReader.ReadBoolean() == true)
                    {
                        bGetPositionGesetzt = true;
                    }
                    else
                    {
                        bGetPositionGesetzt = false;
                    }
                }
                else if (e.NotificationHandle == hGetFahrbefehlGestartet)   // Abfrage ob der Fahrbefehl gestartet wurde
                {
                    if (binReader.ReadBoolean() == true)
                    {
                        bGetFahrbefehlGestartet = true;
                    }
                    else
                    {
                        bGetFahrbefehlGestartet = false;
                    }
                }
                else if (e.NotificationHandle == hGetFahrbefehlAbgeschlossen)   // Abfrage ob der Fahrbefehl abgeschlossen wurde
                {
                    if (binReader.ReadBoolean() == true)
                    {
                        bGetFahrbefehlAbgeschlossen = true;
                    }
                    else
                    {
                        bGetFahrbefehlAbgeschlossen = false;
                    }
                }
                else if (e.NotificationHandle == hGetAchseWirdGestoppt)      // Abfrage ob die Achse gestoppt wird
                {
                    if (binReader.ReadBoolean() == true)
                    {
                        bGetAchseWirdGestoppt = true;
                    }
                    else
                    {
                        bGetAchseWirdGestoppt = false;
                    }
                }
                else if (e.NotificationHandle == hGetAchseGestoppt)      // Abfrage ob die Achse gestoppt wurde
                {
                    if (binReader.ReadBoolean() == true)
                    {
                        bGetAchseGestoppt = true;
                    }
                    else
                    {
                        bGetAchseGestoppt = false;
                    }
                }
                else if (e.NotificationHandle == hGetNotAus)
                {
                    if (binReader.ReadBoolean() == true)    // Not-Aus gedrückt
                    {
                        bGetNotAus = true;
                        // Invoke: Threatübergreifender Zugriff
                        Visualisierung_NotAus.Invoke(new Action(() => Visualisierung_NotAus.BackColor = Color.Green));
                    }
                    else
                    {
                        bGetNotAus = false;
                        bErrorNotAus = true;
                        bErrorSicherheitseinrichtung = true;        // Merker für allgemeinen Fehler Sicherheitseinrichtungen
                        // Invoke: Threatübergreifender Zugriff
                        Visualisierung_NotAus.Invoke(new Action(() => Visualisierung_NotAus.BackColor = Color.Red));
                    }
                }
                else if (e.NotificationHandle == hGetNotAusQuittiert)
                {
                    if (binReader.ReadBoolean() == true)    // Not-Aus quittieren gedrückt
                    {
                        bGetNotAusQuittiert = true;

                        // Ist ein Sicherheitsfehler aufgetreten?
                        if (bErrorSicherheitseinrichtung == true)
                        {
                            // Fehler nicht mehr vorhanden?
                            if (bGetNotAus == true && bGetPneumatikdruckVorhanden == true && bGetDruckluftZugeschaltet == true && (bGetKnebelschalterEinrichten == true || bGetAbdeckungGeschlossen == true))
                            {
                                // Fehler zurücksetzen
                                bErrorNotAus = false;
                                bErrorAbdeckungGeschlossen = false;
                                bErrorPneumatikdruckVorhanden = false;
                                bErrorVentileEingeschaltet = false;
                                bErrorKnebelschalter = false;
                                bErrorSicherheitseinrichtung = false;        // Merker für allgemeinen Fehler Sicherheitseinrichtungen

                                bFehlerbehandlungSicherheitseinrichtungenAbgeschlossen = false; // Merker zurücksetzen

                                // Alle Geräte weiterhin angeschlossen?
                                if (bMilliohmmeterVerbunden == true && bDigitalanzeigeVerbunden == true && bTwinCATVerbunden == true && bTemperaturreglerVerbunden == true && bKraftsensorVerbunden == true)
                                {
                                    Button_HubEinstellen.Invoke(new Action(() => Button_HubEinstellen.Enabled = true));    // Button für die Hubeinstellung freigeben
                                }
                                else
                                {
                                    // Milliohmmeter nicht verbunden?
                                    if (bMilliohmmeterVerbunden == false)
                                    {
                                        Button_MilliohmmeterVerbinden.Enabled = true;
                                    }

                                    // Kraftsensor nicht verbunden?
                                    if (bKraftsensorVerbunden == false)
                                    {
                                        Button_KraftsensorVerbinden.Enabled = true;
                                    }

                                    // Digitalanzeige nicht verbunden?
                                    if (bDigitalanzeigeVerbunden == false)
                                    {
                                        Button_DigitalanzeigeVerbinden.Enabled = true;
                                    }

                                    // TwinCAT nicht verbunden?
                                    if (bTwinCATVerbunden == false)
                                    {
                                        Button_TwinCATVerbinden.Enabled = true;
                                    }

                                    // Temperaturregler nicht verbunden?
                                    if (bTemperaturreglerVerbunden == false)
                                    {
                                        Button_TemperaturreglerVerbinden.Enabled = true;
                                    }
                                }
                            }
                        }
                        
                    }
                    else
                    {
                        bGetNotAusQuittiert = false;
                    }
                }
                else if (e.NotificationHandle == hGetPneumatikdruckVorhanden)
                {
                    if (binReader.ReadBoolean() == true)    
                    {
                        bGetPneumatikdruckVorhanden = true;
                        // Invoke: Threatübergreifender Zugriff
                        Visualisierung_PneumatikdruckVorhanden.Invoke(new Action(() => Visualisierung_PneumatikdruckVorhanden.BackColor = Color.Green));
                    }
                    else
                    {
                        bGetPneumatikdruckVorhanden = false;
                        bErrorPneumatikdruckVorhanden = true;
                        bErrorSicherheitseinrichtung = true;        // Merker für allgemeinen Fehler Sicherheitseinrichtungen
                        Visualisierung_PneumatikdruckVorhanden.Invoke(new Action(() => Visualisierung_PneumatikdruckVorhanden.BackColor = Color.Red));
                    }
                }
                else if (e.NotificationHandle == hGetDruckluftZugeschaltet)
                {
                    if (binReader.ReadBoolean() == true)
                    {
                        bGetDruckluftZugeschaltet = true;
                        Visualisierung_VentileEingeschaltet.Invoke(new Action(() => Visualisierung_VentileEingeschaltet.BackColor = Color.Green));
                    }
                    else
                    {
                        bGetDruckluftZugeschaltet = false;
                        bErrorVentileEingeschaltet = true;
                        bErrorSicherheitseinrichtung = true;        // Merker für allgemeinen Fehler Sicherheitseinrichtungen
                        Visualisierung_VentileEingeschaltet.Invoke(new Action(() => Visualisierung_VentileEingeschaltet.BackColor = Color.Red));
                    }
                }
                else if (e.NotificationHandle == hGetMesstasterVorhanden)
                {
                    if (binReader.ReadBoolean() == false)
                    {
                        bGetMesstasterVorhanden = true;
                        // Invoke: Threatübergreifender Zugriff
                        Visualisierung_MikroschalterMesstaster.Invoke(new Action(() => Visualisierung_MikroschalterMesstaster.BackColor = Color.Green));
                    }
                    else
                    {
                        bGetMesstasterVorhanden = false;
                        bErrorMesstasterVorhanden = true;
                        // Invoke: Threatübergreifender Zugriff
                        Visualisierung_MikroschalterMesstaster.Invoke(new Action(() => Visualisierung_MikroschalterMesstaster.BackColor = Color.Red));
                    }
                }
                else if (e.NotificationHandle == hGetAbdeckungGeschlossen)
                {
                    if (binReader.ReadBoolean() == true)
                    {
                        bGetAbdeckungGeschlossen = true;
                        // Invoke: Threatübergreifender Zugriff
                        Visualisierung_AbdeckungGeschlossen.Invoke(new Action(() => Visualisierung_AbdeckungGeschlossen.BackColor = Color.Green));

                        // Wenn der Hub eingestellt wurde und die Suche nach dem Startpunkt aussteht
                        if (bHubEingestellt == true)
                        {
                            Invoke(new Action(() => Button_StartpunktAnfahren.Enabled = true));
                        }
                    }
                    else
                    {
                        // Zum Test um mit offender Abdeckung zu prüfen
                        bGetAbdeckungGeschlossen = true;

                        //bGetAbdeckungGeschlossen = false;
                        //// Invoke: Threatübergreifender Zugriff
                        //Visualisierung_AbdeckungGeschlossen.Invoke(new Action(() => Visualisierung_AbdeckungGeschlossen.BackColor = Color.Red));

                        //// Wenn der Hub eingestellt wurde und die Suche nach dem Startpunkt aussteht
                        //if (bHubEingestellt == true && bStartpunktAngefahren == false)
                        //{
                        //    Invoke(new Action(() => Button_StartpunktAnfahren.Enabled = false));
                        //}
                    }
                }
                else if (e.NotificationHandle == hGetKnebelschalterEinrichten)
                {
                    if (binReader.ReadBoolean() == true)
                    {
                        bGetKnebelschalterEinrichten = true;
                        // Invoke: Threatübergreifender Zugriff
                        Visualisierung_Betriebsart.Invoke(new Action(() => Visualisierung_Betriebsart.Text = "Betriebsart Einrichten"));

                        // Prüfung gestartet?
                        if (bPruefungAktiv == true)
                        {
                            bErrorKnebelschalter = true;
                            bErrorSicherheitseinrichtung = true;        // Merker für allgemeinen Fehler Sicherheitseinrichtungen
                        }

                        // Fehler aufgetreten?
                        if (bErrorKnebelschalter == true)
                        {
                            Visualisierung_Betriebsart.Invoke(new Action(() => Visualisierung_Betriebsart.BackColor = Color.Red));
                        }
                        else
                        {
                            Visualisierung_Betriebsart.Invoke(new Action(() => Visualisierung_Betriebsart.BackColor = Color.Green));
                        }
                    }
                    else
                    {
                        bGetKnebelschalterEinrichten = false;
                    }
                }
                else if (e.NotificationHandle == hGetKnebelschalterBetrieb)
                {
                    if (binReader.ReadBoolean() == true)
                    {
                        bGetKnebelschalterBetrieb = true;
                        // Invoke: Threatübergreifender Zugriff
                        Visualisierung_Betriebsart.Invoke(new Action(() => Visualisierung_Betriebsart.Text = "Betriebsart Betrieb"));

                        // Fehler aufgetreten?
                        if (bErrorKnebelschalter == true)
                        {
                            Visualisierung_Betriebsart.Invoke(new Action(() => Visualisierung_Betriebsart.BackColor = Color.Red));
                        }
                        else
                        {
                            Visualisierung_Betriebsart.Invoke(new Action(() => Visualisierung_Betriebsart.BackColor = Color.Green));
                        }

                        // Widerstand überprüft und Hub eingestellt?
                        if (bWiderstandUeberprueft == true && bHubeinstellungAbgeschlossen == true)
                        {
                            Button_Start.Enabled = true;
                        }
                    }
                    else
                    {
                        bGetKnebelschalterBetrieb = false;
                    }
                }
                else if (e.NotificationHandle == hGetMesstasterGenullt)
                {
                    if (binReader.ReadBoolean() == true)
                    {
                        bGetMesstasterGenullt = true;
                    }
                    else
                    {
                        bGetMesstasterGenullt = false;
                    }
                }
                else if (e.NotificationHandle == hGetHeizpatronenZugeschaltet)
                {
                    if (binReader.ReadBoolean() == true)
                    {
                        bGetHeizpatronenZugeschaltet = true;
                        Visualisierung_HeizpatronenZugeschaltet.Invoke(new Action(() => Visualisierung_HeizpatronenZugeschaltet.BackColor = Color.Green));
                    }
                    else
                    {
                        bGetHeizpatronenZugeschaltet = false;
                        Visualisierung_HeizpatronenZugeschaltet.Invoke(new Action(() => Visualisierung_HeizpatronenZugeschaltet.BackColor = Color.Red));
                    }
                }
                else if (e.NotificationHandle == hGetHeizpatronenMasseGeschaltet)
                {
                    if (binReader.ReadBoolean() == true)
                    {
                        bGetHeizpatronenMasseGeschaltet = true;
                    }
                    else
                    {
                        bGetHeizpatronenMasseGeschaltet = false;
                    }
                }
                else if (e.NotificationHandle == hGetPositonsFlanke)
                {
                    if (binReader.ReadBoolean() == true)
                    {
                        bGetPositonsFlanke = true;

                        // Erzeugen der Flanke stoppen
                        bSetPositionsFlankeErzeugen = false;
                        tcClient.WriteAny(hSetPositionsFlankeErzeugen, bSetPositionsFlankeErzeugen);
                    }
                    else
                    {
                        bGetPositonsFlanke = false;
                    }
                }
                else if (e.NotificationHandle == hGetMotorposition)
                {
                    dGetMotorposition = binReader.ReadDouble();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //-----------------------------------------------------
            // Abfrage ob Abdeckung offen ist und Knebelschalter auf Stellung Betrieb steht
            //-----------------------------------------------------
            if (bGetAbdeckungGeschlossen == false && bGetKnebelschalterBetrieb == true)
            {
                bErrorAbdeckungGeschlossen = true;
                bErrorSicherheitseinrichtung = true;        // Merker für allgemeinen Fehler Sicherheitseinrichtungen
            }

            //-----------------------------------------------------
            // Reaktion auf Fehler
            //-----------------------------------------------------
            // Ist ein Fehler bei den Sicherheitseinrichtungen aufgetreten?
            if (bErrorSicherheitseinrichtung == true && bFehlerbehandlungSicherheitseinrichtungenAbgeschlossen == false)
            {
                //-----------------------------------------------------
                // Reset auf den Startzustand
                // Durchgeführte Schritte müssen wiederholt werden
                //-----------------------------------------------------
                // Hubeinstellung
                bHubeinstellungAbgeschlossen = false;                    // Merker der Hub ist nicht eingestellt
                bStartpunktAngefahren = false;                // Merker Startpunkt für die Prüfung wurde nicht gefunden
                bFensterHubEinstellenOffen = false;         // Merker Fenster Hub einstellen ist nicht offen
                Button_HubEinstellen.Invoke(new Action(() => Button_HubEinstellen.Enabled = false));    // Button sperren
                chart_Widerstand.Invoke(new Action(() => chart_Widerstand.Visible = true));            // Chart für den Widerstand einblenden
                Panel_HubEinstellen.Invoke(new Action(() => Panel_HubEinstellen.Visible = false));        // Panel für die Einstellung des Hubs ausblenden

                // Widerstand überprüfen
                bWiderstandUeberprueft = false;             // Merker Widerstand noch nicht geprüft
                Button_WiderstandPruefen.Invoke(new Action(() => Button_WiderstandPruefen.Enabled = false));   // Button für die Prüfung sperren
                Panel_WiderstandPruefen.Invoke(new Action(() => Panel_WiderstandPruefen.Visible = false));    // Panel für die Überprüfung ausblenden

                // Prüfungsstart/Durchführung
                Button_Start.Invoke(new Action(() => Button_Start.Enabled = false));
                Button_Zuruecksetzen.Invoke(new Action(() => Button_Zuruecksetzen.Enabled = false));
                Button_PruefungAbbrechen.Invoke(new Action(() => Button_PruefungAbbrechen.Enabled = false));
                bPruefungAktiv = false;

                bFehlerbehandlungSicherheitseinrichtungenAbgeschlossen = true;   // Merker, dass die Fehlerbehandlung durchgeführt wurde
            }

            bThreadAktiv = false;   // Merker Thread nicht aktiv
        }

        //-----------------------------------------------------
        // Servomotor einschalten
        //-----------------------------------------------------
        private bool ServoMotor_Einschalten()
        {
            try
            {
                // Drehrichtung Rechts freigeben oder sperren
                if (bSetDrehrichtungRechts == true)
                {
                    tcClient.WriteAny(hSetDrehrichtungPositiv, true);
                }
                else
                {
                    tcClient.WriteAny(hSetDrehrichtungPositiv, false);
                }

                // Drehrichtung Links freigeben oder sperren
                if (bSetDrehrichtungLinks == true)
                {
                    tcClient.WriteAny(hSetDrehrichtungNegativ, true);
                }
                else
                {
                    tcClient.WriteAny(hSetDrehrichtungNegativ, false);
                }

                // Servomotor einschalten
                tcClient.WriteAny(hSetMotorEinschalten, true);

                // Timeout Timer starten
                bTimeoutTwinCAT = false;    // Merker für Timeout zurücksetzen
                Timer_TimeoutTwinCAT.Interval = iTimeTimeoutTwinCAT;    // iTimeTimeoutTwinCAT
                Timer_TimeoutTwinCAT.Enabled = true;    // Timer starten

                // Warten, dass der Motor eingeschaltet wurde und kein Timeout aufgetreten ist
                while (bGetMotorEingeschaltet == false && bTimeoutTwinCAT == false)
                {

                }

                Timer_TimeoutTwinCAT.Enabled = false;   // Timer stoppen

                // Motor eingeschaltet und kein Timeout?
                if (bGetMotorEingeschaltet == true && bTimeoutTwinCAT == false)
                {
                    Visualisierung_MotorEingeschaltet.BackColor = Color.Green;
                }
                else
                {
                    bErrorTwinCAT = true;
                }
            }
            catch (Exception _Exception)
            {
                MessageBox.Show("Kommunikation TwinCAT - Motor einschalten:\n\n" + _Exception.Message);
                bErrorTwinCAT = true;
            }

            return bErrorTwinCAT;
        }

        //-----------------------------------------------------
        // Servomotor ausschalten
        //-----------------------------------------------------
        private bool ServoMotor_Ausschalten()
        {
            try
            {
                tcClient.WriteAny(hSetMotorEinschalten, false);           // Motor ausschalten

                // Timeout Timer starten
                bTimeoutTwinCAT = false;    // Merker für Timeout zurücksetzen
                Timer_TimeoutTwinCAT.Interval = iTimeTimeoutTwinCAT;
                Timer_TimeoutTwinCAT.Enabled = true;    // Timer starten

                // Warten, bis der Motor ausgeschaltet ist und kein Timeout aufgetreten ist
                while (bGetMotorEingeschaltet == true && bTimeoutTwinCAT == false)
                {

                }

                Timer_TimeoutTwinCAT.Enabled = false;   // Timer stoppen

                // Motor ausgeschaltet und kein Timeout?
                if (bGetMotorEingeschaltet == false && bTimeoutTwinCAT == false)
                {
                    Visualisierung_MotorEingeschaltet.BackColor = colorStandardGrau;
                }
                else
                {
                    bErrorTwinCAT = true;
                }

            }
            catch (Exception _Exception)
            {
                MessageBox.Show("Kommunikation TwinCAT - Motor ausschalten:\n\n" + _Exception.Message);
                bErrorTwinCAT = true;
            }

            return bErrorTwinCAT;
        }

        //-----------------------------------------------------
        // Servomotor stoppen
        //-----------------------------------------------------
        private bool ServoMotor_Stop()
        {
            // Fahrbefehl und Achse stoppen
            try
            {
                tcClient.WriteAny(hSetFahrbefehl, false);
                tcClient.WriteAny(hSetGeregelteFahrt, false);
                tcClient.WriteAny(hSetAchseStop, true);

                // Timeout Timer starten
                bTimeoutTwinCAT = false;    // Merker für Timeout zurücksetzen
                Timer_TimeoutTwinCAT.Interval = iTimeTimeoutTwinCAT;
                Timer_TimeoutTwinCAT.Enabled = true;    // Timer starten

                // Warten, bis der Motor gestoppt wurde und kein Timeout aufgetreten ist
                while (bGetAchseGestoppt == false && bTimeoutTwinCAT == false)
                {

                }

                Timer_TimeoutTwinCAT.Enabled = false;   // Timer stoppen
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bErrorTwinCAT = true;
            }

            // Motor gestoppt und kein Timeout?
            if (bGetAchseGestoppt == true && bTimeoutTwinCAT == false && bErrorTwinCAT == false)
            {
                try
                {
                    // Stop Befehl zurücknehmen
                    tcClient.WriteAny(hSetAchseStop, false);

                    // Timeout Timer starten
                    bTimeoutTwinCAT = false;    // Merker für Timeout zurücksetzen
                    Timer_TimeoutTwinCAT.Interval = iTimeTimeoutTwinCAT;
                    Timer_TimeoutTwinCAT.Enabled = true;    // Timer starten

                    // Warten, bis der Merker Achse gestoppt zurückgesetzt wurde und kein Timeout aufgetreten ist
                    while (bGetAchseGestoppt == true && bTimeoutTwinCAT == false)
                    {

                    }

                    Timer_TimeoutTwinCAT.Enabled = false;   // Timer stoppen

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    bErrorTwinCAT = true;
                }
            }

            // Merker Motor gestoppt false und kein Timeout?
            if (bGetAchseGestoppt == false && bTimeoutTwinCAT == false && bErrorTwinCAT == false)
            {
                bErrorTwinCAT = false;
            }
            else
            {
                bErrorTwinCAT = true;
            }

            return bErrorTwinCAT;
        }

        //-----------------------------------------------------
        // Servomotor Drehrichtung Rechts und Links freigeben oder sperren
        //-----------------------------------------------------
        private bool ServoMotor_Drehrichtung()
        {
            try
            {
                // Drehrichtung Links freigeben oder sperren
                if (bSetDrehrichtungLinks == true)
                {
                    tcClient.WriteAny(hSetDrehrichtungNegativ, true);
                }
                else
                {
                    tcClient.WriteAny(hSetDrehrichtungNegativ, false);
                }

                // Drehrichung Rechts freigeben oder sperren
                if (bSetDrehrichtungRechts == true)
                {
                    tcClient.WriteAny(hSetDrehrichtungPositiv, true);
                }
                else
                {
                    tcClient.WriteAny(hSetDrehrichtungPositiv, false);
                }
            }
            catch (Exception _Exception)
            {
                MessageBox.Show("Kommunikation TwinCAT - Motor ausschalten:\n\n" + _Exception.Message);
                bErrorTwinCAT = true;
            }

            return bErrorTwinCAT;
        }

        //-----------------------------------------------------
        // Aktuelle Position und Soll Position auf dSetIstPosition setzen
        //-----------------------------------------------------
        private bool ServoMotor_SetPosition()
        {
            try
            {
                // Position auf dSetIstPosition setzen
                tcClient.WriteAny(hSetClearPositionLag, bSetClearPositionLag);  // Bei Set Position Schleppabstand löschen, wenn true;
                tcClient.WriteAny(hSetPositionValue, dSetIstPosition);
                tcClient.WriteAny(hSetPosition, true);

                // Timeout Timer starten
                bTimeoutTwinCAT = false;    // Merker für Timeout zurücksetzen
                Timer_TimeoutTwinCAT.Interval = iTimeTimeoutTwinCAT;    // iTimeTimeoutTwinCAT
                Timer_TimeoutTwinCAT.Enabled = true;    // Timer starten

                // Warten, dass die Position gesetzt wurde und kein Timeout eingetreten ist
                while (bGetPositionGesetzt == false && bTimeoutTwinCAT == false)
                {

                }

                Timer_TimeoutTwinCAT.Enabled = false;   // Timer stoppen

                // Position auf 0 gesetzt und kein Timeout?
                if (bGetPositionGesetzt == true && bTimeoutTwinCAT == false)
                {
                    // Flag, für Position setzen, zurücksetzen
                    tcClient.WriteAny(hSetPosition, false);

                    // Timeout Timer starten
                    bTimeoutTwinCAT = false;    // Merker für Timeout zurücksetzen
                    Timer_TimeoutTwinCAT.Interval = iTimeTimeoutTwinCAT;    // iTimeTimeoutTwinCAT
                    Timer_TimeoutTwinCAT.Enabled = true;    // Timer starten

                    // Warten, dass das Flag zurück genommen wurde und kein Timeout eingetreten ist
                    while (bGetPositionGesetzt == true && bTimeoutTwinCAT == false)
                    {

                    }

                    Timer_TimeoutTwinCAT.Enabled = false;   // Timer stoppen
                }

                // Timeout eingetreten?
                if (bTimeoutTwinCAT == true)
                {
                    bErrorTwinCAT = true;
                }
            }
            catch (Exception _Exception)
            {
                MessageBox.Show("Kommunikation TwinCAT - Position 0 setzen:\n\n" + _Exception.Message);
                bErrorTwinCAT = true;
            }

            return bErrorTwinCAT;
        }

        //-----------------------------------------------------
        // Motor um dSetZielposition in Grad drehen
        //-----------------------------------------------------
        private bool ServoMotor_DrehungAufZielposition()
        {
            try
            {
                tcClient.WriteAny(hSetFirstZielposition, dSetFirstZielposition);           // Position in Grad
                tcClient.WriteAny(hSetFirstZielgeschwindigkeit, dSetFirstZielgeschwindigkeit);     // Einheit Grad/s
                tcClient.WriteAny(hSetAcceleration, dSetAcceleration);
                tcClient.WriteAny(hSetDeceleration, dSetDeceleration);

                // Beider Befehle Aktiv?
                if (bSetUngeregelteFahrbefehl == true && bSetGereglteFahrbefehl == true)
                {
                    MessageBox.Show("Kommunikation TwinCAT - Motor drehen:\n\n" + "Beide Fahrbefehle Aktiv");
                    bErrorTwinCAT = true;
                }
                else if (bSetUngeregelteFahrbefehl == true) // Einfache Fahrt starten?
                {
                    tcClient.WriteAny(hSetGeregelteFahrt, false);
                    tcClient.WriteAny(hSetFahrbefehl, true);            // Fahrbefehl starten
                }
                else if (bSetGereglteFahrbefehl == true)    // Geregelte Fahrt starten?
                {
                    MessageBox.Show("Kommunikation TwinCAT - Motor drehen:\n\n" + "Einfache Fahrbefehl aktiv");
                    bErrorTwinCAT = true;
                }
                else // Keine Fahrt starten?
                {
                    MessageBox.Show("Kommunikation TwinCAT - Motor drehen:\n\n" + "Kein Fahrbefehl aktiv");
                    bErrorTwinCAT = true;
                }

                // Fahrbehl konnte gestartet werden?
                if (bErrorTwinCAT == false)
                {
                    // Timeout Timer starten
                    bTimeoutTwinCAT = false;    // Merker für Timeout zurücksetzen
                    Timer_TimeoutTwinCAT.Interval = iTimeTimeoutTwinCAT;
                    Timer_TimeoutTwinCAT.Enabled = true;    // Timer starten

                    // Warten, dass die Drehung gestartet wurde und kein Timeout aufgetreten ist
                    while (bGetFahrbefehlGestartet == false && bTimeoutTwinCAT == false)
                    {

                    }

                    Timer_TimeoutTwinCAT.Enabled = false;   // Timer stoppen

                    //-----------------------------------------------------
                    // Warten, dass die Fahrzeit abgelaufen ist
                    //-----------------------------------------------------
                    // Timer für Fahrzeit starten, wenn kein Fehler vorliegt
                    if (bTimeoutTwinCAT == false)
                    {
                        // Timeout Timer starten
                        Timer_TimeoutTwinCAT.Interval = iTimeTimeoutServomotor;
                        Timer_TimeoutTwinCAT.Enabled = true;    // Timer starten
                    }

                    // Warten, dass die Drehung abgeschlossen wurde und kein Timeout aufgetreten ist
                    while (bGetFahrbefehlAbgeschlossen == false && bTimeoutTwinCAT == false)
                    {

                    }

                    Timer_TimeoutTwinCAT.Enabled = false;   // Timer stoppen

                    //-----------------------------------------------------
                    // Fahrbefehl zurücknehemn
                    //-----------------------------------------------------
                    // Drehung abgeschlossen und kein Timeout?
                    if (bGetFahrbefehlAbgeschlossen == true && bTimeoutTwinCAT == false)
                    {
                        // Fahrbefehle zurück nehmen
                        bSetUngeregelteFahrbefehl = false;
                        tcClient.WriteAny(hSetFahrbefehl, bSetUngeregelteFahrbefehl);
                        tcClient.WriteAny(hSetGeregelteFahrt, false);

                        // Timeout Timer starten
                        bTimeoutTwinCAT = false;    // Merker für Timeout zurücksetzen
                        Timer_TimeoutTwinCAT.Interval = iTimeTimeoutTwinCAT;
                        Timer_TimeoutTwinCAT.Enabled = true;    // Timer starten

                        // Warten, dass der Merker für Drehung abgeschlossen zurückgesetzt wurde und kein Timeout aufgetreten ist
                        while (bGetFahrbefehlAbgeschlossen == true && bTimeoutTwinCAT == false)
                        {

                        }

                        Timer_TimeoutTwinCAT.Enabled = false;   // Timer stoppen
                    }

                    // Timeout eingetreten
                    if (bTimeoutTwinCAT == true)
                    {
                        bErrorTwinCAT = true;
                    }
                }

                
            }
            catch (Exception _Exception)
            {
                MessageBox.Show("Kommunikation TwinCAT - Motor drehen:\n\n" + _Exception.Message);
                bErrorTwinCAT = true;
            }

            return bErrorTwinCAT;
        }

        //-----------------------------------------------------
        // Servomotor Fahrbefehl starten
        //-----------------------------------------------------
        private bool ServoMotor_StartGeregeltDrehen()
        {
            try
            {
                tcClient.WriteAny(hSetFirstZielposition, dSetFirstZielposition);            // Position in Grad
                tcClient.WriteAny(hSetSecondZielposition, dSetSecondZielposition);          // Position in Grad
                tcClient.WriteAny(hSetThirdZielposition, dSetThirdZielposition);            // Position in Grad
                tcClient.WriteAny(hSetFirstZielgeschwindigkeit, dSetFirstZielgeschwindigkeit);        // Einheit Grad/s
                tcClient.WriteAny(hSetSecondZielgeschwindigkeit, dSetSecondZielgeschwindigkeit);        // Einheit Grad/s
                tcClient.WriteAny(hSetThirdZielgeschwindigkeit, dSetThirdZielgeschwindigkeit);        // Einheit Grad/s
                tcClient.WriteAny(hSetAcceleration, dSetAcceleration);
                tcClient.WriteAny(hSetDeceleration, dSetDeceleration);

                // Beider Befehle Aktiv?
                if (bSetUngeregelteFahrbefehl == true && bSetGereglteFahrbefehl == true)
                {
                    MessageBox.Show("Kommunikation TwinCAT - Motor drehen:\n\n" + "Beide Fahrbefehle Aktiv");
                    bErrorTwinCAT = true;
                }
                else if (bSetGereglteFahrbefehl == true) // Geregelte Fahrt starten?
                {
                    tcClient.WriteAny(hSetFahrbefehl, false);
                    tcClient.WriteAny(hSetGeregelteFahrt, true);        // Fahrbefehl starten
                }
                else if (bSetUngeregelteFahrbefehl == true)    // Einfache Fahrt starten?
                {
                    MessageBox.Show("Kommunikation TwinCAT - Motor drehen:\n\n" + "Einfache Fahrbefehl aktiv");
                    bErrorTwinCAT = true;
                }
                else // Keine Fahrt starten?
                {
                    MessageBox.Show("Kommunikation TwinCAT - Motor drehen:\n\n" + "Kein Fahrbefehl aktiv");
                    bErrorTwinCAT = true;
                }
            }
            catch (Exception _Exception)
            {
                MessageBox.Show("Kommunikation TwinCAT - Motor drehen:\n\n" + _Exception.Message);
                bErrorTwinCAT = true;
            }

            return bErrorTwinCAT;
        }

        //-----------------------------------------------------
        // Servomotor Fahrbefehl starten
        //-----------------------------------------------------
        private bool ServoMotor_StartDrehen()
        {
            try
            {
                tcClient.WriteAny(hSetFirstZielposition, dSetFirstZielposition);           // Position in Grad
                tcClient.WriteAny(hSetFirstZielgeschwindigkeit, dSetFirstZielgeschwindigkeit);     // Einheit Grad/s
                tcClient.WriteAny(hSetAcceleration, dSetAcceleration);
                tcClient.WriteAny(hSetDeceleration, dSetDeceleration);

                tcClient.WriteAny(hSetFahrbefehl, true);            // Fahrbefehl starten
                tcClient.WriteAny(hSetGeregelteFahrt, false);
            }
            catch (Exception _Exception)
            {
                MessageBox.Show("Kommunikation TwinCAT - Motor drehen:\n\n" + _Exception.Message);
                bErrorTwinCAT = true;
            }

            return bErrorTwinCAT;
        }

        //-----------------------------------------------------
        // Fahrbefehl zurück nehmen
        //-----------------------------------------------------
        private bool ServoMotor_FahrbefehlLoeschen()
        {
            try
            {
                // Fahrbefehle zurück nehmen
                bSetUngeregelteFahrbefehl = false;
                bSetGereglteFahrbefehl = false;
                tcClient.WriteAny(hSetFahrbefehl, false);
                tcClient.WriteAny(hSetGeregelteFahrt, bSetGereglteFahrbefehl);

                // Timeout Timer starten
                bTimeoutTwinCAT = false;    // Merker für Timeout zurücksetzen
                Timer_TimeoutTwinCAT.Interval = iTimeTimeoutTwinCAT;
                Timer_TimeoutTwinCAT.Enabled = true;    // Timer starten

                // Warten, dass der Merker für Drehung abgeschlossen zurückgesetzt wurde und kein Timeout aufgetreten ist
                while (bGetFahrbefehlAbgeschlossen == true && bTimeoutTwinCAT == false)
                {

                }

                Timer_TimeoutTwinCAT.Enabled = false;   // Timer stoppen

                if (bTimeoutTwinCAT == true)    // Timeout abgelaufen?
                {
                    bErrorTwinCAT = true;
                }

            }
            catch (Exception _Exception)
            {
                MessageBox.Show("Kommunikation TwinCAT - Motor drehen:\n\n" + _Exception.Message);
                bErrorTwinCAT = true;
            }

            return bErrorTwinCAT;
        }

        //-----------------------------------------------------
        // Messtaster nullen
        //-----------------------------------------------------
        private bool TwinCAT_MesstasterNullen()
        {
            int iCase = 0;
            
            bool bAbgeschlossen = false;

            bTimeoutTwinCAT = false;    // Merker für Timeout zurücksetzen

            while (bErrorTwinCAT == false && bAbgeschlossen == false && bTimeoutTwinCAT == false)
            {
                switch (iCase)
                {
                    // Messtaster nullen
                    case 0:
                        try
                        {
                            tcClient.WriteAny(hSetMesstasterNullen, true);
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation TwinCAT - Messtaster nullen:\n\n" + _Exception.Message);
                            bErrorTwinCAT = true;
                        }

                        // Timeout Timer starten
                        bTimeoutTwinCAT = false;    // Merker für Timeout zurücksetzen
                        Timer_TimeoutTwinCAT.Interval = iTimeTimeoutTwinCAT;
                        Timer_TimeoutTwinCAT.Enabled = true;    // Timer starten

                        iCase = 1;

                        break;

                    // Warten, dass der Messtaster genullt wurde
                    case 1:
                        if (bGetMesstasterGenullt == true)
                        {
                            Timer_TimeoutTwinCAT.Enabled = false;   // Timer stoppen

                            iCase = 2;
                        }

                        break;

                    // Messtaster Nullen zurück ziehen
                    case 2:
                        try
                        {
                            tcClient.WriteAny(hSetMesstasterNullen, false);
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation TwinCAT - Messtaster nullen:\n\n" + _Exception.Message);
                            bErrorTwinCAT = true;
                        }

                        // Timeout Timer starten
                        bTimeoutTwinCAT = false;    // Merker für Timeout zurücksetzen
                        Timer_TimeoutTwinCAT.Interval = iTimeTimeoutTwinCAT;
                        Timer_TimeoutTwinCAT.Enabled = true;    // Timer starten

                        iCase = 3;

                        break;

                    // Warten, dass der Messtaster nicht mehr genullt ist
                    case 3:
                        if (bGetMesstasterGenullt == false)
                        {
                            Timer_TimeoutTwinCAT.Enabled = false;   // Timer stoppen

                            bAbgeschlossen = true;
                        }

                        break;
                }
            }

            if (bTimeoutTwinCAT == true)
            {
                bErrorTwinCAT = true;
            }
            

            return bErrorTwinCAT;
        }
        #endregion

        //-----------------------------------------------------
        // Methoden für die Digitalanzeige
        //-----------------------------------------------------
        #region Digitalanzeige Kommunikation

        //-----------------------------------------------------
        // Digitalanzeige verbinden
        // Globale Fehlervariable bErrorDigitalanzeige wird gesetzt
        //-----------------------------------------------------
        private void Digitalanzeige_Verbinden()
        {
            int iCaseDigitalanzeigeConnect = 0;
            int iPositionLuftlagertisch = -1;

            // Kein Fehler aufgetreten und Verbindung noch nicht hergestellt?
            while (bErrorDigitalanzeige == false && bDigitalanzeigeVerbunden == false)
            {
                switch (iCaseDigitalanzeigeConnect)
                {
                    //-----------------------------------------------------
                    // Com-Port öffnen
                    //-----------------------------------------------------
                    case 0:
                        Digitalanzeige_OpenCOMPort();

                        iCaseDigitalanzeigeConnect = 1;

                        break;

                    //-----------------------------------------------------
                    // Anzeigewert testweise auslesen
                    //-----------------------------------------------------
                    case 1:
                        iPositionLuftlagertisch = Digitalanzeige_AktuellenWertAuslesen();

                        iCaseDigitalanzeigeConnect = 2;

                        break;

                    case 2:
                        iPositionLuftlagertisch = Digitalanzeige_MinWertAuslesen();

                        iCaseDigitalanzeigeConnect = 3;

                        break;

                    //-----------------------------------------------------
                    // Anzeige reseten
                    //-----------------------------------------------------
                    case 3:
                        Digitalanzeige_ResetAnzeige();

                        iCaseDigitalanzeigeConnect = 4;

                        break;

                    //-----------------------------------------------------
                    // Min/Max Reset
                    //-----------------------------------------------------
                    case 4:
                        Digitalanzeige_MinMaxReset();

                        bDigitalanzeigeVerbunden = true;    // Connect abgeschlossen

                        break;
                }
            }

            // Digitalanzeige erfolgreich verbunden und kein Fehler aufgetreten?
            if (bDigitalanzeigeVerbunden == true && bErrorDigitalanzeige == false)
            {
                Button_DigitalanzeigeVerbinden.BackColor = Color.Green;
            }
            else
            {
                // Wenn Port offen ist, Port wieder schließen
                if (serialPort_Digitalanzeige.IsOpen == true)
                {
                    try
                    {
                        serialPort_Digitalanzeige.Close();  // Port schließen
                    }
                    catch (Exception _Exception)
                    {
                        MessageBox.Show("Kommunikation Digitalanzeige:\n\n" + _Exception.GetType().Name);
                        bErrorDigitalanzeige = true;
                    }
                }

                bDigitalanzeigeVerbunden = false;
                Button_DigitalanzeigeVerbinden.BackColor = Color.Red;
                Button_DigitalanzeigeVerbinden.Enabled = true;
            }
        }

        private bool Digitalanzeige_OpenCOMPort()
        {
            Button_DigitalanzeigeVerbinden.Enabled = false;

            //-----------------------------------------------------
            // Com-Port öffnen
            //-----------------------------------------------------
            try
            {
                // Port einstellen
                serialPort_Digitalanzeige.PortName = strCOMPortDigitalanzeige;
                serialPort_Digitalanzeige.BaudRate = iBaudrateDigitalanzeige;

                serialPort_Digitalanzeige.Open(); // Port öffnen
            }
            catch (Exception _Exception)
            {
                MessageBox.Show("Kommunikation Digitalanzeige:\n\n" + _Exception.GetType().Name);
                bErrorDigitalanzeige = true;
            }

            return bErrorDigitalanzeige;
        }

        //-----------------------------------------------------
        // Anzeige der Digitalanzeige reseten
        // Globale Fehlervariable bErrorDigitalanzeige wird gesetzt
        //-----------------------------------------------------
        private int Digitalanzeige_ResetAnzeige()
        {
            string strBefehl;
            string strSetValue;
            string strBCC;
            string strGesamteBefehl;
            string strAntwort = NAK;

            int iAntwort = NAK_Dig;

            //-----------------------------------------------------
            // Digitalanzeige zurücksetzen
            //-----------------------------------------------------
            try
            {
                // Befehl zum Rücksetzen der Anzeige zusammensetzen
                strBefehl = "54";
                strSetValue = "1";
                strBCC = "3";
                strGesamteBefehl = EOT + strDigitalanzeigeAdresse + STX + strBefehl + strSetValue + ETX + strBCC;

                // Befehl senden
                serialPort_Digitalanzeige.ReadExisting();   // Gesamten Buffer auslesen
                serialPort_Digitalanzeige.DiscardInBuffer();
                serialPort_Digitalanzeige.DiscardOutBuffer();
                serialPort_Digitalanzeige.Write(strGesamteBefehl);

                Timer_DigitalanzeigeWartezeitVorAuslesen.Enabled = true;
                while (bTimerWartezeitVorAuslesenAbgelaufen == false)
                {

                }
                bTimerWartezeitVorAuslesenAbgelaufen = false;

                strAntwort = serialPort_Digitalanzeige.ReadExisting();

                // Kein ACK empfangen?
                if (strAntwort != ACK)
                {
                    // Übertragung einmalig erneut starten, Befehl senden
                    serialPort_Digitalanzeige.ReadExisting();   // Gesamten Buffer auslesen
                    serialPort_Digitalanzeige.DiscardInBuffer();
                    serialPort_Digitalanzeige.DiscardOutBuffer();
                    serialPort_Digitalanzeige.Write(strGesamteBefehl);

                    iAntwort = serialPort_Digitalanzeige.ReadByte();      // ACK oder NAK auslesen

                    // Erneut kein ACK erhalten
                    if (iAntwort != ACK_Dig)
                    {
                        MessageBox.Show("Kommunikation Digitalanzeige:\n\n" + "Kein ACK erhalten.");

                        iAntwort = NAK_Dig;             // iAntwort explizit auf NAK setzen
                        bErrorDigitalanzeige = true;    // Merker Fehler aufgetreten
                    }
                }

                // ACK empfangen
                if (strAntwort == ACK)
                {
                    bKommunikationsfehlerAufgetreten = false;   // Merker rücksetzen
                }
            }
            catch (Exception _Exception)
            {
                MessageBox.Show("Kommunikation Digitalanzeige:\n\n" + _Exception.GetType().Name);
                bErrorDigitalanzeige = true;
                iAntwort = NAK_Dig;
            }

            //-----------------------------------------------------
            // Befehl zum Reset der Digitalanzeige zurücknehmen
            //-----------------------------------------------------
            if (strAntwort == ACK && bErrorDigitalanzeige == false)   // Kein Fehler aufgetreten?
            {
                try
                {
                    // Befehl Löoschen des Merkers zum Rücksetzen der Anzeige zusammensetzen
                    strBefehl = "54";
                    strSetValue = "0";
                    strBCC = "2";
                    strGesamteBefehl = EOT + strDigitalanzeigeAdresse + STX + strBefehl + strSetValue + ETX + strBCC;

                    // Befehl senden
                    serialPort_Digitalanzeige.ReadExisting();   // Gesamten Buffer auslesen
                    serialPort_Digitalanzeige.DiscardInBuffer();
                    serialPort_Digitalanzeige.DiscardOutBuffer();
                    serialPort_Digitalanzeige.Write(strGesamteBefehl);

                    Timer_DigitalanzeigeWartezeitVorAuslesen.Enabled = true;
                    while (bTimerWartezeitVorAuslesenAbgelaufen == false)
                    {

                    }
                    bTimerWartezeitVorAuslesenAbgelaufen = false;

                    strAntwort = serialPort_Digitalanzeige.ReadExisting();

                    // Kein ACK empfangen?
                    if (strAntwort != ACK)
                    {
                        // Übertragung einmalig erneut starten, Befehl senden
                        serialPort_Digitalanzeige.DiscardInBuffer();
                        serialPort_Digitalanzeige.DiscardOutBuffer();
                        serialPort_Digitalanzeige.Write(strGesamteBefehl);

                        iAntwort = serialPort_Digitalanzeige.ReadByte();      // ACK oder NAK auslesen

                        // Erneut kein ACK erhalten
                        if (iAntwort != ACK_Dig)
                        {
                            MessageBox.Show("Kommunikation Digitalanzeige:\n\n" + "Kein ACK erhalten.");

                            iAntwort = NAK_Dig;             // iAntwort explizit auf NAK setzen
                            bErrorDigitalanzeige = true;    // Merker Fehler aufgetreten
                        }
                    }
                    
                    // ACK empfangen
                    if (strAntwort == ACK)
                    {
                        bKommunikationsfehlerAufgetreten = false;   // Merker rücksetzen
                    }
                }
                catch (Exception _Exception)
                {
                    MessageBox.Show("Kommunikation Digitalanzeige:\n\n" + _Exception.GetType().Name);
                    bErrorDigitalanzeige = true;
                    iAntwort = NAK_Dig;
                }
            }

            return iAntwort;
        }

        //-----------------------------------------------------
        // Min und Max Werte der Digitalanzeige reseten
        // Globale Fehlervariable bErrorDigitalanzeige wird gesetzt
        //-----------------------------------------------------
        private int Digitalanzeige_MinMaxReset()
        {
            string strBefehl;
            string strSetValue;
            string strBCC;
            string strGesamteBefehl;
            string strAntwort = NAK;

            int iAntwort = NAK_Dig;

            try
            {
                strBefehl = "58";   // Code zum Löschen von Min und Max Wert
                strSetValue = "1";
                strBCC = "?";    // BCC = Block Check Character
                strGesamteBefehl = EOT + strDigitalanzeigeAdresse + STX + strBefehl + strSetValue + ETX + strBCC;
                serialPort_Digitalanzeige.ReadExisting();   // Gesamten Buffer auslesen
                serialPort_Digitalanzeige.DiscardInBuffer();
                serialPort_Digitalanzeige.DiscardOutBuffer();
                serialPort_Digitalanzeige.Write(strGesamteBefehl);    // Befehl senden

                Timer_DigitalanzeigeWartezeitVorAuslesen.Enabled = true;
                while (bTimerWartezeitVorAuslesenAbgelaufen == false)
                {

                }
                bTimerWartezeitVorAuslesenAbgelaufen = false;

                strAntwort = serialPort_Digitalanzeige.ReadExisting();

                // Kein ACK empfangen?
                if (strAntwort != ACK)
                {
                    // Übertragung einmalig erneut starten, Befehl senden
                    serialPort_Digitalanzeige.ReadExisting();   // Gesamten Buffer auslesen
                    serialPort_Digitalanzeige.DiscardInBuffer();
                    serialPort_Digitalanzeige.DiscardOutBuffer();
                    serialPort_Digitalanzeige.Write(strGesamteBefehl);

                    iAntwort = serialPort_Digitalanzeige.ReadByte();      // ACK oder NAK auslesen

                    // Erneut kein ACK erhalten
                    if (iAntwort != ACK_Dig)
                    {
                        MessageBox.Show("Kommunikation Digitalanzeige:\n\n" + "Kein ACK erhalten.");

                        iAntwort = NAK_Dig;             // iAntwort explizit auf NAK setzen
                        bErrorDigitalanzeige = true;    // Merker Fehler aufgetreten
                    }
                }

                // ACK empfangen
                if (strAntwort == ACK)
                {
                    bKommunikationsfehlerAufgetreten = false;   // Merker rücksetzen
                }

            }
            catch (Exception _Exception)
            {
                MessageBox.Show("Kommunikation Digitalanzeige:\n\n" + _Exception.GetType().Name);
                bErrorDigitalanzeige = true;
                iAntwort = NAK_Dig;
            }

            return iAntwort;
        }

        //-----------------------------------------------------
        // Min Wert der Digitalanzeige auslesen - in µm
        // Rückgabewert ist Min-Positon in µm
        // Globale Fehlervariable bErrorDigitalanzeige wird gesetzt
        //-----------------------------------------------------
        private int Digitalanzeige_MinWertAuslesen()
        {
            string strBefehl;
            string strAntwort;
            string strOverhead;
            string strAntwortFormatiert;
            string strGesamteBefehl;

            int iAntwort = 0;

            try
            {
                strBefehl = ":6";   // Befehl um Min Wert auszulesen
                strGesamteBefehl = EOT + strDigitalanzeigeAdresse + strBefehl + ENQ;
                serialPort_Digitalanzeige.ReadExisting();   // Gesamten Buffer auslesen
                serialPort_Digitalanzeige.DiscardInBuffer();
                serialPort_Digitalanzeige.DiscardOutBuffer();
                serialPort_Digitalanzeige.Write(strGesamteBefehl);

                strAntwort = serialPort_Digitalanzeige.ReadTo(ETX);   // Bis ETX auslesen

                // Steuerzeichen aus String entfernen
                strAntwortFormatiert = strAntwort.Replace(STX, "");
                strAntwortFormatiert = strAntwortFormatiert.Replace(strBefehl, "");

                iAntwort = Convert.ToInt32(strAntwortFormatiert); // Messwert in Integer speichern (µm)

                strOverhead = serialPort_Digitalanzeige.ReadExisting(); // BCC + ggf. Overhead auslesen

                bKommunikationsfehlerAufgetreten = false;   // Merker rücksetzen
            }
            catch (Exception _Exception)
            {
                if (bKommunikationsfehlerAufgetreten == false)  // Noch kein Fehler aufgetreten?
                {
                    bKommunikationsfehlerAufgetreten = true;    // Merker Fehler nun aufgetreten
                    iAntwort = Digitalanzeige_MinWertAuslesen();
                }
                else
                {
                    MessageBox.Show("Kommunikation Digitalanzeige:\n\n" + _Exception.GetType().Name);
                    bErrorDigitalanzeige = true;    // Merker Fehler aufgetreten
                }
            }

            return iAntwort;
        }

        //-----------------------------------------------------
        // Max Wert der Digitalanzeige auslesen - in µm
        // Rückgabewert ist Max-Positon in µm
        // Globale Fehlervariable bErrorDigitalanzeige wird gesetzt
        //-----------------------------------------------------
        private int Digitalanzeige_MaxWertAuslesen()
        {
            string strBefehl;
            string strAntwort;
            string strOverhead;
            string strAntwortFormatiert;
            string strGesamteBefehl;

            int iAntwort = 0;

            try
            {
                strBefehl = ":7";   // Befehl um Max Wert auszulesen
                strGesamteBefehl = EOT + strDigitalanzeigeAdresse + strBefehl + ENQ;
                serialPort_Digitalanzeige.ReadExisting();   // Gesamten Buffer auslesen
                serialPort_Digitalanzeige.DiscardInBuffer();
                serialPort_Digitalanzeige.DiscardOutBuffer();
                serialPort_Digitalanzeige.Write(strGesamteBefehl);

                strAntwort = serialPort_Digitalanzeige.ReadTo(ETX);   // Bis ETX auslesen

                // Steuerzeichen aus String entfernen
                strAntwortFormatiert = strAntwort.Replace(STX, "");
                strAntwortFormatiert = strAntwortFormatiert.Replace(strBefehl, "");

                iAntwort = Convert.ToInt16(strAntwortFormatiert); // Messwert in Integer speichern (µm)

                strOverhead = serialPort_Digitalanzeige.ReadExisting(); // BCC + ggf. Overhead auslesen

                bKommunikationsfehlerAufgetreten = false;   // Merker rücksetzen
            }
            catch (Exception _Exception)
            {
                if (bKommunikationsfehlerAufgetreten == false)  // Noch kein Fehler aufgetreten?
                {
                    bKommunikationsfehlerAufgetreten = true;    // Merker Fehler nun aufgetreten
                    iAntwort = Digitalanzeige_MaxWertAuslesen();
                }
                else
                {
                    MessageBox.Show("Kommunikation Digitalanzeige:\n\n" + _Exception.GetType().Name);
                    bErrorDigitalanzeige = true;    // Merker Fehler aufgetreten
                }
            }

            return iAntwort;
        }

        //-----------------------------------------------------
        // Aktuellen Wert der Digitalanzeige auslesen
        // Rückgabewert ist Aktuelle-Positon in µm
        // Globale Fehlervariable bErrorDigitalanzeige wird gesetzt
        //-----------------------------------------------------
        private int Digitalanzeige_AktuellenWertAuslesen()
        {
            string strBefehl;
            string strAntwort;
            string strOverhead;
            string strAntwortFormatiert;
            string strGesamteBefehl;

            int iAntwort = 0;

            try
            {
                strBefehl = ":3";   // Befehl um Aktuellen Wert auszulesen
                strGesamteBefehl = EOT + strDigitalanzeigeAdresse + strBefehl + ENQ;
                serialPort_Digitalanzeige.ReadExisting();   // Gesamten Buffer auslesen
                serialPort_Digitalanzeige.DiscardInBuffer();
                serialPort_Digitalanzeige.DiscardOutBuffer();
                serialPort_Digitalanzeige.Write(strGesamteBefehl);

                strAntwort = serialPort_Digitalanzeige.ReadTo(ETX);   // Bis ETX auslesen

                // Steuerzeichen aus String entfernen
                strAntwortFormatiert = strAntwort.Replace(STX, "");
                strAntwortFormatiert = strAntwortFormatiert.Replace(strBefehl, "");

                iAntwort = Convert.ToInt16(strAntwortFormatiert); // Messwert in Integer speichern (µm)

                strOverhead = serialPort_Digitalanzeige.ReadExisting(); // BCC + ggf. Overhead auslesen

                bKommunikationsfehlerAufgetreten = false;   // Merker rücksetzen
            }
            catch (Exception _Exception)
            {
                if (bKommunikationsfehlerAufgetreten == false)  // Noch kein Fehler aufgetreten?
                {
                    bKommunikationsfehlerAufgetreten = true;    // Merker Fehler nun aufgetreten
                    iAntwort = Digitalanzeige_AktuellenWertAuslesen();
                }
                else
                {
                    MessageBox.Show("Kommunikation Digitalanzeige:\n\n" + _Exception.GetType().Name);
                    bErrorDigitalanzeige = true;    // Merker Fehler aufgetreten
                }
                
            }

            return iAntwort;
        }
        #endregion

        //-----------------------------------------------------
        // Methoden für das Milliohmmeter
        //-----------------------------------------------------
        #region Milliohmmeter Kommunikation

        private void Milliohmmeter_Verbinden()
        {
            int iCaseMilliohmmeterConnect = 0;

            // Solange die Verbindung nicht hergestellt wurde und kein Fehler aufgetreten ist
            while (bErrorMilliohmmeter == false && bMilliohmmeterVerbunden == false)
            {
                switch (iCaseMilliohmmeterConnect)
                {
                    //-----------------------------------------------------
                    // Com-Port öffnen
                    //-----------------------------------------------------
                    case 0:
                        try
                        {
                            // Port einstellen
                            serialPort_Milliohmmeter.PortName = strCOMPortMilliohmmeter;
                            serialPort_Milliohmmeter.BaudRate = iBaudrateMilliohmmeter;

                            serialPort_Milliohmmeter.Open(); // Port öffnen

                            iCaseMilliohmmeterConnect = 1;
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                            bErrorMilliohmmeter = true;
                        }

                        break;

                    //-----------------------------------------------------
                    // Verbindung testen
                    //-----------------------------------------------------
                    case 1:
                        Milliohmmeter_VerbindungTesten();

                        iCaseMilliohmmeterConnect = 2;

                        break;

                    //-----------------------------------------------------
                    // Milliohmmeter initialisieren
                    //-----------------------------------------------------
                    case 2:
                        Milliohmmeter_Init();

                        if (bErrorMilliohmmeter == false)   // Kein Fehler aufgetreten?
                        {
                            bMilliohmmeterVerbunden = true;
                        }

                        break;
                }
            }

            // Steuerelemente visualisieren
            // Milliohmmeter Verbunden und kein Fehler aufgetreten?
            if (bMilliohmmeterVerbunden == true & bErrorMilliohmmeter == false)
            {
                Button_MilliohmmeterVerbinden.BackColor = Color.Green;
            }
            else
            {
                // Wenn Port offen ist, Port wieder schließen
                if (serialPort_Milliohmmeter.IsOpen == true)
                {
                    try
                    {
                        serialPort_Milliohmmeter.Close();   // Port schließen
                    }
                    catch (Exception _Exception)
                    {
                        MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                        bErrorMilliohmmeter = true;
                    }
                }

                bMilliohmmeterVerbunden = false;                        // Merker Milliohmmeter nicht verbunden
                Button_MilliohmmeterVerbinden.Enabled = true;
                Button_MilliohmmeterVerbinden.BackColor = Color.Red;
            }
        }


        //-----------------------------------------------------
        // Verbindung zum Milliohmmeter testen
        //-----------------------------------------------------
        private bool Milliohmmeter_VerbindungTesten()
        {
            string strBefehl;
            string strGesamteBefehl;
            string strAntwort;

            int iAntwort;

            int iCase = 0;

            bool bFehlerDaten = false;
            bool bTestAbgeschlossen = false;

            while (bTestAbgeschlossen == false && bErrorMilliohmmeter == false)
            {
                // SCPI Version abfragen
                switch (iCase)
                {
                    // Befehl senden
                    case 0:
                        try
                        {
                            strBefehl = "SYST:VERS?";   // Befehl zum Abfragen der Version
                            strGesamteBefehl = STX + strBefehl + LF + ETX;
                            serialPort_Milliohmmeter.Write(strGesamteBefehl);

                            iCase = 1;
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                            bErrorMilliohmmeter = true;
                        }

                        break;

                    // Antwort auslesen
                    case 1:
                        try
                        {
                            iAntwort = serialPort_Milliohmmeter.ReadByte();  // Antwort auslesen

                            if ((Convert.ToChar(iAntwort)).ToString() == ACK)  // Wurde ein ACK empfangen
                            {
                                iCase = 2;
                            }
                            else if ((Convert.ToChar(iAntwort)).ToString() == NAK)  // Wurde ein NAK empfangen
                            {
                                MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + "NAK erhalten");
                                bErrorMilliohmmeter = true;
                            }
                            else
                            {
                                iCase = 1;
                            }
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                            bErrorMilliohmmeter = true;
                        }

                        break;

                    // Daten anfordern
                    case 2:
                        try
                        {
                            strGesamteBefehl = EOT;
                            serialPort_Milliohmmeter.Write(strGesamteBefehl);   // Befehl senden um Daten anzufordern

                            iCase = 3;
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                            bErrorMilliohmmeter = true;
                        }
                        
                        break;

                    // Daten auslesen
                    case 3:
                        try
                        {
                            strAntwort = serialPort_Milliohmmeter.ReadTo(ETX);    // Bis ETX auslesen, da ETX das letzte Zeichen ist

                            // Steuerzeichen aus String entfernen
                            strAntwort = strAntwort.Replace(STX, "");    // STX entfernen
                            strAntwort = strAntwort.Replace(CR, "");     // CR entfernen
                            strAntwort = strAntwort.Replace(LF, "");     // LF entfernen
                            strAntwort = strAntwort.Replace(ETX, "");    // ETX entfernen

                            iCase = 4;
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                            bErrorMilliohmmeter = true;
                        }
                        
                        break;

                    // Empfang mit ACK bestätigen
                    case 4:
                        try
                        {
                            strGesamteBefehl = ACK;
                            serialPort_Milliohmmeter.Write(strGesamteBefehl);

                            iCase = 5;
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                            bErrorMilliohmmeter = true;
                        }
                        
                        break;

                    // Auslesen ob weitere Daten vorhanden sind
                    case 5:
                        try
                        {
                            iAntwort = serialPort_Milliohmmeter.ReadByte();

                            // Keine weiteren Daten vorhanden
                            if ((Convert.ToChar(iAntwort)).ToString() == EOT)
                            {
                                bTestAbgeschlossen = true;
                            }
                            else
                            {
                                bFehlerDaten = true;    // Fehler da zu viele Daten vorhanden sind
                                iCase = 3;  // weitere Daten auslesen
                            }


                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                            bErrorMilliohmmeter = true;
                        }

                        break;
                }

            }

            // Wenn der Fehler aufgetreten ist
            if (bFehlerDaten == true)
            {
                MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + "Zu viele Daten erhalten.");
                bErrorMilliohmmeter = true;
            }

            return bErrorMilliohmmeter;
        }

        //-----------------------------------------------------
        // Milliohmmeter initialisieren
        //-----------------------------------------------------
        private bool Milliohmmeter_Init()
        {
            string strBefehl;
            string strGesamteBefehl;

            int iAntwort;

            //-----------------------------------------------------
            // 20 mV Messmethode einstellen
            //-----------------------------------------------------
            try
            {
                // Befehl senden
                strBefehl = "SOUR:VOLT:LIM:STAT 1";
                strGesamteBefehl = STX + strBefehl + LF + ETX;
                serialPort_Milliohmmeter.Write(strGesamteBefehl);

                // Antwort auslesen
                iAntwort = serialPort_Milliohmmeter.ReadByte();

                // Wurde ein NAK gesetzt?
                if ((Convert.ToChar(iAntwort)).ToString() == NAK)
                {
                    bErrorMilliohmmeter = true;  // Merker für Fehler
                    MessageBox.Show("Kommunikation Milliohmmeter:\n\nNAK erhalten.");
                }
            }
            catch (Exception _Exception)
            {
                MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                bErrorMilliohmmeter = true;
            }

            //-----------------------------------------------------
            // Einzelmessung einstellen
            //-----------------------------------------------------
            if (bErrorMilliohmmeter == false)
            {
                try
                {
                    // Befehl senden
                    strBefehl = "INIT:CONT 0";
                    strGesamteBefehl = STX + strBefehl + LF + ETX;
                    serialPort_Milliohmmeter.Write(strGesamteBefehl);

                    // Antwort auslesen
                    iAntwort = serialPort_Milliohmmeter.ReadByte();

                    // Wurde ein NAK gesetzt?
                    if ((Convert.ToChar(iAntwort)).ToString() == NAK)
                    {
                        bErrorMilliohmmeter = true;  // Merker für Fehler
                        MessageBox.Show("Kommunikation Milliohmmeter:\n\nNAK erhalten.");
                    }
                }
                catch (Exception _Exception)
                {
                    MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                    bErrorMilliohmmeter = true;
                }
            }

            //-----------------------------------------------------
            // Art des Prüflings auf R einstellen
            //-----------------------------------------------------
            if (bErrorMilliohmmeter == false)
            {
                try
                {
                    // Befehl senden
                    strBefehl = "SENS:FRES:LOAD REAL";
                    strGesamteBefehl = STX + strBefehl + LF + ETX;
                    serialPort_Milliohmmeter.Write(strGesamteBefehl);

                    // Antwort auslesen
                    iAntwort = serialPort_Milliohmmeter.ReadByte();

                    // Wurde ein NAK gesetzt?
                    if ((Convert.ToChar(iAntwort)).ToString() == NAK)
                    {
                        bErrorMilliohmmeter = true;  // Merker für Fehler
                        MessageBox.Show("Kommunikation Milliohmmeter:\n\nNAK erhalten.");
                    }
                }
                catch (Exception _Exception)
                {
                    MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                    bErrorMilliohmmeter = true;
                }
            }

            //-----------------------------------------------------
            // Hohe Auflösung einstellen
            //-----------------------------------------------------
            if (bErrorMilliohmmeter == false)
            {
                try
                {
                    // Befehl senden
                    strBefehl = "SENS:FRES:RES 0.00005";
                    strGesamteBefehl = STX + strBefehl + LF + ETX;
                    serialPort_Milliohmmeter.Write(strGesamteBefehl);

                    // Antwort auslesen
                    iAntwort = serialPort_Milliohmmeter.ReadByte();

                    // Wurde ein NAK gesetzt?
                    if ((Convert.ToChar(iAntwort)).ToString() == NAK)
                    {
                        bErrorMilliohmmeter = true;  // Merker für Fehler
                        MessageBox.Show("Kommunikation Milliohmmeter:\n\nNAK erhalten.");
                    }
                }
                catch (Exception _Exception)
                {
                    MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                    bErrorMilliohmmeter = true;
                }
            }

            //-----------------------------------------------------
            // Konvertierung auf Medium einstellen
            //-----------------------------------------------------
            if (bErrorMilliohmmeter == false)
            {
                try
                {
                    // Befehl senden
                    strBefehl = "SENS:FRES:NPLC MIN";
                    strGesamteBefehl = STX + strBefehl + LF + ETX;
                    serialPort_Milliohmmeter.Write(strGesamteBefehl);

                    // Antwort auslesen
                    iAntwort = serialPort_Milliohmmeter.ReadByte();

                    // Wurde ein NAK gesetzt?
                    if ((Convert.ToChar(iAntwort)).ToString() == NAK)
                    {
                        bErrorMilliohmmeter = true;  // Merker für Fehler
                        MessageBox.Show("Kommunikation Milliohmmeter:\n\nNAK erhalten.");
                    }
                }
                catch (Exception _Exception)
                {
                    MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                    bErrorMilliohmmeter = true;
                }
            }

            return bErrorMilliohmmeter;
        }

        //-----------------------------------------------------
        // Widertstandsmessung starten
        //-----------------------------------------------------
        private bool Milliohmmeter_MessungStarten()
        {
            string strBefehl;
            string strGesamteBefehl;
            int iAntwort;

            try
            {
                strBefehl = "IN";
                strGesamteBefehl = STX + strBefehl + LF + ETX;
                serialPort_Milliohmmeter.Write(strGesamteBefehl);

                // Antwort auslesen
                iAntwort = serialPort_Milliohmmeter.ReadByte();

                // Messung erfolgreich gestartet?
                if ((Convert.ToChar(iAntwort)).ToString() != ACK)  // Wurde kein ACK empfangen
                {
                    MessageBox.Show("Kommunikation Milliohmmeter:\n\nNAK erhalten.");
                    bErrorMilliohmmeter = true;  // Merker Fehler in der Kommunikation
                }
            }
            catch (Exception _Exception)
            {
                MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                bErrorMilliohmmeter = true;
            }

            return bErrorMilliohmmeter;
        }

        //-----------------------------------------------------
        // Abfragen ob die Messung fertig ist
        // Rückgabewert ist, ob die Messung abgeschlossen ist
        //-----------------------------------------------------
        private bool Milliohmmeter_IstMessungAbgeschlossen()
        {
            string strBefehl;
            string strGesamteBefehl;
            string strAntwort;

            int iAntwort;

            bool bMessungAbschlossen = false;

            try
            {
                // Befehl senden
                strBefehl = "STAT:OPER:COND?";
                strGesamteBefehl = STX + strBefehl + LF + ETX;
                serialPort_Milliohmmeter.Write(strGesamteBefehl);

                // Antwort auslesen
                iAntwort = serialPort_Milliohmmeter.ReadByte();

                // Befehl erfolgreich übertragen?
                if ((Convert.ToChar(iAntwort)).ToString() == ACK)  // Wurde ein ACK empfangen
                {
                    // Daten anfordern
                    strGesamteBefehl = EOT;
                    serialPort_Milliohmmeter.Write(strGesamteBefehl);   // Befehl senden um Daten anzufordern

                    // Daten auslesen
                    strAntwort = serialPort_Milliohmmeter.ReadTo(ETX);    // Bis ETX auslesen, da ETX das letzte Zeichen ist

                    // Steuerzeichen aus String entfernen
                    strAntwort = strAntwort.Replace(STX, "");    // STX entfernen
                    strAntwort = strAntwort.Replace(CR, "");     // CR entfernen
                    strAntwort = strAntwort.Replace(LF, "");     // LF entfernen
                    strAntwort = strAntwort.Replace(ETX, "");    // ETX entfernen

                    // Empfang mit ACK bestätigen
                    strGesamteBefehl = ACK;
                    serialPort_Milliohmmeter.Write(strGesamteBefehl);

                    // Auslesen ob weitere Daten vorhanden sind
                    iAntwort = serialPort_Milliohmmeter.ReadByte();

                    // Weiteren Daten vorhanden?    -> Fehlerhaft da nicht zulässig
                    if ((Convert.ToChar(iAntwort)).ToString() != EOT)
                    {
                        // Solange kein EOT empfangen wurde
                        while ((Convert.ToChar(iAntwort)).ToString() == EOT)
                        {
                            // Daten auslesen
                            strAntwort = serialPort_Milliohmmeter.ReadTo(ETX);    // Bis ETX auslesen, da ETX das letzte Zeichen ist

                            // Empfang mit ACK bestätigen
                            strGesamteBefehl = ACK;
                            serialPort_Milliohmmeter.Write(strGesamteBefehl);

                            // Auslesen ob weitere Daten vorhanden sind
                            iAntwort = serialPort_Milliohmmeter.ReadByte();
                        }

                        MessageBox.Show("Kommunikation Milliohmmeter:\n\nZu viele Daten erhalten.");    // Aufgabe das zu viele Daten empfangen wurden
                        bErrorMilliohmmeter = true;  // Merker Fehler in der Kommunikation
                    }
                    else    // EOT empfangen
                    {
                        // Messwert noch nicht vorhanden?
                        if (strAntwort != "256")
                        {
                            bMessungAbschlossen = false;
                        }
                        else // Messwert vorhanden
                        {
                            bMessungAbschlossen = true;
                        }
                    }
                }
                else    // NAK erhalten
                {
                    MessageBox.Show("Kommunikation Milliohmmeter:\n\nNAK erhalten.");
                    bErrorMilliohmmeter = true;  // Merker Fehler in der Kommunikation
                }
            }
            catch (Exception _Exception)
            {
                MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                bErrorMilliohmmeter = true;
            }

            return bMessungAbschlossen;
        }

        //-----------------------------------------------------
        // Messwert auslesen
        // Rückgabewert ist Widerstand in mOhm als String
        //-----------------------------------------------------
        private string Milliohmmeter_MesswertAuslesen()
        {
            string strBefehl;
            string strGesamteBefehl = "";
            string strAntwort = "";

            int iAntwort;

            try
            {
                // Befehl senden
                strBefehl = "FE?";       // Befehl zum Anfordern des Messwertes
                strGesamteBefehl = STX + strBefehl + LF + ETX;
                serialPort_Milliohmmeter.Write(strGesamteBefehl);

                // Antwort auslesen
                iAntwort = serialPort_Milliohmmeter.ReadByte();

                // Befehl erfolgreich übertragen?
                if ((Convert.ToChar(iAntwort)).ToString() == ACK)  // Wurde ein ACK empfangen
                {
                    // Daten anfordern
                    strGesamteBefehl = EOT;
                    serialPort_Milliohmmeter.Write(strGesamteBefehl);   // Befehl senden um Daten anzufordern

                    // Daten auslesen
                    strAntwort = serialPort_Milliohmmeter.ReadTo(ETX);    // Bis ETX auslesen, da ETX das letzte Zeichen ist

                    // Steuerzeichen aus String entfernen
                    strAntwort = strAntwort.Replace(STX, "");    // STX entfernen
                    strAntwort = strAntwort.Replace(CR, "");     // CR entfernen
                    strAntwort = strAntwort.Replace(LF, "");     // LF entfernen
                    strAntwort = strAntwort.Replace(ETX, "");    // ETX entfernen

                    // Empfang mit ACK bestätigen
                    strGesamteBefehl = ACK;
                    serialPort_Milliohmmeter.Write(strGesamteBefehl);

                    // Auslesen ob weitere Daten vorhanden sind
                    iAntwort = serialPort_Milliohmmeter.ReadByte();

                    // Weiteren Daten vorhanden?    -> Fehlerhaft da nicht zulässig
                    if ((Convert.ToChar(iAntwort)).ToString() != EOT)
                    {
                        // Solange kein EOT empfangen wurde
                        while ((Convert.ToChar(iAntwort)).ToString() == EOT)
                        {
                            // Daten auslesen
                            strAntwort = serialPort_Milliohmmeter.ReadTo(ETX);    // Bis ETX auslesen, da ETX das letzte Zeichen ist

                            // Empfang mit ACK bestätigen
                            strGesamteBefehl = ACK;
                            serialPort_Milliohmmeter.Write(strGesamteBefehl);

                            // Auslesen ob weitere Daten vorhanden sind
                            iAntwort = serialPort_Milliohmmeter.ReadByte();
                        }

                        MessageBox.Show("Kommunikation Milliohmmeter:\n\nZu viele Daten erhalten.");    // Aufgabe das zu viele Daten empfangen wurden
                        bErrorMilliohmmeter = true;
                    }
                }
                else // Kein ACK erhalten
                {
                    MessageBox.Show("Kommunikation Milliohmmeter:\n\nNAK erhalten.");
                    bErrorMilliohmmeter = true;
                }
            }
            catch (Exception _Exception)
            {
                MessageBox.Show("Kommunikation Milliohmmeter:\n\n" + _Exception.GetType().Name);
                bErrorMilliohmmeter = true;
            }

            return strAntwort;
        }
        #endregion

        //-----------------------------------------------------
        // Methoden für den Wachendorff Temperaturregler
        //-----------------------------------------------------
        #region Temperaturregler Kommunikation

        //-----------------------------------------------------
        // Verbindung mit Temperaturregler herstellen
        //-----------------------------------------------------
        private void Temperaturregler_Verbinden()
        {
            int iCaseTemperaturreglerConnect = 0;

            // Timer Timerout Temperaturregler initialisieren
            Timer_TimeoutTemperaturregler = new System.Timers.Timer();
            Timer_TimeoutTemperaturregler.Elapsed += OnTimer_TimeoutTemperaturregler;
            Timer_TimeoutTemperaturregler.AutoReset = false;
            Timer_TimeoutTemperaturregler.Enabled = false;

            // Sonlage kein Fehler aufgetreten ist und der Temperaturregler nicht verbunden ist
            while (bErrorTemperaturregler == false && bTemperaturreglerVerbunden == false)
            {
                switch (iCaseTemperaturreglerConnect)
                {
                    //-----------------------------------------------------
                    // Com-Port öffnen
                    //-----------------------------------------------------
                    case 0:
                        try
                        {
                            // Port einstellen
                            serialPort_Temperaturregler.PortName = strCOMPortTemperaturregler;
                            serialPort_Temperaturregler.BaudRate = iBaudrateTemperaturregler;

                            serialPort_Temperaturregler.Open(); // Port öffnen

                            iCaseTemperaturreglerConnect = 1;
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Temperaturregler:\n\n" + _Exception.GetType().Name);
                            bErrorTemperaturregler = true;
                        }

                        break;

                    //-----------------------------------------------------
                    // Verbindung testen
                    // Adresse des Temperaturreglers auslesen -> 247
                    //-----------------------------------------------------
                    case 1:
                        usModbusAdresse = 2318;     // Adresse zum Auslesen des Parameters Slave Adresse 2318
                        usAnzahlRegisterRead = 1;   // 1 Register soll gelesen werden

                        Modbus_ReadFunction03();

                        // Adressen des Temperaturreglers unterschiedlich?
                        if (usDatenTemperaturregler != byAdresseTemperaturregler)
                        {
                            MessageBox.Show("Kommunikation Temperaturregler:\n\n" + "Fehler beim Auslesen der Adresse.");
                            bErrorTemperaturregler = true;
                        }

                        iCaseTemperaturreglerConnect = 2;

                        break;

                    //-----------------------------------------------------
                    // Regelung stoppen
                    //-----------------------------------------------------
                    case 2:
                        usModbusAdresse = 1214;     // Adresse für stoppen
                        usWertSchreibenTemperaturregler = 0;

                        Modbus_WriteFunction06();

                        if (usDatenTemperaturregler == usWertSchreibenTemperaturregler)
                        {
                            bTemperaturreglerVerbunden = true;
                        }
                        else if (usDatenTemperaturregler != usWertSchreibenTemperaturregler && bErrorTemperaturregler == false) // Fehler setzen wenn false
                        {
                            MessageBox.Show("Kommunikation Temperaturregler:\n\n" + "Fehler beim Auslesen.");
                            bErrorTemperaturregler = true;
                        }

                        break;
                }
            }

            // Prüfung unter Temperatur standardmäßig deaktivieren
            checkBox_PruefungMitTemperatur.Checked = false;

            // Steuerelemente visualisieren
            if (bTemperaturreglerVerbunden == true & bErrorTemperaturregler == false)
            {
                Button_TemperaturreglerVerbinden.BackColor = Color.Green;
            }
            else
            {
                // Wenn Port offen ist, Port wieder schließen
                if (serialPort_Temperaturregler.IsOpen == true)
                {
                    try
                    {
                        serialPort_Temperaturregler.Close();  // Port schließen
                    }
                    catch (Exception _Exception)
                    {
                        MessageBox.Show("Kommunikation Temperaturregler:\n\n" + _Exception.GetType().Name);
                        bErrorTemperaturregler = true;
                    }
                }

                bTemperaturreglerVerbunden = false;
                Button_TemperaturreglerVerbinden.Enabled = true;
                Button_TemperaturreglerVerbinden.BackColor = Color.Red;
            }
        }

        //-----------------------------------------------------
        // Temperaturvorgaben einstellen
        //-----------------------------------------------------
        private void Temperaturregler_TemperaturEinstellen()
        {
            int iCase = 0;
            bool bUebertragungAbgeschlossen = false;

            double dEingestellteTemperaturOben = 0.0;
            double dEingestellteTemperaturUnten = 0.0;

            double dHystereseTemperaturregler1 = 0.0;
            double dHystereseTemperaturregler2 = 0.0;

            while (bUebertragungAbgeschlossen == false && bErrorTemperaturregler == false)
            {
                switch (iCase)
                {
                    // Temperaturvogabe und Offset auslesen
                    case 0:
                        dSollTemperaturHeizpatroneOben = Convert.ToDouble(numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben.Value);
                        dSollTemperaturHeizpatroneUnten = Convert.ToDouble(numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten.Value);

                        dOffsetTemperaturOben = Convert.ToDouble(numericUpDown_OffsetTemperaturProbeOben.Value);
                        dOffsetTemperaturUnten = Convert.ToDouble(numericUpDown_OffsetTemperaturProbeUnten.Value);

                        iCase = 1;

                        break;

                    // Sollwert Untere Probe des Temperaturreglers auslesen
                    case 1:
                        usModbusAdresse = 1250;     // Adresse zum Auslesen des Setpoint für Regler 1
                        usAnzahlRegisterRead = 1;   // 1 Register soll gelesen werden
                        Modbus_ReadFunction03();

                        // Kein Fehler aufgetreten
                        if (bErrorTemperaturregler == false)
                        {
                            // Durch 10 teilen, da die Temperatur in Zenti °C ausgelesen wird (Bsp. 80°C entsprechen 800)
                            dEingestellteTemperaturUnten = Convert.ToDouble(usDatenTemperaturregler / 10.0);
                        }

                        iCase = 2;

                        break;

                    // Sollwert Obere Probe des Temperaturreglers auslesen
                    case 2:
                        usModbusAdresse = 1249;     // Adresse zum Auslesen des Setpoint für Regler 2
                        usAnzahlRegisterRead = 1;   // 1 Register soll gelesen werden
                        Modbus_ReadFunction03();

                        // Kein Fehler aufgetreten
                        if (bErrorTemperaturregler == false)
                        {
                            // Durch 10 teilen, da die Temperatur in Zenti °C ausgelesen wird (Bsp. 80°C entsprechen 800)
                            dEingestellteTemperaturOben = Convert.ToDouble(usDatenTemperaturregler / 10.0);
                        }

                        iCase = 3;

                        break;

                    // Setpoint für Regler 1 überschreiben
                    case 3:
                        // Sind Sollwert und aktuelle Vorgabe unterschiedlich?
                        if ((dSollTemperaturHeizpatroneUnten + dOffsetTemperaturUnten) != dEingestellteTemperaturUnten)
                        {
                            usModbusAdresse = 1250;     // Adresse zum Schreiben des Setpoint für Regler 1
                            // Soll Temperatur mit 10 multiplizieren, da Temperatur in Zenti °C (Bsp. 80°C entsprechen 800)
                            usWertSchreibenTemperaturregler = (ushort)((dSollTemperaturHeizpatroneUnten + dOffsetTemperaturUnten) * 10.0);
                            Modbus_WriteFunction06();
                        }

                        iCase = 4;

                        break;

                    // Setpoint für Regler 2 überschreiben
                    case 4:
                        // Sind Sollwert und aktuelle Vorgabe unterschiedlich?
                        if ((dSollTemperaturHeizpatroneOben + dOffsetTemperaturOben) != dEingestellteTemperaturOben)
                        {
                            usModbusAdresse = 1249;     // Adresse zum Schreiben des Setpoint für Regler 2
                            // Soll Temperatur mit 10 multiplizieren, da Temperatur in Zenti °C (Bsp. 80°C entsprechen 800)
                            usWertSchreibenTemperaturregler = (ushort)((dSollTemperaturHeizpatroneOben + dOffsetTemperaturOben) * 10.0);
                            Modbus_WriteFunction06();
                        }

                        iCase = 5;

                        break;

                    // Hysterese Regler 1 auslesen
                    case 5:
                        usModbusAdresse = 2039;     // Adresse zum Auslesen der Hysterese für Regler 1
                        usAnzahlRegisterRead = 1;   // 1 Register soll gelesen werden
                        Modbus_ReadFunction03();

                        // Kein Fehler aufgetreten
                        if (bErrorTemperaturregler == false)
                        {
                            // Durch 10 teilen, da die Temperatur in Zenti °C ausgelesen wird (Bsp. 80°C entsprechen 800)
                            dHystereseTemperaturregler1 = Convert.ToDouble(usDatenTemperaturregler / 10.0);
                        }

                        iCase = 6;

                        break;

                    // Hysterese Regler 2 auslesen
                    case 6:
                        usModbusAdresse = 2058;     // Adresse zum Auslesen der Hysterese für Regler 2
                        usAnzahlRegisterRead = 1;   // 1 Register soll gelesen werden
                        Modbus_ReadFunction03();

                        // Kein Fehler aufgetreten
                        if (bErrorTemperaturregler == false)
                        {
                            // Durch 10 teilen, da die Temperatur in Zenti °C ausgelesen wird (Bsp. 80°C entsprechen 800)
                            dHystereseTemperaturregler2 = Convert.ToDouble(usDatenTemperaturregler / 10.0);
                        }

                        iCase = 7;

                        break;

                    // Hysterese Regler 1 überschreiben
                    case 7:
                        // Sind Sollwert und aktuelle Vorgabe unterschiedlich?
                        if (iHystereseTemperaturregler != dHystereseTemperaturregler1)
                        {
                            usModbusAdresse = 2039;     // Adresse zum Schreiben des Setpoint für Regler 2
                            // Soll Temperatur mit 10 multiplizieren, da Temperatur in Zenti °C (Bsp. 80°C entsprechen 800)
                            usWertSchreibenTemperaturregler = (ushort)(iHystereseTemperaturregler * 10.0);
                            Modbus_WriteFunction06();
                        }

                        iCase = 8;

                        break;

                    // Hysterese Regler 2 überschreiben
                    case 8:
                        // Sind Sollwert und aktuelle Vorgabe unterschiedlich?
                        if (iHystereseTemperaturregler != dHystereseTemperaturregler2)
                        {
                            usModbusAdresse = 2058;     // Adresse zum Schreiben des Setpoint für Regler 2
                            // Soll Temperatur mit 10 multiplizieren, da Temperatur in Zenti °C (Bsp. 80°C entsprechen 800)
                            usWertSchreibenTemperaturregler = (ushort)(iHystereseTemperaturregler * 10.0);
                            Modbus_WriteFunction06();
                        }

                        bUebertragungAbgeschlossen = true;

                        break;
                }
            }
        }

        //-----------------------------------------------------
        // Modbus - Lese Funktion 03
        //-----------------------------------------------------
        private void Modbus_ReadFunction03()
        {
            int iCase = 0;
            int iNextCaseMerker = 0;
            bool bBefehlAbgeschlossen = false;

            ushort usCRCLow;
            ushort usCRCHigh;

            byte byFunktion = 3;                            // Funktionscode - ReadHoldingRegisters
            byte[] byarrModbusAdresse = new byte[2];        // 2 Bytes für die Modbus-Adresse
            byte[] byarrAnzahlRegisterRead = new byte[2];   // 2 Bytes für die Anzahl der Register die gelesen werden sollen
            byte byCRCLow;                            // CRC Byte Low
            byte byCRCHigh;                           // CRC Byte High

            byte[] byarrTempMessage = new byte[6];          // Temporäre Nachricht mit 6 Bytes für die Bestimmung des CRC - (Slave Adresse, Funktion, ModbusAdresse und Anzahl Register Read)
            byte[] byarrMessage = new byte[8];              // 8 Bytes für die gesamte Nachricht

            // Variablen zum Auslesen der Daten
            byte[] byarrAntwortVorlauf = new byte[3];       // 3 Bytes Vorlauf -  Adresse, Funktion, Anzahl Bytes
            byte[] byarrAntwortDaten = new byte[2];         // Byte Array mit Anzahl der Daten die Folgen erstellen
            byte[] byarrAntwortCRC = new byte[5];           // 2 Bytes für CRC
            int iIstAnzahlBytesImPuffer = 0;
            int iSollAnzahlBytesImPuffer = 0;


            byarrModbusAdresse[0] = (byte)(usModbusAdresse >> 8);           // Höherwertige Byte ermitteln
            byarrModbusAdresse[1] = (byte)((usModbusAdresse << 8) >> 8);    // Niederwertige Byte ermitteln

            byarrAnzahlRegisterRead[0] = (byte)(usAnzahlRegisterRead >> 8);           // Höherwertige Byte ermitteln
            byarrAnzahlRegisterRead[1] = (byte)((usAnzahlRegisterRead << 8) >> 8);    // Niederwertige Byte ermitteln

            // Temporäre Message erstellen
            byarrTempMessage[0] = byAdresseTemperaturregler;
            byarrTempMessage[1] = byFunktion;
            byarrTempMessage[2] = byarrModbusAdresse[0];
            byarrTempMessage[3] = byarrModbusAdresse[1];
            byarrTempMessage[4] = byarrAnzahlRegisterRead[0];
            byarrTempMessage[5] = byarrAnzahlRegisterRead[1];

            // CRC bestimmen
            CRC_GenerationFunction(byarrTempMessage);

            // CRC High ermitteln
            usCRCHigh = usCRC;
            usCRCHigh = (ushort)(usCRCHigh >> 8);
            byCRCHigh = (byte)(usCRCHigh);

            // CRC Low ermitteln
            usCRCLow = usCRC;
            usCRCLow = (ushort)(usCRCLow << 8);
            usCRCLow = (ushort)(usCRCLow >> 8);
            byCRCLow = (byte)(usCRCLow);

            // Message erstellen
            byarrTempMessage.CopyTo(byarrMessage, 0);
            byarrMessage[6] = byCRCLow;
            byarrMessage[7] = byCRCHigh;

            // Solange kein Fehler aufgetreten ist, kein Timeout gekommen ist und der Befehl nicht abgeschlossen ist
            while (bErrorTemperaturregler == false && bTimeoutTemperaturregler == false && bBefehlAbgeschlossen == false)
            {
                switch (iCase)
                {
                    //-----------------------------------------------------
                    // Befehl senden
                    //-----------------------------------------------------
                    case 0:
                        try
                        {
                            // Ein und Ausgangspuffer leeren
                            serialPort_Temperaturregler.DiscardInBuffer();
                            serialPort_Temperaturregler.DiscardOutBuffer();

                            // Befehl senden
                            serialPort_Temperaturregler.Write(byarrMessage, 0, byarrMessage.Length);
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Temperaturregler:\n\n" + _Exception.GetType().Name);
                            bErrorTemperaturregler = true;
                        }

                        iNextCaseMerker = 3;
                        iSollAnzahlBytesImPuffer = 3;   // Drei Bytes müssen im Puffer sein
                        iIstAnzahlBytesImPuffer = 0;    // Ist Anzahl der gelesenen Bytes zurück setzen

                        iCase = 1;

                        break;

                    //-----------------------------------------------------
                    // Timer für Timeout initialisieren und starten
                    //-----------------------------------------------------
                    case 1:
                        Timer_TimeoutTemperaturregler.Interval = iTimeTimeoutTemperaturregler;
                        Timer_TimeoutTemperaturregler.Enabled = true;

                        iCase = 2;

                        break;

                    //-----------------------------------------------------
                    // Abfragen ob x Bytes im Puffer sind
                    //-----------------------------------------------------
                    case 2:
                        try
                        {
                            // Anzahl der Bytes im Puffer auslesen
                            iIstAnzahlBytesImPuffer = serialPort_Temperaturregler.BytesToRead; 

                            // Anzahl nicht erreicht?
                            if (iIstAnzahlBytesImPuffer < iSollAnzahlBytesImPuffer)
                            {
                                iCase = 2;
                            }
                            else
                            {
                                Timer_TimeoutTemperaturregler.Enabled = false;  // Timer stoppen
                                // In den vorgemerkten Case springen, da die Anzahl erreicht wurde
                                iCase = iNextCaseMerker;
                            }
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Temperaturregler:\n\n" + _Exception.GetType().Name);
                            bErrorTemperaturregler = true;
                        }

                        // Bei Timeout Error setzen
                        if (bTimeoutTemperaturregler == true)
                        {
                            bErrorTemperaturregler = true;
                        }

                        break;

                    //-----------------------------------------------------
                    // Ersten 3 Bytes auslesen - Vorlauf
                    //-----------------------------------------------------
                    case 3:
                        try
                        {
                            // 3 Bytes auslesen - 3te Byte gibt an, wie viele Bytes folgen
                            serialPort_Temperaturregler.Read(byarrAntwortVorlauf, 0, byarrAntwortVorlauf.Length);

                            iSollAnzahlBytesImPuffer = byarrAntwortVorlauf[2];  // Anzahl Datenbytes
                            iIstAnzahlBytesImPuffer = 0;    // Ist Anzahl der gelesenen Bytes zurück setzen

                            iNextCaseMerker = 4;
                            iCase = 1;  // Timer neu starten und auf die Bytes warten
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Temperaturregler:\n\n" + _Exception.GetType().Name);
                            bErrorTemperaturregler = true;
                        }

                        break;

                    //-----------------------------------------------------
                    // Datenbytes auslesen
                    //-----------------------------------------------------
                    case 4:
                        try
                        {
                            // Datenbytes auslesen
                            serialPort_Temperaturregler.Read(byarrAntwortDaten, 0, byarrAntwortDaten.Length);

                            iNextCaseMerker = 5;
                            iSollAnzahlBytesImPuffer = 2;   // 2 Bytes für CRC
                            iIstAnzahlBytesImPuffer = 0;    // Ist Anzahl der gelesenen Bytes zurück setzen

                            iCase = 1;  // Timer neu starten und auf die Bytes warten
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Temperaturregler:\n\n" + _Exception.GetType().Name);
                            bErrorTemperaturregler = true;
                        }

                        break;

                    //-----------------------------------------------------
                    // CRC auslesen
                    //-----------------------------------------------------
                    case 5:
                        try
                        {
                            // CRC auslesen
                            serialPort_Temperaturregler.Read(byarrAntwortCRC, 0, byarrAntwortCRC.Length);
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Temperaturregler:\n\n" + _Exception.GetType().Name);
                            bErrorTemperaturregler = true;
                        }

                        iCase = 6;

                        break;

                    //-----------------------------------------------------
                    // Daten formatieren
                    //-----------------------------------------------------
                    case 6:
                        // Daten zusammenführen
                        usDatenTemperaturregler = (ushort)(byarrAntwortDaten[0]);
                        usDatenTemperaturregler = (ushort)(usDatenTemperaturregler << 8);
                        usDatenTemperaturregler = (ushort)(usDatenTemperaturregler + (ushort)(byarrAntwortDaten[1]));

                        bBefehlAbgeschlossen = true;

                        break;

                }
            }
        }

        //-----------------------------------------------------
        // Modbus - Schreib Funktion 06
        //-----------------------------------------------------
        private void Modbus_WriteFunction06()
        {
            int iCase = 0;
            bool bBefehlAbgeschlossen = false;


            byte byFunktion = 06;
            byte[] byarrModbusAdresse = new byte[2];        // 2 Bytes für die Modbus-Adresse
            byte[] byarrValue = new byte[2];                // 2 Bytes für dén Wert der geschrieben werden soll

            byte[] byarrTempMessage = new byte[6];          // Temporäre Nachricht mit 6 Bytes für die Bestimmung des CRC - (Slave Adresse, Funktion, ModbusAdresse und Value)
            byte[] byarrMessage = new byte[8];              // 8 Bytes für die gesamte Nachricht
            byte byCRCLow;                            // CRC Byte Low
            byte byCRCHigh;                           // CRC Byte High

            ushort usCRCLow;
            ushort usCRCHigh;


            // Variablen zum Auslesen der Daten
            byte[] byarrAntwortVorlauf = new byte[2];       // Bytes Vorlauf -  Adresse, Funktion
            byte[] byarrAntwortDaten = new byte[4];         // Byte Array mit Anzahl der Daten die Folgen erstellen
            byte[] byarrAntwortCRC = new byte[5];           // 2 Bytes für CRC
            int iIstAnzahlBytesImPuffer = 0;


            byarrModbusAdresse[0] = (byte)(usModbusAdresse >> 8);           // Höherwertige Byte ermitteln
            byarrModbusAdresse[1] = (byte)((usModbusAdresse << 8) >> 8);    // Niederwertige Byte ermitteln

            byarrValue[0] = (byte)(usWertSchreibenTemperaturregler >> 8);           // Höherwertige Byte ermitteln
            byarrValue[1] = (byte)((usWertSchreibenTemperaturregler << 8) >> 8);    // Niederwertige Byte ermitteln

            // Temporäre Message erstellen
            byarrTempMessage[0] = byAdresseTemperaturregler;
            byarrTempMessage[1] = byFunktion;
            byarrTempMessage[2] = byarrModbusAdresse[0];
            byarrTempMessage[3] = byarrModbusAdresse[1];
            byarrTempMessage[4] = byarrValue[0];
            byarrTempMessage[5] = byarrValue[1];

            // CRC bestimmen
            CRC_GenerationFunction(byarrTempMessage);

            // CRC High ermitteln
            usCRCHigh = usCRC;
            usCRCHigh = (ushort)(usCRCHigh >> 8);
            byCRCHigh = (byte)(usCRCHigh);

            // CRC Low ermitteln
            usCRCLow = usCRC;
            usCRCLow = (ushort)(usCRCLow << 8);
            usCRCLow = (ushort)(usCRCLow >> 8);
            byCRCLow = (byte)(usCRCLow);

            // Message erstellen
            byarrTempMessage.CopyTo(byarrMessage, 0);
            byarrMessage[6] = byCRCLow;
            byarrMessage[7] = byCRCHigh;

            // Zum Test!!!
            bErrorTemperaturregler = false;
            bTimeoutTemperaturregler = false;

            // Solange kein Fehler aufgetreten ist, kein Timeout gekommen ist und der Befehl nicht abgeschlossen ist
            while (bErrorTemperaturregler == false && bTimeoutTemperaturregler == false && bBefehlAbgeschlossen == false)
            {
                switch (iCase)
                {
                    //-----------------------------------------------------
                    // Befehl senden
                    //-----------------------------------------------------
                    case 0:
                        try
                        {
                            // Ein und Ausgangspuffer leeren
                            serialPort_Temperaturregler.DiscardInBuffer();
                            serialPort_Temperaturregler.DiscardOutBuffer();

                            // Befehl senden
                            serialPort_Temperaturregler.Write(byarrMessage, 0, byarrMessage.Length);
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Temperaturregler:\n\n" + _Exception.GetType().Name);
                            bErrorTemperaturregler = true;
                        }

                        iCase = 1;

                        break;

                    //-----------------------------------------------------
                    // Timer für Timeout der Kommunikation starten
                    //-----------------------------------------------------
                    case 1:
                        Timer_TimeoutTemperaturregler.Interval = iTimeTimeoutTemperaturregler;
                        Timer_TimeoutTemperaturregler.Enabled = true;

                        iCase = 2;

                        break;

                    //-----------------------------------------------------
                    // Abfragen ob 8 Bytes im Puffer sind
                    //-----------------------------------------------------
                    case 2:
                        try
                        {
                            // Anzahl der Bytes im Puffer auslesen
                            iIstAnzahlBytesImPuffer = serialPort_Temperaturregler.BytesToRead;

                            // Anzahl nicht erreicht?
                            if (iIstAnzahlBytesImPuffer < 8)
                            {
                                iCase = 2;  // Weiter warten
                            }
                            else
                            {
                                Timer_TimeoutTemperaturregler.Enabled = false;  // Timer stoppen

                                iCase = 3;
                            }
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Temperaturregler:\n\n" + _Exception.GetType().Name);
                            bErrorTemperaturregler = true;
                        }

                        // Bei Timeout Error setzen
                        if (bTimeoutTemperaturregler == true)
                        {
                            bErrorTemperaturregler = true;
                        }

                        break;

                    //-----------------------------------------------------
                    // Ersten 2 Bytes auslesen - Vorlauf
                    //-----------------------------------------------------
                    case 3:
                        try
                        {
                            // 2 Bytes auslesen
                            serialPort_Temperaturregler.Read(byarrAntwortVorlauf, 0, byarrAntwortVorlauf.Length);

                            iCase = 4;
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Temperaturregler:\n\n" + _Exception.GetType().Name);
                            bErrorTemperaturregler = true;
                        }

                        break;

                    //-----------------------------------------------------
                    // Datenbytes auslesen - 2 Bytes für Modbus Adresse, 2 Bytes für Value
                    //-----------------------------------------------------
                    case 4:
                        try
                        {
                            // Datenbytes auslesen
                            serialPort_Temperaturregler.Read(byarrAntwortDaten, 0, byarrAntwortDaten.Length);

                            iCase = 5;
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Temperaturregler:\n\n" + _Exception.GetType().Name);
                            bErrorTemperaturregler = true;
                        }

                        break;

                    //-----------------------------------------------------
                    // CRC auslesen
                    //-----------------------------------------------------
                    case 5:
                        try
                        {
                            // CRC auslesen
                            serialPort_Temperaturregler.Read(byarrAntwortCRC, 0, byarrAntwortCRC.Length);
                        }
                        catch (Exception _Exception)
                        {
                            MessageBox.Show("Kommunikation Temperaturregler:\n\n" + _Exception.GetType().Name);
                            bErrorTemperaturregler = true;
                        }

                        iCase = 6;

                        break;

                    //-----------------------------------------------------
                    // Daten formatieren
                    //-----------------------------------------------------
                    case 6:
                        // Daten zusammenführen
                        usDatenTemperaturregler = (ushort)(byarrAntwortDaten[2]);
                        usDatenTemperaturregler = (ushort)(usDatenTemperaturregler << 8);
                        usDatenTemperaturregler = (ushort)(usDatenTemperaturregler + (ushort)(byarrAntwortDaten[3]));

                        bBefehlAbgeschlossen = true;

                        break;
                }
            }
        }

        //-----------------------------------------------------
        // Modbus - CRC Generierung
        //-----------------------------------------------------
        private void CRC_GenerationFunction(byte[] byarrMessage)
        {
            ushort usPoly = 0xA001;   // Generatorpolynom
            ushort usTempCRC;       // Temp CRC zur bestimmung des LSB
            ushort usSign;          // Einzelnes Zeichen der Message
            int iCounterMoveRight;

            byte bySign;
            int iIndexSign = 0;

            usCRC = 0xFFFF;  // CRC Register initialisieren

            // Solange Ende der Message noch nicht erreicht wurde
            while (iIndexSign < byarrMessage.Length)
            {
                bySign = byarrMessage[iIndexSign];              // Zeichen auslesen
                usSign = (ushort)Convert.ToInt16(bySign);       // Zeichen in unsigned Short wandeln

                usCRC = (ushort)(usCRC ^ usSign);

                iIndexSign++;
                iCounterMoveRight = 0;

                // Solange keine 8 Verschiebungen durchgeführt worden sind
                while (iCounterMoveRight < 8)
                {
                    usTempCRC = usCRC;

                    // Alle Bits außer dem ersten 0 setzen -> LSB herausfiltern
                    usTempCRC = (ushort)(usTempCRC << 15);
                    usTempCRC = (ushort)(usTempCRC >> 15);

                    usCRC = (ushort)(usCRC >> 1);       // Bitweise nach Rechts schieben - Um 1

                    // Ist das LSB 0?
                    if (usTempCRC == 1)
                    {
                        usCRC = (ushort)(usCRC ^ usPoly);         // Xor mit Generatorpolynom
                    }

                    iCounterMoveRight++;
                }
            }
        }

        #endregion

        //-----------------------------------------------------
        // Methoden für den Kraftsensor
        //-----------------------------------------------------
        #region Kraftsensor Kommunikation

        //-----------------------------------------------------
        // Kraftsensor Verbinden
        //-----------------------------------------------------
        private void Kraftsensor_Verbinden()
        {
            Kraftsensor_OpenPort();     // Verbindung herstellen

            // Verbindung erfolgreich?
            if (iKraftsensorHandle > 0)
            {
                Button_KraftsensorVerbinden.Enabled = false;    // Button sperren

                Kraftsensor_Init();     // Kraftsensor initialisieren
            }

            // Fehlererkennung bei der Verbindung mit dem Kraftsensor
            if (iKraftsensorHandle == (int)API9206.ENErrors.INVALID_PORT_ERROR)    // COM-Port konnte nicht geöffnet werden
            {
                bKraftsensorVerbunden = false;                          // Merker der Kraftsensor ist nicht verbunden

                Button_KraftsensorVerbinden.BackColor = Color.Red;
                Button_KraftsensorVerbinden.Enabled = true;             // Button zum Verbinden freigeben

                MessageBox.Show("Kommunikation Kraftsensor:\n\n" + strCOMPortKraftsensor + " konnte nicht geöffnet werden.");
            }
            else if (iReturnCode != (int)API9206.ENErrors.FUNCTION_OK)
            {
                bKraftsensorVerbunden = false;                          // Merker der Kraftsensor ist nicht verbunden

                Button_KraftsensorVerbinden.BackColor = Color.Red;
                Button_KraftsensorVerbinden.Enabled = true;             // Button zum Verbinden freigeben

                MessageBox.Show("Kommunikation Kraftsensor:\n\nKommunikationsfehler.");
            }
            else
            {
                bKraftsensorVerbunden = true;                           // Merker der Kraftsensor ist verbunden

                Button_KraftsensorVerbinden.BackColor = Color.Green;
            }
        }

        //-----------------------------------------------------
        // Kraftsensor Port Öffnen
        //-----------------------------------------------------
        private void Kraftsensor_OpenPort()
        {
            // COM-Portnummer ermitteln
            strPortNummerKraftsensor = strCOMPortKraftsensor.Replace("COM", "");    // COM ausschneiden
            iPortNummerKraftsensor = Int32.Parse(strPortNummerKraftsensor);         // Portnummer in Integer wandeln

            // COM-Port öffnen
            iKraftsensorHandle = API9206.OpenInterface(iPortNummerKraftsensor);     // Handle gibt an, ob ein Sensor verbunden ist
        }

        //-----------------------------------------------------
        // Kraftsensor Init
        //-----------------------------------------------------
        private void Kraftsensor_Init()
        {
            StringBuilder strbGetUnit = new StringBuilder();
            int iMessmodus = 0;
            int iMessdatenrate = 0;

            //-----------------------------------------------------
            // Einstellungen vornehmen
            //-----------------------------------------------------
            // Hinterlegte Einheit auslesen
            iReturnCode = API9206.GetUnit(iKraftsensorHandle, strbGetUnit);
            if (iReturnCode == (int)API9206.ENErrors.FUNCTION_OK)   // Kommunikation erfolgreich
            {
                // Einheit Newton eingestellt?
                if (strbGetUnit.ToString() != "N")
                {
                    strbGetUnit.Clear();
                    strbGetUnit.Append("N");
                    iReturnCode = API9206.SetUnit(iKraftsensorHandle, strbGetUnit); // Einheit einstellen
                }
            }

            // Messmodus auslesen und ggf. anpassen
            if (iReturnCode == (int)API9206.ENErrors.FUNCTION_OK)   // Kommunikation erfolgreich
            {
                iReturnCode = API9206.GetMeasurementMode(iKraftsensorHandle, ref iMessmodus);   // Messmodus auslesen

                if (iReturnCode == (int)API9206.ENErrors.FUNCTION_OK)   // Kommunikation erfolgreich
                {
                    // Messmodus MEAS_MODE_DMS_15mV_DC eingestellte?
                    if (iMessmodus != (int)API9206.ENMeasurementModes.MEAS_MODE_DMS_15mV_DC)    // Messmodus durch angeschlossenen Kraftsensor vorgegeben
                    {
                        iReturnCode = API9206.SetMeasurementMode(iKraftsensorHandle, (int)API9206.ENMeasurementModes.MEAS_MODE_DMS_15mV_DC);
                    }
                }
            }

            // Messdatenrate auslesen und ggf. anpassen
            if (iReturnCode == (int)API9206.ENErrors.FUNCTION_OK)   // Kommunikation erfolgreich
            {
                iReturnCode = API9206.GetMeasurementDatarate(iKraftsensorHandle, ref iMessdatenrate);   // Messrate auslesen

                if (iReturnCode == (int)API9206.ENErrors.FUNCTION_OK)   // Kommunikation erfolgreich
                {
                    // Messdatenrate DATARATE_10 (DATARATE_400 vorher) eingestellte?
                    if (iMessdatenrate != (int)API9206.ENMeasurementDataRates.DATARATE_10)
                    {
                        iReturnCode = API9206.SetMeasurementDatarate(iKraftsensorHandle, (int)API9206.ENMeasurementDataRates.DATARATE_10);
                    }
                }
            }
        }
        #endregion

        //-----------------------------------------------------
        // Methoden die durch Timer-Events ausgelöst werden
        //-----------------------------------------------------
        #region Timer-Events
        //-----------------------------------------------------
        // Timer für Timeout der Abfrage von TwinCAT
        //-----------------------------------------------------
        private void OnTimer_TimeoutTwinCAT(Object source, ElapsedEventArgs e)
        {
            Timer_TimeoutTwinCAT.Enabled = false;
            bTimeoutTwinCAT = true;
        }

        //-----------------------------------------------------
        // Timer für die Wartezeit während der Initialisierung von TwinCAT
        //-----------------------------------------------------
        private void OnTimer_WartezeitInitTwinCAT(Object source, ElapsedEventArgs e)
        {
            Timer_WartezeitReaktionTwinCAT.Enabled = false;
            bWartezeitReaktionTwinCATAbgelaufen = true;
        }

        //-----------------------------------------------------
        // Timer für die Oxidation (Default 250 ms)
        //-----------------------------------------------------
        private void OnTimer_WartezeitOxidation(Object source, ElapsedEventArgs e)
        {
            Timer_WartezeitOxidation.Enabled = false;
            bWartezeitOxidationAbgelaufen = true;
        }

        //-----------------------------------------------------
        // Timer für die Widerstandsmessung (Default 250 ms)
        //-----------------------------------------------------
        private void OnTimer_WartezeitWiderstandsmessung(Object source, ElapsedEventArgs e)
        {
            Timer_WartezeitWiderstandsmessung.Enabled = false;
            bWartezeitWiderstandsmessungAbgelaufen = true;
        }

        //-----------------------------------------------------
        // Timer für Timeout der Abfrage vom Temperaturregler
        //-----------------------------------------------------
        private void OnTimer_TimeoutTemperaturregler(Object source, ElapsedEventArgs e)
        {
            Timer_TimeoutTemperaturregler.Enabled = false;
            bTimeoutTemperaturregler = true;
        }

        //-----------------------------------------------------
        // Timer für die Wartezeit ehe die Temperatur erneut gemessen wird
        //-----------------------------------------------------
        private void OnTimer_WartezeitTemperaturmessung(Object source, ElapsedEventArgs e)
        {
            Timer_WartezeitTemperaturmessung.Enabled = false;
            bWartezeitTemperaturmessungAbgelaufen = true;
        }

        //-----------------------------------------------------
        // Timer für die Wartezeit ehe die Temperatur abgefragt wird.
        // Für den Idlebetrieb
        //-----------------------------------------------------
        private void OnTimer_WartezeitTemperaturmessungIdlebetrieb(object source, ElapsedEventArgs e)
        {
            bTimerTemperaturmessungAusgeloest = true;   // Merker, dass der Timer aktiv ist

            int iCase = 0;
            bool bAuslesenAbgeschlossen = false;

            while (bAuslesenAbgeschlossen == false && bErrorTemperaturregler == false)
            {
                switch (iCase)
                {
                    // Timer stoppen
                    case 0:
                        Timer_WartezeitTemperaturmessungIdlebetrieb.Enabled = false;

                        iCase = 1;

                        break;

                    // Temperatur der oberen Probe auslesen
                    case 1:
                        usModbusAdresse = 1000;     // Temperatur der oberen Probe auslesen
                        usAnzahlRegisterRead = 1;
                        Modbus_ReadFunction03();

                        if (bErrorTemperaturregler == false)
                        {
                            // Durch 10 teilen, um Temperatur in °C zu erhalten (Bsp. 246 = 24,6 °C)
                            dIstTemperaturProbeOben = Convert.ToDouble(usDatenTemperaturregler / 10.0); // Ergebniss in Double konvertieren
                            Invoke(new Action(() => Label_IstTemperaturHeizpatroneObenValue.Text = dIstTemperaturProbeOben.ToString() + " °C"));
                        }

                        iCase = 2;

                        break;

                    // Temperatur der unteren Probe auslesen
                    case 2:
                        usModbusAdresse = 1001;     // Temperatur der unteren Probe auslesen
                        usAnzahlRegisterRead = 1;
                        Modbus_ReadFunction03();

                        if (bErrorTemperaturregler == false)
                        {
                            // Durch 10 teilen, um Temperatur in °C zu erhalten (Bsp. 246 = 24,6 °C)
                            dIstTemperaturProbeUnten = Convert.ToDouble(usDatenTemperaturregler / 10.0); // Ergebniss in Double konvertieren
                            Invoke(new Action(() => Label_IstTemperaturHeizpatroneUntenValue.Text = dIstTemperaturProbeUnten.ToString() + " °C"));
                        }

                        bAuslesenAbgeschlossen = true;

                        break;
                }
            }

            // Kein Fehler aufgetreten?
            if (bErrorTemperaturregler == false)
            {
                // Timer neu starten
                Timer_WartezeitTemperaturmessungIdlebetrieb.Enabled = true;
            }
            else
            {
                MessageBox.Show("Kommunikation Temperaturregler:\n\n" + "Auslesen fehlgeschlagen");
            }

            bTimerTemperaturmessungAusgeloest = false;   // Merker, dass der Timer nicht aktiv ist

            // In CSV speichern
            dSollTemperaturHeizpatroneUnten = Convert.ToDouble(numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten.Value);
            dOffsetTemperaturUnten = Convert.ToDouble(numericUpDown_OffsetTemperaturProbeUnten.Value);

            // Aktuelle Uhrzeit ermitteln
            DateTime dtAktuelleUhrzeit = DateTime.Now;
            string strAktuelleUhrzeit = dtAktuelleUhrzeit.ToLongTimeString();

            swFileWriterPruefergebnisse.WriteLine(dSollTemperaturHeizpatroneUnten.ToString() + TAB + dOffsetTemperaturUnten.ToString() + TAB + dIstTemperaturProbeUnten.ToString() + TAB + strAktuelleUhrzeit);
        }


        //-----------------------------------------------------
        // Timer für den Idle Betrieb
        // Ermöglicht die Reaktion auf Änderungen durch TwinCAT, wenn keine Methode bearbeitet wird
        //-----------------------------------------------------
        private void OnTimer_IdelBetrieb(Object source, ElapsedEventArgs e)
        {
            Timer_IdleBetrieb.Enabled = false;      // Timer stoppen
        }

        //-----------------------------------------------------
        // Timer um die Reikraftmessung zu starten
        //-----------------------------------------------------
        private void OnTimer_ReibkraftmessungEnde(Object source, ElapsedEventArgs e)
        {
            Timer_ReibkraftmessungEnde.Enabled = false;
            bTimerReibkraftmessungEndeErreicht = true;
        }

        //-----------------------------------------------------
        // Timer bevor die Digitalanzeige angesprochen wird
        //-----------------------------------------------------
        private void OnTimer_DigitalanzeigeWartezeitVorAbfrage(Object source, ElapsedEventArgs e)
        {
            Timer_DigitalanzeigeWartezeitVorAnfrage.Enabled = false;
            bTimerWartezeitVorAbfrageAbgelaufen = true;
        }

        //-----------------------------------------------------
        // Timer bevor das Ergebnis der Digitalanzeige ausgelesen wird
        //-----------------------------------------------------
        private void OnTimer_DigitalanzeigeWartezeitVorAuslesen(Object source, ElapsedEventArgs e)
        {
            Timer_DigitalanzeigeWartezeitVorAuslesen.Enabled = false;
            bTimerWartezeitVorAuslesenAbgelaufen = true;
        }

        #endregion

        //-----------------------------------------------------
        // Backgroundworker für die Prüfungsdurchführung
        //-----------------------------------------------------
        private void BackgroundWorker_DoWork_Pruefungsdurchfuehrung(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            //-----------------------------------------------------
            // Variablen für die Prüfungsdurchführung
            //-----------------------------------------------------
            bool bPruefungEnde = false;
            bool bFehlerErkannt = false;

            string strTextToCSV;
            

            string strZeichenAbbruchCSV = "";
            int iCase = 0;
            int iCaseVorschau = 0;

            // Grenzwerte für das Prüfungsende
            uint uiGrenzwertZyklen = 0;
            double dGrenzwertWiderstand = 0;

            // Start und Ziel-Position je Zyklus
            int iPositionLinksJeZyklus = 99;
            int iPositionRechtsJeZyklus = 99;
            int iHubJeZyklus = 99;

            // Min und Max-Position je Zyklus
            int iMinPosition = 99;
            int iMaxPosition = 99;
            int iMaxHubLuftlagertisch = 99;

            // Messwerte je Zyklus
            string strZyklusWiderstand = "";
            string strTempZyklusWiderstand = "";
            double dZyklusWiderstand = 0;   // Aktuelle Widerstandswert
            int iReibkraftValue = 99;
            uint uiZyklusCounter = 0;

            // Reibkraftwerte
            bool bReibkraftGemessen = false;
            float fAktuelleReibkraft = 0;                       // Wert der ausgelesen wird
            
            float fReibkraftDruck = 0;
            float fReibkraftZug = 0;
            double[] darrReibkraftwerteZug = new double[50];    // Speicher für die Zugkräfte
            double[] darrReibkraftwerteDruck = new double[50];  // Speicher für die Druckkräfte
            int iCounterReibkraftwerteZug = 0;                  // Counter für den Speicher
            int iCounterReibkraftwerteDruck = 0;
            int iCounterMax = 0;
            string strCSVReibkraftZeile1 = "";
            string strCSVReibkraftZeile2 = "";
            string strCSVReibkraftZeile3 = "";


            // Temperaturmesswerte
            bool bTemperaturOk = false;     // Merker für Temperatur ok - false = nicht ok
            double dMinTemperaturProbeOben = 0.0;
            double dMinTemperaturProbeUnten = 0.0;
            double dMaxTemperaturProbeOben = 0.0;
            double dMaxTemperaturProbeUnten = 0.0;

            // Aktuelle Uhrzeit erfassen
            DateTime dtAktuelleUhrzeit = DateTime.Now;
            string strAktuelleUhrzeit;

            // Messzeit Milliohmmeter erfassen
            TimeSpan tsTimespanMilliohmmeter;
            int iMesszeitMilliohmmeterMS = 0;

            TimeSpan tsTimeSpanFahrzeit;
            int iFahrzeitNachLinks = 0;
            int iFahrzeitNachRechts = 0;

            // Variablen für die Darstellung der verstrichenen Zeit
            DateTime dtStartzeitPruefung = DateTime.Now;
            TimeSpan tsVerstricheneZeit;
            double dVerstricheneZeitMS;
            string strVerstricheneZeit = "";    // Verstrichene Zeit während der Prüfung

            // Variablen für die verbleibende Zeit
            DateTime dtVerbleibendeZeit = DateTime.Now;
            TimeSpan tsVerbleibendeZeit;
            double dVerbleibendeZeitMS;
            string strVerbleibendeZeit;
            double dZeitJeZyklusMS;
            int iZykluszahlVerbleibendeZeit = 0;

            // Schrittweite für den Ladebalken für die Zyklen
            double dSchrittweiteLadebalken = 0;

            //-----------------------------------------------------
            // Prüfung durchführen
            //-----------------------------------------------------
            // Keine Fehler aufgetreten und der Messtaster geparkt?
            if (bGetMesstasterVorhanden == true)
            {
                //-----------------------------------------------------
                // Widerstandsplott formatieren
                //-----------------------------------------------------
                Series serWiderstand = new Series();
                serWiderstand.ChartType = SeriesChartType.Line;

                chart_Widerstand.Invoke(new Action(() => chart_Widerstand.Series.Add(serWiderstand)));  // Datenserie dem Chart hinzufügen

                // Widerstandswert plotten Punkt (0 | 0)
                Invoke(new Action(() => serWiderstand.Points.AddXY(uiZyklusCounter, 0.0)));

                //-----------------------------------------------------
                // Reibkraftplot formatieren
                // Wenn die Reibkraft gemessen werden soll
                //-----------------------------------------------------
                Series serReibkraftDruck = new Series();
                Series serReibkraftZug = new Series();

                if (bReibkraftmessungAktiviert == true)
                {
                    serReibkraftDruck.ChartType = SeriesChartType.Line;
                    serReibkraftZug.ChartType = SeriesChartType.Line;

                    serReibkraftDruck.LegendText = "Druckkraft";
                    serReibkraftZug.LegendText = "Zugkraft";

                    serReibkraftDruck.Legend = "Legend1";
                    serReibkraftZug.Legend = "Legend1";

                    serReibkraftDruck.IsVisibleInLegend = true;
                    serReibkraftZug.IsVisibleInLegend = true;

                    chart_Reibkraft.Invoke(new Action(() => chart_Reibkraft.Series.Add(serReibkraftDruck)));
                    chart_Reibkraft.Invoke(new Action(() => chart_Reibkraft.Series.Add(serReibkraftZug)));

                    Invoke(new Action(() => serReibkraftDruck.Points.AddXY(uiZyklusCounter, 0.0)));
                    Invoke(new Action(() => serReibkraftZug.Points.AddXY(uiZyklusCounter, 0.0)));
                }

                //-----------------------------------------------------
                // Temperaturplot formatieren
                // Wenn Prüfung unter Temperatur durchgeführt werden soll
                //-----------------------------------------------------
                Series serTemperaturOben = new Series();
                Series serTemperaturUnten = new Series();

                if (bPruefungMitTemperatur == true)
                {
                    serTemperaturOben.ChartType = SeriesChartType.Line;
                    serTemperaturUnten.ChartType = SeriesChartType.Line;

                    serTemperaturOben.LegendText = "Temperatur obere Heizpatrone";
                    serTemperaturUnten.LegendText = "Temperatur untere Heizpatrone";

                    serTemperaturOben.Legend = "Legend1";
                    serTemperaturUnten.Legend = "Legend1";

                    serTemperaturOben.IsVisibleInLegend = true;
                    serTemperaturUnten.IsVisibleInLegend = true;

                    Invoke(new Action(() => Chart_IstTemperatur.Series.Add(serTemperaturOben)));
                    Invoke(new Action(() => Chart_IstTemperatur.Series.Add(serTemperaturUnten)));

                    Invoke(new Action(() => serTemperaturOben.Points.AddXY(uiZyklusCounter, 0.0)));
                    Invoke(new Action(() => serTemperaturUnten.Points.AddXY(uiZyklusCounter, 0.0)));
                }

                //-----------------------------------------------------
                // Grenzwerte festlegen
                //-----------------------------------------------------
                dGrenzwertWiderstand = Convert.ToDouble(NumericUpDown_RGrenzwertEnde.Value);
                uiGrenzwertZyklen = Convert.ToUInt32(NumericUpDown_GrenzwertZyklen.Value);

                //-----------------------------------------------------
                // Fortschrittsbalken einstellen
                //-----------------------------------------------------
                dSchrittweiteLadebalken = 1000.0 / uiGrenzwertZyklen;  // Faktor für einen Schritt berechnen

                progressBar.Minimum = 0;    // Minimalwert festlegen
                progressBar.Maximum = 1000; // Maximalwert festlegen

                //-----------------------------------------------------
                // Temperaturregler einstellen
                //-----------------------------------------------------
                if (bPruefungMitTemperatur == true)
                {
                    // Temperaturvorgaben einstellen
                    Temperaturregler_TemperaturEinstellen();

                    // Fehler mit dem Temperaturregler aufgetreten?
                    if (bErrorTemperaturregler == true)
                    {
                        bFehlerErkannt = true; // Merker, dass ein Fehler aufgetreten ist
                        MessageBox.Show("Kommunikation Temperaturregler:\n\n" + "Fehler beim Übertragen der Daten.");
                    }
                    else
                    {
                        // Grenzwerte für die Temperatur berechnen
                        dMinTemperaturProbeOben = (dSollTemperaturHeizpatroneOben + dOffsetTemperaturOben) - iHystereseTemperaturregler;
                        dMinTemperaturProbeUnten = (dSollTemperaturHeizpatroneUnten + dOffsetTemperaturUnten) - iHystereseTemperaturregler;

                        dMaxTemperaturProbeOben = (dSollTemperaturHeizpatroneOben + dOffsetTemperaturOben) + iHystereseTemperaturregler;
                        dMaxTemperaturProbeUnten = (dSollTemperaturHeizpatroneUnten + dOffsetTemperaturUnten) + iHystereseTemperaturregler;

                        // Masse der Heizpatronen zuschalten
                        try
                        {
                            bSetHeizpatronenMasseSchalten = true;
                            tcClient.WriteAny(hSetHeizpatronenMasseSchalten, bSetHeizpatronenMasseSchalten);
                        }
                        catch (Exception ex)
                        {
                            bSetHeizpatronenMasseSchalten = false;
                            bErrorTwinCAT = true;

                            MessageBox.Show(ex.Message);
                        }

                        // Temperaturregelung starten
                        // Kein Fehler beim Zuschalten der Masse aufgetreten?
                        if (bErrorTwinCAT == false)
                        {
                            // Temperaturregelung starten
                            usModbusAdresse = 1214;                 // Adresse zum Starten/Stoppen der Regelung
                            usWertSchreibenTemperaturregler = 1;
                            Modbus_WriteFunction06();

                            // Wurde die Änderung am Temperaturregler übernommen?
                            if (usWertSchreibenTemperaturregler == usDatenTemperaturregler)
                            {
                                bTemperaturregelungAktiv = true;            // Merker Temperaturregelung Aktiv
                            }
                            else
                            {
                                bTemperaturregelungAktiv = false;
                                bErrorTemperaturregler = true;
                                bFehlerErkannt = true; // Merker, dass ein Fehler aufgetreten ist
                                MessageBox.Show("Kommunikation Temperaturregler:\n\n" + "Fehler bei Start der Regelung.");
                            }
                        }
                    }
                }

                //-----------------------------------------------------
                // Prüfungsdurchführung
                // Solange kein Fehler auftritt und die Prüfung nicht beendet wird
                //-----------------------------------------------------
                while (bFehlerErkannt == false && bErrorSicherheitseinrichtung == false && bPruefungEnde == false && bButtonPruefungAbbrechen == false)
                {
                    switch (iCase)
                    {
                        //-----------------------------------------------------
                        // Ergebnisstabelle erstellen
                        //-----------------------------------------------------
                        case 0:
                            strTextToCSV = "";  // String leeren

                            strTextToCSV = "Zyklus Nr." + TAB + "Widerstand [mOhm]" + TAB + "Abbruch nach X [mOhm]" + TAB;
                            strTextToCSV = strTextToCSV + "Lineartisch Halteposition Links [µm]" + TAB + "Lineartisch Halteposition Rechts [µm]" + TAB + "Hub je Zyklus [µm]" + TAB;
                            strTextToCSV = strTextToCSV + "Lineartisch Min-Position [µm]" + TAB + "Lineartisch Max-Position [µm]" + TAB + "Max-Hub [µm]" + TAB;
                            strTextToCSV = strTextToCSV + "Uhrzeit" + TAB + "Messzeit Widerstandsmessung [ms]" + TAB;
                            strTextToCSV = strTextToCSV + "Soll Temperatur Oben [°C]" + TAB + "Ist Temperatur Oben [°C]" + TAB;
                            strTextToCSV = strTextToCSV + "Soll Temperatur Unten [°C]" + TAB + "Ist Temperatur Unten [°C]" + TAB;
                            strTextToCSV = strTextToCSV + "Reibkraft Druckrichtung [N]" + TAB + "Reibkraft Zugrichtung [N]" + TAB;
                            strTextToCSV = strTextToCSV + "Fahrzeit Fahrtrichtung Links [ms]" + TAB + "Fahrzeit Fahrtrichtung Rechts [ms]";

                            // Text in csv-Datei schreiben
                            try
                            {
                                swFileWriterPruefergebnisse.WriteLine(strTextToCSV);
                            }
                            catch (Exception _Exception)
                            {
                                MessageBox.Show("csv-Datei beschreiben:\n\n" + _Exception.GetType().Name);
                                bErrorSchreibeCSV = true;          // Merker für Fehler setzen
                            }
                            iCase = 2;

                            break;

                        //-----------------------------------------------------
                        // 
                        //-----------------------------------------------------
                        case 1:


                            iCase = 2;

                            break;

                        //-----------------------------------------------------
                        // Motor in Startposition drehen
                        //-----------------------------------------------------
                        case 2:
                            // Sollvorgaben setzen
                            dSetFirstZielposition = 360 * (-1);         // Motor auf Position 360 Grad drehen
                            dSetFirstZielgeschwindigkeit = dGeschwindigkeitServomotorReibkraftmessung;  // in Grad/s
                            dSetAcceleration = 0;           // Default Beschleunigung nutzen
                            dSetDeceleration = 0;           // Default Verzögerung nutzen

                            // Fahrbefehle setzen
                            bSetUngeregelteFahrbefehl = true;
                            bSetGereglteFahrbefehl = false;

                            ServoMotor_DrehungAufZielposition();

                            iCase = 3;

                            break;

                        //-----------------------------------------------------
                        // Servomotor Position 0 setzen
                        //-----------------------------------------------------
                        case 3:
                            dSetIstPosition = 0;
                            bSetClearPositionLag = false;   // Schleppabstand nicht löschen

                            ServoMotor_SetPosition();

                            iCaseVorschau = 4;      // Vorschau speichern
                            iCase = 33;             // Timer vor der Abfrage der Digitalanzeige zu starten

                            break;

                        //-----------------------------------------------------
                        // Aktuellen Wert der Digitalanzeige 0 setzen
                        //-----------------------------------------------------
                        case 4:
                            Digitalanzeige_ResetAnzeige();

                            iCase = 5;

                            break;

                        //-----------------------------------------------------
                        // Min und Max-Wert der Digitalanzeige reseten
                        //-----------------------------------------------------
                        case 5:
                            Digitalanzeige_MinMaxReset();

                            iCase = 6;

                            break;

                        //-----------------------------------------------------
                        // Optional Tara des Kraftsensors ermitteln
                        // Optional Proben aufheizen
                        //-----------------------------------------------------
                        case 6:
                            // Prüfung mit Reibkraftmessung?
                            if (bReibkraftmessungAktiviert == true)
                            {
                                // Reibkraft messen
                                //API9206.GetValue(iKraftsensorHandle, ref fReibkraftTara);   // Reibkraft als Float auslesen

                                //API9206.GetValue(iKraftsensorHandle, ref fAktuelleReibkraft);
                                //fTaraReibkraftDruck = fTaraReibkraftDruck + fAktuelleReibkraft;
                            }

                            // Soll eine Prüfung unter Temperatur durchgeführt werden?
                            if (bPruefungMitTemperatur == true)
                            {
                                // Zunächst Temperatur auslesen und prüfen, ob die Temperatur im Soll ist
                                // Merker setzen, damit die Temperatur ausgelesen werden kann
                                bWartezeitTemperaturmessungAbgelaufen = true;
                                iCase = 27;
                            }
                            else
                            {
                                dtStartzeitPruefung = DateTime.Now; // Uhrzeit für verstrichene Prüfungszeit
                                dtVerbleibendeZeit = DateTime.Now;  // Uhrzeit für verbleibende Prüfungszeit

                                iCase = 7;  // Zyklus starten
                            }

                            break;

                        //-----------------------------------------------------
                        // Servomotor auf Position 180 Grad drehen
                        //-----------------------------------------------------
                        case 7:
                            uiZyklusCounter++;  // Neuer Zyklus
                            iZykluszahlVerbleibendeZeit++;

                            Label_AktuellerZyklusValue.Invoke(new Action(() => Label_AktuellerZyklusValue.Text = uiZyklusCounter.ToString()));

                            iCounterReibkraftwerteDruck = 0; // Counter zurück setze

                            // Gereglte Fahrt?
                            if (bGeregelteFahrt == true)
                            {
                                // Sollvorgaben setzen
                                dSetFirstZielposition = 60 * (-1);                             // 45 Grad für 1/8 Umdrehung
                                dSetSecondZielposition = 120 * (-1);    // 90 Grad für Viertel Umdrehung
                                dSetThirdZielposition = 180 * (-1);    // 45 Grad für 1/8 Umdrehung
                                dSetFirstZielgeschwindigkeit = dZielgeschwindigkeit1;   // in Grad/s
                                dSetSecondZielgeschwindigkeit = dZielgeschwindigkeit2;
                                dSetThirdZielgeschwindigkeit = dZielgeschwindigkeit3;
                                dSetAcceleration = 0;           // Default Beschleunigung nutzen
                                dSetDeceleration = 0;           // Default Verzögerung nutzen

                                // Fahrbefehle setzen
                                bSetUngeregelteFahrbefehl = false;
                                bSetGereglteFahrbefehl = true;

                                ServoMotor_StartGeregeltDrehen();
                            }
                            else // ungeregelt
                            {
                                // Wenn die Reibkraft mitgemessen werden soll
                                if (bReibkraftmessungAktiviert == true)
                                {
                                    //-----------------------------------------------------
                                    // Messung der Reibkraft soll bei Erreichen von 45° erfolgen
                                    //-----------------------------------------------------
                                    // Flanke für Reibkraftmessung erzeugen
                                    dSetPositonFuerFlanke = -45;
                                    tcClient.WriteAny(hSetPositonFuerFlanke, dSetPositonFuerFlanke);

                                    bSetPositionsFlankeErzeugen = true;
                                    tcClient.WriteAny(hSetPositionsFlankeErzeugen, bSetPositionsFlankeErzeugen);
                                }

                                // Sollvorgaben setzen
                                dSetFirstZielposition = 180 * (-1);
                                dSetFirstZielgeschwindigkeit = dZielgeschwindigkeit1;

                                // Fahrbefehle setzen
                                bSetGereglteFahrbefehl = false;
                                bSetUngeregelteFahrbefehl = true;

                                ServoMotor_StartDrehen();
                            }

                            if (bErrorTwinCAT == false) // Timer starten, wenn kein Fehler aufgetreten ist
                            {
                                // Soll die Reibkraft gemessen werden?
                                if (bReibkraftmessungAktiviert == true)
                                {
                                    bReibkraftGemessen = false; // Merker löschen, dass ein Wert gemessen wurde

                                    //// Timer für die Reibkraftmessung starten
                                    //bTimerReibkraftmessungStarten = false;
                                    //Timer_ReibkraftmessungStarten.Interval = iTimeReibkraftmessungStarten;
                                    //Timer_ReibkraftmessungStarten.Enabled = true;
                                }

                                // Aktuelle Uhrzeit ermitteln
                                dtAktuelleUhrzeit = DateTime.Now;

                                // Timeout Timer starten
                                bTimeoutTwinCAT = false;    // Merker für Timeout zurücksetzen
                                Timer_TimeoutTwinCAT.Interval = iTimeTimeoutServomotor;
                                Timer_TimeoutTwinCAT.Enabled = true;    // Timer starten
                            }

                            iCase = 9;

                            break;

                        //-----------------------------------------------------
                        // Reibkraft messen
                        //-----------------------------------------------------
                        case 8:
                            // Reibkraft messen
                            API9206.GetValue(iKraftsensorHandle, ref fAktuelleReibkraft);   // Reibkraft als Float auslesen

                            darrReibkraftwerteDruck[iCounterReibkraftwerteDruck] = Convert.ToDouble(fAktuelleReibkraft);

                            fAktuelleReibkraft = fAktuelleReibkraft - fTaraWertKraftsensor; // Tara abziehen

                            // Nicht der erste Zyklus?
                            if (uiZyklusCounter != 1)
                            {
                                // Gewichteter Mittelwert mit 70:30
                                fReibkraftDruck = fReibkraftDruck * (float)0.7 + fAktuelleReibkraft * (float)0.3;
                            }
                            else
                            {
                                fReibkraftDruck = fAktuelleReibkraft;
                            }

                            bReibkraftGemessen = true;      // Merker, dass die Reibkraft gemessen wurde

                            iCase = 9;

                            break;

                        //-----------------------------------------------------
                        // Warten, dass die Drehung abgeschlossen wurde
                        //-----------------------------------------------------
                        case 9:
                            // Flanke für die Positionsüberschreitung gekommen und Messung noch nicht durchgeführt?
                            if (bReibkraftmessungAktiviert == true && bGetPositonsFlanke && bReibkraftGemessen == false)
                            {
                                iCase = 8;  // Reibkraft messen
                            }
                            else if (bGetFahrbefehlAbgeschlossen == true && bTimeoutTwinCAT == false)   // Fahrbefehl abgeschlossen?
                            {
                                Timer_TimeoutTwinCAT.Enabled = false;   // Timer für Timeout stoppen

                                // Fahrzeit erfassen
                                tsTimeSpanFahrzeit = DateTime.Now.Subtract(dtAktuelleUhrzeit);
                                iFahrzeitNachLinks = Convert.ToInt16(tsTimeSpanFahrzeit.TotalMilliseconds);

                                ServoMotor_FahrbefehlLoeschen();

                                iCase = 10;
                            }
                            else if (bTimeoutTwinCAT == true)   // Fehler eingetreten?
                            {
                                bErrorTwinCAT = true;
                            }
                            else // Kein Fehler?
                            {
                                iCase = 9;
                            }

                            break;

                        //-----------------------------------------------------
                        // Timer Wartezeit Oxidation starten
                        //-----------------------------------------------------
                        case 10:
                            Timer_WartezeitOxidation.Interval = iTimerWartezeitOxidation;
                            Timer_WartezeitOxidation.Enabled = true;

                            iCaseVorschau = 11;
                            iCase = 33;             // Timer vor der Abfrage der Digitalanzeige zu starten

                            break;

                        //-----------------------------------------------------
                        // Aktuellen Wert der Digitalanzeige auslesen
                        //-----------------------------------------------------
                        case 11:
                            iPositionLinksJeZyklus = Digitalanzeige_AktuellenWertAuslesen();  // Startposition auslesen

                            iCase = 12;  // Warten das Timer Oxidation abgelaufen ist

                            break;

                        //-----------------------------------------------------
                        // Warten, dass Timer Oxidation abgelaufen ist
                        //-----------------------------------------------------
                        case 12:
                            while (bWartezeitOxidationAbgelaufen == false)
                            {

                            }

                            Timer_WartezeitOxidation.Enabled = false;
                            bWartezeitOxidationAbgelaufen = false;  // Merker zurück setzen

                            iCase = 13;  // Motor drehen

                            break;

                        //-----------------------------------------------------
                        // Servomotor um 180 Grad drehen
                        //-----------------------------------------------------
                        case 13:

                            iCounterReibkraftwerteZug = 0; // Counter zurück setzen

                            // Gereglte Fahrt?
                            if (bGeregelteFahrt == true)
                            {
                                // Sollvorgaben setzen
                                dSetFirstZielposition = 240 * (-1);                             // 45 Grad für 1/8 Umdrehung
                                dSetSecondZielposition = 300 * (-1);    // 90 Grad für Viertel Umdrehung
                                dSetThirdZielposition = 360 * (-1);    // 45 Grad für 1/8 Umdrehung
                                dSetFirstZielgeschwindigkeit = dZielgeschwindigkeit1;   // in Grad/s
                                dSetSecondZielgeschwindigkeit = dZielgeschwindigkeit2;
                                dSetThirdZielgeschwindigkeit = dZielgeschwindigkeit3;
                                dSetAcceleration = 0;           // Default Beschleunigung nutzen
                                dSetDeceleration = 0;           // Default Verzögerung nutzen

                                // Fahrbefehle setzen
                                bSetUngeregelteFahrbefehl = false;
                                bSetGereglteFahrbefehl = true;

                                ServoMotor_StartGeregeltDrehen();
                            }
                            else // ungeregelt
                            {
                                // Wenn die Reibkraft mitgemessen werden soll
                                if (bReibkraftmessungAktiviert == true)
                                {
                                    //-----------------------------------------------------
                                    // Messung der Reibkraft soll bei Erreichen von 225° erfolgen
                                    //-----------------------------------------------------
                                    // Flanke für Reibkraftmessung erzeugen
                                    dSetPositonFuerFlanke = -180 - 45;
                                    tcClient.WriteAny(hSetPositonFuerFlanke, dSetPositonFuerFlanke);

                                    bSetPositionsFlankeErzeugen = true;
                                    tcClient.WriteAny(hSetPositionsFlankeErzeugen, bSetPositionsFlankeErzeugen);
                                }

                                // Sollvorgaben setzen
                                dSetFirstZielposition = 360 * (-1);
                                dSetFirstZielgeschwindigkeit = dZielgeschwindigkeit1;

                                // Fahrbefehle setzen
                                bSetGereglteFahrbefehl = false;
                                bSetUngeregelteFahrbefehl = true;

                                ServoMotor_StartDrehen();
                            }

                            if (bErrorTwinCAT == false) // Timer starten, wenn kein Fehler aufgetreten ist
                            {
                                // Soll die Reibkraft gemessen werden?
                                if (bReibkraftmessungAktiviert == true)
                                {
                                    bReibkraftGemessen = false; // Merker löschen, dass ein Wert gemessen wurde

                                    //// Timer für die Reibkraftmessung starten
                                    //bTimerReibkraftmessungStarten = false;
                                    //Timer_ReibkraftmessungStarten.Interval = iTimeReibkraftmessungStarten;
                                    //Timer_ReibkraftmessungStarten.Enabled = true;
                                }

                                // Aktuelle Uhrzeit ermitteln
                                dtAktuelleUhrzeit = DateTime.Now;

                                // Timeout Timer starten
                                bTimeoutTwinCAT = false;    // Merker für Timeout zurücksetzen
                                Timer_TimeoutTwinCAT.Interval = iTimeTimeoutServomotor;
                                Timer_TimeoutTwinCAT.Enabled = true;    // Timer starten
                            }

                            iCase = 15;

                            break;

                        //-----------------------------------------------------
                        // Reibkraft messen
                        //-----------------------------------------------------
                        case 14:
                            // Reibkraft messen
                            API9206.GetValue(iKraftsensorHandle, ref fAktuelleReibkraft);   // Reibkraft als Float auslesen

                            fAktuelleReibkraft = fAktuelleReibkraft - fTaraWertKraftsensor; // Tara abziehen

                            // Nicht der erste Zyklus?
                            if (uiZyklusCounter != 1)
                            {
                                // Gewichteter Mittelwert mit 70:30
                                fReibkraftZug = fReibkraftZug * (float)0.7 + fAktuelleReibkraft * (float)0.3;
                            }
                            else
                            {
                                fReibkraftZug = fAktuelleReibkraft;
                            }

                            bReibkraftGemessen = true;      // Merker, dass die Reibkraft gemessen wurde


                            iCase = 15;

                            break;

                        //-----------------------------------------------------
                        // Warten, dass die Drehung abgeschlossen wurde
                        //-----------------------------------------------------
                        case 15:
                            // Flanke für Überschreitung der Position aufgetreten und Messung noch nicht durchgeführt?
                            if (bReibkraftmessungAktiviert == true && bGetPositonsFlanke == true && bReibkraftGemessen == false)
                            {
                                iCase = 14;  // Reibkraft messen
                            }
                            else if (bGetFahrbefehlAbgeschlossen == true && bTimeoutTwinCAT == false)   // Fahrbefehl abgeschlossen?
                            {
                                Timer_TimeoutTwinCAT.Enabled = false;   // Timer für Timeout stoppen

                                // Fahrzeit erfassen
                                tsTimeSpanFahrzeit = DateTime.Now.Subtract(dtAktuelleUhrzeit);
                                iFahrzeitNachRechts = Convert.ToInt16(tsTimeSpanFahrzeit.TotalMilliseconds);

                                ServoMotor_FahrbefehlLoeschen();

                                iCase = 16;  // Widerstandsmessung starten
                            }
                            else if (bTimeoutTwinCAT == true)   // Fehler eingetreten?
                            {
                                bErrorTwinCAT = true;
                            }
                            else
                            {
                                iCase = 15;  // Reibkraft messen
                            }

                            break;

                        //-----------------------------------------------------
                        // Widerstandsmessung und Timer Oxidation starten
                        //-----------------------------------------------------
                        case 16:
                            // Timer Oxidation starten
                            Timer_WartezeitOxidation.Interval = iTimerWartezeitOxidation;
                            Timer_WartezeitOxidation.Enabled = true;

                            //-----------------------------------------------------
                            // Soll eine Prüfung unter Einfluss von Temperatur durchgeführt werden?
                            //-----------------------------------------------------
                            if (bPruefungMitTemperatur == true)
                            {
                                // Temperaturregelung stoppen
                                usModbusAdresse = 1214;                 // Adresse zum Starten/Stoppen der Regelung
                                usWertSchreibenTemperaturregler = 0;
                                Modbus_WriteFunction06();

                                // Masse der Heizpatronen abschalten
                                try
                                {
                                    bSetHeizpatronenMasseSchalten = false;
                                    tcClient.WriteAny(hSetHeizpatronenMasseSchalten, bSetHeizpatronenMasseSchalten);
                                }
                                catch (Exception ex)
                                {
                                    bErrorTwinCAT = true;
                                }

                                // Wurde die Änderung am Temperaturregler nicht übernommen?
                                if (usWertSchreibenTemperaturregler != usDatenTemperaturregler)
                                {
                                    bErrorTemperaturregler = true;
                                }
                            }

                            if (bErrorTwinCAT == false && bErrorTemperaturregler == false)
                            {
                                // Widerstandsmessung starten
                                Milliohmmeter_MessungStarten();

                                // Timer Widerstandsmessung starten
                                Timer_WartezeitWiderstandsmessung.Interval = iTimerWartezeitWiderstandsmessung;
                                Timer_WartezeitWiderstandsmessung.Enabled = true;

                                // Aktuelle Uhrzeit ermitteln
                                dtAktuelleUhrzeit = DateTime.Now;
                            }
                            else
                            {
                                Timer_WartezeitOxidation.Enabled = false;
                            }

                            iCaseVorschau = 17;
                            iCase = 33;             // Timer vor der Abfrage der Digitalanzeige zu starten

                            break;

                        //-----------------------------------------------------
                        // Aktuellen Wert der Digitalanzeige auslesen
                        //-----------------------------------------------------
                        case 17:
                            iPositionRechtsJeZyklus = Digitalanzeige_AktuellenWertAuslesen();  // Endposition auslesen

                            iCaseVorschau = 18;
                            iCase = 33;             // Timer vor der Abfrage der Digitalanzeige zu starten

                            break;

                        //-----------------------------------------------------
                        // Min-Wert der Digitalanzeige auslesen
                        //-----------------------------------------------------
                        case 18:
                            iMinPosition = Digitalanzeige_MinWertAuslesen();

                            iCaseVorschau = 19;
                            iCase = 33;             // Timer vor der Abfrage der Digitalanzeige zu starten

                            break;

                        //-----------------------------------------------------
                        // Max-Wert der Digitalanzeige auslesen
                        //-----------------------------------------------------
                        case 19:
                            iMaxPosition = Digitalanzeige_MaxWertAuslesen();

                            iCaseVorschau = 20;
                            iCase = 33;             // Timer vor der Abfrage der Digitalanzeige zu starten

                            break;

                        //-----------------------------------------------------
                        // Min und Max-Wert reseten
                        //-----------------------------------------------------
                        case 20:
                            Digitalanzeige_MinMaxReset();

                            iCase = 21;

                            break;

                        //-----------------------------------------------------
                        // Warten, dass Timer Widerstandsmessung abgelaufen ist.
                        //-----------------------------------------------------
                        case 21:
                            while (bWartezeitWiderstandsmessungAbgelaufen == false)
                            {

                            }

                            Timer_WartezeitWiderstandsmessung.Enabled = false;
                            bWartezeitWiderstandsmessungAbgelaufen = false; // Merker zurücksetzen

                            // Messzeit erfassen
                            tsTimespanMilliohmmeter = DateTime.Now.Subtract(dtAktuelleUhrzeit);
                            iMesszeitMilliohmmeterMS = Convert.ToInt16(tsTimespanMilliohmmeter.TotalMilliseconds);

                            iCase = 22;

                            break;

                        //-----------------------------------------------------
                        // Widerstand auslesen
                        //-----------------------------------------------------
                        case 22:
                            // Ist die Messung abgeschlossen
                            if (Milliohmmeter_IstMessungAbgeschlossen() == true)
                            {
                                strZyklusWiderstand = Milliohmmeter_MesswertAuslesen();

                                //-----------------------------------------------------
                                // Soll eine Prüfung unter Einfluss von Temperatur durchgeführt werden?
                                //-----------------------------------------------------
                                if (bPruefungMitTemperatur == true)
                                {
                                    // Masse der Heizpatronen einschalten
                                    try
                                    {
                                        bSetHeizpatronenMasseSchalten = true;
                                        tcClient.WriteAny(hSetHeizpatronenMasseSchalten, bSetHeizpatronenMasseSchalten);
                                    }
                                    catch (Exception ex)
                                    {
                                        bSetHeizpatronenMasseSchalten = false;
                                        bErrorTwinCAT = true;
                                    }

                                    // Kein Fehler beim Zuschalten der Masse aufgetreten?
                                    if (bErrorTwinCAT == false)
                                    {
                                        // Temperaturregelung starten
                                        usModbusAdresse = 1214;                 // Adresse zum Starten/Stoppen der Regelung
                                        usWertSchreibenTemperaturregler = 1;
                                        Modbus_WriteFunction06();

                                        // Wurde die Änderung am Temperaturregler nicht übernommen?
                                        if (usWertSchreibenTemperaturregler != usDatenTemperaturregler)
                                        {
                                            bErrorTemperaturregler = true;
                                        }
                                    }
                                }

                                //-----------------------------------------------------
                                // Messwert auswerten und Widerstand errechnen
                                //-----------------------------------------------------
                                // Widerstand zu Groß?
                                if (strZyklusWiderstand == ">>>")    // Zeichen dafür, das keine leitende Verbindung besteht
                                {
                                    strZyklusWiderstand = "inf";

                                    dZyklusWiderstand = dGrenzwertWiderstand + 1000;   // 
                                }
                                else
                                {
                                    // Einheit kürzen
                                    if (strZyklusWiderstand.Contains("MOHM"))    // Einheit in Milliohm?
                                    {
                                        strZyklusWiderstand = strZyklusWiderstand.Replace("MOHM", ""); // Einheit in Milliohm entfernen
                                    }
                                    else
                                    {
                                        strZyklusWiderstand.Replace("OHM", ""); // Einheit in OHM entfernen
                                        dZyklusWiderstand = Convert.ToDouble(strZyklusWiderstand);
                                        dZyklusWiderstand = dZyklusWiderstand * 1000.0; // Einheit in mOhm wandeln
                                        strZyklusWiderstand = dZyklusWiderstand.ToString();
                                    }
                                }

                                iCase = 23;
                            }
                            else // Messung noch nicht abgeschlossen
                            {
                                Timer_WartezeitWiderstandsmessung.Interval = 10;    // 10 ms warten
                                Timer_WartezeitWiderstandsmessung.Enabled = true;   // Timer starten

                                iCase = 21;
                            }

                            break;

                        //-----------------------------------------------------
                        // Messergebnisse in csv-Datei speichern
                        //      Temperatur wird zu Beginn des Zyklus gemessen
                        //-----------------------------------------------------
                        case 23:
                            // Gefahrener Hub
                            iHubJeZyklus = Math.Abs(iPositionRechtsJeZyklus - iPositionLinksJeZyklus);
                            // Max-Hub berechnen
                            iMaxHubLuftlagertisch = Math.Abs(iMaxPosition - iMinPosition);

                            // Aktuelle Uhrzeit ermitteln
                            dtAktuelleUhrzeit = DateTime.Now;
                            strAktuelleUhrzeit = dtAktuelleUhrzeit.ToLongTimeString();

                            // Grenzwert für den Widerstand nicht überschritten?
                            if (dZyklusWiderstand <= dGrenzwertWiderstand)
                            {
                                strZeichenAbbruchCSV = "";  // Zeichen leer
                            }
                            else
                            {
                                strZeichenAbbruchCSV = "x"; // x um die entsprechende Zeile zu kennzeichnen
                            }

                            strTextToCSV = "";  // String leeren
                            strTextToCSV = uiZyklusCounter.ToString() + TAB + strZyklusWiderstand + TAB + strZeichenAbbruchCSV + TAB;
                            strTextToCSV = strTextToCSV + iPositionLinksJeZyklus.ToString() + TAB + iPositionRechtsJeZyklus.ToString() + TAB + iHubJeZyklus.ToString() + TAB;
                            strTextToCSV = strTextToCSV + iMinPosition.ToString() + TAB + iMaxPosition.ToString() + TAB + iMaxHubLuftlagertisch.ToString() + TAB;
                            strTextToCSV = strTextToCSV + strAktuelleUhrzeit + TAB + iMesszeitMilliohmmeterMS.ToString() + TAB;

                            // Prüfung unter Einfluss von Temperatur wird durchgeführt
                            if (bPruefungMitTemperatur == true)
                            {
                                strTextToCSV = strTextToCSV + "" + TAB + "" + TAB;  // T oben
                                strTextToCSV = strTextToCSV + "" + TAB + "" + TAB;  // T unten
                            }
                            else
                            {
                                strTextToCSV = strTextToCSV + "-" + TAB + "-" + TAB;  // T oben
                                strTextToCSV = strTextToCSV + "-" + TAB + "-" + TAB;  // T unten
                            }

                            // Wenn die Reibkraft gemessen wird
                            if (bReibkraftmessungAktiviert == true)
                            {
                                strTextToCSV = strTextToCSV + fReibkraftDruck.ToString() + TAB + fReibkraftZug.ToString() + TAB;    // Reibkraft Druck und Zug
                            }
                            else
                            {
                                strTextToCSV = strTextToCSV + "-" + TAB + "-" + TAB;    // Reibkraft Druck und Zug
                            }

                            strTextToCSV = strTextToCSV + iFahrzeitNachLinks.ToString() + TAB + iFahrzeitNachRechts.ToString();

                            // Text in csv-Datei schreiben
                            try
                            {
                                swFileWriterPruefergebnisse.WriteLine(strTextToCSV);
                            }
                            catch (Exception _Exception)
                            {
                                MessageBox.Show("csv-Datei beschreiben:\n\n" + _Exception.GetType().Name);
                                bErrorSchreibeCSV = true;          // Merker für Fehler setzen
                            }

                            // Soll die Reibkraft mitgemessen werden?
                            if (bReibkraftmessungAktiviert == true)
                            {
                                //// Anzahl der Werte ermitteln
                                //if (iCounterReibkraftwerteDruck > iCounterReibkraftwerteZug)
                                //{
                                //    iCounterMax = iCounterReibkraftwerteDruck;
                                //}
                                //else
                                //{
                                //    iCounterMax = iCounterReibkraftwerteZug;
                                //}

                                //strCSVReibkraftZeile1 = "";

                                //// Spalten füllen
                                //for (int i = 0; i < iCounterMax; i++)
                                //{
                                //    // Druck Wert vorhanden?
                                //    if (iCounterReibkraftwerteDruck >= i)
                                //    {
                                //        strCSVReibkraftZeile1 = strCSVReibkraftZeile1 + TAB + darrReibkraftwerteDruck[i].ToString();
                                //    }
                                //    else
                                //    {
                                //        strCSVReibkraftZeile1 = strCSVReibkraftZeile1 + TAB + "0";
                                //    }

                                //    // Zug Wert vorhanden?
                                //    if (iCounterReibkraftwerteZug >= i)
                                //    {
                                //        strCSVReibkraftZeile1 = strCSVReibkraftZeile1 + TAB + darrReibkraftwerteZug[i].ToString();
                                //    }
                                //    else
                                //    {
                                //        strCSVReibkraftZeile1 = strCSVReibkraftZeile1 + TAB + "0";
                                //    }

                                //    // Letzte Zeile noch nicht erreicht?
                                //    if (i < iCounterMax - 1)
                                //    {
                                //        strCSVReibkraftZeile1 = strCSVReibkraftZeile1 + LF; // Zeilenumbruch einfügen
                                //    }
                                //    else // Zyklusnummer ohne LF ergänzen
                                //    {
                                //        strCSVReibkraftZeile1 = strCSVReibkraftZeile1 + TAB + iZylusCounter.ToString();
                                //    }
                                //}


                                strCSVReibkraftZeile1 = uiZyklusCounter.ToString() + TAB + fReibkraftDruck.ToString() + TAB + fReibkraftZug.ToString();
                                // Text in csv-Datei schreiben
                                try
                                {
                                    swFileWriterReibwerte.WriteLine(strCSVReibkraftZeile1);
                                }
                                catch (Exception _Exception)
                                {
                                    MessageBox.Show("csv-Datei Reibwerte beschreiben:\n\n" + _Exception.GetType().Name);

                                    // Datei schließen
                                    swFileWriterReibwerte.Dispose();
                                    swFileWriterReibwerte.Close();

                                    bFileCSVReibwerteOffen = false;   // Merker Datei ist geschlossen
                                    bErrorErstelleCSV = true;          // Merker für Fehler setzen
                                }

                                // Neuen Tarawert ermitteln
                                //API9206.GetValue(iKraftsensorHandle, ref fTaraReibkraft);
                            }

                            iCase = 24;

                            break;

                        //-----------------------------------------------------
                        // Plots mit Daten füllen
                        //      Temperatur wird vor dem aktuellen Zyklus gemessen
                        //-----------------------------------------------------
                        case 24:
                            // Widerstandswert plotten
                            strTempZyklusWiderstand = strZyklusWiderstand.Replace(".", ",");    // Punkt durch Komma ersetzen
                            dZyklusWiderstand = Convert.ToDouble(strTempZyklusWiderstand);
                            Invoke(new Action(() => serWiderstand.Points.AddXY(uiZyklusCounter, dZyklusWiderstand)));

                            // Temperatur plotten
                            if (bPruefungMitTemperatur == true)
                            {
                                Invoke(new Action(() => serTemperaturOben.Points.AddXY(uiZyklusCounter, dIstTemperaturProbeOben)));
                                Invoke(new Action(() => serTemperaturUnten.Points.AddXY(uiZyklusCounter, dIstTemperaturProbeUnten)));
                            }

                            // Reibkraft plotten
                            if (bReibkraftmessungAktiviert == true)
                            {
                                Invoke(new Action(() => serReibkraftDruck.Points.AddXY(uiZyklusCounter, Convert.ToDouble(fReibkraftDruck))));
                                Invoke(new Action(() => serReibkraftZug.Points.AddXY(uiZyklusCounter, Convert.ToDouble(fReibkraftZug))));
                            }

                            // Charts refreshen/Neu zeichnen
                            //-----------------------------------------------------
                            // Prüfungsdurchführund durch BackgroundWorker im neuen Thread
                            //-----------------------------------------------------
                            // Backgroundworker nicht aktiv?
                            if (backgroundWorker_Refresh.IsBusy != true)
                            {
                                backgroundWorker_Refresh.RunWorkerAsync();
                            }

                            // Refresh();

                            iCase = 25;

                            break;

                        //-----------------------------------------------------
                        // Warten, dass Timer Oxidation abgelaufen ist
                        //-----------------------------------------------------
                        case 25:
                            while (bWartezeitOxidationAbgelaufen == false)
                            {

                            }

                            Timer_WartezeitOxidation.Enabled = false;
                            bWartezeitOxidationAbgelaufen = false;  // Merker zurücksetzen

                            iCase = 26;

                            break;

                        //-----------------------------------------------------
                        // Abfrage, ob Prüfung mit Temperatur
                        //-----------------------------------------------------
                        case 26:
                            if (bPruefungMitTemperatur == true)
                            {
                                iCase = 27; // Temperatur überprüfen
                            }
                            else
                            {
                                iCase = 30; // Visualisierung des Prüfungsfortschrittes
                            }

                            break;

                        //-----------------------------------------------------
                        // Temperatur auslesen
                        //      Temperatur für den nächsten Zyklus auslesen
                        //-----------------------------------------------------
                        case 27:
                            // Kein Timer gestartet/Timer abgelaufen?
                            if (bWartezeitTemperaturmessungAbgelaufen == true)
                            {
                                // Temperatur der oberen Probe auslesen
                                usModbusAdresse = 1000;     // Temperatur der oberen Probe auslesen
                                usAnzahlRegisterRead = 1;
                                Modbus_ReadFunction03();

                                if (bErrorTemperaturregler == false)
                                {
                                    // Durch 10 teilen, um Temperatur in °C zu erhalten (Bsp. 246 = 24,6 °C)
                                    dIstTemperaturProbeOben = Convert.ToDouble(usDatenTemperaturregler / 10.0); // Ergebniss in Double konvertieren
                                    Invoke(new Action(() => Label_IstTemperaturHeizpatroneObenValue.Text = dIstTemperaturProbeOben.ToString() + " °C"));
                                }

                                // Temperatur der unteren Probe auslesen
                                usModbusAdresse = 1001;     // Temperatur der unteren Probe auslesen
                                usAnzahlRegisterRead = 1;
                                Modbus_ReadFunction03();

                                if (bErrorTemperaturregler == false)
                                {
                                    // Durch 10 teilen, um Temperatur in °C zu erhalten (Bsp. 246 = 24,6 °C)
                                    dIstTemperaturProbeUnten = Convert.ToDouble(usDatenTemperaturregler / 10.0); // Ergebniss in Double konvertieren
                                    Invoke(new Action(() => Label_IstTemperaturHeizpatroneUntenValue.Text = dIstTemperaturProbeUnten.ToString() + " °C"));
                                }

                                iCase = 28; // Auswertung der Temperaturen
                            }
                            else // Wartezeit noch nicht abgelaufen
                            {
                                iCase = 27;
                            }

                            break;

                        //-----------------------------------------------------
                        // Temperatur im Soll?
                        //-----------------------------------------------------
                        case 28:
                            //-----------------------------------------------------
                            // Temperatur der oberen und unteren Heizpatrone im Soll?
                            //-----------------------------------------------------
                            // Obere Temperatur im Soll?
                            if (dMaxTemperaturProbeOben > dIstTemperaturProbeOben && dIstTemperaturProbeOben > dMinTemperaturProbeOben)
                            {
                                // Untere Temperatur im Soll?
                                if (dMaxTemperaturProbeUnten > dIstTemperaturProbeUnten && dIstTemperaturProbeUnten > dMinTemperaturProbeUnten)
                                {
                                    bTemperaturOk = true;
                                }
                            }
                            
                            // Ist die Temperatur ok und erster Zyklus?
                            if (bTemperaturOk == true && uiZyklusCounter == 0)
                            {
                                iCase = 7;  // Start des ersten Zyklus
                            }
                            else if (bTemperaturOk == true)
                            {
                                iCase = 30; // Visualisierung des Prüfungsfortschrittes
                            }
                            else // Temperatur nicht ok
                            {
                                iCase = 29; // Timer für das Aufheizen starten
                            }

                            break;

                        //-----------------------------------------------------
                        // Timer für Aufheizen starten
                        //-----------------------------------------------------
                        case 29:

                            // Timer starten
                            bWartezeitTemperaturmessungAbgelaufen = false;      // Merker löschen
                            Timer_WartezeitTemperaturmessung.Interval = iWartezeitTemperaturmessung;
                            Timer_WartezeitTemperaturmessung.Enabled = true;

                            iCase = 27;     // Temperatur auslesen

                            break;

                        //-----------------------------------------------------
                        // Visualisierung des Prüfungsfortschrittes
                        //-----------------------------------------------------
                        case 30:
                            //-----------------------------------------------------
                            // Verstrichene Zeit berechnen
                            //-----------------------------------------------------
                            dtAktuelleUhrzeit = DateTime.Now;

                            tsVerstricheneZeit = DateTime.Now.Subtract(dtStartzeitPruefung);    // Verstrichene Zeit ermitteln

                            strVerstricheneZeit = tsVerstricheneZeit.Days.ToString() + " Tage, ";
                            strVerstricheneZeit = strVerstricheneZeit + tsVerstricheneZeit.Hours.ToString() + ":";
                            strVerstricheneZeit = strVerstricheneZeit + tsVerstricheneZeit.Minutes.ToString() + ":";
                            strVerstricheneZeit = strVerstricheneZeit + tsVerstricheneZeit.Seconds.ToString();

                            Label_VerstricheneZeitValue.Invoke(new Action(() => Label_VerstricheneZeitValue.Text = strVerstricheneZeit));   // Verstrichene Zeit ausgeben

                            //-----------------------------------------------------
                            // Verbleibende Zeit berechnen
                            //-----------------------------------------------------
                            // Verstrichene Zeit ermitteln seit Beginn/Fortsetzung der Prüfung
                            tsVerstricheneZeit = DateTime.Now.Subtract(dtVerbleibendeZeit);
                            dVerstricheneZeitMS = tsVerstricheneZeit.TotalMilliseconds;

                            dZeitJeZyklusMS = dVerstricheneZeitMS / iZykluszahlVerbleibendeZeit;              // Verstrichene Zeit für einen Zyklus bestimmen

                            dVerbleibendeZeitMS = dZeitJeZyklusMS * (uiGrenzwertZyklen - uiZyklusCounter);

                            tsVerbleibendeZeit = TimeSpan.FromMilliseconds(dVerbleibendeZeitMS);

                            
                            strVerbleibendeZeit = tsVerbleibendeZeit.Days.ToString() + " Tage, ";
                            strVerbleibendeZeit = strVerbleibendeZeit + tsVerbleibendeZeit.Hours.ToString() + ":";
                            strVerbleibendeZeit = strVerbleibendeZeit + tsVerbleibendeZeit.Minutes.ToString() + ":";
                            strVerbleibendeZeit = strVerbleibendeZeit + tsVerbleibendeZeit.Seconds.ToString();

                            Label_RestzeitValue.Invoke(new Action(() => Label_RestzeitValue.Text = strVerbleibendeZeit));   // Verbleibende Zeit ausgeben

                            //-----------------------------------------------------
                            // Ladebalken visualisieren
                            //-----------------------------------------------------
                            progressBar.Invoke(new Action(() => progressBar.Value = Convert.ToInt32(uiZyklusCounter * dSchrittweiteLadebalken)));

                            iCase = 31;

                            break;

                        //-----------------------------------------------------
                        // Servomotor Position 0 setzen
                        //-----------------------------------------------------
                        case 31:
                            dSetIstPosition = 0;
                            bSetClearPositionLag = false;   // Schleppabstand nicht löschen

                            ServoMotor_SetPosition();

                            //-----------------------------------------------------
                            // Bedingung Prüfungsende überprüfen
                            //-----------------------------------------------------
                            // Anzahl Zyklen erreicht oder Widerstand hat den Grenzwert überschritten?
                            if (uiZyklusCounter >= uiGrenzwertZyklen || dZyklusWiderstand > dGrenzwertWiderstand)
                            {
                                bPruefungEnde = true;   // Merker Prüfungsende erreicht
                            }
                            else if (bButtonPruefungPause == true)     // Prüfung pausiert?
                            {
                                // Button freigeben/sperren
                                Invoke(new Action(() => button_csvKopieren.Enabled = true));
                                Invoke(new Action(() => Button_Pausieren.Enabled = true));
                                Invoke(new Action(() => Button_PruefungAbbrechen.Enabled = true));

                                iCase = 32; // Prüfung wurde pausiert
                            }
                            else
                            {
                                iCase = 7;  // Servomotor drehen (Neuer Zyklus startet)
                            }

                            break;

                        //-----------------------------------------------------
                        // Warten, dass die Prüfung fortgesetzt wird oder die Ergebnisse kopiert werden
                        //-----------------------------------------------------
                        case 32:

                            // Kopie der csv-Datei erstellen?
                            if (bButtoncsvKopieren == true)
                            {
                                bButtoncsvKopieren = false; // Merker zurücksetzen

                                // Bestehende csv-Datei schließen
                                swFileWriterPruefergebnisse.Dispose();
                                swFileWriterPruefergebnisse.Close();
                                bFileCSVPruefergebnisseOffen = false;

                                // Dateinamen und Pfad zusammen setzen
                                strFilenameCopyCSV = "Zwischenstand nach " + uiZyklusCounter.ToString() + " Zyklen - " + strDateinameCSV;
                                strFilenameCopyCSV = strDateipfadCSV + "\\" + strFilenameCopyCSV;   
                                    
                                // csv-Datei erstellen
                                try
                                {
                                    // Neue Datei erstellen und durch false alte überschreiben. UTF8 Encoding um Umlaute darstellen zu können
                                    swFileCopyWriter = new StreamWriter(strFilenameCopyCSV, false, Encoding.UTF8);

                                    bFileCopyCSVOffen = true;   // Merker Leere Kopie ist geöffnet
                                }
                                catch (Exception _Exception)
                                {
                                    MessageBox.Show("csv-Datei Kopie erstellen:\n\n Kopie konnte nicht erstellt werden.\n" + _Exception.GetType().Name);

                                    bFileCopyCSVOffen = false;   // Merker Leere Kopie ist geschlossen
                                }

                                // Konnte die Datei geöffnet werden?
                                if (bFileCopyCSVOffen == true)
                                {
                                    // csv Datei mit den Ergebnissen laden
                                    try
                                    {
                                        // Neue Datei erstellen und durch false alte überschreiben. UTF8 Encoding um Umlaute darstellen zu können
                                        srFileReader = new StreamReader(strFilenameCSV, Encoding.UTF8);

                                        bFileCSVPruefergebnisseOffen = true;   // Merker Datei ist geöffnet
                                    }
                                    catch (Exception _Exception)
                                    {
                                        MessageBox.Show("csv-Datei laden:\n\ncsv-Datei konnte nicht geladen werden\n" + _Exception.GetType().Name);

                                        bFileCSVPruefergebnisseOffen = false;   // Merker Datei ist geschlossen
                                    }
                                }

                                // Sind beide Dateien geöffnet?
                                if (bFileCSVPruefergebnisseOffen == true && bFileCopyCSVOffen == true)
                                {
                                    strTextToCSV = "";

                                    // Solange der string nicht null ist - null Ende der csv-Datei
                                    while (strTextToCSV != null && bErrorErstelleCSV == false)
                                    {
                                        // Eine Zeile auslesen
                                        try
                                        {
                                            strTextToCSV = srFileReader.ReadLine();
                                        }
                                        catch (Exception _Exception)
                                        {
                                            MessageBox.Show("csv-Datei auslesen:\n\nZeile konnte nicht ausgelesen werden.\n" + _Exception.GetType().Name);

                                            bErrorErstelleCSV = true;          // Merker für Fehler setzen
                                        }

                                        // Wenn der ausgelesene String nicht null ist
                                        if (strTextToCSV != null && bErrorErstelleCSV == false)
                                        {
                                            // Eingelesene Zeile schreiben
                                            try
                                            {
                                                swFileCopyWriter.WriteLine(strTextToCSV);
                                            }
                                            catch (Exception _Exception)
                                            {
                                                MessageBox.Show("csv-Kopie beschreiben:\n\nDie Kopie der csv-Datei konnte nicht beschrieben werden.\n" + _Exception.GetType().Name);

                                                bErrorErstelleCSV = true;          // Merker für Fehler setzen
                                            }
                                        }
                                            
                                    }

                                    if (bErrorErstelleCSV == false)
                                    {
                                        MessageBox.Show("Kopieren der csv-Datei ist abgeschlossen.");
                                    }

                                    bErrorErstelleCSV = false;
                                }

                                // CSV-Dateien schließen
                                if (bFileCSVPruefergebnisseOffen == true)
                                {
                                    srFileReader.Dispose();
                                    srFileReader.Close();
                                    bFileCSVPruefergebnisseOffen = false;
                                }
                                    
                                if (bFileCopyCSVOffen)
                                {
                                    swFileCopyWriter.Dispose();
                                    swFileCopyWriter.Close();
                                    bFileCopyCSVOffen = false;
                                }

                                // csv-Datei der Ergebnisse mit Schreibzugriff öffnen
                                try
                                {
                                    // csv-Datei öffnen - true um an die Datei anzuhängen
                                    swFileWriterPruefergebnisse = new StreamWriter(strFilenameCSV, true, Encoding.UTF8);

                                    bFileCSVPruefergebnisseOffen = true;
                                }
                                catch (Exception _Exception)
                                {
                                    MessageBox.Show("csv-Datei konnte nicht geöffnet werden:\n\n" + _Exception.GetType().Name);

                                    bFileCSVPruefergebnisseOffen = false;   // Merker Leere Kopie ist geschlossen
                                }

                                // Button wieder freigeben
                                Invoke(new Action(() => Button_Pausieren.Enabled = true));
                            }

                            // Wurde der Button erneut gedrückt und somit der Merker zurück gesetzt?
                            if (bButtonPruefungPause == false)
                            {
                                // Uhrzeit neu aufzeichnen, da die Prüfung unterbrochen wurde
                                dtVerbleibendeZeit = DateTime.Now;  // Uhrzeit für verbleibende Prüfungszeit

                                iZykluszahlVerbleibendeZeit = 0;    // Zähler zurück setzen, da die Prüfung unterbrochen wurde

                                Invoke(new Action(() => Button_Pausieren.Enabled = true));

                                iCase = 7;  // Prüfung fortsetzen
                            }

                            break;

                        //-----------------------------------------------------
                        // Timer starten, bevor die Digitalanzeige abgefragt werden darf
                        //-----------------------------------------------------
                        case 33:
                            bTimerWartezeitVorAbfrageAbgelaufen = false;
                            Timer_DigitalanzeigeWartezeitVorAnfrage.Enabled = true;
                            iCase = 34;

                            break;

                        //-----------------------------------------------------
                        // Warten, bevor die Digitalanzeige abgefragt werden darf
                        //-----------------------------------------------------
                        case 34:
                            if (bTimerWartezeitVorAbfrageAbgelaufen == true)
                            {
                                bTimerWartezeitVorAbfrageAbgelaufen = false;
                                Timer_DigitalanzeigeWartezeitVorAnfrage.Enabled = false;

                                iCase = iCaseVorschau;  // Sprung zum Case, der in der Vorschau gespeichert ist
                            }

                            break;

                    }

                    // Fehlererkennung
                    if (bErrorTwinCAT == true || bErrorMilliohmmeter == true || bErrorKraftsensor == true || bErrorDigitalanzeige == true || bErrorTemperaturregler == true || bErrorSchreibeCSV == true)
                    {
                        bFehlerErkannt = true;
                    }
                }
            }

            //-----------------------------------------------------
            // Ggf. aktive Timer stoppen
            //-----------------------------------------------------
            Timer_ReibkraftmessungEnde.Enabled = false;
            Timer_WartezeitWiderstandsmessung.Enabled = false;
            Timer_WartezeitOxidation.Enabled = false;
            Timer_WartezeitTemperaturmessung.Enabled = false;
            Timer_TimeoutTwinCAT.Enabled = false;
            Timer_DigitalanzeigeWartezeitVorAnfrage.Enabled = false;


            //-----------------------------------------------------
            // Fehler in der Kommunikation mit Geräten aufgetreten?
            // Fehler mit den Sicherheitseinrichtungen aufgetreten?
            //-----------------------------------------------------
            if (bErrorTwinCAT == true || bErrorDigitalanzeige == true || bErrorMilliohmmeter == true || bErrorKraftsensor == true || bErrorSicherheitseinrichtung == true)
            {
                // Modul zur Fehlerbehandlung aufrufen
                Fehlerbehandlung();
            }

            // Prüfungsende erreicht oder Button Prüfung abbrechen gedrückt??
            if (bPruefungEnde == true || bButtonPruefungAbbrechen == true)
            {
                Button_Zuruecksetzen.Invoke(new Action(() => Button_Zuruecksetzen.Enabled = true));
                Button_PruefungAbbrechen.Invoke(new Action(() => Button_PruefungAbbrechen.Enabled = false));
                Button_Pausieren.Invoke(new Action(() => Button_Pausieren.Enabled = false));

                ServoMotor_Ausschalten();
            }



            bPruefungAktiv = false;
            bButtonPruefungPause = false;

            // Wenn eine der csv Dateien offen ist
            if (bFileCSVPruefergebnisseOffen == true)
            {
                // Datei schließen
                swFileWriterPruefergebnisse.Dispose();
                swFileWriterPruefergebnisse.Close();
                bFileCSVPruefergebnisseOffen = false;
            }

            if (bFileCSVReibwerteOffen == true)
            {
                // Datei schließen
                swFileWriterReibwerte.Dispose();
                swFileWriterReibwerte.Close();
                bFileCSVReibwerteOffen = false;
            }

            if (bFileCopyCSVOffen == true)
            {
                // Datei schließen
                swFileCopyWriter.Dispose();
                swFileCopyWriter.Close();
                bFileCopyCSVOffen = false;
            }

            //-----------------------------------------------------
            // Fenster mit den Prüfungsergebnissen aufrufen
            //-----------------------------------------------------
            Prüfungsergebnis FormPruefungsergebnis = new Prüfungsergebnis();

            // Wurden Zyklen durchgeführt?
            if (uiZyklusCounter > 0)
            {
                // Daten an globale Variablen übergeben umd Ergebnis in neuem Fenster zu visualisieren
                strGrenzwertZyklen = uiGrenzwertZyklen.ToString();
                strErreichteZyklen = uiZyklusCounter.ToString();
                strGrenzwertWiderstand = dGrenzwertWiderstand.ToString();
                strLetzteWiderstandswert = dZyklusWiderstand.ToString();
                strPruefungsdauer = strVerstricheneZeit;
            }
            else
            {

            }

            //-----------------------------------------------------
            // Neues Fenster im Vordergrund öffnen
            //-----------------------------------------------------
            FormPruefungsergebnis.ShowDialog();

            // Ende
        }

        private void BackgroundWorker_DoWork_StartpunktAnfahren(object sender, DoWorkEventArgs e)
        {
            int iCase = 0;
            int iCaseVorschau = 0;
            int iHub = 0;
            int iCounterVersuche = 0;
            bool bStartpunktGefunden = false;
            

            bool bErsteZyklusDerSuche = true;

            // Variablen für die Startpunktsuche
            bool bStartpunktBereichLinksGefunden = false;
            double dStartpunktwinkelbereich = 0.0;

            // Variablen Luftlagertisch
            double dMinWertDigitalanzeige = 0.0;
            double dMaxWertDigitalanzeige = 0.0;
            double dHub = 0.0;
            double dHalbeHub = 0.0;
            double dMittelstellungLuftlagertisch = 0.0;   // Mittelstellung des Luftlagertisches
            double dPositionLuftlagertisch = 64000.0;       // Startwert, liegt außerhalbn des möglichen Bereiches vom System

            double dKanteb = 0.0;
            double dVorherigePositionLuftlagertisch = 0.0;

            double dAbweichungMaxPositionLuftlagertisch = 100;
            double dAbweichungMinPositionLuftlagertisch = 100;

            // Variablen Motor
            double dAnkathete = 0.0;        // Ankathete für die Berechnung des nötigen Drehwinkels
            double dPhi = 0.0;
            double dZielWinkel = 0.0;
            double dGeschwindigkeit = 50;

            // Variablen für die Reibkraftmessung
            bool bErsteKraftwert = false;
            float fAkutelleKraftwert = 0;
            bool bReibkraftGemessen = false;
            int iCounterZyklusReibkraftmittel = 1;
            float fAktuelleReibwert = 0;

            int iTestCounter = 0;


            // Variablen zurücksetzen
            bStartpunktAngefahren = false;
            bStartpunktAnfahrenAbbrechen = false;

            //-----------------------------------------------------
            // Startposition finden
            //-----------------------------------------------------
            // Solange der Startpunkt nicht gefunden wurde und kein Fehler aufgetreten ist
            while (bStartpunktAngefahren == false && bStartpunktAnfahrenAbbrechen == false && bErrorDigitalanzeige == false && bErrorTwinCAT == false && bErrorSicherheitseinrichtung == false && bGetMesstasterVorhanden == true && bGetAbdeckungGeschlossen == true)
            {
                switch (iCase)
                {
                    //-----------------------------------------------------
                    // Servomotor einschalten, wenn ausgeschaltet
                    //-----------------------------------------------------
                    case 0:
                        if (bGetMotorEingeschaltet == false)    // Motor ausgeschaltet?
                        {
                            // Vorschub in beide Richtungen freigeben
                            bSetDrehrichtungRechts = true;
                            bSetDrehrichtungLinks = true;

                            // Motor einschalten
                            ServoMotor_Einschalten();
                        }
                        else
                        {
                            // Vorschub in beide Richtungen freigeben
                            bSetDrehrichtungRechts = true;
                            bSetDrehrichtungLinks = true;

                            // Vorschub setzen
                            ServoMotor_Drehrichtung();
                        }

                        iCaseVorschau = 1;
                        iCase = 14;             // Timer vor der Abfrage der Digitalanzeige zu starten

                        break;

                    //-----------------------------------------------------
                    // Position vom Servomotor auf 0 setzen
                    //-----------------------------------------------------
                    case 1:
                        dSetIstPosition = 0;
                        bSetClearPositionLag = false;   // Schleppabstand nicht löschen

                        ServoMotor_SetPosition();

                        // Servo um 360° drehen
                        dZielWinkel = 360;
                        dGeschwindigkeit = 300;
                        iCase = 2;  // Servomotor um Zielwinkel drehen

                        break;

                    //-----------------------------------------------------
                    // Servomotor um Winkel drehen
                    //-----------------------------------------------------
                    case 2:
                        // Sollvorgaben setzen
                        dSetFirstZielposition = dZielWinkel * (-1);        // um Phi drehen
                        dSetFirstZielgeschwindigkeit = dGeschwindigkeit;   // in Grad/s
                        dSetAcceleration = 0;           // Default Beschleunigung nutzen
                        dSetDeceleration = 0;           // Default Verzögerung nutzen
                        bSetUngeregelteFahrbefehl = true;
                        bSetGereglteFahrbefehl = false;

                        // Motor drehen
                        ServoMotor_DrehungAufZielposition();

                        iCaseVorschau = 3;
                        iCase = 14;             // Timer vor der Abfrage der Digitalanzeige zu starten

                        break;

                    //-----------------------------------------------------
                    // Min Wert der Digitalanzeige auslesen
                    // Linke Position
                    //-----------------------------------------------------
                    case 3:
                        dMinWertDigitalanzeige = Digitalanzeige_MinWertAuslesen();

                        if (bErrorDigitalanzeige == false)  // Kein Fehler aufgetreten?
                        {
                            iCaseVorschau = 4;      // Max-Wert der Anzeige auslesen
                            iCase = 14;             // Timer vor der Abfrage der Digitalanzeige zu starten
                        }

                        break;

                    //-----------------------------------------------------
                    // Max Wert der Digitalanzeige auslesen
                    // Rechte Position
                    //-----------------------------------------------------
                    case 4:
                        dMaxWertDigitalanzeige = Digitalanzeige_MaxWertAuslesen();

                        if (bErrorDigitalanzeige == false)  // Kein Fehler aufgetreten?
                        {
                            // Hub in µm berechnen
                            iHub = Convert.ToInt16(Math.Abs(dMaxWertDigitalanzeige - dMinWertDigitalanzeige));

                            iCase = 5;  // Servoposition 0 setzen
                        }

                        break;

                    //-----------------------------------------------------
                    // Position vom Servomotor auf 0 setzen
                    //-----------------------------------------------------
                    case 5:
                        dSetIstPosition = 0;
                        bSetClearPositionLag = false;   // Schleppabstand nicht löschen

                        ServoMotor_SetPosition();

                        iCaseVorschau = 6;      // Aktuellen Wert der Anzeige auslesen
                        iCase = 14;             // Timer vor der Abfrage der Digitalanzeige zu starten

                        break;

                    //-----------------------------------------------------
                    // Aktuellen Wert der Digitalanzeige auslesen
                    //-----------------------------------------------------
                    case 6:
                        dPositionLuftlagertisch = Digitalanzeige_AktuellenWertAuslesen();   // Aktuelle Position auslesen

                        // Winkel berechnen
                        dHub = Math.Abs(dMaxWertDigitalanzeige - dMinWertDigitalanzeige);
                        dMittelstellungLuftlagertisch = dMaxWertDigitalanzeige - (dHub / 2.0);

                        if (dPositionLuftlagertisch < dMittelstellungLuftlagertisch)
                        {
                            dKanteb = Math.Abs(dMittelstellungLuftlagertisch - dPositionLuftlagertisch);
                            dPhi = Math.Cos(dKanteb / (dHub / 2.0));
                            dPhi = (dPhi / Math.PI) * 180.0;
                            dPhi = dPhi + 90;
                        }
                        else
                        {
                            dKanteb = Math.Abs(dPositionLuftlagertisch - dMittelstellungLuftlagertisch);
                            dHalbeHub = dHub / 2.0;
                            dPhi = Math.Acos(dKanteb / dHalbeHub);
                            dPhi = (dPhi / Math.PI) * 180.0;
                            dPhi = dPhi + 5.0;
                        }

                        dGeschwindigkeit = 200;

                        iCase = 7;

                        //// Wurde der erste Zyklus bereits durchgeführt?
                        //if (bErsteZyklusDerSuche == false)
                        //{
                        //    dVorherigePositionLuftlagertisch = dPositionLuftlagertisch; // Alte Position merken
                        //}

                        //dPositionLuftlagertisch = Digitalanzeige_AktuellenWertAuslesen();   // Aktuelle Position auslesen

                        //if (bErrorDigitalanzeige == false)  // Kein Fehler aufgetreten?
                        //{
                        //    if (bErsteZyklusDerSuche == false && Math.Abs(dMaxWertDigitalanzeige - dPositionLuftlagertisch) <= 1.0)
                        //    {
                        //        bStartpunktBereichLinksGefunden = true;
                        //        dStartpunktwinkelbereich++; // Bereich hoch zählen

                        //        dZielWinkel = dZielWinkel + 1;
                        //        iCase = 7;
                        //    }
                        //    else if (bStartpunktBereichLinksGefunden == true)   // Bereich wurde wieder verlassen
                        //    {
                        //        bStartpunktGefunden = true;
                        //        dStartpunktwinkelbereich = dStartpunktwinkelbereich / 2.0;  // Mitte berechnen

                        //        dZielWinkel = dZielWinkel + (360 - dStartpunktwinkelbereich);
                        //        dGeschwindigkeit = 200;
                        //        iCase = 7;  // Servo um berechneten Winkel drehen
                        //    }
                        //    else if (Math.Abs(dMaxWertDigitalanzeige - dPositionLuftlagertisch) <= 1)
                        //    {
                        //        // Servo im Ausgangspunkt neu positionieren
                        //        dZielWinkel = 90;
                        //        dGeschwindigkeit = 200;
                        //        iCase = 2;
                        //    }
                        //    else
                        //    {
                        //        dZielWinkel = dZielWinkel + 1;
                        //        iCase = 7;
                        //    }

                        //}

                        //bErsteZyklusDerSuche = false;

                        break;

                    //-----------------------------------------------------
                    // Servomotor um Winkel drehen
                    //-----------------------------------------------------
                    case 7:
                        // Sollvorgaben setzen
                        dSetFirstZielposition = dPhi * (-1);        // um Phi drehen
                        dSetFirstZielgeschwindigkeit = dGeschwindigkeit;   // in Grad/s
                        dSetAcceleration = 0;           // Default Beschleunigung nutzen
                        dSetDeceleration = 0;           // Default Verzögerung nutzen
                        bSetUngeregelteFahrbefehl = true;
                        bSetGereglteFahrbefehl = false;

                        // Motor drehen
                        ServoMotor_DrehungAufZielposition();

                        if (iCounterVersuche < 0)
                        {
                            iCounterVersuche++;

                            iCase =  1;
                        }
                        else
                        {
                            iCaseVorschau = 8;      // Anzeige reset
                            iCase = 14;             // Timer vor der Abfrage der Digitalanzeige zu starten
                        }

                        break;

                    //-----------------------------------------------------
                    // Aktuelle Position der Digitalanzeige rücksetzen
                    //-----------------------------------------------------
                    case 8:
                        Digitalanzeige_ResetAnzeige();

                        iCaseVorschau = 9;
                        iCase = 14;             // Timer vor der Abfrage der Digitalanzeige zu starten

                        break;

                    //-----------------------------------------------------
                    // Min- und Maxwert der Digitalanzeige rücksetzen
                    //-----------------------------------------------------
                    case 9:
                        Digitalanzeige_MinMaxReset();

                        iCase = 10; // Positon Servomotor 0 setzen

                        break;

                    //-----------------------------------------------------
                    // Position Servomotor 0 setzen
                    // Suche nach Startpunkt ist abgeschlossen
                    //-----------------------------------------------------
                    case 10:
                        dSetIstPosition = 0;
                        bSetClearPositionLag = true;

                        ServoMotor_SetPosition();

                        // Prüfung mit Reibkraftmessung durchführen?
                        if (bReibkraftmessungAktiviert == true)
                        {
                            Invoke(new Action(() => textBox_HubEinstellenAktuelleAufgabe.Text = "Tara der Reibkraft messen."));

                            //-----------------------------------------------------
                            // 2 Sekunden lang laufen Tarawert für den Kraftsensor messen
                            //-----------------------------------------------------
                            // Timer für Ende der Reibkraftmessung starten
                            bTimerReibkraftmessungEndeErreicht = false;
                            Timer_ReibkraftmessungEnde.Interval = iTimeReibkraftmessungEnde;
                            Timer_ReibkraftmessungEnde.Enabled = true;


                            iCase = 12; // Reibkraft messen
                        }
                        else
                        {
                            Invoke(new Action(() => textBox_HubEinstellenAktuelleAufgabe.Text = "Mittelstellung anfahren."));

                            iCase = 13; // Mittelstellung anfahren
                        }

                        break;

                    //-----------------------------------------------------
                    // Reibkraft messen
                    //-----------------------------------------------------
                    case 12:
                        // Ersten Wert bereits erhalten
                        if (bErsteKraftwert == true)
                        {
                            API9206.GetValue(iKraftsensorHandle, ref fAkutelleKraftwert);   // Kraft als Float auslesen
                            // Gewichteten Mittelwert berechnen
                            fTaraWertKraftsensor = fTaraWertKraftsensor * (float)0.7 + fAkutelleKraftwert * (float)0.3;
                        }
                        else
                        {
                            API9206.GetValue(iKraftsensorHandle, ref fTaraWertKraftsensor);   // Kraft als Float auslesen
                            bErsteKraftwert = true;
                        }

                        if (bTimerReibkraftmessungEndeErreicht == true)
                        {
                            Invoke(new Action(() => textBox_HubEinstellenAktuelleAufgabe.Text = "Mittelstellung anfahren."));

                            iCase = 13;
                        }

                        break;

                    //-----------------------------------------------------
                    // Ungefähre Mittelstellung anfahren - Motor um 90 Grad drehen
                    //-----------------------------------------------------
                    case 13:
                        dSetFirstZielposition = 90 * (-1);          // 90 Grad
                        dSetFirstZielgeschwindigkeit = 300;
                        dSetAcceleration = 0;           // Default Wert nutzen
                        dSetDeceleration = 0;           // Default Wert nutzen
                        bSetUngeregelteFahrbefehl = true;
                        bSetGereglteFahrbefehl = false;

                        // Zum Test mit Druckfeder rausgenommen
                        //ServoMotor_DrehungAufZielposition();

                        // Drehrichtung Rechts sperren
                        bSetDrehrichtungRechts = false;

                        ServoMotor_Drehrichtung();

                        bStartpunktAngefahren = true;     // Merker Startpunkt gefunden

                        Invoke(new Action(() => textBox_HubEinstellenAktuelleAufgabe.Text = "Startpunkt angefahren."));

                        break;

                    //-----------------------------------------------------
                    // Timer starten, bevor die Digitalanzeige abgefragt werden darf
                    //-----------------------------------------------------
                    case 14:
                        bTimerWartezeitVorAbfrageAbgelaufen = false;
                        Timer_DigitalanzeigeWartezeitVorAnfrage.Enabled = true;
                        iCase = 15;

                        break;

                    //-----------------------------------------------------
                    // Warten, bevor die Digitalanzeige abgefragt werden darf
                    //-----------------------------------------------------
                    case 15:
                        if (bTimerWartezeitVorAbfrageAbgelaufen == true)
                        {
                            bTimerWartezeitVorAbfrageAbgelaufen = false;
                            Timer_DigitalanzeigeWartezeitVorAnfrage.Enabled = false;

                            iCase = iCaseVorschau;  // Sprung zum Case, der in der Vorschau gespeichert ist
                        }

                        break;

                    default:
                        break;
                }
            }

            Timer_ReibkraftmessungEnde.Enabled = false;

            //-----------------------------------------------------
            // Fehlerbehandlung
            //-----------------------------------------------------
            // Fehler aufgetreten
            if (bErrorDigitalanzeige == true || bErrorTwinCAT == true || bErrorSicherheitseinrichtung == true)
            {
                // Fenster Hubeinstellung schließen
                // Steuerelemente zurücksetzen
                Invoke(new Action(() => Panel_HubEinstellen.Visible = false));    // Panel für die Einstellung des Hubs ausblenden
                Invoke(new Action(() => Button_HubEinstellen.Enabled = false));   // Button sperren, da Digitalanzeige nicht verbunden ist
                
                // Variablen zurücksetzen
                bHubeinstellungAbgeschlossen = false;
                bStartpunktAngefahren = false;     // Merker Startpunkt nicht gefunden
                bFensterHubEinstellenOffen = false;

                Fehlerbehandlung();     // Methode zur Fehlerbehandlung aufrufen
            }
            else
            {
                if (bGetMesstasterVorhanden == false)
                {
                    MessageBox.Show("Messtaster ist nicht geparkt.");
                }
                else if (bGetAbdeckungGeschlossen == false)
                {
                    MessageBox.Show("Abdeckung wurde geöffnet.");
                }
                else if (bStartpunktAnfahrenAbbrechen == false) // Button Abbrechen nicht gedrückt?
                {
                    Invoke(new Action(() => Button_HubEinstellenSpeichern.Enabled = true));
                }

                // Button freigeben
                Invoke(new Action(() => button_HubErmitteln.Enabled = true));
                Invoke(new Action(() => Button_MesstasterNullen.Enabled = true));
                Invoke(new Action(() => Button_HubEinstellenAbbrechen.Enabled = true));
                Invoke(new Action(() => Button_StartpunktAnfahren.Enabled = true));
                Invoke(new Action(() => CheckBox_HubEingestellt.Enabled = true));
            }
        }

        private void BackgroundWorker_DoWork_Refresh(object sender, DoWorkEventArgs e)
        {
            Refresh();
        }

        private void Button_Zuruecksetzen_Click(object sender, EventArgs e)
        {
            ResetSoftware();

            // Hubeinstellung freigeben
            Button_HubEinstellen.Enabled = true;
        }

        private void button_csvKopieren_Click(object sender, EventArgs e)
        {
            bButtoncsvKopieren = true;          // Merker, dass der Button gedrückt wurde

            // Button sperren
            button_csvKopieren.Enabled = false;
            Button_Pausieren.Enabled = false;
        }

        private void Button_Pausieren_Click(object sender, EventArgs e)
        {
            Button_Pausieren.Enabled = false;

            // Button bisher nicht gedrückt?
            if (bButtonPruefungPause == false)
            {
                // Steuerelemente sperren
                Button_PruefungAbbrechen.Enabled = false;

                bButtonPruefungPause = true;    // Merker Button gedrückt
            }
            else
            {
                bButtonPruefungPause = false;   // Merker rücksetzen
            }
        }

        private void button_WerteAnTemperaturreglerÜbertragen_Click(object sender, EventArgs e)
        {
            // Temperaturvorgaben einstellen
            Temperaturregler_TemperaturEinstellen();

            // Fehler mit dem Temperaturregler aufgetreten?
            if (bErrorTemperaturregler == true)
            {
                MessageBox.Show("Kommunikation Temperaturregler:\n\n" + "Fehler beim Übertragen der Daten.");
            }
            else
            {
                button_ProbenAufheizen.Enabled = true;
            }
        }

        private void button_ProbenAufheizen_Click(object sender, EventArgs e)
        {
            // Aufheizen nicht aktiv?
            if (bTemperaturregelungAktiv == false)
            {
                // Masse der Heizpatronen zuschalten
                try
                {
                    bSetHeizpatronenMasseSchalten = true;
                    tcClient.WriteAny(hSetHeizpatronenMasseSchalten, bSetHeizpatronenMasseSchalten);
                }
                catch (Exception ex)
                {
                    bSetHeizpatronenMasseSchalten = false;
                    bErrorTwinCAT = true;

                    MessageBox.Show(ex.Message);
                }

                // Kein Fehler beim Zuschalten der Masse aufgetreten?
                if (bErrorTwinCAT == false)
                {
                    // Temperaturregelung starten
                    usModbusAdresse = 1214;                 // Adresse zum Starten/Stoppen der Regelung
                    usWertSchreibenTemperaturregler = 1;
                    Modbus_WriteFunction06();
                }
            }
            else
            {
                // Timer für die zyklische Abfrage im Idle Betrieb stoppen
                Timer_WartezeitTemperaturmessungIdlebetrieb.Enabled = false;

                while (bTimerTemperaturmessungAusgeloest == true)
                {
                    // Solange der Timer noch Aktiv arbeitet warten
                }

                // Regelung stoppen
                usModbusAdresse = 1214;                 // Adresse zum Starten/Stoppen der Regelung
                usWertSchreibenTemperaturregler = 0;
                Modbus_WriteFunction06();
            }

            // Wurde die Änderung übernommen?
            if (usWertSchreibenTemperaturregler == usDatenTemperaturregler)
            {
                // War die Regelung vor der Änderung nicht aktiv?
                if (bTemperaturregelungAktiv == false)
                {
                    bTemperaturregelungAktiv = true;            // Merker Temperaturregelung Aktiv

                    bButtonProbenAufheizenAktiv = true;
                    button_ProbenAufheizen.BackColor = Color.Green;

                    // Timer für die zyklische Abfrage der Temperatur im Idle Betrieb starten
                    Timer_WartezeitTemperaturmessungIdlebetrieb.Enabled = true;
                }
                else
                {
                    bTemperaturregelungAktiv = false;           // Merker Temperaturregelung nicht aktiv
                    bButtonProbenAufheizenAktiv = false;
                    button_ProbenAufheizen.BackColor = Color.Gray;
                }
            }
            else if (bErrorTemperaturregler == true) // Fehler bei der Kommunikation aufgetreten?
            {
                MessageBox.Show("Kommunikation Temperaturregler:\n\n" + "Fehler bei der Kommunikation.");

                // Heizpatronen abschalten
                try
                {
                    bSetHeizungFreigeben = false;
                    tcClient.WriteAny(hSetHeizpatronenFreigeben, bSetHeizungFreigeben);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                Fehlerbehandlung();
            }
            else
            {
                // Heizpatronen abschalten
                try
                {
                    bSetHeizungFreigeben = false;
                    tcClient.WriteAny(hSetHeizpatronenFreigeben, bSetHeizungFreigeben);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                // Timer für die zyklische Abfrage im Idle Betrieb stoppen
                Timer_WartezeitTemperaturmessungIdlebetrieb.Enabled = false;
                while (bTimerTemperaturmessungAusgeloest == true)
                {
                    // Solange der Timer noch Aktiv arbeitet warten
                }

                // Aufgabe einer Fehlermeldung
                if (bTemperaturregelungAktiv == false)
                {
                    MessageBox.Show("Kommunikation Temperaturregler:\n\n" + "Regelung konnte nicht gestartet werden.");
                }
                else
                {
                    MessageBox.Show("Kommunikation Temperaturregler:\n\n" + "Regelung konnte nicht gestoppt werden.");
                }
            }

            if (bErrorTwinCAT == true)
            {
                Fehlerbehandlung();
            }
        }

        private void numericUpDown_SollTemperaturOben_ValueChanged(object sender, EventArgs e)
        {
            if (bPruefungMitTemperatur == true)
            {
                // Temperaturwert übergeben
                numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben.Value = numericUpDown_SollTemperaturOben.Value;
            }
        }

        private void numericUpDown_SollTemperaturUnten_ValueChanged(object sender, EventArgs e)
        {
            if (bPruefungMitTemperatur == true)
            {
                // Temperaturwert übergeben
                numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten.Value = numericUpDown_SollTemperaturUnten.Value;
            }
        }

        private void numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown_SollTemperaturOben.Value = numericUpDown_TemperaturregelungSollTemperaturHeizpatroneOben.Value;
        }

        private void numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown_SollTemperaturUnten.Value = numericUpDown_TemperaturregelungSollTemperaturHeizpatroneUnten.Value;
        }

        private void button_ReibkraftMessen_Click(object sender, EventArgs e)
        {
            float fAktuelleReibkraft = 0;

            // Reibkraft messen
            API9206.GetValue(iKraftsensorHandle, ref fAktuelleReibkraft);   // Reibkraft als Float auslesen

            fAktuelleReibkraft = fAktuelleReibkraft - fTaraWertKraftsensor; // Tara abziehen

            label10.Text = fAktuelleReibkraft.ToString();
        }

        private void serialPort_Digitalanzeige_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }
    }
}
