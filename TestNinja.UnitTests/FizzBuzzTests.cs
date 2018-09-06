using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{

    [TestFixture]
    class FizzBuzzTests
    {
        #region Typical
        [Test]
        public void GetOutput_InputDivisibleBy3And5_ReturnsFizzBuzz()
        {
            // Arrange

            // Act
            var result = FizzBuzz.GetOutput(15);

            // Assert
            result.ShouldBe("FizzBuzz");
        }

        [Test]
        public void GetOutput_InputDivisibleBy3Only_ReturnsFizz()
        {
            // Arrange


            // Act
            var result = FizzBuzz.GetOutput(3);

            // Assert
            result.ShouldBe("Fizz");
        }

        [Test]
        public void GetOutput_InputDivisibleBy5Only_ReturnsBuzz()
        {
            // Arrange

            // Act
            var result = FizzBuzz.GetOutput(5);

            // Assert
            result.ShouldBe("Buzz");
        }

        [Test]
        public void GetOutput_InputNotDivisibleBy3Or5_ReturnsSameNumber()
        {
            // Arrange

            // Act
            var result = FizzBuzz.GetOutput(1);

            // Assert
            result.ShouldBe("1");
        }

        #endregion

        #region Monolithic but is it better?

        [Test]
        [TestCase(-1, "-1")]
        [TestCase(0, "FizzBuzz")]
        [TestCase(1, "1")]
        [TestCase(2, "2")]
        [TestCase(3, "Fizz")]
        [TestCase(4, "4")]
        [TestCase(5, "Buzz")]
        [TestCase(6, "Fizz")]
        [TestCase(7, "7")]
        [TestCase(8, "8")]
        [TestCase(9, "Fizz")]
        [TestCase(10, "Buzz")]
        [TestCase(11, "11")]
        [TestCase(12, "Fizz")]
        [TestCase(13, "13")]
        [TestCase(14, "14")]
        [TestCase(15, "FizzBuzz")]
        [TestCase(30, "FizzBuzz")]
        [TestCase(45, "FizzBuzz")]
        public void GetOutput_WhenCalledWithInput_ReturnsExpectedValue(int input, string expected)
        {
            // Arrange

            // Act
            var result = FizzBuzz.GetOutput(input);

            // Assert
            result.ShouldBe(expected);
        }

        #endregion

    }
}
