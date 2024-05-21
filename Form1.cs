using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OurClasses;
using PGTA2_DEF;


namespace PGTA2_DEF
{
    public partial class Form1 : Form
    {
        DataTable dataTable = new DataTable();
        String fileName = "";

        public static DataTable GenerateDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("NUM", typeof(int));
            dataTable.Columns.Add("SAC", typeof(int));
            dataTable.Columns.Add("SIC", typeof(int));
            dataTable.Columns.Add("Time of day", typeof(TimeSpan));
            dataTable.Columns.Add("Target Report Descriptor: TYP", typeof(string));
            dataTable.Columns.Add("Target Report Descriptor: SIM", typeof(string));
            dataTable.Columns.Add("Target Report Descriptor: RDP", typeof(string));
            dataTable.Columns.Add("Target Report Descriptor: SPI", typeof(string));
            dataTable.Columns.Add("Target Report Descriptor: RAB", typeof(string));
            dataTable.Columns.Add("Target Report Descriptor: TST", typeof(string));
            dataTable.Columns.Add("Target Report Descriptor: ERR", typeof(string));
            dataTable.Columns.Add("Target Report Descriptor: XPP", typeof(string));
            dataTable.Columns.Add("Target Report Descriptor: ME", typeof(string));
            dataTable.Columns.Add("Target Report Descriptor: MI", typeof(string));
            dataTable.Columns.Add("Target Report Descriptor: FOE/FRI", typeof(string));
            dataTable.Columns.Add("Target Report Descriptor: ADSB#EP", typeof(string));
            dataTable.Columns.Add("Target Report Descriptor: ADSB#VAL", typeof(string));
            dataTable.Columns.Add("Target Report Descriptor: SCN#EP", typeof(string));
            dataTable.Columns.Add("Target Report Descriptor: SCN#VAL", typeof(string));
            dataTable.Columns.Add("Target Report Descriptor: PAI#EP", typeof(string));
            dataTable.Columns.Add("Target Report Descriptor: PAI#VAL", typeof(string));
            dataTable.Columns.Add("Measured Position in Polar Co-ordinates: Rho", typeof(double));
            dataTable.Columns.Add("Measured Position in Polar Co-ordinates: Theta", typeof(double));
            dataTable.Columns.Add("Mode-3/A Code: V", typeof(string));
            dataTable.Columns.Add("Mode-3/A Code: G", typeof(string));
            dataTable.Columns.Add("Mode-3/A Code: L", typeof(string));
            dataTable.Columns.Add("Mode-3/A Code: mode3A", typeof(string));
            dataTable.Columns.Add("Flight Level: V", typeof(string));
            dataTable.Columns.Add("Flight Level: G", typeof(string));
            dataTable.Columns.Add("Flight Level: Fligt Level", typeof(string));
            dataTable.Columns.Add("Flight Level: modeC corrected", typeof(string));
            dataTable.Columns.Add("Radar Plot Characteristics: SRL", typeof(double));
            dataTable.Columns.Add("Radar Plot Characteristics: SSR", typeof(double));
            dataTable.Columns.Add("Radar Plot Characteristics: SAM", typeof(double));
            dataTable.Columns.Add("Radar Plot Characteristics: PRL", typeof(double));
            dataTable.Columns.Add("Radar Plot Characteristics: PAM", typeof(double));
            dataTable.Columns.Add("Radar Plot Characteristics: RPD", typeof(double));
            dataTable.Columns.Add("Radar Plot Characteristics: APD", typeof(double));
            dataTable.Columns.Add("Aircraft Address", typeof(string));
            dataTable.Columns.Add("Aircraft Identification", typeof(string));
            dataTable.Columns.Add("BDS Register Data. 4.0. MPCStatus");
            dataTable.Columns.Add("BDS Register Data. 4.0. MPCPalt");
            dataTable.Columns.Add("BDS Register Data. 4.0. FMstatus");
            dataTable.Columns.Add("BDS Register Data. 4.0. FMalt");
            dataTable.Columns.Add("BDS Register Data. 4.0. BPstatus");
            dataTable.Columns.Add("BDS Register Data. 4.0. BPpres");
            dataTable.Columns.Add("BDS Register Data. 4.0. modeStat");
            dataTable.Columns.Add("BDS Register Data. 4.0. VNAV");
            dataTable.Columns.Add("BDS Register Data. 4.0. ALThold");
            dataTable.Columns.Add("BDS Register Data. 4.0. App");
            dataTable.Columns.Add("BDS Register Data. 4.0. targetalt_status");
            dataTable.Columns.Add("BDS Register Data. 4.0. targetalt_source");
            dataTable.Columns.Add("BDS Register Data. 5.0. RAstatus");
            dataTable.Columns.Add("BDS Register Data. 5.0. RA");
            dataTable.Columns.Add("BDS Register Data. 5.0. TTAstatus");
            dataTable.Columns.Add("BDS Register Data. 5.0. TTA");
            dataTable.Columns.Add("BDS Register Data. 5.0. GSstatus");
            dataTable.Columns.Add("BDS Register Data. 5.0. GS");
            dataTable.Columns.Add("BDS Register Data. 5.0. TARstatus");
            dataTable.Columns.Add("BDS Register Data. 5.0. TAR");
            dataTable.Columns.Add("BDS Register Data. 5.0. TASstatus");
            dataTable.Columns.Add("BDS Register Data. 5.0. TAS");
            dataTable.Columns.Add("BDS Register Data. 6.0. HDGstatus");
            dataTable.Columns.Add("BDS Register Data. 6.0. HDG");
            dataTable.Columns.Add("BDS Register Data. 6.0. IASstatus");
            dataTable.Columns.Add("BDS Register Data. 6.0. IAS");
            dataTable.Columns.Add("BDS Register Data. 6.0. MACHstatus");
            dataTable.Columns.Add("BDS Register Data. 6.0. MACH");
            dataTable.Columns.Add("BDS Register Data. 6.0. BARstatus");
            dataTable.Columns.Add("BDS Register Data. 6.0. BAR");
            dataTable.Columns.Add("BDS Register Data. 6.0. IVVstatus");
            dataTable.Columns.Add("BDS Register Data. 6.0. IVV");
            dataTable.Columns.Add("Track number", typeof(int));
            dataTable.Columns.Add("Calculated position in cartesian coordinates: x", typeof(double));
            dataTable.Columns.Add("Calculated position in cartesian coordinates: y", typeof(double));
            dataTable.Columns.Add("Calculated track velocity in polar representartion: groundspeed", typeof(double));
            dataTable.Columns.Add("Calculated track velocity in polar representartion: heading", typeof(double));
            dataTable.Columns.Add("Track Status: CNF", typeof(string));
            dataTable.Columns.Add("Track Status: RAD", typeof(string));
            dataTable.Columns.Add("Track Status: DOU", typeof(string));
            dataTable.Columns.Add("Track Status: MAH", typeof(string));
            dataTable.Columns.Add("Track Status: CDM", typeof(string));
            dataTable.Columns.Add("Track Status: TRE", typeof(string));
            dataTable.Columns.Add("Track Status: GHO", typeof(string));
            dataTable.Columns.Add("Track Status: SUP", typeof(string));
            dataTable.Columns.Add("Track Status: TCC", typeof(string));
            dataTable.Columns.Add("Height measured by 3D radar", typeof(double));
            dataTable.Columns.Add("Communications/ACAS Capability and Flight Status: COM", typeof(string));
            dataTable.Columns.Add("Communications/ACAS Capability and Flight Status: STAT", typeof(string));
            dataTable.Columns.Add("Communications/ACAS Capability and Flight Status: SID", typeof(string));
            dataTable.Columns.Add("Communications/ACAS Capability and Flight Status: MSCC", typeof(string));
            dataTable.Columns.Add("Communications/ACAS Capability and Flight Status: ARC", typeof(string));
            dataTable.Columns.Add("Communications/ACAS Capability and Flight Status: AIC", typeof(string));
            dataTable.Columns.Add("Communications/ACAS Capability and Flight Status: B1A", typeof(string));
            dataTable.Columns.Add("Communications/ACAS Capability and Flight Status: B1B", typeof(string));
            dataTable.Columns.Add("Latitude", typeof(double));
            dataTable.Columns.Add("Longitude", typeof(double));
            dataTable.Columns.Add("h");


