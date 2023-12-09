
using Cosmos.HAL.BlockDevice.Registers;
using Cosmos.System.Graphics;
using Mirage;
using Mirage.DataKit;
using Mirage.InputKit;
using Mirage.SurfaceKit;
using Mirage.TextKit;
using Mirage.UIKit;
using PERFEXIOS.UIKit;
using PerfexiOSMirage.Mirage_beta_1._0.Mirage_beta_1._0.src.DE;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace PERFEXIOS.PerfexiOSMirage.Mirage_beta_1._0.Mirage_beta_1._0.src.DE
{
     class FileButton : UICanvasView 
     {
        public bool Clicked;
        private Mirage.GraphicsKit.Canvas icon { get; set; }
        public  UITextView content;

        public DirectoryInfo info;

        public FileInfo finfo;
        
        public FileButton(Size size, string path) : base(size)
        {
            base.OnMouseClick.Bind(HandleMouseDown);
            base.OnMouseRelease.Bind(HandleMouseRelease);

            info = new(path);
            FileAttributes attributes = info.Attributes;
            switch(attributes)
            {
                case FileAttributes.Directory:
                    icon = Resources.FolderIcon;
                    break;
                case FileAttributes.System:
                    icon = Resources.SysFileIcon;
                    break;
                default: 
                    switch(info.Extension)
                    {
                        case "txt":
                            icon = Resources.TextFileIcon;


                            break;
                        default:
                            icon = Resources.FileIconA;

                            break;


                    }
    
                    break;

            }
            content = new UITextView(info.Name)
            {
                Location= new Point(40, 0),
                ReadOnly = true,
                Editable = true,
               

            };

            base.Add(content);
        }
        
        public override void PaintSelf()
        {
            if(Window == null)
            {
                return;
            }


            base.Canvas.DrawImage(0, 0, icon);
          
            

        }
        



        public readonly Signal<SignalArgs> OnOpen = new();
        public void rename(string newpath)
        {
            if(!Directory.Exists(newpath))
            {
                return;
            }
            Directory.CreateDirectory(newpath);

            
        }

        private void HandleMouseDown(MouseArgs args)
        {
            Clicked = true;
            QueuePaint();
        }

        private void HandleMouseRelease(MouseArgs args)
        {
            Clicked = false;
            QueuePaint();
        }




    }
}
