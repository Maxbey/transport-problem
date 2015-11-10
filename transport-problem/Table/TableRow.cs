using System;
using System.Linq;
using System.Windows.Forms;

namespace transport_problem.Table
{
    public class TableRow : TableParentElement
    {
        private Supplier _supplier;

        private int _stock;

        public TableRow(int index, Supplier supplier) : base(index)
        {
            _supplier = supplier;
            _stock = supplier.GetStock();

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

        public int GetStock()
        {
            return _stock;
        }

        public void SetStock(int stock)
        {
            _stock = stock;
        }

        public Supplier GetSupplier()
        {
            return _supplier;
        }
    }
}