using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrusaderFramework.PageObjects
{
    public class LoginPage
    {
        [FindsBy(How = How.Id , Using = "input-204")] // локатор за елемент username
        private IWebElement Username; // декларирање на променлива за инстанцата Username од типотIWebElement (класа на Selenium Web Element, кој претставува HTML елемент (тело, табела))

        [FindsBy(How = How.Id , Using = "input-207")] // локатор за елемент password
        private IWebElement Password; // декларирање на променлива за инстанцата Password од типотIWebElement

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")] // локатор за елемент login button
        private IWebElement LoginBtn; // декларирање на променлива за инстанцата LoginBtn од типотIWebElement

        // до варијаблите пристапуваме преку методи


        private IWebDriver Driver; // декларирање на променлива за инстанцата Dirver од типотIWebElement (класа на Selenium Web Elementot (го претставува веб прелистувачот)

        public LoginPage(IWebDriver driver) // дефинирање на конструктор за класата Login Page
        {
            this.Driver = driver; // доделување на вредност
            PageFactory.InitElements(driver, this); // доделување на вредноста на параметарот
        }

        // reusable method for log in - оваа метода може да ја користи било кој тест што проверува логирање
        public void Login(string username, string password)
        {
            Username.SendKeys(username); // елемент со акција за впишување на текст во полето Username
            Password.SendKeys(password); // елемент со акција за впишување на текст во полето Password
            LoginBtn.Click(); // елемент со акција за кликање на login button
        }

        // метода со која го земаме Username
        public IWebElement GetUsername()
        {
            return Username;
        }

        // метода со која го земаме Password
        public IWebElement GetPassword()
        {
            return Password;
        }

        // метода со која доаѓаме до Login button
        public IWebElement GetLoginBtn()
        {
            return LoginBtn;
        }

        // метода со која проверуваме error message по неуспешен login
        public void AssertErrorMessage(string expectedMessage, IWebDriver driver)
        {
            IReadOnlyCollection<IWebElement> errorMessages = driver.FindElements(By.XPath("//div[@class='red--text text-center col col-12']")); // наоѓање на error message
            Assert.That(errorMessages, Is.Not.Null, "Expected to find error messages"); // се потврдува дека се добива error message
            Assert.That(errorMessages.Count, Is.EqualTo(1), "Expected exactly one error message"); // се потврдува дека се добива точно 1 error message

            string errorMessage = errorMessages.First().Text; // го враќа текстот на пораката за грешка
            Assert.That(errorMessage, Is.EqualTo(expectedMessage)); // потврда дали добиената порака за error message се совпаѓа со очекуваната порака. Ако не се совпадне театот ќе падне.
        }
    }
}
