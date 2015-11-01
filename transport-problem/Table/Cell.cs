namespace transport_problem.Table
{
    public class Cell
    {
        private TableRow _row;
        private TableColumn _column;

        private int rate;

        public Cell(TableRow row, int r)
        {
            _row = row;
            rate = r;
        }

        public void BindColumn(TableColumn column)
        {
            _column = column;
        }

        public int GetRate()
        {
            return rate;
        }

        public TableRow GetRow()
        {
            return _row;
        }

        public TableColumn GetColumn()
        {
            return _column;
        }

        public int GetRowIndex()
        {
            return _row.GetIndex();
        }

        public int GetColumnIndex()
        {
            return _column.GetIndex();
        }

        public Supplier GetSupplier()
        {
            return _row.GetSupplier();
        }

        public Consumer GetConsumer()
        {
            return _column.GetConsumer();
        }

        public void Remove()
        {
            this.GetColumn().UnbindCell(this);
            this.GetRow().UnbindCell(this);
        }
    }
}