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
    }
}