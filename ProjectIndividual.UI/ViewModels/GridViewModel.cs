using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using Microsoft.Win32;
using ProjectIndividual.Domain.FileManagment;
using ProjectIndividual.Domain.GridComponent.Entities;
using ProjectIndividual.Domain.RulesComponent.Entities;
using ProjectIndividual.UI.Commands;
using ProjectIndividual.UI.CustomThreads;
using ProjectIndividual.UI.Helpers;
using ProjectIndividual.UI.Views;
using Grid = ProjectIndividual.Domain.GridComponent.Entities.Grid;
using Point = System.Windows.Point;

namespace ProjectIndividual.UI.ViewModels
{
    public class GridViewModel : INotifyPropertyChanged
    {
        #region Fields
        public Grid grid;
        private uint generation = 0;
        public  bool isStarted { get; set; } = false;
        public bool isPaused { get; set; } = false;
        private BasicCommand openRulesCommand;
        private BasicCommand loadGridCommand;
        private BasicCommand startGridCommand;
        private BasicCommand jumpStepsCommand;
        private BasicCommand nextStepCommand;
        private BasicCommand seeAllCellsCommand;
        private BasicCommand resetGridCommand;
        private BasicCommand saveCurrentGridCommand;
        private BasicCommand closeAppCommand;

        private ComputingThread computingThread;
        private uint size = 1;
        private uint scale = 1;//50;
        private int offsetX = 0, offsetY = 0;
        private bool passedMaxCount = false;
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

        public BasicCommand CloseAppCommand
        {
            get { return closeAppCommand; }
        }

        public uint JumpSteps { get; set; } =0;
    
        public uint Scale
        {
            get
            {
                return scale;
                
            }
            set
            {
                //scale = value;
                //foreach (var cellViewModel in rects)
                //{
                //    cellViewModel.Size = scale*size;
                //}
                //RaisePropertyChanged("Rectangles");
            }
        }

        /// <summary>
        /// True when rules are loaded
        /// </summary>
        public bool isStartable{get { return grid.Rules != null; }}

        private ObservableCollection<CellViewModel> rects = new ObservableCollection<CellViewModel>(); 
        public ObservableCollection<CellViewModel> Rectangles
        {
            get
            {
                foreach (var cellViewModel in rects)
                {
                    cellViewModel.RaisePropertyChanged("Fill");
                }
                //rects = new ObservableCollection<CellViewModel>(
                //    grid.ImportantCells.Select(c => new CellViewModel(c, size* scale, offsetX, offsetY)));
                return rects;
            }
        }
        public int ScreenWidth { get; } = (int)System.Windows.SystemParameters.PrimaryScreenWidth/10;
        public int ScreenHeight { get; } = (int)System.Windows.SystemParameters.PrimaryScreenHeight/10;
        public string LivingCellsCount
        {
            get
            {
                if (!passedMaxCount)
                {
                    int count = grid.ImportantCells.Count();
                    passedMaxCount = count > 1000;
                    return count.ToString();
                }
                return "> 1000";
            }
        }

        public uint Generation{get { return generation; }}
        
        public string RulesName { get { return grid.Rules == null ? "Not loaded" : grid.Rules.Name; } }
        #endregion

        #region Constructors
        public GridViewModel()
        {
            grid = new Grid();
            openRulesCommand = new BasicCommand(this.OpenRulesWindow, () => (isPaused || !isStarted));
            loadGridCommand = new BasicCommand(this.OpenLoadGridWindow, () => !isStarted);
            startGridCommand = new BasicCommand(StartPauseComputingGrid, ()=>true);
            jumpStepsCommand = new BasicCommand(JumpNSteps, ()=>true );
            nextStepCommand = new BasicCommand(Update,()=>true);
            resetGridCommand = new BasicCommand(ResetGrid, ()=>true );
            saveCurrentGridCommand = new BasicCommand(SaveCurrentGrid, ()=>true );
            closeAppCommand = new BasicCommand(ExitApplication, () => true );
        }

        public void ExitApplication()
        {
            computingThread?.Stop();
        }


        public GridViewModel(IList<Cell> cells)
        {
            this.grid = new Grid(cells);
            rects = new ObservableCollection<CellViewModel>(
                grid.ImportantCells.Select(c => new CellViewModel(c, size * scale, offsetX, offsetY)));
        }

        public GridViewModel(IList<Cell> cells, RulesSet rules)
        {
            this.grid = new Grid(cells, rules);
            rects = new ObservableCollection<CellViewModel>(
                grid.ImportantCells.Select(c => new CellViewModel(c, size * scale, offsetX, offsetY)));
        }

        #endregion

