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
        private Booking _bookingExisting;
        private IBookingRepository _repository;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _bookingExisting = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2000, 1, 10),
                DepartureDate = DepartOn(2000, 1, 15),
                Reference = "a"
            };

            _repository = Mock.Of<IBookingRepository>();
            Mock.Get(_repository)
                .Setup(r => r.GetActiveBookings(1))
                .Returns(new List<Booking> { _bookingExisting }.AsQueryable());

        }

        [Test]
        public void
            BookingStartsAndEndsBeforeExistingBooking_ReturnsEmptyString()
        {
            // Arrange
            var bookingCurrent = new Booking
            {
                Id = 1,
                ArrivalDate = Before(_bookingExisting.ArrivalDate, days: 2),
                DepartureDate = Before(_bookingExisting.ArrivalDate)
            };

            // Act
            var result = BookingHelperWithDependencyInjection.OverlappingBookingsExist(bookingCurrent, _repository);

            // Assert
            result.ShouldBeEmpty();
        }

        [Test]
        public void BookingStartsBeforeAndEndsDuringExistingBooking_ReturnsExistingBookingReference()
        {
            // Arrange
            var bookingCurrent = new Booking
            {
                Id = 1,
                ArrivalDate = Before(_bookingExisting.ArrivalDate),
                DepartureDate = After(_bookingExisting.ArrivalDate)
            };

            // Act
            var result = BookingHelperWithDependencyInjection.OverlappingBookingsExist(bookingCurrent, _repository);

            // Assert
            result.ShouldBe(_bookingExisting.Reference);
        }

        [Test]
        public void BookingStartsBeforeAndEndsAfterExistingBooking_ReturnsExistingBookingReference()
        {
            // Arrange
            var bookingCurrent = new Booking
            {
                Id = 1,
                ArrivalDate = Before(_bookingExisting.ArrivalDate),
                DepartureDate = After(_bookingExisting.DepartureDate)
            };

            // Act
            var result = BookingHelperWithDependencyInjection.OverlappingBookingsExist(bookingCurrent, _repository);

            // Assert
            result.ShouldBe(_bookingExisting.Reference);
        }

        [Test]
        public void BookingStartsAndEndsDuringExistingBooking_ReturnsExistingBookingReference()
        {
            // Arrange
            var bookingCurrent = new Booking
            {
                Id = 1,
                ArrivalDate = After(_bookingExisting.ArrivalDate),
                DepartureDate = Before(_bookingExisting.DepartureDate)
            };

            // Act
            var result = BookingHelperWithDependencyInjection.OverlappingBookingsExist(bookingCurrent, _repository);

            // Assert
            result.ShouldBe(_bookingExisting.Reference);
        }

        [Test]
        public void BookingStartsDuringAndEndsAfterExistingBooking_ReturnsExistingBookingReference()
        {
            // Arrange
            var bookingCurrent = new Booking
            {
                Id = 1,
                ArrivalDate = After(_bookingExisting.ArrivalDate),
                DepartureDate = After(_bookingExisting.DepartureDate)
            };

            // Act
            var result = BookingHelperWithDependencyInjection.OverlappingBookingsExist(bookingCurrent, _repository);

            // Assert
            result.ShouldBe(_bookingExisting.Reference);
        }

        [Test]
        public void BookingStartsAndEndsAfterExistingBooking_ReturnsEmptyString()
        {
            // Arrange
            var bookingCurrent = new Booking
            {
                Id = 1,
                ArrivalDate = After(_bookingExisting.DepartureDate),
                DepartureDate = After(_bookingExisting.DepartureDate, days: 2)
            };

            // Act
            var result = BookingHelperWithDependencyInjection.OverlappingBookingsExist(bookingCurrent, _repository);

            // Assert
            result.ShouldBeEmpty();
        }

        [Test]
        public void BookingsOverlapButCurrentBookingIsCancelled_ReturnsEmptyString()
        {
            // Arrange
            var bookingCurrent = new Booking
            {
                Id = 1,
                ArrivalDate = After(_bookingExisting.ArrivalDate),
                DepartureDate = After(_bookingExisting.DepartureDate),
                Status = "Cancelled"
            };

            // Act
            var result = BookingHelperWithDependencyInjection.OverlappingBookingsExist(bookingCurrent, _repository);

            // Assert
            result.ShouldBeEmpty();
        }

        [Test]
        public void BookingsOverlapButExistingBookingIsCancelled_ReturnsEmptyString()
        {
            // Arrange
            Mock.Get(_repository)
                .Setup(r => r.GetActiveBookings(1))
                .Returns(new List<Booking> { }.AsQueryable());

            var bookingCurrent = new Booking
            {
                Id = 1,
                ArrivalDate = After(_bookingExisting.ArrivalDate),
                DepartureDate = After(_bookingExisting.DepartureDate)
            };

            // Act
            var result = BookingHelperWithDependencyInjection.OverlappingBookingsExist(bookingCurrent, _repository);

            // Assert
            result.ShouldBeEmpty();
        }

        // These private helper classes help reduce dependency on magic numbers
        // and helps reveal the intent of our unit tests
        #region Private Helper Methods

        private DateTime ArriveOn(int year, int month, int day)
        {
            // Assuming check-in time of 2pm
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            // Assuming check-out time of 10am
            return new DateTime(year, month, day, 10, 0, 0);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        #endregion Private Helper Methods
    }
}
