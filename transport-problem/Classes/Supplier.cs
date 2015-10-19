namespace transport_problem.Classes
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

    }
}