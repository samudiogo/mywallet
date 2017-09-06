using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWallet.Domain.Models;
using MyWallet.Domain.Services;
using MyWallet.Domain.Services.Contracts;

namespace MyWallet.Domain.Tests
{
    [TestClass]
    public class PurchaseCaseTest
    {


        private User _user;
        private IPurchase _purchase;
        private ICardMonitor _cardMonitor;

        [TestInitialize]
        public void Init()
        {
            _user = new User(Guid.Empty, "Samuel",
                "samudiogo@stone.com.br", "123", Guid.NewGuid().ToString("N"));
            var wallet = new Wallet(Guid.Empty, _user);
            _cardMonitor = new CardMonitor();
            _purchase = new Purchase(ref wallet, _cardMonitor);
        }

        [TestMethod]
        public void Successful_Little_Purchase()
        {
            //Arrange:
            var nubank = new Card(Guid.Empty, "123", new DateTime(2017, 10, 10),
                new DateTime(), "123", 300m, true);
            var visa = new Card(Guid.Empty, "456", new DateTime(2017, 10, 10),
                new DateTime(), "123", 500m, true);
            var master = new Card(Guid.Empty, "789", new DateTime(2017, 10, 10),
                new DateTime(), "123", 120m, true);

            _purchase.Wallet.Cards.Add(nubank);
            _purchase.Wallet.Cards.Add(visa);
            _purchase.Wallet.Cards.Add(master);

            _purchase.Wallet.AjustRealtLimit(_purchase.Wallet.GetMaximmumLimit());

            var groceryPurchase = new Acquisition(Guid.Empty, "ZS - rise and meat", 30m);

            //Act:
            _purchase.Buy(groceryPurchase);
            var usedCard = _purchase.Wallet.Cards.FirstOrDefault(c => c.Id.Equals(master.Id));
            //Assert
            Assert.IsNotNull(usedCard);
            Assert.IsTrue(usedCard.Purchases.Contains(groceryPurchase));
        }

        [TestMethod]
        public void Successful_Big_Purchase_with_many_cards()
        {
            //Arrange:
            var espressoMachine = new Acquisition(Guid.Empty, "Espresso Machine Walmart", 799.99m);

            var nubank = new Card(Guid.Empty, "123", new DateTime(2017, 10, 10),
                new DateTime(), "123", 300m, true);
            var visa = new Card(Guid.Empty, "456", new DateTime(2017, 10, 10),
                new DateTime(), "123", 500m, true);
            var master = new Card(Guid.Empty, "789", new DateTime(2017, 10, 10),
                new DateTime(), "123", 120m, true);

            _purchase.Wallet.Cards.Add(nubank);
            _purchase.Wallet.Cards.Add(visa);
            _purchase.Wallet.Cards.Add(master);

            _purchase.Wallet.AjustRealtLimit(_purchase.Wallet.GetMaximmumLimit());


            //Act:
            _purchase.Buy(espressoMachine);

            var usedMaster = _purchase.Wallet.Cards.FirstOrDefault(c => c.Id.Equals(master.Id));
            var usedNubank = _purchase.Wallet.Cards.FirstOrDefault(c => c.Id.Equals(nubank.Id));
            var usedVisa = _purchase.Wallet.Cards.FirstOrDefault(c => c.Id.Equals(visa.Id));

            //Assert
            Assert.IsNotNull(usedMaster);
            Assert.IsTrue(usedMaster.Purchases.Any(p => p.Amount.Equals(120m)));

            Assert.IsNotNull(usedNubank);
            Assert.IsTrue(usedNubank.Purchases.Any(p => p.Amount.Equals(300m)));

            Assert.IsNotNull(usedVisa);
            Assert.IsTrue(usedVisa.Purchases.Any(p => p.Amount.Equals(379.99m)));
        }

        [TestMethod]
        public void Sucessful_three_aquiriments()
        {
            //arrange:
            var groceryPurchase = new Acquisition(Guid.Empty, "ZS - rise and meat", 100m);
            var wardrobePurchase = new Acquisition(Guid.Empty, "ZS - rise and meat", 599.99m);
            var concertPurchase = new Acquisition(Guid.Empty, "ZS - rise and meat", 60m);

            var nubank = new Card(Guid.Empty, "123", new DateTime(2017, 10, 10),
                new DateTime(), "123", 300m, true);
            var visa = new Card(Guid.Empty, "456", new DateTime(2017, 10, 10),
                new DateTime(), "123", 500m, true);
            var master = new Card(Guid.Empty, "789", new DateTime(2017, 10, 10),
                new DateTime(), "123", 120m, true);

            _purchase.Wallet.Cards.Add(nubank);
            _purchase.Wallet.Cards.Add(visa);
            _purchase.Wallet.Cards.Add(master);

            _purchase.Wallet.AjustRealtLimit(_purchase.Wallet.GetMaximmumLimit());

            //act:
            _purchase.Buy(groceryPurchase);
            _purchase.Buy(wardrobePurchase);
            _purchase.Buy(concertPurchase);

            var usedMaster = _purchase.Wallet.Cards.FirstOrDefault(c => c.Id.Equals(master.Id));
            var usedNubank = _purchase.Wallet.Cards.FirstOrDefault(c => c.Id.Equals(nubank.Id));
            var usedVisa = _purchase.Wallet.Cards.FirstOrDefault(c => c.Id.Equals(visa.Id));

            //Assert:
            Assert.IsNotNull(usedMaster);
            Assert.IsTrue(usedMaster.Purchases.Any(p => p.Amount.Equals(100m) && p.Description.Equals(groceryPurchase.Description)));
            Assert.IsTrue(usedMaster.Purchases.Any(p => p.Amount.Equals(20m) && p.Description.Equals(wardrobePurchase.Description)));

            Assert.IsTrue(usedMaster.Purchases.Count == 2);

            Assert.IsNotNull(usedNubank);
            Assert.IsTrue(usedNubank.Purchases.Any(p => p.Amount.Equals(300m) && p.Description.Equals(wardrobePurchase.Description)));
            Assert.IsTrue(usedNubank.Purchases.Count == 1);

            Assert.IsNotNull(usedVisa);
            Assert.IsTrue(usedVisa.Purchases.Any(p => p.Amount.Equals(279.99m) && p.Description.Equals(wardrobePurchase.Description)));
            Assert.IsTrue(usedVisa.Purchases.Any(p => p.Amount.Equals(60.0m) && p.Description.Equals(concertPurchase.Description)));
            Assert.IsTrue(usedVisa.Purchases.Count == 2);

            Assert.IsTrue(_purchase.Wallet.GetMaximmumLimit().Equals(160.01m));
        }
    }
}