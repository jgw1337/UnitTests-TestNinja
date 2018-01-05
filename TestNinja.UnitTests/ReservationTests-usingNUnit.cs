using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    // Using NUnit third-party package
    [TestFixture]
    public class ReservationTests_usingNUnit
    {
        [Test]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            // Arrange
            var reservation = new Reservation();

            // Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            // Assert
            // Note: All three assertion statements are valid for NUnit
            Assert.IsTrue(result);
            Assert.That(result, Is.True);
            Assert.That(result == true);
        }

        [Test]
        public void CanBeCancelledBy_UserMadeTheReservation_ReturnsTrue()
        {
            // Arrange
            var user = new User();
            var reservation = new Reservation { MadeBy = user };

            // Act
            var result = reservation.CanBeCancelledBy(user);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_UserDidNotMakeTheReservation_ReturnsFalse()
        {
            // Assert
            var user1 = new User();
            var user2 = new User();
            var reservation = new Reservation { MadeBy = user1 };

            // Act
            var result = reservation.CanBeCancelledBy(user2);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
