using System.Net;
using PdfFillerClient.DTO.Document;
using PdfFillerClient.Exceptions;

namespace PdfFillerClient.API
{
    /// <summary>
    /// API service for work with tokens.
    /// "/document" allows to manage PDFfiller documents. Once document is uploaded 
    /// through API, it can be used to create signature request, fill request or 
    /// perform other operations.
    /// API describring located here http://developers.pdffiller.com/#document
    /// </summary>
    public class Document
    {
        private readonly IApiClient _apiClient;
        private const string ApiPath = "/document";

        public Document(IApiClient apiClientInstancee)
        {
            _apiClient = apiClientInstancee;
        }

        /// <summary>
        /// Uploads a new document to your PDFfiller account by file.
        /// </summary>
        /// <param name="filePath">Path to file for upload.</param>
        /// <returns>Returns uploaded document info object with additional data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public DocumentCreateResponse CreateDocument(string filePath)
        {
            byte[] response = _apiClient.UploadFile(ApiPath, filePath);
            var fileInfo = _apiClient.GetResponseBody<DocumentCreateResponse>(response);
            return fileInfo;
        }

        /// <summary>
        /// Uploads a new document to your PDFfiller account by file URL or file base64 string.
        /// </summary>
        /// <param name="newFile">DocumentCreateRequest object filled with file URL or file base64 string.</param>
        /// <returns>Returns uploaded document info object.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public DocumentCreateResponse CreateDocument(DocumentCreateRequest newFile)
        {
            var response = _apiClient.Call(ApiPath, "POST", newFile);
            var fileInfo = _apiClient.GetResponseBody<DocumentCreateResponse>(response);
            return fileInfo;
        }

        /// <summary>
        /// Lists all user documents.
        /// </summary>
        /// <returns>Returns documents list with pagination data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public DocumentsListResponse GetDocumentsList()
        {
            var response = _apiClient.Call(ApiPath, "GET", null);
            var documentsList = _apiClient.GetResponseBody<DocumentsListResponse>(response);
            return documentsList;
        }

        /// <summary>
        /// Shows information about previously uploaded document.
        /// </summary>
        /// <param name="documentId">Document id.</param>
        /// <returns>Returns document object with it's data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public DocumentCreateResponse GetDocumentInfo(long documentId)
        {
            var response = _apiClient.Call(ApiPath + "/" + documentId, "GET", null);
            var documentInfo = _apiClient.GetResponseBody<DocumentCreateResponse>(response);
            return documentInfo;
        }

        /// <summary>
        /// Deletes the document from user’s account.
        /// </summary>
        /// <param name="documentId">Document id.</param>
        /// <returns>Returns delete response object with deletion info.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public DocumentDeleteResponse DeleteDocument(long documentId)
        {
            var response = _apiClient.Call(ApiPath + "/" + documentId, "DELETE", null);
            var deleteInfo = _apiClient.GetResponseBody<DocumentDeleteResponse>(response);
            return deleteInfo;
        }
    }
}
