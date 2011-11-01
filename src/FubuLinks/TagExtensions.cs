using FubuMVC.Core.Assets.Files;
using FubuMVC.Core.Assets.Http;
using FubuMVC.Core.View;
using HtmlTags;

namespace FubuLinks
{
    public static class TagExtensions
    {
        public static HtmlTag ImageFor(this IFubuPage page, string path)
        {
            var url = AssetContentHandler.DetermineAssetUrl(AssetFolder.images, path);
            return new HtmlTag("img").Attr("src", url);
        } 
    }
}