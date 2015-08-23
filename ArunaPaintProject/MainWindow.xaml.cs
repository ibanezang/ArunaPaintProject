using ArunaPaintProject.Class.Pen;
using ArunaPaintProject.UIComponent;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        List<Button> penSizeButtons;
        List<Button> penColorButtons;
        List<Button> actionButtons;

        Visibility functionButtonState;
        Visibility penSizeButtonState;
        Visibility penColorButtonState;
        Visibility actionButtonState;
        
        ActionTabItem activeTabItem = null;
        ActionInkCanvas activeCanvas = null;

        PenManager penManager;
        bool isShown;
        bool isEraserMode;

        int tabCounter;

        //pen sizes
        int PEN_SIZE_SMALL = int.Parse(ConfigurationManager.AppSettings["Pen.Size.Small"]);
        int PEN_SIZE_MEDIUM = int.Parse(ConfigurationManager.AppSettings["Pen.Size.Medium"]);
        int PEN_SIZE_LARGE = int.Parse(ConfigurationManager.AppSettings["Pen.Size.Large"]);

        public MainWindow()
        {
            InitializeComponent();

            tabCounter = 1;

            //initialize pen manager
            penManager = new PenManager();

            MakeDraggable(ButtonsGrid, DragArea);
            isShown = false;

            MainButton.Background = loadBackgroundImage("Button.Main");
            createPenSizeButton();
            createPenColorButton();
            createActionButton();
            createEraserButton();
            createSaveButton();
            createComingSoonButton();

            configureFunctionButtons();

            AddTab.IsSelected = false;
            createNewTab();
            MainTabControl.SelectionChanged += MainTabControl_SelectionChanged;

        }

        

        private void createComingSoonButton()
        {
            // do something here
            comingSoonButton.Width = 50;
            comingSoonButton.Height = 50;
            comingSoonButton.Background = loadBackgroundImage("Button.Coming.Soon");
            comingSoonButton.Click += saveButton_Click;
        }

        private void createSaveButton()
        {
            saveButton.Width = 50;
            saveButton.Height = 50;
            saveButton.Background = loadBackgroundImage("Button.Save");
            saveButton.Click += saveButton_Click;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            // do save routine here

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

        public void createPenColorButton()
        {
            penColorButton.Width = 50;
            penColorButton.Height = 50;
            penColorButton.Background = loadBackgroundImage("Button.Color");
            penColorButton.Click += penColorButton_Click;
            createAllColorButton();
        }

        void penColorButton_Click(object sender, RoutedEventArgs e)
        {
            penColorButtonState = penColorButtonState != System.Windows.Visibility.Collapsed ?
                System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
            changePenColorButtonState(penColorButtonState);
        }

        void changePenColorButtonState(System.Windows.Visibility state)
        {
            penColorButtons.ForEach(x => x.Visibility = state);
        }

        public void createAllColorButton()
        {
            penColorButtonState = System.Windows.Visibility.Collapsed;
            penColorButtons = new List<Button>();
            penColorButtons.Add(createColorButton(Brushes.White));
            penColorButtons.Add(createColorButton(Brushes.Red));
            penColorButtons.Add(createColorButton(Brushes.Green));
            penColorButtons.Add(createColorButton(Brushes.Blue));
            penColorButtons.ForEach(x => PenColorPanel.Children.Add(x));
            changePenColorButtonState(penColorButtonState);
        }

        public Button createColorButton(Brush color)
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

        public void createPenSizeButton()
        {
            penSizeButton.Width = 50;
            penSizeButton.Height = 50;
            penSizeButton.Background = loadBackgroundImage("Button.PenSize");
            penSizeButton.Click += penSizeButton_Click;
            createAllSizeButtons();
        }

        void penSizeButton_Click(object sender, RoutedEventArgs e)
        {
            penSizeButtonState = penSizeButtonState != System.Windows.Visibility.Collapsed ? 
                System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
            changePenSizeButtonState(penSizeButtonState);
        }

        void changePenSizeButtonState(System.Windows.Visibility state){
            penSizeButtons.ForEach(x => x.Visibility = state);
        }

        void createAllSizeButtons()
        {
            penSizeButtonState = System.Windows.Visibility.Collapsed;
            penSizeButtons = new List<Button>();
            penSizeButtons.Add(createSizeButton("S"));
            penSizeButtons.Add(createSizeButton("M"));
            penSizeButtons.Add(createSizeButton("L"));
            penSizeButtons.ForEach(x => PenSizePanel.Children.Add(x));
            changePenSizeButtonState(penSizeButtonState);
        }

        public Button createSizeButton(string label)
        {
            Button newButton = new Button();
            newButton.Width = 50;
            newButton.Height = 50;
            var hiddenLabel = new Label();
            hiddenLabel.Content = label;
            hiddenLabel.Visibility = System.Windows.Visibility.Hidden;
            newButton.Content = hiddenLabel;
            newButton.Background = loadBackgroundImage("Button.Pensize." + label);
            newButton.Click += sizeButton_Click;
            return newButton;
        }

        void sizeButton_Click(object sender, RoutedEventArgs e)
        {
            var content = ((Label)((Button)sender).Content).Content.ToString();
            var size = 0;
            switch (content)
            {

                case "S": size = PEN_SIZE_SMALL;
                    break;
                case "M": size = PEN_SIZE_MEDIUM;
                    break;
                case "L": size = PEN_SIZE_LARGE;
                    break;
                default: size = PEN_SIZE_SMALL;
                    break;
            }

            penManager.ChangePenSize(size).SetPen(activeCanvas);
        }
        
        void changePenColor(Brush color)
        {
            penManager.ChangeColor(color).SetPen(activeCanvas);
        }

        public Button createEraserButton()
        {
            eraserButton.Background = loadBackgroundImage("Button.Eraser");
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

        public void createActionButton()
        {
            actionButton.Width = 50;
            actionButton.Height = 50;
            actionButton.Background = loadBackgroundImage("Button.Action");
            actionButton.Click += actionButton_Click;
            createAllActionButtons();
        }

        void actionButton_Click(object sender, RoutedEventArgs e)
        {
            actionButtonState = actionButtonState != System.Windows.Visibility.Collapsed ?
                System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
            changeActionButtonState(actionButtonState);
        }

        void changeActionButtonState(System.Windows.Visibility state)
        {
            actionButtons.ForEach(x => x.Visibility = state);
        }

        void createAllActionButtons()
        {
            actionButtonState = System.Windows.Visibility.Collapsed;
            actionButtons = new List<Button>();
            actionButtons.Add(createUndoButton());
            actionButtons.Add(createRedoButton());
            changeActionButtonState(actionButtonState);
        }

        public Button createUndoButton()
        {
            undoButton.Content = "Undo";
            undoButton.Width = 50;
            undoButton.Height = 50;
            undoButton.Click += undo;
            return undoButton;
        }

        

        public Button createRedoButton()
        {
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

        private void configureFunctionButtons()
        {
            functionButtons = new List<Button>();
            functionButtons.Add(penSizeButton);
            functionButtons.Add(penColorButton);
            functionButtons.Add(actionButton);
            functionButtons.Add(eraserButton);
            functionButtons.Add(saveButton);
            functionButtons.Add(comingSoonButton);
            functionButtonState = System.Windows.Visibility.Collapsed;
            changeFunctionButtonState(functionButtonState);
        }

        void changeFunctionButtonState(System.Windows.Visibility state)
        {
            functionButtons.ForEach(x => x.Visibility = state);
            if (functionButtonState == System.Windows.Visibility.Collapsed)
            {
                penSizeButtonState = System.Windows.Visibility.Collapsed;
                penColorButtonState = System.Windows.Visibility.Collapsed;
                actionButtonState = System.Windows.Visibility.Collapsed;
                changePenSizeButtonState(penSizeButtonState);
                changePenColorButtonState(penColorButtonState);
                changeActionButtonState(actionButtonState);
            }
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            functionButtonState = functionButtonState != System.Windows.Visibility.Collapsed ?
                System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
            changeFunctionButtonState(functionButtonState);
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

        private ImageBrush loadBackgroundImage(string imageName)
        {
            var image = new ImageBrush(
                new BitmapImage(
                    new Uri(
                        ConfigurationManager.AppSettings[imageName], UriKind.Relative
                        )));
            return new ImageBrush(
                new BitmapImage(
                    new Uri(
                        ConfigurationManager.AppSettings[imageName], UriKind.Relative
                        )));
        }
    }
}
