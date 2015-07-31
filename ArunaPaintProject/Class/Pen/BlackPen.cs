using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace ArunaPaintProject.Class.Pen
{
    public class BlackPen : PenHandler
    {
        public BlackPen(PenHandler pen): base(pen)
        {
        }

        public override void SetPen(UIComponent.ActionInkCanvas canvas)
        {
            canvas.DefaultDrawingAttributes.Color = Colors.Black;
            canvas.DefaultDrawingAttributes.Width = 15;
            canvas.DefaultDrawingAttributes.Height = 15;
            canvas.UseCustomCursor = true;
            string activeDir = System.IO.Directory.GetCurrentDirectory();
            canvas.Cursor = new Cursor(activeDir + ".\\Images\\Eraser.cur");
        }

        public override PenColors GetPenColor()
        {
            return PenColors.Black;
        }
    }
}
