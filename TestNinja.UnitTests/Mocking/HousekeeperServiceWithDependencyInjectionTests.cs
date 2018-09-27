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
        private string _statementFilename;

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

            _statementFilename = "filename";
            Mock.Get(_statementGenerator)
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(() => _statementFilename);  // Func/delegate/lambda expression for "lazy evaluation" so we can change it in the Unit Tests
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
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_HousekeepEmailHasProblems_ShouldNotInteractWithStatementGenerator(string email)
        {
            // Arrange
            _housekeeper.Email = email;

            // Act
            _service.SendStatementEmails(_statementDate);

            // Assert
            Mock.Get(_statementGenerator)
                .Verify(
                    sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate),
                    Times.Never
                );
        }

        [Test]
        public void SendStatementEmails_WhenCalled_InteractsWithEmailSender()
        {
            // Arrange
            Mock.Get(_statementGenerator)
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(_statementFilename);

            // Act
            _service.SendStatementEmails(_statementDate);

            // Assert
            VerifyEmailSenderInteractionOccurs();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_StatementFileNameHasProblems_InteractsWithEmailSender(string statementFilename)
        {
            // Arrange
            _statementFilename = statementFilename;

            // Act
            _service.SendStatementEmails(_statementDate);

            // Assert
            VerifyEmailSenderInteractionNeverOccurs();
        }

        private void VerifyEmailSenderInteractionNeverOccurs()
        {
            Mock.Get(_emailSender)
                .Verify(
                    es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                    Times.Never
                );
        }

        private void VerifyEmailSenderInteractionOccurs()
        {
            Mock.Get(_emailSender)
                .Verify(es => es.EmailFile(_housekeeper.Email, _housekeeper.StatementEmailBody, _statementFilename, It.IsAny<string>()));
        }

    }
}
