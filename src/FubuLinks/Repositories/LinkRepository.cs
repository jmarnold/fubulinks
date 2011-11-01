using System.Collections.Generic;

namespace FubuLinks.Repositories
{
    public class LinkRepository : ILinkRepository
    {
        private readonly List<Link> _links = new List<Link>();
        
        public IEnumerable<Link> GetAll()
        {
            return _links;
        }

        public void Insert(Link link)
        {
            _links.Add(link);
        }

        public void Save()
        {
            //no-op
        }

        // Just here for testing since this is in-memory
        public void Clear()
        {
            _links.Clear();
        }
    }
}