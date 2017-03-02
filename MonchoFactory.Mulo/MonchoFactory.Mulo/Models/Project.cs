using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonchoFactory.Mulo.WebApi.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string Title { get; set; }
        public string FacebookUrl { get; set; }
        public string SoundcloudUrl { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Instrument> Instruments { get; set; }

        //Location Fields

        public string LocationName { get; set; }
        public string LocationGooglePlaceId { get; set; }
        public double LocationLatitude { get; set; }
        public double LocationLongitude { get; set; }
    }
}