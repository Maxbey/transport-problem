using System;
using System.Collections;
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
            {
                AddRandomTransportation();
            }

            ResetPotentials();
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

                    TableRow row = cell.GetRow();
                    TableColumn column = cell.GetColumn();

                    if (!row.HavePotential() && !column.HavePotential())
                    {
                        continue;
                    }

                    if (!row.HavePotential())
                    {
                        row.SetPotential(cell.GetRate() - Convert.ToInt32(column.GetPotential()));
                    }

                    else if (!column.HavePotential())
                    {
                        column.SetPotential(cell.GetRate() - Convert.ToInt32(row.GetPotential()));
                    }
                }
            }
        }

        private void CalculateDistrubution()
        {
            foreach (TableRow row in _table.GetRows())
            {
                foreach (Cell cell in row.GetCells())
                {
                    cell.SetDistributionIndex(cell.GetRate() - Convert.ToInt32(row.GetPotential()) - Convert.ToInt32(cell.GetColumn().GetPotential()));
                }
            }
        }

        private void ResetPotentials()
        {
            foreach (TableRow row in _table.GetRows())
            {
                row.RemovePotential();
            }

            foreach (TableColumn column in _table.GetColumns())
            {
                column.RemovePotential();
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

        private void AddRandomTransportation()
        {
            Cell cell = GetRandomFreeCell();

            cell.AddTransportation(new Transportation(0, cell.GetRate()));
        }

        public void Otimize()
        {

            while (!IsOptimal())
            {
                Cell top = GetMinDistributionIndexCell();

                MessageBox.Show(_table.GetTotalTransportationsPrice().ToString());
                DoRedistribution(BuildRedistributionCycle(top));
            }

        }

        private void DoRedistribution(Cell[] cycle)
        {
            MessageBox.Show("Cycle");

            foreach (Cell cell in cycle)
            {
                MessageBox.Show(cell.GetRate().ToString());
            }

            ArrayList calculated = new ArrayList();
            int min = GetMinTransportationCargo(cycle);

            var arr = cycle.ToArray();

            for (int i = 0; i < cycle.Length; i++)
            {
                Cell cell = (Cell) arr[i];

                if (calculated.Contains(cell))
                    continue;

                Transportation transportation = cell.GetTransportation();

                if (i == 0)
                {
                    cell.AddTransportation(new Transportation(min, cell.GetRate()));
                    calculated.Add(cell);
                    continue;
                }

                cell.AddTransportation(i%2 == 0
                    ? new Transportation(transportation.GetCargo() + min,
                        cell.GetRate())
                    : new Transportation(transportation.GetCargo() - min,
                        cell.GetRate()));

                calculated.Add(cell);
            }
        }

        private int GetMinTransportationCargo(Cell[] cycle)
        {
            int min = cycle.Skip(1).First().GetTransportation().GetCargo();

            foreach (Cell cell in cycle.Where(cell => cell.haveTransportation() && cell.GetTransportation().GetCargo() < min))
            {
                min = cell.GetTransportation().GetCargo();
            }

            return min;
        }

        private Cell[] BuildRedistributionCycle(Cell top)
        {
            ArrayList cycles = new ArrayList();
            ArrayList firstCycle = new ArrayList {top};

            cycles.Add(firstCycle);

            do
            {
                var a = cycles.ToArray();

                foreach (ArrayList cycle in cycles.ToArray())
                {
                    if (CheckCycleFinal(cycle))
                    {
                        return cycle.Cast<Cell>().Reverse().Skip(1).Reverse().ToArray();
                    }

                    MakeCycleBranches(cycles, cycle);

                }

            } while (true);
        }

        private void MakeCycleBranches(ArrayList cycles, ArrayList cycle)
        {
            Cell first = cycle.Cast<Cell>().First();

            Cell last = cycle.Cast<Cell>().Last();
            Cell secondLast = cycle.Cast<Cell>().Reverse().Skip(1).FirstOrDefault();

            ArrayList originalCycle = (ArrayList) cycle.Clone();

            TableRow row = last.GetRow();
            TableColumn column = last.GetColumn();

            bool sameCycle = true;

            foreach (Cell cell in row.GetCells())
            {
                if(cell == last)
                    continue;

                if (secondLast != null && secondLast.GetRow() == row && cell.GetColumnIndex() > last.GetColumnIndex() - secondLast.GetColumnIndex())
                    continue;

                if(!cell.haveTransportation() && cell != first)
                    continue;

                if (sameCycle)
                {
                    cycle.Add(cell);
                    sameCycle = false;
                }
                else
                {
                    ArrayList newBranch = (ArrayList)originalCycle.Clone();
                    newBranch.Add(cell);

                    cycles.Add(newBranch);
                }
            }

            foreach (Cell cell in column.GetCells())
            {
                if (cell == last)
                    continue;

                if (!cell.haveTransportation())
                    continue;

                if (secondLast != null && secondLast.GetColumn() == column && cell.GetRowIndex() > last.GetColumnIndex() - secondLast.GetRowIndex())
                    continue;

                ArrayList newBranch = (ArrayList)originalCycle.Clone();
                newBranch.Add(cell);

                cycles.Add(newBranch);
            }
        }

        private bool CheckCycleFinal(ArrayList cycle)
        {
            if (cycle.Count < 4)
                return false;

            Cell first = cycle.Cast<Cell>().First();
            Cell last = cycle.Cast<Cell>().Last();

            return first == last;
        }

        private ArrayList GetCellSiblings(Cell cell)
        {
            int rowIndex = cell.GetRowIndex();
            int columnIndex = cell.GetColumnIndex();

            ArrayList directions = new ArrayList();

            if (rowIndex != 0)
            {
                directions.Add(_table.GetRow(rowIndex - 1).GetCell(columnIndex));
            }

            if (rowIndex != _table.GetRowsCnt() - 1)
            {
                directions.Add(_table.GetRow(rowIndex + 1).GetCell(columnIndex));
            }
            if (columnIndex != 0)
            {
                directions.Add(_table.GetColumn(columnIndex - 1).GetCell(rowIndex));
            }

            if(columnIndex != _table.GetColumnsCnt() - 1)
            {
                directions.Add(_table.GetColumn(columnIndex + 1).GetCell(rowIndex));
            }

            return directions;
        }

        private Cell GetRandomFreeCell()
        {
            Random rand = new Random();
            Cell cell;
            do
            {
                int row = rand.Next(_table.GetRowsCnt());
                int col = rand.Next(_table.GetColumnsCnt());

                cell = _table.GetRow(row).GetCell(col);
            }

            while (cell.haveTransportation()) ;

            return cell;
        }

        public Cell GetMinDistributionIndexCell()
        {
            Cell min = _table.GetRow(0).GetCell(0); 

            foreach (TableRow row in _table.GetRows())
            {
                foreach (Cell cell in row.GetCells())
                {
                    if (cell.GetDistributionIndex() < min.GetDistributionIndex())
                    {
                        min = cell;
                    }
                }
            }

            return min;
        }
    }
}