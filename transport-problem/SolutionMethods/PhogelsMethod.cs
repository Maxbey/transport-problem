using System;
using System.Collections.Concurrent;
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


                if (maxInRows >= maxInColumns)
                {
                    int supplierIndex = Array.IndexOf(diffInRows, maxInRows);
                    rate = _suppliers[supplierIndex].GetRates().Min();
                    int consumerIndex = Array.IndexOf(_suppliers[supplierIndex].GetRates(), rate);
                    MessageBox.Show("Row: Supplier with stock " + _suppliers[supplierIndex].GetStock() + " Consumer with stock " + _consumers[consumerIndex].GetRequirement() + " on rate " + rate);
                    makeTransportation(consumerIndex, supplierIndex, rate);
                }
                else
                {
                    int consumerIndex = Array.IndexOf(diffInColumns, maxInColumns);
                    rate = columns[consumerIndex].Min();

                    int supplierIndex = Array.IndexOf(columns[consumerIndex], rate);
                    MessageBox.Show("Column: Supplier with stock " + _suppliers[supplierIndex].GetStock() + " Consumer with stock " + _consumers[consumerIndex].GetRequirement() + " on rate " + rate);
                    makeTransportation(consumerIndex, supplierIndex, rate);
                }
            }

            MessageBox.Show("Total " + _solution.getTotal());
        }


        private int DiffBtwTwoMinValues (int[] values)
        {
            if (values.Length == 1)
            {
                return values[0];
            }

            int [] arr = new int[values.Length];

            Array.Copy(values, arr, values.Length);
            Array.Sort(arr);

            return Math.Abs(arr[1] - arr[0]);
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
                    if (_suppliers[j].GetRates().Length > 0)
                    {
                        columns[i][j] = _suppliers[j].GetRates()[i];
                    }
                }
            }

            return columns;
        }

        private void makeTransportation(int consumerIndex, int supplierIndex, int rate)
        {
            Сonsumer consumer = _consumers[consumerIndex];
            Supplier supplier = _suppliers[supplierIndex];

            if (consumer.GetRequirement() >= supplier.GetStock())
            {
                _solution.AddTransportation(supplier.GetStock(), rate);
                consumer.SetRequirement(consumer.GetRequirement() - supplier.GetStock());
                _suppliers = _suppliers.Where(val => val != supplier).ToArray();
            }
            else
            {
                _solution.AddTransportation(consumer.GetRequirement(), rate);
                supplier.SetStock(supplier.GetStock() - consumer.GetRequirement());
                removeConsumer(consumerIndex);
            }
        }

        private void removeConsumer(int consumerIndex)
        {
            _consumers = _consumers.Where(val => val != _consumers[consumerIndex]).ToArray();

            foreach (Supplier supplier in _suppliers)
            {
                supplier.removeRate(consumerIndex);
            }
        }
    }
}