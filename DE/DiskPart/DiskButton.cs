using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Mirage.GraphicsKit;
using Cosmos.HAL.BlockDevice;
using Mirage.UIKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirage;
using Mirage.InputKit;
using System.IO;
using Cosmos.System;
using Mirage.SurfaceKit;

namespace PERFEXIOS.DE.DiskPart
{
    class DiskButton : UIView
    {
        private DiskPart parent;
        private UIContextMenu Options;
        private UITextView Name;
        private int number;
        private int Y = 0;
        
        public Canvas Icon { get;private set; }
        public bool selected = false;
        private UICanvasView selectedrect;
        private SurfaceManager sm;
        public DiskButton(SurfaceManager sm, Disk disk,DiskPart parent,int number ) 
        {
            Y = number * 32;
            base.ExplicitWidth = base.Window.Size.Width - 16;
            base.ExplicitHeight = 32;
            this.parent = parent;
            this.number = number;
            this.sm = sm;
            switch(disk.Type)
            {
                case BlockDeviceType.HardDrive:
                    Icon = Resources.hddicon;
                    break;
                case BlockDeviceType.RemovableCD:
                    Icon = Resources.cdricon; break;
                default: Icon = Resources.otherDiskIcon;  break;

            }
            
            base.OnMouseClick.Bind(HandleClicked);
            Name = new UITextView("DRIVE#:" + number)
            {
                Editable = true,
                ReadOnly = true,
                AllowNewLines = false,
                Wrapping = false,
                Location = new(16, 0),


            };

            Options = new UIContextMenu(new()
            {
                new()
                {
                    new("Create Partition",() => _ = new newpartDialogue(sm,disk,this.parent))
                },
            });
            
            base.Add(Name);
          
         
        }

        public override void PaintSelf()
        {
            
            base.Window.Surface.Canvas.DrawImage(base.RootLocation.X,base.RootLocation.Y, Icon);
            QueuePaint();
        }

        private void HandleClicked(MouseArgs args)
        {
            if(args.State == MouseState.Left)
            {
                this.parent.SelectedDisk = number;

                parent.ondrivechange.Fire(new());
            }
            if(args.State == MouseState.Right)
            {
                pa
            }

        }
    }
}
