using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

//using HttpDOMLoader;
//using System.Threading;

namespace ContentProvider.Lib
{
    public struct Episode
    {
        public string Name { get; private set; }
        public string SiteLink { get; private set; }

        public Episode(string name, string link)
            : this()
        {
            Name = name;
            SiteLink = link;
        }

        public override string ToString()
        {
            return Name + "\t" + SiteLink;
        }
    }
    public struct Show
    {
        public string Name { get; private set; }
        public Episode[] Episodes { get; private set; }

        public Show(string name, Episode[] episodes)
            : this()
        {
            Name = name;
            Episodes = episodes;
        }

        public override string ToString()
        {
            string result = Name + "\n";
            result += string.Join("\n", Episodes);
            return result;
        }
    }
    public struct ShowLink
    {
        public string Name { get; private set; }
        public string SiteLink { get; private set; }
        public string ImgLink { get; private set; }

        public ShowLink(string name, string link, string img)
            : this()
        {
            Name = name;
            SiteLink = link;
            ImgLink = img;
        }

        public override string ToString()
        {
 	         return Name + "\t" + SiteLink + "\t" + ImgLink;
        }
    }

    public enum MediaType { Image, Video, Audio, Other, Subtitle }
    public struct Link
    {
        public MediaType Media { get; private set; }
        public string LinkString { get; private set; }

        public Link(MediaType type, string link)
            : this()
        {
            Media = type;
            LinkString = link;
        }

        public override string ToString()
        {
            return LinkString;
        }
    }

    public abstract class BaseCDNModule
    {
        public static string DOMHOST;

        public bool UseHttpDOM { get; set; }
        public string Name { get; protected set; }
        protected WebClient Client { get; set; }

        public BaseCDNModule(string name)
        {
            UseHttpDOM = true;
            Name = name;
            Client = new WebClient();
            Client.Encoding = Encoding.UTF8;
        }
        public abstract ShowLink[] Browse(string type, int page);
        public abstract Show GetContentList(string rPath);
        public abstract Link[] GetContentLink(string rPath);
        public abstract string Request(string reqPath, NameValueCollection query);

        protected bool TryDownloadString(string url, out string html)
        {
            try
            {
                html = Client.DownloadString(url);
                return true;
            }
            catch (WebException ex)
            {
                html = null;
                return false;
            }
        }
        protected bool TryDownloadDOM(string url, out string html)
        {
            return TryDownloadString(DOMHOST + "domget?url=" + url, out html);
        }

        public override string ToString()
        {
            return Name + " Module";
        }

        public string htmlEnc(string html)
        {
            return WebUtility.HtmlEncode(html);
        }
        public string htmlDec(string html)
        {
            return WebUtility.HtmlDecode(html);
        }
    }
}
