using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Data;
using ClassLibrary;


namespace OurClasses
{

    public class Decoder48
    {
        public DateTime timeOfDay { get; set; }

        public double latitude { get; set; }
        public double longitude { get; set; }

        public int SAC { get; set; }
        public int SIC { get; set; }

        public string FX { get; set; }

        string TYP { get; set; }
        string SIM { get; set; }
        string RDP { get; set; }
        string SPI { get; set; }
        string RAB { get; set; }
        string TST { get; set; }
        string ERR { get; set; }
        string XPP { get; set; }
        string ME { get; set; }
        string MI { get; set; }
        string FOE_FRI { get; set; }
        string ADSB_EP { get; set; }
        string ADSB_VAL { get; set; }
        string SCN_EP { get; set; }
        string SCN_VAL { get; set; }
        string PAI_EP { get; set; }
        string PAI_VAL { get; set; }
        //string? SPARE { get; set; }

        public string V { get; set; }
        public string G { get; set; }
        public string L { get; set; }
        public string mode3A { get; set; }

        public string V2 { get; set; }
        public string G2 { get; set; }
        public double FL { get; set; }

        public string SRL { get; set; }
        public string SSR { get; set; }
        public string SAM { get; set; }
        public string PRL { get; set; }
        public string PAM { get; set; }
        public string RPD { get; set; }
        public string APD { get; set; }
        public double SRL2 { get; set; }
        public int SRR2 { get; set; }
        public double SAM2 { get; set; }
        public double PRL2 { get; set; }
        public double PAM2 { get; set; }
        public double RPD2 { get; set; }
        public double APD2 { get; set; }

        public List<int> fullModeS { get; set; }//mirar-ho bé

        public int MCPstatus { get; set; }
        public double MCPalt { get; set; }
        public int FMstatus { get; set; }
        public double FMalt { get; set; }
        public int BPstatus { get; set; }
        public double BPpres { get; set; }
        public int modeStat { get; set; }
        public int VNAV { get; set; }
        public int ALThold { get; set; }
        public int App { get; set; } //approach
        public int targetalt_status { get; set; }
        public string targetalt_source { get; set; }

        public int RAstatus { get; set; }
        public double RA { get; set; } //roll angle
        public int TTAstatus { get; set; }
        public double TTA { get; set; } //true track angle
        public int GSstatus { get; set; }
        public double GS { get; set; } //ground speed
        public int TARstatus { get; set; }
        public double TAR { get; set; } //track angle rate
        public int TASstatus { get; set; }
        public double TAS { get; set; } //true airspeed

        public int HDGstatus { get; set; }
        public double HDG { get; set; }
        public int IASstatus { get; set; }
        public double IAS { get; set; }
        public int MACHstatus { get; set; }
        public double MACH { get; set; }
        public int BARstatus { get; set; }
        public double BAR { get; set; }
        public int IVVstatus { get; set; }
        public double IVV { get; set; }

        public int tracknumber { get; set; }

        public double x_component { get; set; }
        public double y_component { get; set; }

        public double rho { get; set; }
        public double theta { get; set; }

        public double groundspeedpolar;
        public double headingpolar;

        public string CNF { get; set; }
        public string RAD { get; set; }
        public string DOU { get; set; }
        public string MAH { get; set; }
        public string CDM { get; set; }
        public string TRE { get; set; }
        public string GHO { get; set; }
        public string SUP { get; set; }
        public string TCC { get; set; }

        public double measuredheight { get; set; }

        public string COM { get; set; }
        public string STAT { get; set; }
        public string SI { get; set; }
        public string MSCC { get; set; }
        public string ARC { get; set; }
        public string AIC { get; set; }
        public string B1A { get; set; }
        public string B1B { get; set; }

        public Coordinates RadarCoordinates { get; private set; }
        public GeoUtils GeoUtils { get; private set; } = new GeoUtils();


        //DECODE 140
        //--------------------------------------------------------------
        public TimeSpan decodeTimeDay(byte[] data)
        {
            double resolution = Math.Pow(2, -7); //in seconds
            double seconds = ByteToDoubleDecoding(data, resolution);
            TimeSpan TimeofDay = TimeSpan.FromSeconds(seconds);
            return TimeofDay;
        }
        //--------------------------------------------------------------
        //GENERAL FUNCTION
        //---------------------------------------------------------------
        private double ByteToDoubleDecoding(byte[] data, double resolution)
        {
            double bytesValue = 0;

            for (int i = 0; i < data.Length; i++)
            {
                bytesValue *= 256; // Multiplicar por 2^8, ya que un byte tiene 8 bits
                bytesValue += data[i];
            }

            return bytesValue * resolution;
        }
        //---------------------------------------------------------------
        
        public double latitudeDecoding(double cooR, double cooTheta, double FL)
        {
            int Rt = 6370000;
            double Fl = 0;
            if (FL > 0)
            {
                Fl = (FL * 100) * GeoUtils.FEET2METERS;
            }
            double hr = 27.257; //m
            double El = Math.Asin((2 * Rt * (Fl - hr) + Math.Pow(Fl, 2) - Math.Pow(hr, 2) - Math.Pow(cooR * GeoUtils.NM2METERS, 2)) / (2 * cooR * GeoUtils.NM2METERS * (Rt + hr)));
            CoordinatesPolar objectPolar = new CoordinatesPolar(cooR * GeoUtils.NM2METERS, cooTheta * GeoUtils.DEGS2RADS, El);
            CoordinatesWGS84 radarWGS84 = new CoordinatesWGS84(GeoUtils.LatLon2Degrees(41, 18, 2.5284, 0) * (Math.PI / 180.0), GeoUtils.LatLon2Degrees(2, 6, 7.4095, 0) * (Math.PI / 180.0), 27.257);

            GeoUtils geoUtils = new GeoUtils();

            CoordinatesXYZ objectRadarCart = geoUtils.change_radar_spherical2radar_cartesian(objectPolar);
            CoordinatesXYZ objectGeocentric = geoUtils.change_radar_cartesian2geocentric(radarWGS84, objectRadarCart);
            CoordinatesWGS84 geodesicWGS84 = geoUtils.change_geocentric2geodesic(objectGeocentric);

            this.latitude = geodesicWGS84.Lat * (180.0 / Math.PI);
            return this.latitude;
        }
        public double longitudeDecoding(double cooR, double cooTheta, double FL)
        {
            int Rt = 6370000;
            double Fl = 0;
            if (FL > 0)
            {
                Fl = (FL * 100) * GeoUtils.FEET2METERS;
            }
            double hr = 27.257; //m
            double El = Math.Asin((2 * Rt * (Fl - hr) + Math.Pow(Fl, 2) - Math.Pow(hr, 2) - Math.Pow(cooR * GeoUtils.NM2METERS, 2)) / (2 * cooR * GeoUtils.NM2METERS * (Rt + hr)));
            CoordinatesPolar objectPolar = new CoordinatesPolar(cooR * GeoUtils.NM2METERS, cooTheta * GeoUtils.DEGS2RADS, El);
            CoordinatesWGS84 radarWGS84 = new CoordinatesWGS84(GeoUtils.LatLon2Degrees(41, 18, 2.5284, 0) * (Math.PI / 180.0), GeoUtils.LatLon2Degrees(2, 6, 7.4095, 0) * (Math.PI / 180.0), 27.257);

            GeoUtils geoUtils = new GeoUtils();

            CoordinatesXYZ objectRadarCart = geoUtils.change_radar_spherical2radar_cartesian(objectPolar);
            CoordinatesXYZ objectGeocentric = geoUtils.change_radar_cartesian2geocentric(radarWGS84, objectRadarCart);
            CoordinatesWGS84 geodesicWGS84 = geoUtils.change_geocentric2geodesic(objectGeocentric);

            this.longitude = geodesicWGS84.Lon * (180.0 / Math.PI);
            return this.longitude;
        }
        

