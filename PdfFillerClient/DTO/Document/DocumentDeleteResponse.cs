using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.Document
{
    public class DocumentDeleteResponse
    {
        public long total { get; set; }
        public string message { get; set; }

        public override string ToString()
        {
            return $"total={total}, message={message}";
        }
    }
}
