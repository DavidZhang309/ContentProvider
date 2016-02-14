using System;
using System.Collections;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace ContentProvider.Lib
{
    public abstract class BaseDataContainer
    {
        protected NameValueCollection Metadata { get; set; }

        public BaseDataContainer()
        {
            Metadata = new NameValueCollection();
        }
        public BaseDataContainer(NameValueCollection metadata)
        {
            Metadata = metadata;
        }

        private string ToString(string delimiter, string format)
        {
            string[] tags = format.Split(',');
            string[] result = new string[tags.Length];

            for (int i = 0; i < tags.Length; i++)
                result[i] = Metadata.Get(tags[i]);
            return string.Join(delimiter, result);
        }

        public string ToCSV(string format)
        {
            return ToString(",", format);
        }
        public string ToTSV(string format)
        {
            return ToString("\t", format);
        }
    }

    public class SeriesInstallment : BaseDataContainer
    {
        public string Name
        {
            get { return Metadata.Get("name"); }
        }
        public string SiteLink
        {
            get { return Metadata.Get("link"); }
        }

        public SeriesInstallment(string name, string link)
            : base ()
        {
            Metadata.Add("name", name);
            Metadata.Add("link", link);
        }

        //tmp, later will be CSV
        public override string ToString()
        {
            return ToTSV("name,link");
        }
    }
    public class ContentSeries : BaseDataContainer
    {
        public string Name
        {
            get { return Metadata.Get("name"); }
        }
        public SeriesInstallment[] Installments
        {
            get;
            private set;
        }

        public ContentSeries(string name, SeriesInstallment[] installments)
        {
            Metadata.Add("name", name);
            Installments = installments;
        }

        public override string ToString()
        {
            string result = ToTSV("name") + "\n";
            result += string.Join("\n", Installments.GetEnumerator());
            return result;
        }
    }
    public class ContentSeriesInfo : BaseDataContainer
    {
        public string Name { get { return Metadata.Get("name"); } }
        public string Link { get { return Metadata.Get("link"); } }
        public string ImgLink { get { return Metadata.Get("img"); } }

        public ContentSeriesInfo(string name, string link, string img)
        {
            Metadata.Add("name", name);
            Metadata.Add("link", link);
            Metadata.Add("img", img);
        }

        public override string ToString()
        {
            return ToTSV("name,link,img");
        }
    }
    public class ContentResource : BaseDataContainer
    {
        public string Link
        {
            get { return Metadata.Get("link"); }
        }
        public MediaType Media {
            get { return (MediaType)Enum.Parse(typeof(MediaType), Metadata.Get("type")); }
        } 

        public ContentResource(MediaType resourceType, string link)
        {
            Metadata.Set("type", resourceType.ToString());
            Metadata.Set("link", link);
        }

        public override string ToString()
        {
            return Link;
        }
    }

    public enum MediaType { Image, Video, Audio, Other, Subtitle }

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
        public abstract ContentSeriesInfo[] Browse(string type, int page);
        public abstract ContentSeries GetContentList(string link);
        public abstract ContentResource[] GetContentLink(string link);
        
        protected bool TryDownloadString(string url, out string html)
        {
            try
            {
                html = Client.DownloadString(url);
                return true;
            }
            catch (WebException)
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
