using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.SignatureRequest
{
    public class SignatureRequestDeleteResponse
    {
        public long total { get; set; }
        public string message { get; set; }

        public override string ToString()
        {
            return $"total={total}, message={message}";
        }
    }
}
