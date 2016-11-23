using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.SignatureRequest
{
    public class AdditionalDocument
    {
        public long id { get; set; }
        public string filename { get; set; }
        public string ip { get; set; }
        public long date_created { get; set; }
        public string document_request_notification { get; set; }
    }
}
