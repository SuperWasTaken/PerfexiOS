/*
 *  This file is part of the Mirage Desktop Environment.
 *  github.com/mirage-desktop/Mirage
 */
using System.Drawing;
using Cosmos.System;
using Mirage.InputKit;

namespace Mirage.SurfaceKit
{
    /// <summary>
    /// Represents one of a title bar's bases being pressed.
    /// </summary>
    public class TitleBarButtonOperation : Operation
    {
        /// <summary>
        /// Initialise a new interactive title bar base operation.
        /// </summary>
        /// <param name="surfaceManager">The surface manager.</param>
        /// <param name="base">The title bar.</param>
        /// <param name="base">The title bar base being pressed.</param>
        /// <param name="baseScreenRect">The rectangle of the base, in screen coordinates.</param>
        public TitleBarButtonOperation(SurfaceManager surfaceManager, TitleBar titleBar, TitleBarButton button, Rectangle baseScreenRect)
        {
            _surfaceManager = surfaceManager;
            _titleBar = titleBar;
            _base = button;
            _baseScreenRect = baseScreenRect;

            _titleBar.UpdateButton(_base);
        }

        /// <summary>
        /// Update the interactive title bar base operation.
        /// </summary>
        public override void Update()
        {
            bool mouseDown = MouseManager.MouseState == MouseState.Left;
            bool mouseOverButton = _baseScreenRect.Contains(MousePointer.Location);

            if (mouseOverButton == false)
            {
                _exited = true;
            }

            _titleBar.UpdateButton(_base);

            if (!mouseDown)
            {
                if (mouseOverButton && !_exited)
                {
                    _base.Callback?.Invoke();
                }
                _surfaceManager.CancelOperation();
                return;
            }
        }

        public override PointerType GetPointerType()
        {
            return PointerType.Default;
        }

        /// <summary>
        /// The surface manager.
        /// </summary>
        private readonly SurfaceManager _surfaceManager;

        /// <summary>
        /// The title bar base being pressed.
        /// </summary>
        private readonly TitleBar _titleBar;

        /// <summary>
        /// The title bar base being pressed.
        /// </summary>
        private readonly TitleBarButton _base;

        /// <summary>
        /// The rectangle of the base, in screen coordinates.
        /// </summary>
        private readonly Rectangle _baseScreenRect;

        /// <summary>
        /// If the mouse left the base during the operation.
        /// </summary>
        private bool _exited = false;
    }
}
