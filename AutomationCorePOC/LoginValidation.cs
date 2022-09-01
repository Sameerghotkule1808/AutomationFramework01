using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace AutomationPOC
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



            //[Test]
            public void DentaSource_InValidlogin()
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
                passwordTxt.SendKeys("Samir@1234");
                Thread.Sleep(3000);

                IWebElement loginClick = _driver.FindElement(By.XPath("/html/body/app/div[3]/div[1]/div[1]/div/div[2]/form/div[3]/button"));
                loginClick.Click();
                Thread.Sleep(3000);

             

                IWebElement verificaionText = _driver.FindElement(By.XPath("/html/body/app/div[3]/div[1]/div[1]/div/div[2]/form/div[1]/div[4]/span"));

                Assert.That(verificaionText.Displayed, Is.True);
            }

            [Test, TestCaseSource(typeof(DataSources), nameof(DataSources.GetLogin_Credentials_TD))]
            public void DentaSource_Validlogin(Login_Credentials model)
             {
                //Navigate to LoginPage
                _driver.Navigate().GoToUrl("https://www.qadentsource.com/User/Welcome/1");

                    IWebElement loginButton1 = _driver.FindElement(By.XPath("//*[@id='collapsibleNavbar']/ul/li[5]/a"));
                    loginButton1.Click();
                    Thread.Sleep(3000);

                    IWebElement usernameTxt1 = _driver.FindElement(By.XPath("/html/body/app/div[3]/div[1]/div[1]/div/div[2]/form/div[1]/div[1]/input"));
                    usernameTxt1.SendKeys(model.UserName);
                    Thread.Sleep(3000);


                    IWebElement passwordTxt1 = _driver.FindElement(By.XPath("//*[@id='txtLoginPassword']"));
                    passwordTxt1.SendKeys(model.Password);
                    Thread.Sleep(3000);

                    IWebElement loginClick1 = _driver.FindElement(By.XPath("/html/body/app/div[3]/div[1]/div[1]/div/div[2]/form/div[3]/button"));
                    loginClick1.Click();
                    Thread.Sleep(3000);

                    IWebElement logoutClick1 = _driver.FindElement(By.XPath("/html/body/app/div[2]/div/div[2]/div/span/span[2]/button"));
                    logoutClick1.Click();
                    Thread.Sleep(3000);

                    IWebElement verificaionText1 = _driver.FindElement(By.XPath("/html/body/app/div[3]/div/div[1]/div[1]/div[1]"));

                    Assert.That(verificaionText1.Displayed, Is.True);



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
