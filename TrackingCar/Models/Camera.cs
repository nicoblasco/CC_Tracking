using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingCar.Models
{
    public enum Point
    {
        N, S, E, O
    }
    public class Camera
    {
        public int CameraID { get; set; }
        public String Numero { get; set; }
        public String Direccion { get; set; }

        public int? CountryID { get; set; }
        public virtual Country Country { get; set; }

        public int? StateID { get; set; }
        public virtual State State { get; set; }

        public int? CityID { get; set; }
        public virtual City City { get; set; }

        public string MapCoordenadaX { get; set; }

        public string MapCoordenadaY { get; set; }

        public Point? Point { get; set; }

        public int Limite { get; set; }

        public virtual ICollection<Tracking> Trakings { get; set; }

    }
}