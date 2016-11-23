using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PdfFillerClient.UnitTests.APITests
{
    [TestClass]
    public class AuthTests : BaseUnitTest
    {
        private PdfFillerApiClient _client;

        [ClassInitialize]
        public static void SetupFixture(TestContext context) { }

        [TestInitialize]
        public void InitTest()
        {
            _client = GetClientInstance(AuthType.ApiKey);
            Assert.IsTrue(_client.IsAuthenticated(), "User should be authenticated!");
        }

        [TestMethod]
        public void TestAuth()
        {
            Assert.IsTrue(_client.IsAuthenticated(), "User should be authenticated!");
        }
    }
}
