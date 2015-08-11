using ArunaPaintProject.UIComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ArunaPaintProject.Class.Pen
{
    public abstract class PenHandler
    {
        private PenHandler next;
        protected int penSize;

	    public PenHandler(PenHandler next) {
		    this.next = next;
            this.penSize = 2;
	    }

	    public PenHandler GetPen(Brush color) {
		    
            if (this.GetPenColor() == color) {
			    return this;
		    }

            if(next != null)
                return next.GetPen(color);
            else return null;
	    }

	    public abstract void SetPen(ActionInkCanvas canvas);

	    public abstract Brush GetPenColor();

        public abstract void ChangePenSize(int penSize);
    }
}
