using Mirage.UIKit;
using PerfexiOS.DE;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOS.Commands
{
    public class Message
    {

      
        internal void send( string msg,int type)
        {

            if (type == 0)
            {
                Console.WriteLine(msg);
            }
            if (type == 1)
            {
                Console.Write(msg);
            }
            else
            {
                Console.WriteLine(msg);
            }
            
        }
       

    }
}
