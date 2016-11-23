using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.FillableTemplate
{
    public class FillableTemplatePopulateResponse
    {
        public long id { get; set; }
        public long document_id { get; set; }

        public override string ToString()
        {
            return $"id={id}, document_id={document_id}";
        }
    }
}
