using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using ContentProvider.Lib;

namespace ContentProvider.UI.Online
{
    public interface IWebServiceModule
    {
        string Name { get; }
        string Request(string path, NameValueCollection query);
    }

    public class ContentProviderModule : IWebServiceModule
    {
        public Dictionary<string, BaseCDNModule> Modules { get; private set; }
        public string Name
        {
            get;
            private set;
        }

        public ContentProviderModule(string name)
        {
            Name = name;
            Modules = new Dictionary<string, BaseCDNModule>();
        }

        public string Request(string path, NameValueCollection query)
        {
            string[] pathing = path.Split('/');
            string requestPath = string.Join("/", pathing, 1, pathing.Length - 1);
            string result = "";

            string moduleName = pathing[0];
            if (Modules.ContainsKey(moduleName))
            {
                BaseCDNModule module = Modules[moduleName];
                switch (requestPath)
                {
                    case "browse":
                        string queryString = query["query"];
                        int page = Convert.ToInt32(query["page"]);
                        ContentSeriesInfo[] infos = module.Browse(queryString, page);
                        result = string.Join("\n", (object[])infos);
                        break;
                    case "get_list":
                        string listPath = query["path"];
                        ContentSeries series = module.GetContentList(listPath);
                        result = series.Name + "\n" + string.Join("\n", (object[])series.Installments);
                        break;
                    case "get_link":
                        string linkPath = query["path"];
                        result = string.Join("\n", (object[])module.GetContentLink(linkPath));
                        break;
                    default:
                        Console.WriteLine("-> Bad request: " + requestPath);
                        break;
                }
            }

            return result;
        }
    }

    public class ResourceModule : IWebServiceModule
    {
        public string Name
        {
            get;
            private set;
        }
        public Dictionary<string, string> Resources { get; private set; }

        public ResourceModule(string name)
        {
            Name = name;
            Resources = new Dictionary<string, string>();
        }
        public string Request(string path, NameValueCollection query)
        {
            string[] pathing = path.Split('/');
            string requestPath = string.Join("/", pathing, 1, pathing.Length - 1);

            string resourceName = pathing[0];
            if (Resources.ContainsKey(resourceName))
                return File.ReadAllText(Resources[resourceName]);
            else
                return "";
        }
    }

    public class APIModule : IWebServiceModule
    {
        public string Name
        {
            get;
            private set;
        }
        public ContentProviderModule ContentModule { get; private set; }

        public APIModule(string name, ContentProviderModule content)
        {
            Name = name;
            ContentModule = content;
        }

        public string Request(string path, NameValueCollection query)
        {
            string[] pathing = path.Split('/');
            string requestPath = string.Join("/", pathing, 1, pathing.Length - 1);
            string result = "";
            
            switch (pathing[0])
            {
                case "get_modules":
                    string[] moduleNames = new string[ContentModule.Modules.Count];
                    int count = 0;
                    foreach (string name in ContentModule.Modules.Keys)
                        moduleNames[count++] = name;
                    result = string.Join("\n", (object[])moduleNames);
                    break;
            }

            return result;
        }
    }

    class WebServiceProvider
    {
        HttpListener listener = new HttpListener();
        public Dictionary<string, IWebServiceModule> Modules { get; private set; }
        public Encoding OutputEncoding { get; set; }

        public WebServiceProvider()
        {
            OutputEncoding = Encoding.UTF8;
            Modules = new Dictionary<string, IWebServiceModule>();
            listener.Prefixes.Add("http://*:6672/");
        }

        public void Start()
        {
            listener.Start();
            listener.BeginGetContext(new AsyncCallback(HttpRecieve), null);
        }

        private static bool WriteToOutputStream(byte[] data, HttpListenerContext webContext)
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

        private void HttpRecieve(IAsyncResult AsyncResult)
        {
            HttpListenerContext webContext = listener.EndGetContext(AsyncResult);
            listener.BeginGetContext(new AsyncCallback(HttpRecieve), null);

            string[] pathing = webContext.Request.Url.LocalPath.Substring(1).Split('/');
            string requestPath = string.Join("/", pathing, 1, pathing.Length - 1);

            if (Modules.ContainsKey(pathing[0]))
            {
                string result = Modules[pathing[0]].Request(requestPath, webContext.Request.QueryString);
                WriteToOutputStream(OutputEncoding.GetBytes(result), webContext);                
            }
            
            webContext.Response.OutputStream.Close();
        }
    }
}
