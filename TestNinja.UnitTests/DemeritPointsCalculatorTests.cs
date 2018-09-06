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
    class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _calc;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _calc = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(500)]
        public void CalculateDemeritPoints_SpeedOutOfBounds_ReturnsError(int value)
        {
            // Act and Assert
            Should.Throw<ArgumentOutOfRangeException>(
                () => _calc.CalculateDemeritPoints(value)
            );
        }

        [Test]
        [TestCase(10)]
        [TestCase(65)]
        public void CalculateDemeritPoints_SpeedLessThanOrEqualToSpeedLimit_ReturnsZero(int value)
        {
            // Act
            var result = _calc.CalculateDemeritPoints(value);

            // Assert
            result.ShouldBe(0);
        }

        [Test]
        [TestCase(70, 1)]
        public void CalculateDemeritPoints_SpeedExceedsSpeedLimit_ReturnsDemerits(int speed, int demerit)
        {
            // Arrange

            // Act
            var result = _calc.CalculateDemeritPoints(speed);

            // Assert
            result.ShouldBe(demerit);
        }
    }
}
