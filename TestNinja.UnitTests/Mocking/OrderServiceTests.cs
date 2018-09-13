using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class OrderServiceTests
    {
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
