using System.Net;
using PdfFillerClient.DTO.SignatureRequest;
using PdfFillerClient.DTO.Messages;
using PdfFillerClient.Exceptions;
using System.Collections.Generic;

namespace PdfFillerClient.API
{
    /// <summary>
    /// API service for work with signatures requests.
    /// /signature_request" feature is hosted and managed by PDFfiller.
    /// PDFfiller provides authentication, email notifications and offers additional features to control signature workflow.
    /// API describring located here http://developers.pdffiller.com/#signature-request
    /// </summary>
    public class SignatureRequest
    {
        private readonly IApiClient _apiClient;
        private const string ApiPath = "/signature_request";

        public SignatureRequest(IApiClient apiClientInstance)
        {
            _apiClient = apiClientInstance;
        }

        /// <summary>
        /// Creates a signature request.
        /// </summary>
        /// <param name="newFile">SignatureRequestCreateRequest object filled with signature request data.</param>
        /// <returns>Returns list of signature requests with new.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public SignatureRequestCreateResponse CreateSignatureRequest(SignatureRequestCreateRequest newRequest)
        {
            var response = _apiClient.Call(ApiPath, "POST", newRequest);
            var requestInfo = _apiClient.GetResponseBody<SignatureRequestCreateResponse>(response);
            return requestInfo;
        }

        /// <summary>
        /// Lists signature requests.
        /// </summary>
        /// <returns>Returns signature requests list with pagination data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public SignatureRequestListResponse GetSignatureRequestsList()
        {
            var response = _apiClient.Call(ApiPath, "GET", null);
            var requestsList = _apiClient.GetResponseBody<SignatureRequestListResponse>(response);
            return requestsList;
        }

        /// <summary>
        /// Retrieves signature request information based on the signature request ID.
        /// </summary>
        /// <param name="sigRecId">Signature request id.</param>
        /// <returns>Returns signature request object with it's data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public SignatureResponse GetSignatureRequestInfo(long sigRecId)
        {
            var response = _apiClient.Call(ApiPath + "/" + sigRecId, "GET", null);
            var signatureRequestInfo = _apiClient.GetResponseBody<SignatureResponse>(response);
            return signatureRequestInfo;
        }

        /// <summary>
        /// Downloads a signature request certificate.
        /// </summary>
        /// <param name="sigRecId">Signature request id.</param>
        /// <returns>Returns downloaded request certificate.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public byte[] DownloadSignatureRequestCertificate(long sigRecId)
        {
            byte[] response = _apiClient.DownloadFile(ApiPath + "/" + sigRecId + "/certificate");
            return response;
        }

        /// <summary>
        /// Downloads a signed document.
        /// </summary>
        /// <param name="sigRecId">Signature request id.</param>
        /// <returns>Returns downloaded signed document.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public byte[] DownloadSignatureRequestDocument(long sigRecId)
        {
            byte[] response = _apiClient.DownloadFile(ApiPath + "/" + sigRecId + "/signed_document");
            return response;
        }

        /// <summary>
        /// Retrieves information about a signature request recipient and signature status.
        /// </summary>
        /// <param name="sigRecId">Signature request id.</param>
        /// <param name="recipientId">Recepient id.</param>
        /// <returns>Returns recipient object data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public RecepientResponse GetSignatureRequestRecipient(long sigRecId, long recipientId)
        {
            var response = _apiClient.Call(ApiPath + "/" + sigRecId + "/recipient/" + recipientId, "GET", null);
            var recipientInfo = _apiClient.GetResponseBody<RecepientResponse>(response);
            return recipientInfo;
        }

        /// <summary>
        /// Reminds SendToSign recipient about the SendToSign request.
        /// </summary>
        /// <param name="sigRecId">Signature request id.</param>
        /// <param name="recipientId">Recepient id.</param>
        /// <returns>Returns recipient remind result message.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public ResponseMessage RemindSignatureRequestRecipient(long sigRecId, long recipientId)
        {
            var response = _apiClient.Call(ApiPath + "/" + sigRecId + "/recipient/" + recipientId + "/remind", "PUT", null);
            var recipientInfo = _apiClient.GetResponseBody<ResponseMessage>(response);
            return recipientInfo;
        }

        /// <summary>
        /// Cancels a signature request for the specified SendToSign ID
        /// </summary>
        /// <param name="sigRecId">Signature request id.</param>
        /// <returns>Returns delete response object with deletion info.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public SignatureRequestDeleteResponse DeleteSignatureRequest(long sigRecId)
        {
            var response = _apiClient.Call(ApiPath + "/" + sigRecId, "DELETE", null);
            var deleteInfo = _apiClient.GetResponseBody<SignatureRequestDeleteResponse>(response);
            return deleteInfo;
        }
    }
}
