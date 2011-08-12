using System;

namespace FubuLinks
{
    public class Link
    {
        public string Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string ShortenedUrl { get; set; }
        public string OriginalUrl { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Link)) return false;
            return Equals((Link) obj);
        }

        public bool Equals(Link other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.OriginalUrl, OriginalUrl);
        }

        public override int GetHashCode()
        {
            return OriginalUrl.GetHashCode();
        }
    }
}