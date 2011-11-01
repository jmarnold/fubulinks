using System;
using FubuLinks.Repositories;
using FubuValidation;

namespace FubuLinks.Features.Links.Create // links/create (POST)
{
    public class PostHandler
    {
        private readonly ILinkRepository _repository;
        private readonly IUrlShortener _urlShortener;

        public PostHandler(ILinkRepository repository, IUrlShortener urlShortener)
        {
            _repository = repository;
            _urlShortener = urlShortener;
        }

        public JsonResponse Execute(CreateLinkInputModel inputModel)
        {
            var response = new JsonResponse { Success = true };
            var link = new Link
                           {
                               DateAdded = DateTime.Now,
                               OriginalUrl = inputModel.OriginalUrl,
                               ShortenedUrl = _urlShortener.Shorten(inputModel.OriginalUrl)
                           };
            _repository.Insert(link);
            _repository.Save();

            return response;
        }
    }

    public class CreateLinkInputModel
    {
        [Required]
        public string OriginalUrl { get; set; }
    }

    public interface IUrlShortener
    {
        string Shorten(string input);
    }

    public class UrlShortener : IUrlShortener
    {
        public string Shorten(string input)
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}