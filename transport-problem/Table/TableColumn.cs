using System;
using System.Linq;

namespace transport_problem.Table
{
    public class TableColumn : TableParentElement
    {
        private Consumer _consumer;

        public TableColumn(int index, Consumer consumer) : base(index)
        {
            _consumer = consumer;
        }

        public void BindCells(Cell[] cells)
        {
            Cells = cells;

            foreach (Cell cell in Cells)
            {
                cell.BindColumn(this);
            }
        }

        public int GetConsumerRequirement()
        {
            return _consumer.GetRequirement();
        }

        public Consumer GetConsumer()
        {
            return _consumer;
        }

    }
}