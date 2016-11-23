using System.Net;
using PdfFillerClient.DTO.Callback;
using PdfFillerClient.Exceptions;

namespace PdfFillerClient.API
{
    /// <summary>
    /// API service for work with tokens.
    /// "/callback" notifies the owner about specific actions associated with a document.
    /// API describring located here http://developers.pdffiller.com/#callback
    /// </summary>
    public class Callback
    {
        private IApiClient apiClient;
        private const string API_PATH = "/callback";

        public Callback(IApiClient apiClientInstancee)
        {
            apiClient = apiClientInstancee;
        }

        /// <summary>
        /// Registers the callback for fill of signature request by the document identifier.
        /// </summary>
        /// <param name="newCallbackRequest">Filled CallbackCreateRequest object.</param>
        /// <returns>Returns created document object info with additional data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public CallbackCreateResponse CreateCallback(CallbackCreateRequest newCallbackRequest)
        {
            var response = apiClient.Call(API_PATH, "POST", newCallbackRequest);
            var callbackInfo = apiClient.GetResponseBody<CallbackCreateResponse>(response);
            return callbackInfo;
        }

        /// <summary>
        /// Lists all created callbacks.
        /// </summary>
        /// <returns>Returns callbacks list with pagination data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public CallbacksListResponse GetCallbacksList()
        {
            var response = apiClient.Call(API_PATH, "GET", null);
            var callbacksList = apiClient.GetResponseBody<CallbacksListResponse>(response);
            return callbacksList;
        }

        /// <summary>
        /// Retrieves information about the callback.
        /// </summary>
        /// <param name="callbackId">Callback id.</param>
        /// <returns>Returns callback object with it's data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public CallbackCreateResponse GetCallbackInfo(long callbackId)
        {
            var response = apiClient.Call(API_PATH + "/" + callbackId, "GET", null);
            var callbackInfo = apiClient.GetResponseBody<CallbackCreateResponse>(response);
            return callbackInfo;
        }

        /// <summary>
        /// Updates the callback.
        /// </summary>
        /// <param name="callbackId">Callback id.</param>
        /// <param name="callback">CallbackCreateRequest object filled with data.</param>
        /// <returns>Returns updated callback object.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public CallbackCreateResponse UpdateCallback(long callbackId, CallbackCreateRequest callback)
        {
            var response = apiClient.Call(API_PATH + "/" + callbackId, "PUT", callback);
            var updatedCallbackInfo = apiClient.GetResponseBody<CallbackCreateResponse>(response);
            return updatedCallbackInfo;
        }

        /// <summary>
        /// Cancels a notification about fill or signature request action.
        /// </summary>
        /// <param name="callbackId">Callback id.</param>
        /// <returns>Returns delete response object with deletion info.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public CallbackDeleteResponse DeleteCallback(long callbackId)
        {
            var response = apiClient.Call(API_PATH + "/" + callbackId, "DELETE", null);
            var deleteInfo = apiClient.GetResponseBody<CallbackDeleteResponse>(response);
            return deleteInfo;
        }
    }
}
