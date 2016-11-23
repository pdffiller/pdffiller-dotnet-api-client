using System;

namespace PdfFillerClient.DTO.Errors
{
    public class Error
    {
        public string id { get; set; }
        public string message { get; set; }

        public override string ToString()
        {
            return $"id={id}, message={message}";
        }
    }
}
