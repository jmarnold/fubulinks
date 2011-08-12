using System;
using System.Web.Routing;
using FubuLinks.Configuration;
using FubuMVC.Core;
using FubuMVC.StructureMap;

namespace FubuLinks
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            FubuApplication
                .For<FubuLinksRegistry>()
                .StructureMapObjectFactory(x => { })
                .Bootstrap(RouteTable.Routes);
        }
    }
}