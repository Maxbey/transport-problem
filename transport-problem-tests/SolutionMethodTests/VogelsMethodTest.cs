using NUnit.Framework;
using transport_problem.SolutionMethods;
using transport_problem.Table;

namespace transport_problem_tests.SolutionMethodTests
{
    [TestFixture]
    public class VogelsMethodTest
    {
        [Test]
        public void TotalPriceTestCaseOne()
        {
            Supplier[] suppliers = new Supplier[3];
            Consumer[] consumers = new Consumer[3];

            suppliers[0] = new Supplier(new int[3]{2, 5, 2}, 180);
            suppliers[1] = new Supplier(new int[3] { 7, 7, 13 }, 300);
            suppliers[2] = new Supplier(new int[3] { 3, 6, 8 }, 120);

            consumers[0] = new Consumer(110);
            consumers[1] = new Consumer(350);
            consumers[2] = new Consumer(140);

            VogelsMethod method = new VogelsMethod(suppliers, consumers);

            Assert.AreEqual(2970, method.GetSolution().GetTotalTransportationsPrice());
        }

        [Test]
        public void TotalPriceTestCaseTwo()
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

            VogelsMethod method = new VogelsMethod(suppliers, consumers);

            Assert.AreEqual(100, method.GetSolution().GetTotalTransportationsPrice());
        }

        [Test]
        public void TotalPriceTestCaseThree()
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

            VogelsMethod method = new VogelsMethod(suppliers, consumers);

            Assert.AreEqual(170, method.GetSolution().GetTotalTransportationsPrice());
        }
    }
}