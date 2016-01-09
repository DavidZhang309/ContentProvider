using System;
using System.Collections.Generic;
using System.Linq;

using CoreFramework;
using ContentProvider.Lib;
using ContentProvider.Web;

namespace ContentProvider.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandConsole console = new CommandConsole() { Prefix = "", PrintTimestamp = false, VerboseLevel = VerboseTag.Info};
            bool running = true;

            Dictionary<string, BaseCDNModule> modules = new Dictionary<string, BaseCDNModule>();
            modules.Add("dailymotion", new DailymotionModule("dailymotion"));
            modules.Add("youtube", new YoutubeModule("youtube"));
            //TODO: Add more modules here
            
            //Commands (TODO: package this better than anonymous functions)
            console.RegisterCommand("quit", new EventCommand(new Action<object, EventCmdArgs>((sender, eventArgs) =>
                {
                    running = false;
                })));
            console.RegisterCommand("help", new EventCommand(new Action<object, EventCmdArgs>((sender, eventArgs) =>
            {
                console.Print("List of functions:\n    ->" + string.Join("\n    ->", console.GetCommandList()));
            })));
            console.RegisterCommand("list_modules", new EventCommand(new Action<object, EventCmdArgs>((sender, eventArgs) =>
            {
                console.Print("Modules:\n    ->" + string.Join("\n    ->", modules.Keys.ToArray()));
            })));

            console.RegisterCommand("browse", new EventCommand(new Action<object, EventCmdArgs>((sender, eventArgs) =>
            {
                if (eventArgs.Arguments.Length == 3)
                {
                    console.Print(string.Join("\n", modules[eventArgs.Arguments[0]].Browse(eventArgs.Arguments[1], Convert.ToInt32(eventArgs.Arguments[2]))));
                }
                else
                {
                    console.Print("Usage: browse [module] [type] [page]");
                }
            })));
            console.RegisterCommand("get_list", new EventCommand(new Action<object, EventCmdArgs>((sender, eventArgs) =>
            {
                if (eventArgs.Arguments.Length == 2)
                {
                    ShowContents show = modules[eventArgs.Arguments[0]].GetContentList(eventArgs.Arguments[1]);
                    console.Print(show.Name + "\n" + string.Join("\n", show.Episodes));
                }
                else
                {
                    console.Print("Usage: get_list [module] [relative_path]");
                }
            })));
            console.RegisterCommand("get_link", new EventCommand(new Action<object, EventCmdArgs>((sender, eventArgs) =>
            {
                if (eventArgs.Arguments.Length == 2)
                {
                    console.Print(string.Join("\n", modules[eventArgs.Arguments[0]].GetContentLink(eventArgs.Arguments[1])));
                }
                else
                {
                    console.Print("Usage: get_link [module] [relative_path]");
                }
            })));

            //Runs the CLI arguments if there are any arguments, else the console goes into interactive mode
            if (args.Length != 0)
                console.Call(string.Join(" ", args), false, true);
            else
                while (running)
                    console.Call(Console.ReadLine(), false, true);
        }
    }
}
