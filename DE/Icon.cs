using Mirage.GraphicsKit;
using Mirage.InputKit;
using Mirage.SurfaceKit;
using Mirage.UIKit;
using PERFEXIOS.UIKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PerfexiOSMirage.Mirage_beta_1._0.Mirage_beta_1._0.src.DE
{
    public class Icon 
    {
        Action callback;
        public bool hovered { get; set; }

        public string title { get; set; }
        private UIButton btn;
        private bool beingrenamed = false;
        public Icon(string title) 
        {
            btn = new UIButton(title);
            btn.Style = new ShortcutButtonStyle(title);
            btn.OnMouseClick.Bind(HandleMouse);
            btn.OnMouseRelease.Bind(OnRelease);
            btn.OnMouseDown.Bind(Hover);
            this.title = title;

        }
        public Icon(string title, Canvas icon) 
        {
            btn.Style = new ShortcutButtonStyle(title);
            btn.OnMouseClick.Bind(HandleMouse);
            btn.OnMouseRelease.Bind(OnRelease);
            btn.OnMouseDown.Bind(Hover);
            this.title = title;
        }



        private void HandleMouse(MouseArgs args)
        {
            
            
        }
        private void OnRelease(MouseArgs args)
        {
            hovered = false;
        }
        private void Hover(MouseArgs args)
        {
            hovered = true;
        }
   
    }
}
