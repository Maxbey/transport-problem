using System;

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


    }
}