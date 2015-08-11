using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ArunaPaintProject.Class.Pen
{
    public class PenManager
    {
        private PenHandler pens;
        private Brush currentColor;
        private int penSize;

        public PenManager()
        {
            currentColor = Brushes.White; // default value is white
            penSize = 2; // default value for penSize
            buildChain();
        }

        private void buildChain()
        {
            pens = new BlackPen(new WhitePen(new RedPen(new GreenPen(new BluePen(null)))));
        }

        public PenHandler ChangeColor(Brush color)
        {
            currentColor = color;
            return GetCurrentPen();
        }

        public PenHandler ChangePenSize(int penSize)
        {
            this.penSize = penSize;
            return GetCurrentPen();
        }

        public PenHandler GetCurrentPen()
        {
            var pen = pens.GetPen(currentColor);
            pen.ChangePenSize(penSize);
            return pen;
        }
    }
}
