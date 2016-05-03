using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ProjectIndividual.Domain.GridComponent.Entities;
using ProjectIndividual.Domain.RulesComponent.Entities;
using ProjectIndividual.UI.Commands;
using ProjectIndividual.UI.Views;

namespace ProjectIndividual.UI.ViewModels
{
    public class GridViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Grid grid;
        private uint generation = 0;
        public bool isStarted { get; set; } = false;
        public bool isPaused { get; set; } = false;
        private WriteableBitmap bitmap;
        private BasicCommand openRulesWindow = new BasicCommand(OpenRulesWindow, () => true); 
        #endregion
        public BasicCommand OpenRulesWindowCommand { get { return openRulesWindow; } }
        public static void OpenRulesWindow()
        {
            var rulesWindow = new RuleWindow();
            rulesWindow.Show();
        }
        
        /// <summary>
        /// True when rules are loaded
        /// </summary>
        public bool isStartable {
            get { return grid.Rules != null; }
        }

        public ImageBrush Brush
        {
            get
            {
                
                ImageBrush brush =new ImageBrush();
                BitmapImage img = new BitmapImage();
                brush.ImageSource = 
            }
        }
        public int LivingCellsCount
        {
            get { return grid.VisitedCells.Values.Select(c => c.State == CellState.Alive).Count(); }
        }
        
        public uint Generation
        {
            get { return generation; }
        }
        public string RulesName { get { return grid.Rules== null? "Not loaded" : grid.Rules.Name; } }
        #region Constructors
        public GridViewModel()
        {
            this.grid = new Grid();
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
        public void Update()
        {
            generation++;
            grid.UpdateGrid();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}