        //DECODE 020
        //--------------------------------------------------------------
        public string TYPDecoding(byte[] data)
        {
            int typ = (data[0] >> 5) & 0b00000111; //obtenemos los tres primeros bits del array de data
            Dictionary<int, string> typDescriptions = new Dictionary<int, string>() //mapeamos el valor de typ
        {
            { 0, "No detection" },
            { 1, "PSR" },
            { 2, "SSR" },
            { 3, "SSR + PSR" },
            { 4, "ModeS All-Cal" },
            { 5, "ModeS Roll-Call" },
            { 6, "ModeS All-Call + PSR" },
            { 7, "ModeS Roll-Call + PSR" }
        };

            return typDescriptions.ContainsKey(typ) ? typDescriptions[typ] : ""; //si no se encuentra, devuelve cadena vacía 
        }

        public string SIMDecoding(byte[] data)
        {

            int sim = (data[0] >> 4) & 0b00000001; //cuarto bit del array de data 00010000
            Dictionary<int, string> simDescriptions = new Dictionary<int, string>()
    {
        { 0, "Actual target report" },
        { 1, "Simulated target report" }
    };

            return simDescriptions.ContainsKey(sim) ? simDescriptions[sim] : "";
        }

        public string RDPDecoding(byte[] data)
        {

            int rdp = (data[0] >> 3) & 0b00000001;
            Dictionary<int, string> rdpDescriptions = new Dictionary<int, string>()
        {
        { 0, "Report from RDP Chain 1" },
        { 1, "Report from RDP Chain 2" }
        };
            return rdpDescriptions.ContainsKey(rdp) ? rdpDescriptions[rdp] : "";
        }

        public string SPIDecoding(byte[] data)
        {

            int spi = (data[0] >> 2) & 0b00000001;
            Dictionary<int, string> spiDescriptions = new Dictionary<int, string>()
        {
        { 0, "Absence of SPI" },
        { 1, "Special Position Identification" }
        };

            return spiDescriptions.ContainsKey(spi) ? spiDescriptions[spi] : "";
        }

        public string RABDecoding(byte[] data)
        {

            int rab = (data[0] >> 1) & 0b00000001;
            Dictionary<int, string> rabDescriptions = new Dictionary<int, string>()
        {
        { 0, "Report from aircraft transponder" },
        { 1, "Report from field monitor (fixed transponder)" }
        };

            return rabDescriptions.ContainsKey(rab) ? rabDescriptions[rab] : "";
        }

        public string TSTDecoding(byte[] data)//es un campo opcional
        {
            int tst = (data[1] >> 7) & 0b00000001;
            Dictionary<int, string> tstDescriptions = new Dictionary<int, string>()
        {
        { 0, "Real target report" },
        { 1, "Test target report" }
        };

            return tstDescriptions.ContainsKey(tst) ? tstDescriptions[tst] : "";
        }
        public string ERRDecoding(byte[] data)//es un campo opcional
        {
            int err = (data[1] >> 6) & 0b00000001;
            Dictionary<int, string> errDescriptions = new Dictionary<int, string>()
        {
        { 0, "No Extended Range" },
        { 1, "Extended Range present" }
        };

            return errDescriptions.ContainsKey(err) ? errDescriptions[err] : "";
        }
        public string XPPDecoding(byte[] data)//es un campo opcional
        {
            int xpp = (data[1] >> 5) & 0b00000001;
            Dictionary<int, string> xppDescriptions = new Dictionary<int, string>()
        {
        { 0, "No X-Pulse present" },
        { 1, "X-Pulse present" }
        };

            return xppDescriptions.ContainsKey(xpp) ? xppDescriptions[xpp] : "";
        }
        public string MEDecoding(byte[] data)//es un campo opcional
        {
            int me = (data[1] >> 4) & 0b00000001;
            Dictionary<int, string> meDescriptions = new Dictionary<int, string>()
        {
        { 0, "No military emergency" },
        { 1, "Military emergency" }
        };

            return meDescriptions.ContainsKey(me) ? meDescriptions[me] : "";

        }
        public string MIDecoding(byte[] data)//es un campo opcional
        {
            int mi = (data[1] >> 3) & 0b00000001;
            Dictionary<int, string> miDescriptions = new Dictionary<int, string>()
        {
        { 0, "No military identification" },
        { 1, "Military identification" }
        };

            return miDescriptions.ContainsKey(mi) ? miDescriptions[mi] : "";
        }
        public string FOE_FRIDecoding(byte[] data)//es un campo opcional
        {
            int foe_fri = (data[1] >> 1) & 0b00000011;
            Dictionary<int, string> foe_friDescriptions = new Dictionary<int, string>()
        {
            { 0, "No Mode 4 interrogation" },
            { 1, "Friendly target" },
            { 2, "Unknown target" },
            { 3, "No reply" }
        };
            return foe_friDescriptions.ContainsKey(foe_fri) ? foe_friDescriptions[foe_fri] : "";
        }
        public string ADSB_EPDecoding(byte[] data)
        {

            int adsb_ep = (data[2] >> 7) & 0b00000001;
            Dictionary<int, string> adsb_epDescriptions = new Dictionary<int, string>()
    {
        { 0, "ADSB not populated" },
        { 1, "ADSB populated" }
    };
            return adsb_epDescriptions.ContainsKey(adsb_ep) ? adsb_epDescriptions[adsb_ep] : "";

        }
        public string ADSB_VALDecoding(byte[] data)
        {

            int adsb_val = (data[2] >> 6) & 0b00000001;
            Dictionary<int, string> adsb_valDescriptions = new Dictionary<int, string>()
    {
        { 0, "not available" },
        { 1, "available" }
    };
            return adsb_valDescriptions.ContainsKey(adsb_val) ? adsb_valDescriptions[adsb_val] : "";

        }
        public string SCN_EPDecoding(byte[] data)
        {

            int scn_ep = (data[2] >> 5) & 0b00000001;
            Dictionary<int, string> scn_epDescriptions = new Dictionary<int, string>()
    {
        { 0, "SCN not populated" },
        { 1, "SCN populated" }
    };
            return scn_epDescriptions.ContainsKey(scn_ep) ? scn_epDescriptions[scn_ep] : "";

        }
        public string SCN_VALDecoding(byte[] data)
        {

            int scn_val = (data[2] >> 4) & 0b00000001;
            Dictionary<int, string> scn_valDescriptions = new Dictionary<int, string>()
    {
        { 0, "not available" },
        { 1, "available" }
    };
            return scn_valDescriptions.ContainsKey(scn_val) ? scn_valDescriptions[scn_val] : "";

        }
        public string PAI_EPDecoding(byte[] data)
        {

            int pai_ep = (data[2] >> 3) & 0b00000001;
            Dictionary<int, string> pai_epDescriptions = new Dictionary<int, string>()
    {
        { 0, "PAI not populated" },
        { 1, "PAI populated" }
    };
            return pai_epDescriptions.ContainsKey(pai_ep) ? pai_epDescriptions[pai_ep] : "";

        }
        public string PAI_VALDecoding(byte[] data)
        {

            int pai_val = (data[2] >> 2) & 0b00000001;
            Dictionary<int, string> pai_valDescriptions = new Dictionary<int, string>()
    {
        { 0, "not available" },
        { 1, "available" }
    };
            return pai_valDescriptions.ContainsKey(pai_val) ? pai_valDescriptions[pai_val] : "";

        }

