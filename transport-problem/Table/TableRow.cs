using System;

namespace transport_problem.Table
{
    public class TableRow
    {
        private readonly int _index;

        private Supplier _supplier;
        private Cell[] _cells;

        public TableRow(int index, Supplier supplier)
        {
            _index = index;

            _supplier = supplier;
            _cells = new Cell[supplier.GetRates().Length];

            CreateCells();
        }

        private void CreateCells()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i] = new Cell(this, _supplier.GetRates()[i]);
            }
        }

        public Cell GetCell(int index)
        {
            if (_cells.Length - 1 < index)
            {
                throw new Exception("Attempt to access non-existent cell. " + "Row index: " + _index + " Rate index: " + index);
            }

            return _cells[index];
        }

        public int GetIndex()
        {
            return _index;
        }

        public int GetElementsCnt()
        {
            return _cells.Length;
        }

        public int GetSupplierStock()
        {
            return _supplier.GetStock();
        }

        public int GetPhogelDiff()
        {
            int[] rates = new int[_cells.Length];

            for (int i = 0; i < _cells.Length; i++)
            {
                rates[i] = _cells[i].GetRate();
            }

            return Helpers.DiffBtwTwoMinValuesInArray(rates);
        }

        public Cell FindMinRateCell()
        {
            Cell min = _cells[0];

            for (int i = 1; i < _cells.Length; i++)
            {
                if (_cells[i].GetRate() < min.GetRate())
                {
                    min = _cells[i];
                }
            }

            return min;
        }
    }
}