using FubuLinks.Handlers.Links.Create;
using FubuLinks.Repositories;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuLinks.Tests.Handlers
{
    [TestFixture]
    public class when_creating_a_link : InteractionContext<PostHandler>
    {
        [Test]
        public void should_insert_link_and_save()
        {
            var inputModel = new CreateLinkInputModel
                                 {
                                     OriginalUrl =
                                         "https://github.com/jmarnold/fubulinks/blob/master/src/FubuLinks.Tests/Properties/AssemblyInfo.cs"
                                 };

            MockFor<ILinkRepository>()
                .Expect(r => r.Insert(new Link {OriginalUrl = inputModel.OriginalUrl}));

            MockFor<ILinkRepository>()
                .Expect(r => r.Save());

            ClassUnderTest
                .Execute(inputModel);

            VerifyCallsFor<ILinkRepository>();
        }

        [Test]
        public void should_shorten_url()
        {
            var inputModel = new CreateLinkInputModel
                                 {
                                     OriginalUrl =
                                         "https://github.com/jmarnold/fubulinks/blob/master/src/FubuLinks.Tests/Properties/AssemblyInfo.cs"
                                 };

            var shortenedUrl = "1234";
            MockFor<IUrlShortener>()
                .Expect(s => s.Shorten(inputModel.OriginalUrl))
                .Return(shortenedUrl);

            MockFor<ILinkRepository>()
                .Expect(r => r.Insert(new Link { OriginalUrl = inputModel.OriginalUrl }))
                .WhenCalled(mi =>
                                {
                                    var link = (Link) mi.Arguments[0];
                                    link
                                        .ShortenedUrl
                                        .ShouldEqual(shortenedUrl);
                                });

            ClassUnderTest
                .Execute(inputModel);

            VerifyCallsFor<IUrlShortener>();
        }
    }
}