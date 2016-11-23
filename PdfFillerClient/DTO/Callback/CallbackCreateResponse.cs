using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.Callback
{
    public class CallbackCreateResponse
    {
        public long id { get; set; }
        public long document_id { get; set; }
        public string event_id { get; set; }
        public string callback_url { get; set; }

        public override string ToString()
        {
            return $"id={id}, document_id={document_id}, event_id={event_id}, callback_url={callback_url}";
        }
    }
}
