using NFEXL.View.ViewModel;
using System.Windows;
using System.Windows.Input;


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

            if (Mouse.LeftButton.Equals(MouseButtonState.Pressed))
            {

                double my = System.Windows.Forms.Control.MousePosition.Y;
                double mx = System.Windows.Forms.Control.MousePosition.X;
                double ileft = InputCombo.Margin.Left + Left + ControlGrid.Margin.Left;
                double itop = InputCombo.Margin.Top + Top + ControlGrid.Margin.Top;
                double oleft = OutputCombo.Margin.Left + Left + ControlGrid.Margin.Left;
                double otop = OutputCombo.Margin.Top + Top + ControlGrid.Margin.Top;

                if (mx > ileft && mx < ileft + InputCombo.ActualWidth)
                {
                    if (my > itop && my < itop + InputCombo.ActualHeight)
                    {
                        return;
                    }
                }
                if (mx > oleft && mx < oleft + OutputCombo.ActualWidth)
                {
                    if (my > otop && my < otop + OutputCombo.ActualHeight)
                    {
                        return;
                    }
                }

                this.Top = my - dy;
                this.Left = mx - dx;

            }
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dx = System.Windows.Forms.Control.MousePosition.X - this.Left;
            dy = System.Windows.Forms.Control.MousePosition.Y - this.Top;
        }

    }
}