        public List<string> decode020(List<byte> data_list)
        {
            byte[] data = data_list.ToArray();
            List<string> decodedData = new List<string>();
            string TYP = TYPDecoding(data);
            string SIM = SIMDecoding(data);
            string RDP = RDPDecoding(data);
            string SPI = SPIDecoding(data);
            string RAB = RABDecoding(data);
            decodedData.Add(TYP);
            decodedData.Add(SIM);
            decodedData.Add(RDP);
            decodedData.Add(SPI);
            decodedData.Add(RAB);
            if (data.Length > 1)
            {
                string TST = TSTDecoding(data);
                string ERR = ERRDecoding(data);
                string XPP = XPPDecoding(data);
                string ME = MEDecoding(data);
                string MI = MIDecoding(data);
                string FOE_FRI = FOE_FRIDecoding(data);
                decodedData.Add(TST);
                decodedData.Add(ERR);
                decodedData.Add(XPP);
                decodedData.Add(ME);
                decodedData.Add(MI);
                decodedData.Add(FOE_FRI);
                if (data.Length > 2)
                {
                    string ADSB_EP = ADSB_EPDecoding(data);
                    string ADSB_VAL = ADSB_VALDecoding(data);
                    string SCN_EP = SCN_EPDecoding(data);
                    string SCN_VAL = SCN_VALDecoding(data);
                    string PAI_EP = PAI_EPDecoding(data);
                    string PAI_VAL = PAI_VALDecoding(data);
                    decodedData.Add(ADSB_EP);
                    decodedData.Add(ADSB_VAL);
                    decodedData.Add(SCN_EP);
                    decodedData.Add(SCN_VAL);
                    decodedData.Add(PAI_EP);
                    decodedData.Add(PAI_VAL);
                }
            }
            return decodedData;
        }
        //--------------------------------------------------------------

        //DECODE 070
        //-----------------------------------------------------------------
        //ahora vamos a decodificara los parámetros de Mode-3A, en representación octal

        public string VDecoding(byte[] data)
        {

            int v = (data[0] >> 7) & 0b00000001;
            Dictionary<int, string> vDescriptions = new Dictionary<int, string>()
    {
        { 0, "Code validated" },
        { 1, "Code not validated" }
    };
            return vDescriptions.ContainsKey(v) ? vDescriptions[v] : "";

        }
        public string GDecoding(byte[] data)
        {

            int g = (data[0] >> 6) & 0b00000001;
            Dictionary<int, string> gDescriptions = new Dictionary<int, string>()
    {
        { 0, "Default" },
        { 1, "Garbled code" }
    };
            return gDescriptions.ContainsKey(g) ? gDescriptions[g] : "";

        }
        public string LDecoding(byte[] data)
        {

            int l = (data[0] >> 5) & 0b00000001;
            Dictionary<int, string> lDescriptions = new Dictionary<int, string>()
    {
        { 0, "Mode-3/A code derived from the reply of the transponder" },
        { 1, "Mode-3/A code not extracted during the last scan" }
    };
            return lDescriptions.ContainsKey(l) ? lDescriptions[l] : "";

        }
        public string mode3ADecoding(byte[] dataItem)
        {
            // Aquí extraemos los bits A, B, C y D del modo 3A
            int A = (dataItem[0] >> 1) & 0b00000111;
            int B = ((dataItem[0] & 0b00000001) << 2) | ((dataItem[1] >> 6) & 0b00000011);
            int C = (dataItem[1] >> 3) & 0b00000111;
            int D = dataItem[1] & 0b00000111;
            int mode3A = A * 1000 + B * 100 + C * 10 + D;
            // Queremos que el resultado tenga al menos 3 dígitos
            string mode3AFormatted = mode3A.ToString();
            return mode3AFormatted;
        }

        public (string, string, string, string) decode070(byte[] data)
        {
            string V = VDecoding(data);
            string G = GDecoding(data);
            string L = LDecoding(data);
            string mode3A = mode3ADecoding(data);
            return (V, G, L, mode3A);
        }
        //---------------------------------------------------------------

        //DECODE 090
        //---------------------------------------------------------------
        //Ahora vamos a decodificar el flight level (binary representation)
        public string V2Decoding(byte[] data)
        {
            int V2 = (data[0] >> 7) & 0b00000001;
            Dictionary<int, string> V2Descriptions = new Dictionary<int, string>()
    {
        { 0, "Code validated" },
        { 1, "Code not validated" }
    };

            return V2Descriptions.ContainsKey(V2) ? V2Descriptions[V2] : "";
        }
        public string G2Decoding(byte[] data)
        {
            int G2 = (data[0] >> 6) & 0b00000001;
            Dictionary<int, string> G2Descriptions = new Dictionary<int, string>()
        {
            { 0, " Default" },
            { 1, " Garbled code" }
        };

            return G2Descriptions.ContainsKey(G2) ? G2Descriptions[G2] : "";
        }
        public double FLDecoding(byte[] data)
        {
            byte firstByte = data[0];
            byte secondByte = data[1];

            int FLBits = (firstByte & 0b00111111) << 8 | secondByte;
            bool isNegative = (firstByte & 0b00100000) != 0;
            double resolutionLSB = 1.0 / 4.0; // Resolución de FL

            if (isNegative)
            {
                FLBits = (~FLBits + 1) & 0x3FFF;
                FLBits *= -1;
            }

            double flightLevel = FLBits * resolutionLSB;
            this.FL = flightLevel;
            return this.FL;

        }

        public (string, string, double) decode090(byte[] data)
        {
            string V = V2Decoding(data);
            string G = G2Decoding(data);
            double FL = FLDecoding(data);
            return (V, G, FL);
        }
        //---------------------------------------------------------------

        //DECODE 130
        //---------------------------------------------------------------
        //Ahora se decodificará Radar Plot Characteristics
        public string SRLDecoding(byte[] data)
        {
            int srl = (data[0] >> 7) & 0b00000001;
            Dictionary<int, string> srlDescriptions = new Dictionary<int, string>()
        {
            { 0, "Absence of Subfield #1: SSR plot runlength" },
            { 1, "Presence of Subfield #1: SSR plot runlength" }
        };

            return srlDescriptions.ContainsKey(srl) ? srlDescriptions[srl] : "";
        }
        public double SRL2Decoding(byte data)
        {
            double resolution = 360.0 / Math.Pow(2, 13); //en dgs
            byte[] data_array = new byte[1];
            data_array[0] = data;
            this.SRL2 = ByteToDoubleDecoding(data_array, resolution);
            return this.SRL2;
        }
        public string SSRDecoding(byte[] data)
        {
            int ssr = (data[0] >> 6) & 0b00000001;
            Dictionary<int, string> ssrDescriptions = new Dictionary<int, string>()
        {
            { 0, "Absence of Subfield #2: Number of received replies for M(SSR)" },
            { 1, "Presence of Subfield #2: Number of received replies for M(SSR)\" " }
        };

            return ssrDescriptions.ContainsKey(ssr) ? ssrDescriptions[ssr] : "";
        }
        public double SSR2Decoding(byte data)
        {
            double resolution = 1; //no tiene unidades
            byte[] data_array = new byte[1];
            data_array[0] = data;
            double SSR2 = ByteToDoubleDecoding(data_array, resolution);
            return SSR2;
        }
        public string SAMDecoding(byte[] data)
        {
            int sam = (data[0] >> 5) & 0b00000001;
            Dictionary<int, string> samDescriptions = new Dictionary<int, string>()
        {
            { 0, "Absence of Subfield #3: Amplitude of received replies for M(SSR)" },
            { 1, "Presence of Subfield #3: Amplitude of received replies for M(SSR)" }
        };
            return samDescriptions.ContainsKey(sam) ? samDescriptions[sam] : "";
        }
        public double SAM2Decoding(byte data)
        {
            double resolution = 1; // dBm
            int data_int = new int();
            if ((data & 128) != 0)
            {
                // Conversión de dos complementos para valores negativos
                data_int = (~data + 1) & 0x7F;
                data_int *= -1;
            }
            this.SAM2 = data_int * resolution;
            return this.SAM2;
        }
        public string PRLDecoding(byte[] data)
        {
            int prl = (data[0] >> 4) & 0b00000001;
            Dictionary<int, string> prlDescriptions = new Dictionary<int, string>()
        {
            { 0, "Absence of Subfield #4: PSR plot runlength" },
            { 1, "Presence of Subfield #4: PSR plot runlength" }
        };
            return prlDescriptions.ContainsKey(prl) ? prlDescriptions[prl] : "";
        }
        public double PRL2Decoding(byte data)
        {
            double resolution = 360.0 / Math.Pow(2, 13); //en dgs
            byte[] data_array = new byte[1];
            data_array[0] = data;
            this.PRL2 = ByteToDoubleDecoding(data_array, resolution);
            return this.PRL2;
        }
        public string PAMDecoding(byte[] data)
        {
            int pam = (data[0] >> 3) & 0b00000001;
            Dictionary<int, string> pamDescriptions = new Dictionary<int, string>()
        {
            { 0, "Absence of Subfield #5: PSR amplitude" },
            { 1, "Presence of Subfield #5: PSR amplitude" }
        };
            return pamDescriptions.ContainsKey(pam) ? pamDescriptions[pam] : "";
        }
        public double PAM2Decoding(byte data)
        {
            double resolution = 1; // dBm
            int data_int = new int();
            if ((data & 128) != 0)
            {
                // Hacemos la conversión de dos complementos para valores negativos
                data_int = ~(data - 1);
                data_int *= -1;
            }
            else { data_int = data; }
            this.PAM2 = data_int * resolution;
            return this.PAM2;
        }
        public string RPDDecoding(byte[] data)
        {
            int rpd = (data[0] >> 2) & 0b00000001;
            Dictionary<int, string> rpdDescriptions = new Dictionary<int, string>()
        {
            { 0, "Absence of Subfield #6: Difference in Range between PSR and SSR plot" },
            { 1, "Presence of Subfield #6: Difference in Range between PSR and SSR plot" }
        };
            return rpdDescriptions.ContainsKey(rpd) ? rpdDescriptions[rpd] : "";
        }
        public double RPD2Decoding(byte data)
        {
            double resolution = 1.0 / 256; // NM
            double data_int = 0;
            if ((data & 128) != 0)
            {
                // Hacemos la conversión de dos complementos para valores negativos
                data_int = ~(data - 1);
                data_int *= -1;
            }
            else { data_int = data; }
            this.RPD2 = data_int * resolution;
            return this.RPD2;
        }
        public string APDDecoding(byte[] data)
        {
            int apd = (data[0] >> 1) & 0b00000001;
            Dictionary<int, string> apdDescriptions = new Dictionary<int, string>()
        {
            { 0, "Absence of Subfield #7: Difference in Azimuth between PSR and SSR plot" },
            { 1, "Presence of Subfield #7: Difference in Azimuth between PSR and SSR plot" }
        };
            return apdDescriptions.ContainsKey(apd) ? apdDescriptions[apd] : "";
        }
        public double APD2Decoding(byte data)
        {
            double resolution = 360.0 / Math.Pow(2, 14); // dgs
            double data_int = 0;
            if ((data & 128) != 0)
            {
                // Hacemos la conversión de dos complementos para valores negativos
                data_int = (~data + 1) & 0x7F;
                data_int *= -1;
            }
            else { data_int = data; }
            this.APD2 = data_int * resolution;
            return this.APD2;
        }

