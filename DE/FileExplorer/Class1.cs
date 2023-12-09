using Cosmos.System.Graphics;
using Mirage.UIKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PERFEXIOS.DE.FileExplorer
{
    class UiFileinfo
    {
        private string path; 
        private string name;
        private FileAttributes attributes;
        private Action callback;
        private UIApplication Parent;

        public UiFileinfo(string path, string name, FileAttributes attributes, Action callback, UIApplication parent = null)
        {
            this.path=path;
            this.name=name;
            this.attributes=attributes;
            this.callback=callback;
            Parent=parent;




        }
    }
}
