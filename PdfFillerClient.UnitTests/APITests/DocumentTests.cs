using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfFillerClient.DTO.Document;

namespace PdfFillerClient.UnitTests.APITests
{
    [TestClass]
    public class DocumentTests : BaseUnitTest
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
        public void DocumentCreateByFileTest()
        {
            string filePath = @"Assets\AdobeXMLFormsSamples.pdf";
            DocumentCreateResponse createdDocument = _client.Document.CreateDocument(filePath);
            Assert.IsNotNull(createdDocument, "Document create response shouldn't be null!");
            Assert.IsInstanceOfType(createdDocument, typeof(DocumentCreateResponse), "Created document object is not of appropriate type!");
            Assert.IsTrue(createdDocument.id > 0, "Id shouldn't be null!");

            DocumentDeleteResponse deleteResponse = _client.Document.DeleteDocument(createdDocument.id);
            Assert.IsNotNull(deleteResponse, "Delete object response shouldn't be null!");
        }

        [TestMethod]
        public void DocumentCreateByUrlTest()
        {
            DocumentCreateRequest newDoc = new DocumentCreateRequest()
            {
                file = "http://partners.adobe.com/public/developer/en/xml/AdobeXMLFormsSamples.pdf"
            };
            DocumentCreateResponse createdDocument = _client.Document.CreateDocument(newDoc);
            Assert.IsNotNull(createdDocument, "Document create response shouldn't be null!");
            Assert.IsInstanceOfType(createdDocument, typeof(DocumentCreateResponse), "Created document object is not of appropriate type!");
            Assert.IsTrue(createdDocument.id > 0, "Id can't be zero!");

            DocumentDeleteResponse deleteResponse = _client.Document.DeleteDocument(createdDocument.id);
            Assert.IsNotNull(deleteResponse, "Delete object response shouldn't be null!");
        }

        [TestMethod]
        public void DocumentsListTest()
        {
            DocumentCreateRequest newDoc = new DocumentCreateRequest()
            {
                file = "http://partners.adobe.com/public/developer/en/xml/AdobeXMLFormsSamples.pdf"
            };
            DocumentCreateResponse createdDocument = _client.Document.CreateDocument(newDoc);
            Assert.IsNotNull(createdDocument, "Document create response shouldn't be null!");

            DocumentsListResponse documentsList = _client.Document.GetDocumentsList();
            Assert.IsNotNull(documentsList, "Documents list response shouldn't be null!");
            Assert.IsInstanceOfType(documentsList, typeof(DocumentsListResponse), "Documents list object is not of appropriate type!");
            Assert.IsNotNull(documentsList.items, "Items shouldn't be null!");
            Assert.IsNotNull(documentsList.total, "Total shouldn't be null!");
            Assert.IsTrue(documentsList.items.Count > 0, "Items shouldn't be empty!");
            Assert.IsNotNull(documentsList.items.Find(x => x.id == createdDocument.id), "Can't find created document!");
            Assert.IsInstanceOfType(documentsList.items.Find(x => x.id == createdDocument.id), typeof(DocumentCreateResponse), "Created document in list is not of appropriate type!");

            DocumentDeleteResponse deleteResponse = _client.Document.DeleteDocument(createdDocument.id);
            Assert.IsNotNull(deleteResponse, "Delete object response shouldn't be null!");
        }

        [TestMethod]
        public void DocumentInfoTest()
        {
            DocumentCreateRequest newDoc = new DocumentCreateRequest()
            {
                file = "http://partners.adobe.com/public/developer/en/xml/AdobeXMLFormsSamples.pdf"
            };
            DocumentCreateResponse createdDocument = _client.Document.CreateDocument(newDoc);
            Assert.IsNotNull(createdDocument, "Document create response shouldn't be null!");

            DocumentCreateResponse documentInfo = _client.Document.GetDocumentInfo(createdDocument.id);
            Assert.IsNotNull(documentInfo, "Document object response shouldn't be null!");
            Assert.IsInstanceOfType(documentInfo, typeof(DocumentCreateResponse), "Document info object is not of appropriate type!");
            Assert.AreEqual(createdDocument.id, documentInfo.id, "Id's not the same!");

            DocumentDeleteResponse deleteResponse = _client.Document.DeleteDocument(createdDocument.id);
            Assert.IsNotNull(deleteResponse, "Delete object response shouldn't be null!");
        }

        [TestMethod]
        public void DocumentDeleteTest()
        {
            DocumentCreateRequest newDoc = new DocumentCreateRequest()
            {
                file = "http://partners.adobe.com/public/developer/en/xml/AdobeXMLFormsSamples.pdf"
            };
            DocumentCreateResponse createdDocument = _client.Document.CreateDocument(newDoc);
            Assert.IsNotNull(createdDocument, "Document create response shouldn't be null!");
            Assert.IsTrue(createdDocument.id > 0, "Id shouldn't be null!");

            DocumentDeleteResponse deleteResponse = _client.Document.DeleteDocument(createdDocument.id);
            Assert.IsNotNull(deleteResponse, "Delete object response shouldn't be null!");
            Assert.IsInstanceOfType(deleteResponse, typeof(DocumentDeleteResponse), "Created document object is not of appropriate type!");
            Assert.AreEqual($"Row {createdDocument.id} was deleted successfully.", deleteResponse.message);
        }
    }
}
