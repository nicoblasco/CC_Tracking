using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingCar.Models
{
    public class Tracking
    {
        public int ID { get; set; }
        public DateTime FechaHora { get; set; }

        public int? CameraID { get; set; }
        public virtual Camera Camera { get; set; }

        public int Velocidad { get; set; }
        
        public string Patente { get; set; }

        public string FotoUrl { get; set; }


    }
}