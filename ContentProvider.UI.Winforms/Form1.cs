using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

using ContentProvider.Lib;
using ContentProvider.Web;

namespace ContentProvider.UI.Winforms
{
    public partial class Form1 : Form
    {
        public const string LOADMORE = "---Load more---";
        private WebClient client;
        private string lastSearch = "";
        private int currentPage = 1;   
        private Bitmap[] pics;

        public Form1()
        {
            InitializeComponent();

            client = new WebClient();

            moduleCombo.Items.Add(new DailymotionModule("dailymotion"));
            moduleCombo.Items.Add(new YoutubeModule("Youtube"));

            if (Directory.Exists(".\\Modules\\"))
                foreach (string file in Directory.GetFiles(".\\Modules\\"))
                    moduleCombo.Items.Add(new ScriptedProvider(Path.GetFileNameWithoutExtension(file), file)); 
        }

        private void NextBrowse()
        {
            BaseCDNModule module = moduleCombo.SelectedItem as BaseCDNModule;
            ShowInfo[] links = module.Browse(searchBox.Text, currentPage);
            foreach (ShowInfo link in links)
                browseBox.Items.Add(new ShowLinkBinding(link));
        }
        private void ChangeListing(ShowContents show)
        {
            listingBox.Items.Clear();
            foreach (Episode episode in show.Episodes)
                listingBox.Items.Add(new EpisodeBinding(episode));
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            browseBox.Items.Clear();
            currentPage = 1;
            lastSearch = searchBox.Text;
            NextBrowse();
            browseBox.Items.Add(LOADMORE);
        }

        private void browseBox_DoubleClick(object sender, EventArgs e)
        {
            if (browseBox.SelectedIndex == -1) return;
            else if (browseBox.SelectedItem is string && browseBox.SelectedItem.ToString() == LOADMORE)
            {
                browseBox.Items.Remove(LOADMORE);
                currentPage++;
                NextBrowse();
                browseBox.Items.Add(LOADMORE);
            }
            else
            {
                System.Console.WriteLine(browseBox.SelectedItem.GetType());
                ShowInfo link = ((ShowLinkBinding)browseBox.SelectedItem).Show;
                BaseCDNModule module = moduleCombo.SelectedItem as BaseCDNModule;

                titleLabel.Text = link.Name;
                //Load cover image
                Stream stream = client.OpenRead(link.ImgLink);
                coverPicture.Image = new Bitmap(stream);
                stream.Close();
                //change listings
                ChangeListing(module.GetContentList(link.SiteLink));
            }
        }
        private void listingBox_DoubleClick(object sender, EventArgs e)
        {
            if (listingBox.SelectedIndex == -1) return;
            BaseCDNModule module = moduleCombo.SelectedItem as BaseCDNModule;
            Episode ep = ((EpisodeBinding)listingBox.SelectedItem).Episode;
            Link[] links = module.GetContentLink(ep.SiteLink);

            mediaTitleLabel.Text = ep.Name;
            linkBox.Items.Clear();
            foreach (Link link in links)
                linkBox.Items.Add(link);
            pics = new Bitmap[links.Length];
            tabControl1.SelectedIndex = 1;
        }

        private void linkBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (linkBox.SelectedIndex == -1) return;
            Link link = (Link)linkBox.SelectedItem;

            if (link.Media == MediaType.Video || link.Media == MediaType.Audio)
            {
                if (!axWindowsMediaPlayer1.Visible)
                {
                    axWindowsMediaPlayer1.Visible = true;
                    picBox.Visible = false;
                }
                axWindowsMediaPlayer1.URL = link.LinkString;
                return;
            }

            if (!picBox.Visible)
            {
                picBox.Visible = true;
                axWindowsMediaPlayer1.Visible = false;
            }
            if (pics[linkBox.SelectedIndex] == null)
            {
                
                Stream iStream = client.OpenRead(link.LinkString);
                Bitmap image = new Bitmap(iStream);
                iStream.Close();
                pics[linkBox.SelectedIndex] = image;
                picBox.Image = image;
            }
            else
            {
                picBox.Image = pics[linkBox.SelectedIndex];
            }
            
        }

        private void manualButton_Click(object sender, EventArgs e)
        {
            BaseCDNModule module = moduleCombo.SelectedItem as BaseCDNModule;
            ContentProvider.Lib.ShowContents show = module.GetContentList(manualBox.Text);

            ChangeListing(show);
        }

        private void moduleCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            browseBox.Items.Clear();
            listingBox.Items.Clear();
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            if (linkBox.SelectedIndex == -1) return;
            Link ep = (Link)linkBox.SelectedItem;
            Uri url = new Uri(ep.LinkString);
            string ext = Path.GetExtension(url.AbsolutePath);
            saveDialog.FileName = mediaTitleLabel.Text + ext;
            DialogResult result = saveDialog.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                
                client.DownloadFileAsync(url, saveDialog.FileName);
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openDialog.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Yes)
                moduleCombo.Items.Add(new ScriptedProvider(Path.GetFileNameWithoutExtension(openDialog.FileName), openDialog.FileName));
            moduleCombo.SelectedIndex = moduleCombo.Items.Count - 1;
        }
    }
}
