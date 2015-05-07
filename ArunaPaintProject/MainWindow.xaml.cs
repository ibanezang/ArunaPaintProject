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
        bool isShown;

        public MainWindow()
        {
            InitializeComponent();
            functionButtons = new List<Button>();
            MakeDraggable(ButtonsGrid, DragArea);
            isShown = false;
            functionButtons.Add(createButton(Colors.AliceBlue));
            functionButtons.Add(createButton(Colors.Red));
            functionButtons.Add(createButton(Colors.Purple));
            functionButtons.Add(createButton(Colors.Gray));
            foreach (Button b in functionButtons)
            {
                b.Visibility = System.Windows.Visibility.Collapsed;
                FunctionButtonsPanel.Children.Add(b);
            }
        }

        private void AddTab_GotFocus(object sender, RoutedEventArgs e)
        {
            AddTab.IsSelected = false;
            MainTabControl.Items
                .Insert(MainTabControl.Items.Count - 1
                , createNewTab("Untitled " + (MainTabControl.Items.Count - 1)));
        }

        private TabItem createNewTab(string title)
        {
            TabItem item = new TabItem();
            item.Header = title;
            item.IsSelected = true;

            var panel = new StackPanel();
            panel.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            panel.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            var canvas = new InkCanvas();
            canvas.Width = 1024;
            canvas.Height = 720;

            item.Content = panel;
            panel.Children.Add(canvas);
            return item;
        }

        public Button createButton(Color color)
        {
            Button newButton = new Button();
            //newButton.Template = MainGrid.FindResource("FloatingButton") as ControlTemplate;
            newButton.Width = 50;
            newButton.Height = 50;
            return newButton;
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

    }
}
