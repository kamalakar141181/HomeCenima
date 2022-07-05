using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator;
namespace CalculatorUnitTest
{
    [TestClass]
    public class CalculatorUnitTestCases
    {
        [TestMethod]
        public void TestMethodAdd()
        {
            Operations bm = new Operations();
            double res = bm.Add(10, 10);
            Assert.AreEqual(res, 20);
        }
        [TestMethod]
        public void TestMethodSubstract()
        {
            Operations bm = new Operations();
            double res = bm.Substract(10, 10);
            Assert.AreEqual(res, 0);
        }
        [TestMethod]
        public void TestMethodDivide()
        {
            Operations bm = new Operations();
            double res = bm.divide(10, 5);
            Assert.AreEqual(res, 2);
        }
        [TestMethod]
        public void TestMethodMultiply()
        {
            Operations bm = new Operations();
            double res = bm.Multiply(10, 10);
            Assert.AreEqual(res, 100);
        }
    }
}
