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
            AddToLib("data.createSeriesInfo", this, thisType.GetMethod("CreateSeriesInfo", BindingFlags.NonPublic | BindingFlags.Instance));
            AddToLib("data.createInstallment", this, thisType.GetMethod("CreateInstallment", BindingFlags.NonPublic | BindingFlags.Instance));
            AddToLib("data.createSeries", this, thisType.GetMethod("CreateSeries", BindingFlags.NonPublic | BindingFlags.Instance));
            AddToLib("data.createResource", this, thisType.GetMethod("CreateResource", BindingFlags.NonPublic | BindingFlags.Instance));
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
        private ContentSeriesInfo CreateSeriesInfo(string name, string link, string img)
        {
            return new ContentSeriesInfo(name, link, img);
        }
        private SeriesInstallment CreateInstallment(string name, string link)
        {
            return new SeriesInstallment(name, link);
        }
        private ContentSeries CreateSeries(string name, LuaTable table)
        {
            SeriesInstallment[] episodes = new SeriesInstallment[table.Values.Count];
            int i = 0;
            foreach (object ep in table.Values)
                episodes[i++] = (SeriesInstallment)ep;
            return new ContentSeries(name, episodes);
        }
        private ContentResource CreateResource(string mediaType, string link)
        {
            return new ContentResource((MediaType)Enum.Parse(typeof(MediaType), mediaType), link);
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

        public override ContentSeriesInfo[] Browse(string type, int page)
        {
            object[] result = Execute(MODULETABLENAME + ".Browse", "\"" + type + "\", " + page);
            if (result == null) return new ContentSeriesInfo[0]; //if no returns
            LuaTable table = result[0] as LuaTable;
            if (table == null) return new ContentSeriesInfo[0]; //if invalid return

            ContentSeriesInfo[] listings = new ContentSeriesInfo[table.Values.Count];
            int i = 0;
            foreach (object info in table.Values)
                listings[i++] = (ContentSeriesInfo)info;

            return listings;
        }
        public override ContentSeries GetContentList(string link)
        {
            object[] result = Execute(MODULETABLENAME + ".GetList", "\"" + link + "\"");
            if (result == null) return new ContentSeries("", new SeriesInstallment[0]);
            return (ContentSeries)result[0];
        }
        public override ContentResource[] GetContentLink(string link)
        {
            object[] result = Execute(MODULETABLENAME + ".GetLink", "\"" + link + "\"");
            if (result == null) return new ContentResource[0]; //if no returns
            LuaTable table = result[0] as LuaTable;
            if (table == null) return new ContentResource[0]; //if invalid return

            ContentResource[] listings = new ContentResource[table.Values.Count];
            int i = 0;
            foreach (object info in table.Values)
                listings[i++] = (ContentResource)info;

            return listings;
        }
    }
}
