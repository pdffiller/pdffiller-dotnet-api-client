using System.Net;
using PdfFillerClient.DTO.FillRequest;
using PdfFillerClient.Exceptions;
using System.Collections.Generic;

namespace PdfFillerClient.API
{
    /// <summary>
    /// API service for work with fill requests.
    /// /signature_request" allows to create a fillable document and share it by link. 
    /// Such fillable documents can be embedded to your site or accessed directly from PDFfiller.
    /// API describring located here http://developers.pdffiller.com/#fill-request
    /// </summary>
    public class FillRequest
    {
        private readonly IApiClient _apiClient;
        private const string ApiPath = "/fill_request";

        public FillRequest(IApiClient apiClientInstance)
        {
            _apiClient = apiClientInstance;
        }

        /// <summary>
        /// Creates a fill request.
        /// </summary>
        /// <param name="newRequest">FillRequestCreateRequest object filled with fill request data.</param>
        /// <returns>Returns .</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public FillRequestCreateResponse CreateFillRequest(FillRequestCreateRequest newRequest)
        {
            var response = _apiClient.Call(ApiPath, "POST", newRequest);
            var requestInfo = _apiClient.GetResponseBody<FillRequestCreateResponse>(response);
            return requestInfo;
        }

        /// <summary>
        /// Lists fill requests.
        /// </summary>
        /// <returns>Returns fill requests list with pagination data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public FillRequestListResponse GetFillRequestsList()
        {
            var response = _apiClient.Call(ApiPath, "GET", null);
            var requestsList = _apiClient.GetResponseBody<FillRequestListResponse>(response);
            return requestsList;
        }

        /// <summary>
        /// Information about created fill request.
        /// </summary>
        /// <param name="fillRequestId">Fill request id.</param>
        /// <returns>Returns fill request object with it's data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public FillRequestCreateResponse GetFillRequestsInfo(long fillRequestId)
        {
            var response = _apiClient.Call(ApiPath, "GET", null);
            var requestInfo = _apiClient.GetResponseBody<FillRequestCreateResponse>(response);
            return requestInfo;
        }

        /// <summary>
        /// Delete fill request.
        /// </summary>
        /// <param name="fillRequestId">Fill request id.</param>
        /// <returns>Returns delete response object with deletion info.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public FillRequestDeleteResponse DeleteFillRequest(long fillRequestId)
        {
            var response = _apiClient.Call(ApiPath + "/" + fillRequestId, "DELETE", null);
            var deleteInfo = _apiClient.GetResponseBody<FillRequestDeleteResponse>(response);
            return deleteInfo;
        }

        /// <summary>
        /// Update fill request.
        /// </summary>
        /// <param name="fillReqeustId">Fill request id.</param>
        /// <param name="fillRequest">FillRequestCreateRequest object filled with data.</param>
        /// <returns>Returns updated fill request object.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public FillRequestCreateResponse UpdateFillRequest(long fillReqeustId, FillRequestCreateResponse fillRequest)
        {
            var response = _apiClient.Call(ApiPath + "/" + fillReqeustId, "PUT", fillRequest);
            var appInfo = _apiClient.GetResponseBody<FillRequestCreateResponse>(response);
            return appInfo;
        }

        /// <summary>
        /// List of all completed forms for the given fill_request.
        /// </summary>
        /// <param name="fillRequestId">Fill request id.</param>
        /// <returns>List of all completed forms for request with fillRequestId.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public FillRequestCreateResponse GetFillRequestsFilledForms(long fillRequestId)
        {
            var response = _apiClient.Call(ApiPath + "/" + fillRequestId + "/filled_form", "GET", null);
            var requestInfo = _apiClient.GetResponseBody<FillRequestCreateResponse>(response);
            return requestInfo;
        }

        /// <summary>
        /// Information about filled form.
        /// </summary>
        /// <param name="fillRequestId">Fill request id.</param>
        /// <param name="filledFormId">Filled form id.</param>
        /// <returns>Filled form for fill request with fillRequestId.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public FillRequestFilledForm GetFillRequestFilledForm(long fillRequestId, long filledFormId)
        {
            var response = _apiClient.Call(ApiPath + $"/" + fillRequestId + "/filled_form/" + filledFormId, "GET", null);
            var requestInfo = _apiClient.GetResponseBody<FillRequestFilledForm>(response);
            return requestInfo;
        }

        /// <summary>
        /// Delete fill request filled form.
        /// </summary>
        /// <param name="fillRequestId">Fill request id.</param>
        /// <param name="filledFormId">Filled form id.</param>
        /// <returns>Filled form for fill request with fillRequestId.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public FillRequestFilledFormDeleteResponse DeleteFillRequestFilledForm(long fillRequestId, long filledFormId)
        {
            var response = _apiClient.Call(ApiPath + "/" + fillRequestId + "/filled_form/" + filledFormId, "DELETE", null);
            var requestInfo = _apiClient.GetResponseBody<FillRequestFilledFormDeleteResponse>(response);
            return requestInfo;
        }

        /// <summary>
        /// Export filled form data in JSON format.
        /// </summary>
        /// <param name="fillRequestId">Fill request id.</param>
        /// <param name="filledFormId">Filled form id.</param>
        /// <returns>Filled form data in JSON format.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public string ExportFillRequestFilledForm(long fillRequestId, long filledFormId)
        {
            var response = _apiClient.Call(ApiPath + "/" + fillRequestId + "/filled_form/" + filledFormId + "/export", "GET", null);
            var exported = _apiClient.GetResponseContent(response);
            return exported;
        }

        /// <summary>
        /// Download filled PDF form.
        /// </summary>
        /// <param name="fillRequestId">Fill request id.</param>
        /// <param name="filledFormId">Filled form id.</param>
        /// <returns>Returns downloaded filled PDF form.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public byte[] DownloadFilledPDFForm(long fillRequestId, long filledFormId)
        {
            byte[] response = _apiClient.DownloadFile(ApiPath + "/" + fillRequestId + "/filled_form/" + filledFormId + "/download");
            return response;
        }
    }
}
