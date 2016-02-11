using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CoreFramework;

namespace ContentProvider.CLI
{
    public class ValueArgs : EventArgs
    {
        public string NewValue { get; private set; }

        public ValueArgs(string newValue)
        {
            NewValue = newValue;
        }
    }

    public class EventCommandValue : ICommandHandler
    {
        public event EventHandler<ValueArgs> OnValueChange;
        private string val;
        public string Value
        {
            get { return val; }
            set
            {
                if (val != value)
                {
                    val = value;
                    if (OnValueChange != null) OnValueChange(this, new ValueArgs(val));
                }
            }
        }

        public void  SetCommand(CommandConsole console, string[] args)
        {
            if (args.Length == 1)
                Value = args[0];
            else
                console.Print("=" + Value);
        }
    }
}
