/*
 *  This file is part of the Mirage Desktop Environment.
 *  github.com/mirage-desktop/Mirage
 */
using System.Collections.Generic;
using Mirage.GraphicsKit;
using Mirage.SurfaceKit;
using Mirage.TextKit;

namespace Mirage.UIKit
{
    /// <summary>
    /// A generic dialogue box.
    /// </summary>
    class UIDialogue : UIWindow
    {
        /// <summary>
        /// Initialise a new dialogue box.
        /// </summary>
        /// <param name="surfaceManager">The surface manager to display the dialogue window in.</param>
        /// <param name="title">The title of the dialogue box.</param>
        /// <param name="description">The description of the dialogue box.</param>
        /// <param name="bases">The bases in the dialogue box, from left to right.</param>
        /// <param name="icon">The icon to display on the dialogue box. Will be automatically scaled.</param>
        public UIDialogue(
            SurfaceManager surfaceManager,
            string title,
            TextBlock description,
            List<UIButton> bases,
            Canvas icon)
            : base(
                surfaceManager,
                400,
                128,
                title,
                titlebar: true,
                resizable: false)
        {
            UICanvasView iconView = new UICanvasView(Filters.Scale(64, 64, icon), alpha: true)
            {
                Location = new(24, 24),
            };
            DescriptionView = new UITextView(description)
            {
                Location = new(112, 24),
                ExplicitWidth = Surface.Canvas.Width - 136,
                Wrapping = true
            };
            UIBoxLayout baseLayout = new UIBoxLayout(UIBoxOrientation.Horizontal)
            {
                Spacing = 8
            };
            foreach (UIButton button in bases)
            {
                button.OnMouseClick.Bind((args) => Close());
                baseLayout.Add(button);
            }
            baseLayout.LayOut();
            baseLayout.Location = new(
                Surface.Canvas.Width - baseLayout.Size.Width - 24,
                Surface.Canvas.Height - baseLayout.Size.Height - 24
            );
            RootView.Add(iconView);
            RootView.Add(DescriptionView);
            RootView.Add(baseLayout);
            Surface.SurfaceManager.Focus = Surface;
        }

        /// <summary>
        /// The text view that displays the dialogue's description.
        /// </summary>
        public UITextView DescriptionView { get; init; }
    }
}
