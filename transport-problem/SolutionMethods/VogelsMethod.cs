using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Windows.Forms;
using transport_problem.Classes;
using transport_problem.Table;
namespace transport_problem.SolutionMethods
{
    public class VogelsMethod
    {
        private Table.Table _table;
        private Solution _solution;

        public VogelsMethod(Supplier[] suppliers, Consumer[] consumers)
        {
            _table = new Table.Table(suppliers, consumers);
            _solution = new Solution(suppliers.Length, consumers.Length);
        }

        public void GetSolution()
        {
            while (_table.GetRowsCnt() != 0 && _table.GetColumnsCnt() != 0)
            {
                int[] rowsDiffs = getDiffsInRows();
                int[] columnsDiffs = getDiffsInColumns();

                int maxInRows = rowsDiffs.Max();
                int maxInColumns = columnsDiffs.Max();


                if (maxInRows > maxInColumns)
                {
                    int rowIndex = Array.IndexOf(rowsDiffs, maxInRows);
                    Cell cell = _table.GetRow(rowIndex).FindMinRateCell();
                    AddTransportation(cell);
                }
                else
                {
                    int columnIndex = Array.IndexOf(columnsDiffs, maxInColumns);
                    Cell cell = _table.GetColumn(columnIndex).FindMinRateCell();
                    AddTransportation(cell);
                }
            }

            MessageBox.Show("Total price " + _solution.getTotal());
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

        private void AddTransportation(Cell cell)
        {
            Supplier supplier = cell.GetSupplier();
            Consumer consumer = cell.GetConsumer();
            int rate = cell.GetRate();

            int supplierStock = supplier.GetStock();
            int consumerNeeds = consumer.GetRequirement();

            if (consumerNeeds > supplierStock)
            {
                _solution.AddTransportation(supplierStock, rate, cell.GetRowIndex(), cell.GetColumnIndex());
                _table.RemoveRow(cell.GetRow());
                consumer.SetRequirement(consumerNeeds - supplierStock);
                supplier.SetStock(0);
            }
            else
            {
                _solution.AddTransportation(consumerNeeds, rate, cell.GetRowIndex(), cell.GetColumnIndex());
                _table.RemoveColumn(cell.GetColumn());
                consumer.SetRequirement(0);
                supplier.SetStock(supplierStock - consumerNeeds);
            }
        }
    }
}