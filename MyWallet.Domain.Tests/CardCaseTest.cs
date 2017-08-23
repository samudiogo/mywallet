using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWallet.Domain.Models;

namespace MyWallet.Domain.Tests
{
    [TestClass]
    public class CardCaseTest
    {

        private Card _fakeCard;

        [TestInitialize]
        public void Init()
        {
            var fakeCardNumber = "4716722155997238";
            var dueDate = new DateTime(2017, 8, 1);
            var expireDate = dueDate.AddYears(4).AddMonths(6);
            var cvv = "238";
            var limit = 500.00m;
            _fakeCard = new Card(Guid.NewGuid(), fakeCardNumber, dueDate, expireDate, cvv, limit, false);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Raise_Exception_When_Limit_is_setted_Negative()
        {
            //Arrange:
            var fakeCardNumber = "4716722155997238";
            var dueDate = new DateTime(2017, 8, 1);
            var expireDate = dueDate.AddYears(4).AddMonths(6);
            var cvv = "238";
            var limit = 500.00m;

            //act:
            var sut = new Card(Guid.NewGuid(), fakeCardNumber, dueDate, expireDate, cvv, -limit, false);

            //Assert:
            Assert.IsInstanceOfType(sut, typeof(Card));
        }

        [TestMethod]
        public void Compare_Credit_Cards_with_same_due_date()
        {
            //Arrange:
            var sut = _fakeCard;

            var other = _fakeCard;

            //Act:
            var result = sut.IsSameDueDate(other);

            //Assert:
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Validate_Is_ExpiredCard()
        {
            //Arrange:
            var fakeCardNumber = "4716722155997238";
            var dueDate = new DateTime(2017, 8, 1);
            var expireDate = dueDate.AddYears(-4).AddMonths(-6);
            var cvv = "238";
            var limit = 500.00m;

            var sut = new Card(Guid.NewGuid(), fakeCardNumber, dueDate, expireDate, cvv, limit, false);
            //Act:
            var result = sut.IsExpiredCard();
            //Assert:
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Validate_Is_over_due_Card()
        {
            //Arrange:
            var fakeCardNumber = "4716722155997238";
            var dueDate = new DateTime(2017, 8, 1);
            var expireDate = dueDate.AddYears(-4).AddMonths(-6);
            var cvv = "238";
            var limit = 500.00m;

            var sut = new Card(Guid.NewGuid(), fakeCardNumber, dueDate, expireDate, cvv, limit, false);
            //Act:
            var result = sut.IsOverDue();
            //Assert:
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReturnFalse_when_purchase_is_over_limit()
        {
            //arrange:
            var macbookPro = 1900.00m;
            var sut = _fakeCard;
            //act:
            var result = sut.IsPurchaseFitsLimit(macbookPro);

            //Assert:
            Assert.IsFalse(result);
        }
    }
}
