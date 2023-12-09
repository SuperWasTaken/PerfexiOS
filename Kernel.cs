using Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Sys = Cosmos.System;

namespace PerfexiOS
{
    public class Kernel : Sys.Kernel
    {

        protected override void BeforeRun()
        {
            var fs = new CosmosVFS();
            VFSManager.RegisterVFS(fs);
            Globals.FSinitalised = true;
            
            

            if (KeyboardManager.ControlPressed)
            {
                Globals.mode = 2;
            }
            else
            {
                try
                {
                    Mirage.DE.DesktopEnvironment.Start("Perfexi OS", "HARDCHAIR1");
                }
                catch (Exception ex)
                {
                    Globals.mode = 2;
                }
            }
            





            

        }

        protected override void Run()
        {
            if(Globals.mode ==2)
            {
                Commands.TextTerminal.loop();
            }

        }
    }
}
