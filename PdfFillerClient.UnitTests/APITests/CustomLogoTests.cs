using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfFillerClient.DTO.CustomLogo;

namespace PdfFillerClient.UnitTests.APITests
{
    [TestClass]
    public class CustomLogoTests : BaseUnitTest
    {
        private PdfFillerApiClient _client;
        private CustomLogoCreateResponse _createdCustomLogo;

        [ClassInitialize]
        public static void SetupFixture(TestContext context) { }

        [TestInitialize]
        public void InitTest()
        {
            _client = GetClientInstance(AuthType.ApiKey);
            Assert.IsTrue(_client.IsAuthenticated(), "User should be authenticated!");

            string filePath = @"Assets\TestLogo.jpg";
            _createdCustomLogo = _client.CustomLogo.UploadCustomLogo(filePath);
            Assert.IsNotNull(_createdCustomLogo, "Custom logo upload response shouldn't be null!");
        }

        [TestMethod]
        public void CustomLogoCreateByFileTest()
        {
            Assert.IsInstanceOfType(_createdCustomLogo, typeof(CustomLogoCreateResponse), "Uploaded custom logo response object is not of appropriate type!");
            Assert.IsTrue(_createdCustomLogo.id > 0, "Id shouldn't be null!");

            CustomLogoDeleteResponse deleteResponse = _client.CustomLogo.DeleteCustomLogo(_createdCustomLogo.id);
            Assert.IsNotNull(deleteResponse, "Delete object response shouldn't be null!");
        }

        [TestMethod]
        public void CustomLogoCreateByURLTest()
        {
            CustomLogoCreateRequest urlLogo = new CustomLogoCreateRequest()
            {
                file = "http://orig12.deviantart.net/250e/f/2012/327/5/4/5405ca7130582d6cbda8cbe0bb0fc9a8-d5lwex1.gif"
            };

            CustomLogoCreateResponse createdUrlCustomLogo = _client.CustomLogo.UploadCustomLogo(urlLogo);
            Assert.IsInstanceOfType(_createdCustomLogo, typeof(CustomLogoCreateResponse), "Uploaded custom logo response object is not of appropriate type!");
            Assert.IsTrue(_createdCustomLogo.id > 0, "Id shouldn't be null!");

            CustomLogoDeleteResponse deleteResponse = _client.CustomLogo.DeleteCustomLogo(_createdCustomLogo.id);
            Assert.IsNotNull(deleteResponse, "Delete object response shouldn't be null!");
        }

        [TestMethod]
        public void CustomLogosListTest()
        {
            CustomLogoListResponse customLogosList = _client.CustomLogo.GetCustomLogosList();
            Assert.IsNotNull(customLogosList, "Custom logos list response shouldn't be null!");
            Assert.IsInstanceOfType(customLogosList, typeof(CustomLogoListResponse), "Custom logos list object is not of appropriate type!");
            Assert.IsNotNull(customLogosList.items, "Items shouldn't be null!");
            Assert.IsNotNull(customLogosList.total, "Total shouldn't be null!");
            Assert.IsTrue(customLogosList.items.Count > 0, "Items shouldn't be empty!");
            Assert.IsNotNull(customLogosList.items.Find(x => x.id == _createdCustomLogo.id), "Can't find uploaded custom logo!");
            Assert.IsInstanceOfType(customLogosList.items.Find(x => x.id == _createdCustomLogo.id), typeof(CustomLogoCreateResponse), "Uploaded custom logo in list is not of appropriate type!");

            CustomLogoDeleteResponse deleteResponse = _client.CustomLogo.DeleteCustomLogo(_createdCustomLogo.id);
            Assert.IsNotNull(deleteResponse, "Delete object response shouldn't be null!");
        }

        [TestMethod]
        public void CustomLogoInfoTest()
        {
            CustomLogoCreateResponse customLogoInfo = _client.CustomLogo.GetCustomLogoInfo(_createdCustomLogo.id);
            Assert.IsNotNull(customLogoInfo, "Custom logo object response shouldn't be null!");
            Assert.IsInstanceOfType(customLogoInfo, typeof(CustomLogoCreateResponse), "Custom logo info object is not of appropriate type!");
            Assert.AreEqual(customLogoInfo.id, customLogoInfo.id, "Id's not the same!");

            CustomLogoDeleteResponse deleteResponse = _client.CustomLogo.DeleteCustomLogo(_createdCustomLogo.id);
            Assert.IsNotNull(deleteResponse, "Delete object response shouldn't be null!");
        }

        [TestMethod]
        public void CustomLogoDeleteTest()
        {
            CustomLogoDeleteResponse deleteResponse = _client.CustomLogo.DeleteCustomLogo(_createdCustomLogo.id);
            Assert.IsNotNull(deleteResponse, "Delete object response shouldn't be null!");
            Assert.IsInstanceOfType(deleteResponse, typeof(CustomLogoDeleteResponse), "Created document object is not of appropriate type!");
            Assert.AreEqual($"Row {_createdCustomLogo.id} was deleted successfully.", deleteResponse.message);
        }
    }
}
