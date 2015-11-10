using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Windows.Forms;
using transport_problem.Table;

namespace transport_problem.SolutionMethods
{
    public class VogelsMethod
    {
        private Table.Table _table;

        public VogelsMethod(Supplier[] suppliers, Consumer[] consumers)
        {
            _table = new Table.Table(suppliers, consumers);
        }

        public Table.Table GetSolution()
        {
            while (_table.GetTotalRequirement() != 0)
            {
                VogelsDiff[] rowsDiffs = GetDiffsInRows();
                VogelsDiff[] columnsDiffs = GetDiffsInColumns();

                VogelsDiff maxInRows = FindMaxVogelsDiff(rowsDiffs);
                VogelsDiff maxInColumns = FindMaxVogelsDiff(columnsDiffs);

                TableParentElement parentElement = maxInRows.GetDIff() > maxInColumns.GetDIff() ? maxInRows.GetParentElement() : maxInColumns.GetParentElement();

                Cell cell = parentElement.FindMinRateCell();
                AddTransportation(cell);
            }

            return _table;
        }

        private VogelsDiff[] GetDiffsInRows()
        {
            VogelsDiff[] diffs = new VogelsDiff[_table.GetRowsCnt()];

            for (int i = 0, c = 0; i < _table.GetRowsCnt(); i++)
            {
                TableRow row = _table.GetRow(i);

                if (row.GetStock() == 0) continue;
                diffs[c] = row.GetVogelsDiff();
                c++;
            }

            return diffs;
        }

        private VogelsDiff[] GetDiffsInColumns()
        {
            VogelsDiff[] diffs = new VogelsDiff[_table.GetColumnsCnt()];

            for (int i = 0, c = 0; i < _table.GetColumnsCnt(); i++)
            {
                TableColumn column = _table.GetColumn(i);

                if (column.GetRequirement() == 0) continue;
                diffs[c] = column.GetVogelsDiff();
                c++;
            }

            return diffs;
        }

        private VogelsDiff FindMaxVogelsDiff(VogelsDiff[] diffs)
        {
            VogelsDiff max = diffs[0];

            foreach (VogelsDiff diff in diffs)
            {
                if (diff != null && (diff.GetDIff() > max.GetDIff()))
                {
                    max = diff;
                }
            }

            return max;
        }

        private void AddTransportation(Cell cell)
        {
            int rate = cell.GetRate();
            int supplierStock = cell.GetRow().GetStock();
            int consumerNeeds = cell.GetColumn().GetRequirement();

            Transportation transportation;

            if (consumerNeeds > supplierStock)
            {
                transportation = new Transportation(supplierStock, rate);
                cell.AddTransportation(transportation);

                _table.RemoveRow(cell.GetRow());
                cell.GetColumn().SetRequirement(consumerNeeds - supplierStock);
                cell.GetRow().SetStock(0);
            }
            else
            {
                transportation = new Transportation(consumerNeeds, rate);
                cell.AddTransportation(transportation);

                _table.RemoveColumn(cell.GetColumn());
                cell.GetColumn().SetRequirement(0);
                cell.GetRow().SetStock(supplierStock - consumerNeeds);
            }
        }
    }
}