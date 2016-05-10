using System;
using System.Windows;
using System.Windows.Input;
using ProjectIndividual.UI.ViewModels;

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

        private void OnCanvasLeftClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = Resources["mainGrid"] as GridViewModel;
            viewModel.AddNewCell(e.GetPosition(cellsGrid).X, e.GetPosition(cellsGrid).Y);
        }

        private void OnCanvasRightClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = Resources["mainGrid"] as GridViewModel;
            viewModel.RemoveCell(e.GetPosition(cellsGrid).X, e.GetPosition(cellsGrid).Y);
        }
    }
}
