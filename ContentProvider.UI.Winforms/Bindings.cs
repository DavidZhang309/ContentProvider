using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;


using ContentProvider.Lib;

namespace ContentProvider.UI.Winforms
{
    public struct ShowLinkBinding
    {
        public ShowInfo Show { get; private set; }

        public ShowLinkBinding(ShowInfo show)
            : this()
        {
            Show = show;
        }

        public override string ToString()
        {
            return WebUtility.HtmlDecode(Show.Name);
        }
    }

    public struct ShowBinding
    {
        public ShowContents Show { get; private set; }

        public ShowBinding(ShowContents show)
            : this()
        {
            Show = show;
        }

        public override string ToString()
        {
            return WebUtility.HtmlDecode(Show.Name);
        }
    }

    public struct EpisodeBinding
    {
        public Episode Episode { get; private set; }

        public EpisodeBinding(Episode episode)
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
