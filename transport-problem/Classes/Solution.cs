using System.Collections.Generic;
using System.Windows.Forms;

namespace transport_problem.Classes
{
    public class Solution
    {
        private List<int[]> _transportations;

        public Solution()
        {
            _transportations = new List<int[]>();
        }

        public void AddTransportation(int cargoCnt, int rate)
        {
            var transportation = new int[2];

            transportation[0] = cargoCnt;
            transportation[1] = rate;

            _transportations.Add(transportation);
        }

        public List<int[]> GetTransportations()
        {
            return _transportations;
        }

        public int getTotal()
        {
            int total = 0;

            foreach (int[] tr in _transportations)
            {
                total += tr[0]*tr[1];
            }

            return total;
        }
    }
}