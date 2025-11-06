using System.Windows;
using System.Windows.Input;

namespace LolSpellOverlay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source == this)
                DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;

            this.Left = screenWidth - this.Width;
            this.Top = 0 + 70;
        }
    }
}