        public List<double> decode130(byte[] data)
        {
            List<double> decodedData = new List<double>();
            int byteIndex = 1;

            byte resultado = 0;
            for (int i = 0; i < 8; i++)
            {
                resultado |= (byte)(((data[0] >> i) & 0b00000001) << (7 - i));
            }

            for (int i = 1; i < 8; i++)
            {
                if (((resultado >> i - 1) & 0b00000001) == 1)
                {
                    switch (i)
                    {
                        case 7:
                            double APD = APD2Decoding(data[byteIndex]);
                            decodedData.Add(APD);
                            break;
                        case 6:
                            double RPD = RPD2Decoding(data[byteIndex]);
                            decodedData.Add(RPD);
                            break;
                        case 5:
                            double PAM = PAM2Decoding(data[byteIndex]);
                            decodedData.Add(PAM);
                            break;
                        case 4:
                            double PRL = PRL2Decoding(data[byteIndex]);
                            decodedData.Add(PRL);
                            break;
                        case 3:
                            double SAM = SAM2Decoding(data[byteIndex]);
                            decodedData.Add(SAM);
                            break;
                        case 2:
                            double SSR = SSR2Decoding(data[byteIndex]);
                            decodedData.Add(SSR);
                            break;
                        case 1:
                            double SRL = SRL2Decoding(data[byteIndex]);
                            decodedData.Add(SRL);
                            break;
                    }
                    byteIndex += 1;
                }
            }
            return decodedData;
        }
        //---------------------------------------------------------------

        //DECODE 250
        //---------------------------------------------------------------
        public int[] fullModeSDecoding(byte[] data)
        {
            int bdsByte = data[7];

            int bds1 = (bdsByte & 0xF0) >> 4;
            int bds2 = bdsByte & 0x0F;

            return new int[] { bds1, bds2 };
        }
        //Mode 4.0
        public int MCPStatusDecoding(byte[] data)
        {
            int mcpstatus = (data[0] >> 7) & 0b00000001;
            this.MCPstatus = mcpstatus;
            return this.MCPstatus;
        }
        public double MCPaltDecoding(byte[] data)
        {
            byte mcpalt1 = (byte)((data[0] >> 0) & 0b01111111);
            byte mcpalt2 = (byte)((data[1] >> 3) & 0b00011111);

            int combinedValue = (mcpalt1 << 5) | mcpalt2;

            double resolution = 16; // Resolución en ft
            double mcpAltitude = combinedValue * resolution;
            this.MCPalt = mcpAltitude;
            return mcpAltitude;
        }
        public int FMstatusDecoding(byte[] data)
        {
            int fmstatus = (data[1] >> 2) & 0b00000001;
            this.FMstatus = fmstatus;
            return this.FMstatus;

        }
        public double FMaltDecoding(byte[] data)
        {
            byte fmalt1 = (byte)((data[1] >> 0) & 0b00000011);
            byte fmalt2 = (byte)((data[2] >> 0) & 0b11111111);
            byte fmalt3 = (byte)((data[3] >> 6) & 0b00000011);

            int combinedValue = (fmalt1 << 10) | (fmalt2 << 2) | fmalt3;

            double resolution = 16; // Resolución en ft
            double fmAltitude = combinedValue * resolution;
            this.FMalt = fmAltitude;
            return fmAltitude;

        }
        public int BPstatusDecoding(byte[] data)
        {
            int bpstatus = (data[3] >> 5) & 0b00000001;
            this.BPstatus = bpstatus;
            return this.BPstatus;
        }
        public double BPpresDecoding(byte[] data)
        {
            byte bppres1 = (byte)((data[3] >> 0) & 0b00011111);
            byte bppres2 = (byte)((data[4] >> 1) & 0b01111111);

            int combinedValue = (bppres1 << 7) | bppres2;

            double resolution = 0.1; // Resolución en mb
            double bppres = combinedValue * resolution;
            this.BPpres = bppres + 800;
            return this.BPpres;

        }
        public int modeStatDecoding(byte[] data)
        {
            int modeStat = (data[5] >> 0) & 0b00000001;
            this.modeStat = modeStat;
            return this.modeStat;
        }
        public int VNAVDecoding(byte[] data)
        {
            int vnav = (data[6] >> 7) & 0b00000001;
            this.VNAV = vnav;
            return this.VNAV;
        }
        public int ALTholdDecoding(byte[] data)
        {
            int ALThold = (data[6] >> 6) & 0b00000001;
            this.ALThold = ALThold;
            return this.ALThold;
        }
        public int AppDecoding(byte[] data)
        {
            int App = (data[6] >> 5) & 0b00000001;
            this.App = App;
            return this.App;
        }
        public int targetalt_statusDecoding(byte[] data)
        {
            int targetalt_status = (data[6] >> 2) & 0b00000001;
            this.targetalt_status = targetalt_status;
            return this.targetalt_status;
        }
        public string targetalt_sourceDecoding(byte[] data)
        {
            int targetalt_source = (data[6] >> 0) & 0b00000011;
            Dictionary<int, string> targetalt_sourceDescriptions = new Dictionary<int, string>()
        {
            { 0, "Unknown" },
            { 1, "Aircraft altitude" },
            { 2, "FCU/MCP selected altitude" },
            { 3, "FMS selected altitude" }
        };
            return targetalt_sourceDescriptions.ContainsKey(targetalt_source) ? targetalt_sourceDescriptions[targetalt_source] : "";
        }

