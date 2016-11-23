namespace PdfFillerClient.DTO.Token
{
    public class TokenDeleteResponse
    {
        public long total { get; set; }
        public string message { get; set; }

        public override string ToString()
        {
            return $"total={total}, message={message}";
        }
    }
}
