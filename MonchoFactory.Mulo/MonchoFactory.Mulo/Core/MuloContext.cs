using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using MonchoFactory.Mulo.WebApi.Models;

namespace MonchoFactory.Mulo.WebApi.Core
{
    public class MuloContext : IdentityDbContext
    {
        public MuloContext() : base("MuloContext")
        {
            //Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public System.Data.Entity.DbSet<MonchoFactory.Mulo.WebApi.Models.Project> Projects { get; set; }
    }
}