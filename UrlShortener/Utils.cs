using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener
{
    public static class Utils
    {
        public static string? FormatAsShortUrl(this string? url)
        {
            if(url is null)
            {
                return url;
            }

            Uri uri = new(url);

            return uri.Host + uri.PathAndQuery;
        }
    }
}
