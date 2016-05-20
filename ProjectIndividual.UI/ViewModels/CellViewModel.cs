using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using ProjectIndividual.Domain.GridComponent.Entities;
using ProjectIndividual.UI.Annotations;

namespace ProjectIndividual.UI.ViewModels
{
    public class CellViewModel : IEquatable<Cell>, INotifyPropertyChanged
    {
        private int offsetX = 0;// (int)System.Windows.SystemParameters.PrimaryScreenWidth / 2;
        private int offsetY = 0;//(int)System.Windows.SystemParameters.PrimaryScreenHeight / 2;

        private Cell cell;
        private uint size;
        public CellViewModel(Cell cell, uint size, int offsetX, int offsetY)
        {
            this.cell = cell;
            this.size = size;
            this.offsetY = offsetY;
            this.offsetX = offsetX;
        }

        public Brush Fill
        {
            get
            {
                return cell.State == CellState.Dead ? Brushes.Black : Brushes.Red;
            }
        }
        public long X { get { return cell.Y * size + offsetY ; } }
        public long Y { get { return cell.X*size + offsetX; } }

        public uint Size
        {
            get { return size; }
            set
            {
                size = value; 
                RaisePropertyChanged("Size");
                RaisePropertyChanged("X");
                RaisePropertyChanged("Y");
                RaisePropertyChanged("Fill");
            }
        }

        public bool Equals(Cell other)
        {
            return cell.Equals(other);
        }

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

    }
}