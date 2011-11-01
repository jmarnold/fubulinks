using FubuLinks.Features;
using FubuMVC.Core;
using FubuMVC.Spark;
using FubuMVC.Validation;

namespace FubuLinks.Configuration
{
    public class FubuLinksRegistry : FubuRegistry
    {
        public FubuLinksRegistry()
        {
            IncludeDiagnostics(true);

            Applies
                .ToThisAssembly();

            this.UseSpark();

            ApplyHandlerConventions(typeof (HandlersMarker));

            this.Validation(x => x.Actions.Include(call => call.HasInput && call.InputType().Name.Contains("Input")));

            Output
                .ToJson
                .WhenCallMatches(call => call.OutputType().Name.StartsWith("Json"));

            Views
                .TryToAttachWithDefaultConventions();
        }
    }
}