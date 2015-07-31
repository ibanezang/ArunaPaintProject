using System;
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
            canvas.DefaultDrawingAttributes.Width = 2;
            canvas.DefaultDrawingAttributes.Height = 2;
            canvas.UseCustomCursor = false;
        }

        public override PenColors GetPenColor()
        {
            return PenColors.Green;
        }
    }
}
