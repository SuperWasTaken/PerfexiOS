using Cosmos.System.FileSystem;
using Mirage.DataKit;
using Mirage.InputKit;
using Mirage.SurfaceKit;
using Mirage.UIKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PERFEXIOS.DE.DiskPart
{
    class newpartDialogue : UIWindow
    {
        private UIinput input;
        private UIButton confirm;
        private Disk target;
        private DiskPart parent;
        private int value = 0;
        public newpartDialogue(SurfaceManager sm,Disk target,DiskPart parent) : base(sm,200,300,"addpart",true)
        {
            this.target = target;
            this.parent = parent;
            input = new UIinput()
            {
                Location = new(100,150),
                Editable = true,
               
            };
            confirm = new("Confirm")
            {
                Location = new(100, 200),
                Style = new UIStandardButtonStyle(),

            };
            input.OnInput.Bind(HandleInputValue);
            confirm.OnMouseClick.Bind(HandleButtonPressed);
            base.RootView.Add(input);
            RootView.Add(confirm);

        }
        private void HandleInputValue(SignalArgs args)
        {
            value = int.Parse(input.Content.Text.Trim());

        }

        private void HandleButtonPressed(MouseArgs args)
        {
            parent.NewPart(target,value);
        }
        
       
    }
}
