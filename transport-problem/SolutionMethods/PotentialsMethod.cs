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

        /*private void MakeBetter()
        {
            Cell top = GetMinDistributionIndexCell();

            ArrayList directions = GetPossibleDirections(top);

            while (!CheckLoopEnd(top, ))
            {
                foreach (Cell cell in directions)
                {
                    GetPossibleDirections(cell);
                }
            }
        }*/

        private bool CheckLoopEnd(Cell top, Cell cell)
        {
            if (top == cell)
            {
                return true;
            }

            if (GetPossibleDirections(cell).Count == 0)
            {
                return true;
            }

            return false;
        }

        private ArrayList GetPossibleDirections(Cell top)
        {
            int rowIndex = top.GetRowIndex();
            int columnIndex = top.GetColumnIndex();

            ArrayList directions = new ArrayList();

            if (rowIndex != 0)
            {
                directions.Add(_table.GetRow(rowIndex - 1).GetCell(columnIndex));
            }
            else if (rowIndex != _table.GetRowsCnt())
            {
                directions.Add(_table.GetRow(rowIndex + 1).GetCell(columnIndex));
            }
            else if (columnIndex != 0)
            {
                directions.Add(_table.GetColumn(columnIndex - 1).GetCell(rowIndex));
            }
            else if(columnIndex != _table.GetColumnsCnt())
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

        private Cell GetMinDistributionIndexCell()
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