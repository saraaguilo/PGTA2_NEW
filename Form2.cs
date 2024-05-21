using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace PGTA2_DEF
{
    public partial class Form2 : Form
    {
        DataTable dataTable = new DataTable();
        int interval = 1;
        int index = 0;
        public Form2(DataTable dataTable)
        {
            InitializeComponent();
            this.dataTable = dataTable;
            timer1.Start();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MapProvider = GMapProviders.GoogleSatelliteMap;
            gMapControl1.NegativeMode = false;
            gMapControl1.PolygonsEnabled = true;
            gMapControl1.RoutesEnabled = true;
            gMapControl1.ShowCenter = false;
            gMapControl1.Zoom = 4;
            gMapControl1.CanDragMap = true;
            gMapControl1.MaxZoom = 10;
            gMapControl1.MinZoom = 1;
            gMapControl1.Position = new PointLatLng(41.1818, 1.0482);



        }

        public class CircleMarker : GMapMarker
        {
            private int diameter;
            private Brush brush;

            public CircleMarker(PointLatLng pos, int diameter, Color color) : base(pos)
            {
                this.diameter = diameter;
                brush = new SolidBrush(color);

                // Ajusta la posición del marcador para que el punto de referencia sea el centro del círculo
                Size = new Size(diameter, diameter);
                Offset = new Point(-diameter / 2, -diameter / 2);
            }

            public override void OnRender(Graphics g)
            {
                // Dibuja un círculo relleno en la posición del marcador
                g.FillEllipse(brush, LocalPosition.X, LocalPosition.Y, diameter, diameter);
            }
        }

        private void stop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void palante_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval /= 1;
        }

        private void palanteR_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval /= 10;
        }

        private void patras_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            this.interval = -1;
        }

        private void patrasR_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            this.interval = this.interval *(-100);
           
            
        }

        // Declaración de variables globales para mantener las posiciones anteriores
        private List<PointLatLng> posicionesAnteriores = new List<PointLatLng>();

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Verificar si hay posiciones nuevas en la DataTable
            if (posicionesAnteriores.Count < dataTable.Rows.Count)
            {
                // Obtener la última posición nueva de la DataTable
                DataRow nuevaFila = dataTable.Rows[posicionesAnteriores.Count];
                double nuevaLatitud = Convert.ToDouble(nuevaFila["Latitude"]);
                double nuevaLongitud = Convert.ToDouble(nuevaFila["Longitude"]);
                PointLatLng nuevaPosicion = new PointLatLng(nuevaLatitud, nuevaLongitud);

                // Agregar la nueva posición a las posiciones anteriores
                posicionesAnteriores.Add(nuevaPosicion);

                // Crear un marcador para la nueva posición
                CircleMarker circleMarker = new CircleMarker(nuevaPosicion, 1, Color.Red);

                // Agregar el marcador al overlay existente o crear uno nuevo si no existe
                GMapOverlay markerOverlay;
                if (gMapControl1.Overlays.Count > posicionesAnteriores.Count - 1)
                {
                    markerOverlay = gMapControl1.Overlays[posicionesAnteriores.Count - 1];
                }
                else
                {
                    markerOverlay = new GMapOverlay("markers_" + posicionesAnteriores.Count);
                    gMapControl1.Overlays.Add(markerOverlay);
                }

                markerOverlay.Markers.Add(circleMarker);
            }
        }

        
    }
}
