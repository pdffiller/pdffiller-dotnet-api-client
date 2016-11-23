using PdfFillerClient.APIClient;
using PdfFillerClient.Exceptions;
using PdfFillerClient.API;

namespace PdfFillerClient
{
    public sealed class PdfFillerApiClient
    {
        public User User;
        public Application Application;
        public Token Token;
        public Document Document;
        public Callback Callback;
        public FillableTemplate FillableTemplate;
        public SignatureRequest SignatureRequest;
        public FillRequest FillRequest;
        public CustomLogo CustomLogo;

        private readonly IApiClient _client;

        public PdfFillerApiClient(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new PdfFillerAppException("Can't create pdf filler api client without API key!");

            _client = new ClientWrapper(apiKey);
            InitApis();
        }

        public PdfFillerApiClient(string clientId, string clientSecret)
        {
            if (string.IsNullOrEmpty(clientId))
                throw new PdfFillerAppException("Can't create pdf filler api client without client id!");

            if (string.IsNullOrEmpty(clientSecret))
                throw new PdfFillerAppException("Can't create pdf filler api client without client secret!");

            _client = new ClientWrapper(clientId, clientSecret);
            InitApis();
        }

        private void InitApis()
        {
            User = new User(_client);
            Application = new Application(_client);
            Token = new Token(_client);
            Document = new Document(_client);
            Callback = new Callback(_client);
            FillableTemplate = new FillableTemplate(_client);
            SignatureRequest = new SignatureRequest(_client);
            FillRequest = new FillRequest(_client);
            CustomLogo = new CustomLogo(_client);
        }

        public bool IsAuthenticated()
        {
            return _client.IsAuthenticated();
        }
    }
}
