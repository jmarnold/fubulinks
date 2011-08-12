using FubuMVC.Core;

namespace FubuLinks.Configuration
{
    public class FubuLinksRegistry : FubuRegistry
    {
        public FubuLinksRegistry()
        {
            IncludeDiagnostics(true);

            Applies
                .ToThisAssembly();

            Views
                .TryToAttachWithDefaultConventions();
        }
    }
}