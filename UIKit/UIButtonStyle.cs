/*
 *  This file is part of the Mirage Desktop Environment.
 *  github.com/mirage-desktop/Mirage
 */
using Mirage.GraphicsKit;

namespace Mirage.UIKit
{
    /// <summary>
    /// Represents a style for a base's background.
    /// </summary>
    public abstract class UIButtonStyle
    {
        /// <summary>
        /// Render the base.
        /// </summary>
        /// <param name="target">Target canvas.</param>
        /// <param name="x">X coordinate of the base.</param>
        /// <param name="y">Y coordinate of the base.</param>
        /// <param name="width">Width of the base.</param>
        /// <param name="height">Height of the base.</param>
        /// <param name="hovered">Hovered state of the base. (currently unsupported)</param>
        /// <param name="pressed">Pressed state of the base.</param>
        /// <param name="@checked">Checked state of the base.</param>
        public abstract void Render(Canvas target, int x, int y, ushort width, ushort height, bool hovered, bool pressed, bool @checked);
    }
}
