using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.Callback
{
    public class CallbackCreateRequest
    {
        /// <summary>
        /// Type of event, which must be reported by a notification. Can be either "fill_request.done" or "signature_request.done".
        /// </summary>
        public string event_id { get; set; }

        /// <summary>
        /// Notification URL.
        /// </summary>
        public string callback_url { get; set; }

        /// <summary>
        /// Document ID of the previously created fill or signature request.
        /// </summary>
        public long document_id { get; set; }

        public override string ToString()
        {
            return $"event_id={event_id}, callback_url={callback_url}, document_id={document_id}";
        }
    }
}
