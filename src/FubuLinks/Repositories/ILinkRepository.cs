using System.Collections.Generic;
using Raven.Client;

namespace FubuLinks.Repositories
{
    public interface ILinkRepository
    {
        IEnumerable<Link> GetAll();
        void Insert(Link link);
        void Save();
    }

    public class LinkRepository : ILinkRepository
    {
        private readonly IDocumentSession _session;

        public LinkRepository(IDocumentSession session)
        {
            _session = session;
        }

        public IEnumerable<Link> GetAll()
        {
            return _session.Query<Link>();
        }

        public void Insert(Link link)
        {
            _session.Store(link);
        }

        public void Save()
        {
            _session.SaveChanges();
        }
    }
}