namespace transport_problem.Table
{
    public class Transportation
    {
        private int _cargoCnt;
        private int _rate;

        public Transportation(int c, int r)
        {
            _cargoCnt = c;
            _rate = r;
        }

        public int GetRate()
        {
            return _rate;
        }

        public int GetCargo()
        {
            return _cargoCnt;
        }
    }
}