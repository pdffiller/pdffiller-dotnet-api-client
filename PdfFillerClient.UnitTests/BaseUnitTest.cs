using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.UnitTests
{
    public enum AuthType
    {
        ApiKey,
        ClientCode
    }

    public class BaseUnitTest
    {
        protected readonly string ApiKey;
        protected readonly string ClientId;
        protected readonly string ClientSecret;
        protected readonly string TestEmail;

        public BaseUnitTest()
        {
            ApiKey = ConfigurationManager.AppSettings["ApiKey"];
            ClientId = ConfigurationManager.AppSettings["ClientId"];
            ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            TestEmail = ConfigurationManager.AppSettings["TestEmail"];
        }

        public PdfFillerApiClient GetClientInstance(AuthType authType)
        {
            switch (authType)
            {
                case AuthType.ApiKey:
                    return new PdfFillerApiClient(ApiKey);
                case AuthType.ClientCode:
                    return new PdfFillerApiClient(ClientId, ClientSecret);
                default:
                    throw new Exception("AuthType is wrong! Choose between AuthType.ApiKey or AuthType.ClientCode");
            }
        }
    }
}
