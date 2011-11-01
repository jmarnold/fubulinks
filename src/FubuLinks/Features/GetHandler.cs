using System.Collections.Generic;
using FubuLinks.Repositories;

namespace FubuLinks.Features
{
    public class GetHandler
    {
        private readonly ILinkRepository _linkRepository;

        public GetHandler(ILinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }

        public DashboardViewModel Execute(DashboardLinksRequestModel linksRequestModel)
        {
            return new DashboardViewModel
                       {
                           Links = _linkRepository.GetAll()
                       };
        }
    }

    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            Links = new List<Link>();
        }

        public IEnumerable<Link> Links { get; set; }
    }

    public class DashboardLinksRequestModel { }
}