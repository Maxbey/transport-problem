using System;
using System.Linq;

namespace transport_problem.Table
{
    public static class Helpers
    {
        public static int DiffBtwTwoMinValuesInArray(int[] values)
        {
            if (values.Length == 1)
                return values[0];

            int a = values.Min();
            int aIndex = Array.IndexOf(values, a);

            int b = values.Skip(aIndex + 1).Min();

            return Math.Abs(a - b);
        }
    }
}