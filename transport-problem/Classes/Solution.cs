using System.Collections.Generic;
using System.Windows.Forms;

namespace transport_problem.Classes
{
    public class Solution
    {
        private Transportation[][] _transportations;

        public Solution(int suppliersCnt, int consumersCnt)
        {
            _transportations = new Transportation[suppliersCnt][];

            for (int i = 0; i < suppliersCnt; i++)
            {
                _transportations[i] = new Transportation[consumersCnt];

                for (int j = 0; j < consumersCnt; j++)
                {
                    _transportations[i][j] = new Transportation(0, 0);
                }
            }
        }

        public void AddTransportation(int cargoCnt, int rate, int supplierIndex, int consumerIndex)
        {
            var transportation = new Transportation(cargoCnt, rate);

            _transportations[supplierIndex][consumerIndex] = transportation;
        }

        public int getTotal()
        {
            int total = 0;

            foreach (Transportation[] row in _transportations)
            {
                foreach (Transportation transportation in row)
                {
                    MessageBox.Show("Transportation " + transportation.GetCargo() + " x " + transportation.GetRate());

                    total += transportation.GetCargo() * transportation.GetRate();
                }

            }

            return total;
        }
    }
}