using Moq;
using NUnit.Framework;
using Shouldly;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class EmployeeControllerDITests
    {
        private Mock<IEmployeeStorage> _empStore;
        private EmployeeControllerDI _controller;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _empStore = new Mock<IEmployeeStorage>();
            _controller = new EmployeeControllerDI(_empStore.Object);
        }

        [Test]
        public void DeleteEmployee_WithValidData_InteractsWithEmpStore()
        {
            // Arrange

            // Act
            _controller.DeleteEmployee(1);

            // Assert
            _empStore.Verify(x => x.Delete(1));
        }

        [Test]
        public void DeleteEmployee_WithValidData_RedirectsToEmployeesRazorView()
        {
            // Arrange
            _empStore.Setup(x => x.Delete(1));

            // Act
            var result = _controller.DeleteEmployee(1);

            // Assert
            result.ShouldBeOfType<RedirectResult>();
        }

    }
}
