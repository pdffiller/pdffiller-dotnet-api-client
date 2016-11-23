using System;
using System.IO;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using PdfFillerClient.DTO.Auth;
using PdfFillerClient.DTO.Errors;
using PdfFillerClient.Exceptions;

namespace PdfFillerClient.APIClient
{
    public sealed class ClientWrapper : IApiClient
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _apiKey;
        private const string ApiUrl = "https://api.pdffiller.com";
        private const string ApiVersion = "v1";
        private const string AuthPath = "/oauth/access_token";
        private const string UserAgent = "PdfFiller-dotNET-client";
        private OAuth2Response _loginData;
        private string _authHeader;

        public ClientWrapper(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            Authenticate();
        }

        public ClientWrapper(string apiKey)
        {
            _apiKey = apiKey;
            Authenticate();
        }

        public HttpWebResponse Call(string urlPath, string httpMethod, object data)
        {
            if (!IsAuthenticated() && urlPath != AuthPath)
                throw new PdfFillerAppException("Client is not authenticated!");

            HttpWebResponse response = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiUrl + "/" + ApiVersion + urlPath);

            request.Method = httpMethod;
            request.UserAgent = UserAgent;
            request.ContentType = "application/json";
            request.Accept = "application/json";

            if (IsAuthenticated())
                request.Headers.Add(HttpRequestHeader.Authorization, _authHeader);

            // Prepare data for POST request
            if (httpMethod == "POST" || httpMethod == "PUT")
            {
                /*if (data == null)
                    throw new PdfFillerAppException("Can't send POST request without data!");*/

                string jsonData = JsonConvert.SerializeObject(data);
                byte[] dataStream = Encoding.UTF8.GetBytes(jsonData);
                request.ContentLength = dataStream.Length;
                using (Stream writeStream = request.GetRequestStream())
                {
                    writeStream.Write(dataStream, 0, dataStream.Length);
                }
            }

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                var responseErrors = GetResponseBodyError((HttpWebResponse)e.Response);
                string msg = $"Error during process request on '{urlPath}' @ {httpMethod} - {e.Message}\nResponse error(s): {responseErrors.GetErrorsMsg()}";
                throw new PdfFillerApiException(msg, responseErrors, e.InnerException);
            }

            return response;
        }

        public byte[] UploadFile(string apiPath, string filePath)
        {
            if (!IsAuthenticated() && apiPath != AuthPath)
                throw new PdfFillerAppException("Client is not authenticated!");

            byte[] responseArray;
            WebClient webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.UserAgent, UserAgent);

            if (IsAuthenticated())
                webClient.Headers.Add(HttpRequestHeader.Authorization, _authHeader);

            string url = $"{ApiUrl}/{ApiVersion}{apiPath}";
            try
            {
                responseArray = webClient.UploadFile(url, filePath);
            }
            catch (WebException e)
            {
                var responseErrors = GetResponseBodyError((HttpWebResponse)e.Response);
                string msg = $"Error during download file from '{url}' - {e.Message}\nResponse error(s): {responseErrors.GetErrorsMsg()}";
                throw new PdfFillerApiException(msg, responseErrors, e.InnerException);
            }

            return responseArray;
        }

        public byte[] DownloadFile(string apiPath)
        {
            if (!IsAuthenticated() && apiPath != AuthPath)
                throw new PdfFillerAppException("Client is not authenticated!");

            byte[] responseArray;
            WebClient webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.UserAgent, UserAgent);

            if (IsAuthenticated())
                webClient.Headers.Add(HttpRequestHeader.Authorization, _authHeader);

            string url = $"{ApiUrl}/{ApiVersion}{apiPath}";
            try
            {
                responseArray = webClient.DownloadData(url);
            }
            catch (WebException e)
            {
                var responseErrors = GetResponseBodyError((HttpWebResponse)e.Response);
                string msg = $"Error during download file from '{url}' - {e.Message}\nResponse error(s): {responseErrors.GetErrorsMsg()}";
                throw new PdfFillerApiException(msg, responseErrors, e.InnerException);
            }

            return responseArray;
        }

        private void Authenticate()
        {
            // OAuth2.0 authentication
            if (!string.IsNullOrEmpty(_clientId) && !string.IsNullOrEmpty(_clientSecret))
            {
                var authData = new OAuth2Request()
                {
                    grant_type = "authorization_code",
                    client_id = _clientId,
                    client_secret = _clientSecret
                };
                var response = Call(AuthPath, "POST", authData);
                var authResponse = GetResponseBody<OAuth2Response>(response);
                _loginData = authResponse;
                _authHeader = authResponse.token_type + " " + authResponse.access_token;
            }
            // Authentication based on user api key
            else
            {
                _authHeader = "Bearer " + _apiKey;
            }
        }

        public bool IsAuthenticated()
        {
            return !string.IsNullOrEmpty(_authHeader);
        }

        public T GetResponseBody<T>(HttpWebResponse response) where T : class
        {
            T data = null;
            try
            {
                var responseBody = new StreamReader(response.GetResponseStream()).ReadToEnd();
                data = JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (Exception e)
            {
                throw new PdfFillerAppException("Can't parse response body! " + e.Message, e.InnerException);
            }

            return data;
        }

        public T GetResponseBody<T>(byte[] responseArray) where T : class
        {
            T data = null;
            try
            {
                var responseBody = Encoding.ASCII.GetString(responseArray);
                data = JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (Exception e)
            {
                throw new PdfFillerAppException("Can't parse response array! " + e.Message, e.InnerException);
            }

            return data;
        }

        private ErrorsList GetResponseBodyError(HttpWebResponse response)
        {
            ErrorsList data;
            try
            {
                var responseBody = new StreamReader(response.GetResponseStream()).ReadToEnd();
                data = JsonConvert.DeserializeObject<ErrorsList>(responseBody);
            }
            catch (Exception e)
            {
                throw new PdfFillerAppException("Can't parse body response error! " + e.Message, e.InnerException);
            }

            return data;
        }

        public string GetResponseContent(HttpWebResponse response)
        {
            string responseBody;
            try
            {
                responseBody = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception e)
            {
                throw new PdfFillerAppException("Can't get response body! " + e.Message, e.InnerException);
            }

            return responseBody;
        }
    }
}
