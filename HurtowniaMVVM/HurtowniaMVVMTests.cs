using NUnit.Framework;
using HurtowniaMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HurtowniaMVVM.ViewModel;

namespace Hurtownia.Tests
{
    [TestFixture]
    public class HurtowniaMVVMTests
    {
        DodajDostawceViewModel ddvm = new DodajDostawceViewModel();

        [Test]
        public void EmailIsNullTest()
        {
            bool result = ddvm.IsValidEmailAddress(null);
            Assert.AreEqual(false, result);
        }

        [TestCase]
        public void EmailIsEmptyStringTest()
        {
            bool result = ddvm.IsValidEmailAddress("");
            Assert.AreEqual(false, result);
        }

        [TestCase]
        public void EmailIsProper()
        {
            string[] adresyEmail = { "anna.abacka@gmail.com", "stefan@stefanowicz.ue", "ababacka-cabacka@gmail.com"};
            foreach (var adres in adresyEmail)
            {
                bool result = ddvm.IsValidEmailAddress(adres);
                Assert.AreEqual(true, result);
            }
        }
    }
}
