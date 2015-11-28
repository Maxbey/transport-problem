using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Windows.Forms;
using transport_problem.Table;

namespace transport_problem.SolutionMethods
{
    public class VogelsMethod : FirstlySolutionMethod
    {

        public VogelsMethod(Supplier[] suppliers, Consumer[] consumers) : base(suppliers, consumers)
        {

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

            RemoveEmptyTransportations();

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

        private void RemoveEmptyTransportations()
        {
            foreach (TableRow row in _table.GetRows())
            {
                foreach (Cell cell in row.GetCells())
                {
                    if(cell.haveTransportation() && cell.GetTransportation().GetCargo() == 0)
                        cell.RemoveTransportation();
                }
            }
        }
    }
}