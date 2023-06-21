using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Sample
{



    public class API9206
    {

        #region ENUM

        //---  ErrorStates | Fehlerzustände
        public enum ENErrors : int
        {
            ///<summary>No execution error occured | Kein Fehler bei der Ausführung</summary>
            FUNCTION_OK = 1,
            ///<summary>Execution error occured | Fehler bei der Ausführung aufgetreten</summary>
            FUNCTION_ERROR = -1,

            ///<summary>Invalid parameter value 1 | Ungültiger Übergabeparameter 1</summary>
            PARAM1_ERROR = -10,
            ///<summary>Invalid parameter value 2 | Ungültiger Übergabeparameter 1</summary>
            PARAM2_ERROR = -11,
            ///<summary>Invalid parameter value 3 | Ungültiger Übergabeparameter 1</summary>
            PARAM3_ERROR = -13,

            ///<summary>Main checksum error | Hauptprüfsummenfehler</summary>
            BCC_ERROR = -100,
            ///<summary>Message checksum error | Nachrichtenprüfsummenfehler</summary>
            BCC_M_ERROR = -101,

            ///<summary>Error while writing to COM port | Fehler beim Schreiben auf dem COM-Port</summary>
            WRITE_ERROR = -120,
            ///<summary>Error while reading from COM port | Fehler beim Lesen vom COM-Port</summary>
            READ_ERROR = -121,
            ///<summary>No response from device (timeout) | Keine Antwort vom Gerät (Timeout)</summary>
            TIMEOUT_ERROR = -122,
            ///<summary>Not all charactes could be sent to device | Nicht alle Zeichen wurden vollständig an das Gerät gesendet</summary>
            WRITE_COMPLETE_ERROR = -123,
            ///<summary>Error while reading characters from serial interface | Es konnten keine Zeichen über die serielle Schnittstelle empfangen werden</summary>
            RECEIVE_ERROR = -124,

            ///<summary>Invalid COM port (1-based) | Ungültiger COM-Port (1-basierend)</summary>
            PORT_NO_ERROR = -130,
            ///<summary>COM port could not be opened | COM-Port konnte nicht geöffnet werden</summary>
            INVALID_PORT_ERROR = -131,
            ///<summary>Could not retrieve COM port settings | COM-Port Einstellungen konnten nicht ermittelt werden</summary>
            GET_COM_STATE_ERROR = -132,
            ///<summary>Could not adjust COM port settings | COM-Port Einstellungen konnten nicht übernommen werden</summary>
            SET_COM_STATE_ERROR = -133,
        }

        //---   Date format | Datumsformat
        public enum ENDateFormat : int
        {
            DATE_FORMAT_0 = 0,				// YYYY-MM-DD
            DATE_FORMAT_1 = 1,				// YYYY/MM/DD
            DATE_FORMAT_2 = 2,				// DD.MM.YYYY
            DATE_FORMAT_3 = 3,				// DD-MM-YYYY
            DATE_FORMAT_4 = 4,				// DD/MM/YYYY
            DATE_FORMAT_5 = 5,				// MM-DD-YYYY
            DATE_FORMAT_6 = 6				// MM/DD/YYYY
        }

        //Measurement Modes | Messmodis
        public enum ENMeasurementModes
        {
            MEAS_MODE_DMS_15mV_DC = 0,
            MEAS_MODE_DMS_15mV_AC = 1,
            MEAS_MODE_DMS_30mV_DC = 2,
            MEAS_MODE_DMS_30mV_AC = 3,
            MEAS_MODE_DMS_250mV_DC = 4,
            MEAS_MODE_POTI_20V = 10,
            MEAS_MODE_U_20V = 20,
            MEAS_MODE_PT100_300_AC = 30,
            MEAS_MODE_PT100_600_AC = 31

        }

        //Measurement Datarates | Datenraten
        public enum ENMeasurementDataRates
        {
            DATARATE_10 = 10,
            DATARATE_16_6 = 17,
            DATARATE_50 = 50,
            DATARATE_60 = 60,
            DATARATE_400 = 400,
            DATARATE_1200 = 1200
        }

        #endregion ENUM



        #region Konstanten

        const string DLLName = "9206.dll";

        public const int SZSENSORTYP = 20 + 1;	        //Größe des Speichers für GeräteTyp +1 für Terminierungszeichen
        public const int SZUNIT = 10 + 1;	            //Größe des Speichers für Einheiten +1 für Terminierungszeichen
        public const int SZSERIALNO = 20 + 1;	        //Größe des Speichers für Seriennummer +1 für Terminierungszeichen
        public const int SZTUNEDATE = 20 + 1;	        //Größe des Speichers für Abgleichdatum +1 für Terminierungszeichen
        public const int SZSWVERSION = 20 + 1;	        //Größe des Speichers für Firmwareversionen +1 für Terminierungszeichen
        public const int SZDESCRIPTION = 220 + 1;	    //Größe des Speichers für Beschreibung +1 für Terminierungszeichen
        public const int SZDEVICENAME = 220 + 1;        //Größe des Speichers für Gerätename +1 für Terminierungszeichen
        public const int SZMANUFACTURERINFO = 100 + 1;           //Größe des Speichers für DLL-Herstellerinformationen +1 für Terminierungszeichen


        #endregion Konstanten



        #region DLL Imports

        //**************************************************************************
        //General interface functions | Allgemeine Schnittstellenfunktionen

        /// <summary>Setup interface settings | Einstellen der Schnittstellenparameter e.g. Baudrate, Databits, Parity, Stopbit | Konfigurieren der Schnittstelleneinstellung wie Baudrate, Datenbits, Parität, Stoppbit </summary>
        /// <param name="intBaudrate"></param>
        /// <param name="intDatabits"></param>
        /// <param name="intParity"></param>
        /// <param name="intStopbit"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetInterfaceConfiguration(int intBaudrate, int intDatabits, int intParity, int intStopbit);


        /// <summary>Set timeout in milliseconds (default = 1000 ms) | Timeoutzeit im Millisekunden einstellen (default = 1000 ms)
        /// 
        /// </summary>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetResponseTimeoutMs(int intValue);


        /// <summary> Open interface port and get port handle | Schnittstelle öffnen und Schnittstellen Handle zurück erhalten </summary>
        /// <param name="intPort"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int OpenInterface(int intPort);


        /// <summary>Close opened interface port | Geöffnete Schnittstelle wieder schliessen</summary>
        /// <param name="intPortHdl"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int CloseInterface(int intPortHdl);


        /// <summary>Setup receive and send buffer length | Empfangs- und Sendepuffer der Schnittstelle einstellen</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intInputLen"></param>
        /// <param name="intOutputLen"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetPortQueueLen(int intPort, int intInputLen, int intOutputLen);



        //**************************************************************************
        // Retrieval of DLL-information | Abfrage von DLL-Informationen

        /// <summary>Read DLL library software version | Software Version der DLL-Bibliothek auslesen</summary>
        /// <param name="strVersion"></param>
        [DllImport(DLLName)]
        public static extern void GetLibVersion(StringBuilder strVersion);


        /// <summary>Read DLL library creation date | Erstelldatum der DLL-Bibliothek auslesen</summary>
        /// <param name="strDate"></param>
        [DllImport(DLLName)]
        public static extern void GetLibCreationDate(StringBuilder strDate);


        /// <summary>Read DLL library creation date | Erstelldatum der DLL-Bibliothek auslesen</summary>
        /// <param name="strInfo"></param>
        [DllImport(DLLName)]
        public static extern void GetLibManufacturerInfo(StringBuilder strInfo);



        //**************************************************************************  
        // Read Device Information | Lesen von Geräteinformationen

        /// <summary>Read sensor Type | Sensor Typ auslesen</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="strInfo"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetSensorType(int intPortHdl, StringBuilder strInfo);

        /// <summary>Read Software Version | Software Version auslesen</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="strInfo"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetSoftwareVersion(int intPortHdl, StringBuilder strInfo);


        /// <summary>Read Serial No as Pointer to char array (max length = 20) | Seriennummer auslesen als Pointer auf char Array (maximal 20 Zeichen)</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="strInfo"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetSerialNr(int intPortHdl, StringBuilder strInfo);


        /// <summary>Read tune counter | Justierzähler auslesen / Abgleichszähler auslesen/summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetTuneCounter(int intPortHdl, ref int intValue);


        /// <summary>Read tune date as unix Time | Justierdatum als unix Zeit auslesen</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetTuneDateAsUnixTime(int intPortHdl, ref uint intValue);


        /// <summary>Read tune date as string | Justierdatum als string auslesen</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intFormat"></param>
        /// <param name="strInfo"></param>
        /// <returns></returns>
        // Read tune date as string | Justierdatum als string auslesen
        [DllImport(DLLName)]
        public static extern int GetTuneDateAsString(int intPortHdl, int intFormat, StringBuilder strInfo);



        //**************************************************************************  
        // Read / Write Deviceparameter | Lesen /Schreiben von Geräteparametern

        /// <summary>Read Description | Gerätebeschreibung auslesen</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="strInfo"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetDescription(int intPortHdl, StringBuilder strInfo);


        /// <summary>Write Description | Gerätebeschreibung schreiben</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="strInfo"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetDescription(int intPortHdl, StringBuilder strInfo);


        /// <summary>Read Device Name | Gerätename auslesen </summary>
        /// <param name="intPortHdl"></param>
        /// <param name="strInfo"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetDeviceName(int intPortHdl, StringBuilder strInfo);


        /// <summary>Write Device Name | Gerätename schreiben</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="strInfo"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetDeviceName(int intPortHdl, StringBuilder strInfo);


        /// <summary> Read special Software Options Byte 1| Spezielle Softwareeigenschaften auslesen Byte 1)</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetSpecialSoftwareOptions1(int intPortHdl, ref int intValue);


        /// <summary> Read special Software Options Byte 2| Spezielle Softwareeigenschaften auslesen Byte 2</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetSpecialSoftwareOptions2(int intPortHdl, ref int intValue);


        /// <summary> Read Number of Average | Anzahl der Mittelwerte aus dem Gerät lesen </summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetAverage(int intPortHdl, ref int intValue);


        /// <summary> Write number of average | Anzahl Mittelwerte in das Geraet schreiben. </summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetAverage(int intPortHdl, int intValue);


        /// <summary>Read number of decimals from device | Lesen der Anzahl Nachkommastellen aus dem Gerät </summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetDecimals(int intPortHdl, ref int intValue);


        /// <summary>Write number of decimals into device | Schreiben der Anzahl Nachkommastellen in das Gerät </summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetDecimals(int intPortHdl, int intValue);


        /// <summary>Read physical unit from device | Lesen der physikalischen Einheit aus dem Gerät </summary>
        /// <param name="intPortHdl"></param>
        /// <param name="strInfo"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetUnit(int intPortHdl, StringBuilder strInfo);


        /// <summary>Write physical unit into device | Schreiben der physikalischen Einheit in das Gerät </summary>
        /// <param name="intPortHdl"></param>
        /// <param name="strInfo"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetUnit(int intPortHdl, StringBuilder strInfo);


        /// <summary> Read Limit  1 | Grenzwert  1 auslesen </summary>
        /// <param name="intPortHdl"></param>
        /// <param name="fltValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetLimit1(int intPortHdl, ref float fltValue);


        /// <summary> Write Limit  1 | Grenzwert  1 schreiben </summary>
        /// <param name="intPortHdl"></param>
        /// <param name="fltValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetLimit1(int intPortHdl, float fltValue);


        /// <summary> Read Limit  2 | Grenzwert  2 auslesen </summary>
        /// <param name="intPortHdl"></param>
        /// <param name="fltValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetLimit2(int intPortHdl, ref float fltValue);


        /// <summary> Write Limit  2 | Grenzwert  2 schreiben </summary>
        /// <param name="intPortHdl"></param>
        /// <param name="fltValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetLimit2(int intPortHdl, float fltValue);


        /// <summary> Read Limit  3 | Grenzwert  3 auslesen </summary>
        /// <param name="intPortHdl"></param>
        /// <param name="fltValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetLimit3(int intPortHdl, ref float fltValue);


        /// <summary> Write Limit  3 | Grenzwert  3 schreiben </summary>
        /// <param name="intPortHdl"></param>
        /// <param name="fltValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetLimit3(int intPortHdl, float fltValue);


        /// <summary> Read Limit  4 | Grenzwert  4 auslesenn </summary>
        /// <param name="intPortHdl"></param>
        /// <param name="fltValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetLimit4(int intPortHdl, ref float fltValue);



        /// <summary> Write Limit  4 | Grenzwert  4 schreiben </summary>
        /// <param name="intPortHdl"></param>
        /// <param name="fltValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetLimit4(int intPortHdl, float fltValue);


        /// <summary> Read Type of Limit 1  | Typ des 1. Grenzwertes auslesen </summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetTypeOfLimit1(int intPortHdl, ref int intValue);


        /// <summary> Write Type of Limit 1  | Typ des 1. Grenzwertes schreiben</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetTypeOfLimit1(int intPortHdl, int intValue);


        /// <summary> Read Type of Limit 2  | Typ des 2. Grenzwertes auslesen</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetTypeOfLimit2(int intPortHdl, ref int intValue);


        /// <summary> Write Type of Limit 2  | Typ des 2. Grenzwertes schreiben</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetTypeOfLimit2(int intPortHdl, int intValue);


        /// <summary> Write Read Type of Limit 3  | Typ des 3. Grenzwertes auslesen</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetTypeOfLimit3(int intPortHdl, ref int intValue);


        /// <summary> Write Type of Limit 3  | Typ des 3. Grenzwertes schreiben</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetTypeOfLimit3(int intPortHdl, int intValue);


        /// <summary> Read Type of Limit 4  | Typ des 4. Grenzwertes auslesen</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetTypeOfLimit4(int intPortHdl, ref int intValue);


        /// <summary> Write Type of Limit 4  | Typ des 4. Grenzwertes schreiben</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetTypeOfLimit4(int intPortHdl, int intValue);



        /// <summary>Write data to Container A (decimal Place, mean value, limit 1 to 4, Type of Limit 1 to 4) | daten in Container A schreiben (Nachkommastellen, anzahl Mittelwerte, Grenzwert 1 bis 4, Typ der Grenzwerte 1 bis 4)</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intDecimalPlace"></param>
        /// <param name="intMeanValue"></param>
        /// <param name="limit1"></param>
        /// <param name="limit2"></param>
        /// <param name="limit3"></param>
        /// <param name="limit4"></param>
        /// <param name="typeOfLimit1"></param>
        /// <param name="typeOfLimit2"></param>
        /// <param name="typeOfLimit3"></param>
        /// <param name="typeOfLimit4"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetDataContainerA(int intPortHdl, int intDecimalPlace, int intMeanValue, float limit1, float limit2, float limit3, float limit4, int typeOfLimit1, int typeOfLimit2, int typeOfLimit3, int typeOfLimit4);


        /// <summary> Write data to Container B (Description) | daten in Container B schreiben (Beschreibung)</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="strInfo"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetDataContainerB(int intPortHdl, StringBuilder strInfo);


        /// <summary> Write data to Container C (Device Name) | daten in Container C schreiben (Geräte Name)</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="strInfo"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetDataContainerC(int intPortHdl, StringBuilder strInfo);


        /// <summary> Read the measurement mode | Messmodus auslesen</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intMeasurementMode"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetMeasurementMode(int intPortHdl, ref int intMeasurementMode);


        /// <summary> Write the measurement mode | Messmodus schreiben</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intMeasurementMode"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetMeasurementMode(int intPortHdl, int intMeasurementMode);


        /// <summary> Read the measurement datarate | Datenrate auslesen</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetMeasurementDatarate(int intPortHdl, ref int intValue);


        /// <summary> Write the measurement datarate | Datenrate schreiben</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetMeasurementDatarate(int intPortHdl, int intValue);


        /// <summary> Read the Excitation | Erregerspannung auslesen</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetExcitation(int intPortHdl, ref int intValue);


        /// <summary> Write the Excitation | Erregerspannung schreiben</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="intValue"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetExcitation(int intPortHdl, int intValue);


        /// <summary> Read the Pt100 Coefficients for temperature measurement | Pt100 koeffizienten für Temeraturmessung auslesen</summary>
        /// <param name="intPortHdl"></param>
        /// <param name="fltValue1"></param>
        /// <param name="fltValue2"></param>
        /// <param name="fltValue3"></param>
        /// <param name="fltValue4"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetPT100Coefficients(int intPortHdl, ref float fltValue1, ref float fltValue2, ref  float fltValue3, ref float fltValue4);


        /// <summary>Write the Pt100 Coefficients for temperature measurement | Pt100 koeffizienten für Temeraturmessung schreiben</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <param name="fltValue1">The FLT value1.</param>
        /// <param name="fltValue2">The FLT value2.</param>
        /// <param name="fltValue3">The FLT value3.</param>
        /// <param name="fltValue4">The FLT value4.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetPT100Coefficients(int intPortHdl, float fltValue1, float fltValue2, float fltValue3, float fltValue4);


        /// <summary>Read the milivolts value that has been teached in | liest den gepeicherten Spannungswert in mV</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <param name="fltValue">The FLT value.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetTeachInValueForSmallLoad(int intPortHdl, ref float fltValue);


        /// <summary>Set Teach In value for small Load | schreibt den Teach in valu für kleine</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int CallTeachInValueForSmallLoad(int intPortHdl);

        /// <summary>Set Teach In value for small Load | schreibt den Teach in valu für kleine</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int CallTeachInValueForSmallLoadWithParameter(int intPortHdl, float fltValue);


        /// <summary>Read the milivolts value that has been teached in | liest den gepeicherten Spannungswert in mV</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <param name="fltValue">The FLT value.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetTeachInValueForBigLoad(int intPortHdl, ref float fltValue);


        /// <summary>Read the milivolts value that has been teached in | liest den gepeicherten Spannungswert in mV</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int CallTeachInValueForBigLoad(int intPortHdl);

        /// <summary>Read the milivolts value that has been teached in | liest den gepeicherten Spannungswert in mV</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int CallTeachInValueForBigLoadWithParameter(int intPortHdl, float fltValue);


        /// <summary>Read the milivolts value that has been teached in | liest den gepeicherten Spannungswert in mV</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <param name="fltValue">The FLT value.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetReferenceValueForSmallLoad(int intPortHdl, ref float fltValue);


        /// <summary>Read the milivolts value that has been teached in | liest den gepeicherten Spannungswert in mV</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <param name="fltValue">The FLT value.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetReferenceValueForSmallLoad(int intPortHdl, float fltValue);


        /// <summary>Read the milivolts value that has been teached in | liest den gepeicherten Spannungswert in mV</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <param name="fltValue">The FLT value.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetReferenceValueForBigLoad(int intPortHdl, ref float fltValue);


        /// <summary>Read the milivolts value that has been teached in | liest den gepeicherten Spannungswert in mV</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <param name="fltValue">The FLT value.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int SetReferenceValueForBigLoad(int intPortHdl, float fltValue);


        /// <summary>Calculates User Calibration | Berechnet die Benutzer kalibrierung</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int CalculateUserCalibration(int intPortHdl);


        /// <summary>Write default user setup to EEPROM | Schreibt die Dafault User Daten in den EEPROM des Geräte</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int ResetDefaultUserData(int intPortHdl);


        /// <summary>Write default container data to EEPROM | Schreibt die Dafault Container Daten in den EEPROM des Geräte
        /// </summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int ResetDefaultContainerData(int intPortHdl);

        /// <summary>Idetifies the Device |  Idetifiziert das Gerät
        /// </summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int IdentifyChannelInDesktopDevice(int intPortHdl);


        /// <summary>Read current measurement value from device | Lesen des momentanen Messwertes aus dem Gerät</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <param name="fltValue">The FLT value.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetValue(int intPortHdl, ref float fltValue);


        /// <summary>Read current measurement value as string with unit from device | Lesen des momentanen Messwertes als String incl. Einheit aus dem Gerät</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <param name="strInfo">The STR info.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetValueString(int intPortHdl, StringBuilder strInfo);


        /// <summary>Change into special mode "Speed Optimized Polling Mode" | In Spezial Mode "Speed Optimized Polling Mode" wechseln</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GotoSpom(int intPortHdl);


        /// <summary>Read current measurement value in the "Speed Optimized Polling Mode" from device | Lesen des momentanen Messwertes im "Speed Optimized Polling Mode" aus dem Gerät</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <param name="fltValue">The FLT value.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetSpomValue(int intPortHdl, ref float fltValue);

        /// <summary>Read the communication Options from device | Liest die Kommunikations Optionen aus dem Gerät</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <param name="fltValue">The FLT value.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetComOptions(int intPortHdl, ref int Value);


        /// <summary>Read current measurement value in the "Speed Optimized Polling Mode" from device | Lesen des momentanen Messwertes im "Speed Optimized Polling Mode" aus dem Gerät</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <param name="fltValue">The FLT value.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetSpomSingleShotValue(int intPortHdl, ref float fltValue);



        //++++   bis hier bearbeitet

        /// <summary>Read current measurement value in the "Speed Optimized Polling Mode" from device as string | Lesen der momentanen Messwerte aus dem gerät als string</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <param name="chrValue">The CHR value.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int GetSpomValueString(int intPortHdl, StringBuilder[] chrValue);


        /// <summary>Quit "Speed Optimized Polling Mode" | "Speed Optimized Polling Mode" beenden</summary>
        /// <param name="intPortHdl">The int port HDL.</param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int StopSpom(int intPortHdl);

        /// <summary>Logger Start | Logger starten</summary>
        /// <param name="intHandle"></param>
        /// <returns></returns>
        [DllImport(DLLName)]
        public static extern int LoggerStart(StringBuilder strValue);


        #endregion


    }
}
