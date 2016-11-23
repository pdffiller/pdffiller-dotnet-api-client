using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.FillRequest
{
    public class FillRequest
    {
        public long id { get; set; }
        public long document_id { get; set; }
        public string document_name { get; set; }
        public string access { get; set; }
        public string status { get; set; }
        public bool email_required { get; set; }
        public bool allow_downloads { get; set; }
        public string redirect_url { get; set; }
        public bool name_required { get; set; }
        public string custom_message { get; set; }
        public long filled_forms_count { get; set; }
        public List<FillRequestEmail> notification_emails { get; set; }
        public string url { get; set; }
        public bool required_fields { get; set; }
        public long custom_logo_id { get; set; }
        public bool enforce_welcome_agreement { get; set; }
        public string[] additional_documents { get; set; }
        public bool reusable { get; set; }
    }

    public class FillRequestEmail
    {
        public string email { get; set; }
        public string name { get; set; }
    }
}
