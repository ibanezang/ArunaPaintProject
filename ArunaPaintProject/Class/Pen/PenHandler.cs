using ArunaPaintProject.UIComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArunaPaintProject.Class.Pen
{
    public abstract class PenHandler
    {
        private PenHandler next;

	    public PenHandler(PenHandler next) {
		    this.next = next;
	    }

	    public PenHandler GetPen(PenColors color) {
		    
            if (this.GetPenColor() == color) {
			    return this;
		    }

            if(next != null)
                return next.GetPen(color);
            else return null;
	    }

	    public abstract void SetPen(ActionInkCanvas canvas);

	    public abstract PenColors GetPenColor();
    }
}
