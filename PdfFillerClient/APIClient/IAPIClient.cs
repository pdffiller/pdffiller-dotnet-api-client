using System;
using System.Net;

namespace PdfFillerClient
{
    public interface IApiClient
    {
        HttpWebResponse Call(string urlPath, string httpMethod, Object data);
        byte[] UploadFile(string apiPath, string filePath);
        byte[] DownloadFile(string apiPath);
        T GetResponseBody<T>(HttpWebResponse response) where T : class;
        T GetResponseBody<T>(byte[] responseArray) where T : class;
        string GetResponseContent(HttpWebResponse response);
        bool IsAuthenticated();
    }
}
