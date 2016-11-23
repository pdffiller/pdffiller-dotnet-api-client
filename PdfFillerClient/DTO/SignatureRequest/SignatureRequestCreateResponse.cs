using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.SignatureRequest
{
    public class SignatureRequestCreateResponse
    {
        public List<SignatureResponse> items { get; set; }
    }

    public class SignatureResponse
    {
        public long id { get; set; }
        public long document_id { get; set; }
        public long date_created { get; set; }
        public long date_signed { get; set; }
        public string method { get; set; }
        public string status { get; set; }
        public string envelope_name { get; set; }
        public string security_pin { get; set; }
        public bool sign_in_order { get; set; }
        public List<RecepientResponse> recipients { get; set; }
    }

    public class RecepientResponse : Recipient
    {
        public long id { get; set; }
        public long user_id { get; set; }
        public string status { get; set; }
        public string ip { get; set; }
        public long date_created { get; set; }
        public long date_signed { get; set; }
    }

}
