namespace PdfFillerClient.DTO.Application
{
    public class ApplicationCreateRequest
    {
        public string name { get; set; }
        public string description { get; set; }
        public string domain { get; set; }

        public override string ToString()
        {
            return $"name={name}, description={description}, domain={domain}";
        }
    }
}
