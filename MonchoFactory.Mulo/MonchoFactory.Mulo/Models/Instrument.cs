using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonchoFactory.Mulo.WebApi.Models
{
    public class Instrument
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Musician> Musicians { get; set; }

    }
}