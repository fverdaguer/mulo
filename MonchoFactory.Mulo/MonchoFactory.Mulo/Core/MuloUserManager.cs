using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MonchoFactory.Mulo.WebApi.Core
{
    public class MuloUserManager : UserManager<IdentityUser>
    {
        public MuloUserManager() : base(new MuloUserStore())
        {
            
        }
    }
}