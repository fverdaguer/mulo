using System;
using System.Configuration;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using MonchoFactory.Mulo.WebApi;
using MonchoFactory.Mulo.WebApi.Core;
using MonchoFactory.Mulo.WebApi.Identity;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace MonchoFactory.Mulo.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
        }
    }
}