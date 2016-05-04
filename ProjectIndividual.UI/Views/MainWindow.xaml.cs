using System.Windows;

namespace ProjectIndividual.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
#warning TMP!!!!
        private void SeeAllClick(object sender, RoutedEventArgs e)
        {
            uiScaleSlider.Value = 1;
        }
    }
}
