using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Attendance_Integration
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Formatters.XmlFormatter.UseXmlSerializer = true;
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", (object)new
            {
                id = RouteParameter.Optional
            });
        }
    }
}
