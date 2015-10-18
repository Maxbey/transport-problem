namespace transport_problem.Classes
{
    public class Supplier
    {
        public Supplier(int rate, int stock)
        {
            SetRate(rate);
            SetStock(stock);
        }

        private int stock;
        private int rate;

        public int GetStock()
        {
            return stock;
        }

        public int GetRate()
        {
            return rate;
        }

        public void SetStock(int stock)
        {
            this.stock = stock;
        }

        public void SetRate(int rate)
        {
            this.rate = rate;
        }

    }
}