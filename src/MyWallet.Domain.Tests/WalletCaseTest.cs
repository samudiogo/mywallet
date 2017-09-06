using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWallet.Domain.Models;

namespace MyWallet.Domain.Tests
{
    [TestClass]
    public class WalletCaseTest
    {
        [TestMethod]
        public void Wallet_Should_have_a_Owner()
        {
            //arrange:
            var user = new User(Guid.NewGuid(), "Samuel", "samudiogo@gmail.com", "123", Guid.NewGuid().ToString("N"));

            //act:
            var sut = new Wallet(Guid.NewGuid(), user);

            //assert:
            Assert.IsNotNull(sut.Owner);
            Assert.Equals(sut.Owner.Name, "Samuel");
        }

        [TestMethod]
        public void Real_Limit_should_to_be_encreased_when_new_card_is_Added_and_real_limit_is_ZERO()
        {
            //arrange:
            var user = new User(Guid.NewGuid(), "Samuel", "samudiogo@gmail.com", "123", Guid.NewGuid().ToString("N"));
            var sut = new Wallet(Guid.NewGuid(), user);
            var nubank = new Card(Guid.Empty, "1234567892", new DateTime(2017, 9, 10), new DateTime(2020, 10, 1), "123", 300m, true);
            var previusRealLimit = sut.RealLimit;
            //act:
            sut.AddNewCard(nubank);
            sut.AjustRealtLimit(nubank.Limit); //Gold plate detected: I could not find the use story for this test method
            var newRealLimit = sut.RealLimit;
            var expectedRealLimit = previusRealLimit + nubank.Limit;
            //assert:
            Assert.AreEqual(newRealLimit, expectedRealLimit,
                $"the real limit has not been updated: newRealLimit:{newRealLimit} | expected: {expectedRealLimit}" );
        }

        [TestMethod]
        public void Should_to_be_decreased_when_new_card_is_removed_and_real_limit_is_not_ZERO()
        {
            //arrange:
            var user = new User(Guid.NewGuid(), "Samuel", "samudiogo@gmail.com", "123", Guid.NewGuid().ToString("N"));
            var sut = new Wallet(Guid.NewGuid(), user);

            var nubank = new Card(Guid.Empty, "1234567892", new DateTime(2017, 9, 10), new DateTime(2020, 10, 1), "123", 300m, true);
            var visa = new Card(Guid.Empty, "1234567893", new DateTime(2017, 9, 5), new DateTime(2020, 11, 1), "123", 500m, true);
            var american = new Card(Guid.Empty, "1234567894", new DateTime(2017, 9, 8), new DateTime(2020, 12, 1), "123", 1000m, true);
            //act:
            sut.AddNewCard(nubank);

            sut.AjustRealtLimit(250m);
            var initialRealLimit = sut.RealLimit; //should be 250m

            sut.RemoveCardByIdAndUpdateLimit(nubank.Id);
            var expectedZeroRealimit = sut.RealLimit; //should be 0m;

            sut.AddNewCard(visa);
            sut.AddNewCard(american);

            var expected1500MaximumLimit = sut.GetMaximmumLimit();

            sut.AjustRealtLimit(1500m);
            sut.RemoveCardByIdAndUpdateLimit(american.Id);

            var expected500MaximumLimit = sut.RealLimit;
            sut.AjustRealtLimit(100m);
            var expected100RealLimit = sut.RealLimit;
            sut.AjustRealtLimit(499m);
            var expected499RealLimit = sut.RealLimit;

            //assert:
            Assert.IsTrue(initialRealLimit > 0m);
            
            Assert.IsTrue(expectedZeroRealimit == 0m);
            Assert.IsTrue(expected1500MaximumLimit == 1500m);
            Assert.IsTrue(expected500MaximumLimit == 500m);
            Assert.IsTrue(expected100RealLimit == 100m);
            Assert.IsTrue(expected499RealLimit == 499m);
        }
        [TestMethod]
        public void Raise_exception_when_user_try_to_set_a_real_limit_that_surpass_the_sum_of_cartds_on_wallet()
        {
            //arrange:
            var user = new User(Guid.NewGuid(), "Samuel", "samudiogo@gmail.com", "123", Guid.NewGuid().ToString("N"));
            var sut = new Wallet(Guid.NewGuid(), user);
            var nubank = new Card(Guid.Empty, "1234567892", new DateTime(2017, 9, 10), new DateTime(2020, 10, 1), "123", 300m, true);
            var previusRealLimit = sut.RealLimit;
            //act:
            sut.AddNewCard(nubank);
            //assert:            
            Assert.ThrowsException<Exception>(() => sut.AjustRealtLimit(nubank.Limit + 1m));

            Assert.IsTrue(previusRealLimit == sut.RealLimit);
        }
    }
}