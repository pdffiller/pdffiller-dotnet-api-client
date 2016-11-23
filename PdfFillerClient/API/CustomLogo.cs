using System.Net;
using PdfFillerClient.DTO.CustomLogo;
using PdfFillerClient.Exceptions;

namespace PdfFillerClient.API
{
    /// <summary>
    /// API service for work with custom logo.
    /// "/custom_logo" allows to upload custom logos, which can be used as а LinkToFill logo.
    /// Attached logo will be shown in the left upper corner of the editor while the recipient is editing the LinkToFill document.
    /// API describring located here http://developers.pdffiller.com/#custom-logo
    /// </summary>
    public class CustomLogo
    {
        private readonly IApiClient _apiClient;
        private const string ApiPath = "/custom_logo";

        public CustomLogo(IApiClient apiClientInstance)
        {
            _apiClient = apiClientInstance;
        }

        /// <summary>
        /// Uploads custom logo to PDFfiller.
        /// Image to use as the custom logo. Can be .jpeg, .jpg, .gif or .png extension image.
        /// </summary>
        /// <param name="filePath">Path to logo for upload.</param>
        /// <returns>Returns uploaded logo info data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public CustomLogoCreateResponse UploadCustomLogo(string filePath)
        {
            byte[] response = _apiClient.UploadFile(ApiPath, filePath);
            var logoInfo = _apiClient.GetResponseBody<CustomLogoCreateResponse>(response);
            return logoInfo;
        }

        /// <summary>
        /// Uploads custom logo by URL or file base64 string.
        /// </summary>
        /// <param name="newFile">CustomLogoCreateRequest object filled with file URL or file base64 string.</param>
        /// <returns>Returns uploaded custom logo info object.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public CustomLogoCreateResponse UploadCustomLogo(CustomLogoCreateRequest newFile)
        {
            var response = _apiClient.Call(ApiPath, "POST", newFile);
            var logoInfo = _apiClient.GetResponseBody<CustomLogoCreateResponse>(response);
            return logoInfo;
        }

        /// <summary>
        /// Lists uploaded custom logos.
        /// </summary>
        /// <returns>Returns list with uploaded logos data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public CustomLogoListResponse GetCustomLogosList()
        {
            var response = _apiClient.Call(ApiPath, "GET", null);
            var customLogosList = _apiClient.GetResponseBody<CustomLogoListResponse>(response);
            return customLogosList;
        }

        /// <summary>
        /// Shows information about uploaded custom logo.
        /// </summary>
        /// <param name="customLogoId">Custom logo id.</param>
        /// <returns>Returns custom logo info data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public CustomLogoCreateResponse GetCustomLogoInfo(long customLogoId)
        {
            var response = _apiClient.Call(ApiPath + "/" + customLogoId, "GET", null);
            var customLogoInfo = _apiClient.GetResponseBody<CustomLogoCreateResponse>(response);
            return customLogoInfo;
        }

        /// <summary>
        /// Deletes custom logo.
        /// </summary>
        /// <param name="customLogoId">Custom logo id.</param>
        /// <returns>Returns delete response object with deletion info.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public CustomLogoDeleteResponse DeleteCustomLogo(long customLogoId)
        {
            var response = _apiClient.Call(ApiPath + "/" + customLogoId, "DELETE", null);
            var deleteInfo = _apiClient.GetResponseBody<CustomLogoDeleteResponse>(response);
            return deleteInfo;
        }
    }
}