            return dataTable;
        }
        public Form1()
        {
            InitializeComponent();
            this.dataTable = GenerateDataTable(); 
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dataTable;
        }

        public void ExportCSV_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Text File|*.csv";
            sfd.FileName = "My Text File";
            sfd.Title = "Save Text File";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = sfd.FileName;
                Program.WriteCSV(dataTable, path);

            }
            
        }

        private void Viewmap_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(dataTable);
            form2.Show();

        }

        private void selectfile_Click(object sender, EventArgs e)
        {

            openFileDialog1.Title = "Search for the .ast file";
            openFileDialog1.ShowDialog();

            if (File.Exists(openFileDialog1.FileName))
            {
                string txt = openFileDialog1.FileName;
                byte[] fileBytes = File.ReadAllBytes(txt);

                List<byte[]> Messages = GetCat48Messages(fileBytes);

                for (int i = 0; i < Messages.Count; i++)
                {
                    List<byte> fspec = Fspec(Messages[i]);

                    byte[] data = Data(Messages[i], fspec.Count);

                    DataRow Row = dataTable.NewRow();
                    Row["NUM"] = i;

                    classifyparams(fspec, data, Row, this.dataTable);
                }
            }
        }

        static int getdeclen(byte l1, byte l2)
        {
            int Length = (l1 << 8) | l2;
            return Length;
        }

        static List<byte[]> GetCat48Messages(byte[] fileBytes)
        {
            List<byte[]> listabyte = new List<byte[]>();
            int i = 0;
            int contador = getdeclen(fileBytes[1], fileBytes[2]);
            //int contador = fileBytes[2];

            while (i < fileBytes.Length)
            {
                byte[] array = new byte[contador];
                for (int j = 0; j < array.Length; j++)
                {
                    array[j] = fileBytes[i];
                    i++;
                }
                listabyte.Add(array);
                if (i + 2 < fileBytes.Length)
                {
                    contador = getdeclen(fileBytes[i + 1], fileBytes[i + 2]);
                    //contador = fileBytes[i + 2];
                }
            }
            return listabyte;
        }

        static void classifyparams(List<byte> fspec, byte[] data, DataRow Row, DataTable dataTable)
        {
            double rho = 0;
            double theta = 0;
            double FL = 0;
            double FL_corrected = 0;
            Decoder48 decoder48 = new Decoder48();
            if (1 == ((fspec[0] >> 7) & 0b00000001))
            {
                byte SAC = data[0];
                byte SIC = data[1];
                Array.Copy(data, 2, data, 0, data.Length - 2);

                Row["SAC"] = SAC;
                Row["SIC"] = SIC;
            }
            if (1 == ((fspec[0] >> 6) & 0b00000001))
            {
                byte[] data_item_140 = new byte[3];
                Array.Copy(data, 0, data_item_140, 0, 3);
                TimeSpan TimeofDay = decoder48.decodeTimeDay(data_item_140);
                Array.Copy(data, 3, data, 0, data.Length - 3);

                Row["Time of day"] = TimeofDay;
            }
            if (1 == ((fspec[0] >> 5) & 0b00000001))
            {
                List<byte> data_item_020 = new List<byte>();
                int i = 0;
                while (i != -1)
                {
                    data_item_020.Add(data[i]);
                    if ((data[i] & 0b00000001) == 1) { i++; }
                    else { i = -1; }
                }
                List<string> result020 = decoder48.decode020(data_item_020);
                Array.Copy(data, data_item_020.Count, data, 0, data.Length - data_item_020.Count);

                Row["Target Report Descriptor: TYP"] = result020[0];
                Row["Target Report Descriptor: SIM"] = result020[1];
                Row["Target Report Descriptor: RDP"] = result020[2];
                Row["Target Report Descriptor: SPI"] = result020[3];
                Row["Target Report Descriptor: RAB"] = result020[4];
                if (data_item_020.Count > 1)
                {
                    Row["Target Report Descriptor: TST"] = result020[5];
                    Row["Target Report Descriptor: ERR"] = result020[6];
                    Row["Target Report Descriptor: XPP"] = result020[7];
                    Row["Target Report Descriptor: ME"] = result020[8];
                    Row["Target Report Descriptor: MI"] = result020[9];
                    Row["Target Report Descriptor: FOE/FRI"] = result020[10];
                    if (data_item_020.Count > 2)
                    {
                        Row["Target Report Descriptor: ADSB#EP"] = result020[11];
                        Row["Target Report Descriptor: ADSB#VAL"] = result020[12];
                        Row["Target Report Descriptor: SCN#EP"] = result020[13];
                        Row["Target Report Descriptor: SCN#VAL"] = result020[14];
                        Row["Target Report Descriptor: PAI#EP"] = result020[15];
                        Row["Target Report Descriptor: PAI#VAL"] = result020[16];
                    }
                }

            }
            if (1 == ((fspec[0] >> 4) & 0b00000001))
            {
                byte[] data_item_040 = new byte[4];
                Array.Copy(data, 0, data_item_040, 0, 4);
                var (Rho, Theta) = decoder48.decode040(data_item_040);
                Array.Copy(data, 4, data, 0, data.Length - 4);
                rho = Rho;
                theta = Theta;

                Row["Measured Position in Polar Co-ordinates: Rho"] = rho;
                Row["Measured Position in Polar Co-ordinates: Theta"] = theta;
            }
            if (1 == ((fspec[0] >> 3) & 0b00000001))
            {
                byte[] data_item_070 = new byte[2];
                Array.Copy(data, 0, data_item_070, 0, 2);
                var (V070, G070, L070, mode3A) = decoder48.decode070(data_item_070);
                Array.Copy(data, 2, data, 0, data.Length - 2);

                Row["Mode-3/A Code: V"] = V070;
                Row["Mode-3/A Code: G"] = G070;
                Row["Mode-3/A Code: L"] = L070;
                Row["Mode-3/A Code: mode3A"] = mode3A;
            }
            if (1 == ((fspec[0] >> 2) & 0b00000001))
            {
                byte[] data_item_090 = new byte[2];
                Array.Copy(data, 0, data_item_090, 0, 2);
                var (V, G, fL) = decoder48.decode090(data_item_090);
                Array.Copy(data, 2, data, 0, data.Length - 2);
                FL = fL;

                Row["Flight Level: V"] = V;
                Row["Flight Level: G"] = G;
                Row["Flight Level: Fligt Level"] = FL;

                if (FL < 60)
                {
                    FL_corrected = modeC_corrected(FL);
                    Row["Flight Level: modeC corrected"] = FL_corrected;
                }
            }
            if (1 == ((fspec[0] >> 1) & 0b00000001))
            {
                int count = 1;
                for (int i = 0; i < 8; i++)
                {
                    if ((data[0] & (1 << i)) != 0)
                    {
                        count++;
                    }
                }
                byte[] data_item_130 = new byte[count];
                Array.Copy(data, 0, data_item_130, 0, count);
                List<double> result130 = decoder48.decode130(data_item_130);
                Array.Copy(data, count, data, 0, data.Length - count);

                byte resultado = 0;
                for (int i = 0; i < 8; i++)
                {
                    resultado |= (byte)(((data_item_130[0] >> i) & 0b00000001) << (7 - i));
                }

                int byteIndex = 0;
                for (int i = 1; i < 8; i++)
                {
                    if (((resultado >> i - 1) & 0b00000001) == 1)
                    {
                        switch (i)
                        {
                            case 7:
                                Row["Radar Plot Characteristics: APD"] = result130[byteIndex];
                                break;
                            case 6:
                                Row["Radar Plot Characteristics: RPD"] = result130[byteIndex];
                                break;
                            case 5:
                                Row["Radar Plot Characteristics: PAM"] = result130[byteIndex];
                                break;
                            case 4:
                                Row["Radar Plot Characteristics: PRL"] = result130[byteIndex];
                                break;
                            case 3:
                                Row["Radar Plot Characteristics: SAM"] = result130[byteIndex];
                                break;
                            case 2:
                                Row["Radar Plot Characteristics: SSR"] = result130[byteIndex];
                                break;
                            case 1:
                                Row["Radar Plot Characteristics: SRL"] = result130[byteIndex];
                                break;
                        }
                        byteIndex += 1;
                    }
                }
            }
            if (1 == ((fspec[0] & 0b00000001)))
            {
                if (1 == ((fspec[1] >> 7) & 0b00000001))
                {
                    byte[] data_item_220 = new byte[3];
                    Array.Copy(data, 0, data_item_220, 0, 3);
                    string result220 = decoder48.decode220(data_item_220);
                    Array.Copy(data, 3, data, 0, data.Length - 3);

                    Row["Aircraft Address"] = result220;
                }
                if (1 == ((fspec[1] >> 6) & 0b00000001))
                {
                    byte[] data_item_240 = new byte[6];
                    Array.Copy(data, 0, data_item_240, 0, 6);
                    string result240 = decoder48.decode240(data_item_240);
                    Array.Copy(data, 6, data, 0, data.Length - 6);

                    Row["Aircraft Identification"] = result240;
                }
                if (1 == ((fspec[1] >> 5) & 0b00000001))
                {
                    int len = data[0] * 8 + 1;
                    byte[] data_item_250 = new byte[len];
                    Array.Copy(data, 0, data_item_250, 0, len);
                    List<object[]> result250 = decoder48.decode250(data_item_250);
                    Array.Copy(data, len, data, 0, data.Length - len);

                    for (int i = 0; i < result250.Count; i++)
                    {
                        if (result250[i].Length == 12)
                        {
                            Row["BDS Register Data. 4.0. MPCStatus"] = result250[i][0];
                            Row["BDS Register Data. 4.0. MPCPalt"] = result250[i][1];
                            Row["BDS Register Data. 4.0. FMstatus"] = result250[i][2];
                            Row["BDS Register Data. 4.0. FMalt"] = result250[i][3];
                            Row["BDS Register Data. 4.0. BPstatus"] = result250[i][4];
                            Row["BDS Register Data. 4.0. BPpres"] = result250[i][5];
                            Row["BDS Register Data. 4.0. modeStat"] = result250[i][6];
                            Row["BDS Register Data. 4.0. VNAV"] = result250[i][7];
                            Row["BDS Register Data. 4.0. ALThold"] = result250[i][8];
                            Row["BDS Register Data. 4.0. App"] = result250[i][9];
                            Row["BDS Register Data. 4.0. targetalt_status"] = result250[i][10];
                            Row["BDS Register Data. 4.0. targetalt_source"] = result250[i][11];
                        }
                        if (result250[i].Length == 10)
                        {
                            Row["BDS Register Data. 5.0. RAstatus"] = result250[i][0];
                            Row["BDS Register Data. 5.0. RA"] = result250[i][1];
                            Row["BDS Register Data. 5.0. TTAstatus"] = result250[i][2];
                            Row["BDS Register Data. 5.0. TTA"] = result250[i][3];
                            Row["BDS Register Data. 5.0. GSstatus"] = result250[i][4];
                            Row["BDS Register Data. 5.0. GS"] = result250[i][5];
                            Row["BDS Register Data. 5.0. TARstatus"] = result250[i][6];
                            Row["BDS Register Data. 5.0. TAR"] = result250[i][7];
                            Row["BDS Register Data. 5.0. TASstatus"] = result250[i][8];
                            Row["BDS Register Data. 5.0. TAS"] = result250[i][9];
                        }
                        if (result250[i].Length == 11)
                        {
                            Row["BDS Register Data. 6.0. HDGstatus"] = result250[i][0];
                            Row["BDS Register Data. 6.0. HDG"] = result250[i][1];
                            Row["BDS Register Data. 6.0. IASstatus"] = result250[i][2];
                            Row["BDS Register Data. 6.0. IAS"] = result250[i][3];
                            Row["BDS Register Data. 6.0. MACHstatus"] = result250[i][4];
                            Row["BDS Register Data. 6.0. MACH"] = result250[i][5];
                            Row["BDS Register Data. 6.0. BARstatus"] = result250[i][6];
                            Row["BDS Register Data. 6.0. BAR"] = result250[i][7];
                            Row["BDS Register Data. 6.0. IVVstatus"] = result250[i][8];
                            Row["BDS Register Data. 6.0. IVV"] = result250[i][9];
                        }
                    }
                }
                if (1 == ((fspec[1] >> 4) & 0b00000001))
                {
                    byte[] data_item_161 = new byte[2];
                    Array.Copy(data, 0, data_item_161, 0, 2);
                    int result161 = decoder48.tracknumberDecoding(data_item_161);
                    Array.Copy(data, 2, data, 0, data.Length - 2);

                    Row["Track number"] = result161;
                }
                if (1 == ((fspec[1] >> 3) & 0b00000001))
                {
                    byte[] data_item_042 = new byte[4];
                    Array.Copy(data, 0, data_item_042, 0, 4);
                    var (x, y) = decoder48.decode042(data_item_042);
                    Array.Copy(data, 4, data, 0, data.Length - 4);

                    Row["Calculated position in cartesian coordinates: x"] = x;
                    Row["Calculated position in cartesian coordinates: y"] = y;
                }
                if (1 == ((fspec[1] >> 2) & 0b00000001))
                {
                    byte[] data_item_200 = new byte[4];
                    Array.Copy(data, 0, data_item_200, 0, 4);
                    var (gs, hd) = decoder48.decode200(data_item_200);
                    Array.Copy(data, 4, data, 0, data.Length - 4);

                    Row["Calculated track velocity in polar representartion: groundspeed"] = gs;
                    Row["Calculated track velocity in polar representartion: heading"] = hd;
                }
                if (1 == ((fspec[1] >> 1) & 0b00000001))
                {
                    List<byte> data_item_170 = new List<byte>();
                    int i = 0;
                    while (i != -1)
                    {
                        data_item_170.Add(data[i]);
                        if ((data[i] & 0b00000001) == 1) { i++; }
                        else { i = -1; }
                    }
                    List<string> result170 = decoder48.decode170(data_item_170);
                    Array.Copy(data, data_item_170.Count, data, 0, data.Length - data_item_170.Count);

                    Row["Track Status: CNF"] = result170[0];
                    Row["Track Status: RAD"] = result170[1];
                    Row["Track Status: DOU"] = result170[2];
                    Row["Track Status: MAH"] = result170[3];
                    Row["Track Status: CDM"] = result170[4];
                    if (data_item_170.Count > 1)
                    {
                        Row["Track Status: TRE"] = result170[5];
                        Row["Track Status: GHO"] = result170[6];
                        Row["Track Status: SUP"] = result170[7];
                        Row["Track Status: TCC"] = result170[8];
                    }
                }
                if (1 == ((fspec[1] & 0b00000001)))
                {
                    if (1 == ((fspec[2] >> 7) & 0b00000001))
                    {
                        Array.Copy(data, 4, data, 0, data.Length - 4);
                    }
                    if (1 == ((fspec[2] >> 6) & 0b00000001))
                    {
                        List<byte> data_item_030 = new List<byte>();
                        int i = 0;
                        while (i != -1)
                        {
                            data_item_030.Add(data[i]);
                            if ((data[i] & 0b00000001) == 1) { i++; }
                            else { i = -1; }
                        }
                        Array.Copy(data, data_item_030.Count, data, 0, data.Length - data_item_030.Count);
                    }
                    if (1 == ((fspec[2] >> 5) & 0b00000001))
                    {
                        Array.Copy(data, 2, data, 0, data.Length - 2);
                    }
                    if (1 == ((fspec[2] >> 4) & 0b00000001))
                    {
                        Array.Copy(data, 4, data, 0, data.Length - 4);
                    }
                    if (1 == ((fspec[2] >> 3) & 0b00000001))
                    {
                        byte[] data_item_110 = new byte[2];
                        Array.Copy(data, 0, data_item_110, 0, 2);
                        double result110 = decoder48.measuredheightDecoding(data_item_110);
                        Array.Copy(data, 2, data, 0, data.Length - 2);

                        Row["Height measured by 3D radar"] = result110;
                    }
                    if (1 == ((fspec[2] >> 2) & 0b00000001))
                    {
                        List<byte> data_item_120 = new List<byte>();
                        int i = 0;
                        while (i != -1)
                        {
                            data_item_120.Add(data[i]);
                            if ((data[i] & 0b00000001) == 1) { i++; }
                            else { i = -1; }
                        }
                        Array.Copy(data, data_item_120.Count, data, 0, data.Length - data_item_120.Count);
                    }
                    if (1 == ((fspec[2] >> 1) & 0b00000001))
                    {
                        byte[] data_item_230 = new byte[2];
                        Array.Copy(data, 0, data_item_230, 0, 2);
                        var (COM, STAT, SID, MSCC, ARC, AIC, B1A, B1B) = decoder48.decode230(data_item_230);
                        Array.Copy(data, 2, data, 0, data.Length - 2);

                        Row["Communications/ACAS Capability and Flight Status: COM"] = COM;
                        Row["Communications/ACAS Capability and Flight Status: STAT"] = STAT;
                        Row["Communications/ACAS Capability and Flight Status: SID"] = SID;
                        Row["Communications/ACAS Capability and Flight Status: MSCC"] = MSCC;
                        Row["Communications/ACAS Capability and Flight Status: ARC"] = ARC;
                        Row["Communications/ACAS Capability and Flight Status: AIC"] = AIC;
                        Row["Communications/ACAS Capability and Flight Status: B1A"] = B1A;
                        Row["Communications/ACAS Capability and Flight Status: B1B"] = B1B;
                    }
                    if (1 == ((fspec[2] & 0b00000001)))
                    {
                        if (1 == ((fspec[3] >> 7) & 0b00000001))
                        {
                            Array.Copy(data, 7, data, 0, data.Length - 7);
                        }
                        if (1 == ((fspec[3] >> 6) & 0b00000001))
                        {
                            Array.Copy(data, 1, data, 0, data.Length - 1);
                        }
                        if (1 == ((fspec[3] >> 5) & 0b00000001))
                        {
                            Array.Copy(data, 2, data, 0, data.Length - 2);
                        }
                        if (1 == ((fspec[3] >> 4) & 0b00000001))
                        {
                            Array.Copy(data, 1, data, 0, data.Length - 1);
                        }
                        if (1 == ((fspec[3] >> 3) & 0b00000001))
                        {
                            Array.Copy(data, 2, data, 0, data.Length - 2);
                        }
                    }
                }
            }
            double lat = decoder48.latitudeDecoding(rho, theta, FL);
            double lon = decoder48.longitudeDecoding(rho, theta, FL);

            double h = 0;
            if (FL > 60)
            {
                h = FL * 100 * 0.3048;
            }
            else
            {
                h = FL_corrected * 100 * 0.3048;
            }

            Row["Latitude"] = lat;
            Row["Longitude"] = lon;
            Row["h"] = h;


            dataTable.Rows.Add(Row);
        }

        static double modeC_corrected(double Indicated_h)
        {
            double modeC_corrected = Indicated_h + (1020 - 1013.2) * 30;
            return modeC_corrected;
        }
        static byte[] Data(byte[] message, int Lenfspec)
        {
            byte[] data = new byte[message.Length - Lenfspec - 3];

            Array.Copy(message, Lenfspec + 3, data, 0, message.Length - Lenfspec - 3);

            return data;
        }
        static List<byte> Fspec(byte[] message)
        {
            List<byte> fspec = new List<byte>();
            int i = 3;
            while (i != 0)
            {
                fspec.Add(message[i]);
                if ((message[i] & 0b00000001) == 1) { i++; }
                else { i = 0; }
            }
            return fspec;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
           
        }   

    }
}
