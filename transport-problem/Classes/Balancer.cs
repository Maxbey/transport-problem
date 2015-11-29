using System;
using System.Collections;
using System.Linq;

namespace transport_problem.Table
{
    public class Balancer
    {
        private ArrayList _suppliers;
        private ArrayList _consumers;

        private int _stock;
        private int _requirement;

        public Balancer(ArrayList suppliers, ArrayList consumers)
        {
            _suppliers = suppliers;
            _consumers = consumers;
        }

        public bool CheckBalance()
        {
            Calculate();

            return _stock == _requirement;
        }

        public void Balance()
        {
            if (_stock > _requirement)
            {
                AddimaginaryConsumer();
            }

            AddImaginarySupplier();
        }

        private void Calculate()
        {
            _stock = 0;
            _requirement = 0;

            foreach (Supplier supplier in _suppliers)
            {
                _stock += supplier.GetStock();
            }

            foreach (Consumer consumer in _consumers.Cast<Consumer>())
            {
                _requirement += consumer.GetRequirement();
            }
        }

        private void AddimaginaryConsumer()
        {
            _consumers.Add(new Consumer(_stock - _requirement));

            foreach (Supplier supplier in _suppliers)
            {
                ArrayList ratesList = new ArrayList();

                foreach (int rate in supplier.GetRates())
                {
                    ratesList.Add(rate);
                }

                ratesList.Add(0);
                supplier.SetRates(ratesList.Cast<int>().ToArray());
            }
        }

        private void AddImaginarySupplier()
        {
            _suppliers.Add(new Supplier(new int[_consumers.Count], _requirement - _stock));
        }
    }
}