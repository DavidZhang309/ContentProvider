using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using NLua;

//using CoreFramework.Scripting.Lua;

//namespace HttpContentProvider.Lib
//{
//    public class ScriptedProvider : BaseCDNModule
//    {
//        public string ScriptPath { get; private set; }
//        WLLuaSandbox sandbox;

//        public ScriptedProvider(string name, string scriptPath)
//            : base(name)
//        {
//            sandbox = new WLLuaSandbox();
//            sandbox.Enviroment["downloadString"] = new Func<string, string>(downloadStr);
//            sandbox.Enviroment["createShowLink"] = new Func<string, string, string, ShowLink>(createShowLink);
//            ScriptPath = scriptPath;
//        }

//        private string downloadStr(string url)
//        {
//            string html;
//            if (TryDownloadString(url, out html))
//                return html;
//            else
//                return "";
//        }
//        private ShowLink createShowLink(string name, string link, string imgLink)
//        {
//            return new ShowLink(name, link, imgLink);
//        }

//        public override ShowLink[] Browse(string type, int page)
//        {
//            sandbox.Execute(string.Format("_list = browse(\"{0}\", {1})", type, page));
//            LuaTable list = sandbox.Enviroment["_list"] as LuaTable;
//            throw new NotImplementedException();
//        }

//        public override Show GetContentList(string rPath)
//        {
//            throw new NotImplementedException();
//        }

//        public override Link[] GetContentLink(string rPath)
//        {
//            throw new NotImplementedException();
//        }

//        public override string Request(string reqPath, System.Collections.Specialized.NameValueCollection query)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
