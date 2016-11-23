using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfFillerClient.DTO.Application;

namespace PdfFillerClient.UnitTests.APITests
{
    [TestClass]
    public class ApplicationTests : BaseUnitTest
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
        public void ApplicationsListTest()
        {
            ApplicationsListResponse apps = _client.Application.GetAppList();

            Assert.IsNotNull(apps.items, "There should be at least 1 application!");
            Assert.IsInstanceOfType(apps.items[0], typeof(ApplicationCreateResponse), "Application object is not of appropriate type!");
            Assert.IsTrue(apps.total > 0, "There cant be less than 0!");
        }

        [TestMethod]
        public void ApplicationCreateTest()
        {
            var newApp = new ApplicationCreateRequest()
            {
                name = "Test123asd",
                description = "Description test",
                domain = "http://google.com"
            };
            ApplicationCreateResponse createdApp = _client.Application.CreateApp(newApp);

            Assert.IsNotNull(createdApp, "Returned application object shouldn't be null!");
            Assert.IsInstanceOfType(createdApp, typeof(ApplicationCreateResponse), "Application object is not of appropriate type!");
            Assert.AreEqual(newApp.name, createdApp.name, "Names not the same!");
            Assert.AreEqual(newApp.description, createdApp.description, "Descriptions not the same!");
            Assert.AreEqual(newApp.domain, createdApp.redirect_uri, "Domains not the same!");
            Assert.AreEqual("google.com", createdApp.domain, "Domains not the same!");

            ApplicationDeleteResponse deleteResponse = _client.Application.DeleteApp(createdApp.id);
            Assert.IsNotNull(deleteResponse, "Returned delete object shouldn't be null!");
        }

        [TestMethod]
        public void ApplicationInfoTest()
        {
            var newApp = new ApplicationCreateRequest()
            {
                name = "Test321asd",
                description = "Test info app test",
                domain = "http://google.com"
            };
            ApplicationCreateResponse createdApp = _client.Application.CreateApp(newApp);
            ApplicationCreateResponse appInfo = _client.Application.GetAppInfo(createdApp.id);

            Assert.IsNotNull(appInfo, "Returned application object shouldn't be null!");
            Assert.IsInstanceOfType(appInfo, typeof(ApplicationCreateResponse), "Application object is not of appropriate type!");
            Assert.AreEqual(newApp.name, appInfo.name, "Names not the same!");
            Assert.AreEqual(newApp.description, appInfo.description, "Descriptions not the same!");
            Assert.AreEqual(newApp.domain, appInfo.redirect_uri, "Domains not the same!");
            Assert.AreEqual("google.com", appInfo.domain, "Domains not the same!");

            ApplicationDeleteResponse deleteResponse = _client.Application.DeleteApp(createdApp.id);
            Assert.IsNotNull(deleteResponse, "Returned delete object shouldn't be null!");
        }

        [TestMethod]
        public void ApplicationDeleteTest()
        {
            var newApp = new ApplicationCreateRequest()
            {
                name = "TestAsd312",
                description = "Test delete app test",
                domain = "http://google.com"
            };
            ApplicationCreateResponse createdApp = _client.Application.CreateApp(newApp);

            ApplicationsListResponse appsAfterCreate = _client.Application.GetAppList();
            var newAppInList = appsAfterCreate.items.Find(x => x.id == createdApp.id);
            Assert.AreEqual(newApp.name, newAppInList.name, "Names not the same!");
            Assert.AreEqual(newApp.description, newAppInList.description, "Descriptions not the same!");
            Assert.AreEqual(newApp.domain, newAppInList.redirect_uri, "Domains not the same!");
            Assert.AreEqual("google.com", newAppInList.domain, "Domains not the same!");

            ApplicationDeleteResponse deleteResponse = _client.Application.DeleteApp(createdApp.id);
            Assert.IsNotNull(deleteResponse, "Returned delete object shouldn't be null!");
            Assert.IsInstanceOfType(deleteResponse, typeof(ApplicationDeleteResponse), "Delete object is not of appropriate type!");
            Assert.AreEqual($"Row {createdApp.id} was deleted successfully.", deleteResponse.message);

            ApplicationsListResponse appsAfterDelete = _client.Application.GetAppList();
            Assert.IsFalse(appsAfterDelete.items.Exists(x => x.id == createdApp.id), "There should be not test's created app!");
        }

        [TestMethod]
        public void ApplicationUpdateTest()
        {
            var newApp = new ApplicationCreateRequest()
            {
                name = "Test312Asd",
                description = "Test update app test",
                domain = "http://google.com"
            };

            var createdApp = _client.Application.CreateApp(newApp);

            var appUpdate = new ApplicationCreateRequest()
            {
                name = "Name updated",
                description = "Description updated",
                domain = "http://yahoo.com"
            };

            ApplicationCreateResponse updatedApp = _client.Application.UpdateApp(createdApp.id, appUpdate);
            Assert.IsNotNull(updatedApp, "Updated app object shouldn't be null!");
            Assert.IsInstanceOfType(updatedApp, typeof(ApplicationCreateResponse), "Updated app object is not of appropriate type!");
            Assert.AreEqual(createdApp.id, updatedApp.id, "Id's not the same!");
            Assert.AreEqual(appUpdate.name, updatedApp.name, "Names not the same!");
            Assert.AreEqual(appUpdate.description, updatedApp.description, "Descriptions not the same!");
            Assert.AreEqual(appUpdate.domain, updatedApp.redirect_uri, "Id's not the same!");

            ApplicationDeleteResponse deleteResponse = _client.Application.DeleteApp(createdApp.id);
            Assert.IsNotNull(deleteResponse, "Returned delete object shouldn't be null!");
        }
    }
}
