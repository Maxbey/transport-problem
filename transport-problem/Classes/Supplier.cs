namespace transport_problem.Table
{
    public class Supplier
    {
        private int stock;
        private int[] rates;

        public Supplier(int[] rates, int stock)
        {
            SetRate(rates);
            SetStock(stock);
        }

        public int GetStock()
        {
            return stock;
        }

        public int[] GetRates()
        {
            return rates;
        }

        public void SetStock(int stock)
        {
            this.stock = stock;
        }

        public void SetRate(int[] rates)
        {
            this.rates = rates;
        }

        public void removeRate(int index)
        {
            int[] rates = GetRates();
            int[] newRates = new int[rates.Length - 1];

            for (int i = 0, c = 0; i < rates.Length; i++)
            {
                if (i == index)
                    continue;

                newRates[c] = rates[i];
                c++;
            }

            SetRate(newRates);
        }

    }
}