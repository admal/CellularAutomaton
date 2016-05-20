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
        private GridViewModel viewModel;
        private Point onGridStartMovePosition;
        private bool wasDragged = false;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = Resources["mainGrid"] as GridViewModel;
        }
#warning TMP!!!!
        private void SeeAllClick(object sender, RoutedEventArgs e)
        {
            uiScaleSlider.Value = 1;
        }

        private void OnCanvasLeftDown(object sender, MouseButtonEventArgs e)
        {
            viewModel.AddNewCell(e.GetPosition(cellsGrid).X, e.GetPosition(cellsGrid).Y);
        }

        private void OnCanvasRightDown(object sender, MouseButtonEventArgs e)
        {
            viewModel.RemoveCell(e.GetPosition(cellsGrid).X, e.GetPosition(cellsGrid).Y);
        }
    }
}
