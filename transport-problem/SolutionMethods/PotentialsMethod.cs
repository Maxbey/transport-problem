using System;
using System.Linq;
using System.Windows.Forms;
using transport_problem.Table;

namespace transport_problem.SolutionMethods
{
    public class PotentialsMethod
    {
        private Table.Table _table;

        public PotentialsMethod(Table.Table table)
        {
            _table = table;
        }

        public bool IsOptimal()
        {
            if (!CheckDegeneracy())
                throw new Exception("Degeneracy");

            CalculatePotentials();
            CalculateDistrubution();

            return _table.GetRows().SelectMany(row => row.GetCells()).All(cell => cell.GetDistributionIndex() >= 0);
        }

        public void CalculatePotentials()
        {
            _table.GetRow(0).SetPotential(0);

            while (!AllPotentialsCalculated())
            {
                foreach (Cell cell in _table.GetRows().SelectMany(row => row.GetCells().Where(cell => cell.haveTransportation())))
                {
                    MessageBox.Show("Rate " + cell.GetRate());

                    TableRow row = cell.GetRow();
                    TableColumn column = cell.GetColumn();

                    if (!row.HavePotential() && !column.HavePotential())
                    {
                        continue;
                    }

                    if (!row.HavePotential())
                    {
                        MessageBox.Show("Row potential" + (cell.GetRate() - Convert.ToInt32(column.GetPotential())));
                        row.SetPotential(cell.GetRate() - Convert.ToInt32(column.GetPotential()));
                    }

                    else if (!column.HavePotential())
                    {
                        MessageBox.Show("Column potential" + (cell.GetRate() - Convert.ToInt32(row.GetPotential())));
                        column.SetPotential(cell.GetRate() - Convert.ToInt32(row.GetPotential()));
                    }
                }
            }
        }

        public void CalculateDistrubution()
        {
            foreach (TableRow row in _table.GetRows())
            {
                foreach (Cell cell in row.GetCells())
                {
                    cell.SetDistributionIndex(cell.GetRate() - Convert.ToInt32(row.GetPotential()) - Convert.ToInt32(cell.GetColumn().GetPotential()));
                }
            }
        }

        private bool AllPotentialsCalculated()
        {
            return _table.GetRows().All(row => row.HavePotential()) && _table.GetColumns().All(column => column.HavePotential());
        }

        private bool CheckDegeneracy()
        {
            if (_table.GetTransportationsCnt() < (_table.GetColumnsCnt() + _table.GetRowsCnt() - 1))
                return false;

            return true;
        }
    }
}