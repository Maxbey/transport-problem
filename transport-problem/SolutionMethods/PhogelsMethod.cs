using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Windows.Forms;
using transport_problem.Table;
namespace transport_problem.SolutionMethods
{
    public class PhogelsMethod
    {
        private Table.Table _table;

        public PhogelsMethod(Supplier[] suppliers, Consumer[] consumers)
        {
            _table = new Table.Table(suppliers, consumers);
        }

        public void GetSolution()
        {
            for (int i = 0; i < _table.GetRowsCnt(); i++)
            {
                TableRow row = _table.GetRow(i);

                MessageBox.Show("Row index " + row.GetIndex() + " Row supplier stock " + row.GetSupplierStock());

                for (int j = 0; j < row.GetElementsCnt(); j++)
                {
                    Cell cell = row.GetCell(j);

                    MessageBox.Show("Cell with supplier stock " + cell.GetRow().GetSupplierStock() + " with consumer needs " + cell.GetColumn().GetConsumerRequirement() + "With rate " + cell.GetRate());
                }
            }
        }
    }
}