using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ArunaPaintProject.Class.Action;

namespace ArunaPaintProject.UIComponent
{
    public class ActionInkCanvas : InkCanvas
    {
        private ActionManager actionManager;
        private bool isEditing;

        public ActionInkCanvas()
        {
            InitializeActionManager();
            this.MouseLeftButtonUp += ActionInkCanvas_MouseLeftButtonUp;
            this.PreviewMouseLeftButtonDown += ActionInkCanvas_PreviewMouseLeftButtonDown;
        }


        void ActionInkCanvas_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // to validate editing state in the canvas
            this.isEditing = true;
        }

        void ActionInkCanvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.isEditing)
            {
                saveAction();
                this.isEditing = false;
            }
        }
        
        private void InitializeActionManager()
        {
            // empty Canvas
            this.actionManager = new ActionManager();
            this.isEditing = false;
        }

        private void saveAction()
        {
            // capture the state of final canvas
            IAction action;

            action = new DrawAction(this); 

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
