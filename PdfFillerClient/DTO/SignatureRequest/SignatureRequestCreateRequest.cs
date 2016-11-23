using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.SignatureRequest
{
    public class SignatureRequestCreateRequest
    {
        /// <summary>
        /// ID of the document used for signature request.
        /// </summary>
        public long document_id { get; set; }

        /// <summary>
        /// Signature request method.
        /// sendtoeach - a document is to be signed individually.
        /// sendtogroup – the same document is to be signed by multiple recipients. 
        /// [sendtoeach|sendtogroup]
        /// </summary>
        public string method { get; set; }

        /// <summary>
        ///  Optional. Name of the signature request. Used only with sendtogroup method.
        /// </summary>
        public string envelope_name { get; set; }

        /// <summary>
        ///  Optional. Security PIN required to open the document. Used only with sendtogroup method. [standard|enhanced]
        /// </summary>
        public string security_pin { get; set; }

        /// <summary>
        /// Optional. Request to sign in specific order. Used only with sendtogroup method.
        /// </summary>
        public string sign_in_order { get; set; }

        /// <summary>
        /// Recipients.
        /// </summary>
        public List<RecepientRequest> recipients { get; set; }
    }

    public class RecepientRequest : Recipient
    {
        
    }
}
