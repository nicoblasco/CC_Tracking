using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingCar.Models
{
    public class City
    {
        public int CityID { get; set; }
        public string Descripcion { get; set; }


        public int? StateID { get; set; }
        public virtual State State { get; set; }

        public int? CountryID { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<Camera> Cameras { get; set; }
}
}