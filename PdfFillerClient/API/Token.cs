using System.Net;
using PdfFillerClient.DTO.Token;
using PdfFillerClient.Exceptions;

namespace PdfFillerClient.API
{
    /// <summary>
    /// API service for work with tokens.
    /// "/token" is a hash which can be used to track a created fill request. 
    /// Token can be added to URL to uniquely identify the document recipient. 
    /// One fill request can have many tokens as they are a part of the fill request URL.
    /// API describring located here http://developers.pdffiller.com/#token
    /// </summary>
    public class Token
    {
        private readonly IApiClient _apiClient;
        private const string ApiPath = "/token";

        public Token(IApiClient apiClientInstancee)
        {
            _apiClient = apiClientInstancee;
        }

        /// <summary>
        /// Creates the token with custom data.
        /// </summary>
        /// <param name="newTokenRequest">Filled TokenCreateRequest object.</param>
        /// <returns>Returns created token info with additional data after create.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public TokenCreateResponse CreateToken(TokenCreateRequest newTokenRequest)
        {
            var response = _apiClient.Call(ApiPath, "POST", newTokenRequest);
            var tokenInfo = _apiClient.GetResponseBody<TokenCreateResponse>(response);
            return tokenInfo;
        }

        /// <summary>
        /// Lists existing tokens.
        /// </summary>
        /// <returns>Returns tokens list with pagination data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public TokensListResponse GetTokensList()
        {
            var response = _apiClient.Call(ApiPath, "GET", null);
            var tokensList = _apiClient.GetResponseBody<TokensListResponse>(response);
            return tokensList;
        }

        /// <summary>
        /// Retrieves  custom data from the token.
        /// </summary>
        /// <param name="tokenId">Token id.</param>
        /// <returns>Returns token object with it's data.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public TokenCreateResponse GetTokenInfo(long tokenId)
        {
            var response = _apiClient.Call(ApiPath + "/" + tokenId, "GET", null);
            var tokenInfo = _apiClient.GetResponseBody<TokenCreateResponse>(response);
            return tokenInfo;
        }

        /// <summary>
        /// Updates custom token data by its identifier.
        /// </summary>
        /// <param name="tokenId">Id of token.</param>
        /// <param name="token">TokenCreateRequest object filled with data.</param>
        /// <returns>Returns updated token object.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public TokenCreateResponse UpdateToken(long tokenId, TokenCreateRequest token)
        {
            var response = _apiClient.Call(ApiPath + "/" + tokenId, "PUT", token);
            var updatedTokenInfo = _apiClient.GetResponseBody<TokenCreateResponse>(response);
            return updatedTokenInfo;
        }

        /// <summary>
        /// Removes the token.
        /// </summary>
        /// <param name="tokenId">Token id.</param>
        /// <returns>Returns delete response object with deletion info.</returns>
        /// <exception cref="PdfFillerApiException">If api request went bad.</exception>
        /// <exception cref="PdfFillerAppException">If client app crashed.</exception>
        public TokenDeleteResponse DeleteToken(long tokenId)
        {
            var response = _apiClient.Call(ApiPath + "/" + tokenId, "DELETE", null);
            var deleteInfo = _apiClient.GetResponseBody<TokenDeleteResponse>(response);
            return deleteInfo;
        }
    }
}
