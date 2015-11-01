using System;
using System.Linq;
using System.Windows.Forms;

namespace transport_problem.Table
{
    public static class Helpers
    {
        public static int DiffBtwTwoMinValuesInArray(int[] values)
        {
            if (values.Length == 1)
                return values[0];

            int[] copy = new int[values.Length];
            Array.Copy(values, copy, values.Length);

            Array.Sort(copy);

            return Math.Abs(copy[1] - copy[0]);
        }

    }
}