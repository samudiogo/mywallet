using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWallet.Domain.Models;

namespace MyWallet.Domain.Tests
{
    [TestClass]
    public class PurchaseCaseTest
    {
        private User _user;
        private Wallet _wallet;

        [TestInitialize]
        public void Init()
        {
            _user = new User(Guid.Empty, "Samuel",
                "samudiogo@stone.com.br","123",Guid.NewGuid().ToString("N"));
            _wallet = new Wallet(Guid.Empty, _user);
        }

        [TestMethod]
        public void Successful_Little_Purchase()
        {
            //Arrange:
            var nubank = new Card(Guid.Empty, "123", new DateTime(2017,10,10),
                new DateTime(),"123",300m,true);
            var visa = new Card(Guid.Empty, "456", new DateTime(2017, 10, 10),
                new DateTime(), "123", 500m, true);
            var master = new Card(Guid.Empty, "789", new DateTime(2017, 10, 10),
                new DateTime(), "123", 120m, true);

            _wallet.Cards.Add(nubank);
            _wallet.Cards.Add(visa);
            _wallet.Cards.Add(master);

            _wallet.AjustRealtLimit(_wallet.GetMaximmumLimit());

            var groceryPurchase = new Purchase("ZS - rise and meat", 30m);
            
            //Act:
            _wallet.Buy(groceryPurchase);
            var usedCard = _wallet.Cards.FirstOrDefault(c => c.Id.Equals(master.Id));
            //Assert
            Assert.IsNotNull(usedCard);
            Assert.IsTrue(usedCard.Purchases.Contains(groceryPurchase));
        }

        [TestMethod]
        public void Successful_Big_Purchase_with_many_cards()
        {
            //Arrange:
            var espressoMachine = new Purchase("Espresso Machine Walmart",799.99m);
            
            var nubank = new Card(Guid.Empty, "123", new DateTime(2017, 10, 10),
                new DateTime(), "123", 300m, true);
            var visa = new Card(Guid.Empty, "456", new DateTime(2017, 10, 10),
                new DateTime(), "123", 500m, true);
            var master = new Card(Guid.Empty, "789", new DateTime(2017, 10, 10),
                new DateTime(), "123", 120m, true);

            _wallet.Cards.Add(nubank);
            _wallet.Cards.Add(visa);
            _wallet.Cards.Add(master);

            _wallet.AjustRealtLimit(_wallet.GetMaximmumLimit());

            
            //Act:
            _wallet.Buy(espressoMachine);

            var usedMaster = _wallet.Cards.FirstOrDefault(c => c.Id.Equals(master.Id));
            var usedNubank = _wallet.Cards.FirstOrDefault(c => c.Id.Equals(nubank.Id));
            var usedVisa = _wallet.Cards.FirstOrDefault(c => c.Id.Equals(visa.Id));

            //Assert
            Assert.IsNotNull(usedMaster);
            Assert.IsTrue(usedMaster.Purchases.Any(p=> p.Amount.Equals(120m)));

            Assert.IsNotNull(usedNubank);
            Assert.IsTrue(usedNubank.Purchases.Any(p => p.Amount.Equals(300m)));

            Assert.IsNotNull(usedVisa);
            Assert.IsTrue(usedVisa.Purchases.Any(p => p.Amount.Equals(379.99m)));
        }
    }
}