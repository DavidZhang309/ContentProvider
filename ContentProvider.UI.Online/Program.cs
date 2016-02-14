using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

using ContentProvider.Lib;
using ContentProvider.Web;

namespace ContentProvider.UI.Online
{
    class Program
    {
        static WebClient client;

        static bool WriteToOutputStream(byte[] data, HttpListenerContext webContext)
        {
            try
            {
                webContext.Response.OutputStream.Write(data, 0, data.Length);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        static bool TryDownloadString(string url, out string html)
        {
            try
            {
                html = client.DownloadString(url);
                return true;
            }
            catch (WebException)
            {
                html = null;
                return false;
            }
        }

        static void OldMain(string[] args)
        {
            Dictionary<string, BaseCDNModule> modules = new Dictionary<string, BaseCDNModule>();
            modules.Add("dailymotion", new DailymotionModule("dailymotion"));
            
            modules.Add("youtube", new YoutubeModule("youtube"));

            if (Directory.Exists(".\\Modules\\"))
                foreach (string file in Directory.GetFiles(".\\Modules\\"))
                {
                    string name = Path.GetFileNameWithoutExtension(file);
                    modules.Add(name, new ScriptedProvider(name, file));
                    Console.WriteLine("Loaded Module: " + name);
                }

            client = new WebClient();
            client.Encoding = Encoding.UTF8;
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://*:6672/");

            listener.Start();

            char[] pathSplit = { '/' };
            while (listener.IsListening)
            {
                HttpListenerContext webContext = listener.GetContext();
                webContext.Response.ContentEncoding = Encoding.UTF8;
                
                string[] pathing = webContext.Request.Url.LocalPath.Split(pathSplit, StringSplitOptions.RemoveEmptyEntries);
                //string requestType = string.Join("/", pathing, 1, pathing.Length - 1);
                string requestPath = string.Join("/", pathing);
                string moduleName = pathing[0];
                Console.WriteLine("{0} - {1}: {2}", DateTime.Now, webContext.Request.RemoteEndPoint.Address, requestPath);
                bool sucess = false;

                if (modules.ContainsKey(moduleName))
                {
                    requestPath = string.Join("/", pathing, 1, pathing.Length - 1);
                    BaseCDNModule module = modules[moduleName];
                    Console.WriteLine("-> Module " + moduleName);
                    string result = null;// module.Request(requestPath, webContext.Request.QueryString);
                    switch (requestPath)
                    {
                        case "browse":
                            string query = webContext.Request.QueryString["query"];
                            int page = Convert.ToInt32(webContext.Request.QueryString["page"]);
                            ContentSeriesInfo[] infos = module.Browse(query, page);
                            result = string.Join("\n", (object[])infos); 
                            break;
                        case "get_list":
                            string listPath = webContext.Request.QueryString["path"];
                            ContentSeries series = module.GetContentList(listPath);
                            result = series.Name + "\n" + string.Join("\n", (object[])series.Installments);
                            break;
                        case "get_link":
                            string linkPath = webContext.Request.QueryString["path"];
                            result = string.Join("\n", (object[])module.GetContentLink(linkPath));
                            break;
                        default:
                            Console.WriteLine("-> Bad request: " + requestPath);
                            break;
                    }
                    if (result != null)
                    {
                        byte[] data = Encoding.UTF8.GetBytes(result);
                        sucess = WriteToOutputStream(data, webContext);
                    }
                }
                webContext.Response.OutputStream.Close();
            }
        }

        static void Main(string[] args)
        {
            WebServiceProvider provider = new WebServiceProvider();
            ContentProviderModule contentModule = new ContentProviderModule("content");
            provider.Modules.Add(contentModule.Name, contentModule);
            ResourceModule resourceModule = new ResourceModule("resource");
            provider.Modules.Add(resourceModule.Name, resourceModule);
            APIModule apiModule = new APIModule("api", contentModule);
            provider.Modules.Add(apiModule.Name, apiModule);

            contentModule.Modules.Add("dailymotion", new DailymotionModule("dailymotion"));
            contentModule.Modules.Add("youtube", new YoutubeModule("youtube"));

            if (Directory.Exists(".\\Modules\\"))
                foreach (string file in Directory.GetFiles(".\\Modules\\"))
                {
                    string name = Path.GetFileNameWithoutExtension(file);
                    contentModule.Modules.Add(name, new ScriptedProvider(name, file));
                    Console.WriteLine("Loaded Module: " + name);
                }
            if (Directory.Exists(".\\Resource\\"))
                foreach (string file in Directory.GetFiles(".\\Resource\\"))
                {
                    string name = Path.GetFileName(file);
                    resourceModule.Resources.Add(name, file);
                }

            provider.Start();

            ManualResetEvent waiter = new ManualResetEvent(false);
            waiter.WaitOne();
        }
    }
}
