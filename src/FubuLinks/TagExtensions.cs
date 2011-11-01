using System.Web.Script.Serialization;
using FubuMVC.Core.Assets.Files;
using FubuMVC.Core.Assets.Http;
using FubuMVC.Core.View;
using HtmlTags;

namespace FubuLinks
{
    public static class TagExtensions
    {
        public static HtmlTag AssetImageFor(this IFubuPage page, string path)
        {
            var url = AssetContentHandler.DetermineAssetUrl(AssetFolder.images, path);
            return new HtmlTag("img").Attr("src", url);
        } 
    }

    public interface IJsonService
    {
        string Serialize(object target);
    }

    public class JsonService : IJsonService
    {
        public string Serialize(object target)
        {
            return new JavaScriptSerializer().Serialize(target);
        }
    }

    public static class JsonExtensions
    {
        public static string SerializeToJson(this IFubuPage page, object model)
        {
            return page.Get<IJsonService>().Serialize(model);
        }
    }
}