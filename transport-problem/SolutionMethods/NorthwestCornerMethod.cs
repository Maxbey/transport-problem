using transport_problem.Table;

namespace transport_problem.SolutionMethods
{
    public class NorthwestCornerMethod
    {
        private Table.Table _table;

        public NorthwestCornerMethod(Supplier[] suppliers, Consumer[] consumers)
        {
            _table = new Table.Table(suppliers, consumers);
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