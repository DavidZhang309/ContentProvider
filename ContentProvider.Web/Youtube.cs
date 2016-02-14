using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace ContentProvider.Lib
{
    public class YoutubeModule : BaseCDNModule
    {
        public YoutubeModule(string name)
            : base(name)
        { }

        public override ContentSeriesInfo[] Browse(string type, int page)
        {
            type = Uri.EscapeDataString(type.Replace(' ', '+'));
            string html = Client.DownloadString(string.Format("http://youtube.com/results?search_query={0}&page={1}", type, page));
            List<ContentSeriesInfo> links = new List<ContentSeriesInfo>();

            int currentIndex = html.IndexOf("class=\"section-list\">");
            while (currentIndex != -1)
            {
                string title = StrUtils.ExtractString(html, "dir=\"ltr\">", "</a><span", currentIndex);
                string link = StrUtils.ExtractString(html, "<a href=\"/", "\"", currentIndex);
                links.Add(new ContentSeriesInfo(title, link, "i.ytimg.com/vi/" + link.Substring(link.IndexOf('=') + 1) + "/mqdefault.jpg"));
                currentIndex = html.IndexOf("<button class=\"yt-uix-button yt-uix-button-size-small yt-uix-button-default yt-uix-button-empty yt-uix-button-has-icon no-icon-markup addto-button video-actions spf-nolink hide-until-delayloaded addto-watch-later-button-sign-in yt-uix-tooltip\"", html.IndexOf(link, currentIndex));
            }
            return links.ToArray();
        }

        public override ContentSeries GetContentList(string link)
        {
            string html;
            if (!TryDownloadString(link, out html))
                return new ContentSeries("None", new SeriesInstallment[0]);
            if (link.StartsWith("watch"))
            {
                string title = StrUtils.ExtractString(html, "<title>", "<", 0);
                return new ContentSeries(title, new SeriesInstallment[] { new SeriesInstallment(title, link) });
            }
            else
                return new ContentSeries("None", new SeriesInstallment[0]);
        }

        public override ContentResource[] GetContentLink(string link)
        {
            string webData;
            if (!TryDownloadString(link, out webData)) return new ContentResource[0];
            string dashLink = StrUtils.ExtractString(webData, "dashmpd\":\"", "\"", 0).Replace("\\/", "/");
            if (!TryDownloadString(dashLink, out webData)) return new ContentResource[0];
            //webData = System.IO.File.ReadAllText(".\\dash.xml");
            int currentIndex = webData.IndexOf("<Representation id");
            List<ContentResource> links = new List<ContentResource>();
            while (currentIndex != -1)
            {
                string contentLink = "http" + StrUtils.ExtractString(webData, "http", "<", currentIndex).Replace("&amp;", "&");
                links.Add(new ContentResource(MediaType.Video, contentLink));
                currentIndex = webData.IndexOf("<Representation id", currentIndex + 1);
            }
            return links.ToArray();
        }
    }
}
