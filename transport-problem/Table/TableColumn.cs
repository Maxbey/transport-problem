using System;
using System.Linq;

namespace transport_problem.Table
{
    public class TableColumn : TableParentElement
    {
        private Consumer _consumer;

        private int _requirement;

        public TableColumn(int index, Consumer consumer) : base(index)
        {
            _consumer = consumer;

            _requirement = consumer.GetRequirement();
        }

        public void BindCells(Cell[] cells)
        {
            Cells = cells;

            foreach (Cell cell in Cells)
            {
                cell.BindColumn(this);
            }
        }

        public int GetRequirement()
        {
            return _requirement;
        }

        public void SetRequirement(int requirement)
        {
            _requirement = requirement;
        }

        public Consumer GetConsumer()
        {
            return _consumer;
        }

    }
}