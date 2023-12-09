using Mirage;
using Mirage.GraphicsKit;
using Mirage.UIKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PERFEXIOS.UIKit
{
    class FileButtonStyle : UIButtonStyle
    {
        private Canvas Icon;
        private Color hovercolor;
       
        public FileButtonStyle() { Icon = Resources.FolderIcon; }
        public FileButtonStyle(Canvas Icon)
        {
            this.Icon = Icon;
            hovercolor = new Color(50,23, 192, 212);
        }
        public override void Render(Canvas target, int x, int y, ushort width, ushort height, bool hovered, bool pressed, bool @checked)
        {
            if (hovered)
            {

                target.DrawFilledRectangle(x, y, width, height, 0, hovercolor);
                
            }
            


        }
    }
}
