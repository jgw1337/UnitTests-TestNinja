using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelperWithDependencyInjection_OverlappingBookingsExistTests
    {
        private IBookingRepository _repository;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _repository = Mock.Of<IBookingRepository>();
        }

        [Test]
        public void Booking1StartsAndEndsBeforeExistingBooking_ReturnsEmptyString()
        {
            // Arrange
            var bookingCurrent = new Booking
            {
                Id = 1,
                ArrivalDate = new DateTime(2000, 1, 1, 0, 0, 0),
                DepartureDate = new DateTime(2000, 1, 5, 0, 0, 0),
                Reference = "a"
            };
            var bookingExisting = new Booking
            {
                Id = 2,
                ArrivalDate = new DateTime(2000, 1, 10, 0, 0, 0),
                DepartureDate = new DateTime(2000, 1, 15, 0, 0, 0),
                Reference = "a"
            };
            Mock.Get(_repository)
                .Setup(r => r.GetActiveBookings(1))
                .Returns(new List<Booking> { bookingExisting }.AsQueryable());

            // Act
            var result = BookingHelperWithDependencyInjection.OverlappingBookingsExist(bookingCurrent, _repository);

            // Assert
            result.ShouldBeEmpty();
        }

    }
}
