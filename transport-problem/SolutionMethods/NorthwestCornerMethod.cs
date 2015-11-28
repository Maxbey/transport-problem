using transport_problem.Table;

namespace transport_problem.SolutionMethods
{
    public class NorthwestCornerMethod : FirstlySolutionMethod
    {
        public NorthwestCornerMethod(Supplier[] suppliers, Consumer[] consumers) : base(suppliers, consumers)
        {

        }

        public Table.Table GetSolution()
        {

            foreach (TableRow row in _table.GetRows())
            {
                foreach (Cell cell in row.GetCells())
                {
                    if (cell.IsActive())
                    {
                        AddTransportation(cell);
                    }
                }
            }

            return _table;
        }
    }
}