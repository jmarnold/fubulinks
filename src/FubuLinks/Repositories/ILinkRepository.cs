using System.Collections.Generic;

namespace FubuLinks.Repositories
{
    public interface ILinkRepository
    {
        IEnumerable<Link> GetAll();
        void Insert(Link link);
        void Save();
    }
}