using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfFillerClient.DTO.FillableTemplate
{
    public class FillableTemplatePopulateRequest
    {
        /// <summary>
        /// Identifier of previously created document with fillable fields.
        /// </summary>
        public long document_id { get; set; }

        /// <summary>
        /// Fields with key-value pair for filleable template.
        /// </summary>
        public Dictionary<string, string> fillable_fields = new Dictionary<string, string>();

        public override string ToString()
        {
            return $"document_id={document_id}, fillable_fields={fillable_fields.Count}";
        }
    }
}
