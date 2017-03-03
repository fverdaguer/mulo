using System.Web.Http;

namespace MonchoFactory.Mulo.WebApi
{
    public class FilterConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            config.Filters.Add(new AuthorizeAttribute());
        }
    }
}