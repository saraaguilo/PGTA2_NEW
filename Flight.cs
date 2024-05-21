using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGTA2_DEF
{
    class Flight
    {
        string ID;
        double[] coordinates;
        TimeSpan time;

        public Flight(string ID, double[] coordinates, TimeSpan time)
        {
            this.ID = ID;
            this.coordinates = coordinates;
            this.time = time;
        }

        public string getID() { return ID; }
        public double[] getcoordinates() { return coordinates; }
        public TimeSpan gettime() { return time; }
    }

}

