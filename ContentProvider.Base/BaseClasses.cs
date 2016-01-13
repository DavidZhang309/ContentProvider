using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

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
    public struct ShowContents
    {
        public string Name { get; private set; }
        public Episode[] Episodes { get; private set; }

        public ShowContents(string name, Episode[] episodes)
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
    public struct ShowInfo
    {
        public string Name { get; private set; }
        public string SiteLink { get; private set; }
        public string ImgLink { get; private set; }

        public ShowInfo(string name, string link, string img)
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

    public struct LinkGroup
    {
        public string Metadata { get; private set; }
        public string[] Links { get; private set; }

        public LinkGroup(string metadata, string[] links)
            : this()
        {
            Metadata = metadata;
            Links = links;
        }

        public override string ToString()
        {
            return Metadata + "\n" + string.Join("\n", Links);
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
        public string Name { get; protected set; }
        protected WebClient Client { get; set; }

        public BaseCDNModule(string name)
        {
            Name = name;
            Client = new WebClient();
            Client.Encoding = Encoding.UTF8;
        }
        public abstract ShowInfo[] Browse(string type, int page);
        public abstract ShowContents GetContentList(string link);
        public abstract Link[] GetContentLink(string link);
        

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
