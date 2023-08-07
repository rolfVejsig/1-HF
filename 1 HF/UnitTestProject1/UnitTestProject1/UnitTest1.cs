using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            Calculator calculator = new Calculator();
            int a = 5;
            int b = 7;

            // Act
            int result = calculator.Add(a, b);

            // Assert
            Assert.Equal(12, result); // This assertion will check if the result is equal to 12
        }
    }
}
