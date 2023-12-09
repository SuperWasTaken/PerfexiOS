using Mirage.SurfaceKit;
using Mirage.UIKit;
using Mirage.GraphicsKit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirage.InputKit;
using Cosmos.System;
using Mirage.DataKit;
using System.Security.Cryptography;
using System.Numerics;
using PERFEXIOS.Extensions;
using System.Text.Json.Nodes;

namespace PERFEXIOS.UIKit
{

    class scrollbar : UIView
    {
        private Double  ScrollOffset; 
        public UIView parent {  get; set; }
        public Double startvalue {get; set; }
        public Double endvalue { get; set; }
        public Double value { get; private set; }

        public readonly string orientation;
        public scrollbar(UIView parent,string orientation,double startvalue,double endvalue) 
        {
            
            this.parent = parent;
            this.startvalue = startvalue;
            this.endvalue = endvalue;
            this.orientation = orientation;
            SliderButton = new("");
            SliderButton.Style = new UIStandardButtonStyle();
            UpButton = new("");
            UpButton.Style = new SbUpButtonStyle();
            DownButton = new("");
            DownButton.Style = new SBDownButtonStyle();
            if (orientation == "H")
            {
                Point p;
                layout = new(UIBoxOrientation.Horizontal);
                layout.ExplicitSize = new(parent.Size.Width, 16);
                Track = new()
                {

                    Location = new(16, 0),
                    ExplicitSize = new(layout.Size.Width - 32, 16),
                    Color = Mirage.GraphicsKit.Color.LightGray,
                   

                };

                ScrollOffset = Track.Size.Width / 100;
                SliderButton.ExplicitWidth = (int)Math.Round(ScrollOffset);
                
            }
            if(orientation == "V" )
            {
                layout = new(UIBoxOrientation.Vertical);
                layout.ExplicitSize = new(16,parent.Size.Height);

                Track = new()
                {
                    Location = new(0, 16),
                    ExplicitWidth = 16,
                    ExplicitHeight = layout.Size.Height-32,
                   
                    Color = Mirage.GraphicsKit.Color.LightGray,
                };
                ScrollOffset = Track.Size.Height / 100;
                
                SliderButton.ExplicitHeight = (int)Math.Round(ScrollOffset);
                SliderButton.ExplicitWidth = 16;

            }
            SliderButton.Location = Track.Location;
            layout.Add(UpButton);
            layout.Add(Track);
            
            layout.Add(DownButton);
            Track.OnMouseDrag.Bind(HandleSlider);

            UpButton.OnMouseClick.Bind(handleUpPressed);
            DownButton.OnMouseClick.Bind(handledownpressed);
           
            base.Add(layout);
            Track.Add(SliderButton);








        }
        private void HandleSlider(MouseArgs args)
        {
            Point p;
            int delta = 0;
            double normalizedValue;
            if (SliderButton.Pressed)
            {
                switch (orientation)
                {
                    case "V":
                        if (args.Y != SliderButton.Location.Y)
                        {
                            delta = args.Y - SliderButton.Location.Y;
                        }

                        p = new Point(SliderButton.Location.X, SliderButton.Location.Y + delta);

                        if (p.Y >= Track.Size.Height - ScrollOffset)
                        {
                            p.Y = (int)Math.Round(Track.Size.Height - ScrollOffset);
                        }
                        if (p.Y < 0)
                        {
                            p.Y = 0;
                        }

                        SliderButton.Location = p;

                        // Calculate the normalized value based on the SliderButton's location and track size
                        normalizedValue = (p.Y / (Track.Size.Height - ScrollOffset)) * (endvalue - startvalue) + startvalue;

                        changeValue(normalizedValue);
                        break;
                      
                    case "H":
                        if (args.X != SliderButton.Location.X)
                        {
                            delta = args.X - SliderButton.Location.X;
                        }

                        p = new Point(SliderButton.Location.X + delta, SliderButton.Location.Y);

                        if (p.X >= Track.Size.Width - ScrollOffset)
                        {
                            p.X = (int)Math.Round(Track.Size.Width - ScrollOffset);
                        }
                        if (p.X > 0)
                        {
                            p.X = 0;
                        }

                        SliderButton.Location = p;

                        // Calculate the normalized value based on the SliderButton's location and track size
                        normalizedValue = (p.X / (Track.Size.Width - ScrollOffset)) * (endvalue - startvalue) + startvalue;

                        changeValue(normalizedValue);
                        break;

                    default:
                        throw new Exception("Invalid Orientation");
                }
            }
        }




        private void changeValue(double value)
        {

            this.value = Math.Min(Math.Max(value, startvalue), endvalue);
            
            
            if (this.value < startvalue )
            {
                this.value = startvalue;
            }
            if(this.value > endvalue )
            {
                this.value = endvalue;
            }

            OnValueChange.Fire(new SignalArgs());


        }

        public override void PaintSelf()
        {
            layout.LayOut();
            QueuePaint();

        }
        private void handleUpPressed(MouseArgs args)
        {
            changeValue(value+ScrollOffset);
        }
        private void handledownpressed(MouseArgs args)
        {
            changeValue(value-ScrollOffset);
        }


        private UIBoxLayout layout;
        private UIButton UpButton;
        private UIButton DownButton;
        private UIButton SliderButton;
        private UIRectView Track;



        public readonly Signal<SignalArgs> OnValueChange = new();


    }
}
