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
using System.Windows.Shapes;

namespace ArunaPaintProject.UITemplate
{
    /// <summary>
    /// Interaction logic for SaveFileDialog.xaml
    /// </summary>
    public partial class SaveFileDialog : Window
    {
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public string SelectedPath { get { return FolderName + "/" + FileName; } }

        public SaveFileDialog()
        {
            InitializeComponent();
        }

        private void buttonOK_Clicked(Object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxFileName.Text))
            {
                MessageBox.Show("Please type a file name!");
                return;
            }
            else
            {
                FileName = textBoxFileName.Text;
            }

            if (string.Equals(labelFolderLocation.Content, "Choose a location"))
            {
                MessageBox.Show("Please choose a location!");
                return;
            }
            else
            {

            }

            this.DialogResult = true;
        }

        private void buttonCancel_Clicked(Object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void buttonBrowse_Click(object sender, RoutedEventArgs e)
        {
            var folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderName = folderDialog.SelectedPath;
                labelFolderLocation.Content = FolderName;
            }                       
        } 
    }
}
