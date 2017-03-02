using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MonchoFactory.Mulo.WebApi.Core
{
    public class Initializer : MigrateDatabaseToLatestVersion<MuloContext, Configuration>
    {

    }
}