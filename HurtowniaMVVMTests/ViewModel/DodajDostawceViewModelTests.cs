using Microsoft.VisualStudio.TestTools.UnitTesting;
using HurtowniaMVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurtowniaMVVM.ViewModel.Tests
{
    [TestClass()]
    public class DodajDostawceViewModelTests
    {
        DodajDostawceViewModel ddvm = new DodajDostawceViewModel();

        [TestMethod()]
        public void EmailIsEmptyStringTest()
        {
            bool result = ddvm.IsValidEmailAddress("");
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void EmailIsProper()
        {
            string[] adresyEmail = { "anna.abacka@gmail.com", "stefan@stefanowicz.ue", "ababacka-cabacka@gmail.com", "ysiu102.ysiaczkowa@ysiu.ysi" , "j@proseware.com9"};
            foreach (var adres in adresyEmail)
            {
                bool result = ddvm.IsValidEmailAddress(adres);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod()]
        public void EmailIsWrongFormat()
        {
            string[] adresyEmail = { "anna@abacka@gmail.com", "stefan@stefanowicz.ueueue@ue.ue", "ababacka-cabacka.wabacka.mamacka@bu@gmail.com", "j.@server1.proseware.com", "j..s@proseware.com", "js*@proseware.com", "js@proseware..com" };
            foreach (var adres in adresyEmail)
            {
                bool result = ddvm.IsValidEmailAddress(adres);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod()]
        public void NumerTelefonuIsEmpty()
        {
            bool result = ddvm.IsValidTelefonNumer("");
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void NumerTelefonuWithSpaces()
        {
            bool result = ddvm.IsValidTelefonNumer("123 456 789");
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void NumerTelefonuTooShort()
        {
            bool result = ddvm.IsValidTelefonNumer("12345678");
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void NumerTelefonuTooLong()
        {
            bool result = ddvm.IsValidTelefonNumer("+481234567890");
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void NumerTelefonuWithLetters()
        {
            bool result = ddvm.IsValidTelefonNumer("+48ab4567890");
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void NumerTelefonuWithChars()
        {
            bool result = ddvm.IsValidTelefonNumer("+4814@567890");
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void NumerTelefonuProper()
        {
            string[] numery = { "+48123456789", "123321123", "098098000", "0774634285", "898767123", "+00989767545" };
            foreach (var nr in numery)
            {
                bool result = ddvm.IsValidTelefonNumer(nr);
                Assert.AreEqual(true, result);
            }
        }
    }
} 