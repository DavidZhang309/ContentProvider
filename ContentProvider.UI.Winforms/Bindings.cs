using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;


using ContentProvider.Lib;

namespace ContentProvider.UI.Winforms
{
    public struct SeriesInfoBinding
    {
        public ContentSeriesInfo Show { get; private set; }

        public SeriesInfoBinding(ContentSeriesInfo show)
            : this()
        {
            Show = show;
        }

        public override string ToString()
        {
            return WebUtility.HtmlDecode(Show.Name);
        }
    }

    public struct SeriesBinding
    {
        public ContentSeries Show { get; private set; }

        public SeriesBinding(ContentSeries show)
            : this()
        {
            Show = show;
        }

        public override string ToString()
        {
            return WebUtility.HtmlDecode(Show.Name);
        }
    }

    public struct InstallmentBinding
    {
        public SeriesInstallment Episode { get; private set; }

        public InstallmentBinding(SeriesInstallment episode)
            : this()
        {
            Episode = episode;
        }

        public override string ToString()
        {
            return WebUtility.HtmlDecode(Episode.Name);
        }
    }
}
