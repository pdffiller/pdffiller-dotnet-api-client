using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfFillerClient.DTO.User;

namespace PdfFillerClient.UnitTests.APITests
{
    [TestClass]
    public class UserTests : BaseUnitTest
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
        public void UserInfoTest()
        {
            UserResponse userResponse = _client.User.GetUserInfo();
            Assert.IsNotNull(userResponse, "User data shouldn't be null!");
            Assert.IsInstanceOfType(userResponse, typeof(UserResponse), "User object is not of appropriate type!");
            Assert.AreEqual(TestEmail, userResponse.email, "User email is wrong!");
        }
    }
}
