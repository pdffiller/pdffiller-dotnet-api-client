namespace PdfFillerClient.DTO.Auth
{
    public class OAuth2Request
    {
        public string grant_type { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }

        public override string ToString()
        {
            return $"grant_type={grant_type}, client_id={client_id}, client_secret={client_secret}";
        }
    }
}
