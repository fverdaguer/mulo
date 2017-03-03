using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonchoFactory.Mulo.WebApi.Models
{
    public class Musician
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Instrument> Instruments { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }

        //Location Fields

        public string LocationName { get; set; }
        public string LocationGooglePlaceId { get; set; }
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; }
    }
}