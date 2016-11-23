using System.Net;
using PdfFillerClient.DTO.Application;
using PdfFillerClient.Exceptions;

namespace PdfFillerClient.API
{
    /// <summary>
    /// API service for work with applications.
    /// "/application" allows to create applications containing client credentials for 
    /// integrating PDFfiller API into the application owner’s service. PDFfiller 
    /// applications use OAuth2 protocol for authenticating users.
    /// API describring located here http://developers.pdffiller.com/#applications
    /// </summary>
    public class Application
    {
        private readonly IApiClient _apiClient;
        private const string ApiPath = "/application";

        public Application(IApiClient apiClientInstancee)
        {
            _apiClient = apiClientInstancee;
        }

        /// <summary>
        /// Creates an application in PDFfiller.
        /// </summary>
        /// <param name="newAppRequest">Filled ApplicationResponse object.</param>
        /// <returns>Returns created app info with additional data after create.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public ApplicationCreateResponse CreateApp(ApplicationCreateRequest newAppRequest)
        {
            var response = _apiClient.Call(ApiPath, "POST", newAppRequest);
            var appInfo = _apiClient.GetResponseBody<ApplicationCreateResponse>(response);
            return appInfo;
        }

        /// <summary>
        /// Lists applications.
        /// </summary>
        /// <returns>Returns applications list with pagination data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public ApplicationsListResponse GetAppList()
        {
            var response = _apiClient.Call(ApiPath, "GET", null);
            var appsList = _apiClient.GetResponseBody<ApplicationsListResponse>(response);
            return appsList;
        }

        /// <summary>
        /// Shows information about the created application.
        /// </summary>
        /// <param name="appId">Application id.</param>
        /// <returns>Returns application object with it's data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public ApplicationCreateResponse GetAppInfo(string appId)
        {
            var response = _apiClient.Call(ApiPath + "/" + appId, "GET", null);
            var appInfo = _apiClient.GetResponseBody<ApplicationCreateResponse>(response);
            return appInfo;
        }

        /// <summary>
        /// Deletes the application.
        /// </summary>
        /// <param name="appId">Application id.</param>
        /// <returns>Returns delete response object with deletion info.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public ApplicationDeleteResponse DeleteApp(string appId)
        {
            var response = _apiClient.Call(ApiPath + "/" + appId, "DELETE", null);
            var deleteInfo = _apiClient.GetResponseBody<ApplicationDeleteResponse>(response);
            return deleteInfo;
        }

        /// <summary>
        /// Updates the application.
        /// </summary>
        /// <param name="appId">Id of applications.</param>
        /// <param name="app">ApplicationCreateRequest object filled with data.</param>
        /// <returns>Returns updated application object.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public ApplicationCreateResponse UpdateApp(string appId, ApplicationCreateRequest app)
        {
            var response = _apiClient.Call(ApiPath + "/" + appId, "PUT", app);
            var appInfo = _apiClient.GetResponseBody<ApplicationCreateResponse>(response);
            return appInfo;
        }
    }
}

