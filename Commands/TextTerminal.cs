using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Commands
{
    public static class TextTerminal
    {
        private static CommandManager cm = new CommandManager();

        public static void loop()
        {
            Console.WriteLine(cm.WorkingDirectory+ "@PERFEXI:READY>");
            var userinput = Console.ReadLine();
             
            
            cm.parse(userinput);
        }

    }
}
