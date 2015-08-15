using System;
using System.Collections.Generic;
using System.Configuration;
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
            this.penSize = int.Parse(ConfigurationManager.AppSettings["Eraser.Size"]);
        }

        public override void SetPen(UIComponent.ActionInkCanvas canvas)
        {
            canvas.DefaultDrawingAttributes.Color = Colors.Black;
            canvas.DefaultDrawingAttributes.Width = penSize;
            canvas.DefaultDrawingAttributes.Height = penSize;
            canvas.UseCustomCursor = true;
            string activeDir = System.IO.Directory.GetCurrentDirectory();
            canvas.Cursor = new Cursor(activeDir + ConfigurationManager.AppSettings["Eraser.Cursor.Path"]);
        }

        public override Brush GetPenColor()
        {
            return Brushes.Black;
        }

        public override void ChangePenSize(int penSize)
        {
            // black pen is used for eraser
        }
    }
}
