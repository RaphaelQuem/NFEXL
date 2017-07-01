using NFEXL.View.ViewModel;
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

namespace NFEXL.View
{
    public partial class MainWindow : Window
    {
        private double dx = 0;
        private double dy = 0;
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new NFEXLVM();
            Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
        public NFEXLVM ViewModel
        {
            get { return DataContext as NFEXLVM; }
            set { DataContext = value; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if(Mouse.LeftButton.Equals(MouseButtonState.Pressed))
            {
                this.Top = System.Windows.Forms.Control.MousePosition.Y - dy;
                this.Left = System.Windows.Forms.Control.MousePosition.X - dx;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dx = System.Windows.Forms.Control.MousePosition.X - this.Left;
            dy = System.Windows.Forms.Control.MousePosition.Y - this.Top;
        }
    }
}
