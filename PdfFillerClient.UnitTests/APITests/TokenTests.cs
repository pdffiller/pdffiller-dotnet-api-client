using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfFillerClient.DTO.Token;

namespace PdfFillerClient.UnitTests.APITests
{
    [TestClass]
    public class TokenTests : BaseUnitTest
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
        public void TokenCreateTest()
        {
            var tokenObj = new TokenCreateRequest();
            var tokenObj1 = new { key = "test", name = "test_name" };
            var tokenObj2 = new { key = "asdf" };
            tokenObj.data.Add(tokenObj1);
            tokenObj.data.Add(tokenObj2);

            TokenCreateResponse createdToken = _client.Token.CreateToken(tokenObj);
            Assert.IsNotNull(createdToken, "Token create response shouldn't be null!");
            Assert.IsInstanceOfType(createdToken, typeof(TokenCreateResponse), "Token create object is not of appropriate type!");
            Assert.IsTrue(createdToken.data.Count == 2, "Token should have data with 2 objects!");

            // TODO: how to check fields of anonymous object from response?
            /*var tokenObj1Type = tokenObj1.GetType();
            var tokenObj2Type = tokenObj2.GetType();
            var Obj1Key = (string)tokenObj1Type.GetProperty("key").GetValue(tokenObj1, null);
            Assert.AreEqual(Obj1Key, tokenObj1.key, "Keys are different!");
            var ObjName = (string)tokenObj1Type.GetProperty("name").GetValue(tokenObj1, null);
            Assert.AreEqual(ObjName, tokenObj1.name, "Names are different!");*/
            TokenDeleteResponse deleteResponse = _client.Token.DeleteToken(createdToken.id);
            Assert.IsNotNull(deleteResponse, "Token delete response shouldn't be null!");
        }

        [TestMethod]
        public void TokensListTest()
        {
            TokensListResponse tokens = _client.Token.GetTokensList();

            Assert.IsNotNull(tokens.items, "There should be at least 1 token!");
            Assert.IsInstanceOfType(tokens, typeof(TokensListResponse), "Token list object is not of appropriate type!");
        }

        [TestMethod]
        public void TokenInfoTest()
        {
            var tokenObj = new TokenCreateRequest();
            tokenObj.data.Add(new { test = "info" });
            var createdToken = _client.Token.CreateToken(tokenObj);
            Assert.IsNotNull(createdToken, "Token create response shouldn't be null!");
            Assert.IsTrue(createdToken.data.Count == 1, "Token should have data with 1 object!");

            TokenCreateResponse tokenInfo = _client.Token.GetTokenInfo(createdToken.id);
            Assert.IsNotNull(tokenInfo, "Token info response shouldn't be null!");
            Assert.AreEqual(createdToken.id, tokenInfo.id, "Id's are not the same!");
            Assert.AreEqual(createdToken.hash, tokenInfo.hash, "Hashes are not the same!");
            Assert.AreEqual(createdToken.data.Count, tokenInfo.data.Count, "Token data count are different!");

            TokenDeleteResponse deleteResponse = _client.Token.DeleteToken(createdToken.id);
            Assert.IsNotNull(deleteResponse, "Token delete response shouldn't be null!");
        }

        [TestMethod]
        public void TokenUpdateTest()
        {
            var tokenObj = new TokenCreateRequest();
            tokenObj.data.Add(new { test1 = "update" });
            var createdToken = _client.Token.CreateToken(tokenObj);
            Assert.IsNotNull(createdToken, "Token create response shouldn't be null!");
            Assert.IsTrue(createdToken.data.Count == 1, "Token should have data with 1 object!");

            tokenObj.data.Add(new { test2 = "update" });
            TokenCreateRequest tokenForUpdate = new TokenCreateRequest()
            {
                data = tokenObj.data
            };

            TokenCreateResponse updatedToken = _client.Token.UpdateToken(createdToken.id, tokenForUpdate);
            Assert.IsNotNull(updatedToken, "Token update response shouldn't be null!");
            Assert.AreEqual(createdToken.id, updatedToken.id, "Id's are not the same!");
            Assert.AreEqual(createdToken.hash, updatedToken.hash, "Hashes are not the same!");
            Assert.IsTrue(updatedToken.data.Count == tokenObj.data.Count, "Token data count are different!");

            TokenDeleteResponse deleteResponse = _client.Token.DeleteToken(createdToken.id);
            Assert.IsNotNull(deleteResponse, "Token delete response shouldn't be null!");
        }

        [TestMethod]
        public void TokenDeleteTest()
        {
            var tokenObj = new TokenCreateRequest();
            tokenObj.data.Add(new { test = "delete" });

            var createdToken = _client.Token.CreateToken(tokenObj);
            Assert.IsNotNull(createdToken, "Token create response shouldn't be null!");

            TokenDeleteResponse deleteResponse = _client.Token.DeleteToken(createdToken.id);
            Assert.IsNotNull(deleteResponse, "Token delete response shouldn't be null!");
            Assert.IsInstanceOfType(deleteResponse, typeof(TokenDeleteResponse), "Token delete info object is not of appropriate type!");
            Assert.AreEqual($"Row {createdToken.id} was deleted successfully.", deleteResponse.message, "Message is wrong!");
        }
    }
}
