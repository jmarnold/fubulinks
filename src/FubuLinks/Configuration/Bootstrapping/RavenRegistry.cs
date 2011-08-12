using Raven.Client;
using Raven.Client.Embedded;
using StructureMap.Configuration.DSL;

namespace FubuLinks.Configuration.Bootstrapping
{
    public class RavenRegistry : Registry
    {
        public RavenRegistry()
        {
            var viewStore = new EmbeddableDocumentStore {RunInMemory = true, UseEmbeddedHttpServer = true};
            viewStore.Initialize();

            For<IDocumentStore>()
                .Singleton()
                .Use(viewStore);

            For<IDocumentSession>()
                .Use(ctx => ctx.GetInstance<IDocumentStore>().OpenSession());
        }
    }
}