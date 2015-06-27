using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ArunaPaintProject.Class;

namespace ArunaPaintProject.UIComponent
{
    public class ActionInkCanvas : InkCanvas
    {
        private ActionManager<DrawAction> actionManager;

        public ActionInkCanvas()
        {
            InitializeActionManager();
            //this.PreviewMouseLeftButtonDown += ActionInkCanvas_PreviewMouseLeftButtonDown;
            this.MouseLeftButtonUp += ActionInkCanvas_MouseLeftButtonUp;
        }

        void ActionInkCanvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            saveAction();
        }

        void Strokes_StrokesChanged(object sender, System.Windows.Ink.StrokeCollectionChangedEventArgs e)
        {
            saveAction();
        }
        
        private void InitializeActionManager()
        {
            // empty Canvas
            this.actionManager = new ActionManager<DrawAction>();
        }

        private void saveAction()
        {
            // capture the state of final canvas
            var action = new DrawAction(this);
            this.actionManager.SaveAction(action);
        }

        public bool CanUndo()
        {
            return actionManager.CanUndo();
        }

        public bool CanRedo()
        {
            return actionManager.CanRedo();
        }

        public void Undo()
        {
            if (CanUndo())
            {
                this.actionManager.UndoAction();
            }
        }

        public void Redo()
        {
            if (CanRedo())
            {
                this.actionManager.RedoAction();
            }
        }
    }
}
