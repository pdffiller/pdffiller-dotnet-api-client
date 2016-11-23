using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfFillerClient.DTO.SignatureRequest;
using PdfFillerClient.DTO.Messages;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PdfFillerClient.UnitTests.APITests
{
    [TestClass]
    public class SignatureRequestTests : BaseUnitTest
    {
        private PdfFillerApiClient _client;
        private SignatureRequestCreateResponse _createdReqResponse;

        [ClassInitialize]
        public static void SetupFixture(TestContext context) { }

        [TestInitialize]
        public void InitTest()
        {
            _client = GetClientInstance(AuthType.ApiKey);
            Assert.IsTrue(_client.IsAuthenticated(), "User should be authenticated!");

            var documentsList = _client.Document.GetDocumentsList();
            Assert.IsNotNull(documentsList, "Documents list response shouldn't be null!");
            Assert.IsTrue(documentsList.items.Count > 0, "Document list shouldn't be empty!");
            var firstDoc = documentsList.items.FirstOrDefault();

            var recepients = new List<RecepientRequest>
            {
                new RecepientRequest
                {
                    email = TestEmail,
                    name = "Arty",
                    order = 1,
                    message_subject = "messageTestSubject",
                    message_text = "messageTestText",
                    access = "signature",
                    require_photo = false
                }
            };

            var newRequest = new SignatureRequestCreateRequest()
            {
                document_id = firstDoc.id,
                method = "sendtoeach",
                security_pin = "standard",
                recipients = recepients
            };

            _createdReqResponse = _client.SignatureRequest.CreateSignatureRequest(newRequest);
            Assert.IsNotNull(_createdReqResponse, "Create object response shouldn't be null!");
        }

        [TestMethod]
        public void SignatureRequestCreateTest()
        {
            Assert.IsNotNull(_createdReqResponse, "Create object response shouldn't be null!");
            Assert.IsInstanceOfType(_createdReqResponse, typeof(SignatureRequestCreateResponse), "Created signature request object is not of appropriate type!");
            Assert.IsTrue(_createdReqResponse.items.Count > 0, "Object count shouldn't be empty!");

            var firstSigReq = _createdReqResponse.items.FirstOrDefault();
            Assert.IsNotNull(firstSigReq, "First object shouldn't be null!");
            Assert.IsTrue(firstSigReq.id > 0, "Id can't be 0!");
            Assert.IsTrue(firstSigReq.document_id > 0, "Document id can't be 0!");
            Assert.IsTrue(firstSigReq.date_created > 0, "Created date timestamp can't be 0!");

            SignatureRequestDeleteResponse deleteResponse = _client.SignatureRequest.DeleteSignatureRequest(firstSigReq.id);
            Assert.IsNotNull(deleteResponse, "Delete response object shouldn't be null!");
        }

        [TestMethod]
        public void SignatureRequestsListTest()
        {
            SignatureRequestListResponse signaturesList = _client.SignatureRequest.GetSignatureRequestsList();
            Assert.IsNotNull(signaturesList, "Signature requests list object response shouldn't be null!");
            Assert.IsInstanceOfType(signaturesList, typeof(SignatureRequestListResponse), "Created signature request object is not of appropriate type!");
            Assert.IsNotNull(signaturesList.items, "Items shouldn't be null!");
            Assert.IsNotNull(signaturesList.total, "Total shouldn't be null!");

            var firstSigReq = _createdReqResponse.items.FirstOrDefault();
            SignatureRequestDeleteResponse deleteResponse = _client.SignatureRequest.DeleteSignatureRequest(firstSigReq.id);
            Assert.IsNotNull(deleteResponse, "Delete response object shouldn't be null!");
        }

        [TestMethod]
        public void SignatureRequestInfoTest()
        {
            SignatureResponse signatureResponse = _client.SignatureRequest.GetSignatureRequestInfo(_createdReqResponse.items.FirstOrDefault().id);
            Assert.IsNotNull(signatureResponse, "Signature object shouldn't be null!");
            Assert.IsTrue(signatureResponse.id > 0, "Id can't be 0!");
            Assert.IsTrue(signatureResponse.document_id > 0, "Document id can't be 0!");
            Assert.IsTrue(signatureResponse.date_created > 0, "Created date timestamp can't be 0!");

            SignatureRequestDeleteResponse deleteResponse = _client.SignatureRequest.DeleteSignatureRequest(signatureResponse.id);
            Assert.IsNotNull(deleteResponse, "Delete response object shouldn't be null!");
        }

        [TestMethod]
        public void SignatureRequestDeleteTest()
        {
            var firstSigReq = _createdReqResponse.items.FirstOrDefault();
            Assert.IsNotNull(firstSigReq, "First object shouldn't be null!");
            Assert.IsTrue(firstSigReq.id > 0, "Id can't be 0!");

            SignatureRequestDeleteResponse deleteResponse = _client.SignatureRequest.DeleteSignatureRequest(firstSigReq.id);
            Assert.IsNotNull(deleteResponse, "Delete response object shouldn't be null!");
            Assert.IsInstanceOfType(deleteResponse, typeof(SignatureRequestDeleteResponse), "Delete response object is not of appropriate type!");
            Assert.AreEqual($"Row {firstSigReq.id} was deleted successfully.", deleteResponse.message);
        }

        // If signature request will be not signed, test will fail. There is no way to sign document through PdfFiller API.
        [TestMethod]
        [Ignore]
        public void DownloadSignatureRequestCertificateTest()
        {
            var dir = Directory.GetParent(Assembly.GetExecutingAssembly().Location);
            var firstSigReq = _createdReqResponse.items.FirstOrDefault();

            byte[] fileBytes = _client.SignatureRequest.DownloadSignatureRequestCertificate(firstSigReq.id);
            Assert.IsNotNull(fileBytes, "Downloaded data shouldn't be null!");
            File.WriteAllBytes(dir + @"\DownloadSignatureRequestCertificateTest.pdf", fileBytes);

            SignatureRequestDeleteResponse deleteResponse = _client.SignatureRequest.DeleteSignatureRequest(firstSigReq.id);
            Assert.IsNotNull(deleteResponse, "Delete response object shouldn't be null!");
        }

        // If signature request will be not signed, test will fail. There is no way to sign document through PdfFiller API.
        [TestMethod]
        [Ignore]
        public void DownloadSignatureRequestDocumentTest()
        {
            var dir = Directory.GetParent(Assembly.GetExecutingAssembly().Location);
            var firstSigReq = _createdReqResponse.items.FirstOrDefault();

            byte[] fileBytes = _client.SignatureRequest.DownloadSignatureRequestDocument(firstSigReq.id);
            Assert.IsNotNull(fileBytes, "Downloaded data shouldn't be null!");
            File.WriteAllBytes(dir + @"\DownloadSignatureRequestDocumentTest.pdf", fileBytes);

            SignatureRequestDeleteResponse deleteResponse = _client.SignatureRequest.DeleteSignatureRequest(firstSigReq.id);
            Assert.IsNotNull(deleteResponse, "Delete response object shouldn't be null!");
        }

        [TestMethod]
        public void GetSignatureRequestRecipientTest()
        {
            var firstSigReq = _createdReqResponse.items.FirstOrDefault();
            Assert.IsNotNull(firstSigReq, "First object shouldn't be null!");
            Assert.IsTrue(firstSigReq.id > 0, "Id can't be 0!");
            Assert.IsNotNull(firstSigReq.recipients, "Recipients shouldn't be null!");
            Assert.IsTrue(firstSigReq.recipients.Count > 0, "Recipients count shouldn't be null!");
            var firstRecipient = firstSigReq.recipients.FirstOrDefault();

            RecepientResponse recipientResponse = _client.SignatureRequest.GetSignatureRequestRecipient(firstSigReq.id, firstRecipient.id);
            Assert.IsNotNull(recipientResponse, "Recipient object response shouldn't be null!");
            Assert.IsInstanceOfType(recipientResponse, typeof(RecepientResponse), "Created signature request object is not of appropriate type!");
            Assert.AreEqual(firstRecipient.user_id, recipientResponse.user_id);

            SignatureRequestDeleteResponse deleteResponse = _client.SignatureRequest.DeleteSignatureRequest(firstSigReq.id);
            Assert.IsNotNull(deleteResponse, "Delete response object shouldn't be null!");
        }

        [TestMethod]
        public void RemindSignatureRequestRecipient()
        {
            var firstSigReq = _createdReqResponse.items.FirstOrDefault();
            Assert.IsNotNull(firstSigReq, "First object shouldn't be null!");
            Assert.IsTrue(firstSigReq.id > 0, "Id can't be 0!");
            Assert.IsNotNull(firstSigReq.recipients, "Recipients shouldn't be null!");
            Assert.IsTrue(firstSigReq.recipients.Count > 0, "Recipients count shouldn't be null!");
            var firstRecipient = firstSigReq.recipients.FirstOrDefault();

            ResponseMessage remindResponse = _client.SignatureRequest.RemindSignatureRequestRecipient(firstSigReq.id, firstRecipient.id);
            Assert.IsNotNull(remindResponse, "Recipient object response shouldn't be null!");
            Assert.IsInstanceOfType(remindResponse, typeof(ResponseMessage), "Created signature request object is not of appropriate type!");
            Assert.AreEqual("An email reminder has been sent to this recipient.", remindResponse.message, "Wrong response message!");

            SignatureRequestDeleteResponse deleteResponse = _client.SignatureRequest.DeleteSignatureRequest(firstSigReq.id);
            Assert.IsNotNull(deleteResponse, "Delete response object shouldn't be null!");
        }
    }
}
