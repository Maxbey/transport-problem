using System;
using System.Linq;
using System.Windows.Forms;
using transport_problem.SolutionMethods;

namespace transport_problem.Table
{
    public class TableParentElement
    {
        protected readonly int Index;
        protected Cell[] Cells;

        private object _potential;

        public TableParentElement(int index)
        {
            Index = index;

            _potential = null;
        }

        public Cell GetCell(int index)
        {
            if (Cells.Length - 1 < index)
            {
                throw new Exception("Attempt to access to non-existent cell");
            }

            return Cells[index];
        }

        public VogelsDiff GetVogelsDiff()
        {
            int[] rates = new int[GetActiveCellsCnt()];

            for (int i = 0, c = 0; i < Cells.Length; i++)
            {
                Cell cell = Cells[i];

                if (cell.IsActive())
                {
                    rates[c] = cell.GetRate();
                    c++;
                }
            }

            return new VogelsDiff(this, Helpers.DiffBtwTwoMinValuesInArray(rates));
        }

        public Cell FindMinRateCell()
        {
            Cell min = FindFirstActiveCell();

            foreach (Cell cell in Cells)
            {
                if (!cell.IsActive())
                    continue;

                if (cell.GetRate() < min.GetRate())
                    min = cell;
            }

            return min;
        }

        private Cell FindFirstActiveCell()
        {
            foreach (Cell cell in Cells)
            {
                if (cell.IsActive())
                {
                    return cell;
                }
            }

            throw new Exception("All cells of this element are disabled");
        }

        public int GetIndex()
        {
            return Index;
        }

        public void UnbindCell(Cell cell)
        {
            cell.Disable();
        }

        public Cell[] GetCells()
        {
            return Cells;
        }

        public int GetCellsCnt()
        {
            return Cells.Length;
        }

        private int GetActiveCellsCnt()
        {
            return Cells.Count(cell => cell.IsActive());
        }

        public Object GetPotential()
        {
            return _potential;
        }

        public void SetPotential(int potential)
        {
            _potential = potential;
        }

        public bool HavePotential()
        {
            return _potential != null;
        }
    }
}