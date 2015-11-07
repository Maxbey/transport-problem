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
                VogelsDiff[] rowsDiffs = GetDiffsInRows();
                VogelsDiff[] columnsDiffs = GetDiffsInColumns();

                VogelsDiff maxInRows = FindMaxVogelsDiff(rowsDiffs);
                VogelsDiff maxInColumns = FindMaxVogelsDiff(columnsDiffs);

                TableParentElement parentElement = maxInRows.GetDIff() > maxInColumns.GetDIff() ? maxInRows.GetParentElement() : maxInColumns.GetParentElement();

                Cell cell = parentElement.FindMinRateCell();
                AddTransportation(cell);
            }

            MessageBox.Show("Total price " + _solution.getTotal());
        }

        private VogelsDiff[] GetDiffsInRows()
        {
            VogelsDiff[] diffs = new VogelsDiff[_table.GetRowsCnt()];

            for (int i = 0; i < _table.GetRowsCnt(); i++)
            {
                TableRow row = _table.GetRow(i);

                diffs[i] = row.GetVogelsDiff();
            }

            return diffs;
        }

        private VogelsDiff[] GetDiffsInColumns()
        {
            VogelsDiff[] diffs = new VogelsDiff[_table.GetColumnsCnt()];

            for (int i = 0; i < _table.GetColumnsCnt(); i++)
            {
                TableColumn column = _table.GetColumn(i);

                diffs[i] = column.GetVogelsDiff();
            }

            return diffs;
        }

        private VogelsDiff FindMaxVogelsDiff(VogelsDiff[] diffs)
        {
            VogelsDiff max = diffs[0];

            foreach (VogelsDiff diff in diffs)
            {
                if (diff.GetDIff() > max.GetDIff())
                {
                    max = diff;
                }
            }

            return max;
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