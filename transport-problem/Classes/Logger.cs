using Excel = Microsoft.Office.Interop.Excel;

namespace transport_problem.Table
{
    public class Logger
    {
        private Table _table;

        private Excel.Application excel;
        private Excel._Worksheet _worksheet;

        private int _iteration;

        public Logger(Table table)
        {
            _table = table;

            excel = new Excel.Application();
            excel.Workbooks.Add();

            _worksheet = (Excel.Worksheet)excel.ActiveSheet;

            _iteration = 1;
        }

        private void InitTableStruct()
        {
            int rowIndex;

            if (_iteration != 1)
            {
                rowIndex = (_table.GetRowsCnt() + 1) * (_iteration - 1) + _iteration;
            }
            else
            {
                rowIndex = 1;
            }

            _worksheet.Cells[rowIndex, 1] = "" + _iteration + " iteration";

            foreach (TableColumn column in _table.GetColumns())
            {

                if (_iteration != 1)
                    rowIndex =(_table.GetRowsCnt() + 1) * (_iteration - 1) + _iteration;
                else
                    rowIndex = 1;

                _worksheet.Cells[rowIndex, column.GetIndex() + 2] = "Consumer need " + column.GetConsumer().GetRequirement();
                
            }

            foreach (TableRow row in _table.GetRows())
            {
                if (_iteration != 1)
                    rowIndex = (_table.GetRowsCnt() + 1) * (_iteration - 1) + _iteration + row.GetIndex() + 1;
                else
                    rowIndex = (row.GetIndex() + 2);

                _worksheet.Cells[rowIndex, 1] = "Suppliers stock " + row.GetSupplier().GetStock();
            }
        }

        public void Log()
        {
            InitTableStruct();

            WriteData();

            _worksheet.Columns.AutoFit();
            _worksheet.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            _iteration++;
        }

        private void WriteData()
        {
            foreach (TableRow row in _table.GetRows())
            {
                foreach (Cell cell in row.GetCells())
                {
                    int rowIndex;

                    if (_iteration != 1)
                        rowIndex = (_table.GetRowsCnt() + 1) * (_iteration - 1) + _iteration + row.GetIndex() + 1;
                    else
                        rowIndex = (row.GetIndex() + 2);

                    if (cell.haveTransportation())
                    {
                        Transportation transportation = cell.GetTransportation();

                        _worksheet.Cells[rowIndex, cell.GetColumnIndex() + 2] = transportation.GetRate() + " [" + transportation.GetCargo() + "]";
                    }
                    else
                    {
                        _worksheet.Cells[rowIndex, cell.GetColumnIndex() + 2] = cell.GetRate();
                    }

                }
            }
        }

        public void ShowLog()
        {
            excel.Visible = true;
        }


    }
}