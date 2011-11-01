using FubuLinks.Repositories;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuLinks.Tests.Repositories
{
    [TestFixture]
    public class LinkRepositoryTester
    {
        private LinkRepository _repository;

        [SetUp]
        public void before_each()
        {
            _repository = new LinkRepository();
            _repository.Clear();
        }

        [Test]
        public void should_insert_links()
        {
            _repository.Insert(new Link());
            _repository.Insert(new Link());
            _repository.Insert(new Link());
            _repository.GetAll().ShouldHaveCount(3);
        }
    }
}