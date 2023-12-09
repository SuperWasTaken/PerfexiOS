
using Mirage.SurfaceKit;
using Mirage.TextKit;
using Mirage.UIKit;


using Mirage;

using System.IO;

using System.Drawing;
using System.Runtime.InteropServices;
using Mirage.DataKit;
using PERFEXIOS.UIKit;
using System;

namespace PERFEXIOS.PerfexiOSMirage.Mirage_beta_1._0.Mirage_beta_1._0.src.DE
{
    class EXPLORER : UIApplication
    {
        private string workingdir { get; set; }
        private UIinput content;
        private UIBoxLayout directorycontrols;
        private UIRectView DcBorder;
        private UIBoxLayout contentlist;
        private scrollbar contentscroll;


        
        private UIRectView ClBorder;
        private UIDialogue Erorr;
        private UIBoxLayout navpannel;
        private UIRectView navpannelborder;

        private string rootdir;
        public EXPLORER(SurfaceManager sm) : base(sm)
        {


            MainWindow = new UIWindow(sm, 600, 500, "FILE EXPLORER", true, false);

             content = new UIinput(new TextStyle(Resources.comicSans, Mirage.GraphicsKit.Color.Black))
             {

                Editable = true,
                Wrapping = false,
                ExplicitSize = new Size(MainWindow.Size.Width, 32),
                Location = new(0, 0),
                AllowNewLines = false,


            };
            directorycontrols = new UIBoxLayout(UIBoxOrientation.Horizontal) { };

            directorycontrols.Location = new(0, 0);


           

            contentlist = new UIBoxLayout(UIBoxOrientation.Vertical);

            MainWindow.RootView.Add(content);
          

            contentlist.Location = new(200, 32);

            MainWindow.RootView.Add(contentlist);

            // Draw the boundryboxes for each section in the file explorer
                                                                                                                                               // Boundry ox for the directory tree
            content.OnInput.Bind(handleinput);
            MainWindow.Surface.OnUpdate.Bind(update);

            contentscroll = new(contentlist, "V", 0,1);
            contentscroll.OnValueChange.Bind(handlescroll);
            contentlist.Add(contentscroll);
            draw();
        }

        private void draw()
        {
            contentlist.RangeLayOut((int)Math.Floor(contentscroll.value),(int)Math.Round((double)contentlist.ExplicitHeight/32));


        }

        private void handleinput(SignalArgs args)
        {
            var p = content.Content.Text.Trim(); // the p value is a temp varible that just removes any white space characters in input 
            changedir(p);
        }

        private void changedir(string path)
        {
            if(!Directory.Exists(path))
            {
                return;
            }

            DirectoryInfo dir = new DirectoryInfo(path);
            this.workingdir = path;


            foreach (var item in contentlist.GetDescendants())
            {

                contentlist.Remove(item);
            }


            foreach (var item in Directory.GetDirectories(workingdir))
            {

                var a = new FileButton(new(contentlist.Size.Width, 32), item);
                contentlist.Add(a);

            }
            foreach (var item in Directory.GetFiles(workingdir))
            {
                var a = new FileButton(new(contentlist.Size.Width, 32), item);
                contentlist.Add(a);
              
            }
         


            contentscroll = new(contentlist, "V", 0, contentlist.GetDescendants().Count - 1);

            contentlist.Add(contentscroll);




            contentscroll.endvalue = contentlist.GetDescendants().Count - 2;

            draw();
        }
        private void update(SignalArgs args)
        {
            contentlist.RangeLayOut((int)Math.Round(contentscroll.value), (int)Math.Round(contentscroll.value)+(int)Math.Round((decimal)contentlist.ExplicitHeight/32));
        }
        private void handlescroll(SignalArgs args)
        {
            draw();
        }

        private void refresh()
        {
            foreach (var item in contentlist.GetDescendants())
            {
               
                contentlist.Remove(item);
            }

             
            foreach (var item in Directory.GetDirectories(workingdir))
            {

                var a = new FileButton(new(contentlist.Size.Width-16, 32),item);
                contentlist.Add(a);

            }
            foreach (var item in Directory.GetFiles(workingdir))
            {
                var a = new FileButton(new(contentlist.Size.Width-16, 32), item);
                contentlist.Add(a);

            }
            var i = (int)Math.Floor((decimal)contentlist.ExplicitHeight / 32);


            contentscroll = new(contentlist, "V", 0, contentlist.GetDescendants().Count);

            contentlist.Add(contentscroll);

            contentscroll.endvalue = contentlist.GetDescendants().Count - 2;






        }

        



    }

   

}










