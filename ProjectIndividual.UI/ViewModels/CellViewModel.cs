using System.Windows.Media;
using ProjectIndividual.Domain.GridComponent.Entities;

namespace ProjectIndividual.UI.ViewModels
{
    public class CellViewModel
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
        public long X { get { return cell.Y * size + offsetX ; } }
        public long Y { get { return cell.X*size + offsetY; } }

        public uint Size
        {
            get { return size; }
        }
    }
}