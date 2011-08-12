using StructureMap.Configuration.DSL;

namespace FubuLinks.Configuration.Bootstrapping
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.LookForRegistries();
                     });
        }
    }
}