namespace PdfFillerClient.DTO.Application
{
    public class ApplicationDeleteResponse
    {
        public int total { get; set; }
        public string message { get; set; }

        public override string ToString()
        {
            return $"id={total}, secret={message}";
        }
    }
}
