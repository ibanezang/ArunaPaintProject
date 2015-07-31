using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArunaPaintProject.Class.Action
{
    public class ActionManager
    {
        private Stack<IAction> undoStack;
        private Stack<IAction> redoStack;

        public ActionManager()
        {
            stackInitialization();
        }

        private void stackInitialization()
        {
            undoStack = new Stack<IAction>();
            redoStack = new Stack<IAction>();
        }

        public void SaveAction(IAction action)
        {
            undoStack.Push(action);
            redoStack.Clear();
        }

        public void UndoAction()
        {
            var action = undoStack.Pop();
            action.Undo();
            redoStack.Push(action);
        }

        public void RedoAction()
        {
            var action = redoStack.Pop();
            action.Redo();
            undoStack.Push(action);
        }

        public bool CanUndo()
        {
            if (undoStack.Count == 0) { return false; }

            return true;
        }

        public bool CanRedo()
        {
            if (redoStack.Count == 0) { return false; }

            return true;
        }
    }
}
