using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Windows.Forms;
using transport_problem.Table;
namespace transport_problem.SolutionMethods
{
    public class PhogelsMethod
    {
        private Table.Table _table;

        public PhogelsMethod(Supplier[] suppliers, Consumer[] consumers)
        {
            _table = new Table.Table(suppliers, consumers);
        }

        public void GetSolution()
        {
            int[] rowsDiffs = getDiffsInRows();
            int[] columnsDiffs = getDiffsInColumns();

            int maxInRows = rowsDiffs.Max();
            int maxInColumns = columnsDiffs.Max();


            if (maxInRows > maxInColumns)
            {
                int rowIndex = Array.IndexOf(rowsDiffs, maxInRows);

                Cell cell = _table.GetRow(rowIndex).FindMinRateCell();
            }
            else
            {
                int columnIndex = Array.IndexOf(columnsDiffs, maxInColumns);

                Cell cell = _table.GetColumn(columnIndex).FindMinRateCell();
            }
        }

        private int[] getDiffsInRows()
        {
            int[] diffs = new int[_table.GetRowsCnt()];

            for (int i = 0; i < _table.GetRowsCnt(); i++)
            {
                TableRow row = _table.GetRow(i);

                diffs[i] = row.GetPhogelDiff();
            }

            return diffs;
        }

        private int[] getDiffsInColumns()
        {
            int[] diffs = new int[_table.GetColumnsCnt()];

            for (int i = 0; i < _table.GetColumnsCnt(); i++)
            {
                TableColumn column = _table.GetColumn(i);

                diffs[i] = column.GetPhogelDiff();
            }

            return diffs;
        }
    }
}