using CrusaderFramework.PageObjects;
using CrusaderFramework.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrusaderFramework.Tests
{
    public class Testing : BaseClass
    {
        [Test]
        public void InvalidLogTest()
        {
            // instanc-а на LoginPage класата која се доделува на променливата loginPage и како параметар ја повикуваме GetDriver методата од BasseClass за да доделиме instanc-а на драјверот
            LoginPage loginPage = new LoginPage(GetDriver());

            // проверка дали input полињата постојат на страницата
            IWebElement usernameField = loginPage.GetUsername();
            IWebElement passwordField = loginPage.GetPassword();
            Assert.That(usernameField, Is.Not.Null, "There should be username input field"); // потврда дека променливата usernameField не е 0. Во спротивно username field недостасува на страната.
            Assert.That(passwordField, Is.Not.Null, "There should be password input field"); // потврда дека променливата passwordField не е 0. Во спротивно password field недостасува на страната.

            // проверка на логирање со invalid username и password 
            loginPage.Login("test", "test");

            // проверка дали по внесени invalid username се добива очекувана error message
            loginPage.AssertErrorMessage("Incorrect email/username or password", Driver);
        }
    }
}
