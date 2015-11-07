using System;
using System.Linq;
using System.Windows.Forms;

namespace transport_problem.Table
{
    public class TableRow : TableParentElement
    {
        private Supplier _supplier;

        public TableRow(int index, Supplier supplier) : base(index)
        {
            _supplier = supplier;
            Cells = new Cell[supplier.GetRates().Length];

            CreateCells();
        }

        private void CreateCells()
        {
            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i] = new Cell(this, _supplier.GetRates()[i]);
            }
        }

        public int GetSupplierStock()
        {
            return _supplier.GetStock();
        }

        public Supplier GetSupplier()
        {
            return _supplier;
        }
    }
}