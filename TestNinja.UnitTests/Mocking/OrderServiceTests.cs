using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class OrderServiceTests
    {
        // The PlaceOrder method interacts with "storage" object
        // ...so we want to verify proper interaction
        //
        //public int PlaceOrder(Order order)
        //{
        //    var orderId = _storage.Store(order);

        //    // Some other work

        //    return orderId;
        //}

        [Test]
        public void PlaceOrder_WhenCalled_StoreTheOrder()
        {
            // Arrange
            var storage = new Mock<IStorage>();
            var orderService = new OrderService(storage.Object);
            var order = new Order();

            // Act
            orderService.PlaceOrder(order);

            // Assert
            storage.Verify(x => x.Store(order));
        }
    }
}
