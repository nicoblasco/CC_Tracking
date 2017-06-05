using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingCar.Models
{
    public class Country
    {
        public int CountryID { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<State> States { get; set; }

        public virtual ICollection<State> Cities { get; set; }
        public virtual ICollection<Camera> Cameras { get; set; }
    }
}