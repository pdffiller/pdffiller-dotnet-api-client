namespace PdfFillerClient.DTO.Auth
{
    public class OAuth2Response
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }

        public override string ToString()
        {
            return $"access_token={access_token}, token_type={token_type}, expires_in={expires_in}";
        }
    }
}
