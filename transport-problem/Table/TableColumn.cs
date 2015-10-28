using System;
using System.Linq;

namespace transport_problem.Table
{
    public class TableColumn
    {
        private readonly int _index;

        private Consumer _consumer;
        private Cell[] _cells;

        public TableColumn(int index, Consumer consumer)
        {
            _index = index;
            _consumer = consumer;
        }

        public void BindCells(Cell[] cells)
        {
            _cells = cells;

            foreach (Cell cell in _cells)
            {
                cell.BindColumn(this);
            }
        }

        public Cell GetCell(int index)
        {
            if (_cells.Length - 1 < index)
            {
                throw new Exception("Attempt to access non-existent cell. " + "Column index: " + _index + " Rate index: " + index);
            }

            return _cells[index];
        }

        public int GetConsumerRequirement()
        {
            return _consumer.GetRequirement();
        }

        public int GetPhogelDiff()
        {
            int[] rates = makeRatesArray();

            return Helpers.DiffBtwTwoMinValuesInArray(rates);
        }

        private int[] makeRatesArray()
        {
            int[] rates = new int[_cells.Length];

            for (int i = 0; i < _cells.Length; i++)
            {
                rates[i] = _cells[i].GetRate();
            }

            return rates;
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

        public int GetIndex()
        {
            return _index;
        }

    }
}