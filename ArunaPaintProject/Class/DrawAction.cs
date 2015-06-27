using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Ink;

namespace ArunaPaintProject.Class
{
    public class DrawAction : IAction
    {
        private InkCanvas originalCanvas;
        private Stroke changedStroke;

        public DrawAction(InkCanvas originalCanvas)
        {
            this.originalCanvas = originalCanvas;
            //save canvas strokes
            this.changedStroke = originalCanvas.Strokes.Last();
        }

        public void Undo()
        {
            this.originalCanvas.Strokes.Remove(changedStroke);
        }

        public void Redo()
        {
            this.originalCanvas.Strokes.Add(changedStroke);
        }
    }
}
