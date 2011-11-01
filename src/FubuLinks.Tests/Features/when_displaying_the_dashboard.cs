using System.Collections.Generic;
using FubuLinks.Features;
using FubuLinks.Repositories;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuLinks.Tests.Features
{
    [TestFixture]
    public class when_displaying_the_dashboard : InteractionContext<GetHandler>
    {
        [Test]
        public void should_set_links()
        {
            var link = new Link
                           {
                               Id = "1234"
                           };

            MockFor<ILinkRepository>()
                .Expect(r => r.GetAll())
                .Return(new List<Link> {link});

            ClassUnderTest
                .Execute(new DashboardRequestModel())
                .Links
                .ShouldContain(l => l.Id.Equals(link.Id));
        }
    }
}