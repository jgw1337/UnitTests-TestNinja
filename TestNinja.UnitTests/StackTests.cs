using NUnit.Framework;
using Shouldly;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class StackTests
    {
        private Stack<string> _fruits;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _fruits = new Stack<string>();
            _fruits.Push("apple");
            _fruits.Push("banana");
            _fruits.Push("cherry");
        }

        [Test]
        public void Count_WhenCalled_ReturnsCountOfList()
        {
            // Act
            var result = _fruits.Count;

            // Assert
            result.ShouldBe(3);
        }

        [Test]
        public void Count_WhenCalledAfterAddingItem_ReturnsCountOfList()
        {
            // Arrange
            _fruits.Push("date");

            // Act
            var result = _fruits.Count;

            // Assert
            result.ShouldBe(4);
        }

        [Test]
        public void Peek_WhenCalled_ReturnsLastItemOfList()
        {
            // Act
            var result = _fruits.Peek();

            // Assert
            result.ShouldSatisfyAllConditions(
                () => _fruits.Count.ShouldBe(3),
                () => result.ShouldBe("cherry")
            );
        }

        [Test]
        public void Peek_WhenCalledOnEmptyStack_ThrowsInvalidOperationException()
        {
            // Arrange
            var stack = new Stack<string>();

            // Act and Assert
            Should.Throw<InvalidOperationException>(
                () => stack.Peek()
            );
        }

        [Test]
        public void Pop_WhenCalled_ReturnsThenRemovesLastItemOfList()
        {
            // Act
            var result = _fruits.Pop();

            // Assert
            result.ShouldSatisfyAllConditions(
                () => _fruits.Count.ShouldBe(2),
                () => result.ShouldBe("cherry")
            );
        }

        [Test]
        public void Pop_WhenCalledOnEmptyStack_ThrowsInvalidOperationException()
        {
            // Arrange
            var stack = new Stack<string>();

            // Act
            Should.Throw<InvalidOperationException>(
                () => stack.Pop()
            );
        }

        [Test]
        public void Push_WithInvalidData_ReturnsException()
        {
            // Act and Assert
            Should.Throw<ArgumentNullException>(
                () => _fruits.Push(null)
            );
        }

        [Test]
        public void Push_WithValidData_ReturnsVoid()
        {
            // Act
            _fruits.Push("date");

            // Assert
            _fruits.Count.ShouldBe(4);
        }
    }
}