        //Mode 5.0
        public int RAstatusDecoding(byte[] data)
        {
            int RAstatus = (data[0] >> 7) & 0b00000001;
            this.RAstatus = RAstatus;
            return this.RAstatus;
        }
        public double RADecoding(byte[] data) //valores negativos?
        {
            byte RA1 = (byte)((data[0] >> 0) & 0b01111111);
            byte RA2 = (byte)((data[1] >> 5) & 0b00000111);

            // Determinar el signo
            byte signByte = (byte)((data[0] >> 6) & 0b00000001);
            int combinedValue;
            if (signByte == 0)
            {
                // Valor positivo
                combinedValue = (RA1 << 3) | RA2;
            }
            else
            {
                // Valor negativo
                combinedValue = ((RA1 << 3) | RA2);
                combinedValue = ((~combinedValue) + 1) & 0x1FF;
                combinedValue *= -1;
            }
            double resolution = 45.0 / 256.0; //resolución en grados
            double RA = combinedValue * resolution;
            this.RA = RA;
            return this.RA;
        }
        public int TTAstatusDecoding(byte[] data)
        {
            int TTAstatus = (data[1] >> 4) & 0b00000001;
            this.TTAstatus = TTAstatus;
            return this.TTAstatus;
        }
        public double TTADecoding(byte[] data)
        {
            byte TTA1 = (byte)((data[1] >> 0) & 0b00001111);
            byte TTA2 = (byte)((data[2] >> 1) & 0b01111111);
            byte signByte = (byte)((data[1] >> 3) & 0b00000001);

            int combinedValue;
            if (signByte == 0)
            {
                // Valor positivo
                combinedValue = (TTA1 << 7) | TTA2;
            }
            else
            {
                // Valor negativo
                combinedValue = ((TTA1 << 7) | TTA2);
                combinedValue = ((~combinedValue) + 1) & 0x3FF;
                combinedValue *= -1;
            }
            double resolution = 90.0 / 512.0;
            double TTA = combinedValue * resolution;
            this.TTA = TTA;
            return this.TTA;
        }
        public int GSstatusDecoding(byte[] data)
        {
            int GSstatus = (data[2] >> 0) & 0b00000001;
            this.GSstatus = GSstatus;
            return this.GSstatus;

        }
        public double GSDecoding(byte[] data)
        {
            byte gs1 = (byte)((data[3] >> 0) & 0b11111111);
            byte gs2 = (byte)((data[4] >> 6) & 0b00000011);

            int combinedValue = (gs1 << 2) | gs2;

            double resolution = (double)(1024 / 512); // Resolución en kt
            double gs = combinedValue * resolution;
            this.GS = gs;
            return this.GS;
        }
        public int TARstatusDecoding(byte[] data)
        {
            int TARstatus = (data[4] >> 5) & 0b00000001;
            this.TARstatus = TARstatus;
            return this.TARstatus;

        }
        public double TARDecoding(byte[] data)
        {
            byte TAR1 = (byte)((data[4] >> 0) & 0b00011111);
            byte TAR2 = (byte)((data[5] >> 3) & 0b00011111);
            byte signByte = (byte)((data[4] >> 4) & 0b00000001);

            int combinedValue;
            if (signByte == 0)
            {
                // Valor positivo
                combinedValue = (TAR1 << 5) | TAR2;
            }
            else
            {
                // Valor negativo
                combinedValue = ((TAR1 << 5) | TAR2);
                combinedValue = ((~combinedValue) + 1) & 0x1FF;
                combinedValue *= -1;
            }
            double resolution = (double)(8.0 / 256); //resolución en grados/s
            double TAR = combinedValue * resolution;
            this.TAR = TAR;
            return this.TAR;
        }
        public int TASstatusDecoding(byte[] data)
        {
            int TASstatus = (data[5] >> 2) & 0b00000001;
            this.TASstatus = TASstatus;
            return this.TASstatus;

        }
        public double TASDecoding(byte[] data)
        {
            byte tas1 = (byte)((data[5] >> 0) & 0b00000011);
            byte tas2 = (byte)((data[6] >> 0) & 0b11111111);

            int combinedValue = (tas1 << 8) | tas2;

            double resolution = 2; // Resolución en kt
            double tas = combinedValue * resolution;
            this.TAS = tas;
            return this.TAS;

        }

        //MODE 6.0
        public int HDGstatusDecoding(byte[] data)
        {
            int HDGstatus = (data[0] >> 7) & 0b00000001;
            this.HDGstatus = HDGstatus;
            return this.HDGstatus;

        }
        public double HDGDecoding(byte[] data)
        {
            byte HDG1 = (byte)((data[0] >> 0) & 0b01111111);
            byte HDG2 = (byte)((data[1] >> 4) & 0b00001111);
            byte signByte = (byte)((data[0] >> 6) & 0b00000001);

            int combinedValue;
            if (signByte == 0)
            {
                // Valor positivo
                combinedValue = (HDG1 << 4) | HDG2;
            }
            else
            {
                // Valor negativo
                combinedValue = ((HDG1 << 4) | HDG2);
                combinedValue = ((~combinedValue) + 1) & 0x3FF;
                combinedValue *= -1;
            }
            double resolution = (double)(90.0 / 512); //resolución en grados/s
            double HDG = combinedValue * resolution;
            this.HDG = HDG;
            return this.HDG;

        }
        public int IASstatusDecoding(byte[] data)
        {
            int IASstatus = (data[1] >> 3) & 0b00000001;
            this.IASstatus = IASstatus;
            return this.IASstatus;

        }
        public double IASDecoding(byte[] data)
        {
            byte ias1 = (byte)((data[1] >> 0) & 0b00000111);
            byte ias2 = (byte)((data[2] >> 1) & 0b01111111);

            int combinedValue = (ias1 << 7) | ias2;

            double resolution = 1; // Resolución en kt
            double ias = combinedValue * resolution;
            this.IAS = ias;
            return this.IAS;
        }
        public int MACHstatusDecoding(byte[] data)
        {
            int MACHstatus = (data[2] >> 0) & 0b00000001;
            this.MACHstatus = MACHstatus;
            return this.MACHstatus;

        }
        public double MACHDecoding(byte[] data)
        {
            byte mach1 = (byte)((data[3] >> 0) & 0b11111111);
            byte mach2 = (byte)((data[4] >> 6) & 0b00000011);

            int combinedValue = (mach1 << 2) | mach2;

            double resolution = 2.048 / 512.0; // Resolución en kt
            double mach = combinedValue * resolution;
            this.MACH = mach;
            return this.MACH;
        }
        public int BARstatusDecoding(byte[] data)
        {
            int BARstatus = (data[4] >> 5) & 0b00000001;
            this.BARstatus = BARstatus;
            return this.BARstatus;

        }
        public double BARDecoding(byte[] data)
        {
            byte BAR1 = (byte)((data[4] >> 0) & 0b00011111);
            byte BAR2 = (byte)((data[5] >> 3) & 0b00011111);
            byte signByte = (byte)((data[4] >> 4) & 0b00000001);

            int combinedValue;
            if (signByte == 0)
            {
                // Valor positivo
                combinedValue = (BAR1 << 5) | BAR2;
            }
            else
            {
                // Valor negativo
                combinedValue = ((BAR1 << 5) | BAR2);
                combinedValue = ((~combinedValue) + 1) & 0x1FF;
                combinedValue *= -1;
            }
            double resolution = (double)(32.0); //resolución en ft/min
            double BAR = combinedValue * resolution;
            this.BAR = BAR;
            return this.BAR;
        }
        public int IVVstatusDecoding(byte[] data)
        {
            int IVVstatus = (data[5] >> 2) & 0b00000001;
            this.IVVstatus = IVVstatus;
            return this.IVVstatus;
        }
        public double IVVDecoding(byte[] data)
        {
            byte IVV1 = (byte)((data[5] >> 0) & 0b00000011);
            byte IVV2 = (byte)((data[6] >> 0) & 0b11111111);
            byte signByte = (byte)((data[5] >> 1) & 0b00000001);

            int combinedValue;
            if (signByte == 0)
            {
                // Valor positivo
                combinedValue = (IVV1 << 8) | IVV2;
            }
            else
            {
                // Valor negativo
                combinedValue = ((IVV1 << 8) | IVV2);
                combinedValue = ((~combinedValue) + 1) & 0x1FF;
                combinedValue *= -1;
            }
            double resolution = (double)(32.0); //resolución en ft/min
            double IVV = combinedValue * resolution;
            this.IVV = IVV;
            return this.IVV;
        }

