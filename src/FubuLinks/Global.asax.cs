using System;
using System.Web.Routing;
using Bottles;
using FubuLinks.Configuration;
using FubuLinks.Configuration.Bootstrapping;
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
                .StructureMapObjectFactory(x => x.AddRegistry<CoreRegistry>())
                .Bootstrap(RouteTable.Routes);

            PackageRegistry.AssertNoFailures();
        }
    }
}