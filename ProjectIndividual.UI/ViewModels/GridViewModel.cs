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
        private EditableImage gridImage;
        private BasicCommand openRulesCommand;
        private BasicCommand loadGridCommand;
        private BasicCommand startGridCommand;
        private BasicCommand jumpStepsCommand;
        private BasicCommand nextStepCommand;
        private BasicCommand seeAllCellsCommand;
        private BasicCommand resetGridCommand;
        private BasicCommand saveCurrentGridCommand;

        private ComputingThread computingThread;

        #endregion

        #region Properties
        public BasicCommand OpenRulesCommand { get { return openRulesCommand; } }
        public BasicCommand OpenLoadGridWindowCommand { get { return loadGridCommand; } }
        public BasicCommand StartGridCommand { get { return startGridCommand; } }

        public BasicCommand JumpStepsCommand{get { return jumpStepsCommand; }}

        public BasicCommand NextStepCommand{get { return nextStepCommand; }}

        public BasicCommand SeeAllCellsCommand{get { return seeAllCellsCommand; }}

        public BasicCommand ResetGridCommand{get { return resetGridCommand; }}

        public BasicCommand SaveCurrentGridCommand
        {
            get { return saveCurrentGridCommand; }
        }

        public uint JumpSteps { get; set; } =0;
    
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
            jumpStepsCommand = new BasicCommand(JumpNSteps, ()=>true );
            nextStepCommand = new BasicCommand(Update,()=>true);
            resetGridCommand = new BasicCommand(ResetGrid, ()=>true );
            saveCurrentGridCommand = new BasicCommand(SaveCurrentGrid, ()=>true );
            //seeAllCellsCommand = new BasicCommand(SeeAllCeels, ()=>true );
        }

        private void SaveCurrentGrid()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Grid file (*.grid)|*.grid";
            if (dialog.ShowDialog() == true)
            {
                FileCreator.WriteToBinaryFile(dialog.FileName, grid);
            }
        }

        private void ResetGrid()
        {
            grid = new Grid();
            gridImage = null;
            generation = 0;
            isStarted = false;
            isPaused = false;

            RaisePropertyChanged("Generation");
            RaisePropertyChanged("Rules");
            RaisePropertyChanged("LivingCellsCount");
            RaisePropertyChanged("GridBrush");
            RaisePropertyChanged("isStartable");
            RaisePropertyChanged("isPaused");
            RaisePropertyChanged("isStarted");
        }

        private void JumpNSteps()
        {
            for (int i = 0; i < JumpSteps; i++)
            {
                Update();
            }
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
            RuleWindow rulesWindow;
            if (isStartable)
            {
                rulesWindow = new RuleWindow(grid.Rules);
            }
            else
            {
                rulesWindow = new RuleWindow();
            }
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