using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ArunaPaintProject.Class.Pen
{
    public class WhitePen : PenHandler
    {
        public WhitePen(PenHandler pen)
            : base(pen)
        {

        }

        public override void SetPen(UIComponent.ActionInkCanvas canvas)
        {
            canvas.DefaultDrawingAttributes.Color = Colors.White;
            canvas.DefaultDrawingAttributes.Width = 2;
            canvas.DefaultDrawingAttributes.Height = 2;
            canvas.UseCustomCursor = false;
        }

        public override PenColors GetPenColor()
        {
            return PenColors.White;
        }
    }
}
