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
using System.Reflection;

namespace PERFEXIOS.DE.DiskPart
{
    class PartitionButton : UIView
    {
        private Disk parent;
        private UIContextMenu Options;
        private UITextView Name;
        public Canvas Icon { get;private set; }

        internal PartitionButton(Disk disk,int number)
        {
            base.ExplicitWidth = base.Window.Size.Width - 16;
            base.ExplicitHeight = 32;
            parent = disk;

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
            Name = new UITextView("PART#:" + number + "Size: " + disk.Partitions[number].Host.BlockCount * disk.Partitions[number].Host.BlockSize / 1024 / 1024 + " MB")
            {
                Editable = true,
                ReadOnly = true,
                AllowNewLines = false,
                Wrapping = false,
                Location = new(32,0)

            };
            base.Add(Name);
        }

        public override void PaintSelf()
        {
            base.Window.Surface.Canvas.DrawImage(this.Location.X, this.Location.Y, Icon);


            QueuePaint();
        }

        private void HandleClicked(MouseArgs args)
        {
            return; // temp just 4 testing
        }
    }
}
