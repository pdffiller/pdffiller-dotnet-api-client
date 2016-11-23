namespace PdfFillerClient.DTO.Document
{
    public class DocumentCreateRequest
    {
        /// <summary>
        /// The uploaded file can be an URL, or base64 string.
        /// </summary>
        public string file { get; set; }

        public override string ToString()
        {
            return $"file={file}";
        }
    }
}
