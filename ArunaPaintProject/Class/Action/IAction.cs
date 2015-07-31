using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArunaPaintProject.Class.Action
{
    public interface IAction
    {
        void Undo();
        void Redo();
    }
}
