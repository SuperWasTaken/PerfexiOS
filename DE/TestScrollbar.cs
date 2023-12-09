using Mirage;
using Mirage.DataKit;
using Mirage.GraphicsKit;
using Mirage.GraphicsKit.Fonts;
using Mirage.SurfaceKit;
using Mirage.TextKit;
using Mirage.UIKit;
using PERFEXIOS.UIKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PERFEXIOS.DE
{
    class Scrollbartest : UIApplication
    {
        private scrollbar sb;
        private double value { get; set; }
        private UITextView val;
        private SurfaceManager sm;
        public Scrollbartest(SurfaceManager sm) : base(sm)
        {
            this.sm = sm;
            MainWindow = new UIWindow(sm, 300, 400, "Testing the Scroolbar LOL", true, false);
            val = new UITextView("0")
            {
                Wrapping = true,
                Location = new(30, 30),
                ExplicitSize = new(200, 300),
                
            };
            
            sb = new scrollbar( MainWindow.RootView,"V", 0, 100);
            MainWindow.RootView.Add(sb);
            MainWindow.RootView.Add(val);
            sb.OnValueChange.Bind(Draw);
        }
        public void Draw(SignalArgs args)
        {
            
            val.Content.Append(" "+sb.value.ToString());
 
        }
        
    
    }
}