        public List<object[]> decode250(byte[] data)
        {
            int REP = data[0];
            byte[] data_mode = new byte[8];
            List<object[]> decodedDataList = new List<object[]>();
            for (int i = 0; i < REP; i++)
            {
                Array.Copy(data, 8 * i + 1, data_mode, 0, 8);
                int[] BDS_address = fullModeSDecoding(data_mode);
                if (BDS_address[0] == 4 && BDS_address[1] == 0)
                {
                    object[] decodedData = new object[12];
                    int MPCStatus = MCPStatusDecoding(data_mode);
                    double MPCPalt = MCPaltDecoding(data_mode);
                    int FMstatus = FMstatusDecoding(data_mode);
                    double FMalt = FMaltDecoding(data_mode);
                    int BPstatus = BPstatusDecoding(data_mode);
                    double BPpres = BPpresDecoding(data_mode);
                    int modeStat = modeStatDecoding(data_mode);
                    int VNAV = VNAVDecoding(data_mode);
                    int ALThold = ALTholdDecoding(data_mode);
                    int App = AppDecoding(data_mode);
                    int targetalt_status = targetalt_statusDecoding(data_mode);
                    string targetalt_source = targetalt_sourceDecoding(data_mode);
                    decodedData[0] = MPCStatus;
                    decodedData[1] = MPCPalt;
                    decodedData[2] = FMstatus;
                    decodedData[3] = FMalt;
                    decodedData[4] = BPstatus;
                    decodedData[5] = BPpres;
                    decodedData[6] = modeStat;
                    decodedData[7] = VNAV;
                    decodedData[8] = ALThold;
                    decodedData[9] = App;
                    decodedData[10] = targetalt_status;
                    decodedData[11] = targetalt_source;
                    decodedDataList.Add(decodedData);
                }
                if (BDS_address[0] == 5 && BDS_address[1] == 0)
                {
                    object[] decodedData = new object[10];
                    int RAstatus = RAstatusDecoding(data_mode);
                    double RA = RADecoding(data_mode);
                    int TTAstatus = TTAstatusDecoding(data_mode);
                    double TTA = TTADecoding(data_mode);
                    int GSstatus = GSstatusDecoding(data_mode);
                    double GS = GSDecoding(data_mode);
                    int TARstatus = TARstatusDecoding(data_mode);
                    double TAR = TARDecoding(data_mode);
                    int TASstatus = TASstatusDecoding(data_mode);
                    double TAS = TASDecoding(data_mode);
                    decodedData[0] = RAstatus;
                    decodedData[1] = RA;
                    decodedData[2] = TTAstatus;
                    decodedData[3] = TTA;
                    decodedData[4] = GSstatus;
                    decodedData[5] = GS;
                    decodedData[6] = TARstatus;
                    decodedData[7] = TAR;
                    decodedData[8] = TASstatus;
                    decodedData[9] = TAS;
                    decodedDataList.Add(decodedData);
                }
                if (BDS_address[0] == 6 && BDS_address[1] == 0)
                {
                    object[] decodedData = new object[11];
                    int HDGstatus = HDGstatusDecoding(data_mode);
                    double HDG = HDGDecoding(data_mode);
                    int IASstatus = IASstatusDecoding(data_mode);
                    double IAS = IASDecoding(data_mode);
                    int MACHstatus = MACHstatusDecoding(data_mode);
                    double MACH = MACHDecoding(data_mode);
                    int BARstatus = BARstatusDecoding(data_mode);
                    double BAR = BARDecoding(data_mode);
                    int IVVstatus = IVVstatusDecoding(data_mode);
                    double IVV = IVVDecoding(data_mode);
                    decodedData[0] = HDGstatus;
                    decodedData[1] = HDG;
                    decodedData[2] = IASstatus;
                    decodedData[3] = IAS;
                    decodedData[4] = MACHstatus;
                    decodedData[5] = MACH;
                    decodedData[6] = BARstatus;
                    decodedData[7] = BAR;
                    decodedData[8] = IVVstatus;
                    decodedData[9] = IVV;
                    decodedData[10] = 0;
                    decodedDataList.Add(decodedData);
                }
            }
            return decodedDataList;
        }

        //---------------------------------------------------------------

        //DECODE 161
        //----------------------------------------------------------------
        public int tracknumberDecoding(byte[] data)
        {
            byte track1 = (byte)((data[0] >> 0) & 0b00001111);
            byte track2 = (byte)((data[1] >> 0) & 0b11111111);
            double resolution = 1;
            int combinedValue;
            combinedValue = (track1 << 8) | track2;
            int tracknumber = (int)(combinedValue * resolution);
            return tracknumber;
        }
        //----------------------------------------------------------------

        //DECODE 042
        //----------------------------------------------------------------
        public double x_componentDecoding(byte[] data)
        {
            byte x1 = (byte)((data[0] >> 0) & 0b11111111);
            byte x2 = (byte)((data[1] >> 0) & 0b11111111);
            byte signByte = (byte)((data[0] >> 7) & 0b00000001);

            int combinedValue;

            if (signByte == 0)
            {
                // Valor positivo
                combinedValue = (x1 << 8) | x2;
            }
            else
            {
                // Valor negativo
                combinedValue = ((x1 << 8) | x2);
                combinedValue = -(~combinedValue + 1);
            }

            double resolution = (double)(1 / 128.0); // Resolución en NM
            double x = combinedValue * resolution;
            this.x_component = x;
            return this.x_component;
        }
        public double y_componentDecoding(byte[] data)
        {
            byte y1 = (byte)((data[2] >> 0) & 0b11111111);
            byte y2 = (byte)((data[3] >> 0) & 0b11111111);
            byte signByte = (byte)((data[2] >> 7) & 0b00000001);

            int combinedValue;

            if (signByte == 0)
            {
                // Valor positivo
                combinedValue = (y1 << 8) | y2;
            }
            else
            {
                // Valor negativo
                combinedValue = ((y1 << 8) | y2);
                combinedValue = -(~combinedValue + 1);
            }

            double resolution = (double)(1 / 128.0); // Resolución en NM
            double y = combinedValue * resolution;
            this.y_component = y;
            return this.y_component;
        }

        public (double, double) decode042(byte[] data)
        {
            double x = x_componentDecoding(data);
            double y = y_componentDecoding(data);
            return (x, y);
        }
        //----------------------------------------------------------------

        // DECODE 040
        //----------------------------------------------------------------
        public double PolarRhoDecoding(byte[] data)
        {
            byte[] rhovec = new byte[] { data[0], data[1] }; //se cogen 16 bits
            double resolutionLSB = 1.0 / Math.Pow(2, 8);
            this.rho = ByteToDoubleDecoding(rhovec, resolutionLSB);
            return this.rho;
        }

        public double PolarThetaDecoding(byte[] data)
        {
            byte[] thetavec = new byte[] { data[2], data[3] }; //se cogen 16 bits
            double resolutionLSB = 360.0 / Math.Pow(2, 16);
            this.theta = ByteToDoubleDecoding(thetavec, resolutionLSB);
            return this.theta;
        }

