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

        private void OnCanvasLeftUp(object sender, MouseButtonEventArgs e)
        {
            if (!wasDragged)
            {
                viewModel.AddNewCell(e.GetPosition(cellsGrid).X, e.GetPosition(cellsGrid).Y);
            }
            wasDragged = false;
        }

        private void OnCanvasRightClick(object sender, MouseButtonEventArgs e)
        {
            viewModel.RemoveCell(e.GetPosition(cellsGrid).X, e.GetPosition(cellsGrid).Y);
        }

        private void MouseMoveOnGrid(object sender, MouseEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                wasDragged = true;
                viewModel.MoveGrid(onGridStartMovePosition, e.GetPosition(cellsGrid));
            }
        }

        private void OnCanvasLeftDown(object sender, MouseButtonEventArgs e)
        {
            onGridStartMovePosition = e.GetPosition(cellsGrid);
        }
    }
}
