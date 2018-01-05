using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    // Using MSTest built-in to Visual Studio
    // ...however, MSTest can only test *all* unit tests
    [TestClass]
    public class ReservationTests_usingMSTest
    {
        [TestMethod]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            // Arrange
            var reservation = new Reservation();

            // Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCancelledBy_UserMadeTheReservation_ReturnsTrue()
        {
            // Arrange
            var user = new User();
            var reservation = new Reservation { MadeBy = user };

            // Act
            var result = reservation.CanBeCancelledBy(user);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCancelledBy_UserDidNotMakeTheReservation_ReturnsFalse()
        {
            // Assert
            var user1 = new User();
            var user2 = new User();
            var reservation = new Reservation { MadeBy = user1 };

            // Act
            var result = reservation.CanBeCancelledBy(user2);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
