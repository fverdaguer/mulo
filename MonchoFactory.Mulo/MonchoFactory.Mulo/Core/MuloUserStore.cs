using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MonchoFactory.Mulo.WebApi.Core
{
    public class MuloUserStore : UserStore<IdentityUser>
    {
        public MuloUserStore() : base(new MuloContext())
        {
            
        }
    }
}