using System;
using System.Linq;
using System.Windows.Forms;
using transport_problem.Classes;
namespace transport_problem.SolutionMethods
{
    public class PhogelsMethod
    {
        private Supplier[] _suppliers;
        private Сonsumer[] _consumers;

        private Solution _solution;

        public PhogelsMethod(Supplier[] suppliers, Сonsumer[] consumers)
        {
            this._suppliers = suppliers;
            this._consumers = consumers;

            this._solution = new Solution();
        }

        public void GetSolution()
        {
            while (_suppliers.Length != 0 && _consumers.Length != 0) 
            {
                int rate;

                int[][] columns = makeColumnsArray();

                int[] diffInRows = DiffInRows();
                int[] diffInColumns = DiffInColumns();

                int maxInRows = diffInRows.Max();
                int maxInColumns = diffInColumns.Max();

                /*MessageBox.Show("Columns");

                foreach (int [] column in columns)
                {
                    foreach (int v in column)
                    {
                        MessageBox.Show(v.ToString());
                    }
                }*/


                if (maxInRows > maxInColumns)
                {
                    int supplierIndex = Array.IndexOf(diffInRows, maxInRows);
                    rate = _suppliers[supplierIndex].GetRates().Min();
                    int consumerIndex = Array.IndexOf(_suppliers[supplierIndex].GetRates(), rate);

                    //MessageBox.Show("Stock " + _suppliers[supplierIndex].GetStock() + " need " + _consumers[consumerIndex].GetRequirement() + " rate " + rate);

                    makeTransportation(_consumers[consumerIndex], _suppliers[supplierIndex], rate);
                }
                else
                {
                    int consumerIndex = Array.IndexOf(diffInColumns, maxInColumns);
                    rate = columns[consumerIndex].Min();

                    int supplierIndex = Array.IndexOf(columns[consumerIndex], rate);

                    //MessageBox.Show("Stock " + _suppliers[supplierIndex].GetStock() + " need " + _consumers[consumerIndex].GetRequirement() + " rate " + rate);
                    
                    makeTransportation(_consumers[consumerIndex], _suppliers[supplierIndex], rate);
                }
            }

            //MessageBox.Show(_solution.getTotal().ToString());

        }


        private int DiffBtwTwoMinValues (int[] values)
        {
            if (values.Length == 1)
                return values[0];

            int a = values.Min();
            int aIndex = Array.IndexOf(values, a);

            int b = values.Skip(aIndex + 1).Min();

            return Math.Abs(a - b);
        }

        private int[] DiffInRows()
        {
            int[] diff = new int[_suppliers.Length];

            for (int i = 0; i < diff.Length; i++)
            {
                diff[i] = DiffBtwTwoMinValues(_suppliers[i].GetRates());
            }

            return diff;
        }

        private int[] DiffInColumns()
        {
            int[] diff = new int[_consumers.Length];

            int[][] columns = makeColumnsArray();


            for (int i = 0; i < diff.Length; i++)
            {
                diff[i] = DiffBtwTwoMinValues(columns[i]);
            }

            return diff;
        }

        private int[][] makeColumnsArray()
        {
            int[][] columns = new int[_consumers.Length][];

            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] = new int[_suppliers.Length];
            }

            for (int i = 0; i < _consumers.Length; i++)
            {
                for (int j = 0; j < _suppliers.Length; j++)
                {
                    columns[i][j] = _suppliers[j].GetRates()[i];
                }
            }

            return columns;
        }

        private void makeTransportation(Сonsumer consumer, Supplier supplier, int rate)
        {
            if (consumer.GetRequirement() > supplier.GetStock())
            {
                _solution.AddTransportation(supplier.GetStock(), rate);
                consumer.SetRequirement(consumer.GetRequirement() - supplier.GetStock());
                _suppliers = _suppliers.Where(val => val != supplier).ToArray();
            }
            else
            {
                _solution.AddTransportation(consumer.GetRequirement(), rate);
                supplier.SetStock(supplier.GetStock() - consumer.GetRequirement());
                _consumers = _consumers.Where(val => val != consumer).ToArray();
            }
        }
    }
}