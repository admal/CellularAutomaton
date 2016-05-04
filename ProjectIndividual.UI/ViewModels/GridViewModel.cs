using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ProjectIndividual.Domain.FileManagment;
using ProjectIndividual.Domain.GridComponent.Entities;
using ProjectIndividual.Domain.RulesComponent.Entities;
using ProjectIndividual.UI.Commands;
using ProjectIndividual.UI.CustomThreads;
using ProjectIndividual.UI.Helpers;
using ProjectIndividual.UI.Views;

namespace ProjectIndividual.UI.ViewModels
{
    public class GridViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Grid grid;
        private uint generation = 0;
        public  bool isStarted { get; set; } = false;
        public bool isPaused { get; set; } = false;
        public int Scale { get; set; } = 1;
        private EditableImage gridImage;
        private BasicCommand openRulesCommand;
        private BasicCommand loadGridCommand;
        private BasicCommand startGridCommand;
        private ComputingThread computingThread;

        #endregion

        #region Properties
        public BasicCommand OpenRulesCommand { get { return openRulesCommand; } }
        public BasicCommand OpenLoadGridWindowCommand { get { return loadGridCommand; } }
        public BasicCommand StartGridCommand { get { return startGridCommand; } }

        /// <summary>
        /// True when rules are loaded
        /// </summary>
        public bool isStartable{get { return grid.Rules != null; }}

        public ImageBrush GridBrush
        {
            get
            {
                if (gridImage != null)
                {
                    var brush = new ImageBrush(gridImage.GetImageSource());
                    return brush;
                }
                else
                {
                    return new ImageBrush();
                }

            }
        }
        public int ScreenWidth { get; } = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
        public int ScreenHeight { get; } = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
        public int LivingCellsCount
        {
            get
            {
                return grid.VisitedCells.Values.Select(c => c.State == CellState.Alive).Count();
            }
        }

        public uint Generation{get { return generation; }}
        public string RulesName { get { return grid.Rules == null ? "Not loaded" : grid.Rules.Name; } }
        #endregion

        #region Constructors
        public GridViewModel()
        {
            grid = new Grid();
            openRulesCommand = new BasicCommand(this.OpenRulesWindow, () => !isStarted);
            loadGridCommand = new BasicCommand(this.OpenLoadGridWindow, () => !isStarted);
            startGridCommand = new BasicCommand(StartPauseComputingGrid, ()=>true);
        }


        public GridViewModel(IList<Cell> cells)
        {
            this.grid = new Grid(cells);

        }

        public GridViewModel(IList<Cell> cells, RulesSet rules)
        {
            this.grid = new Grid(cells, rules);
        }

        #endregion

        #region Methods
        private void OpenLoadGridWindow()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Grid files (.grid)|*.grid";
            dialog.FilterIndex = 1;

            // Call the ShowDialog method to show the dialog box.
            bool? userClickedOK = dialog.ShowDialog();
            if (userClickedOK == true)
            {
                grid = FileLoader.ReadFromBinaryFile<Grid>(dialog.FileName);
                gridImage = new EditableImage(ScreenWidth, ScreenHeight, grid);
                RaisePropertyChanged("GridBrush");
                RaisePropertyChanged("LivingCellsCount");
                RaisePropertyChanged("RulesName");
                RaisePropertyChanged("isStartable");
                MessageBox.Show("Grid loaded properly!");
            }
        }
        public void OpenRulesWindow()
        {
            var rulesWindow = new RuleWindow();
            rulesWindow.Show();
        }
        private void StartPauseComputingGrid()
        {
            if (isStarted)
            {
                computingThread.RunPause();
                isPaused = !isPaused;
                RaisePropertyChanged("isPaused");
                return;
            }
            if (!isPaused)
            {
                computingThread = new ComputingThread(this);
                var thread = new Thread(new ThreadStart(computingThread.Start));
                isStarted = true;
                RaisePropertyChanged("isStarted");
                thread.Start();
            }
        }
        public void Update()
        {
            generation++;
            grid.UpdateGrid();
            gridImage.UpdateImage();
            RaisePropertyChanged("Generation");
            RaisePropertyChanged("GridBrush");
            RaisePropertyChanged("LivingCellsCount");
        }
        #endregion

        #region IPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}