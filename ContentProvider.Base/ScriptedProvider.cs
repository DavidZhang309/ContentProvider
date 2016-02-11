using System;
using System.Reflection;
using NLua;

using CoreFramework;
//using CoreFramework.Scripting.Lua;

namespace ContentProvider.Lib
{
    public class ScriptedProvider : BaseCDNModule
    {
        #region "LUA CODE"
        private const string BUILDSANDBOX = @"{0} = {{ 
    assert = assert,
    error = error,
    ipairs = ipairs,
    next = next, 
    pairs = pairs,
    pcall = pcall,
    print = print,
    select = select,
    tonumber = tonumber,
    tostring = tostring,
    type = type,
    unpack = unpack,
    _VERSION = _VERSION,
    xpcall = xpcall,
    coroutine = {{
        create = coroutine.create,
        resume = coroutine.resume,
        running = coroutine.running,
        status = coroutine.status,
        wrap = coroutine.wrap,
        yield = coroutine.yield }},
    string = {{
        byte = string.byte,
        char = string.char,
        find = string.find,
        format = string.format,
        gmatch = string.gmatch,
        gsub = string.gsub,
        len = string.len,
        lower = string.lower,
        match = string.match,
        rep = string.rep,
        reverse = string.reverse,
        sub = string.sub,
        upper = string.upper }},
    table = {{
        insert = table.insert,
        maxn = table.maxn,
        remove = table.remove,
        sort = table.sort }},
    math = {{
        abs = math.abs,
        acos = math.acos,
        asin = math.asin,
        atan = math.atan,
        atan2 = math.atan,
        ceil = math.ceil,
        cos = math.cos,
        cosh = math.cosh,
        deg = math.deg,
        exp = math.exp,
        floor = math.floor,
        fmod = math.fmod,
        frexp = math.frexp,
        huge = math.huge,
        ldexp = math.ldexp,
        log = math.log,
        log10 = math.log10,        
        max = math.max,
        min = math.min,
        modf = math.modf,
        pi = math.pi,
        pow = math.pow,
        rad = math.rad,
        random = math.random,
        randomseed = math.randomseed,
        sin = math.sin,
        sinh = math.sinh,
        sqrt = math.sqrt,
        tan = math.tan,
        tanh = math.tanh }},
    os = {{
        clock = os.clock,
        difftime = os.difftime,
        time = os.time }}
    }}";
        #endregion

        private const string SANDBOXTABLENAME = "SB";
        private const string MODULETABLENAME = "MOD";

        private string modulePath;
        
        protected NLua.Lua Interpreter { get; private set; }

        protected LuaTable SBEnviroment { get; private set; }

        public bool AssertErrors { get; set; }

        public ScriptedProvider(string name, string modulePath)
            : base(name)
        {
            this.modulePath = modulePath;
            Reload();
        }

        protected void AddToLib(string funcName, object target, MethodBase method)
        {
            Interpreter.RegisterFunction(SANDBOXTABLENAME + "." + funcName, target, method);
        }

        private void LoadLib()
        {
            Type thisType = GetType();
            
            AddToLib("string.indexOf", this, thisType.GetMethod("IndexOf", BindingFlags.NonPublic | BindingFlags.Instance));
            AddToLib("string.lastIndexOf", this, thisType.GetMethod("LastIndexOf", BindingFlags.NonPublic | BindingFlags.Instance));
            AddToLib("string.split", this, thisType.GetMethod("Split", BindingFlags.NonPublic | BindingFlags.Instance));
            AddToLib("string.extract", null, typeof(StrUtils).GetMethod("ExtractString"));
            AddToLib("downloadString", Client, Client.GetType().GetMethod("DownloadString", new Type[] { typeof(string) }));
            Interpreter.NewTable(SANDBOXTABLENAME + ".data");
            AddToLib("data.createShowInfo", this, thisType.GetMethod("CreateShowInfo", BindingFlags.NonPublic | BindingFlags.Instance));
            AddToLib("data.createEpisode", this, thisType.GetMethod("CreateEpisode", BindingFlags.NonPublic | BindingFlags.Instance));
            AddToLib("data.createShowContents", this, thisType.GetMethod("CreateShowContents", BindingFlags.NonPublic | BindingFlags.Instance));
            AddToLib("data.createLink", this, thisType.GetMethod("CreateLink", BindingFlags.NonPublic | BindingFlags.Instance));
        }

        private int IndexOf(string str, string search, int startIndex)
        {
            return str.IndexOf(search, startIndex);
        }
        private int LastIndexOf(string str, string search)
        {
            return str.LastIndexOf(search);
        }
        private string[] Split(string str, string limiter)
        {
            return str.Split(new string[] { limiter }, StringSplitOptions.None);
        }

        //factory
        private ShowInfo CreateShowInfo(string name, string link, string img)
        {
            return new ShowInfo(name, link, img);
        }
        private Episode CreateEpisode(string name, string link)
        {
            return new Episode(name, link);
        }
        private ShowContents CreateShowContents(string name, LuaTable table)
        {
            Episode[] episodes = new Episode[table.Values.Count];
            int i = 0;
            foreach (object ep in table.Values)
                episodes[i++] = (Episode)ep;
            return new ShowContents(name, episodes);
        }
        private Link CreateLink(string mediaType, string link)
        {
            return new Link((MediaType)Enum.Parse(typeof(MediaType), mediaType), link);
        }

        public void Reload()
        {
            Interpreter = new Lua();
            Interpreter.NewTable(SANDBOXTABLENAME); //create sandboxed enviroment
            SBEnviroment = (LuaTable)Interpreter[SANDBOXTABLENAME];
            Interpreter.DoString(string.Format(BUILDSANDBOX, SANDBOXTABLENAME), "Building Sandbox");//sandboxing

            LoadLib();

            //tmp: load module
            Interpreter.DoString(string.Format(MODULETABLENAME + " = loadfile(\"{0}\", \"t\", {1})()", modulePath.Replace("\\", "\\\\"), SANDBOXTABLENAME));
        }

        protected object[] Execute(string func, string arg)
        {
            try
            {
                return Interpreter.DoString(string.Format("return {0}({1})", func, arg), func);
            }
            catch (Exception ex)
            {
                if (AssertErrors)
                {
                    Console.WriteLine(ex.InnerException);
                    Console.WriteLine(ex.StackTrace);
                }
                return null;
            }
        }

        public override ShowInfo[] Browse(string type, int page)
        {
            object[] result = Execute(MODULETABLENAME + ".Browse", "\"" + type + "\", " + page);
            if (result == null) return new ShowInfo[0]; //if no returns
            LuaTable table = result[0] as LuaTable;
            if (table == null) return new ShowInfo[0]; //if invalid return

            ShowInfo[] listings = new ShowInfo[table.Values.Count];
            int i = 0;
            foreach (object info in table.Values)
                listings[i++] = (ShowInfo)info;

            return listings;
        }
        public override ShowContents GetContentList(string link)
        {
            object[] result = Execute(MODULETABLENAME + ".GetList", "\"" + link + "\"");
            if (result == null) return new ShowContents("", new Episode[0]);
            return (ShowContents)result[0];
        }
        public override Link[] GetContentLink(string link)
        {
            object[] result = Execute(MODULETABLENAME + ".GetLink", "\"" + link + "\"");
            if (result == null) return new Link[0]; //if no returns
            LuaTable table = result[0] as LuaTable;
            if (table == null) return new Link[0]; //if invalid return

            Link[] listings = new Link[table.Values.Count];
            int i = 0;
            foreach (object info in table.Values)
                listings[i++] = (Link)info;

            return listings;
        }
    }
}
