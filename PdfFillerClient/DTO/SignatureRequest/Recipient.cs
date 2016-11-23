using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.SignatureRequest
{
    public class Recipient
    {
        /// <summary>
        /// Email address of the document recipient.
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Document recipient’s name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Document signing order. Required only when signature request method is sendtogroup.
        /// </summary>
        public long order { get; set; }

        /// <summary>
        /// Message subject.
        /// </summary>
        public string message_subject { get; set; }

        /// <summary>
        /// Message text.
        /// </summary>
        public string message_text { get; set; }

        /// <summary>
        /// full – edit and sign.
        /// signature – signature only.
        /// [full, signature]
        /// </summary>
        public string access { get; set; }

        /// <summary>
        /// Additional documents required to be attached to the signed document.
        /// </summary>
        public List<AdditionalDocument> additional_documents { get; set; }

        /*
        /// <summary>
        /// Fields.
        /// </summary>
        public Dictionary<string, string[]> fields { get; set; }
        */

        /// <summary>
        /// True if recepient should have photo, false if not.
        /// </summary>
        public bool require_photo { get; set; }
    }
}
