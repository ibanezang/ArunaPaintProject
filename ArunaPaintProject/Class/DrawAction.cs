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
        private StrokeCollection undoStrokes;
        private StrokeCollection redoStrokes;

        public DrawAction(InkCanvas originalCanvas)
        {
            this.originalCanvas = originalCanvas;
            //save canvas strokes
            this.undoStrokes = originalCanvas.Strokes.Clone();
        }

        public void Undo()
        {
            this.redoStrokes = this.originalCanvas.Strokes.Clone();
            this.originalCanvas.Strokes = undoStrokes;
        }

        public void Redo()
        {
            this.originalCanvas.Strokes = redoStrokes;
        }
    }
}
