using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class HousekeeperServiceWithDependencyInjectionTests
    {
        private HousekeeperServiceWithDependencyInjection _service;

        private IUnitOfWork _unitOfWork;
        private IStatementGenerator _statementGenerator;
        private IEmailSender _emailSender;
        private IXtraMessageBox _messageBox;

        private Housekeeper _housekeeper;
        private readonly DateTime _statementDate = new DateTime(2000, 1, 1);

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _unitOfWork = Mock.Of<IUnitOfWork>();
            _statementGenerator = Mock.Of<IStatementGenerator>();
            _emailSender = Mock.Of<IEmailSender>();
            _messageBox = Mock.Of<IXtraMessageBox>();

            _service = new HousekeeperServiceWithDependencyInjection(_unitOfWork, _statementGenerator, _emailSender, _messageBox);

            _housekeeper = new Housekeeper
            {
                Email = "a",
                FullName = "b",
                Oid = 1,
                StatementEmailBody = "c"
            };
            Mock.Get(_unitOfWork)
                .Setup(uow => uow.Query<Housekeeper>())
                .Returns(new List<Housekeeper> { _housekeeper }.AsQueryable());
        }

        [Test]
        public void SendStatementEmails_WhenCalled_InteractsWithStatementGenerator()
        {
            // Arrange

            // Act
            _service.SendStatementEmails(_statementDate);

            // Assert
            Mock.Get(_statementGenerator)
                .Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }

        [Test]
        public void SendStatementEmails_HousekeepEmailIsNull_ShouldNotInteractWithStatementGenerator()
        {
            // Arrange
            _housekeeper.Email = null;

            // Act
            _service.SendStatementEmails(_statementDate);

            // Assert
            Mock.Get(_statementGenerator)
                .Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), Times.Never);
        }

        [Test]
        public void SendStatementEmails_HousekeepEmailIsWhitespace_ShouldNotInteractWithStatementGenerator()
        {
            // Arrange
            _housekeeper.Email = " ";

            // Act
            _service.SendStatementEmails(_statementDate);

            // Assert
            Mock.Get(_statementGenerator)
                .Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), Times.Never);
        }

        [Test]
        public void SendStatementEmails_HousekeepEmailIsEmpty_ShouldNotInteractWithStatementGenerator()
        {
            // Arrange
            _housekeeper.Email = "";

            // Act
            _service.SendStatementEmails(_statementDate);

            // Assert
            Mock.Get(_statementGenerator)
                .Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), Times.Never);
        }
    }
}
