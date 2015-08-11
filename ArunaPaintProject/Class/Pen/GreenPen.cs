﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ArunaPaintProject.Class.Pen
{
    public class GreenPen : PenHandler
    {
        public GreenPen(PenHandler pen)
            : base(pen)
        {

        }
        public override void SetPen(UIComponent.ActionInkCanvas canvas)
        {
            canvas.DefaultDrawingAttributes.Color = Colors.Green;
            canvas.DefaultDrawingAttributes.Width = penSize;
            canvas.DefaultDrawingAttributes.Height = penSize;
            canvas.UseCustomCursor = false;
        }

        public override Brush GetPenColor()
        {
            return Brushes.Green;
        }

        public override void ChangePenSize(int penSize)
        {
            this.penSize = penSize;
        }
    }
}
