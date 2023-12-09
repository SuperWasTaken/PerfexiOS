using Mirage;
using Mirage.GraphicsKit;
using Mirage.TextKit;
using Mirage.UIKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PERFEXIOS.UIKit
{
    class ShortcutButtonStyle : UIButtonStyle
    {
        public Canvas icon { get; set; }
        public string name { get; set; }
        public ShortcutButtonStyle(string name) { this.name = name; }
        public ShortcutButtonStyle(string name, Canvas icon) { this.icon = icon; this.name = name;  }

      
       
        public override void Render(Canvas target, int x, int y, ushort width, ushort height, bool hovered, bool pressed, bool @checked)
        {
            
            icon._Width = 24;
            icon._Height = 24;
            target.DrawImage(x+8, y+8, icon);
            
        }






    }
}
