using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
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
    class DiskPart : UIApplication
    {
        public int SelectedDisk { get; set; } = 0;
        private UIBoxLayout disklayout;
        private UIBoxLayout partlayout;
        private UITextView nopartitionsfound;
        private UIContextMenu diskoptions;
        private UIWindow NewPartDialogue;
        public DiskPart(SurfaceManager sm) :base(sm)
        {
            UIWindow MainWindow = new(sm, 500, 300, "DISKPART", true, false);
            disklayout = new(UIBoxOrientation.Vertical);
            disklayout.Location = new(0, 0);

            
            
           

            partlayout = new(UIBoxOrientation.Vertical);
            partlayout.Location = new(200, 0);
            partlayout.ExplicitWidth = 200;
            for(int i = 0; i < VFSManager.GetDisks().Count-1; i++)
            {
                var d = VFSManager.GetDisks()[i];
                disklayout.Add(new DiskButton(sm,d,this,i));
               
            }
            var disk = VFSManager.GetDisks()[SelectedDisk];
            nopartitionsfound = new UITextView("No Partitions found!" + "\nSIZE OF DISK:\n" + disk.Size + " BYTES")
            {
                Editable = false,
                Wrapping = false,

            };

            NewPartDialogue = new(sm, 300, 200, "AddPart", true);
            base.AddApplicationWindow(NewPartDialogue);
            MainWindow.RootView.Add(disklayout);
            MainWindow.RootView.Add(partlayout);
            this.ondrivechange.Bind(handledrivechange);
            ondrivechange.Fire(new SignalArgs());
            MainWindow.Surface.OnUpdate.Bind(update);
        }
        private void update(SignalArgs args)
        {
            disklayout.LayOut();

        }
        private void handledrivechange(SignalArgs args)
        {
            var d = VFSManager.GetDisks()[SelectedDisk];
            foreach(var item in partlayout.GetDescendants())
            {
                partlayout.Remove(item);
            }
            if(d.Partitions.Count <= 0)
            {
                partlayout.Add(nopartitionsfound);

            }
            for(var i = 0; i < d.Partitions.Count-1; i++)
            {
                partlayout.Add(new PartitionButton(d, i));
            }
            

        }


        public readonly Signal<SignalArgs> ondrivechange = new();
        public readonly Signal<SignalArgs> opencontext = new();

        

        public void NewPart(Disk target, int size)
        {
            try
            {
                target.CreatePartition(size);
            }
            catch(Exception e)
            {
                return;
            }



        }
    }
}
