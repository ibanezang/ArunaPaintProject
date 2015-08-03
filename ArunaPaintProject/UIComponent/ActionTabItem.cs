using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ArunaPaintProject.UITemplate;

namespace ArunaPaintProject.UIComponent
{
    public class ActionTabItem : TabItem
    {
        public ActionInkCanvas ActionCanvas { get; set; }
        public ActionTabItem()
        {
            var closeableHeader = new CloseableHeader();
            this.Header = closeableHeader;

            // Attach to the CloseableHeader events
            // (Mouse Enter/Leave, Button Click, and Label resize)
            closeableHeader.button_close.MouseEnter +=
               new MouseEventHandler(button_close_MouseEnter);
            closeableHeader.button_close.MouseLeave +=
               new MouseEventHandler(button_close_MouseLeave);
            closeableHeader.button_close.Click +=
               new RoutedEventHandler(button_close_Click);
            closeableHeader.label_TabTitle.SizeChanged +=
               new SizeChangedEventHandler(label_TabTitle_SizeChanged);
        }

        public ActionTabItem(string title)
        {
            var closeableHeader = new CloseableHeader();
            this.Header = closeableHeader;

            closeableHeader.label_TabTitle.Content = title;
            // Attach to the CloseableHeader events
            // (Mouse Enter/Leave, Button Click, and Label resize)
            closeableHeader.button_close.MouseEnter +=
               new MouseEventHandler(button_close_MouseEnter);
            closeableHeader.button_close.MouseLeave +=
               new MouseEventHandler(button_close_MouseLeave);
            closeableHeader.button_close.Click +=
               new RoutedEventHandler(button_close_Click);
            closeableHeader.label_TabTitle.SizeChanged +=
               new SizeChangedEventHandler(label_TabTitle_SizeChanged);
        }

        // Button MouseEnter - When the mouse is over the button - change color to Red
        void button_close_MouseEnter(object sender, MouseEventArgs e)
        {
            ((CloseableHeader)this.Header).button_close.Foreground = Brushes.Red;
        }
        // Button MouseLeave - When mouse is no longer over button - change color back to black
        void button_close_MouseLeave(object sender, MouseEventArgs e)
        {
            ((CloseableHeader)this.Header).button_close.Foreground = Brushes.Black;
        }
        // Button Close Click - Remove the Tab - (or raise
        // an event indicating a "CloseTab" event has occurred)
        void button_close_Click(object sender, RoutedEventArgs e)
        {
            var parent = (TabControl)this.Parent;
            if(parent.Items.IndexOf(this) == parent.Items.Count - 2 && parent.Items.Count != 2)
                ((TabItem)parent.Items.GetItemAt(parent.Items.IndexOf(this) - 1)).Focus();

            ((TabControl)this.Parent).Items.Remove(this);
        }
        // Label SizeChanged - When the Size of the Label changes
        // (due to setting the Title) set position of button properly
        void label_TabTitle_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ((CloseableHeader)this.Header).button_close.Margin = new Thickness(
               ((CloseableHeader)this.Header).label_TabTitle.ActualWidth + 5, 3, 4, 0);
        }

        // Override OnSelected - Show the Close Button
        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            ((CloseableHeader)this.Header).button_close.Visibility = Visibility.Visible;
        }

        // Override OnUnSelected - Hide the Close Button
        protected override void OnUnselected(RoutedEventArgs e)
        {
            base.OnUnselected(e);
            ((CloseableHeader)this.Header).button_close.Visibility = Visibility.Hidden;
        }

        // Override OnMouseEnter - Show the Close Button
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            ((CloseableHeader)this.Header).button_close.Visibility = Visibility.Visible;
        }

        // Override OnMouseLeave - Hide the Close Button (If it is NOT selected)
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (!this.IsSelected)
            {
                ((CloseableHeader)this.Header).button_close.Visibility = Visibility.Hidden;
            }
        }
    }
}
