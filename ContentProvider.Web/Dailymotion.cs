using System;
using System.Collections.Generic;
using System.Text;

using ContentProvider.Lib;

namespace ContentProvider.Web
{
    public class DailymotionModule : BaseCDNModule
    {
        public string Host { get; set; }
        private string[] QUALITY = { "1080", "720", "480", "360", "240", "auto" };

        public DailymotionModule(string name)
            : base(name)
        {
            Host = "http://www.dailymotion.com/";
        }

        /// <summary>
        /// browses playlists
        /// </summary>
        /// <param name="type"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public override ShowInfo[] Browse(string type, int page)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get playlist data
        /// </summary>
        /// <param name="rPath"></param>
        /// <returns></returns>
        public override ShowContents GetContentList(string link)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// From webpage, grabs highest quality availiable link
        /// </summary>
        /// <param name="link">the link path to video page</param>
        /// <returns>list of links</returns>
        public override Link[] GetContentLink(string link)
        {
            string html;
            if (link.StartsWith("/")) link = Host + link;
            if (!TryDownloadString(link, out html)) return new Link[0];

            int baseIndex = html.IndexOf("qualities");

            string url = null;
            foreach (string quality in QUALITY)
            {
                int qualityIndex = html.IndexOf("\"" + quality + "\"", baseIndex);
                url = StrUtils.ExtractString(html, "\"url\":\"", "\"", qualityIndex);
                if (url != null) break; //if a quality is found, stop
            }
            return url == null ? new Link[0] : new Link[] { new Link(MediaType.Video, url.Replace("\\/", "/")) };
        }
    }
}
