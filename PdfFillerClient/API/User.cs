using System.Net;
using PdfFillerClient.DTO.User;
using PdfFillerClient.Exceptions;

namespace PdfFillerClient.API
{
    /// <summary>
    /// API service for work with users.
    /// API describring located here http://developers.pdffiller.com/#user
    /// </summary>
    public class User
    {
        private readonly IApiClient _apiClient;
        private const string ApiPath = "/user";

        public User(IApiClient apiClientInstancee)
        {
            _apiClient = apiClientInstancee;
        }

        /// <summary>
        /// Method for getting user info.
        /// </summary>
        /// <returns>Returns UserResponse object with user data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public UserResponse GetUserInfo()
        {
            var response = _apiClient.Call(ApiPath + "/me", "GET", null);
            var userInfo = _apiClient.GetResponseBody<UserResponse>(response);
            return userInfo;
        }
    }
}
