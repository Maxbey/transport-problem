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
                AddRandomTransportation();

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

        private void AddRandomTransportation()
        {
            Cell cell = GetFreeCell();

            cell.AddTransportation(new Transportation(0, 0));
        }

        public void Otimize()
        {
            Cell top = GetMinDistributionIndexCell();

            ArrayList cycle = BuildRedistributionCycle(top); 

            DoRedistribution(cycle);
        }

        private void DoRedistribution(ArrayList cycle)
        {
            //redistibution
        }

        private ArrayList BuildRedistributionCycle(Cell top)
        {
            ArrayList cycles = new ArrayList();
            ArrayList firstCycle = new ArrayList {top};

            cycles.Add(firstCycle);

            do
            {
                var a = cycles.ToArray();

                foreach (ArrayList cycle in cycles.ToArray())
                {
                    if (CheckCycleNowhere(cycle))
                    {
                        cycles.Remove(cycle);
                    }
                    else if (CheckCycleFinal(cycle))
                    {
                        return cycle;
                    }
                    else
                    {
                        MakeCycleBranches(cycles, cycle);
                    }
                }

            } while (cycles.Count != 1);

            return cycles.Cast<ArrayList>().First();
        }

        private void MakeCycleBranches(ArrayList cycles, ArrayList cycle)
        {
            Cell last = cycle.Cast<Cell>().Last();
            Cell secondLast = cycle.Cast<Cell>().Reverse().Skip(1).FirstOrDefault();
            ArrayList originalCycle = (ArrayList) cycle.Clone();


            ArrayList siblings = GetCellSiblings(last);
            var sameCycle = true;

            foreach (Cell sibling in siblings.Cast<Cell>().Where(sibling => sibling.haveTransportation() && sibling != secondLast))
            {
                if (!sameCycle)
                {
                    ArrayList newBranch = (ArrayList) originalCycle.Clone();
                    newBranch.Add(sibling);

                    cycles.Add(newBranch);
                }

                else
                {
                    cycle.Add(sibling);
                    sameCycle = false;
                }
            }
        }

        private bool CheckCycleFinal(ArrayList cycle)
        {
            if (cycle.Count < 4)
                return false;

            Cell first = cycle.Cast<Cell>().First();
            Cell last = cycle.Cast<Cell>().Last();

            return GetCellSiblings(last).Cast<Cell>().Any(sibling => sibling == first);
        }

        private bool CheckCycleNowhere(ArrayList cycle)
        {
            Cell last = cycle.Cast<Cell>().Last();
            Cell secondLast = cycle.Cast<Cell>().Reverse().Skip(1).FirstOrDefault();

            return GetCellSiblings(last).Cast<Cell>().All(cell => !cell.haveTransportation() || cell == secondLast);
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

        private Cell GetFreeCell()
        {
            foreach (TableRow row in _table.GetRows())
            {
                foreach (Cell cell in row.GetCells())
                {
                    if (!cell.haveTransportation())
                        return cell;
                }
            }

            throw new Exception("Cannot get free cell");
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