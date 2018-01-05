using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class MathTests
    {
        [Test]
        public void Add_AddTwoNumbers_ReturnsSum()
        {
            // Arrange
            Math m = new Math();

            // Act
            var result = m.Add(1, 2);

            // Assert
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Max_1stArgumentIsGreater_Returns1stArgument()
        {
            Math m = new Math();

            var result = m.Max(2, 1);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_2ndArgumentIsGreater_Returns2ndArgument()
        {
            Math m = new Math();

            var result = m.Max(1, 2);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_BothArgumentsAreEqual_ReturnsSameArgument()
        {
            Math m = new Math();

            var result = m.Max(1, 1);

            Assert.That(result, Is.EqualTo(1));
        }
    }
}
