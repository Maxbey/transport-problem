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


    }
}