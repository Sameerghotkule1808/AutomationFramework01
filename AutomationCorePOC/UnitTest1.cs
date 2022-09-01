using AutomationPOC;

namespace AutomationCorePOC
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var x = DataSources.GetLogin_Credentials_TD();
            Assert.Pass();
        }
    }
}