using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArunaPaintProject.Class.Pen
{
    public class PenManager
    {
        private PenHandler pens;

        public PenManager()
        {
            buildChain();
        }

        private void buildChain()
        {
            pens = new BlackPen(new WhitePen(new RedPen(new GreenPen(new BluePen(null)))));
        }

        public PenHandler GetPen(PenColors color)
        {
            return pens.GetPen(color);
        }
    }
}
