using NUnit.Framework;
using transport_problem.SolutionMethods;
using transport_problem.Table;

namespace transport_problem_tests.SolutionMethodTests
{
    [TestFixture]
    public class PotentialsMethodTest
    {
        [Test]
        public void IsOptimalTestCaseOne()
        {
            Supplier[] suppliers = new Supplier[3];
            Consumer[] consumers = new Consumer[4];

            suppliers[0] = new Supplier(new int[4] { 2, 3, 2, 4 }, 30);
            suppliers[1] = new Supplier(new int[4] { 3, 2, 5, 1 }, 40);
            suppliers[2] = new Supplier(new int[4] { 4, 3, 2, 6 }, 20);

            consumers[0] = new Consumer(20);
            consumers[1] = new Consumer(30);
            consumers[2] = new Consumer(30);
            consumers[3] = new Consumer(10);

            VogelsMethod vogelsMethod = new VogelsMethod(suppliers, consumers);

            Table solution = vogelsMethod.GetSolution();

            PotentialsMethod potentialsMethod = new PotentialsMethod(solution);

            Assert.True(potentialsMethod.IsOptimal());
        }

        [Test]
        public void IsOptimalTestCaseTwo()
        {
            Supplier[] suppliers = new Supplier[3];
            Consumer[] consumers = new Consumer[4];

            suppliers[0] = new Supplier(new int[4] { 2, 3, 2, 4 }, 30);
            suppliers[1] = new Supplier(new int[4] { 3, 2, 5, 1 }, 40);
            suppliers[2] = new Supplier(new int[4] { 4, 3, 2, 6 }, 20);

            consumers[0] = new Consumer(20);
            consumers[1] = new Consumer(30);
            consumers[2] = new Consumer(30);
            consumers[3] = new Consumer(10);

            NorthwestCornerMethod ncMethod = new NorthwestCornerMethod(suppliers, consumers);

            Table solution = ncMethod.GetSolution();

            PotentialsMethod potentialsMethod = new PotentialsMethod(solution);

            Assert.False(potentialsMethod.IsOptimal());
        }

        [Test]
        public void OptimizeTestCaseOne()
        {
            Supplier[] suppliers = new Supplier[3];
            Consumer[] consumers = new Consumer[4];

            suppliers[0] = new Supplier(new int[4] { 2, 3, 2, 4 }, 30);
            suppliers[1] = new Supplier(new int[4] { 3, 2, 5, 1 }, 40);
            suppliers[2] = new Supplier(new int[4] { 4, 3, 2, 6 }, 20);

            consumers[0] = new Consumer(20);
            consumers[1] = new Consumer(30);
            consumers[2] = new Consumer(30);
            consumers[3] = new Consumer(10);

            NorthwestCornerMethod ncMethod = new NorthwestCornerMethod(suppliers, consumers);

            Table solution = ncMethod.GetSolution();

            PotentialsMethod potentialsMethod = new PotentialsMethod(solution);

            while (!potentialsMethod.IsOptimal())
            {
                potentialsMethod.Otimize();
            }

            Assert.AreEqual(solution.GetTotalTransportationsPrice(), 170);
        }

        [Test]
        public void OptimizeTestCaseTwo()
        {
            Supplier[] suppliers = new Supplier[4];
            Consumer[] consumers = new Consumer[5];

            suppliers[0] = new Supplier(new int[5] { 9, 5, 7, 10, 18 }, 78);
            suppliers[1] = new Supplier(new int[5] { 36, 29, 6, 38, 40 }, 94);
            suppliers[2] = new Supplier(new int[5] { 41, 20, 11, 25, 19 }, 29);
            suppliers[3] = new Supplier(new int[5] { 30, 28, 13, 39, 50 }, 86);

            consumers[0] = new Consumer(49);
            consumers[1] = new Consumer(60);
            consumers[2] = new Consumer(78);
            consumers[3] = new Consumer(50);
            consumers[4] = new Consumer(50);

            NorthwestCornerMethod ncMethod = new NorthwestCornerMethod(suppliers, consumers);

            Table solution = ncMethod.GetSolution();

            PotentialsMethod potentialsMethod = new PotentialsMethod(solution);

            while (!potentialsMethod.IsOptimal())
            {
                potentialsMethod.Otimize();
            }

            Assert.AreEqual(solution.GetTotalTransportationsPrice(), 4870);
        }

        [Test]
        public void OptimizeTestCaseThree()
        {
            Supplier[] suppliers = new Supplier[3];
            Consumer[] consumers = new Consumer[4];

            suppliers[0] = new Supplier(new int[4] { 5, 2, 1, 8 }, 10);
            suppliers[1] = new Supplier(new int[4] { 2, 1, 2, 3 }, 10);
            suppliers[2] = new Supplier(new int[4] { 4, 8, 1, 4 }, 20);

            consumers[0] = new Consumer(14);
            consumers[1] = new Consumer(15);
            consumers[2] = new Consumer(5);
            consumers[3] = new Consumer(6);

            NorthwestCornerMethod ncMethod = new NorthwestCornerMethod(suppliers, consumers);

            Table solution = ncMethod.GetSolution();

            PotentialsMethod potentialsMethod = new PotentialsMethod(solution);

            while (!potentialsMethod.IsOptimal())
            {
                potentialsMethod.Otimize();
            }

            Assert.AreEqual(solution.GetTotalTransportationsPrice(), 100);
        }
    }
}