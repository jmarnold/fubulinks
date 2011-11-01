using FubuLinks.Repositories;
using StructureMap.Configuration.DSL;

namespace FubuLinks.Configuration.Bootstrapping
{
    public class DefaultConventionsRegistry : Registry
    {
        public DefaultConventionsRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.WithDefaultConventions();
                     });

            For<ILinkRepository>().Singleton().Use<LinkRepository>();
        }
    }
}