        public (double, double) decode040(byte[] data)
        {
            double rho = PolarRhoDecoding(data);
            double theta = PolarThetaDecoding(data);
            return (rho, theta);
        }
        //---------------------------------------------------------------

        // DECODE 200
        //----------------------------------------------------------------
        public double groundspeedpolarDecoding(byte[] data)
        {

            int combinedValue = (data[0] << 8) | data[1];
            bool isNegative = (combinedValue & 0x8000) != 0;
            if (isNegative)
            {
                combinedValue = -(~combinedValue + 1);
            }

            double resolution = Math.Pow(2, -14) * 3600; // Resolución en NM
            this.groundspeedpolar = combinedValue * resolution;

            return this.groundspeedpolar;
        }

        public double headingpolarDecoding(byte[] data)
        {
            int combinedValue = (data[2] << 8) | data[3];
            bool isNegative = (combinedValue & 0x8000) != 0;
            if (isNegative)
            {
                combinedValue = -(~combinedValue + 1);
            }
            double resolution = 360.0 / Math.Pow(2, 16); // Resolución en grados
            this.headingpolar = combinedValue * resolution;

            return this.headingpolar;
        }

        public (double, double) decode200(byte[] data)
        {
            double groundspeed = groundspeedpolarDecoding(data);
            double heading = headingpolarDecoding(data);
            return (groundspeed, heading);
        }
        //----------------------------------------------------------------

        // DECODE 170
        //----------------------------------------------------------------
        public string CNFDecoding(byte[] data)
        {
            int cnf = (data[0] >> 7) & 0b00000001;
            Dictionary<int, string> cnfDescriptions = new Dictionary<int, string>()
        {
            { 0, "Confirmed Track" },
            { 1, "Tentative Track" }
        };
            return cnfDescriptions.ContainsKey(cnf) ? cnfDescriptions[cnf] : "";

        }
        public string RADDecoding(byte[] data)
        {
            int rad = (data[0] >> 5) & 0b00000011;
            Dictionary<int, string> radDescriptions = new Dictionary<int, string>()
        {
            { 0, "Combined Track" },
            { 1, "PSR Track" },
            { 2, "SSR/Mode S Track" },
            { 3, "Invalid" }
        };
            return radDescriptions.ContainsKey(rad) ? radDescriptions[rad] : "";
        }
        public string DOUDecoding(byte[] data)
        {
            int dou = (data[0] >> 4) & 0b00000001;
            Dictionary<int, string> douDescriptions = new Dictionary<int, string>()
        {
            { 0, "Normal confidence" },
            { 1, "Low confidence in plot to track association" }
        };
            return douDescriptions.ContainsKey(dou) ? douDescriptions[dou] : "";

        }
        public string MAHDecoding(byte[] data)
        {
            int mah = (data[0] >> 3) & 0b00000001;
            Dictionary<int, string> mahDescriptions = new Dictionary<int, string>()
        {
            { 0, "No horizontal man.sensed" },
            { 1, "Horizontal man. sensed" }
        };
            return mahDescriptions.ContainsKey(mah) ? mahDescriptions[mah] : "";

        }
        public string CDMDecoding(byte[] data)
        {
            int cdm = (data[0] >> 1) & 0b00000011;
            Dictionary<int, string> cdmDescriptions = new Dictionary<int, string>()
        {
            { 0, "Maintaining" },
            { 1, "Climbing" },
            { 2, "Descending" },
            { 3, "Unknown" }
        };
            return cdmDescriptions.ContainsKey(cdm) ? cdmDescriptions[cdm] : "";
        }
        public string TREDecoding(byte[] data)
        {
            int tre = (data[1] >> 7) & 0b00000001;
            Dictionary<int, string> treDescriptions = new Dictionary<int, string>()
        {
            { 0, "Track still alive" },
            { 1, "End of track lifetime(last report for this track)" }
        };
            return treDescriptions.ContainsKey(tre) ? treDescriptions[tre] : "";
        }
        public string GHODecoding(byte[] data)
        {
            int gho = (data[1] >> 6) & 0b00000001;
            Dictionary<int, string> ghoDescriptions = new Dictionary<int, string>()
        {
            { 0, "True target track." },
            { 1, "Ghost target track." }
        };
            return ghoDescriptions.ContainsKey(gho) ? ghoDescriptions[gho] : "";
        }
        public string SUPDecoding(byte[] data)
        {
            int sup = (data[1] >> 5) & 0b00000001;
            Dictionary<int, string> supDescriptions = new Dictionary<int, string>()
        {
            { 0, "no" },
            { 1, "yes" }
        };
            return supDescriptions.ContainsKey(sup) ? supDescriptions[sup] : "";
        }
        public string TCCDecoding(byte[] data)
        {
            int tcc = (data[1] >> 4) & 0b00000001;
            Dictionary<int, string> tccDescriptions = new Dictionary<int, string>()
        {
            { 0, "Tracking performed in so-called Radar Plane, i.e. neither slant range correction nor stereographical projection was applied." },
            { 1, "Slant range correction and a suitable projection technique are used to track in a 2D.reference plane, tangential to the earth model at the Radar Site co-ordinates." }
        };
            return tccDescriptions.ContainsKey(tcc) ? tccDescriptions[tcc] : "";
        }
        public List<string> decode170(List<byte> data_list)
        {
            byte[] data = data_list.ToArray();
            List<string> decodedData = new List<string>();
            string CNF = CNFDecoding(data);
            string RAD = RADDecoding(data);
            string DOU = DOUDecoding(data);
            string MAH = MAHDecoding(data);
            string CDM = CDMDecoding(data);
            decodedData.Add(CNF);
            decodedData.Add(RAD);
            decodedData.Add(DOU);
            decodedData.Add(MAH);
            decodedData.Add(CDM);
            if (data.Length > 1)
            {
                string TRE = TREDecoding(data);
                string GHO = GHODecoding(data);
                string SUP = SUPDecoding(data);
                string TCC = TCCDecoding(data);
                decodedData.Add(TRE);
                decodedData.Add(GHO);
                decodedData.Add(SUP);
                decodedData.Add(TCC);
            }
            return decodedData;
        }
        //----------------------------------------------------------------

        // DECODE 110
        //----------------------------------------------------------------
        public double measuredheightDecoding(byte[] data)
        {
            byte h1 = (byte)((data[0] >> 0) & 0b00111111);
            byte h2 = (byte)((data[1] >> 0) & 0b11111111);
            byte signByte = (byte)((data[0] >> 5) & 0b00000001);

            int combinedValue;

            if (signByte == 0)
            {
                // Valor positivo
                combinedValue = (h1 << 8) | h2;
            }
            else
            {
                // Valor negativo
                combinedValue = ((h1 << 8) | h2);
                combinedValue = -(~combinedValue + 1);
            }

            double resolution = (double)(25.0); // Resolución en ft
            double h = combinedValue * resolution;
            this.measuredheight = h;
            return this.measuredheight;
        }
        //----------------------------------------------------------------