        #region Methods
        private void SaveCurrentGrid()
        {
            if (grid.VisitedCells.Count == 0)
            {
                MessageBox.Show("The grid is empty!");
                return;
            }
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Grid file (*.grid)|*.grid";
            if (dialog.ShowDialog() == true)
            {
                //grid.Rules = null;
                FileCreator.WriteToBinaryFile(dialog.FileName, grid);
            }
        }

        private void ResetGrid()
        {
            grid = new Grid();
            generation = 0;
            isStarted = false;
            isPaused = false;
            computingThread?.Stop();
            rects.Clear();
            RaisePropertyChanged("Generation");
            RaisePropertyChanged("Rules");
            RaisePropertyChanged("LivingCellsCount");
            RaisePropertyChanged("Rectangles");
            RaisePropertyChanged("isStartable");
            RaisePropertyChanged("isPaused");
            RaisePropertyChanged("isStarted");
            RaisePropertyChanged("LivingCellsCount");
        }

        private void JumpNSteps()
        {
            for (int i = 0; i < JumpSteps; i++)
            {
                Update();
            }
        }

        private void OpenLoadGridWindow()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Grid files (.grid)|*.grid";
            dialog.FilterIndex = 1;

            // Call the ShowDialog method to show the dialog box.
            bool? userClickedOK = dialog.ShowDialog();
            if (userClickedOK == true)
            {
                try
                {
                    grid = FileLoader.ReadFromBinaryFile<Grid>(dialog.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Provided file does not contain proper grid!", "Error!", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                rects = new ObservableCollection<CellViewModel>(
                    grid.ImportantCells.Select(c => new CellViewModel(c, size * scale, offsetX, offsetY)));
                grid.ClearNewGenration();//clear if sth left during saving
                grid.Rules = null;
                RaisePropertyChanged("Rectangles");
                RaisePropertyChanged("LivingCellsCount");
                RaisePropertyChanged("RulesName");
                RaisePropertyChanged("isStartable");
                MessageBox.Show("Grid loaded properly!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public void OpenRulesWindow()
        {
            RuleWindow rulesWindow;
            if (isStartable)
            {
                //   rulesWindow = new RuleWindow(grid.Rules);
                rulesWindow = new RuleWindow(this);
            }
            else
            {
                grid.Rules = new RulesSet();
                // rulesWindow = new RuleWindow(grid.Rules);
                rulesWindow = new RuleWindow(this);
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

        public void AddNewCell(double x, double y)
        {
            if (!isPaused && isStarted)
            {
                return;
            }
            int actualX = (int)Math.Floor(x /size/scale + offsetX);
            int actualY = (int)Math.Floor(y / size / scale + offsetY);
            Debug.WriteLine("New cell at: " + actualX + ", " +actualY);
            var pos = new Position(actualX, actualY);
            CellState newCellState = grid.SwitchCellState(pos);
            var newCell = new CellViewModel(grid.GetCell(pos) , size*scale, offsetX,offsetY);
            if (!rects.Contains(newCell))
            {
                rects.Add(newCell);
            }
            RaisePropertyChanged("Rectangles");
            RaisePropertyChanged("LivingCellsCount");
        }

        public void RemoveCell(double x, double y)
        {
            if (!isPaused && isStarted)
            {
                return;
            }
            int actualX = (int)Math.Floor(x / size / scale);
            int actualY = (int)Math.Floor(y / size / scale);
            var pos = new Position(actualX, actualY);
            try
            {
                var cellToRemove = grid.GetCell(pos);
                rects.Remove(rects.FirstOrDefault(c => c.Equals(cellToRemove)));
            }
            catch (Exception){}//just ignore

            grid.RemoveCell(pos);
            
            RaisePropertyChanged("Rectangles");
            RaisePropertyChanged("LivingCellsCount");
        }

        SynchronizationContext uiContext = SynchronizationContext.Current;
        public void Update()
        {
            generation++;
            var newCells = grid.UpdateGrid();
            foreach (var newCell in newCells)
            {
                uiContext.Send(x => rects.Add(new CellViewModel(newCell, size * scale, offsetX, offsetY)),null); 
                        //send to ui thread
            }
            RaisePropertyChanged("Generation");
            RaisePropertyChanged("Rectangles");
            RaisePropertyChanged("LivingCellsCount");
        }
        public void MoveGrid(Point startPosition, Point endPosition)
        {
            int offsetX = (int) ((endPosition.X - startPosition.X)/size/scale);
            int offsetY = (int) ((endPosition.Y - startPosition.Y)/size/scale);
            Debug.Write( endPosition.X +" - " + startPosition.X + " / " + size + " / " + scale +" = " );
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            Debug.WriteLine(this.offsetX + ", " + this.offsetY);
            RaisePropertyChanged("Rectangles");
        }
        #endregion

        #region IPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
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