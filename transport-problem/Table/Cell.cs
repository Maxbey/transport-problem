namespace transport_problem.Table
{
    public class Cell
    {
        private TableRow _row;
        private TableColumn _column;

        private int rate;

        private bool _active;

        private Transportation _transportation = null;

        private int _distributionIndex;

        public Cell(TableRow row, int r)
        {
            _row = row;
            rate = r;

            Enable();
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
            this.Disable();
        }

        public void Enable()
        {
            _active = true;
        }

        public void Disable()
        {
            _active = false;
        }

        public bool IsActive()
        {
            return _active;
        }

        public void AddTransportation(Transportation transportation)
        {
            _transportation = transportation;
        }

        public void RemoveTransportation()
        {
            _transportation = null;
        }

        public bool haveTransportation()
        {
            return _transportation != null;
        }

        public Transportation GetTransportation()
        {
            return _transportation;
        }

        public int GetDistributionIndex()
        {
            return _distributionIndex;
        }

        public void SetDistributionIndex(int index)
        {
            _distributionIndex = index;
        }
    }
}