        // DECODE 230
        //----------------------------------------------------------------
        public string COMDecoding(byte[] data)
        {
            int com = (data[0] >> 5) & 0b00000111;
            Dictionary<int, string> comDescriptions = new Dictionary<int, string>()
        {
            { 0, "No communications capability (surveillance only)" },
            { 1, "Comm. A and Comm. B capability" },
            { 2, "Comm. A, Comm. B and Uplink ELM" },
            { 3, "Comm. A, Comm. B, Uplink ELM and Downlink ELM" },
            { 4, "Level 5 Transponder capability" },
            { 5, "Not assigned" },
            { 6, "Not assigned" },
            { 7, "Not assigned" }
        };
            return comDescriptions.ContainsKey(com) ? comDescriptions[com] : "";
        }
        public string STATDecoding(byte[] data)
        {
            int stat = (data[0] >> 2) & 0b00000111;
            Dictionary<int, string> statDescriptions = new Dictionary<int, string>()
        {
            { 0, "No alert, no SPI, aircraft airborne" },
            { 1, "No alert, no SPI, aircraft on ground" },
            { 2, "Alert, no SPI, aircraft airborne" },
            { 3, "Alert, no SPI, aircraft on ground" },
            { 4, "Alert, SPI, aircraft airborne or on ground" },
            { 5, "No alert, SPI, aircraft airborne or on ground" },
            { 6, "Not assigned" },
            { 7, "Unknown" }
        };
            return statDescriptions.ContainsKey(stat) ? statDescriptions[stat] : "";
        }
        public string SIDecoding(byte[] data)
        {
            int si = (data[0] >> 1) & 0b00000001;
            Dictionary<int, string> siDescriptions = new Dictionary<int, string>()
        {
            { 0, "SI-Code Capable" },
            { 1, "II-Code Capable" }
        };
            return siDescriptions.ContainsKey(si) ? siDescriptions[si] : "";
        }
        public string MSCCDecoding(byte[] data)
        {
            int mscc = (data[1] >> 7) & 0b00000001;
            Dictionary<int, string> msccDescriptions = new Dictionary<int, string>()
        {
            { 0, "No" },
            { 1, "Yes" }
        };
            return msccDescriptions.ContainsKey(mscc) ? msccDescriptions[mscc] : "";
        }
        public string ARCDecoding(byte[] data)
        {
            int arc = (data[1] >> 6) & 0b00000001;
            Dictionary<int, string> arcDescriptions = new Dictionary<int, string>()
        {
            { 0, "100 ft resolution" },
            { 1, "25 ft resolution" }
        };
            return arcDescriptions.ContainsKey(arc) ? arcDescriptions[arc] : "";
        }
        public string AICDecoding(byte[] data)
        {
            int aic = (data[1] >> 5) & 0b00000001;
            Dictionary<int, string> aicDescriptions = new Dictionary<int, string>()
        {
            { 0, "No" },
            { 1, "Yes" }
        };
            return aicDescriptions.ContainsKey(aic) ? aicDescriptions[aic] : "";
        }
        public string B1ADecoding(byte[] data)
        {
            int b1a = (data[1] >> 4) & 0b00000001;
            Dictionary<int, string> b1aDescriptions = new Dictionary<int, string>()
        {
            { 0, "BDS 1,0 bit 16 = 0" },
            { 1, "BDS 1,0 bit 16 = 1" }
        };
            return b1aDescriptions.ContainsKey(b1a) ? b1aDescriptions[b1a] : "";
        }
        public string B1BDecoding(byte[] data)
        {
            int B1B = (data[1] >> 0) & 0b00001111;
            this.B1B = "BDS 1,0 bits 37/40 = " + Convert.ToString(B1B, 2).PadLeft(4, '0');
            return this.B1B;
        }
        public (string, string, string, string, string, string, string, string) decode230(byte[] data)
        {
            string COM = COMDecoding(data);
            string STAT = STATDecoding(data);
            string SID = SIDecoding(data);
            string MSCC = MSCCDecoding(data);
            string ARC = ARCDecoding(data);
            string AIC = AICDecoding(data);
            string B1A = B1ADecoding(data);
            string B1B = B1BDecoding(data);
            return (COM, STAT, SID, MSCC, ARC, AIC, B1A, B1B);
        }
        //----------------------------------------------------------------

        // DECODE 220
        //----------------------------------------------------------------
        public string decode220(byte[] data)
        {
            string aircraft_address = BitConverter.ToString(data).Replace("-", "");
            return aircraft_address;
        }
        //----------------------------------------------------------------

        // DECODE 240
        //----------------------------------------------------------------
        public string decode240(byte[] data)
        {
            string cadenabits = string.Join("", Array.ConvertAll(data, b => Convert.ToString(b, 2).PadLeft(8, '0')));

            string[] aircraft_identification = new string[8];
            for (int i = 0; i < 8; i++)
            {
                aircraft_identification[i] = cadenabits.Substring(i * 6, 6);
                if (aircraft_identification[i] == "000001")
                {
                    aircraft_identification[i] = "A";
                }
                if (aircraft_identification[i] == "000010")
                {
                    aircraft_identification[i] = "B";
                }
                if (aircraft_identification[i] == "000011")
                {
                    aircraft_identification[i] = "C";
                }
                if (aircraft_identification[i] == "000100")
                {
                    aircraft_identification[i] = "D";
                }
                if (aircraft_identification[i] == "000101")
                {
                    aircraft_identification[i] = "E";
                }
                if (aircraft_identification[i] == "000110")
                {
                    aircraft_identification[i] = "F";
                }
                if (aircraft_identification[i] == "000111")
                {
                    aircraft_identification[i] = "G";
                }
                if (aircraft_identification[i] == "001000")
                {
                    aircraft_identification[i] = "H";
                }
                if (aircraft_identification[i] == "001001")
                {
                    aircraft_identification[i] = "I";
                }
                if (aircraft_identification[i] == "001010")
                {
                    aircraft_identification[i] = "J";
                }
                if (aircraft_identification[i] == "001011")
                {
                    aircraft_identification[i] = "K";
                }
                if (aircraft_identification[i] == "001100")
                {
                    aircraft_identification[i] = "L";
                }
                if (aircraft_identification[i] == "001101")
                {
                    aircraft_identification[i] = "M";
                }
                if (aircraft_identification[i] == "001110")
                {
                    aircraft_identification[i] = "N";
                }
                if (aircraft_identification[i] == "001111")
                {
                    aircraft_identification[i] = "O";
                }
                if (aircraft_identification[i] == "010000")
                {
                    aircraft_identification[i] = "P";
                }
                if (aircraft_identification[i] == "010001")
                {
                    aircraft_identification[i] = "Q";
                }
                if (aircraft_identification[i] == "010010")
                {
                    aircraft_identification[i] = "R";
                }
                if (aircraft_identification[i] == "010011")
                {
                    aircraft_identification[i] = "S";
                }
                if (aircraft_identification[i] == "010100")
                {
                    aircraft_identification[i] = "T";
                }
                if (aircraft_identification[i] == "010101")
                {
                    aircraft_identification[i] = "U";
                }
                if (aircraft_identification[i] == "010110")
                {
                    aircraft_identification[i] = "V";
                }
                if (aircraft_identification[i] == "010111")
                {
                    aircraft_identification[i] = "W";
                }
                if (aircraft_identification[i] == "011000")
                {
                    aircraft_identification[i] = "X";
                }
                if (aircraft_identification[i] == "011001")
                {
                    aircraft_identification[i] = "Y";
                }
                if (aircraft_identification[i] == "011010")
                {
                    aircraft_identification[i] = "Z";
                }
                if (aircraft_identification[i] == "100000")
                {
                    aircraft_identification[i] = " ";
                }
                if (aircraft_identification[i] == "110000")
                {
                    aircraft_identification[i] = "0";
                }
                if (aircraft_identification[i] == "110001")
                {
                    aircraft_identification[i] = "1";
                }
                if (aircraft_identification[i] == "110010")
                {
                    aircraft_identification[i] = "2";
                }
                if (aircraft_identification[i] == "110011")
                {
                    aircraft_identification[i] = "3";
                }
                if (aircraft_identification[i] == "110100")
                {
                    aircraft_identification[i] = "4";
                }
                if (aircraft_identification[i] == "110101")
                {
                    aircraft_identification[i] = "5";
                }
                if (aircraft_identification[i] == "110110")
                {
                    aircraft_identification[i] = "6";
                }
                if (aircraft_identification[i] == "110111")
                {
                    aircraft_identification[i] = "7";
                }
                if (aircraft_identification[i] == "111000")
                {
                    aircraft_identification[i] = "8";
                }
                if (aircraft_identification[i] == "111001")
                {
                    aircraft_identification[i] = "9";
                }
            }
            string ai = string.Join("", aircraft_identification);
            return ai;
        }
        //----------------------------------------------------------------
    }
}