namespace UrlShortener.Models
{
    public class Url
    {
        public Guid Id { get; set; }
        public int TimesAccessed { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
