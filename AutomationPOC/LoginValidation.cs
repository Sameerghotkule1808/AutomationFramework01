using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace AutomationScript
{
    public class LoginValidation
    {

        [TestFixture]
        public class Poc
        {
            IWebDriver _driver;

            [SetUp]
            public void Initiazation()
            {
                _driver = new ChromeDriver();
                _driver.Manage().Window.Maximize();
                Thread.Sleep(2000);
            }



            [Test]
            public void DentaSource_Validlogin()
            {

                //Navigate to LoginPage
                _driver.Navigate().GoToUrl("https://www.qadentsource.com/User/Welcome/1");

                IWebElement loginButton = _driver.FindElement(By.XPath("//*[@id='collapsibleNavbar']/ul/li[5]/a"));
                loginButton.Click();
                Thread.Sleep(3000);

                IWebElement usernameTxt = _driver.FindElement(By.XPath("/html/body/app/div[3]/div[1]/div[1]/div/div[2]/form/div[1]/div[1]/input"));
                usernameTxt.SendKeys("samir.mg@intechhub.com");
                Thread.Sleep(3000);


                IWebElement passwordTxt = _driver.FindElement(By.XPath("//*[@id='txtLoginPassword']"));
                passwordTxt.SendKeys("Samir@123");
                Thread.Sleep(3000);

                IWebElement loginClick = _driver.FindElement(By.XPath("/html/body/app/div[3]/div[1]/div[1]/div/div[2]/form/div[3]/button"));
                loginClick.Click();
                Thread.Sleep(3000);

                IWebElement logoutClick = _driver.FindElement(By.XPath("/html/body/app/div[2]/div/div[2]/div/span/span[2]/button"));
                logoutClick.Click();
                Thread.Sleep(3000);

                IWebElement verificaionText = _driver.FindElement(By.XPath("/html/body/app/div[3]/div/div[1]/div[1]/div[1]"));

                Assert.That(verificaionText.Displayed, Is.True);



            }

            [TearDown]
            public void Close()
            {
                Thread.Sleep(15000);
                _driver.Quit();
            }
        }
    }
}
