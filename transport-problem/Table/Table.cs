using System;
using System.Linq;

namespace transport_problem.Table
{
    public class Table
    {

        private TableColumn[] _columns;
        private TableRow[] _rows;

        public Table(Supplier[] suppliers, Consumer[] consumers)
        {
            _rows = new TableRow[suppliers.Length];
            _columns = new TableColumn[consumers.Length];

            CreateRows(suppliers);
            CreateColumns(consumers);
        }

        private void CreateRows(Supplier[] suppliers)
        {
            for(int i = 0; i < _rows.Length; i++)
            {
                _rows[i] = new TableRow(i, suppliers[i]);
            }
        }

        private void CreateColumns(Consumer[] consumers)
        {
            for (int i = 0; i < _columns.Length; i++)
            {
                TableColumn column = new TableColumn(i, consumers[i]);
                Cell[] cells = new Cell[_rows.Length];

                for (int j = 0; j < _rows.Length; j++)
                {
                    cells[j] = _rows[j].GetCell(i);
                }

                column.BindCells(cells);
                _columns[i] = column;
            }
        }

        public int GetRowsCnt()
        {
            return _rows.Length;
        }

        public int GetColumnsCnt()
        {
            return _columns.Length;
        }

        public TableRow GetRow(int index)
        {
            if (index > GetRowsCnt() - 1)
            {
                throw new Exception("Attempt to access non-existent row with index " + index);
            }

            return _rows[index];
        }

        public TableColumn GetColumn(int index)
        {
            if (index > GetColumnsCnt() - 1)
            {
                throw new Exception("Attempt to access non-existent column with index " + index);
            }

            return _columns[index];
        }

        public void RemoveRow(TableRow row)
        {
            foreach (Cell cell in row.GetCells())
            {
                cell.Remove();
            }

            _rows = _rows.Where(val => val != row).ToArray();
        }

        public void RemoveColumn(TableColumn column)
        {
            foreach (Cell cell in column.GetCells())
            {
                cell.Remove();
            }

            _columns = _columns.Where(val => val != column).ToArray();
        }
    }
}