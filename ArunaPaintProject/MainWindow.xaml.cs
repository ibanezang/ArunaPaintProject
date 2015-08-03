using ArunaPaintProject.Class.Pen;
using ArunaPaintProject.UIComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArunaPaintProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Button> functionButtons;
        Button undoButton;
        Button redoButton;
        Button eraserButton;

        ActionTabItem activeTabItem = null;
        ActionInkCanvas activeCanvas = null;

        PenManager penManager;
        bool isShown;
        bool isEraserMode;

        int tabCounter;

        public MainWindow()
        {
            InitializeComponent();

            tabCounter = 1;

            //initialize pen manager
            penManager = new PenManager();

            functionButtons = new List<Button>();
            MakeDraggable(ButtonsGrid, DragArea);
            isShown = false;
            functionButtons.Add(createButton(Brushes.Red));
            functionButtons.Add(createButton(Brushes.Green));
            functionButtons.Add(createButton(Brushes.Blue));
            functionButtons.Add(createButton(Brushes.White));
            functionButtons.Add(createEraserButton());
            functionButtons.Add(createRedoButton());
            functionButtons.Add(createUndoButton());
            foreach (Button b in functionButtons)
            {
                b.Visibility = System.Windows.Visibility.Collapsed;
                FunctionButtonsPanel.Children.Add(b);
            }


            AddTab.IsSelected = false;
            createNewTab();
            MainTabControl.SelectionChanged += MainTabControl_SelectionChanged;

        }

        // create new tab
        void createNewTab()
        {
            MainTabControl.Items
                .Insert(MainTabControl.Items.Count - 1
                , createNewTab("Untitled " + (tabCounter++)));
        }

        void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateActiveTabItem();
            updateUndoRedoActionButtons();
        }

        private void updateActiveTabItem()
        {
            var items = FindVisualChildren<ActionTabItem>(MainTabControl);
            foreach (var item in items)
            {
                if (item.IsSelected)
                {
                    activeTabItem = item;
                    activeCanvas = item.ActionCanvas;
                    break;
                }
            }
            penManager.GetCurrentPen().SetPen(activeCanvas);
        }

        private void AddTab_GotFocus(object sender, RoutedEventArgs e)
        {
            AddTab.IsSelected = false;
            createNewTab();
        }

        private TabItem createNewTab(string title)
        {
            ActionTabItem item = new ActionTabItem(title);
            item.IsSelected = true;

            var panel = new StackPanel();
            panel.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            panel.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            var canvas = new ActionInkCanvas();
            canvas.MouseLeftButtonUp += canvas_MouseLeftButtonUp;
            canvas.Width = 1024;
            canvas.Height = 720;
            canvas.Background = Brushes.Black;
            penManager.GetCurrentPen().SetPen(canvas);

            item.Content = panel;
            item.ActionCanvas = canvas;
            panel.Children.Add(canvas);
            return item;
        }

        void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            updateUndoRedoActionButtons();
        }

        public Button createButton(Brush color)
        {
            Button newButton = new Button();
            newButton.Width = 50;
            newButton.Height = 50;
            newButton.Background = color;
            newButton.Click += colorButton_Click;
            return newButton;
        }

        void colorButton_Click(object sender, RoutedEventArgs e)
        {
            changePenColor(((Button)sender).Background);
            disableEraserMode();
        }

        void changePenColor(Brush color)
        {
            penManager.ChangeColor(color).SetPen(activeCanvas);
        }

        public Button createEraserButton()
        {
            eraserButton = new Button();
            eraserButton.Content = "Eraser";
            eraserButton.Width = 50;
            eraserButton.Height = 50;
            eraserButton.Click += eraserButton_Click;

            return eraserButton;
        }

        void eraserButton_Click(object sender, RoutedEventArgs e)
        {
            changeEraserMode();
        }

        void changeEraserMode()
        {
            if (!isEraserMode)
            {
                enableEraserMode();
            }
            else
            {
                disableEraserMode();
            }
        }

        public void enableEraserMode()
        {
            penManager.ChangeColor(Brushes.Black).SetPen(activeCanvas);
            isEraserMode = true;
        }

        public void disableEraserMode()
        {
            if (penManager.GetCurrentPen().GetPenColor() == Brushes.Black)
            {
                penManager.ChangeColor(Brushes.White).SetPen(activeCanvas);
            }
            isEraserMode = false;
        }

        public Button createUndoButton()
        {
            undoButton = new Button();
            undoButton.Content = "Undo";
            undoButton.Width = 50;
            undoButton.Height = 50;
            undoButton.Click += undo;
            return undoButton;
        }

        public Button createRedoButton()
        {
            redoButton = new Button();
            redoButton.Content = "Redo";
            redoButton.Width = 50;
            redoButton.Height = 50;
            redoButton.Click += redo;
            return redoButton;
        }

        private void undo(object sender, RoutedEventArgs e)
        {
            activeTabItem.ActionCanvas.Undo();
            updateUndoRedoActionButtons();
        }

        private void redo(object sender, RoutedEventArgs e)
        {
            activeTabItem.ActionCanvas.Redo();
            updateUndoRedoActionButtons();
        }
        
        private void updateUndoRedoActionButtons()
        {
            undoButton.IsEnabled = activeTabItem.ActionCanvas.CanUndo();
            redoButton.IsEnabled = activeTabItem.ActionCanvas.CanRedo();
        }
                
        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            if (isShown)
            {
                foreach (Button b in functionButtons)
                {
                    b.Visibility = System.Windows.Visibility.Collapsed;
                }
                isShown = false;
            }
            else
            {
                foreach (Button b in functionButtons)
                {
                    b.Visibility = System.Windows.Visibility.Visible;
                }
                isShown = true;
            }
            
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public void MakeDraggable(System.Windows.UIElement moveThisElement, System.Windows.UIElement movedByElement)
        {
            bool isMousePressed = false;
            System.Windows.Media.TranslateTransform transform = new System.Windows.Media.TranslateTransform(0, 0);
            moveThisElement.RenderTransform = transform;
            System.Windows.Point originalPoint = new System.Windows.Point(0, 0), currentPoint;

            movedByElement.MouseLeftButtonDown += (a, b) =>
            {
                isMousePressed = true;
                originalPoint = ((System.Windows.Input.MouseEventArgs)b).GetPosition(moveThisElement);
            };

            movedByElement.MouseLeftButtonUp += (a, b) => isMousePressed = false;

            movedByElement.MouseLeave += (a, b) => isMousePressed = false;

            movedByElement.MouseMove += (a, b) =>
            {
                if (!isMousePressed) return;

                currentPoint = ((System.Windows.Input.MouseEventArgs)b).GetPosition(moveThisElement);

                transform.X += currentPoint.X - originalPoint.X;
                transform.Y += currentPoint.Y - originalPoint.Y;
            };
        }

        private void Window_ContentRendered_1(object sender, EventArgs e)
        {
            updateActiveTabItem();
            updateUndoRedoActionButtons();
        }

    }
}
