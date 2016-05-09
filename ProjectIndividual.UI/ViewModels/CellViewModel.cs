using System.Windows.Media;
using ProjectIndividual.Domain.GridComponent.Entities;

namespace ProjectIndividual.UI.ViewModels
{
    public class CellViewModel
    {
        private int OFFSET_X = (int)System.Windows.SystemParameters.PrimaryScreenWidth / 2;
        private int OFFSET_Y = (int)System.Windows.SystemParameters.PrimaryScreenHeight / 2;

        private Cell cell;
        private uint size;
        public CellViewModel(Cell cell, uint size)
        {
            this.cell = cell;
            this.size = size;
        }

        public Brush Fill
        {
            get
            {
                return cell.State == CellState.Dead ? Brushes.Black : Brushes.Red;
            }
        }
        public long X { get { return cell.X * size + OFFSET_X ; } }
        public long Y { get { return cell.Y*size + OFFSET_Y; } }

        public uint Size
        {
            get { return size; }
        }
    }
}