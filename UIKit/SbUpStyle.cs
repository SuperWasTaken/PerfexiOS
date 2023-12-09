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
    class SbUpButtonStyle : UIButtonStyle
    {
        public override void Render(Canvas target, int x, int y, ushort width, ushort height, bool hovered, bool pressed, bool @checked)
        {
            target.DrawImage(x, y, Resources.ScroolBarUp);
        }
    }
}
